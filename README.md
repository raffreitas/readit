# Readit

Catálogo de livros em Blazor com .NET 11 e interatividade no servidor.

## Executar

Requer o SDK .NET 11.

```powershell
dotnet run --project services/catalog/Readit.Catalog --launch-profile http
```

Acesse http://localhost:8080. O catálogo está disponível em `/` e `/livros`.

## Catálogo

- Modelo `Book` em `services/catalog/Readit.Catalog/Models/Book.cs`, com os campos ID, Name, Pages, Author, ImageUrl, Price e InStock.
- Dados de exemplo em `services/catalog/Readit.Catalog/Data/BookCatalog.cs`; ainda não há persistência ou integração com API.
- Busca por título e autor, sem diferenciar maiúsculas ou acentos; filtro de estoque e ordenação por título, preço e páginas.
- Capas ilustrativas locais em SVG, sem depender de serviços externos.
- Layout responsivo, estados de busca vazia e mensagens em português.

## Validar compilação

```powershell
dotnet build Readit.slnx
```

## Publicar na VM Linux com systemd

O catálogo escuta em HTTP na porta 8080, em todas as interfaces, diretamente pelo Kestrel.
A VM precisa do ASP.NET Core Runtime 11 compatível com a compilação. O SDK só é necessário para publicar.

Na raiz do repositório:

```powershell
dotnet publish services/catalog/Readit.Catalog -c Release -o ./publish/catalog
```

Na VM, crie o usuário e a pasta na primeira instalação:

```bash
sudo useradd --system --user-group --create-home --home-dir /var/lib/readit --shell /usr/sbin/nologin readit
sudo install -d -o readit -g readit /opt/readit/catalog
```

Copie todo o conteúdo de `publish/catalog` para `/opt/readit/catalog`, incluindo `wwwroot` e os arquivos de configuração.
Copie também `deploy/systemd/readit-catalog.service` para a VM.

Confirme o executável com `command -v dotnet` e a presença de `Microsoft.AspNetCore.App` 11.x com `dotnet --list-runtimes`.
O serviço assume `/usr/bin/dotnet`; ajuste `ExecStart` antes de instalar se o caminho for diferente.

No diretório onde recebeu o arquivo do serviço:

```bash
sudo chown -R readit:readit /opt/readit/catalog
sudo install -m 644 readit-catalog.service /etc/systemd/system/readit-catalog.service
sudo systemctl daemon-reload
sudo systemctl enable --now readit-catalog
```

Libere a entrada TCP 8080 no Security Group da EC2 para os IPs que devem acessar o catálogo e no firewall da VM, se ativo.
Para acesso pela internet, a VM também precisa de IP público e uma rota para o Internet Gateway.

Acesse `http://IP_PUBLICO_DA_VM:8080`. Para verificar na própria VM:

```bash
curl --fail http://localhost:8080/health
sudo systemctl status readit-catalog
sudo journalctl -u readit-catalog -n 100 --no-pager
```

O systemd inicia o catálogo com a VM e reinicia o processo se ele encerrar.
Para atualizar, pare o serviço, substitua os arquivos publicados e inicie novamente:

```bash
sudo systemctl stop readit-catalog
# Copie os novos arquivos publicados para /opt/readit/catalog.
sudo chown -R readit:readit /opt/readit/catalog
sudo systemctl start readit-catalog
```

O serviço de dicas de leitura é opcional e permanece desabilitado por padrão.
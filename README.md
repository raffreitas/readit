# Readit

Catálogo de livros em Blazor com .NET 11 e interatividade no servidor.

## Executar

Requer o SDK .NET 11.

```powershell
dotnet run --project catalog/Readit.Catalog --launch-profile http
```

Acesse http://localhost:5178. O catálogo está disponível em `/` e `/livros`.

## Catálogo

- Modelo `Book` em `catalog/Readit.Catalog/Models/Book.cs`, com os campos ID, Name, Pages, Author, ImageUrl, Price e InStock.
- Dados de exemplo em `catalog/Readit.Catalog/Data/BookCatalog.cs`; ainda não há persistência ou integração com API.
- Busca por título e autor, sem diferenciar maiúsculas ou acentos; filtro de estoque e ordenação por título, preço e páginas.
- Capas ilustrativas locais em SVG, sem depender de serviços externos.
- Layout responsivo, estados de busca vazia e mensagens em português.

## Validar compilação

```powershell
dotnet build Readit.slnx
```

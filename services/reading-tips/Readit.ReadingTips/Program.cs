var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/api/reading-tip", () => Results.Ok(new
{
    title = "Uma página por vez",
    message = "Reserve 15 minutos do seu dia para ler. Pequenos hábitos abrem espaço para grandes histórias."
}));

await app.RunAsync();
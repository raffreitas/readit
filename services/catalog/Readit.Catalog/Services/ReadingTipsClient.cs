using System.Text.Json;
using Microsoft.Extensions.Options;

namespace Readit.Catalog.Services;

public sealed record ReadingTip(string Title, string Message);

public sealed class ReadingTipsClient(
    HttpClient httpClient,
    IOptions<ReadingTipsOptions> options,
    ILogger<ReadingTipsClient> logger)
{
    public async Task<ReadingTip?> GetTipAsync(CancellationToken cancellationToken = default)
    {
        if (!options.Value.Enabled)
        {
            return null;
        }

        try
        {
            var tip = await httpClient.GetFromJsonAsync<ReadingTip>("api/reading-tip", cancellationToken);
            return tip is not null && !string.IsNullOrWhiteSpace(tip.Title)
                                   && !string.IsNullOrWhiteSpace(tip.Message)
                ? tip
                : null;
        }
        catch (Exception exception) when (exception is HttpRequestException or TaskCanceledException or JsonException)
        {
            logger.LogWarning(exception, "Não foi possível obter a dica de leitura.");
            return null;
        }
    }
}
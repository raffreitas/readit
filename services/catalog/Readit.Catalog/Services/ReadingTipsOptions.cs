namespace Readit.Catalog.Services;

public sealed class ReadingTipsOptions
{
    public const string SectionName = "ReadingTips";
    public bool Enabled { get; set; }
    public string BaseUrl { get; set; } = "";
}
namespace TrackYourSpendings.Application.ConfigOptions;

public class EmailOption
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; }
    public string SenderEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
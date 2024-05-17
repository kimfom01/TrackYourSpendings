namespace TrackYourSpendings.Application.ConfigOptions;

public class EmailOption
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string SenderEmail { get; set; }
    public string Password { get; set; }
}
namespace Infrastructure.Email.Settings.Base;

internal class EmailSettings(string provider, string server, int port, bool useSsl)
{
    //Properties
    public string Provider { get; set; } = provider;
    public string Server { get; set; } = server;
    public int Port { get; set; } = port;
    public bool UseSsl { get; set; } = useSsl;
}

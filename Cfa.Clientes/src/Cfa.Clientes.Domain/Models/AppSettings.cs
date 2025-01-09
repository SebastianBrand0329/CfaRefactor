namespace Cfa.Clientes.Domain.Models;

public class AppSettings
{
    private static AppSettings settings;
    public static AppSettings Settings { get => settings; set => settings = value; }
    public LoggingSettings Logging { set; get; }
}

public class LoggingSettings
{
    public string DefaultConnection { get; set; }
    public string FolderFile { get; set; }
    public string FolderDatabase { get; set; }
    public string DbName { get; set; }
    public List<string> Segments { set; get; }
}
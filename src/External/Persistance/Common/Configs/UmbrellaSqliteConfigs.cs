namespace Persistence.Common.Configs;

internal class UmbrellaSqliteConfigs
{
    public static string Database => "umbrella.db3";
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, Database);
}

namespace ViewEFCoreTemplates;

public class Data
{
   static string InstallToolTemplates()
    {
        return "dotnet new install Microsoft.EntityFrameworkCore.Templates";
    }
    static string InstallTemplatesIntoProject()
    {
        return "dotnet new ef-templates";
    }
}

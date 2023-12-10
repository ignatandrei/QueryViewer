
namespace DB2Gui.AspireHost;

public static class BlazorWebAssemblyProjectExtensions
{
    public static IResourceBuilder<ProjectResource> AddWebAssemblyProject<TProject>(
        this IDistributedApplicationBuilder builder, string name,
        IResourceBuilder<ProjectResource> api) 
        where TProject : IServiceMetadata, new()
    {
        var projectbuilder = builder.AddProject<TProject>(name);
        var p=new TProject();
        string hostApi= p.ProjectPath;
        var dir = Path.GetDirectoryName(hostApi);
        ArgumentNullException.ThrowIfNull(dir);
        var wwwroot = Path.Combine(dir, "wwwroot");
        if (!Directory.Exists(wwwroot)) {
            Directory.CreateDirectory(wwwroot);
        }
        var file = Path.Combine(wwwroot, "appsettings.json");
        if (!File.Exists(file))
            File.WriteAllText(file, "{}");
        projectbuilder =projectbuilder.WithEnvironment(ctx =>
        {
            if (api.Resource.TryGetAllocatedEndPoints(out var end))
            {
                if (end.Any())
                {
                    var fileContent = File.ReadAllText(file);
                    var dict=JsonSerializer.Deserialize<Dictionary<string,object>>(fileContent);
                    ArgumentNullException.ThrowIfNull(dict);
                    dict["HOSTAPI"] = end.First().UriString;
                    File.WriteAllText(file,JsonSerializer.Serialize(dict));
                }
                    
            }

        });
        return projectbuilder;

    }
}
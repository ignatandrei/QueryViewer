var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var hostApi = builder.Configuration["HOSTAPI"];
if (string.IsNullOrEmpty(hostApi))
{
    hostApi = builder.HostEnvironment.BaseAddress;
    var dict = new Dictionary<string, string?> { { "HOSTAPI", hostApi } };
    builder.Configuration.AddInMemoryCollection(dict.ToArray());
}

builder.Services.AddKeyedScoped("db",(sp,_) => new HttpClient { BaseAddress = new Uri(hostApi) });


builder.Services.AddSingleton(builder.Configuration);
await Task.WhenAll(
       builder.Build().RunAsync(),
          CreateData(hostApi)
             );

async Task<bool> CreateData(string x)
{
    await Task.Delay(10_000);
    Console.WriteLine("ths"+x);   
    return true;
}
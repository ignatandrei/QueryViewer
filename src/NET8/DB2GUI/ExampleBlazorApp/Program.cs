var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var hostApi = builder.Configuration["hostApi"];
if(string.IsNullOrEmpty(hostApi))
    hostApi = builder.HostEnvironment.BaseAddress;

Console.WriteLine($"hostApi: {hostApi}");
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(hostApi) });

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
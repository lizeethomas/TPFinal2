using BlazorFinalStrasbourg.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using BlazorFinalStrasbourg;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//v6
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://20.242.208.197") });

//app
builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://20.237.68.150") });


builder.Services.AddSingleton<LoginService>();

await builder.Build().RunAsync();
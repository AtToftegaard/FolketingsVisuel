using FolketingetOData;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using Simple.OData.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//builder.Services.AddSingleton(ctx =>
//{
//    var container = new Container(new Uri(uriString: OdataEndpoint));
//    return container;
//});
var OdataEndpoint = builder.Configuration["FolketingODataEndpoint"] ?? throw new ArgumentNullException("Missing OdataEndpoint from appsettings");
builder.Services.AddScoped(x => new ODataClient(OdataEndpoint));

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

await builder.Build().RunAsync();

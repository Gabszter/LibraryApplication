using LibrarianApplication.Blazor;
using LibrarianApplication.Blazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7032") });
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IBookService, BookService>();

await builder.Build().RunAsync();

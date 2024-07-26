using AIWorryJar.Components;
using AIWorryJar.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddOptions();
builder.Services.Configure<ChatGptOptions>(options =>
    {
        options.OpenAIApiKey = builder.Configuration.GetValue<string>("OPENAI_API_KEY");
        options.Prompt = builder.Configuration.GetValue<string>("OPENAI_PROMPT");
    }
); 

builder.Services.AddLogging(builder => builder.AddConsole());

builder.Services.AddSingleton<ChatGptService>();

var app = builder.Build();

var options = app.Services.GetService<IOptions<ChatGptOptions>>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

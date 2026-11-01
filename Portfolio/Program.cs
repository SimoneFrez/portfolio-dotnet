using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton(
    ProjetoService.DeArquivo(
        Path.Combine(builder.Environment.ContentRootPath, "Data", "projetos.json")));

var app = builder.Build();
// Permite publicar o site em um subcaminho (ex.: /portfolio-dotnet no GitHub Pages)
var caminhoBase = builder.Configuration["PathBase"];
if (!string.IsNullOrEmpty(caminhoBase))
{
    app.UsePathBase(caminhoBase);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

app.Run();

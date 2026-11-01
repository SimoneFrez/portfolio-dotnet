using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSingleton(
    ProjetoService.DeArquivo(
        Path.Combine(builder.Environment.ContentRootPath, "Data", "projetos.json")));
builder.Services.AddDbContext<PortfolioDbContext>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("Padrao")));

var app = builder.Build();

// Cria o banco e a tabela na primeira execução
using (var escopo = app.Services.CreateScope())
{
    escopo.ServiceProvider.GetRequiredService<PortfolioDbContext>()
        .Database.EnsureCreated();
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
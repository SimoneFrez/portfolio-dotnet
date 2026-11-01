using System.Text.Json;
using Portfolio.Models;

namespace Portfolio.Services;

public class ProjetoService
{
    private readonly IReadOnlyList<Projeto> _projetos;

    public ProjetoService(IEnumerable<Projeto> projetos)
    {
        _projetos = projetos.ToList();
    }

    public static ProjetoService DeArquivo(string caminho)
    {
        var json = File.ReadAllText(caminho);
        var opcoes = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var projetos = JsonSerializer.Deserialize<List<Projeto>>(json, opcoes) ?? new();
        return new ProjetoService(projetos);
    }

    public IReadOnlyList<Projeto> Listar() => _projetos;

    public Projeto? BuscarPorSlug(string slug) =>
        _projetos.FirstOrDefault(p => p.Slug == slug);
}
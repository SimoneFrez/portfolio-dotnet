using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages.Projetos;

public class IndexModel : PageModel
{
    private readonly ProjetoService _servico;

    public IndexModel(ProjetoService servico) => _servico = servico;

    public IReadOnlyList<Projeto> Projetos { get; private set; } = [];

    public void OnGet() => Projetos = _servico.Listar();
}
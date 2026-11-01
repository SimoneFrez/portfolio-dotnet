using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Pages.Projetos;

public class DetalheModel : PageModel
{
    private readonly ProjetoService _servico;

    public DetalheModel(ProjetoService servico) => _servico = servico;

    public Projeto Projeto { get; private set; } = default!;

    public IActionResult OnGet(string slug)
    {
        var projeto = _servico.BuscarPorSlug(slug);
        if (projeto is null)
            return NotFound();

        Projeto = projeto;
        return Page();
    }
}
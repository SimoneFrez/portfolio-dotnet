using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Pages;

public class ContatoModel : PageModel
{
    private readonly PortfolioDbContext _db;

    public ContatoModel(PortfolioDbContext db) => _db = db;

    [BindProperty]
    [Required(ErrorMessage = "Informe seu nome.")]
    [StringLength(80, ErrorMessage = "O nome deve ter até 80 caracteres.")]
    public string Nome { get; set; } = "";

    [BindProperty]
    [Display(Name = "E-mail")]
    [Required(ErrorMessage = "Informe seu e-mail.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = "";

    [BindProperty]
    [Required(ErrorMessage = "Escreva uma mensagem.")]
    [StringLength(1000, MinimumLength = 10,
        ErrorMessage = "A mensagem deve ter entre 10 e 1000 caracteres.")]
    public string Mensagem { get; set; } = "";

    public bool Enviado { get; private set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        _db.Mensagens.Add(new MensagemContato
        {
            Nome = Nome,
            Email = Email,
            Texto = Mensagem
        });
        await _db.SaveChangesAsync();

        Enviado = true;
        ModelState.Clear();
        Nome = Email = Mensagem = "";
        return Page();
    }
}
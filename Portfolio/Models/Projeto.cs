namespace Portfolio.Models;

public record Projeto(
    string Slug,
    string Titulo,
    string Resumo,
    string Linguagem,
    List<string> Tecnologias,
    string? LinkGitHub);
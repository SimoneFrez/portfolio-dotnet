using Portfolio.Models;
using Portfolio.Services;

namespace Portfolio.Testes;

public class ProjetoServiceTestes
{
    private static Projeto NovoProjeto(string slug) =>
        new(slug, "Título " + slug, "Resumo", "Java", ["Spring Boot"], null);

    [Fact]
    public void Listar_RetornaTodosOsProjetos()
    {
        var servico = new ProjetoService([NovoProjeto("a"), NovoProjeto("b")]);

        Assert.Equal(2, servico.Listar().Count);
    }

    [Fact]
    public void BuscarPorSlug_SlugExistente_RetornaOProjeto()
    {
        var servico = new ProjetoService([NovoProjeto("a"), NovoProjeto("b")]);

        var projeto = servico.BuscarPorSlug("b");

        Assert.NotNull(projeto);
        Assert.Equal("b", projeto.Slug);
    }

    [Fact]
    public void BuscarPorSlug_SlugInexistente_RetornaNulo()
    {
        var servico = new ProjetoService([NovoProjeto("a")]);

        Assert.Null(servico.BuscarPorSlug("nao-existe"));
    }

    [Fact]
    public void DeArquivo_LeOsProjetosDoJson()
    {
        var caminho = Path.GetTempFileName();
        File.WriteAllText(caminho, """
                                   [{ "slug": "x", "titulo": "X", "resumo": "R",
                                      "linguagem": "C#", "tecnologias": ["a"], "linkGitHub": null }]
                                   """);

        try
        {
            var servico = ProjetoService.DeArquivo(caminho);

            Assert.Single(servico.Listar());
            Assert.Equal("x", servico.Listar()[0].Slug);
        }
        finally
        {
            File.Delete(caminho);
        }
    }
}
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Models;

namespace Portfolio.Testes;

public class ContatoBancoTestes
{
    [Fact]
    public async Task SalvarMensagem_GravaNoBanco()
    {
        using var conexao = new SqliteConnection("DataSource=:memory:");
        conexao.Open();
        var opcoes = new DbContextOptionsBuilder<PortfolioDbContext>()
            .UseSqlite(conexao)
            .Options;

        using (var db = new PortfolioDbContext(opcoes))
        {
            db.Database.EnsureCreated();
            db.Mensagens.Add(new MensagemContato
            {
                Nome = "Ana",
                Email = "ana@exemplo.com",
                Texto = "Gostei do site!"
            });
            await db.SaveChangesAsync();
        }

        using (var db = new PortfolioDbContext(opcoes))
        {
            var mensagem = await db.Mensagens.SingleAsync();
            Assert.Equal("Ana", mensagem.Nome);
        }
    }
}
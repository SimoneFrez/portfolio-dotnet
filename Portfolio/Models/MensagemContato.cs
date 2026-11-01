namespace Portfolio.Models;

public class MensagemContato
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Email { get; set; } = "";
    public string Texto { get; set; } = "";
    public DateTime EnviadaEm { get; set; } = DateTime.UtcNow;
}
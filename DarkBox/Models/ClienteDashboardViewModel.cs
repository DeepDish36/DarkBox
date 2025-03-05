using DarkBox.Models;

public class ClienteDashboardViewModel
{
    public string NomeUsuario { get; set; } = "";
    public string Email { get; set; } = "";
    public string PlanoAssinatura { get; set; } = "Free";  // Se quiser exibir o plano do cliente
    public decimal ArmazenamentoUsadoMB { get; set; } = 0; // Para exibir espaço usado
    public decimal ArmazenamentoTotalMB { get; set; } = 500; // Limite baseado no plano

    public List<Project> Projetos { get; set; } = new();
    public List<Notification> Notificacoes { get; set; } = new();
}

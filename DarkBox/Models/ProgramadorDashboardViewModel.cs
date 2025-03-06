namespace DarkBox.Models
{
    public class ProgramadorDashboardViewModel
    {
        public string NomeUsuario { get; set; }
        public List<Project> Projetos { get; set; }
        public List<Notification> Notificacoes { get; set; } = new();
    }
}

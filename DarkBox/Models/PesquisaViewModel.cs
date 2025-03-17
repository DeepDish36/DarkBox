namespace DarkBox.Models
{
    public class PesquisaViewModel
    {
        public string Query { get; set; }
        public List<User> Users { get; set; }
        public List<Project> Projetos { get; set; }

        public List<Notification> Notificacoes { get; set; } = new List<Notification>();
    }
}

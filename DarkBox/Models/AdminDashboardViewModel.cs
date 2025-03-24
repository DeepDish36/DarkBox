namespace DarkBox.Models
{
    public class AdminDashboardViewModel
    {
        public string NomeUsuario { get; set; } = string.Empty;
        public int TotalUsuarios { get; set; }
        public int TotalProjetos { get; set; }
        public int ProjetosPendentes { get; set; }
        public int ProjetosConcluidos { get; set; }
        public List<ProjectRequest> Projetos { get; set; } = new();
    }
}

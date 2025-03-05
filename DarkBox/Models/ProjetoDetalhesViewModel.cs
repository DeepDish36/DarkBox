using System;
using System.Collections.Generic;

namespace DarkBox.Models
{
    public class ProjetoDetalhesViewModel
    {
        public int ProjectID { get; set; }
        public DateTime UploadedAt { get; set; }
        public string? ReadmeHtml { get; set; }
        public List<ProjectFile> Arquivos { get; set; } = new List<ProjectFile>();
        public List<Notification> Notificacoes { get; set; } = new List<Notification>();
        public List<Project> Projetos { get; set; } = new List<Project>();
    }
}



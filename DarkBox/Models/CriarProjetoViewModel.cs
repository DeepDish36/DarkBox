using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DarkBox.Models
{
    public class CriarProjetoViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public string? Readme { get; set; }

        public List<IFormFile>? Arquivos { get; set; }
    }
}

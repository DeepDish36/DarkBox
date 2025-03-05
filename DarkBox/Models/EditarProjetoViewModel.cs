using DarkBox.Models;
using System.ComponentModel.DataAnnotations;

public class EditarProjetoViewModel
{
    public int ProjectId { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres.")]
    public string Title { get; set; }

    [Required]
    [StringLength(1000, ErrorMessage = "A descrição não pode ter mais de 1000 caracteres.")]
    public string Description { get; set; }

    public DateTime? UploadedAt { get; set; }

    public List<Notification> Notificacoes { get; set; } = new();
}

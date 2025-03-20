using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DarkBox.Models;

public partial class ProjectRequest
{
    public int RequestId { get; set; }

    public int ClientId { get; set; }

    [Required(ErrorMessage = "O título do pedido é obrigatório.")]
    public string RequestedTitle { get; set; } = null!;

    [Required(ErrorMessage = "A descrição do pedido é obrigatória.")]
    public string RequestedDescription { get; set; } = null!;

    public string? Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User Client { get; set; } = null!;

    public int? DeveloperId { get; set; }
    public virtual User? Developer { get; set; }
}

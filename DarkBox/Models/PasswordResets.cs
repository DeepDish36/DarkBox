using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DarkBox.Models
{
    public class PasswordResets
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserID { get; set; }

        public string? Token { get; set; }

        public DateTime Expiration { get; set; }

        [ForeignKey("UserID")]
        public virtual User? User { get; set; }
    }
}

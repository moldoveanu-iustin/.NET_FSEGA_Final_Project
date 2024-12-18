using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Notificare")]
    public class Notificare
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; } // FK la AspNetUsers
        public IdentityUser User { get; set; } //
        [Required]
        [MaxLength(500)]
        public string Descriere { get; set; } 
        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // "Citita" sau "Necitita"
        [Required]
        public DateTime OraTrimitere { get; set; }
    }
}

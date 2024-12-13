using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Firma")]
    public class Firma
    {
        [Column("ID_Firma")]
        public int Id { get; set; }
        [Column("Nume_Firma")]
        public string Nume { get; set; }
        [Column("Adresa")]
        public string Adresa { get; set; }
        [Column("Telefon")]
        public string Telefon { get; set; }

        [Column("Email")]
        public string Email { get; set; }
        [Column("CUI")]
        public string Cui {  get; set; }
    }
}

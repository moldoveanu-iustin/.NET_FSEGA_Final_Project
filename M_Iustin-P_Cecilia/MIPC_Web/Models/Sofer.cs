using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Soferi")]
    public class Sofer
    {
        public string Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string PozaProfil { get; set; }
        // Relatie cu Masina
        public int MasinaId { get; set; }
        public Masina Masina { get; set; }
    }
}

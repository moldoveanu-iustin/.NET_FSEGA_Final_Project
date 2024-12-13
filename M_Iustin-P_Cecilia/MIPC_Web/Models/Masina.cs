using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Masini")]
    public class Masina
    {
        public int Id { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public int AnFabricatie { get; set; }
        public string NumarInmatriculare { get; set; }
        public decimal PretPerKm { get; set; }

        // Relație cu Firma
        public int FirmaId { get; set; }
        public Firma Firma { get; set; }
    }
}

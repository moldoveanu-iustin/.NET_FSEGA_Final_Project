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

        // Relatie cu Firma
        public int FirmaId { get; set; }
        public Firma Firma { get; set; }

        public string imagine1 { get; set; }
        public string imagine2 { get; set; }
        public bool PreluataDeSofer { get; set; }
    }
}

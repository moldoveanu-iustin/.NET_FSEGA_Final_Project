using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Rezervari")]
    public class Rezervare
    {
        [Column("ID_Firma")]
        public int Id { get; set; }
        public DateTime DataRezervare { get; set; }
        public DateTime DataCursa { get; set; }
        public string LocatiePlecare { get; set; }
        public string LocatieSosire { get; set; }
        public decimal PretTotal { get; set; }

        // Relație cu Client
        public int ClientId { get; set; }
        public Client Client { get; set; }

        // Relație cu Sofer
        public int SoferId { get; set; }
        public Sofer Sofer { get; set; }
    }
}

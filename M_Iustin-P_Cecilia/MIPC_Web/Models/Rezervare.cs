using Microsoft.Identity.Client;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Rezervare")]
    public class Rezervare
    {
        [Column("ID_Rezervare")]
        public int Id { get; set; }
        public DateTime DataRezervare { get; set; }
        public DateTime DataStartCursa { get; set; }
        public DateTime DataStopCursa { get; set; }
        public string LocatiePlecare { get; set; }
        public string LocatieSosire { get; set; }
        public decimal? DistantaTotala {  get; set; }
        public decimal PretTotal { get; set; }

        // Relatie cu AspNetUsers
        [ForeignKey("AspNetUsers")]
        public string ClientId { get; set; } 
        public Microsoft.AspNetCore.Identity.IdentityUser Client { get; set; }

        // Relatie cu Sofer
        [ForeignKey("Sofer")]
        public string SoferId { get; set; }
        public Sofer Sofer { get; set; }
        //Status Rezervare
        public string Status { get; set; } // "In asteptare", "Acceptata", "Respinsa"
    }
}

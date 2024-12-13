using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    //[Table("")]
    public class Utilizator
    {
        public string Id { get; set; }
        public string NumeUtilizator { get; set; }
        public string Email { get; set; }
        public string Parola { get; set; }
        public string Rol { get; set; } // Ex: "Admin", "Sofer", "Client"
    }
}

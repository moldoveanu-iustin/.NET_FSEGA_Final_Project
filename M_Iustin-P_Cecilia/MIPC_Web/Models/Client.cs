﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MIPC_Web.Models
{
    [Table("Clienti")]
    public class Client
    {
        public int Id { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
    }
}

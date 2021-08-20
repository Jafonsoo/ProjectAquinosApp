using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace M2UApp.Models
{
    public class ArtigosPreparados
    {
        public int Id { get; set; }
       // public DateTime date { get; set; }
        public string ArtigosDesproporcionais { get; set; }
        public int Encomendas_ID { get; set; }
        public int Armazem_ID { get; set; }
        public string User_Username { get; set; } 
    }
}
 
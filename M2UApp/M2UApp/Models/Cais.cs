using System;
using System.Collections.Generic;
using System.Text;

namespace M2UApp.Models
{
    public class Cais
    {
        public int Id { get; set; }
        public int NumeroCais { get; set; }
        public string NomeCais { get; set; }
        public int IdArmazem { get; set; }
        public Armazens Armazens { get; set; }
    }
}

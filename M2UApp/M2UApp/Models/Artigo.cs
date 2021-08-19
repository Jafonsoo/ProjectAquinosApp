using System;
using System.Collections.Generic;
using System.Text;

namespace M2UApp.Models
{
    public class Artigo
    {
        public int Id { get; set; }
        public string Referencia_Artigo { get; set; }
        public int Quantidade { get; set; }
        public int QuantidadePicado { get; set; }
        public string NumeroSerie { get; set; }
        public string QuantidadeQuantidadePicado { get { return QuantidadePicado + " / " + Quantidade; } }
    }
}

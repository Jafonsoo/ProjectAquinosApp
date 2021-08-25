using System;
using System.Collections.Generic;
using System.Text;

namespace M2UApp.Models
{
    public class ExpedicaoArtigo
    {
        public int Id { get; set; }
        public string Referencia_Artigo { get; set; }
        public int Quantidade { get; set; } 
        public int QuantidadePicado { get; set; }
        public int QuantidadeEnviado { get; set; }
        public string NumeroSerie { get; set; }
        public int Total { get; set; }
        public string NumeroEncomenda { get; set; }
        public int Armazem_ID { get; set; }
        public string Armazem_Nome { get; set; }
        public string User_Username { get; set; }
        public string Cais_Nome { get; set; }
        public string QuantidadeQuantidadePicado { get { return QuantidadePicado + " / " + Total; } }
    }
}

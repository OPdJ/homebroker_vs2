using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BolsaValor.Models
{
    public class Ativo
    {
        public int IDAtivo { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public Double Valor { get; set; }
        public string Sigla { get; set; }
    }
}
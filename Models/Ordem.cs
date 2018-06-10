using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BolsaValor.Models
{
    public class Ordem
    {
        [Required]
        public int IDOrdem { get; set; }

        public string TipoOrdem { get; set; }

        [Required]
        [Display(Name ="Data")]
        public DateTime DataOrdem { get; set; }

        [Display(Name ="Preço")]
        public Double Preco { get; set; }

        [Display(Name ="Quantidade Atual")]
        public int QtdCadastrada { get; set; }

        [Display(Name ="Quantidade Compra")]
        public int QtdVendida { get; set; }

        [Display(Name ="Ativo")]
        public int IDAtivo { get; set; }
        public IEnumerable<SelectListItem> Ativo { get; set; }
    }
}
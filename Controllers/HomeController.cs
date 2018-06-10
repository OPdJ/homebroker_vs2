using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BolsaValor.Models;

namespace BolsaValor.Controllers
{
    public class HomeController : Controller
    {

        public static List<Ativo> ativos = new List<Ativo> {
            new Ativo(){ IDAtivo = 1, Descricao="ABC BRASIL PN", Quantidade=300, Sigla="ABCB4", Valor=12 }
        };

        public static List<Ordem> ordens = new List<Ordem>();
        public static List<OrdemXAtivoModel> ordxativ = new List<OrdemXAtivoModel>();
        public static List<OrdemXAtivoModel> ordxativCompra = new List<OrdemXAtivoModel>();

        IEnumerable<OrdemXAtivoModel> historico;

        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Heading = "Ativos";

            return View();
        }

        public ActionResult Compra()
        {
            ViewBag.Heading = "Ordem - Compra";
            ViewBag.IDAtivo = new SelectList(ativos, "IDAtivo", "Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult Compra(Ordem ordem)
        {
            ViewBag.IDAtivo = new SelectList(ativos, "IDAtivo", "Descricao");
            ordem.IDOrdem = ordens.Count + 1;
            ordem.TipoOrdem = "Compra";

            ordens.Add(ordem);

            historico = (from ord in ordens
                         join ativ in ativos on ord.IDAtivo equals ativ.IDAtivo into ordxativ
                         from ativ in ordxativ.DefaultIfEmpty()
                         select new OrdemXAtivoModel { Ordem = ord, Ativo = ativ });
            ordxativ.AddRange(historico);
            ordxativCompra.AddRange(historico);

            return View("Index");
        }

        public ActionResult Venda()
        {
            ViewBag.Heading = "Ordem - Venda";
            ViewBag.IDAtivoCompra = new SelectList(ordxativCompra, "Ativo.IDAtivo", "Ativo.Descricao");

            return View();
        }

        [HttpPost]
        public ActionResult Venda(Ordem ordem)
        {
            ViewBag.IDAtivoCompra = new SelectList(ordxativCompra, "Ativo.IDAtivo", "Ativo.Descricao");
            ordem.IDOrdem = ordens.Count + 1;
            ordem.TipoOrdem = "Venda";
            ordens.Add(ordem);

            historico = (from ord in ordens
                         join ativ in ativos on ord.IDAtivo equals ativ.IDAtivo into ordxativ
                         from ativ in ordxativ.DefaultIfEmpty()
                         select new OrdemXAtivoModel { Ordem = ord, Ativo = ativ });
            ordxativ.AddRange(historico);

            return View("Index");
        }

        public ActionResult Historico()
        {
            historico = (from ord in ordens
                         join ativ in ativos on ord.IDAtivo equals ativ.IDAtivo into ordxativ
                         from ativ in ordxativ.DefaultIfEmpty()
                         select new OrdemXAtivoModel { Ordem = ord, Ativo = ativ });

            return View(historico);
        }

        // Funçoes Auxiliares
        [HttpPost]
        public ActionResult GetAtivoCompra(int? IDAtivo)
        {
            object data = ""; 
            foreach (var item in ativos)
            {
                if (item.IDAtivo == IDAtivo)
                {
                    data = item;
                    break;
                }
            }
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetAtivoVenda(int? IDAtivo)
        {
            object data = "";
            foreach (var item in ordxativ)
            {
                if (item.Ativo.IDAtivo == IDAtivo)
                {
                    data = item;
                    break;
                }
            }
            return Json(data);
        }
    }
}
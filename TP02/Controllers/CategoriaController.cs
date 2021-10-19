using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP02.DAO;
using TP02.Models;

namespace TP02.Controllers
{
    public class CategoriaController : Controller
    {
        #region GET
        public ActionResult Index()
        {
            CategoriasDAO dao = new CategoriasDAO();

            IList<Categoria> categs;

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                categs = dao.lista().ToList();
                ViewBag.Categorias = categs;
            }
            else
            {
                categs = new List<Categoria> { dao.buscaID(int.Parse(Request.QueryString["id"])) };
                ViewBag.Categorias = categs.ToList();
            }
            

            return View();
        }        

        [HttpGet]
        [ActionName("Atualizar")]
        public ActionResult Atualizar()
        {
            CategoriasDAO dao = new CategoriasDAO();
            
            Categoria categ = dao.buscaID(int.Parse(Request.QueryString["id"]));
            ViewBag.Categorias = categ;


            return View();
        }

        [HttpGet]
        [ActionName("Adicionar")]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Buscar")]
        public ActionResult Buscar()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Creditos")]
        public ActionResult Creditos()
        {
            return View();
        }
        #endregion

        #region POST
        [HttpPost]
        [ActionName("Adiciona")]
        public void Adiciona()
        {
            Categoria categ = new Categoria(Request.Form["nome"], Request.Form["desc"]);

            CategoriasDAO dao = new CategoriasDAO();

            dao.adiciona(categ);

            Response.Redirect("/Categoria");
        }

        [HttpPost]
        [ActionName("BuscaID")]
        public void BuscaID()
        {
            Response.Redirect($"/Categoria?id={Request.Form["id"]}");
        }

        [HttpPost]
        [ActionName("Atualiza")]
        public void Atualiza()
        {
            Categoria categ = new Categoria(int.Parse(Request.Form["id"]), Request.Form["nome"], Request.Form["desc"]);

            CategoriasDAO dao = new CategoriasDAO();

            dao.atualiza(categ);

            Response.Redirect("/Categoria");
        }

        [HttpPost]
        [ActionName("Exclui")]
        public void Exclui()
        {
            CategoriasDAO dao = new CategoriasDAO();

            dao.exclui(int.Parse(Request.Form["id"]));

            Response.Redirect("/Categoria");
        }
#endregion
    }
}
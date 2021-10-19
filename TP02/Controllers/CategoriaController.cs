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
        // GET: Categoria
        public ActionResult Index()
        {
            CategoriasDAO dao = new CategoriasDAO();

            if (String.IsNullOrEmpty(Request.QueryString["id"]))
            {
                IList<Categoria> categs = dao.lista().ToList();
                ViewBag.Categorias = categs;
            }
            else
            {
                Categoria categ = dao.buscaID(int.Parse(Request.QueryString["id"]));
                ViewBag.Categorias = categ;
            }
            

            return View();
        }
        

        // GET: Categoria/Atualizar
        [HttpGet]
        [ActionName("Atualizar")]
        public ActionResult Atualizar()
        {
            CategoriasDAO dao = new CategoriasDAO();

            
                Categoria categ = dao.buscaID(int.Parse(Request.QueryString["id"]));
                ViewBag.Categorias = categ;


            return View();
        }

        // GET: Categoria/Adicionar
        [HttpGet]
        [ActionName("Adicionar")]
        public ActionResult Adicionar()
        {
            return View();
        }

        // GET: Categoria/Buscar
        [HttpGet]
        [ActionName("Buscar")]
        public ActionResult Buscar()
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

            Response.Redirect("/Categorias");
        }

        [HttpPost]
        [ActionName("BuscaID")]
        public void BuscaID()
        {
            Response.Redirect($"/Categorias?id={Request.Form["id"]}");
        }

        [HttpPost]
        [ActionName("Atualiza")]
        public void Atualiza()
        {
            Categoria categ = new Categoria(int.Parse(Request.Form["id"]), Request.Form["nome"], Request.Form["desc"]);

            CategoriasDAO dao = new CategoriasDAO();

            dao.atualiza(categ);

            Response.Redirect("/Categorias");
        }

        [HttpPost]
        [ActionName("Exclui")]
        public void Exclui()
        {
            CategoriasDAO dao = new CategoriasDAO();

            dao.exclui(int.Parse(Request.QueryString["id"]));

            Response.Redirect("/Categorias");
        }
#endregion
    }
}
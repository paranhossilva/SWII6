using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP03.DAO;
using TP03.Models;

namespace TP03.Controllers
{
    public class ContainerController : Controller
    {
        #region GET
        // Listar
        public ActionResult Index()
        {
            ContainerDAO dao = new ContainerDAO();

            IList<Container> containers;

            containers = dao.lista();

            ViewBag.Containers = containers.ToList();

            return View();
        }

        [HttpGet]
        [ActionName("Adicionar")]
        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Atualizar")]
        public ActionResult Atualizar()
        {
            ContainerDAO dao = new ContainerDAO();

            Container container = dao.buscaID(int.Parse(Request.QueryString["id"]));

            ViewBag.Containers = container;

            return View();
        }
        #endregion

        #region POST
        [HttpPost]
        [ActionName("Adiciona")]
        public void Adiciona()
        {
            Container container = new Container(0, Request.Form["tipo"], float.Parse(Request.Form["tamanho"]), int.Parse(Request["numbl"]));

            ContainerDAO dao = new ContainerDAO();

            dao.adiciona(container);

            Response.Redirect("/Container");
        }

        [HttpPost]
        [ActionName("Atualiza")]
        public void Atualiza()
        {
            Container container = new Container(int.Parse(Request["id"]), Request.Form["tipo"], float.Parse(Request.Form["tamanho"]), int.Parse(Request["numbl"]));

            ContainerDAO dao = new ContainerDAO();

            dao.atualiza(container);

            Response.Redirect("/Container");
        }

        [HttpPost]
        [ActionName("Exclui")]
        public void Exclui()
        {
            ContainerDAO dao = new ContainerDAO();

            dao.exclui(int.Parse(Request.Form["id"]));

            Response.Redirect("/Container");
        }
        #endregion
    }
}
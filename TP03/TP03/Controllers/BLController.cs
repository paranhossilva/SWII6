using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP03.DAO;
using TP03.Models;

namespace TP03.Controllers
{
    public class BLController : Controller
    {

        #region GET
        //Listar
        public ActionResult Index()
        {
            BLDAO dao = new BLDAO();

            IList<BL> bls = new List<BL>();

            bls = dao.lista();

            ViewBag.BLs = bls.ToList();

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
            BLDAO dao = new BLDAO();

            BL bl = dao.buscaID(int.Parse(Request.QueryString["id"]));

            ViewBag.BLs = bl;

            return View();
        }
        #endregion

        #region POST
        [HttpPost]
        [ActionName("Adiciona")]
        public void Adiciona()
        {
            BL bl = new BL(0, int.Parse(Request.Form["consignee"]), Request.Form["navio"]);

            BLDAO dao = new BLDAO();

            dao.adiciona(bl);

            Response.Redirect("/BL");
        }

        [HttpPost]
        [ActionName("Atualiza")]
        public void Atualiza()
        {
            BL bl = new BL(int.Parse(Request.Form["id"]), int.Parse(Request.Form["consignee"]), Request.Form["navio"]);

            BLDAO dao = new BLDAO();

            dao.atualiza(bl);

            Response.Redirect("/BL");
        }

        [HttpPost]
        [ActionName("Exclui")]
        public void Exclui()
        {
            BLDAO dao = new BLDAO();

            dao.exclui(int.Parse(Request.Form["id"]));

            Response.Redirect("/BL");
        }
        #endregion
    }
}
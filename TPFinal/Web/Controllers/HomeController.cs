using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private String URI = "http://localhost:44308/api/usuario";
        private int id = -2;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Logar")]
        public async Task Logar()
        {
            Usuario usuario = new Usuario()
            {
                Id = 0,
                Nome = Request.Form["usuario"],
                Senha = Request.Form["senha"],
                Status = true
            };

            await Logar(usuario);

            if (id > 0)
            {
                Session["id"] = id;

                Response.Redirect("/Produto");
            }
            else if (id == 0)
            {
                ViewBag.Message = "Usuário ou senha incorretos";

                RedirectToAction("Index");
            }
            else
            {
                ViewBag.Message = "Falha ao conectar-se à API";

                RedirectToAction("Index");
            }
        }


        private async Task Logar(Usuario usuario)
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync($"{URI}/nome={usuario.Nome}&senha={usuario.Senha}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        Usuario item = JsonConvert.DeserializeObject<Usuario>(json);

                        if (item != null)
                            this.id = item.Id;
                        else
                            this.id = 0;
                    }
                    else
                    {                        
                        this.id = -1;
                    }
                }
            }
        }
    }
}
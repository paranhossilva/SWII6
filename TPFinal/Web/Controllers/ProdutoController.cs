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
    public class ProdutoController : Controller
    {
        List<Produto> produtos = new List<Produto>();
        private String URI = "http://localhost:44308/api/produto";

        #region GET
        //Listar
        public async Task<ActionResult> Index()
        {           
            await GetAllProdutos();
            if (produtos != null)
                ViewBag.Produtos = produtos.ToList();

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
        public async Task<ActionResult> Atualizar()
        {
            await GetProdutoById(int.Parse(Request.QueryString["id"]));

            return View();
        }
        #endregion

        #region POST
        [HttpPost]
        [ActionName("Adiciona")]
        public async Task Adiciona()
        {
            Produto produto = new Produto
            {
                Nome = Request.Form["nome"],
                Preco = float.Parse(Request.Form["preco"]),
                Status = Request.Form["status"] == "true",
                IdCad = int.Parse(Request.Form["idCad"])
            };

            AddProduto(produto);

            Response.Redirect("/Produto");
        }

        [HttpPost]
        [ActionName("Atualiza")]
        public async Task Atualiza()
        {
            Produto produto = new Produto
            {
                Id = int.Parse(Request.Form["id"]),
                Nome = Request.Form["nome"],
                Preco = float.Parse(Request.Form["preco"]),
                Status = Request.Form["status"] == "true",
                IdUp = int.Parse(Request.Form["idUp"])
            };

            UpdateProduto(produto);

            Response.Redirect("/Produto");
        }

        [HttpPost]
        [ActionName("Exclui")]
        public async Task Exclui()
        {
            DeleteProduto(int.Parse(Request.Form["id"]));

            Response.Redirect("/Produto");
        }
        #endregion

        #region Métodos Privados
        private async Task GetAllProdutos()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {                       
                        var json = await response.Content.ReadAsStringAsync();

                        produtos = JsonConvert.DeserializeObject<Produto[]>(json).ToList();
                    }
                    else
                        ViewBag.Message = "Falha ao conectar-se à API";
                }
            }
        }

        private async Task GetProdutoById(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{URI}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    Produto item = JsonConvert.DeserializeObject<Produto>(json);

                    if (item != null)
                        ViewBag.Produto = item;

                    else
                        ViewBag.Message = "Produto não encontrado";
                }
                else
                    ViewBag.Message = "Falha ao obter o Produto: " + response.StatusCode;
            }
        }

        private async void AddProduto(Produto produto)
        {
            using (var client = new HttpClient())
            {
                var result = await client.PostAsJsonAsync($"{URI}", produto);
            }
        }

        private async void UpdateProduto(Produto produto)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{URI}/{produto.Id}", produto);

                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Produto atualizado com sucesso!";
                else
                    ViewBag.Message = "Falha ao atualizar Produto: " + response.StatusCode;
            }
        }

        private async void DeleteProduto(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                HttpResponseMessage response = await client.DeleteAsync($"{URI}/{id}");

                if (response.IsSuccessStatusCode)
                    ViewBag.Message = "Produto excluído com sucesso!";
                else
                    ViewBag.Message = "Falha ao excluir o Produto : " + response.StatusCode;
            }
        }
        #endregion
    }
}
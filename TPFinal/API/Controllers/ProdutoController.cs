using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class ProdutoController : ApiController
    {
        static readonly IProdutoRepositorio repositorio = new ProdutoRepositorio();

        public IEnumerable<Produto> GetAllProdutos()
        {
            return repositorio.GetAll();
        }

        public Produto GetProduto(int id)
        {
            Produto item = repositorio.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;
        }

        public HttpResponseMessage PostProduto(Produto item)
        {
            item = repositorio.Add(item);

            var response = Request.CreateResponse<Produto>(HttpStatusCode.Created, item);

            String uri = Url.Link("DefaultApi", new { id = item.Id });

            response.Headers.Location = new Uri(uri);

            return response;
        }

        public void PutProduto(int id, Produto produto)
        {
            produto.Id = id;

            repositorio.Update(produto);
        }

        public void DeleteProduto(int id)
        {
            Produto item = repositorio.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            repositorio.Remove(id);
        }
    }
}
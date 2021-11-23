using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{    
    public class LivrosController : ApiController
    {
        static readonly ILivroRepositorio repositorio = new LivroRepositorio();

        public IEnumerable<Livro> GetAllLivros()
        {
            return repositorio.GetAll();
        }

        public Livro GetLivro(int id)
        {
            Livro item = repositorio.Get(id);

            /*if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);*/

            return item;
        }

        public IEnumerable<Livro> GetLivrosPorCategoria(String categoria)
        {
            return repositorio.GetAll().Where(
                l => String.Equals(l.Categoria, categoria, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostLivro(Livro item)
        {
            item = repositorio.Add(item);
            var response = Request.CreateResponse<Livro>(HttpStatusCode.Created, item);

            String uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);

            return response;
        }

        public void PutLivro(int id, Livro livro)
        {
            livro.Id = id;

            if (!repositorio.Update(livro))            
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public void DeleteLivro(int id)
        {
            Livro item = repositorio.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            repositorio.Remove(id);
        }
    }
}

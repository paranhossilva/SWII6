using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class UsuarioController : ApiController
    {
        static readonly IUsuarioRepositorio repositorio = new UsuarioRepositorio();

        public IEnumerable<Usuario> GetAllUsuarios()
        {
            return repositorio.GetAll();
        }
        
        public Usuario GetUsuario(int id)
        {
            Usuario item = repositorio.Get(id);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;
        }

        public Usuario Login(Usuario usuario)
        {
            Usuario item = repositorio.Login(usuario);

            if (item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return item;
        }

        public HttpResponseMessage PostUsuario(Usuario usuario)
        {
            Usuario item = usuario;

            repositorio.Add(item);

            var response = Request.CreateResponse<Usuario>(HttpStatusCode.Created, item);

            String uri = Url.Link("DefaultApi", new { id = item.Id });

            response.Headers.Location = new Uri(uri);

            return response;
        }

        public void PutUsuario(int id, Usuario usuario)
        {
            usuario.Id = id;

            repositorio.Update(usuario);
        }

        public void DeleteUsuario(int id)
        {
            Usuario item = repositorio.Get(id);

            if(item == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            repositorio.Remove(id);
        }
    }
}

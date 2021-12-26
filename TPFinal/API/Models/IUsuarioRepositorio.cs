using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public interface IUsuarioRepositorio
    {
        IEnumerable<Usuario> GetAll();
        Usuario Get(int id);
        Usuario Add(Usuario item);
        void Remove(int id);
        void Update(Usuario item);
        Usuario Login(Usuario usuario);
    }
}
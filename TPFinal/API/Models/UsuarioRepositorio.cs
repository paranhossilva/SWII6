using API.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private UsuarioDAO dao = new UsuarioDAO();

        public Usuario Add(Usuario item)
        {
            if(item == null)
                throw new ArgumentNullException("item");

            dao.Insert(item);

            return item;
        }

        public Usuario Get(int id)
        {
            return dao.SelectID(id);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return dao.SelectAll();
        }

        public Usuario Login(Usuario usuario)
        {
            return dao.Login(usuario.Nome, usuario.Senha);
        }

        public void Remove(int id)
        {
            dao.Delete(id);
        }

        public void Update(Usuario item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            dao.Update(item);
        }
    }
}
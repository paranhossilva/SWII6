using API.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private ProdutoDAO dao = new ProdutoDAO();

        public Produto Add(Produto item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            dao.Insert(item);

            return item;
        }

        public Produto Get(int id)
        {
            return dao.SelectID(id);
        }

        public IEnumerable<Produto> GetAll()
        {
            return dao.SelectAll();
        }

        public void Remove(int id)
        {
            dao.Delete(id);
        }

        public void Update(Produto item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            dao.Update(item);
        }
    }
}
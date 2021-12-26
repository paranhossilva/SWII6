using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public interface IProdutoRepositorio
    {
        IEnumerable<Produto> GetAll();
        Produto Get(int id);
        Produto Add(Produto item);
        void Remove(int id);
        void Update(Produto item);
    }
}
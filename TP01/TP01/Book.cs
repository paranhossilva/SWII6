using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01
{
    class Book
    {
        private String name;
        private List<Author> author = new List<Author>();
        private double price;
        private int qty = 0;

        #region Getter e Setters
        public String Name { get => name; }
        public List<Author> Authors { get => author; }
        public double Price { get => price; set => price = value; }
        public int Qty { get => qty; set => qty = value; }
        #endregion

        #region Contrutores
        public Book(string name, double price, int qty)
        {
            this.name = name;
            this.price = price;
            this.qty = qty;
        }
        public Book(string name, double price)
        {
            this.name = name;
            this.price = price;
        }
        #endregion

        override
        public String ToString()
        {
            String aux = "Author";

            foreach (var item in author)
            {
                aux += $"[name={item.Name}, email={item.Email}, gender={item.Gender}], ";
            }

            aux = aux.Substring(0, aux.Length - 2);

            return $"Book[name={name}, authors={aux}, price={price}, qty={qty}]";
        }

        public String getAuthorNames()
        {
            String ret = null;

            foreach (var item in author)
            {
                ret += $"{item.Name}, ";
            }

            ret = ret.Substring(0, ret.Length - 2);

            return ret;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class LivroRepositorio : ILivroRepositorio
    {
        private List<Livro> livros = new List<Livro>();
        private int _nextId = 1;

        public LivroRepositorio()
        {
            using (StreamReader file = new StreamReader(HttpContext.Current.Server.MapPath("~/Livros.json"))) {

                String json = file.ReadToEnd();
                
                file.Close();

                JsonSerializer serializer = new JsonSerializer();

                livros = JsonConvert.DeserializeObject<List<Livro>>(json);

                if (livros.Count > 0)
                    _nextId += livros.Last().Id;
            }
        }

        public Livro Add(Livro item)
        {
            if(item == null)
                throw new ArgumentNullException("Item Vazio");

            item.Id = _nextId++;
            livros.Add(item);

            Escreve();

            return item;
        }

        public Livro Get(int id)
        {
            return livros.Find(l => l.Id == id);
        }

        public IEnumerable<Livro> GetAll()
        {
            return livros;
        }

        public void Remove(int id)
        {
            livros.RemoveAll(l => l.Id == id);

            Escreve();
        }

        public bool Update(Livro item)
        {
            if (item == null)
                throw new ArgumentNullException("Item Vazio");

            int index = livros.FindIndex(l => l.Id == item.Id);
            bool ret;

            if (index < 0)
                ret = false;
            else
            {
                livros.RemoveAt(index);
                livros.Add(item);

                Escreve();

                ret = true;
            }

            return ret;
        }

        private void Escreve()
        {
            using (StreamWriter file = File.CreateText(HttpContext.Current.Server.MapPath("~/Livros.json")))
            {
                JsonSerializer serializer = new JsonSerializer();

                serializer.Serialize(file, livros);

                file.Close();
            }
        }
    }
}
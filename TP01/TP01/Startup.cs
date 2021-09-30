using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP01
{
    public class Startup
    {
        private Conn conn = new Conn();
        private Authors authors;
        private Books books;
        private String texto;
        private String links = "<br/><br/>" +
                        "<a href='/Livro/Nome'>Nome dos Livros</a><br/>" +
                        "<a href='/Livro/String'>Metodo toString()</a><br/>" +
                        "<a href='/Livro/Autor'>Nome dos Autores</a><br/>" +
                        "<a href='/Livro/ApresentarLivro'>Metodo getAuthorNames()</a><br/>" +
                        "<a href='/Creditos'>Creditos</a>";

        public void Configure(IApplicationBuilder app)
        {
            authors = conn.autor();
            books = conn.livros();
            
            app.Run(Roteamento);
        }

        public Task Roteamento(HttpContext context)
        {
            var caminhos = new Dictionary<String, RequestDelegate>
            {
                { "/Livro/Nome", Nome },
                { "/Livro/String", LivroString },
                { "/Livro/Autor", Autor },
                { "/Livro/ApresentarLivro", LivroAutor },
                { "/Creditos", Creditos }
            };

            if (caminhos.ContainsKey(context.Request.Path))
            {
                return caminhos[context.Request.Path].Invoke(context);
            }
            else
                return context.Response.WriteAsync("Página inexistente");

        }
        public Task Nome(HttpContext context)
        {
            texto = "<b>Nome dos Livros</b><br/>===============================================<br/>";

            foreach (var item in books.Lista)
            {
                texto += item.Name + "\n";
            }
			
			texto += links;
			
            return context.Response.WriteAsync(texto);
        }
        public Task LivroString(HttpContext context)
        {
            texto = "<b>Metodo toString()</b><br/>===============================================<br/>";

            foreach (var item in books.Lista)
            {
                texto += item.ToString() + "\n----------------------------------------------\n";
            }
			
			texto += links;
			
            return context.Response.WriteAsync(texto);
        }
        public Task Autor(HttpContext context)
        {
            texto = "<b>Nome dos Autores</b><br/>===============================================<br/>";

            foreach (var item in authors.Lista)
            {
                texto += item.Name + "\n----------------------------------------------\n";
            }
			
			texto += links;

            return context.Response.WriteAsync(texto);
        }
        public Task NomeAutor(HttpContext context)
        {
             texto = "<b>Metodo getAuthorNames()</b><br/>===============================================<br/>";

            foreach (var item in books.Lista)
            {
                texto += item.Name + " - " + item.getAuthorNames() + "\n----------------------------------------------\n";
            }
			
			texto += links;
			
            return context.Response.WriteAsync(texto);
        }
		
		public Task Creditos(HttpContext context)
        {
            return context.Response.WriteAsync("<b>Bruno Paranhos Silva    CB3005437</b>" + links);
        }		
    }
}

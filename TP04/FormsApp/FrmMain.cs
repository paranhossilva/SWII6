using FormsApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsApp
{
    public partial class FrmMain : Form
    {
        private List<Livro> livros = new List<Livro>();
        private String URI = "http://localhost:56781/api/livros";

        public FrmMain()
        {
            GetAllLivros();

            InitializeComponent();            
        }

        private async void GetAllLivros()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        dgvDados.Rows.Clear();

                        var LivroJson = await response.Content.ReadAsStringAsync();
                        livros = JsonConvert.DeserializeObject<Livro[]>(LivroJson).ToList();

                        foreach (var item in livros)
                        {
                            dgvDados.Rows.Add(item.Id, item.Titulo, item.Autor, item.Categoria);
                        }
                    }
                    else
                        MessageBox.Show("Não foi possível obter o livro : " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void GetLivroById(int id)
        {
            using (var client = new HttpClient())
            {                
                HttpResponseMessage response = await client.GetAsync($"{URI}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var LivroJson = await response.Content.ReadAsStringAsync();

                    Livro item = JsonConvert.DeserializeObject<Livro>(LivroJson);

                    if(item != null)
                    {
                        dgvDados.Rows.Clear();
                    
                        dgvDados.Rows.Add(item.Id, item.Titulo, item.Autor, item.Categoria);
                    }
                    else
                        MessageBox.Show("Livro não encontrado", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Falha ao obter o Livro: " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteLivro(int id) 
        {  
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                HttpResponseMessage response = await client.DeleteAsync(
                    $"{URI}/{id}");
                
                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Livro Excluído com sucesso!");
                else
                    MessageBox.Show("Falha ao excluir o Livro : " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            GetAllLivros();
        }

        private void btnRetornar_Click(object sender, EventArgs e)
        {
            GetAllLivros();
        }

        private void btnPorId_Click(object sender, EventArgs e)
        {
            int id = -1;
            String prompt = "Informe o código do Produto.";
            String titulo = "Buscar Livro por ID";            
            String resultado = Microsoft.VisualBasic.Interaction.InputBox(prompt, titulo, null, 600, 350);            
            
            if (!String.IsNullOrEmpty(resultado))
                id = Convert.ToInt32(resultado);

            if (id > 0)
                GetLivroById(id);
        }

        private void btnIncluir_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<FrmAdd>().Count() > 0)
            {
                Form add = Application.OpenForms["FrmAdd"];
                add.ShowDialog();
            }
            else
            {
                FrmAdd add = new FrmAdd();
                add.ShowDialog();
            }

            GetAllLivros();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            Livro livro = new Livro(
                int.Parse(dgvDados.CurrentRow.Cells[0].Value.ToString()),
                (String)dgvDados.CurrentRow.Cells[1].Value,
                (String)dgvDados.CurrentRow.Cells[2].Value,
                (String)dgvDados.CurrentRow.Cells[3].Value);            

            if (Application.OpenForms.OfType<FrmAtualiza>().Count() > 0)
            {
                Form atualiza = Application.OpenForms["FrmAtualiza"];
                atualiza.ShowDialog();
            }
            else
            {
                FrmAtualiza atualiza = new FrmAtualiza(livro);
                atualiza.ShowDialog();
            }

            GetAllLivros();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Você realmente deseja excluir o livro {dgvDados.CurrentRow.Cells[1].Value}?",
                "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                DeleteLivro(int.Parse(dgvDados.CurrentRow.Cells[0].Value.ToString()));

            GetAllLivros();
        }
    }
}

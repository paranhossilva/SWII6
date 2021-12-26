using Desktop.Models;
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

namespace Desktop
{
    public partial class FrmMain : Form
    {
        private List<Usuario> usuarios = new List<Usuario>();
        private String URI = "http://localhost:44308/api/usuario";

        public FrmMain()
        {
            InitializeComponent();

            GetAllUsuarios();
        }

        private async void GetAllUsuarios()
        {
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync(URI))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        dgvDados.Rows.Clear();

                        var json = await response.Content.ReadAsStringAsync();

                        usuarios = JsonConvert.DeserializeObject<Usuario[]>(json).ToList();

                        foreach (var item in usuarios)
                        {
                            dgvDados.Rows.Add(item.Id, item.Nome, item.Status ? "Ativo" : "Inativo");
                        }
                    }
                    else
                        MessageBox.Show("Não foi possível obter nenhum usuário: " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void GetUsuarioById(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{URI}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    Usuario item = JsonConvert.DeserializeObject<Usuario>(json);

                    if(item != null)
                    {
                        dgvDados.Rows.Clear();

                        dgvDados.Rows.Add(item.Id, item.Nome, item.Status ? "Ativo" : "Inativo");
                    }
                    else
                        MessageBox.Show("Usuário não encontrado", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                    MessageBox.Show("Falha ao obter o Usuário: " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void DeleteUsuario(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(URI);
                HttpResponseMessage response = await client.DeleteAsync($"{URI}/{id}");

                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Usuário excluído com sucesso!");
                else
                    MessageBox.Show("Falha ao excluir o Usuário : " + response.StatusCode, "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);                
            }

            GetAllUsuarios();
        }

        private void btnAdd_Click(object sender, EventArgs e)
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

            GetAllUsuarios();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                $"Você realmente deseja excluir o Usuário {dgvDados.CurrentRow.Cells[1].Value}?",
                "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                DeleteUsuario(int.Parse(dgvDados.CurrentRow.Cells[0].Value.ToString()));
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            Usuario usuario = new Usuario
            {
                Id = int.Parse(dgvDados.CurrentRow.Cells[0].Value.ToString()),
                Nome = (String)dgvDados.CurrentRow.Cells[1].Value,
                Status = dgvDados.CurrentRow.Cells[1].Value.ToString().Equals("Ativo")
            };

            if (Application.OpenForms.OfType<FrmAtualiza>().Count() > 0)
            {
                Form atualiza = Application.OpenForms["FrmAtualiza"];
                atualiza.ShowDialog();
            }
            else
            {
                FrmAtualiza atualiza = new FrmAtualiza(usuario);
                atualiza.ShowDialog();
            }

            GetAllUsuarios();
        }

        private void btnID_Click(object sender, EventArgs e)
        {
            int id = -1;
            String prompt = "Informe o código do Produto.";
            String titulo = "Buscar Livro por ID";
            String resultado = Microsoft.VisualBasic.Interaction.InputBox(prompt, titulo, null, 600, 350);

            if (!String.IsNullOrEmpty(resultado))
                id = Convert.ToInt32(resultado);

            if (id > 0)
                GetUsuarioById(id);
        }

        private void pctReload_Click(object sender, EventArgs e)
        {
            GetAllUsuarios();
        }
    }
}

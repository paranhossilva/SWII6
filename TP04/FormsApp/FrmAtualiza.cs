using FormsApp.Models;
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
    public partial class FrmAtualiza : Form
    {
        private Livro livro;
        private String URI = "http://localhost:56781/api/livros";

        public FrmAtualiza(Livro livro)
        {
            InitializeComponent();

            this.livro = livro;

            txtTitulo.Text = livro.Titulo;
            txtAutor.Text = livro.Autor;
            cmbSituacao.SelectedItem = livro.Categoria;
            
        }
        
        private void VerificaCampos()
        {
            btnAtualizar.Enabled = !(String.IsNullOrEmpty(txtTitulo.Text) || String.IsNullOrEmpty(txtAutor.Text) || cmbSituacao.SelectedIndex < 0);
        }

        private async void UpdateLivro()
        {
            livro.Titulo = txtTitulo.Text;
            livro.Autor = txtAutor.Text;
            livro.Categoria = cmbSituacao.Text;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{URI}/{livro.Id}", livro);

                if (response.IsSuccessStatusCode)
                    MessageBox.Show("Livro atualizado com sucesso!");
                else
                    MessageBox.Show("Falha ao atualizar o Livro : " + response.StatusCode, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Fecha();
        }

        private void Fecha()
        {
            Limpa();

            GC.Collect();

            if (Application.OpenForms.OfType<FrmMain>().Count() > 0)
            {
                Form main = Application.OpenForms["FrmMain"];
                this.Hide();
                main.Show();
            }
            else
            {
                FrmMain main = new FrmMain();
                this.Hide();
                main.Show();
            }
        }

        private void Limpa()
        {
            txtTitulo.Text = null;
            txtAutor.Text = null;
            cmbSituacao.SelectedIndex = -1;

            btnAtualizar.Enabled = false;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpa();
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            UpdateLivro();
        }

        private void FrmAtualiza_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fecha();
        }

        private void cmbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaCampos();
        }

        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }

        private void txtAutor_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }
    }
}

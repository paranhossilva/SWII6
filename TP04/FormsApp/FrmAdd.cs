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
    public partial class FrmAdd : Form
    {
        private String URI = "http://localhost:56781/api/livros";

        public FrmAdd()
        {
            InitializeComponent();
        }

        private void VerificaCampos()
        {
            btnSalvar.Enabled = !(String.IsNullOrEmpty(txtTitulo.Text) || String.IsNullOrEmpty(txtAutor.Text) || cmbSituacao.SelectedIndex < 0);
        }

        private async void AddLivro()
        {
            Livro livro = new Livro(0, txtTitulo.Text, txtAutor.Text, cmbSituacao.Text);

            using (var client = new HttpClient())
            {
                var serializado = JsonConvert.SerializeObject(livro);
                //var content = new StringContent(serializado, Encoding.UTF8, "aplication/json");
                var result = await client.PostAsJsonAsync($"{URI}/{livro.Id}", livro);
            }

            Fecha();
        }

        private void Fecha()
        {
            Limpa();

            this.Hide();
        }

        private void Limpa()
        {
            txtTitulo.Text = null;
            txtAutor.Text = null;
            cmbSituacao.SelectedIndex = -1;

            btnSalvar.Enabled = false;
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpa();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            AddLivro();
        }

        private void FrmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fecha();
        }

        private void cmbSituacao_SelectedIndexChanged(object sender, EventArgs e)
        {
            VerificaCampos();
        }

        private void txtAutor_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }

        private void txtTitulo_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }
    }
}

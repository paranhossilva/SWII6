using Desktop.Models;
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
    public partial class FrmAdd : Form
    {
        private String URI = "http://localhost:44308/api/usuario";

        public FrmAdd()
        {
            InitializeComponent();
        }

        private void VerificaCampos()
        {
            btnSalvar.Enabled = !(String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtSenha.Text));
        }

        private async void AddUsuario()
        {
            Usuario usuario = new Usuario {
                Nome = txtNome.Text,
                Senha = txtSenha.Text,
                Status = chkStatus.Checked
            };

            using (var client = new HttpClient())
            {                
                var result = await client.PostAsJsonAsync($"{URI}", usuario);
            }

            Fecha();
        }

        private void Limpa()
        {
            txtNome.Text = null;
            txtSenha.Text = null;
            chkStatus.Checked = true;
            btnSalvar.Enabled = false;
        }

        private void Fecha()
        {
            Limpa();

            this.Hide();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            AddUsuario();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            Limpa();
        }

        private void txtNome_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }

        private void txtSenha_KeyUp(object sender, KeyEventArgs e)
        {
            VerificaCampos();
        }

        private void FrmAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fecha();
        }
    }
}

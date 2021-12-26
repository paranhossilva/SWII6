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
    public partial class FrmAtualiza : Form
    {
        private Usuario usuario;
        private String URI = "http://localhost:44308/api/usuario";

        public FrmAtualiza(Usuario usuario)
        {
            InitializeComponent();

            this.usuario = usuario;

            txtNome.Text = usuario.Nome;
            chkStatus.Checked = usuario.Status;
        }

        private void VerificaCampos()
        {
            btnSalvar.Enabled = !(String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtSenha.Text));
        }

        private async void UpdateUsuario()
        {
            usuario.Nome = txtNome.Text;
            usuario.Senha = txtSenha.Text;
            usuario.Status = chkStatus.Checked;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PutAsJsonAsync($"{URI}/{usuario.Id}", usuario);

                if(response.IsSuccessStatusCode)
                    MessageBox.Show("Usuário atualizado com sucesso!");
                else
                    MessageBox.Show("Falha ao atualizar Usuário: " + response.StatusCode, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            UpdateUsuario();
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

        private void FrmAtualiza_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fecha();
        }
    }
}

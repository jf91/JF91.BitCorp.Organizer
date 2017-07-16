using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace BC_Organizer
{
    public partial class FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARDESCRICAO : Form
    {
        public OleDbConnection LigacaoDB;

        FORM_GERIR_SERVICOS_INTERNET FormGerirServicosInternet_Objects = (FORM_GERIR_SERVICOS_INTERNET)Application.OpenForms["FORM_GERIR_SERVICOS_INTERNET"];

        #region PROGRAMA

        public FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARDESCRICAO()
        {
            InitializeComponent();

            string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

            LigacaoDB = new OleDbConnection(EnderecoDB);

            TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO.Text = FormGerirServicosInternet_Objects.AUX_Descricao;
        }

        private void MENUSTRIP_GestaoServicosInternet_AdicionarDescricao_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_GestaoServicosInternet_AdicionarDescricao_GRAVAR_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarDescricao() == true)
            {
                try
                {
                    FormGerirServicosInternet_Objects.AUX_Descricao = TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO.Text;
                    MessageBox.Show("Descrição Guardada", "Informação Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO.Text != "")
                BUTTON_GestaoServicosInternet_AdicionarDescricao_GRAVAR.Enabled = true;
            
            if (TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO.Text == "")
                BUTTON_GestaoServicosInternet_AdicionarDescricao_GRAVAR.Enabled = false;
        }

        private void TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        #endregion

        #region FUNÇÕES DO FORM
/*
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··

        ╒╬═════════════════╬╕
         ║ FUNÇÕES DA FORM ║  -> FUNÇÕES DO PRÓPRIO FORM
        ╘╬═════════════════╬╛
*/

//       ╔═════════════════════════════════╗
//       ║..:    REGULAR EXPRESSIONS    :..║

        public bool REGEX_Texto_AdicionarDescricao()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");

            string AdicionarDescricao = TEXTBOX_GestaoServicosInternet_AdicionarDescricao_ADICIONARDESCRICAO.Text;

            if (ValidarTexto.IsMatch(AdicionarDescricao) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no conteúdo da Descrição. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }



        /*
        ··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
        ··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
        */
        #endregion
    }
}

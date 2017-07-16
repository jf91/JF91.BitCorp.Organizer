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
    public partial class FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARNOTA : Form
    {
        public OleDbConnection LigacaoDB;

        FORM_GERIR_SERVICOS_INTERNET FormGerirServicosInternet_Objects = (FORM_GERIR_SERVICOS_INTERNET)Application.OpenForms["FORM_GERIR_SERVICOS_INTERNET"];

        #region PROGRAMA

        public FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARNOTA()
        {
            InitializeComponent();

            string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

            LigacaoDB = new OleDbConnection(EnderecoDB);

            TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA.Text = FormGerirServicosInternet_Objects.AUX_Nota;
        }

        private void MENUSTRIP_GestaoServicosInternet_AdicionarNota_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_GestaoServicosInternet_AdicionarNota_GRAVAR_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarNota() == true)
            {
                try
                {
                    FormGerirServicosInternet_Objects.AUX_Nota = TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA.Text;
                    MessageBox.Show("Nota Guardada", "Informação Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA.Text != "")
                BUTTON_GestaoServicosInternet_AdicionarNota_GRAVAR.Enabled = true;

            if (TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA.Text == "")
                BUTTON_GestaoServicosInternet_AdicionarNota_GRAVAR.Enabled = false;
        }

        private void TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA_KeyPress(object sender, KeyPressEventArgs e)
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

        public bool REGEX_Texto_AdicionarNota()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");

            string AdicionarNota = TEXTBOX_GestaoServicosInternet_AdicionarNota_ADICIONARNOTA.Text;

            if (ValidarTexto.IsMatch(AdicionarNota) == false)
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

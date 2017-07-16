using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace BC_Organizer
{
    public partial class FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA : Form
    {
        public OleDbConnection LigacaoDB;

        FORM_GERIR_PROJECTOS FormGerirProjectos_Objects = (FORM_GERIR_PROJECTOS)Application.OpenForms["FORM_GERIR_PROJECTOS"];

        public FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA()
        {
            InitializeComponent();

            string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

            LigacaoDB = new OleDbConnection(EnderecoDB);

            TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA.Text = FormGerirProjectos_Objects.AUX_Nota;
        }

        private void MENUSTRIP_FormGestaoProjectosAdicionarNota_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_FormGestaoProjectosAdicionarNota_GRAVAR_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarNota() == true)
            {
                try
                {
                    FormGerirProjectos_Objects.AUX_Nota = TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA.Text;
                    MessageBox.Show("Nota Guardada", "Informação Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA.Text != "")
                BUTTON_FormGestaoProjectosAdicionarNota_GRAVAR.Enabled = true;

            if (TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA.Text == "")
                BUTTON_FormGestaoProjectosAdicionarNota_GRAVAR.Enabled = false;
        }

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

            string AdicionarNota = TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA.Text;

            if (ValidarTexto.IsMatch(AdicionarNota) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no conteúdo da Nota. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        private void TEXTBOX_FormGestaoProjectosAdicionarNota_ADICIONAR_NOTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        /*
        ··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
        ··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
        */
        #endregion
    }
}

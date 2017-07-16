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
    public partial class FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO : Form
    {
        public OleDbConnection LigacaoDB;

        FORM_GERIR_ENCOMENDAS FormGerirEncomendas_Objects = (FORM_GERIR_ENCOMENDAS)Application.OpenForms["FORM_GERIR_ENCOMENDAS"];

        public FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO()
        {
            InitializeComponent();

            string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

            LigacaoDB = new OleDbConnection(EnderecoDB);

            TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO.Text = FormGerirEncomendas_Objects.AUX_Descricao;
        }

        private void _FormGestaoDeEncomendasAlterarDescricao_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_FormGestaoDeEncomendasAlterarDescricao_GRAVAR_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarDescricao() == true)
            {
                try
                {
                    FormGerirEncomendas_Objects.AUX_Descricao = TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO.Text;
                    MessageBox.Show("Descrição Guardada", "Informação Guardada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO_TextChanged(object sender, EventArgs e)
        {
            if (TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO.Text != "")
                BUTTON_FormGestaoDeEncomendasAlterarDescricao_GRAVAR.Enabled = true;

            if (TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO.Text == "")
                BUTTON_FormGestaoDeEncomendasAlterarDescricao_GRAVAR.Enabled = false;
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

        public bool REGEX_Texto_AlterarDescricao()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");

            string AdicionarDescricao = TEXTBOX_FormGestaoDeEncomendasAlterarDescricao_ALTERAR_DESCRICAO.Text;

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

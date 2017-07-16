using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

namespace BC_Organizer
{
    public partial class FORM_GERIR_NOTAS : Form
    {
        #region OBJECTOS PUBLICOS

        public OleDbConnection LigacaoDB;

        public OleDbDataReader Reader;

        public string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        #endregion

        #region PROGRAMA

        public FORM_GERIR_NOTAS()
        {
            InitializeComponent();;

            LigacaoDB = new OleDbConnection(EnderecoDB);

            Refresh_ListBox_TagPageRemoverNota();

            Refresh_ListBox_TabPageAlterarNota();            
        }

        private void PICTUREBOX_FormGestaoNotas_TabPageGestaoNotas_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 1;
        }

        private void LABEL_FormGestaoNotas_TabPageGestaoNotas_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 1;
        }

        private void PICTUREBOX_FormGestaoNotas_TabPageGestaoNotas_REMOVER_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 2;
        }

        private void LABEL_FormGestaoNotas_TabPageGestaoNotas_REMOVER_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 2;
        }

        private void PICTUREBOX_FormGestaoNotas_TabPageGestaoNotas_ALTERAR_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 3;
        }

        private void LABEL_FormGestaoNotas_TabPageGestaoNotas_ALTERAR_NOTA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoNotas.SelectedIndex = 3;
        }

        private void MENUSTRIP_FormGestaoNotas_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME_TextChanged(object sender, EventArgs e)
        {
        }

        private void BUTTON_FormGestaoNotas_TabPageAdicionarNota_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarNota() == true)
            {
                AdicionarNota();

                Refresh_ListBox_TagPageRemoverNota();

                Refresh_ListBox_TabPageAlterarNota();

                FormInicio_Objects.Refresh_ListBox_NOTAS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                TABCONTROL_FormGestaoNotas.SelectedIndex = 0;
            }
        }

        private void LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_NOTA_TabPageRemover_SeleccaoItem();

            if (TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text != "")
                BUTTON_FormGestaoNotas_TabPageRemoverNota_REMOVER.Enabled = true;
        }

        private void LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_NOTAS_TabPageAlterar_SeleccaoItem();

            if (TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text != "")
                BUTTON_FormGestaoNotas_TabPageAlterarNota_GRAVAR_ALTERACOES.Enabled = true;
        }

        private void BUTTON_FormGestaoNotas_TabPageRemoverNota_REMOVER_Click(object sender, EventArgs e)
        {
            RemoverNota();

            Refresh_ListBox_TagPageRemoverNota();

            Refresh_ListBox_TabPageAlterarNota();

            FormInicio_Objects.Refresh_ListBox_NOTAS();

            FormInicio_Objects.Limpar_Labels_AUX();

            Limpar_Textbox();

            BUTTON_FormGestaoNotas_TabPageRemoverNota_REMOVER.Enabled = false;
        }

        private void BUTTON_FormGestaoNotas_TabPageAlterarNota_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarNota() == true)
            {
                AlterarNota();

                Refresh_ListBox_TagPageRemoverNota();

                Refresh_ListBox_TabPageAlterarNota();

                FormInicio_Objects.Refresh_ListBox_NOTAS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                BUTTON_FormGestaoNotas_TabPageAlterarNota_GRAVAR_ALTERACOES.Enabled = false;
            }
        }

        private void TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA_KeyPress(object sender, KeyPressEventArgs e)
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
//       ║..:   ACTUALIZAR LISTBOX's    :..║

         public void Refresh_ListBox_TagPageRemoverNota()
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();               
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_TabPageAlterarNota()
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Add(Reader[0].ToString());                    
                }
                Reader.Close();                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:       GERIR NOTAS         :..║

        public void AdicionarNota()
        {
            LigacaoDB.Open();

            try
            {
                if (TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text != "")
                {
                    try
                    {
                        OleDbCommand Command_AdicionarNota = new OleDbCommand();

                        string Query_AdicionarEncomenda = "INSERT INTO Notas(Nome, Nota) VALUES('" + TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text + "','" + TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text + "');";

                        Command_AdicionarNota.CommandText = Query_AdicionarEncomenda;
                        Command_AdicionarNota.Connection = LigacaoDB;

                        Command_AdicionarNota.ExecuteNonQuery();

                        TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
                        TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

                        MessageBox.Show("Nota Inserida com Sucesso!", "Nota Adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }

                    catch (Exception EX)
                    { }
                }

                else
                    MessageBox.Show("Tem de indicar pelo menos um nome para a nota!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void RemoverNota()
        {
            LigacaoDB.Open();

            try
            {
                string Query_DeleteNota = "DELETE FROM Notas WHERE Nome = '" + TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text + "'";

                OleDbCommand Command_RemoverNota = new OleDbCommand(Query_DeleteNota, LigacaoDB);

                Command_RemoverNota.CommandText = Query_DeleteNota;
                Command_RemoverNota.Connection = LigacaoDB;

                Command_RemoverNota.ExecuteNonQuery();               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void AlterarNota()
        {
            LigacaoDB.Open();

            try
            {
                string Query_AlterarNota = "UPDATE Notas SET Nome = '" + TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text + "', Nota = '" + TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text + "' WHERE Nome = '" + LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.SelectedItem.ToString() + "'";

                OleDbCommand Command_AlterarNota = new OleDbCommand();

                Command_AlterarNota.CommandText = Query_AlterarNota;
                Command_AlterarNota.Connection = LigacaoDB;

                Command_AlterarNota.ExecuteNonQuery();

                MessageBox.Show("Nota Alterada com Sucesso!", "Nota Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:    PREENCHER TEXTBOX'S    :..║

        public void ListBox_NOTA_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Notas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Notas WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
                }              
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_NOTAS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Notas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Notas WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
                }               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:     LIMPAR TEXTBOX's      :..║

        public void Limpar_Textbox()
        {
            try
            {
                TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
                TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

                TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = "";
                TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = "";

                TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = "";
                TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = "";
            }

            catch(Exception EX)
            { }
        }

//       ╔═════════════════════════════════╗
//       ║..:    REGULAR EXPRESSIONS    :..║
                
        public bool REGEX_Texto_AdicionarNota()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");

            string AdicionarEncomenda_NOME = TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text;
            string AdicionarEncomenda_CONTEUDO = TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text;

            if (ValidarTexto.IsMatch(AdicionarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_CONTEUDO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nota. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        public bool REGEX_Texto_AlterarNota()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");

            string AlterarEncomenda_NOME = TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text; ;
            string AlterarEncomenda_CONTEUDO = TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text;

            if (ValidarTexto.IsMatch(AlterarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_CONTEUDO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nota. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
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

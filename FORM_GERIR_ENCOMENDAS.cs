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
    public partial class FORM_GERIR_ENCOMENDAS : Form
    {
        #region OBJECTOS PUBLICOS

        public OleDbConnection LigacaoDB;

        public OleDbDataReader Reader;

        public string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        public string AUX_Descricao;
        public string AUX_Nota;

        #endregion

        #region PROGRAMA

        public FORM_GERIR_ENCOMENDAS()
        {
            InitializeComponent();             

            LigacaoDB = new OleDbConnection(EnderecoDB);

            Refresh_ListBox_TagPageRemoverEncomenda();

            Refresh_ListBox_TabPageAlterarEncomenda();   
        }

        private void PICTUREBOX_FormGerirEncomendas_ADICIONAR_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 1;
        }

        private void LABEL_FormGerirEncomendas_ADICIONAR_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 1;
        }

        private void PICTUREBOX_FormGerirEncomendas_REMOVER_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 2;
        }

        private void LABEL_FormGerirEncomendas_REMOVER_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 2;
        }

        private void PICTUREBOX__FormGerirEncomendas_ALTERAR_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 3;
        }

        private void LABEL_FormGerirEncomendas_ALTERAR_ENCOMENDA_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGerirEncomendas.SelectedIndex = 3;
        }

        private void BUTTON_FormGerirEncomendas_TabPageAdicionarEncomenda_ADICIONAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO FormGestaoEncomendasAdicionarDescricao = new FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO();
            FormGestaoEncomendasAdicionarDescricao.ShowDialog();
        }

        private void BUTTON_FormGerirEncomendas_TabPageAdicionarEncomenda_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_ENCOMENDAS_ADICIONARNOTA FormGestaoEncomendasAdicionarNota = new FORM_GERIR_ENCOMENDAS_ADICIONARNOTA();
            FormGestaoEncomendasAdicionarNota.ShowDialog();
        }

        private void BUTTON_FormGerirEncomendas_TabPageAlteraraEncomenda_ALTERAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO FormGestaoEncomendasAlterarDescricao = new FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO();
            FormGestaoEncomendasAlterarDescricao.ShowDialog();
        }

        private void BUTTON_FormGerirEncomendas_TabPageAlteraraEncomenda_ALTERAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA FormGestaoEncomendasAlterarNota = new FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA();
            FormGestaoEncomendasAlterarNota.ShowDialog();
        }

        private void MENUSTRIP_FormGerirEncomendas_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_FormGerirEncomendas_TabPageAdicionarEncomenda_ADICIONAR_ENCOMENDA_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarEncomenda() == true)
            {
                AdicionarEncomenda();

                Refresh_ListBox_TagPageRemoverEncomenda();

                Refresh_ListBox_TabPageAlterarEncomenda();

                FormInicio_Objects.Refresh_ListBox_ENCOMENDAS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                TABCONTROL_FormGerirEncomendas.SelectedIndex = 0;
            }
        }

        private void LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_ENCOMENDAS_TabPageRemover_SeleccaoItem();

            if (TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text != "")
                BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = true;
        }

        private void BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER_Click(object sender, EventArgs e)
        {
            RemoverEncomenda();

            Refresh_ListBox_TagPageRemoverEncomenda();

            Refresh_ListBox_TabPageAlterarEncomenda();

            FormInicio_Objects.Refresh_ListBox_ENCOMENDAS();

            FormInicio_Objects.Limpar_Labels_AUX();

            Limpar_Textbox();

            BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = false;
        }

        private void LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_ENCOMENDAS_TabPageAlterar_SeleccaoItem();

            if (TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text != "")
                BUTTON_FormGerirEncomendas_TabPageAlteraraEncomenda_GUARDAR_ALTERACOES.Enabled = true;
        }

        private void BUTTON_FormGerirEncomendas_TabPageAlteraraEncomenda_GUARDAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarEncomenda() == true)
            {
                AlterarEncomenda();

                Refresh_ListBox_TagPageRemoverEncomenda();

                Refresh_ListBox_TabPageAlterarEncomenda();

                FormInicio_Objects.Refresh_ListBox_ENCOMENDAS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                BUTTON_FormGerirEncomendas_TabPageAlteraraEncomenda_GUARDAR_ALTERACOES.Enabled = false;
            }
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }         
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }  
        }

        private void TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA_KeyPress(object sender, KeyPressEventArgs e)
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

        public void Refresh_ListBox_TagPageRemoverEncomenda()
        {
            try
            {
                LigacaoDB.Open();

                LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public void Refresh_ListBox_TabPageAlterarEncomenda()
        {
            try
            {
                LigacaoDB.Open();

                LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }


//       ╔═════════════════════════════════╗
//       ║..:     GERIR ENCOMENDAS      :..║

        public void AdicionarEncomenda()
        {
            LigacaoDB.Open();

            try
            {
                if (TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text != "")
                {
                    try
                    {
                        OleDbCommand Command_AdicionarEncomenda = new OleDbCommand();

                        string AdicionarDescricao = AUX_Descricao;

                        string Query_AdicionarEncomenda = "INSERT INTO Encomendas(Nome, Entidade, Data, Estado, Valor, Descritivo, Nota) VALUES('" + TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text + "','" + TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text + "','" + TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text + "','" + TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text + "','" + TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text + "', '" + AUX_Descricao + "', '" + AUX_Nota + "');";

                        Command_AdicionarEncomenda.CommandText = Query_AdicionarEncomenda;
                        Command_AdicionarEncomenda.Connection = LigacaoDB;

                        Command_AdicionarEncomenda.ExecuteNonQuery();

                        MessageBox.Show("Encomenda Inserida com Sucesso!", "Encomenda Adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);                        
                    }

                    catch (Exception EX)
                    { }
                }

                else
                    MessageBox.Show("Tem de indicar pelo menos um nome para a encomenda", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void RemoverEncomenda()
        {
            LigacaoDB.Open();

            try
            {
                string Query_DeleteEncomenda = "DELETE FROM Encomendas WHERE Nome = '" + TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text + "'";

                OleDbCommand Command_RemoverEncomenda = new OleDbCommand(Query_DeleteEncomenda, LigacaoDB);

                Command_RemoverEncomenda.CommandText = Query_DeleteEncomenda;
                Command_RemoverEncomenda.Connection = LigacaoDB;

                Command_RemoverEncomenda.ExecuteNonQuery();    
            
                MessageBox.Show("Encomenda Removida com Sucesso!", "Encomenda Removida", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void AlterarEncomenda()
        {
            LigacaoDB.Open();

            try
            {
                string Query_AlterarEncomenda = "UPDATE Encomendas SET Nome = '" + TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text + "', Entidade = '" + TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text + "', Data = '" + TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text + "', Estado = '" + TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text + "', Valor = '" + TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text + "', Descritivo = '" + AUX_Descricao + "', Nota = '" + AUX_Nota + "' WHERE Nome = '" + LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.SelectedItem.ToString() + "'";

                OleDbCommand Command_AlterarEncomenda = new OleDbCommand();

                Command_AlterarEncomenda.CommandText = Query_AlterarEncomenda;
                Command_AlterarEncomenda.Connection = LigacaoDB;

                Command_AlterarEncomenda.ExecuteNonQuery();

                MessageBox.Show("Encomenda Alterada com Sucesso!", "Encomenda Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);                         
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:    PREENCHER TEXTBOX'S    :..║

        public void ListBox_ENCOMENDAS_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxData = "SELECT Data FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDescritivo = "SELECT Descritivo FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Encomendas WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDescritivo.Read())
                {
                    AUX_Descricao = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    AUX_Nota = Reader_RefreshTextBoxNota["Nota"].ToString();
                }               

                if (LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems != null)
                    BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = true;
                if (LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems == null)
                    BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = false;
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_ENCOMENDAS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxData = "SELECT Data FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDescritivo = "SELECT Descritivo FROM Encomendas WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Encomendas WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDescritivo.Read())
                {
                    AUX_Descricao = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    AUX_Nota = Reader_RefreshTextBoxNota["Nota"].ToString();
                }                

                if (LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems != null)
                    BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = true;
                if (LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems == null)
                    BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = false;
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
                TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text = "";

                TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ENTIDADE.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_DATA.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ESTADO.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_VALOR.Text = "";

                TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text = "";
                TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text = "";

                AUX_Descricao = "";
                AUX_Nota = "";
            }

            catch(Exception EX)
            { }
        }

//       ╔═════════════════════════════════╗
//       ║..:    REGULAR EXPRESSIONS    :..║
                
        public bool REGEX_Texto_AdicionarEncomenda()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");

            string AdicionarEncomenda_NOME = TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text;
            string AdicionarEncomenda_ENTIDADE = TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text;
            string AdicionarEncomenda_DATA = TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text;
            string AdicionarEncomenda_ESTADO = TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text;
            string AdicionarEncomenda_VALOR = TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text;

            if (ValidarTexto.IsMatch(AdicionarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarData.IsMatch(AdicionarEncomenda_DATA) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_ESTADO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AdicionarEncomenda_VALOR) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        public bool REGEX_Texto_AlterarEncomenda()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");

            string AlterarEncomenda_NOME = TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text; ;
            string AlterarEncomenda_ENTIDADE = TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text;
            string AlterarEncomenda_DATA = TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text;
            string AlterarEncomenda_ESTADO = TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text;
            string AlterarEncomenda_VALOR = TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text;

            if (ValidarTexto.IsMatch(AlterarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarData.IsMatch(AlterarEncomenda_DATA) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_ESTADO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AlterarEncomenda_VALOR) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
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
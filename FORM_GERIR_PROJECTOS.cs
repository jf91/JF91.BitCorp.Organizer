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
    public partial class FORM_GERIR_PROJECTOS : Form
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

        public FORM_GERIR_PROJECTOS()
        {
            InitializeComponent();

            LigacaoDB = new OleDbConnection(EnderecoDB);

            Refresh_ListBox_TagPageRemoverProjecto();

            Refresh_ListBox_TabPageAlterarProjecto();
        }

        private void PICTUREBOX_FormGestaoProjectos_TabPageGestaoProjectos_ADICIONAR_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 1;
        }

        private void LABEL_FormGestaoProjectos_TabPageGestaoProjectos_ADICIONAR_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 1;
        }

        private void PICTUREBOX_REMOVER_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 2;
        }

        private void LABEL_FormGestaoProjectos_TabPageGestaoProjectos_REMOVER_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 2;
        }

        private void PICTUREBOX_FormGestaoProjectos_TabPageGestaoProjectos_ALTERAR_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 3;
        }

        private void LABEL_FormGestaoProjectos_TabPageGestaoProjectos_ALTERAR_PROJECTO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoProjectos.SelectedIndex = 3;
        }

        private void BUTTON_FormGestaoProjectos_TabPageAdicionarProjecto_ADICIONAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARDESCRICAO FormGestaoProjectosAdicionarDescricao = new FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARDESCRICAO();
            FormGestaoProjectosAdicionarDescricao.ShowDialog();
        }

        private void BUTTON_FormGestaoProjectos_TabPageAdicionarProjecto_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA FormGestaoProjectosAdicionarNota = new FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA();
            FormGestaoProjectosAdicionarNota.ShowDialog();
        }

        private void BUTTON_FormGestaoProjectos_TabPageAlterarProjecto_ALTERAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARDESCRICAO FormGestaoProjectosAlterarDescricao = new FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARDESCRICAO();
            FormGestaoProjectosAlterarDescricao.ShowDialog();
        }

        private void BUTTON_FormGestaoProjectos_TabPageAlterarProjecto_ALTERAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARNOTA FormGestaoProjectosAlterarNota = new FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARNOTA();
            FormGestaoProjectosAlterarNota.ShowDialog();
        }

        private void MENUSTRIP_FormGestaoProjectos_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_FormGestaoProjectos_TabPageAdicionarProjecto_ADICIONAR_PROJECTO_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarProjecto() == true)
            {
                AdicionarProjecto();

                Refresh_ListBox_TagPageRemoverProjecto();

                Refresh_ListBox_TabPageAlterarProjecto();

                FormInicio_Objects.Refresh_ListBox_PROJECTOS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                TABCONTROL_FormGestaoProjectos.SelectedIndex = 0;
            }
        }

        private void LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_PROJECTOS_TabPageRemover_SeleccaoItem();

            if (TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text != "")
                BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = true;
        }

        private void LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_PROJECTOS_TabPageAlterar_SeleccaoItem();

            if (TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text != "")
                BUTTON_FormGestaoProjectos_TabPageAlterarProjecto_GRAVAR_ALTERACOES.Enabled = true;
        }

        private void BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER_Click(object sender, EventArgs e)
        {
            RemoverProjecto();

            Refresh_ListBox_TagPageRemoverProjecto();

            Refresh_ListBox_TabPageAlterarProjecto();

            FormInicio_Objects.Refresh_ListBox_PROJECTOS();

            FormInicio_Objects.Limpar_Labels_AUX();

            Limpar_Textbox();

            BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = false;
        }

        private void BUTTON_FormGestaoProjectos_TabPageAlterarProjecto_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarProjecto() == true)
            {
                AlterarProjecto();

                Refresh_ListBox_TagPageRemoverProjecto();

                Refresh_ListBox_TabPageAlterarProjecto();

                FormInicio_Objects.Refresh_ListBox_PROJECTOS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                BUTTON_FormGestaoProjectos_TabPageAlterarProjecto_GRAVAR_ALTERACOES.Enabled = false;
            }
        }
        
        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME_KeyPress(object sender, KeyPressEventArgs e)
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
//       ║..:    ACTUALIZAR LISTBOX's   :..║

        public void Refresh_ListBox_TagPageRemoverProjecto() // Actualizar a ListBox na TabPage -> Remover Projecto
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_TabPageAlterarProjecto() // Actualizar a ListBox na TabPage -> Alterar Projecto
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:      GERIR PROJECTOS      :..║

        public void AdicionarProjecto() // Adicionar um novo Projecto à Base de Dados
        {
            LigacaoDB.Open();

            try
            {
                if (TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text != "")
                {
                    try
                    {
                        OleDbCommand Command_AdicionarProjecto = new OleDbCommand();

                        string Query_AdicionarProjecto = "INSERT INTO Projectos(Nome, Tipo, Entidade, Data, Previsão, Estado, Descritivo, Nota) VALUES('" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text + "','" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text + "','" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text + "','" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text + "','" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text + "', '" + TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text + "', '" + AUX_Descricao + "', '" + AUX_Nota + "');";

                        Command_AdicionarProjecto.CommandText = Query_AdicionarProjecto;
                        Command_AdicionarProjecto.Connection = LigacaoDB;

                        Command_AdicionarProjecto.ExecuteNonQuery();

                        MessageBox.Show("Projecto Inserido com Sucesso!", "Projecto Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);                       
                    }

                    catch (Exception EX)
                    { }
                }

                else
                    MessageBox.Show("Tem de indicar pelo menos um nome para o projecto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void RemoverProjecto() // Remover Projecto Selecionado na ListBox (Usando como base a TextBox -> Nome)
        {
            LigacaoDB.Open();

            try
            {
                string Query_DeleteProjecto = "DELETE FROM Projectos WHERE Nome = '" + TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text + "'";

                OleDbCommand Command_RemoverProjecto = new OleDbCommand(Query_DeleteProjecto, LigacaoDB);

                Command_RemoverProjecto.CommandText = Query_DeleteProjecto;
                Command_RemoverProjecto.Connection = LigacaoDB;

                Command_RemoverProjecto.ExecuteNonQuery();               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void AlterarProjecto() // Alterar Projecto Selecionado na ListBox (Usando como base a TextBox -> Nome)
        {
            LigacaoDB.Open();

            try
            {
                
                string Query_AlterarProjecto = "UPDATE Projectos SET Nome = '" + TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text + "', Tipo = '" + TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text + "', Entidade = '" + TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text + "', Data = '" + TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text + "', Previsão = '" + TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text + "', Descritivo = '" + AUX_Descricao + "', Nota = '" + AUX_Nota + "' WHERE Nome = '" + LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.SelectedItem.ToString() + "'";

                OleDbCommand Command_AlterarProjecto = new OleDbCommand();

                Command_AlterarProjecto.CommandText = Query_AlterarProjecto;
                Command_AlterarProjecto.Connection = LigacaoDB;

                Command_AlterarProjecto.ExecuteNonQuery();

                MessageBox.Show("Projecto Alterado com Sucesso!", "Projecto Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);              
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:    PREENCHER TEXTBOX'S    :..║

        public void ListBox_PROJECTOS_TabPageRemover_SeleccaoItem() // Passar para as TextBox's da TabPage Remover Projecto os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxData = "SELECT Data FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPrevisao = "SELECT Previsão FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM Projectos WHERE Nome = '" + ID + "'";

                string Query_RefreshTextBoxDescritivo = "SELECT Descritivo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Projectos WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPrevisao = new OleDbCommand(Query_RefreshTextBoxPrevisao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);

                OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxPrevisao.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;

                Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPrevisao = Command_RefreshTextBoxPrevisao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();

                OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxPrevisao.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PREVISAO.Text = Reader_RefreshTextBoxPrevisao["Previsão"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }                

                if (LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItems != null)
                    BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = true;
                if (LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItems == null)
                    BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = false;
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_PROJECTOS_TabPageAlterar_SeleccaoItem() // Passar para as TextBox's da TabPage Alterar Projecto os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxData = "SELECT Data FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPrevisao = "SELECT Previsão FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM Projectos WHERE Nome = '" + ID + "'";

                string Query_RefreshTextBoxDescritivo = "SELECT Descritivo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Projectos WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPrevisao = new OleDbCommand(Query_RefreshTextBoxPrevisao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxPrevisao.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPrevisao = Command_RefreshTextBoxPrevisao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxPrevisao.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text = Reader_RefreshTextBoxPrevisao["Previsão"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxDescritivo.Read())
                {
                    AUX_Descricao = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    AUX_Nota = Reader_RefreshTextBoxNota["Nota"].ToString();
                }                
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:     LIMPAR TEXTBOX's      :..║

        public void Limpar_Textbox() // Limpar todas as TextBox's
        {
            try
            {
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text = "";

                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_TIPO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ENTIDADE.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_DATA.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PREVISAO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ESTADO.Text = "";

                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text = "";
                TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text = "";

                AUX_Descricao = "";
                AUX_Nota = "";
            }

            catch(Exception EX)
            { }
        }

//       ╔═════════════════════════════════╗
//       ║..:    REGULAR EXPRESSIONS    :..║
                
        public bool REGEX_Texto_AdicionarProjecto()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");

            string AdicionarEncomenda_NOME = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text;
            string AdicionarEncomenda_TIPO = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text;
            string AdicionarEncomenda_ENTIDADE = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text;
            string AdicionarEncomenda_DATA = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text;
            string AdicionarEncomenda_PREVISAO = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text;
            string AdicionarEncomenda_ESTADO = TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text;

            if (ValidarTexto.IsMatch(AdicionarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_TIPO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarTexto.IsMatch(AdicionarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AdicionarEncomenda_DATA) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AdicionarEncomenda_PREVISAO) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_ESTADO) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        public bool REGEX_Texto_AlterarProjecto()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");

            string AlterarEncomenda_NOME = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text;
            string AlterarEncomenda_TIPO = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text;
            string AlterarEncomenda_ENTIDADE = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text;
            string AlterarEncomenda_DATA = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text;
            string AlterarEncomenda_PREVISAO = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text;
            string AlterarEncomenda_ESTADO = TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text;

            if (ValidarTexto.IsMatch(AlterarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_TIPO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarTexto.IsMatch(AlterarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AlterarEncomenda_DATA) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AlterarEncomenda_PREVISAO) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_ESTADO) == false)
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

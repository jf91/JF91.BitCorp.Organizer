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
using System.IO;
using System.Text.RegularExpressions;

namespace BC_Organizer
{
    public partial class FORM_GERIR_SERVICOS_INTERNET : Form
    {
        #region OBJECTOS PUBLICOS

        public OleDbConnection LigacaoDB;

        public OleDbDataReader Reader;

        public string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        public string AUX_Descricao;
        public string AUX_Nota;

        #endregion

        #region  PROGRAMA

        public FORM_GERIR_SERVICOS_INTERNET()
        {
            InitializeComponent();

            LigacaoDB = new OleDbConnection(EnderecoDB);

            Refresh_ListBox_TagPageRemoverServico();

            Refresh_ListBox_TabPageAlterarServico();
        }

        private void PICTUREBOX__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_ADICIONARSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 1;
        }

        private void LABEL__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_ADICIONARSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 1;
        }

        private void PICTUREBOX__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_REMOVERSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 2;
        }

        private void LABEL__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_REMOVERSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 2;
        }

        private void PICTUREBOX__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_ALTERARSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 3;
        }

        private void LABEL__FormGestaoServicosInternet_GESTAOSERVICOSINTERNET_ALTERARSERVICO_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 3;
        }

        private void BUTTON_GestaoServicosInternet_ADICIONARSERVICO_ADICIONARDESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARDESCRICAO FormGestaoServicosInternetAdicionarDescricao = new FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARDESCRICAO();

            FormGestaoServicosInternetAdicionarDescricao.ShowDialog();
        }

        private void BUTTON_GestaoServicosInternet_ADICIONARSERVICO_ADICIONARNOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARNOTA FormGestaoServicosInternetAdicionarNota = new FORM_GERIR_SERVICOS_INTERNET_ADICIONARSERVICO_ADICIONARNOTA();

            FormGestaoServicosInternetAdicionarNota.ShowDialog();
        }

        private void BUTTON_GestaoServicosInternet_ALTERARSERVICO_ALTERARDESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVICOS_INTERNET_ALTERARSERVICO_ALTERARDESCRICAO FormGestaoServicosInternetAlterarDescricao = new FORM_GERIR_SERVICOS_INTERNET_ALTERARSERVICO_ALTERARDESCRICAO();

            FormGestaoServicosInternetAlterarDescricao.ShowDialog();
        }

        private void BUTTON_GestaoServicosInternet_ALTERARSERVICO_ALTERARNOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVICOS_INTERNET_ALTERARSERVICO_ALTERARNOTA FormGestaoServicosInternetAlterarNota = new FORM_GERIR_SERVICOS_INTERNET_ALTERARSERVICO_ALTERARNOTA();

            FormGestaoServicosInternetAlterarNota.ShowDialog();
        }

        private void MENUSTRIP_GestaoServicosInternet_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_GestaoServicosInternet_ADICIONARSERVICO_ADICIONARSERVICO_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarServico() == true)
            {
                
                    AdicionarServico();

                    Refresh_ListBox_TagPageRemoverServico();

                    Refresh_ListBox_TabPageAlterarServico();

                    FormInicio_Objects.Refresh_ListBox_SERVICOS();

                    FormInicio_Objects.Limpar_Labels_AUX();

                    Limpar_Textbox();

                    TABCONTROL_FormGestaoServicosInternet.SelectedIndex = 0;
            }
        }

        private void LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVICOS_TabPageRemover_SeleccaoItem();

            if (TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_NOME.Text != "")
                BUTTON_GestaoServicosInternet_REMOVERSERVICO_REMOVER.Enabled = true;
        }

        private void BUTTON_GestaoServicosInternet_REMOVERSERVICO_REMOVER_Click(object sender, EventArgs e)
        {
            RemoverServico();

            Refresh_ListBox_TagPageRemoverServico();

            Refresh_ListBox_TabPageAlterarServico();

            FormInicio_Objects.Refresh_ListBox_SERVICOS();

            FormInicio_Objects.Limpar_Labels_AUX();

            Limpar_Textbox();

            BUTTON_GestaoServicosInternet_REMOVERSERVICO_REMOVER.Enabled = false;
        }

        private void LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVICOS_TabPageAlterar_SeleccaoItem();

            if (TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME.Text != "")
                BUTTON_GestaoServicosInternet_ALTERARSERVICO_GRAVARALTERACOES.Enabled = true;
        }

        private void BUTTON_GestaoServicosInternet_ALTERARSERVICO_GRAVARALTERACOES_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarServico() == true)
            {
                AlterarServico();

                Refresh_ListBox_TagPageRemoverServico();

                Refresh_ListBox_TabPageAlterarServico();

                FormInicio_Objects.Refresh_ListBox_SERVICOS();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                BUTTON_GestaoServicosInternet_ALTERARSERVICO_GRAVARALTERACOES.Enabled = false;
            }


        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_USERNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_DATAASSINATURA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_USERNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_DATAASSINATURA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }
        }

        private void TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ESTADO_KeyPress(object sender, KeyPressEventArgs e)
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

        public void Refresh_ListBox_TagPageRemoverServico()
        {
            try
            {
                LigacaoDB.Open();

                LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM WebServices ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public void Refresh_ListBox_TabPageAlterarServico()
        {
            try
            {
                LigacaoDB.Open();

                LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM WebServices ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Servico = new ListViewItem(Reader[0].ToString());
                    LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.Items.Add(Reader[0].ToString());
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

        public void AdicionarServico()
        {
            LigacaoDB.Open();

            try
            {
                if (TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_NOME.Text != "")
                {
                    try
                    {
                        OleDbCommand Command_AdicionarServico = new OleDbCommand();

                        string AdicionarDescricao = AUX_Descricao;

                        string Query_AdicionarServico = "INSERT INTO WebServices(Nome, Entidade, Tipo, Login_Username, Login_Password, Data_Assinatura, Valor, Estado, Descritivo, Nota) VALUES('" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_NOME.Text + "','" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ENTIDADE.Text + "','" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_TIPO.Text + "','" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_USERNAME.Text + "','" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_PASSWORD.Text + "', '" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_DATAASSINATURA.Text + "', '" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_VALOR.Text + "', '" + TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ESTADO.Text + "', '" + AUX_Descricao + "', '" + AUX_Nota + "');";

                        Command_AdicionarServico.CommandText = Query_AdicionarServico;
                        Command_AdicionarServico.Connection = LigacaoDB;

                        Command_AdicionarServico.ExecuteNonQuery();

                        MessageBox.Show("Servico Inserido com Sucesso!", "Servico Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch (Exception EX)
                    { }
                }

                else
                    MessageBox.Show("Tem de indicar pelo menos um nome para servico", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void RemoverServico()
        {
            LigacaoDB.Open();

            try
            {
                string Query_DeleteServico = "DELETE FROM WebServices WHERE Nome = '" + TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_NOME.Text + "'";

                OleDbCommand Command_RemoverServico = new OleDbCommand(Query_DeleteServico, LigacaoDB);

                Command_RemoverServico.CommandText = Query_DeleteServico;
                Command_RemoverServico.Connection = LigacaoDB;

                Command_RemoverServico.ExecuteNonQuery();

                MessageBox.Show("Servico Removido com Sucesso!", "Servico Removido", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void AlterarServico()
        {
            LigacaoDB.Open();

            try
            {
                string Query_AlterarServico = "UPDATE WebServices SET Nome = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME.Text + "', Entidade = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ENTIDADE.Text + "', Tipo = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_TIPO.Text + "', Login_Username = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_USERNAME.Text + "', Login_Password = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_PASSWORD.Text + "', Data_Assinatura = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_DATAASSINATURA.Text + "', Valor = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_VALOR.Text + "', Estado = '" + TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ESTADO.Text + "', Descritivo = '" + AUX_Descricao + "', Nota = '" + AUX_Nota + "' WHERE Nome = '" + LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.SelectedItem.ToString() + "'";

                OleDbCommand Command_AlterarServico = new OleDbCommand();

                Command_AlterarServico.CommandText = Query_AlterarServico;
                Command_AlterarServico.Connection = LigacaoDB;

                Command_AlterarServico.ExecuteNonQuery();

                MessageBox.Show("Servico Alterado com Sucesso!", "Servico Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        //       ╔═════════════════════════════════╗
        //       ║..:    PREENCHER TEXTBOX'S    :..║

        public void ListBox_SERVICOS_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDataAssinatura = "SELECT Data_Assinatura FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM WebServices WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDataAssinatura = new OleDbCommand(Query_RefreshTextBoxDataAssinatura, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxDataAssinatura.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDataAssinatura = Command_RefreshTextBoxDataAssinatura.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxDataAssinatura.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_DATAASSINATURA.Text = Reader_RefreshTextBoxDataAssinatura["Data_Assinatura"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                if (LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS.SelectedItems != null)
                    BUTTON_GestaoServicosInternet_REMOVERSERVICO_REMOVER.Enabled = true;
                if (LISTBOX_GestaoServicosInternet_REMOVERSERVICO_SERVICOS.SelectedItems == null)
                    BUTTON_GestaoServicosInternet_REMOVERSERVICO_REMOVER.Enabled = false;
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_SERVICOS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxUsername = "SELECT Login_Username FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPassword = "SELECT Login_Password FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDataAssinatura = "SELECT Data_Assinatura FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDescricao = "SELECT Descritivo FROM WebServices WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM WebServices WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxUsername = new OleDbCommand(Query_RefreshTextBoxUsername, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPassword = new OleDbCommand(Query_RefreshTextBoxPassword, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDataAssinatura = new OleDbCommand(Query_RefreshTextBoxDataAssinatura, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescricao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxUsername.Connection = LigacaoDB;
                Command_RefreshTextBoxPassword.Connection = LigacaoDB;
                Command_RefreshTextBoxDataAssinatura.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxDescricao.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxUsername = Command_RefreshTextBoxUsername.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPassword = Command_RefreshTextBoxPassword.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDataAssinatura = Command_RefreshTextBoxDataAssinatura.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescricao = Command_RefreshTextBoxDescricao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxUsername.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_USERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
                }

                while (Reader_RefreshTextBoxPassword.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_PASSWORD.Text = Reader_RefreshTextBoxPassword["Login_Password"].ToString();
                }

                while (Reader_RefreshTextBoxDataAssinatura.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_DATAASSINATURA.Text = Reader_RefreshTextBoxDataAssinatura["Data_Assinatura"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxDescricao.Read())
                {
                    AUX_Descricao = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    AUX_Nota = Reader_RefreshTextBoxNota["Nota"].ToString();
                }

                if (LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.SelectedItems != null)
                    BUTTON_GestaoServicosInternet_ALTERARSERVICO_GRAVARALTERACOES.Enabled = true;
                if (LISTBOX_GestaoServicosInternet_ALTERARSERVICO_SERVICOS.SelectedItems == null)
                    BUTTON_GestaoServicosInternet_ALTERARSERVICO_GRAVARALTERACOES.Enabled = false;
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        //       ╔═════════════════════════════════╗
        //       ║..:     LIMPAR TEXTBOX's      :..║

        public void Limpar_Textbox()
        {
            try
            {
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_NOME.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ENTIDADE.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_TIPO.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_USERNAME.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_PASSWORD.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_DATAASSINATURA.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_VALOR.Text = "";
                TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ESTADO.Text = "";

                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_NOME.Text = "";
                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_ENTIDADE.Text = "";
                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_TIPO.Text = "";
                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_DATAASSINATURA.Text = "";
                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_VALOR.Text = "";
                TEXTBOX_GestaoServicosInternet_REMOVERSERVICO_ESTADO.Text = "";

                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ENTIDADE.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_TIPO.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_USERNAME.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_PASSWORD.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_DATAASSINATURA.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_VALOR.Text = "";
                TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ESTADO.Text = "";

                AUX_Descricao = "";
                AUX_Nota = "";
            }

            catch (Exception EX)
            { }
        }

        //       ╔═════════════════════════════════╗
        //       ║..:    REGULAR EXPRESSIONS    :..║

        public bool REGEX_Texto_AdicionarServico()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");

            string AdicionarServico_NOME = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_NOME.Text;
            string AdicionarServico_ENTIDADE = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ENTIDADE.Text;
            string AdicionarServico_Tipo = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_TIPO.Text;
            string AdicionarServico_Username = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_USERNAME.Text;
            string AdicionarServico_Password = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_PASSWORD.Text;
            string AdicionarServico_DataAssinatura = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_DATAASSINATURA.Text;
            string AdicionarServico_Valor = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_VALOR.Text;
            string AdicionarServico_Estado = TEXTBOX_GestaoServicosInternet_ADICIONARSERVICO_ESTADO.Text;

            if (ValidarTexto.IsMatch(AdicionarServico_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarServico_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarServico_Tipo) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Tipo. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarServico_Username) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Username. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarServico_Password) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Password. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AdicionarServico_DataAssinatura) == false)
            {
                MessageBox.Show(@"Não indicou o campo Data de Assinatura correctamente. Deve respeitar o formato de Data (DD-MM-AAAA). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AdicionarServico_Valor) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarServico_Estado) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        public bool REGEX_Texto_AlterarServico()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");

            string AlterarServico_NOME = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_NOME.Text; ;
            string AlterarServico_ENTIDADE = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ENTIDADE.Text;
            string AlterarServico_TIPO = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_TIPO.Text;
            string AlterarServico_USERNAME = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_USERNAME.Text;
            string AlterarServico_PASSWORD = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_PASSWORD.Text;
            string AlterarServico_DATAASINATURA = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_DATAASSINATURA.Text;
            string AlterarServico_VALOR = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_VALOR.Text;
            string AlterarServico_ESTADO = TEXTBOX_GestaoServicosInternet_ALTERARSERVICO_ESTADO.Text;

            if (ValidarTexto.IsMatch(AlterarServico_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarServico_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarServico_USERNAME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Username. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarServico_PASSWORD) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Password. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AlterarServico_DATAASINATURA) == false)
            {
                MessageBox.Show(@"Não indicou o campo Data de Assinatura correctamente. Deve respeitar o formato de Data (DD-MM-AAAA). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AlterarServico_VALOR) == false)
            {
                MessageBox.Show(@"Não indicou o campo Valor correctamente. Deve respeitar o formato de Valor (Ex. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarServico_ESTADO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Estado. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
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

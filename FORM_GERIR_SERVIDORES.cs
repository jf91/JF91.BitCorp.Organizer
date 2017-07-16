using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BC_Organizer
{
    public partial class FORM_GERIR_SERVIDORES : Form
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

        public FORM_GERIR_SERVIDORES()
        {
            InitializeComponent();
            
            LigacaoDB = new OleDbConnection(EnderecoDB);

            Refresh_ListBox_TagPageRemoverServidor();

            Refresh_ListBox_TabPageAlterarServidor();
        }

        private void PICTUREBOX_FormGestaoServidores_TabPageGestaoServidores_ADICIONAR_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 1;
        }

        private void LABEL_FormGestaoServidores_TabPageGestaoServidores_ADICIONAR_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 1;
        }

        private void PICTUREBOX_FormGestaoServidores_TabPageGestaoServidores_REMOVER_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 2;
        }

        private void LABEL_FormGestaoServidores_TabPageGestaoServidores_REMOVER_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 2;
        }

        private void PICTUREBOX_FormGestaoServidores_TabPageGestaoServidores_ALTERAR_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 3;
        }

        private void LABEL_FormGestaoServidores_TabPageGestaoServidores_ALTERAR_SERVIDOR_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormGestaoServidores.SelectedIndex = 3;
        }

        private void BUTTON_FormGestaoServidores_TabPageAdicionarServidor_ADICIONAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO FormGestaoServidoresFormAdicionarDescricao = new FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO();
            FormGestaoServidoresFormAdicionarDescricao.ShowDialog();
        }

        private void BUTTON_FormGestaoServidores_TabPageAdicionarServidor_ADICIONAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA FormGestaoServidoresAdicionarNota = new FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA();
            FormGestaoServidoresAdicionarNota.ShowDialog();
        }

        private void BUTTON_FormGestaoServidores_TabPageAlterarServidor_ALTERAR_DESCRICAO_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO FormGestaoServidoresFormAlterarDescricao = new FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO();
            FormGestaoServidoresFormAlterarDescricao.ShowDialog();
        }

        private void BUTTON_FormGestaoServidores_TabPageAlterarServidor_ALTERAR_NOTA_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_ FormGestaoServidoresFormAlterarNota = new FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_();
            FormGestaoServidoresFormAlterarNota.ShowDialog();
        }

        private void MENUSTRIP_FormGestaoServidores_BUTTON_FECHAR_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BUTTON_FormGestaoServidores_TabPageAdicionarServidor_ADICIONAR_SERVIDOR_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AdicionarServidor() == true)
            {
                AdicionarServidor();

                Refresh_ListBox_TagPageRemoverServidor();

                Refresh_ListBox_TabPageAlterarServidor();

                FormInicio_Objects.Refresh_ListBox_SERVIDORES();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                TABCONTROL_FormGestaoServidores.SelectedIndex = 0;
            }
        }

        private void LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVIDORES_TabPageRemover_SeleccaoItem(); 
          
            if (TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text != "")
                BUTTON_FormGestaoServidores_TabPageRemoverServidor_REMOVER.Enabled = true;
        }

        private void LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVIDORES_TabPageAlterar_SeleccaoItem();

            if(TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text != "")
                BUTTON_FormGestaoServidores_TabPageAlterarServidor_GRAVAR_ALTERACOES.Enabled = true;
        }

        private void BUTTON_FormGestaoServidores_TabPageRemoverServidor_REMOVER_Click(object sender, EventArgs e)
        {
            RemoverServidor();

            Refresh_ListBox_TagPageRemoverServidor();

            Refresh_ListBox_TabPageAlterarServidor();

            FormInicio_Objects.Refresh_ListBox_SERVIDORES();

            FormInicio_Objects.Limpar_Labels_AUX();

            Limpar_Textbox();

            BUTTON_FormGestaoServidores_TabPageRemoverServidor_REMOVER.Enabled = false;
        }

        private void BUTTON_FormGestaoServidores_TabPageAlterarServidor_GRAVAR_ALTERACOES_Click(object sender, EventArgs e)
        {
            if (REGEX_Texto_AlterarServidor() == true)
            {
                AlterarServidor();

                Refresh_ListBox_TagPageRemoverServidor();

                Refresh_ListBox_TabPageAlterarServidor();

                FormInicio_Objects.Refresh_ListBox_SERVIDORES();

                FormInicio_Objects.Limpar_Labels_AUX();

                Limpar_Textbox();

                BUTTON_FormGestaoServidores_TabPageAlterarServidor_GRAVAR_ALTERACOES.Enabled = false;
            }
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)34 || e.KeyChar == (char)37 || e.KeyChar == (char)39 || e.KeyChar == (char)44 || e.KeyChar == (char)60 || e.KeyChar == (char)61 || e.KeyChar == (char)62 || e.KeyChar == (char)92)
            {
                e.Handled = true;
            }     
        }

        private void TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO_KeyPress(object sender, KeyPressEventArgs e)
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

        public void Refresh_ListBox_TagPageRemoverServidor() // Actualizar a ListBox na TabPage -> Remover Servidor
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Add(Reader[0].ToString());                    
                }
                Reader.Close();

                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_TabPageAlterarServidor() // Actualizar a ListBox na TabPage -> Alterar Servidor
        {
            LigacaoDB.Open();

            try
            {                
                LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                }
                Reader.Close();
                             
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:     GERIR SERVIDORES      :..║

        public void AdicionarServidor() // Adicionar um novo servidor à Base de Dados
        {
            LigacaoDB.Open();

            try
            {
                if (TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text != "")
                {
                    try
                    {
                        OleDbCommand Command_AdicionarServidor = new OleDbCommand();

                        string AdicionarDescricao = AUX_Descricao;

                        string Query_AdicionarServidor = "INSERT INTO Servidores(Nome, Hostname, IP, Login_Username, Login_Password, Tipo, Entidade, Plano_Pagamento, Valor, Data_Contracto, Descritivo, Nota) VALUES('" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text + "','" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text + "','" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text + "','" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text + "','" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text + "', '" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text + "', '" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text + "', '" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text + "', '" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text + "', '" + TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text + "', '" + AUX_Descricao + "', '" + AUX_Nota + "');";

                        Command_AdicionarServidor.CommandText = Query_AdicionarServidor;
                        Command_AdicionarServidor.Connection = LigacaoDB;

                        Command_AdicionarServidor.ExecuteNonQuery();

                        MessageBox.Show("Servidor Inserido com Sucesso!", "Servidor Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    catch (Exception EX)
                    { }
                }

                else
                    MessageBox.Show("Tem de indicar pelo menos um nome para o servidor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

         public void RemoverServidor() // Remover Servidor Selecionado na ListBox (Usando como base a TextBox -> Nome)
         {
             LigacaoDB.Open();

             try
             {      
                 string Query_DeleteServidor = "DELETE FROM Servidores WHERE Nome = '" + TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text + "'";

                 OleDbCommand Command_RemoverServidor = new OleDbCommand(Query_DeleteServidor, LigacaoDB);

                 Command_RemoverServidor.CommandText = Query_DeleteServidor;
                 Command_RemoverServidor.Connection = LigacaoDB;

                 Command_RemoverServidor.ExecuteNonQuery();                 
             }

             catch(Exception EX)
             { }

             LigacaoDB.Close();
         }

         public void AlterarServidor() // Alterar Servidor Selecionado na ListBox (Usando como base a TextBox -> Nome)
         {
             LigacaoDB.Open();

             try
             {
                 string Query_AlterarServidor = "UPDATE Servidores SET Nome = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text + "', Hostname = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text + "', IP = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text + "', Login_Username = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text + "', Login_Password = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text + "', Tipo = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text + "', Entidade = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text + "', Plano_Pagamento = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text + "', Valor = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text + "', Data_Contracto = '" + TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text + "', Descritivo = '" + AUX_Descricao + "', Nota = '" + AUX_Nota + "' WHERE Nome = '" + LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.SelectedItem.ToString() + "'";

                 OleDbCommand Command_AlterarServidor = new OleDbCommand();

                 Command_AlterarServidor.CommandText = Query_AlterarServidor;
                 Command_AlterarServidor.Connection = LigacaoDB;

                 Command_AlterarServidor.ExecuteNonQuery();

                 MessageBox.Show("Servidor Alterado com Sucesso!", "Servidor Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);                 
             }

             catch(Exception EX)
             { }

             LigacaoDB.Close();
         }

//       ╔═════════════════════════════════╗
//       ║..:    PREENCHER TEXTBOX'S    :..║

        public void ListBox_SERVIDORES_TabPageRemover_SeleccaoItem() // Passar para as TextBox's da TabPage Remover Servidor os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxHostname = "SELECT Hostname FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxIP = "SELECT IP FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPlanoPagamento = "SELECT Plano_Pagamento FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDataContracto = "SELECT Data_Contracto FROM Servidores WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxHostName = new OleDbCommand(Query_RefreshTextBoxHostname, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxIP = new OleDbCommand(Query_RefreshTextBoxIP, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPlanoPagamento = new OleDbCommand(Query_RefreshTextBoxPlanoPagamento, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDataContracto = new OleDbCommand(Query_RefreshTextBoxDataContracto, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxHostName.Connection = LigacaoDB;
                Command_RefreshTextBoxIP.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxPlanoPagamento.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDataContracto.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxHostName = Command_RefreshTextBoxHostName.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxIP = Command_RefreshTextBoxIP.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPlanoPagamento = Command_RefreshTextBoxPlanoPagamento.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDataContracto = Command_RefreshTextBoxDataContracto.ExecuteReader();


                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxHostName.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
                }

                while (Reader_RefreshTextBoxIP.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxPlanoPagamento.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDataContracto.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
                }                               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close(); 
        }

        public void ListBox_SERVIDORES_TabPageAlterar_SeleccaoItem() // Passar para as TextBox's da TabPage Alterar Servidor os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxHostname = "SELECT Hostname FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxIP = "SELECT IP FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxUsername = "SELECT Login_Username FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPassword = "SELECT Login_Password FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPlanoPagamento = "SELECT Plano_Pagamento FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxValor = "SELECT Valor FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDataContracto = "SELECT Data_Contracto FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxAlterarDescricao = "SELECT Descritivo FROM Servidores WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxAlterarNota = "SELECT Nota FROM Servidores WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxHostName = new OleDbCommand(Query_RefreshTextBoxHostname, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxIP = new OleDbCommand(Query_RefreshTextBoxIP, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxUsername = new OleDbCommand(Query_RefreshTextBoxUsername, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPassword = new OleDbCommand(Query_RefreshTextBoxPassword, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPlanoPagamento = new OleDbCommand(Query_RefreshTextBoxPlanoPagamento, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDataContracto = new OleDbCommand(Query_RefreshTextBoxDataContracto, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxAlterarDescricao = new OleDbCommand(Query_RefreshTextBoxAlterarDescricao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxAlterarNota = new OleDbCommand(Query_RefreshTextBoxAlterarNota, LigacaoDB);


                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxHostName.Connection = LigacaoDB;
                Command_RefreshTextBoxIP.Connection = LigacaoDB;
                Command_RefreshTextBoxUsername.Connection = LigacaoDB;
                Command_RefreshTextBoxPassword.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxPlanoPagamento.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDataContracto.Connection = LigacaoDB;
                Command_RefreshTextBoxAlterarDescricao.Connection = LigacaoDB;
                Command_RefreshTextBoxAlterarNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxHostName = Command_RefreshTextBoxHostName.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxUsername = Command_RefreshTextBoxUsername.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPassword = Command_RefreshTextBoxPassword.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxIP = Command_RefreshTextBoxIP.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPlanoPagamento = Command_RefreshTextBoxPlanoPagamento.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDataContracto = Command_RefreshTextBoxDataContracto.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxAlterarDescricao = Command_RefreshTextBoxAlterarDescricao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxAlterarNota = Command_RefreshTextBoxAlterarNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxHostName.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
                }

                while (Reader_RefreshTextBoxIP.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
                }

                while (Reader_RefreshTextBoxUsername.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
                }

                while (Reader_RefreshTextBoxPassword.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = Reader_RefreshTextBoxPassword["Login_Password"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxPlanoPagamento.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDataContracto.Read())
                {
                    TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
                }

                while (Reader_RefreshTextBoxAlterarDescricao.Read())
                {
                    AUX_Descricao = Reader_RefreshTextBoxAlterarDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxAlterarNota.Read())
                {
                    AUX_Nota = Reader_RefreshTextBoxAlterarNota["Nota"].ToString();
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
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text = "";

                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = "";

                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = "";
                 TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = "";

                 AUX_Descricao = "";
                 AUX_Nota = "";
             }

             catch(Exception EX)
             { }
         }

//       ╔═════════════════════════════════╗
//       ║..:    REGULAR EXPRESSIONS    :..║

        public bool REGEX_Texto_AdicionarServidor()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");
            Regex ValidarIP = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

            string AdicionarEncomenda_NOME = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text;
            string AdicionarEncomenda_HOSTNAME = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text;
            string AdicionarEncomenda_IP = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text;
            string AdicionarEncomenda_USERNAME = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text;
            string AdicionarEncomenda_PASSWORD = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text;
            string AdicionarEncomenda_TIPO = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text;
            string AdicionarEncomenda_ENTIDADE = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text;
            string AdicionarEncomenda_PLANOPAGAMENTO = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text;
            string AdicionarEncomenda_VALOR = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text;
            string AdicionarEncomenda_DATACONTRACTO = TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text;

            if (ValidarTexto.IsMatch(AdicionarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_HOSTNAME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Hostname. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarIP.IsMatch(AdicionarEncomenda_IP) == false)
            {
                MessageBox.Show(@"Nao indicou o campo IP correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_USERNAME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Username. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_PASSWORD) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Password. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_TIPO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Tipo. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AdicionarEncomenda_PLANOPAGAMENTO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Plano de Pagamento. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AdicionarEncomenda_VALOR) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Valor correctamente. Deve respeitar o formato de Valor (EX. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AdicionarEncomenda_DATACONTRACTO) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data de Contracto correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            else
                return true;
        }

        public bool REGEX_Texto_AlterarServidor()
        {
            Regex ValidarTexto = new Regex(@"[a-z A-Z 0-9 - ~ ^ ´ ` º ª + ( ) / * | @ ; : . » « ? # !]$");
            Regex ValidarData = new Regex(@"(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2})$|^(([0-2]\d|[3][0-1])\-([0]\d|[1][0-2])\-[2][0]\d{2}\s([0-1]\d|[2][0-3])\:[0-5]\d\:[0-5]\d)$");
            Regex ValidarValor = new Regex(@"([0-9 - + . € $])?€");
            Regex ValidarIP = new Regex(@"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

            string AlterarEncomenda_NOME = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text;
            string AlterarEncomenda_HOSTNAME = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text;
            string AlterarEncomenda_IP = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text;
            string AlterarEncomenda_USERNAME = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text;
            string AlterarEncomenda_PASSWORD = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text;
            string AlterarEncomenda_TIPO = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text;
            string AlterarEncomenda_ENTIDADE = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text;
            string AlterarEncomenda_PLANOPAGAMENTO = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text;
            string AlterarEncomenda_VALOR = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text;
            string AlterarEncomenda_DATACONTRACTO = TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text;

            if (ValidarTexto.IsMatch(AlterarEncomenda_NOME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Nome. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_HOSTNAME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Hostname. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }
            
            if (ValidarIP.IsMatch(AlterarEncomenda_IP) == false)
            {
                MessageBox.Show(@"Nao indicou o campo IP correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_USERNAME) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Username. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_PASSWORD) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Password. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_TIPO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Tipo. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_ENTIDADE) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Entidade. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarTexto.IsMatch(AlterarEncomenda_PLANOPAGAMENTO) == false)
            {
                MessageBox.Show(@"Introduziu caracteres inválidos no campo Plano de Pagamento. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarValor.IsMatch(AlterarEncomenda_VALOR) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Valor correctamente. Deve respeitar o formato de Valor (EX. 350.50€). Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
                return false;
            }

            if (ValidarData.IsMatch(AlterarEncomenda_DATACONTRACTO) == false)
            {
                MessageBox.Show(@"Nao indicou o campo Data de Contracto correctamente. Deve respeitar o formato de Data DD/MM/AAAA. Não pode introduzir os seguintes caracteres: ( " + char.ConvertFromUtf32(34) + " ),  ( " + char.ConvertFromUtf32(37) + " ), ( " + char.ConvertFromUtf32(39) + " ), ( " + char.ConvertFromUtf32(44) + " ), ( " + char.ConvertFromUtf32(60) + " ), ( " + char.ConvertFromUtf32(61) + " ), ( " + char.ConvertFromUtf32(62) + " )");
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

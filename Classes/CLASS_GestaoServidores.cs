using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public class CLASS_GestaoServidores
    {
        public static OleDbConnection LigacaoDB;

        public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        public static FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO FormGerirServidoresAdicionarDescricao_Objects = (FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO)Application.OpenForms["FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO"];

        public static FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA FormGerirServidoresAdicionarNota_Objects = (FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA)Application.OpenForms["FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA"];

        public static FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO FormGerirServidoresAlterarDescricao_Objects = (FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO)Application.OpenForms["FORM_GERIR_SERVIDORES_ALTERAR_ALTERARDESCRICAO"];
        public static FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_ FormGerirServidoresAlterarNota_Objects = (FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_)Application.OpenForms["FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_"];

        public static FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        public static OleDbDataReader Reader;

        public static FORM_GERIR_SERVIDORES FormGerirServidores = new FORM_GERIR_SERVIDORES();

        public string AUX_Descricao;
        public string AUX_Descricao2;
        public string AUX_Descricao3;
        public string AUX_Nota;
        public string AUX_Nota2;
        public string AUX_Nota3;
        public string AUX_Nome;

        public static void Refresh_ListBox_TagPageRemoverServidor() // Actualizar a ListBox na TabPage -> Remover Servidor
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Add(Reader[0].ToString());                    
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void Refresh_ListBox_TabPageAlterarServidor() // Actualizar a ListBox na TabPage -> Alterar Servidor
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void AdicionarServidor() // Adicionar um novo servidor à Base de Dados
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            if (FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text != "")
            {
                try
                {
                    LigacaoDB.Open();

                    OleDbCommand Command_AdicionarServidor = new OleDbCommand();

                    string AdicionarDescricao = FormGerirServidores.AUX_Descricao;

                    string Query_AdicionarServidor = "INSERT INTO Servidores(Nome, Hostname, IP, Login_Username, Login_Password, Tipo, Entidade, Plano_Pagamento, Valor, Data_Contracto, Descritivo, Nota) VALUES('" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text + "','" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text + "','" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text + "','" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text + "','" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text + "', '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text + "', '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text + "', '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text + "', '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text + "', '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text + "', '" + FormGerirServidores.AUX_Descricao + "', '" + FormGerirServidores.AUX_Nota + "');";

                    Command_AdicionarServidor.CommandText = Query_AdicionarServidor;
                    Command_AdicionarServidor.Connection = LigacaoDB;

                    Command_AdicionarServidor.ExecuteNonQuery();

                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text = "";

                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = "";

                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = "";
                    FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = "";

                    MessageBox.Show("Servidor Inserido com Sucesso!", "Servidor Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LigacaoDB.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
                MessageBox.Show("Tem de indicar pelo menos um nome para o servidor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void RemoverServidor() // Remover Servidor Selecionado na ListBox (Usando como base a TextBox -> Nome)
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_DeleteServidor = "DELETE FROM Servidores WHERE Nome = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text + "'";

            OleDbCommand Command_RemoverServidor = new OleDbCommand(Query_DeleteServidor, LigacaoDB);

            Command_RemoverServidor.CommandText = Query_DeleteServidor;
            Command_RemoverServidor.Connection = LigacaoDB;

            Command_RemoverServidor.ExecuteNonQuery();

            LigacaoDB.Close();

            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                    //LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Encomenda).ToString();
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                    //LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Encomenda).ToString();
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text = "";

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = "";

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = "";
        }

        public static void AlterarServidor() // Alterar Servidor Selecionado na ListBox (Usando como base a TextBox -> Nome)
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_AlterarServidor = "UPDATE Servidores SET Nome = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text + "', Hostname = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text + "', IP = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text + "', Login_Username = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text + "', Login_Password = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text + "', Tipo = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text + "', Entidade = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text + "', Plano_Pagamento = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text + "', Valor = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text + "', Data_Contracto = '" + FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text + "', Descritivo = '" + FormGerirServidores.AUX_Descricao + "', Nota = '" + FormGerirServidores.AUX_Nota + "' WHERE Nome = '" + FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.SelectedItem.ToString() + "'";

            OleDbCommand Command_AlterarServidor = new OleDbCommand();

            Command_AlterarServidor.CommandText = Query_AlterarServidor;
            Command_AlterarServidor.Connection = LigacaoDB;

            Command_AlterarServidor.ExecuteNonQuery();

            MessageBox.Show("Servidor Alterado com Sucesso!", "Servidor Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LigacaoDB.Close();
            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            try
            {
                LigacaoDB.Open();

                FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.Items.Add(Reader[0].ToString());
                    //LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Encomenda).ToString();
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_USERNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PASSWORD.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAdicionarServidor_DATA_CONTRACTO.Text = "";

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = "";

            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = "";
            FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = "";
        }

        public static void ListBox_SERVIDORES_TabPageRemover_SeleccaoItem() // Passar para as TextBox's da TabPage Remover Servidor os valores do Item selecionado na ListBox
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.SelectedItem.ToString();

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
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxHostName.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_HOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
            }

            while (Reader_RefreshTextBoxIP.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_IP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
            }

            while (Reader_RefreshTextBoxTipo.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxPlanoPagamento.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_PLANO_PAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDataContracto.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageRemoverServidor_DATA_CONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
            }

            LigacaoDB.Close();

            if (FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.SelectedItems != null)
                FormGerirServidores.BUTTON_FormGestaoServidores_TabPageRemoverServidor_REMOVER.Enabled = true;
            if (FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageRemoverServidor_SERVIDORES.SelectedItems == null)
                FormGerirServidores.BUTTON_FormGestaoServidores_TabPageRemoverServidor_REMOVER.Enabled = false;
        }

        public static void ListBox_SERVIDORES_TabPageAlterar_SeleccaoItem() // Passar para as TextBox's da TabPage Alterar Servidor os valores do Item selecionado na ListBox
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirServidores.LISTBOX_FormGestaoServidores_TabPageAlterarServidor_SERVIDORES.SelectedItem.ToString();

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
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxHostName.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_HOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
            }

            while (Reader_RefreshTextBoxIP.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_IP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
            }

            while (Reader_RefreshTextBoxUsername.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_USERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
            }

            while (Reader_RefreshTextBoxPassword.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PASSWORD.Text = Reader_RefreshTextBoxPassword["Login_Password"].ToString();
            }

            while (Reader_RefreshTextBoxTipo.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxPlanoPagamento.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_PLANO_PAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDataContracto.Read())
            {
                FormGerirServidores.TEXTBOX_FormGestaoServidores_TabPageAlterarServidor_DATA_CONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
            }

            while (Reader_RefreshTextBoxAlterarDescricao.Read())
            {
                FormGerirServidores.AUX_Descricao = Reader_RefreshTextBoxAlterarDescricao["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoxAlterarNota.Read())
            {
                FormGerirServidores.AUX_Nota = Reader_RefreshTextBoxAlterarNota["Nota"].ToString();
            }

            LigacaoDB.Close();
        }
    }
}

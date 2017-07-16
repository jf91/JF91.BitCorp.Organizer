using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public static class CLASS_InfoServidores
    {
        //public static OleDbConnection LigacaoDB;

        //public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        //public static FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO FormGerirServidoresAdicionarDescricao_Objects = (FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO)Application.OpenForms["FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARDESCRICAO"];

        //public static FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA FormGerirServidoresAdicionarNota_Objects = (FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA)Application.OpenForms["FORM_GERIR_SERVIDORES_ADICIONARSERVIDOR_ADICIONARNOTA"];

        //public static FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO FormGerirServidoresAlterarDescricao_Objects = (FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARDESCRICAO)Application.OpenForms["FORM_GERIR_SERVIDORES_ALTERAR_ALTERARDESCRICAO"];

        //public static FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_ FormGerirServidoresAlterarNota_Objects = (FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_)Application.OpenForms["FORM_GERIR_SERVIDORES_ALTERARSERVIDOR_ALTERARNOTA_"];

        //public static FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        //public static OleDbDataReader Reader;

        public static FORM_INICIO FormInicio = new FORM_INICIO();

        //public string AUX_Descricao;
        //public string AUX_Descricao2;
        //public string AUX_Descricao3;
        //public string AUX_Nota;
        //public string AUX_Nota2;
        //public string AUX_Nota3;
        //public string AUX_Nome;

        public static void Refresh_ListBox_SERVIDORES() // Actualizar ListBox de Servidores
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                FormInicio.LigacaoDB.Open();

                FormInicio.LISTVIEW_FormInicio_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = FormInicio.LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                FormInicio.Reader = Command_RefresListBox.ExecuteReader();

                while (FormInicio.Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(FormInicio.Reader[0].ToString());
                    FormInicio.LISTVIEW_FormInicio_SERVIDORES.Items.Add(FormInicio.Reader[0].ToString());
                }
                FormInicio.Reader.Close();

                FormInicio.LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void ListBox_SERVIDORES_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            FormInicio.LigacaoDB.Open();

            string ID = FormInicio.LISTVIEW_FormInicio_SERVIDORES.SelectedItem.ToString();

            string Query_RefreshTextBoxNome = "SELECT Nome FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxHostname = "SELECT Hostname FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxIP = "SELECT IP FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxPlanoPagamento = "SELECT Plano_Pagamento FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxValor = "SELECT Valor FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxDataContracto = "SELECT Data_Contracto FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxDescricao = "SELECT Descritivo FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxNota = "SELECT Nota FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxUsername = "SELECT Login_Username FROM Servidores WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxPassword = "SELECT Login_Password FROM Servidores WHERE Nome = '" + ID + "'";
            
            OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxHostName = new OleDbCommand(Query_RefreshTextBoxHostname, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxIP = new OleDbCommand(Query_RefreshTextBoxIP, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxPlanoPagamento = new OleDbCommand(Query_RefreshTextBoxPlanoPagamento, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxDataContracto = new OleDbCommand(Query_RefreshTextBoxDataContracto, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescricao, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxUsername = new OleDbCommand(Query_RefreshTextBoxUsername, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxPassword = new OleDbCommand(Query_RefreshTextBoxPassword, FormInicio.LigacaoDB);

            Command_RefreshTextBoxNome.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxHostName.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxIP.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxTipo.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxEntidade.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxPlanoPagamento.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxValor.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxDataContracto.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxDescricao.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxNota.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxUsername.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxPassword.Connection = FormInicio.LigacaoDB;

            OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxHostName = Command_RefreshTextBoxHostName.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxIP = Command_RefreshTextBoxIP.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxPlanoPagamento = Command_RefreshTextBoxPlanoPagamento.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxDataContracto = Command_RefreshTextBoxDataContracto.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxDescricao = Command_RefreshTextBoxDescricao.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoNota = Command_RefreshTextBoxNota.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxUsername = Command_RefreshTextBoxUsername.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoPassword = Command_RefreshTextBoxPassword.ExecuteReader();

            while (Reader_RefreshTextBoxNome.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxHostName.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxHOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
            }

            while (Reader_RefreshTextBoxIP.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxIP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
            }

            while (Reader_RefreshTextBoxTipo.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxTIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxPlanoPagamento.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxPLANOPAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxVALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDataContracto.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxDATACONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
            }

            while (Reader_RefreshTextBoxDescricao.Read())
            {
                FormInicio.TEXTBOX_FormInicio_SERVIDORES_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoNota.Read())
            {
                FormInicio.TEXTBOX_FormInicio_SERVIDORES_NOTA.Text = Reader_RefreshTextBoNota["Nota"].ToString();
            }

            while (Reader_RefreshTextBoxUsername.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxUSERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
            }

            while (Reader_RefreshTextBoPassword.Read())
            {
                FormInicio.LABEL_FormInicio_SERVIDORES_auxPASSWORD.Text = Reader_RefreshTextBoPassword["Login_Password"].ToString();
            }

            FormInicio.LigacaoDB.Close();
        }
    }
}


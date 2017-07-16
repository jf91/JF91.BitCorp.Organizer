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

namespace BC_Organizer
{
    public partial class FORM_INICIO : Form
    {
        #region OBJECTOS PUBLICOS
        public OleDbConnection LigacaoDB;

        public OleDbDataReader Reader;

        public string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";
        #endregion

        #region PROGRAMA

        public FORM_INICIO()
        {
            InitializeComponent();            

            LigacaoDB = new OleDbConnection(EnderecoDB);

            Inicio_Refresh_ListBox_SERVIDORES();

            Inicio_Refresh_ListBox_PROJECTOS();

            Inicio_Refresh_ListBox_ENCOMENDAS();

            Inicio_Refresh_ListBox_SERVICOS();

            Inicio_Refresh_ListBox_NOTAS();

            Limpar_Labels_AUX();
        }

        private void MENUSTRIP_INICIO_BUTTON_SAIR_Click(object sender, EventArgs e)
        {
            Application.Exit();           
        }

        private void PICTUREBOX_INICIO_SERVIDORES_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 1;
        }

        private void LABEL_INICIO_PAGINA_PRINCIPAL_SERVIDORES_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 1;
        }

        private void PICTUREBOX_INICIO_PROJECTOS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 2;
        }

        private void LABEL_FormInicio_PAGINA_PRINCIPAL_PROJECTOS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 2;
        }

        private void PICTUREBOX_FormInicio_ENCOMENDAS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 3;
        }

        private void LABEL_FormInicio_PAGINA_PRINCIPAL_ENCOMENDAS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 3;
        }

        private void PICTUREBOX_FormInicio_SERVICOS_INTERNET_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 4;
        }

        private void LABEL_FormInicio_PAGINA_PRINCIPAL_SERVICOS_INTERNET_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 4;
        }

        private void PICTUREBOX_FormInicio_NOTAS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 5;
        }

        private void LABEL_FormInicio_PAGINA_PRINCIPAL_NOTAS_Click(object sender, EventArgs e)
        {
            TABCONTROL_FormInicio.SelectedIndex = 5;
        }

        private void MENUSTRIP_FormInicio_BUTTON_GERIR_SERVIDORES_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVIDORES FormGestaoServidores = new FORM_GERIR_SERVIDORES();
            FormGestaoServidores.ShowDialog();
        }

        private void MENUSTRIP_FormInicio_BUTTON_GERIR_PROJECTOS_Click(object sender, EventArgs e)
        {
            FORM_GERIR_PROJECTOS FormGestaoProjectos = new FORM_GERIR_PROJECTOS();
            FormGestaoProjectos.ShowDialog();
        }

        private void MENUSTRIP_FormInicio_BUTTON_GERIR_ENCOMENDAS_Click(object sender, EventArgs e)
        {
            FORM_GERIR_ENCOMENDAS FormGestaoEncomendas = new FORM_GERIR_ENCOMENDAS();
            FormGestaoEncomendas.ShowDialog();
        }

        private void MENUSTRIP_FormInicio_BUTTON_GERIR_SERVICOS_INTERNET_Click(object sender, EventArgs e)
        {
            FORM_GERIR_SERVICOS_INTERNET FormGestaoServicosInternet = new FORM_GERIR_SERVICOS_INTERNET();
            FormGestaoServicosInternet.ShowDialog();
        }

        private void MENUSTRIP_FormInicio_BUTTON_GERIR_NOTAS_Click(object sender, EventArgs e)
        {
            FORM_GERIR_NOTAS FormGestaoNotas = new FORM_GERIR_NOTAS();
            FormGestaoNotas.ShowDialog();
        }

        private void MENUSTRIP_FormInicio_BUTTON_SOBRE_Click(object sender, EventArgs e)
        {
            FORM_SOBRE FormSobre = new FORM_SOBRE();
            FormSobre.ShowDialog();
        }

        private void LISTVIEW_FormInicio_SERVIDORES_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVIDORES_SeleccaoItem();
        }

        private void LISTVIEW_FormInicio_PROJECTOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_PROJECTOS_SeleccaoItem();
        }

        private void LISTVIEW_FormInicio_ENCOMENDAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_ENCOMENDAS_SeleccaoItem();
        }

        private void LISTVIEW_FormInicio_NOTAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_NOTAS_SeleccaoItem();
        }

        private void LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox_SERVICOS_SeleccaoItem();
        }

        #endregion

        #region FUNÇÕES DO FORM
        /*
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··

        ╒╬══════════════════════════════════╬╕
         ║ FUNÇÕES PARA USO DE OUTROS FORMS ║  -> FUNÇÕES PARA SEREM ACEDIDAS POR FORMS EXTERNOS
        ╘╬══════════════════════════════════╬╛
*/
        public void Refresh_ListBox_SERVIDORES() // Actualizar ListBox de Servidores
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_SERVIDORES.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            {  }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_PROJECTOS() // Actualizar ListBox de Projectos
        {
            LigacaoDB.Open();

            try
            { 
                LISTVIEW_FormInicio_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            {  }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_ENCOMENDAS() // Actualizar ListBox de Encomendas
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_ENCOMENDAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_ENCOMENDAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_NOTAS() // Actualizar ListBox de Notas
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_NOTAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            LigacaoDB.Close();
        }

        public void Refresh_ListBox_SERVICOS() // Actualizar ListBox de Encomendas
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM WebServices ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem ServicosInternet = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }

            LigacaoDB.Close();
        }


/*
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
*/

/*
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··

        ╒╬═════════════════╬╕
         ║ FUNÇÕES DA FORM ║  -> FUNÇÕES DO PRÓPRIO FORM
        ╘╬═════════════════╬╛
*/

//       ╔═════════════════════════════════╗
//       ║..: INFORMAÇÃO DOS SERVIDORES :..║

        public void Inicio_Refresh_ListBox_SERVIDORES() // Actualizar ListBox de Servidores
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_SERVIDORES.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Servidores ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_SERVIDORES.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_SERVIDORES_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTVIEW_FormInicio_SERVIDORES.SelectedItem.ToString();

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

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxHostName = new OleDbCommand(Query_RefreshTextBoxHostname, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxIP = new OleDbCommand(Query_RefreshTextBoxIP, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPlanoPagamento = new OleDbCommand(Query_RefreshTextBoxPlanoPagamento, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDataContracto = new OleDbCommand(Query_RefreshTextBoxDataContracto, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescricao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxUsername = new OleDbCommand(Query_RefreshTextBoxUsername, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPassword = new OleDbCommand(Query_RefreshTextBoxPassword, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxHostName.Connection = LigacaoDB;
                Command_RefreshTextBoxIP.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxPlanoPagamento.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDataContracto.Connection = LigacaoDB;
                Command_RefreshTextBoxDescricao.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;
                Command_RefreshTextBoxUsername.Connection = LigacaoDB;
                Command_RefreshTextBoxPassword.Connection = LigacaoDB;

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
                    LABEL_FormInicio_SERVIDORES_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxHostName.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxHOSTNAME.Text = Reader_RefreshTextBoxHostName["Hostname"].ToString();
                }

                while (Reader_RefreshTextBoxIP.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxIP.Text = Reader_RefreshTextBoxIP["IP"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxTIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxPlanoPagamento.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxPLANOPAGAMENTO.Text = Reader_RefreshTextBoxPlanoPagamento["Plano_Pagamento"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxVALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDataContracto.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxDATACONTRACTO.Text = Reader_RefreshTextBoxDataContracto["Data_Contracto"].ToString();
                }

                while (Reader_RefreshTextBoxDescricao.Read())
                {
                    TEXTBOX_FormInicio_SERVIDORES_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoNota.Read())
                {
                    TEXTBOX_FormInicio_SERVIDORES_NOTA.Text = Reader_RefreshTextBoNota["Nota"].ToString();
                }

                while (Reader_RefreshTextBoxUsername.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxUSERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
                }

                while (Reader_RefreshTextBoPassword.Read())
                {
                    LABEL_FormInicio_SERVIDORES_auxPASSWORD.Text = Reader_RefreshTextBoPassword["Login_Password"].ToString();
                }               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..: INFORMAÇÃO DOS PROJECTOS  :..║

        public void Inicio_Refresh_ListBox_PROJECTOS() // Actualizar ListBox de Projectos
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();               
            }

            catch (Exception EX)
            {  }

            LigacaoDB.Close();
        }

        public void ListBox_PROJECTOS_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTVIEW_FormInicio_PROJECTOS.SelectedItem.ToString();

                string Query_RefreshTextBoxNome = "SELECT Nome FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxTipo = "SELECT Tipo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxData = "SELECT Data FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxPrevisao = "SELECT Previsão FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxEstado = "SELECT Estado FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxDescricao = "SELECT Descritivo FROM Projectos WHERE Nome = '" + ID + "'";
                string Query_RefreshTextBoxNota = "SELECT Nota FROM Projectos WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxTipo = new OleDbCommand(Query_RefreshTextBoxTipo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxPrevisao = new OleDbCommand(Query_RefreshTextBoxPrevisao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescricao, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxTipo.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxPrevisao.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxDescricao.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxTipo = Command_RefreshTextBoxTipo.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxPrevisao = Command_RefreshTextBoxPrevisao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescricao = Command_RefreshTextBoxDescricao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxTIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxDATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxPrevisao.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxPREVISAO.Text = Reader_RefreshTextBoxPrevisao["Previsão"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    LABEL_FormInicio_PROJECTOS_auxESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxDescricao.Read())
                {
                    TEXTBOX_FormInicio_PROJECTOS_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    TEXTBOX_FormInicio_PROJECTOS_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
                }             
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..: INFORMAÇÃO DAS ENCOMENDAS :..║

        public void Inicio_Refresh_ListBox_ENCOMENDAS() // Actualizar ListBox de Encomendas
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_ENCOMENDAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_ENCOMENDAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();               
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_ENCOMENDAS_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTVIEW_FormInicio_ENCOMENDAS.SelectedItem.ToString();

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
                OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
                OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

                Command_RefreshTextBoxNome.Connection = LigacaoDB;
                Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
                Command_RefreshTextBoxData.Connection = LigacaoDB;
                Command_RefreshTextBoxEstado.Connection = LigacaoDB;
                Command_RefreshTextBoxValor.Connection = LigacaoDB;
                Command_RefreshTextBoxDescricao.Connection = LigacaoDB;
                Command_RefreshTextBoxNota.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxDescricao = Command_RefreshTextBoxDescricao.ExecuteReader();
                OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

                while (Reader_RefreshTextBoxNome.Read())
                {
                    LABEL_FormInicio_ENCOMENDAS_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    LABEL_FormInicio_ENCOMENDAS_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxData.Read())
                {
                    LABEL_FormInicio_ENCOMENDAS_auxDATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    LABEL_FormInicio_ENCOMENDAS_auxESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    LABEL_FormInicio_ENCOMENDAS_auxVALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxDescricao.Read())
                {
                    TEXTBOX_FormInicio_ENCOMENDAS_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    TEXTBOX_FormInicio_ENCOMENDAS_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
                }               
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═══════════════════════════════════════════╗
//       ║..: INFORMAÇÃO DOS SERVICOS DE INTERNET :..║

        public void Inicio_Refresh_ListBox_SERVICOS() // Actualizar ListBox de Encomendas
        {
            LigacaoDB.Open();

            try
            {
                LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM WebServices ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();               
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_SERVICOS_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTBOX_FormInicio_SERVICOSINTERNET_SERVICOS.SelectedItem.ToString();

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
                    LABEL_FormInicio_SERVICOSINTERNET_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
                }

                while (Reader_RefreshTextBoxEntidade.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
                }

                while (Reader_RefreshTextBoxTipo.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxTIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
                }

                while (Reader_RefreshTextBoxUsername.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxUSERNAME.Text = Reader_RefreshTextBoxUsername["Login_Username"].ToString();
                }

                while (Reader_RefreshTextBoxPassword.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxPASSWORD.Text = Reader_RefreshTextBoxPassword["Login_Password"].ToString();
                }

                while (Reader_RefreshTextBoxDataAssinatura.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxDATAASSINATURA.Text = Reader_RefreshTextBoxDataAssinatura["Data_Assinatura"].ToString();
                }

                while (Reader_RefreshTextBoxValor.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxVALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
                }

                while (Reader_RefreshTextBoxEstado.Read())
                {
                    LABEL_FormInicio_SERVICOSINTERNET_auxESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
                }

                while (Reader_RefreshTextBoxDescricao.Read())
                {
                    TEXTBOX_FormInicio_SERVICOSINTERNET_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
                }

                while (Reader_RefreshTextBoxNota.Read())
                {
                    TEXTBOX_FormInicio_SERVICOSINTERNET_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
                }
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:   INFORMAÇÃO DAS NOTAS    :..║

        public void Inicio_Refresh_ListBox_NOTAS() // Actualizar ListBox de Notas
        {
            LigacaoDB.Open();

            try
            {
                LISTVIEW_FormInicio_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    LISTVIEW_FormInicio_NOTAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();                
            }

            catch (Exception EX)
            { }

            LigacaoDB.Close();
        }

        public void ListBox_NOTAS_SeleccaoItem() // Passar para as TextBox's os valores do Item selecionado na ListBox
        {
            LigacaoDB.Open();

            try
            {
                string ID = LISTVIEW_FormInicio_NOTAS.SelectedItem.ToString();

                string Query_RefreshTextBoxConteudo = "SELECT Nota FROM Notas WHERE Nome = '" + ID + "'";

                OleDbCommand Command_RefreshTextBoxConteudo = new OleDbCommand(Query_RefreshTextBoxConteudo, LigacaoDB);

                Command_RefreshTextBoxConteudo.Connection = LigacaoDB;

                OleDbDataReader Reader_RefreshTextBoxConteudo = Command_RefreshTextBoxConteudo.ExecuteReader();

                while (Reader_RefreshTextBoxConteudo.Read())
                {
                    TEXTBOX_FormInicio_NOTAS_CONTEUDO.Text = Reader_RefreshTextBoxConteudo["Nota"].ToString();
                }                
            }

            catch(Exception EX)
            { }

            LigacaoDB.Close();
        }

//       ╔═════════════════════════════════╗
//       ║..:  LIMPAR LABELS AUXILIARES :..║

        public void Limpar_Labels_AUX() // Limpar todas as labels auxiliares e textbox's usadas apenas para informação
        {
            try
            {
                LABEL_FormInicio_SERVIDORES_auxNOME.Text = "";
                LABEL_FormInicio_SERVIDORES_auxHOSTNAME.Text = "";
                LABEL_FormInicio_SERVIDORES_auxIP.Text = "";
                LABEL_FormInicio_SERVIDORES_auxTIPO.Text = "";
                LABEL_FormInicio_SERVIDORES_auxENTIDADE.Text = "";
                LABEL_FormInicio_SERVIDORES_auxPLANOPAGAMENTO.Text = "";
                LABEL_FormInicio_SERVIDORES_auxVALOR.Text = "";
                LABEL_FormInicio_SERVIDORES_auxDATACONTRACTO.Text = "";
                LABEL_FormInicio_SERVIDORES_auxUSERNAME.Text = "";
                LABEL_FormInicio_SERVIDORES_auxPASSWORD.Text = "";
                TEXTBOX_FormInicio_SERVIDORES_DESCRICAO.Text = "";
                TEXTBOX_FormInicio_SERVIDORES_NOTA.Text = "";

                LABEL_FormInicio_PROJECTOS_auxNOME.Text = "";
                LABEL_FormInicio_PROJECTOS_auxTIPO.Text = "";
                LABEL_FormInicio_PROJECTOS_auxENTIDADE.Text = "";
                LABEL_FormInicio_PROJECTOS_auxDATA.Text = "";
                LABEL_FormInicio_PROJECTOS_auxPREVISAO.Text = "";
                LABEL_FormInicio_PROJECTOS_auxESTADO.Text = "";
                TEXTBOX_FormInicio_PROJECTOS_DESCRICAO.Text = "";
                TEXTBOX_FormInicio_PROJECTOS_NOTA.Text = "";

                LABEL_FormInicio_ENCOMENDAS_auxNOME.Text = "";
                LABEL_FormInicio_ENCOMENDAS_auxENTIDADE.Text = "";
                LABEL_FormInicio_ENCOMENDAS_auxDATA.Text = "";
                LABEL_FormInicio_ENCOMENDAS_auxESTADO.Text = "";
                LABEL_FormInicio_ENCOMENDAS_auxVALOR.Text = "";
                TEXTBOX_FormInicio_ENCOMENDAS_DESCRICAO.Text = "";
                TEXTBOX_FormInicio_ENCOMENDAS_NOTA.Text = "";

                LABEL_FormInicio_SERVICOSINTERNET_auxNOME.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxENTIDADE.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxTIPO.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxUSERNAME.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxPASSWORD.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxDATAASSINATURA.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxVALOR.Text = "";
                LABEL_FormInicio_SERVICOSINTERNET_auxESTADO.Text = "";
                TEXTBOX_FormInicio_SERVICOSINTERNET_DESCRICAO.Text = "";
                TEXTBOX_FormInicio_SERVICOSINTERNET_NOTA.Text = "";

                TEXTBOX_FormInicio_NOTAS_CONTEUDO.Text = "";
            }

            catch(Exception EX)
            { }
        }

/*
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
··•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••····•••·· ··•••·· ··•••·· ··•••·· ··•••·· ··•••··
*/
        #endregion
    }
}

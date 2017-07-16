using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public static class CLASS_InfoEncomendas
    {
        //public static OleDbConnection LigacaoDB;

        //public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        //public static FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO FormGerirEncomendasAdicionarDescricao_Objects = (FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO"];
        //public static FORM_GERIR_ENCOMENDAS_ADICIONARNOTA FormGerirEncomendasAdicionarNota_Objects = (FORM_GERIR_ENCOMENDAS_ADICIONARNOTA)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ADICIONARNOTA"];

        //public static FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO FormGerirEncomendasAlterarDescricao_Objects = (FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO"];
        //public static FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA FormGerirEncomendasAlterarNota_Objects = (FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA"];

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

        public static void Refresh_ListBox_ENCOMENDAS()
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                FormInicio.LigacaoDB.Open();

                FormInicio.LISTVIEW_FormInicio_ENCOMENDAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = FormInicio.LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                FormInicio.Reader = Command_RefresListBox.ExecuteReader();

                while (FormInicio.Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(FormInicio.Reader[0].ToString());
                    FormInicio.LISTVIEW_FormInicio_ENCOMENDAS.Items.Add(FormInicio.Reader[0].ToString());
                }
                FormInicio.Reader.Close();
                
                //FormInicio.LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void ListBox_ENCOMENDAS_SeleccaoItem()
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            FormInicio.LigacaoDB.Open();

            string ID = FormInicio.LISTVIEW_FormInicio_ENCOMENDAS.SelectedItem.ToString();

            string Query_RefreshTextBoxNome = "SELECT Nome FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxEntidade = "SELECT Entidade FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxData = "SELECT Data FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxEstado = "SELECT Estado FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxValor = "SELECT Valor FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxDescritivo = "SELECT Descritivo FROM Encomendas WHERE Nome = '" + ID + "'";
            string Query_RefreshTextBoxNota = "SELECT Nota FROM Encomendas WHERE Nome = '" + ID + "'";

            OleDbCommand Command_RefreshTextBoxNome = new OleDbCommand(Query_RefreshTextBoxNome, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxEntidade = new OleDbCommand(Query_RefreshTextBoxEntidade, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxData = new OleDbCommand(Query_RefreshTextBoxData, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxEstado = new OleDbCommand(Query_RefreshTextBoxEstado, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxValor = new OleDbCommand(Query_RefreshTextBoxValor, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxDescricao = new OleDbCommand(Query_RefreshTextBoxDescritivo, FormInicio.LigacaoDB);
            OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, FormInicio.LigacaoDB);

            Command_RefreshTextBoxNome.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxEntidade.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxData.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxEstado.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxValor.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxDescricao.Connection = FormInicio.LigacaoDB;
            Command_RefreshTextBoxNota.Connection = FormInicio.LigacaoDB;

            OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxDescricao = Command_RefreshTextBoxDescricao.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

            while (Reader_RefreshTextBoxNome.Read())
            {
                FormInicio.LABEL_FormInicio_ENCOMENDAS_auxNOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormInicio.LABEL_FormInicio_ENCOMENDAS_auxENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxData.Read())
            {
                FormInicio.LABEL_FormInicio_ENCOMENDAS_auxDATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
            }

            while (Reader_RefreshTextBoxEstado.Read())
            {
                FormInicio.LABEL_FormInicio_ENCOMENDAS_auxESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormInicio.LABEL_FormInicio_ENCOMENDAS_auxVALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDescricao.Read())
            {
                FormInicio.TEXTBOX_FormInicio_ENCOMENDAS_DESCRICAO.Text = Reader_RefreshTextBoxDescricao["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormInicio.TEXTBOX_FormInicio_ENCOMENDAS_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            FormInicio.LigacaoDB.Close();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public static class CLASS_InfoNotas
    {
        //public static OleDbConnection LigacaoDB;

        //public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        //public static OleDbDataReader Reader;

        public static FORM_INICIO FormInicio = new FORM_INICIO();

        public static void Refresh_ListBox_NOTAS()
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                FormInicio.LigacaoDB.Open();

                FormInicio.LISTVIEW_FormInicio_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = FormInicio.LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                FormInicio.Reader = Command_RefresListBox.ExecuteReader();

                while (FormInicio.Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(FormInicio.Reader[0].ToString());
                    FormInicio.LISTVIEW_FormInicio_NOTAS.Items.Add(FormInicio.Reader[0].ToString());
                }
                FormInicio.Reader.Close();

                FormInicio.LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void ListBox_NOTAS_SeleccaoItem()
        {
            //LigacaoDB = new OleDbConnection(EnderecoDB);

            FormInicio.LigacaoDB.Open();

            string ID = FormInicio.LISTVIEW_FormInicio_NOTAS.SelectedItem.ToString();

            string Query_RefreshTextBoxConteudo = "SELECT Nota FROM Notas WHERE Nome = '" + ID + "'";

            OleDbCommand Command_RefreshTextBoxConteudo = new OleDbCommand(Query_RefreshTextBoxConteudo, FormInicio.LigacaoDB);

            Command_RefreshTextBoxConteudo.Connection = FormInicio.LigacaoDB;

            OleDbDataReader Reader_RefreshTextBoxConteudo = Command_RefreshTextBoxConteudo.ExecuteReader();

            while (Reader_RefreshTextBoxConteudo.Read())
            {
                FormInicio.TEXTBOX_FormInicio_NOTAS_CONTEUDO.Text = Reader_RefreshTextBoxConteudo["Nota"].ToString();
            }

            FormInicio.LigacaoDB.Close();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public class CLASS_GestaoNotas
    {
        public static OleDbConnection LigacaoDB;

        public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        public static OleDbDataReader Reader;

        public static FORM_GERIR_NOTAS FormGerirNotas = new FORM_GERIR_NOTAS();

        public static void Refresh_ListBox_TagPageRemoverNota()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void Refresh_ListBox_TabPageAlterarNota()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Add(Reader[0].ToString());                    
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void AdicionarNota()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            if (FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text != "")
            {
                try
                {
                    LigacaoDB.Open();

                    OleDbCommand Command_AdicionarNota = new OleDbCommand();

                    string Query_AdicionarEncomenda = "INSERT INTO Notas(Nome, Nota) VALUES('" + FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text + "','" + FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text + "');";

                    Command_AdicionarNota.CommandText = Query_AdicionarEncomenda;
                    Command_AdicionarNota.Connection = LigacaoDB;

                    Command_AdicionarNota.ExecuteNonQuery();

                    FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
                    FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

                    MessageBox.Show("Nota Inserida com Sucesso!", "Nota Adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LigacaoDB.Close();

                    try
                    {
                        LigacaoDB.Open();

                        FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Clear();

                        OleDbCommand Command_RefresListBox = new OleDbCommand();
                        Command_RefresListBox.Connection = LigacaoDB;

                        string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                        Command_RefresListBox.CommandText = Query_RefresListBox;

                        Reader = Command_RefresListBox.ExecuteReader();

                        while (Reader.Read())
                        {
                            ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                            FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.Items.Add(Reader[0].ToString());
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

                        FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Clear();

                        OleDbCommand Command_RefresListBox = new OleDbCommand();
                        Command_RefresListBox.Connection = LigacaoDB;

                        string Query_RefresListBox = "SELECT Nome FROM Notas ORDER BY ID ASC";

                        Command_RefresListBox.CommandText = Query_RefresListBox;

                        Reader = Command_RefresListBox.ExecuteReader();

                        while (Reader.Read())
                        {
                            ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                            FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.Items.Add(Reader[0].ToString());
                            //LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Encomenda).ToString();
                        }
                        Reader.Close();

                        LigacaoDB.Close();
                    }

                    catch (Exception EX)
                    {
                        MessageBox.Show(EX.Message.ToString());
                    }
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
                MessageBox.Show("Tem de indicar pelo menos um nome para a nota!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = "";
        }

        public static void RemoverNota()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_DeleteNota = "DELETE FROM Notas WHERE Nome = '" + FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text + "'";

            OleDbCommand Command_RemoverNota = new OleDbCommand(Query_DeleteNota, LigacaoDB);

            Command_RemoverNota.CommandText = Query_DeleteNota;
            Command_RemoverNota.Connection = LigacaoDB;

            Command_RemoverNota.ExecuteNonQuery();

            LigacaoDB.Close();

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = "";
        }

        public static void AlterarNota()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_AlterarNota = "UPDATE Notas SET Nome = '" + FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text + "', Nota = '" + FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text + "' WHERE Nome = '" + FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.SelectedItem.ToString() + "'";

            OleDbCommand Command_AlterarNota = new OleDbCommand();

            Command_AlterarNota.CommandText = Query_AlterarNota;
            Command_AlterarNota.Connection = LigacaoDB;

            Command_AlterarNota.ExecuteNonQuery();

            MessageBox.Show("Nota Alterada com Sucesso!", "Nota Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LigacaoDB.Close();

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAdicionarNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = "";

            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = "";
            FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = "";
        }

        public static void ListBox_NOTA_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageRemoverNota_NOTAS.SelectedItem.ToString();

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
                FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageRemoverNota_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            LigacaoDB.Close();
        }

        public static void ListBox_NOTAS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirNotas.LISTBOX_FormGestaoNotas_TabPageAlterarNota_NOTAS.SelectedItem.ToString();

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
                FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormGerirNotas.TEXTBOX_FormGestaoNotas_TabPageAlterarNota_NOTA.Text = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            LigacaoDB.Close();
        }
    }
}

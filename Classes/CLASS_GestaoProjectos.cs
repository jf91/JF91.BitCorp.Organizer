using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public class CLASS_GestaoProjectos
    {
        public static OleDbConnection LigacaoDB;

        public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        public static FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARDESCRICAO FormGerirProjectosAdicionarDescricao_Objects = (FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARDESCRICAO)Application.OpenForms["FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARDESCRICAO"];
        public static FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA FormGerirProjectosAdicionarNota_Objects = (FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA)Application.OpenForms["FORM_GERIR_PROJECTOS_ADICIONARPROJECTO_ADICIONARNOTA"];

        public static FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARDESCRICAO FormGerirProjectosAlterarDescricao_Objects = (FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARDESCRICAO)Application.OpenForms["FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARDESCRICAO"];
        public static FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARNOTA FormGerirProjectosAlterarNota_Objects = (FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARNOTA)Application.OpenForms["FORM_GERIR_PROJECTOS_ALTERARPROJECTO_ALTERARNOTA"];

        public static FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        public static OleDbDataReader Reader;

        public static FORM_GERIR_PROJECTOS FormGerirProjectos = new FORM_GERIR_PROJECTOS();

        public string AUX_Descricao;
        public string AUX_Descricao2;
        public string AUX_Descricao3;
        public string AUX_Nota;
        public string AUX_Nota2;
        public string AUX_Nota3;
        public string AUX_Nome;

        public static void Refresh_ListBox_TagPageRemoverProjecto()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void Refresh_ListBox_TabPageAlterarProjecto()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void AdicionarProjecto()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            if (FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text != "")
            {
                try
                {
                    LigacaoDB.Open();

                    OleDbCommand Command_AdicionarProjecto = new OleDbCommand();

                    string Query_AdicionarProjecto = "INSERT INTO Projectos(Nome, Tipo, Entidade, Data, Previsão, Estado, Descritivo, Nota) VALUES('" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text + "','" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text + "','" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text + "','" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text + "','" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text + "', '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text + "', '" + FormGerirProjectos.AUX_Descricao + "', '" + FormGerirProjectos.AUX_Nota + "');";

                    Command_AdicionarProjecto.CommandText = Query_AdicionarProjecto;
                    Command_AdicionarProjecto.Connection = LigacaoDB;

                    Command_AdicionarProjecto.ExecuteNonQuery();

                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text = "";
                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text = "";
                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text = "";
                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text = "";
                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text = "";
                    FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text = "";

                    MessageBox.Show("Projecto Inserido com Sucesso!", "Projecto Adicionado", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LigacaoDB.Close();

                    try
                    {
                        LigacaoDB.Open();

                        FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Clear();

                        OleDbCommand Command_RefresListBox = new OleDbCommand();
                        Command_RefresListBox.Connection = LigacaoDB;

                        string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                        Command_RefresListBox.CommandText = Query_RefresListBox;

                        Reader = Command_RefresListBox.ExecuteReader();

                        while (Reader.Read())
                        {
                            ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                            FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
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

                        FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Clear();

                        OleDbCommand Command_RefresListBox = new OleDbCommand();
                        Command_RefresListBox.Connection = LigacaoDB;

                        string Query_RefresListBox = "SELECT Nome FROM Projectos ORDER BY ID ASC";

                        Command_RefresListBox.CommandText = Query_RefresListBox;

                        Reader = Command_RefresListBox.ExecuteReader();

                        while (Reader.Read())
                        {
                            ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                            FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.Items.Add(Reader[0].ToString());
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
                MessageBox.Show("Tem de indicar pelo menos um nome para o projecto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void RemoverProjecto()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_DeleteProjecto = "DELETE FROM Projectos WHERE Nome = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text + "'";

            OleDbCommand Command_RemoverProjecto = new OleDbCommand(Query_DeleteProjecto, LigacaoDB);

            Command_RemoverProjecto.CommandText = Query_DeleteProjecto;
            Command_RemoverProjecto.Connection = LigacaoDB;

            Command_RemoverProjecto.ExecuteNonQuery();

            LigacaoDB.Close();

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text = "";

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ESTADO.Text = "";

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text = "";
        }

        public static void AlterarProjecto()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_AlterarProjecto = "UPDATE Projectos SET Nome = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text + "', Tipo = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text + "', Entidade = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text + "', Data = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text + "', Previsão = '" + FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text + "', Descritivo = '" + FormGerirProjectos.AUX_Descricao2 + "', Nota = '" + FormGerirProjectos.AUX_Nota2 + "' WHERE Nome = '" + FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.SelectedItem.ToString() + "'";

            OleDbCommand Command_AlterarProjecto = new OleDbCommand();

            Command_AlterarProjecto.CommandText = Query_AlterarProjecto;
            Command_AlterarProjecto.Connection = LigacaoDB;

            Command_AlterarProjecto.ExecuteNonQuery();

            MessageBox.Show("Projecto Alterado com Sucesso!", "Projecto Alterado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LigacaoDB.Close();

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAdicionarProjecto_ESTADO.Text = "";

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ESTADO.Text = "";

            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text = "";
            FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text = "";
        }

        public static void ListBox_PROJECTOS_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItem.ToString();

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
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxTipo.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxData.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
            }

            while (Reader_RefreshTextBoxPrevisao.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PREVISAO.Text = Reader_RefreshTextBoxPrevisao["Previsão"].ToString();
            }

            while (Reader_RefreshTextBoxEstado.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageRemoverProjecto_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
            }

            LigacaoDB.Close();

            if (FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItems != null)
                FormGerirProjectos.BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = true;
            if (FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageRemoverProjecto_PROJECTOS.SelectedItems == null)
                FormGerirProjectos.BUTTON_FormGestaoProjectos_TabPageRemoverProjecto_REMOVER.Enabled = false;
        }

        public static void ListBox_PROJECTOS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirProjectos.LISTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PROJECTOS.SelectedItem.ToString();

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
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxTipo.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_TIPO.Text = Reader_RefreshTextBoxTipo["Tipo"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxData.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
            }

            while (Reader_RefreshTextBoxPrevisao.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_PREVISAO.Text = Reader_RefreshTextBoxPrevisao["Previsão"].ToString();
            }

            while (Reader_RefreshTextBoxEstado.Read())
            {
                FormGerirProjectos.TEXTBOX_FormGestaoProjectos_TabPageAlterarProjecto_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
            }

            while (Reader_RefreshTextBoxDescritivo.Read())
            {
                FormGerirProjectos.AUX_Descricao2 = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormGerirProjectos.AUX_Nota2 = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            LigacaoDB.Close();
        }
    }
}

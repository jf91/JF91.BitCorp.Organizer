using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Windows.Forms;

namespace BC_Organizer
{
    public class CLASS_GestaoEncomendas
    {
        public static OleDbConnection LigacaoDB;

        public static string EnderecoDB = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=.\data.accdb; Jet OLEDB:Database Password=haze6n!root;";

        public static FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO FormGerirEncomendasAdicionarDescricao_Objects = (FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ADICIONARENCOMENDA_ADICIONARDESCRICAO"];
        public static FORM_GERIR_ENCOMENDAS_ADICIONARNOTA FormGerirEncomendasAdicionarNota_Objects = (FORM_GERIR_ENCOMENDAS_ADICIONARNOTA)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ADICIONARNOTA"];

        public static FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO FormGerirEncomendasAlterarDescricao_Objects = (FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARDESCRICAO"];
        public static FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA FormGerirEncomendasAlterarNota_Objects = (FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA)Application.OpenForms["FORM_GERIR_ENCOMENDAS_ALTERARENCOMENDA_ALTERARNOTA"];

        public static FORM_INICIO FormInicio_Objects = (FORM_INICIO)Application.OpenForms["FORM_INICIO"];

        public static OleDbDataReader Reader;

        public string AUX_Descricao;
        public string AUX_Descricao2;
        public string AUX_Descricao3;
        public string AUX_Nota;
        public string AUX_Nota2;
        public string AUX_Nota3;
        public string AUX_Nome;
        
        public static FORM_GERIR_ENCOMENDAS FormGerirEncomendas = new FORM_GERIR_ENCOMENDAS();
        
        public static void Refresh_ListBox_TagPageRemoverEncomenda()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.Items.Add(Reader[0].ToString());
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

        public static void Refresh_ListBox_TabPageAlterarEncomenda()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            try
            {
                LigacaoDB.Open();

                FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.Items.Clear();

                OleDbCommand Command_RefresListBox = new OleDbCommand();
                Command_RefresListBox.Connection = LigacaoDB;

                string Query_RefresListBox = "SELECT Nome FROM Encomendas ORDER BY ID ASC";

                Command_RefresListBox.CommandText = Query_RefresListBox;

                Reader = Command_RefresListBox.ExecuteReader();

                while (Reader.Read())
                {
                    ListViewItem Encomenda = new ListViewItem(Reader[0].ToString());
                    FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.Items.Add(Reader[0].ToString());
                }
                Reader.Close();

                LigacaoDB.Close();
            }

            catch (Exception EX)
            {
                MessageBox.Show(EX.Message.ToString());
            }
        }

        public static void AdicionarEncomenda()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            if (FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text != "")
            {
                try
                {
                    LigacaoDB.Open();

                    OleDbCommand Command_AdicionarEncomenda = new OleDbCommand();

                    string AdicionarDescricao = FormGerirEncomendas.AUX_Descricao;

                    string Query_AdicionarEncomenda = "INSERT INTO Encomendas(Nome, Entidade, Data, Estado, Valor, Descritivo, Nota) VALUES('" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text + "','" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text + "','" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text + "','" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text + "','" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text + "', '" + FormGerirEncomendas.AUX_Descricao + "', '" + FormGerirEncomendas.AUX_Nota + "');";

                    Command_AdicionarEncomenda.CommandText = Query_AdicionarEncomenda;
                    Command_AdicionarEncomenda.Connection = LigacaoDB;

                    Command_AdicionarEncomenda.ExecuteNonQuery();

                    FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text = "";
                    FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text = "";
                    FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text = "";
                    FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text = "";
                    FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text = "";

                    MessageBox.Show("Encomenda Inserida com Sucesso!", "Encomenda Adicionada", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LigacaoDB.Close();
                }

                catch (Exception EX)
                {
                    MessageBox.Show(EX.Message.ToString(), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
                MessageBox.Show("Tem de indicar pelo menos um nome para a encomenda", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public static void RemoverEncomenda()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_DeleteEncomenda = "DELETE FROM Encomendas WHERE Nome = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text + "'";

            OleDbCommand Command_RemoverEncomenda = new OleDbCommand(Query_DeleteEncomenda, LigacaoDB);

            Command_RemoverEncomenda.CommandText = Query_DeleteEncomenda;
            Command_RemoverEncomenda.Connection = LigacaoDB;

            Command_RemoverEncomenda.ExecuteNonQuery();

            LigacaoDB.Close();

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text = "";

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_VALOR.Text = "";

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text = "";
        }

        public static void AlterarEncomenda()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string Query_AlterarEncomenda = "UPDATE Encomendas SET Nome = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text + "', Entidade = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text + "', Data = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text + "', Estado = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text + "', Valor = '" + FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text + "', Descritivo = '" + FormGerirEncomendas.AUX_Descricao3 + "', Nota = '" +FormGerirEncomendas.AUX_Nota3 + "' WHERE Nome = '" + FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.SelectedItem.ToString() + "'";

            OleDbCommand Command_AlterarEncomenda = new OleDbCommand();

            Command_AlterarEncomenda.CommandText = Query_AlterarEncomenda;
            Command_AlterarEncomenda.Connection = LigacaoDB;

            Command_AlterarEncomenda.ExecuteNonQuery();

            MessageBox.Show("Encomenda Alterada com Sucesso!", "Encomenda Alterada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LigacaoDB.Close();

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAdicionarEncomenda_VALOR.Text = "";

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_VALOR.Text = "";

            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text = "";
            FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text = "";
        }

        public static void ListBox_ENCOMENDAS_TabPageRemover_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItem.ToString();

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
            OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
            OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

            Command_RefreshTextBoxNome.Connection = LigacaoDB;
            Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
            Command_RefreshTextBoxData.Connection = LigacaoDB;
            Command_RefreshTextBoxEstado.Connection = LigacaoDB;
            Command_RefreshTextBoxValor.Connection = LigacaoDB;
            Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
            Command_RefreshTextBoxNota.Connection = LigacaoDB;

            OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

            while (Reader_RefreshTextBoxNome.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxData.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
            }

            while (Reader_RefreshTextBoxEstado.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageRemoverEncomenda_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDescritivo.Read())
            {
                FormGerirEncomendas.AUX_Descricao3 = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormGerirEncomendas.AUX_Nota3 = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            LigacaoDB.Close();

            if (FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems != null)
                FormGerirEncomendas.BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = true;
            if (FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems == null)
                FormGerirEncomendas.BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = false;
        }

        public static void ListBox_ENCOMENDAS_TabPageAlterar_SeleccaoItem()
        {
            LigacaoDB = new OleDbConnection(EnderecoDB);

            LigacaoDB.Open();

            string ID = FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda.SelectedItem.ToString();

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
            OleDbCommand Command_RefreshTextBoxDescritivo = new OleDbCommand(Query_RefreshTextBoxDescritivo, LigacaoDB);
            OleDbCommand Command_RefreshTextBoxNota = new OleDbCommand(Query_RefreshTextBoxNota, LigacaoDB);

            Command_RefreshTextBoxNome.Connection = LigacaoDB;
            Command_RefreshTextBoxEntidade.Connection = LigacaoDB;
            Command_RefreshTextBoxData.Connection = LigacaoDB;
            Command_RefreshTextBoxEstado.Connection = LigacaoDB;
            Command_RefreshTextBoxValor.Connection = LigacaoDB;
            Command_RefreshTextBoxDescritivo.Connection = LigacaoDB;
            Command_RefreshTextBoxNota.Connection = LigacaoDB;

            OleDbDataReader Reader_RefreshTextBoxNome = Command_RefreshTextBoxNome.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEntidade = Command_RefreshTextBoxEntidade.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxData = Command_RefreshTextBoxData.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxEstado = Command_RefreshTextBoxEstado.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxValor = Command_RefreshTextBoxValor.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxDescritivo = Command_RefreshTextBoxDescritivo.ExecuteReader();
            OleDbDataReader Reader_RefreshTextBoxNota = Command_RefreshTextBoxNota.ExecuteReader();

            while (Reader_RefreshTextBoxNome.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_NOME.Text = Reader_RefreshTextBoxNome["Nome"].ToString();
            }

            while (Reader_RefreshTextBoxEntidade.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ENTIDADE.Text = Reader_RefreshTextBoxEntidade["Entidade"].ToString();
            }

            while (Reader_RefreshTextBoxData.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_DATA.Text = Reader_RefreshTextBoxData["Data"].ToString();
            }

            while (Reader_RefreshTextBoxEstado.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_ESTADO.Text = Reader_RefreshTextBoxEstado["Estado"].ToString();
            }

            while (Reader_RefreshTextBoxValor.Read())
            {
                FormGerirEncomendas.TEXTBOX_FormGerirEncomendas_TabPageAlteraraEncomenda_VALOR.Text = Reader_RefreshTextBoxValor["Valor"].ToString();
            }

            while (Reader_RefreshTextBoxDescritivo.Read())
            {
                FormGerirEncomendas.AUX_Descricao3 = Reader_RefreshTextBoxDescritivo["Descritivo"].ToString();
            }

            while (Reader_RefreshTextBoxNota.Read())
            {
                FormGerirEncomendas.AUX_Nota3 = Reader_RefreshTextBoxNota["Nota"].ToString();
            }

            LigacaoDB.Close();

            if (FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems != null)
                FormGerirEncomendas.BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = true;
            if (FormGerirEncomendas.LISTBOX_FormGerirEncomendas_TabPageRemoverEncomenda.SelectedItems == null)
                FormGerirEncomendas.BUTTON_FormGerirEncomendas_TabPageRemoverEncomenda_REMOVER.Enabled = false;
        }
    }
}

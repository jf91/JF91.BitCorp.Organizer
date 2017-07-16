namespace BC_Organizer
{
    partial class FORM_GERIR_ENCOMENDAS_ADICIONARNOTA
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FORM_GERIR_ENCOMENDAS_ADICIONARNOTA));
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR = new System.Windows.Forms.Button();
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA = new System.Windows.Forms.TextBox();
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA = new System.Windows.Forms.Label();
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota = new System.Windows.Forms.MenuStrip();
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.SuspendLayout();
            this.SuspendLayout();
            // 
            // BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR
            // 
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Enabled = false;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Image = global::BC_Organizer.Properties.Resources.save2;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Location = new System.Drawing.Point(171, 283);
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Name = "BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR";
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Size = new System.Drawing.Size(69, 23);
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.TabIndex = 10;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Text = "Gravar";
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.UseVisualStyleBackColor = true;
            this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR.Click += new System.EventHandler(this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR_Click);
            // 
            // TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA
            // 
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Location = new System.Drawing.Point(28, 63);
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Multiline = true;
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Name = "TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA";
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Size = new System.Drawing.Size(353, 212);
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.TabIndex = 9;
            this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.TextChanged += new System.EventHandler(this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA_TextChanged);
            // 
            // LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA
            // 
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.AutoSize = true;
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Location = new System.Drawing.Point(25, 42);
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Name = "LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA";
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Size = new System.Drawing.Size(77, 13);
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.TabIndex = 8;
            this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA.Text = "Adicionar Nota";
            // 
            // MENUSTRIP_FormGestaoDeEncomendasAdicionarNota
            // 
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fecharToolStripMenuItem});
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.Location = new System.Drawing.Point(0, 0);
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.Name = "MENUSTRIP_FormGestaoDeEncomendasAdicionarNota";
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.Size = new System.Drawing.Size(417, 24);
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.TabIndex = 7;
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.Text = "menuStrip1";
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Image = global::BC_Organizer.Properties.Resources.Delete;
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.fecharToolStripMenuItem.Text = "Fechar";
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.fecharToolStripMenuItem_Click);
            // 
            // FORM_GERIR_ENCOMENDAS_ADICIONARNOTA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 314);
            this.Controls.Add(this.BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR);
            this.Controls.Add(this.TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA);
            this.Controls.Add(this.LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA);
            this.Controls.Add(this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FORM_GERIR_ENCOMENDAS_ADICIONARNOTA";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gestão de Encomendas: Adicionar Nota";
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.ResumeLayout(false);
            this.MENUSTRIP_FormGestaoDeEncomendasAdicionarNota.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BUTTON_FormGestaoDeEncomendasAdicionarNota_GRAVAR;
        private System.Windows.Forms.TextBox TEXTBOX_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA;
        private System.Windows.Forms.Label LABEL_FormGestaoDeEncomendasAdicionarNota_ADICIONAR_NOTA;
        private System.Windows.Forms.MenuStrip MENUSTRIP_FormGestaoDeEncomendasAdicionarNota;
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
    }
}
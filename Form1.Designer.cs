namespace MagicTrick_Tirana
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnListarPartidas = new System.Windows.Forms.Button();
            this.lblVersao = new System.Windows.Forms.Label();
            this.lblPartidasTitulo = new System.Windows.Forms.Label();
            this.lstJogadores = new System.Windows.Forms.ListBox();
            this.lblJogadoresTitulo = new System.Windows.Forms.Label();
            this.lstPartidas = new System.Windows.Forms.ListBox();
            this.lblGrupoProjeto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnListarPartidas
            // 
            this.btnListarPartidas.BackColor = System.Drawing.Color.Black;
            this.btnListarPartidas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnListarPartidas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListarPartidas.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListarPartidas.ForeColor = System.Drawing.Color.White;
            this.btnListarPartidas.Location = new System.Drawing.Point(136, 289);
            this.btnListarPartidas.Name = "btnListarPartidas";
            this.btnListarPartidas.Size = new System.Drawing.Size(220, 47);
            this.btnListarPartidas.TabIndex = 0;
            this.btnListarPartidas.Text = "Listar Partidas";
            this.btnListarPartidas.UseVisualStyleBackColor = false;
            this.btnListarPartidas.Click += new System.EventHandler(this.btnListarPartidas_Click);
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.BackColor = System.Drawing.Color.Transparent;
            this.lblVersao.Font = new System.Drawing.Font("Roboto Condensed", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersao.ForeColor = System.Drawing.Color.White;
            this.lblVersao.Location = new System.Drawing.Point(12, 547);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(37, 15);
            this.lblVersao.TabIndex = 5;
            this.lblVersao.Text = "label3";
            // 
            // lblPartidasTitulo
            // 
            this.lblPartidasTitulo.AutoSize = true;
            this.lblPartidasTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblPartidasTitulo.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartidasTitulo.ForeColor = System.Drawing.Color.White;
            this.lblPartidasTitulo.Location = new System.Drawing.Point(841, 21);
            this.lblPartidasTitulo.Name = "lblPartidasTitulo";
            this.lblPartidasTitulo.Size = new System.Drawing.Size(77, 24);
            this.lblPartidasTitulo.TabIndex = 3;
            this.lblPartidasTitulo.Text = "Partidas";
            this.lblPartidasTitulo.Visible = false;
            // 
            // lstJogadores
            // 
            this.lstJogadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(39)))));
            this.lstJogadores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstJogadores.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstJogadores.FormattingEnabled = true;
            this.lstJogadores.ItemHeight = 24;
            this.lstJogadores.Location = new System.Drawing.Point(709, 384);
            this.lstJogadores.Name = "lstJogadores";
            this.lstJogadores.Size = new System.Drawing.Size(331, 192);
            this.lstJogadores.TabIndex = 2;
            this.lstJogadores.Visible = false;
            // 
            // lblJogadoresTitulo
            // 
            this.lblJogadoresTitulo.AutoSize = true;
            this.lblJogadoresTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblJogadoresTitulo.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJogadoresTitulo.ForeColor = System.Drawing.Color.White;
            this.lblJogadoresTitulo.Location = new System.Drawing.Point(786, 357);
            this.lblJogadoresTitulo.Name = "lblJogadoresTitulo";
            this.lblJogadoresTitulo.Size = new System.Drawing.Size(182, 24);
            this.lblJogadoresTitulo.TabIndex = 4;
            this.lblJogadoresTitulo.Text = "Jogadores da Partida";
            this.lblJogadoresTitulo.Visible = false;
            // 
            // lstPartidas
            // 
            this.lstPartidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(39)))));
            this.lstPartidas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPartidas.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPartidas.ForeColor = System.Drawing.Color.Black;
            this.lstPartidas.FormattingEnabled = true;
            this.lstPartidas.ItemHeight = 24;
            this.lstPartidas.Location = new System.Drawing.Point(709, 48);
            this.lstPartidas.Name = "lstPartidas";
            this.lstPartidas.Size = new System.Drawing.Size(331, 288);
            this.lstPartidas.TabIndex = 1;
            this.lstPartidas.Visible = false;
            this.lstPartidas.SelectedIndexChanged += new System.EventHandler(this.lstPartidas_SelectedIndexChanged);
            // 
            // lblGrupoProjeto
            // 
            this.lblGrupoProjeto.AutoSize = true;
            this.lblGrupoProjeto.BackColor = System.Drawing.Color.Transparent;
            this.lblGrupoProjeto.Font = new System.Drawing.Font("Roboto Condensed", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrupoProjeto.ForeColor = System.Drawing.Color.White;
            this.lblGrupoProjeto.Location = new System.Drawing.Point(12, 562);
            this.lblGrupoProjeto.Name = "lblGrupoProjeto";
            this.lblGrupoProjeto.Size = new System.Drawing.Size(37, 15);
            this.lblGrupoProjeto.TabIndex = 6;
            this.lblGrupoProjeto.Text = "label3";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::MagicTrick_Tirana.Properties.Resources.Magic;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1052, 585);
            this.Controls.Add(this.lblGrupoProjeto);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.lblJogadoresTitulo);
            this.Controls.Add(this.lblPartidasTitulo);
            this.Controls.Add(this.lstJogadores);
            this.Controls.Add(this.lstPartidas);
            this.Controls.Add(this.btnListarPartidas);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1070, 632);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1070, 632);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnListarPartidas;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.Label lblPartidasTitulo;
        private System.Windows.Forms.ListBox lstJogadores;
        private System.Windows.Forms.Label lblJogadoresTitulo;
        private System.Windows.Forms.ListBox lstPartidas;
        private System.Windows.Forms.Label lblGrupoProjeto;
    }
}


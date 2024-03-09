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
            this.btnCriarPartida = new System.Windows.Forms.Button();
            this.btnEntrarPartida = new System.Windows.Forms.Button();
            this.btnCriar = new System.Windows.Forms.Button();
            this.lblNomePartida = new System.Windows.Forms.Label();
            this.txtNomePartida = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.pnlCriarPartida = new System.Windows.Forms.Panel();
            this.pnlEntrarNaPartida = new System.Windows.Forms.Panel();
            this.txtSenhaDaPartida = new System.Windows.Forms.TextBox();
            this.lblSenhaDaPartida = new System.Windows.Forms.Label();
            this.txtNomeJogador = new System.Windows.Forms.TextBox();
            this.lblNomeJogador = new System.Windows.Forms.Label();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.pnlListar = new System.Windows.Forms.Panel();
            this.pnlCriarPartida.SuspendLayout();
            this.pnlEntrarNaPartida.SuspendLayout();
            this.pnlListar.SuspendLayout();
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
            this.lblPartidasTitulo.Location = new System.Drawing.Point(145, 46);
            this.lblPartidasTitulo.Name = "lblPartidasTitulo";
            this.lblPartidasTitulo.Size = new System.Drawing.Size(77, 24);
            this.lblPartidasTitulo.TabIndex = 3;
            this.lblPartidasTitulo.Text = "Partidas";
            // 
            // lstJogadores
            // 
            this.lstJogadores.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(39)))));
            this.lstJogadores.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstJogadores.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstJogadores.FormattingEnabled = true;
            this.lstJogadores.ItemHeight = 24;
            this.lstJogadores.Location = new System.Drawing.Point(11, 315);
            this.lstJogadores.Name = "lstJogadores";
            this.lstJogadores.Size = new System.Drawing.Size(363, 192);
            this.lstJogadores.TabIndex = 2;
            // 
            // lblJogadoresTitulo
            // 
            this.lblJogadoresTitulo.AutoSize = true;
            this.lblJogadoresTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lblJogadoresTitulo.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblJogadoresTitulo.ForeColor = System.Drawing.Color.White;
            this.lblJogadoresTitulo.Location = new System.Drawing.Point(88, 288);
            this.lblJogadoresTitulo.Name = "lblJogadoresTitulo";
            this.lblJogadoresTitulo.Size = new System.Drawing.Size(182, 24);
            this.lblJogadoresTitulo.TabIndex = 4;
            this.lblJogadoresTitulo.Text = "Jogadores da Partida";
            // 
            // lstPartidas
            // 
            this.lstPartidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(39)))));
            this.lstPartidas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstPartidas.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstPartidas.ForeColor = System.Drawing.Color.Black;
            this.lstPartidas.FormattingEnabled = true;
            this.lstPartidas.ItemHeight = 24;
            this.lstPartidas.Location = new System.Drawing.Point(11, 73);
            this.lstPartidas.Name = "lstPartidas";
            this.lstPartidas.Size = new System.Drawing.Size(363, 192);
            this.lstPartidas.TabIndex = 1;
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
            // btnCriarPartida
            // 
            this.btnCriarPartida.BackColor = System.Drawing.Color.Black;
            this.btnCriarPartida.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCriarPartida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCriarPartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriarPartida.ForeColor = System.Drawing.Color.White;
            this.btnCriarPartida.Location = new System.Drawing.Point(136, 357);
            this.btnCriarPartida.Name = "btnCriarPartida";
            this.btnCriarPartida.Size = new System.Drawing.Size(220, 47);
            this.btnCriarPartida.TabIndex = 7;
            this.btnCriarPartida.Text = "Criar Partida";
            this.btnCriarPartida.UseVisualStyleBackColor = false;
            this.btnCriarPartida.Click += new System.EventHandler(this.btnCriarPartida_Click);
            // 
            // btnEntrarPartida
            // 
            this.btnEntrarPartida.BackColor = System.Drawing.Color.Black;
            this.btnEntrarPartida.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEntrarPartida.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrarPartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrarPartida.ForeColor = System.Drawing.Color.White;
            this.btnEntrarPartida.Location = new System.Drawing.Point(136, 423);
            this.btnEntrarPartida.Name = "btnEntrarPartida";
            this.btnEntrarPartida.Size = new System.Drawing.Size(220, 47);
            this.btnEntrarPartida.TabIndex = 8;
            this.btnEntrarPartida.Text = "Entrar na Partida";
            this.btnEntrarPartida.UseVisualStyleBackColor = false;
            this.btnEntrarPartida.Click += new System.EventHandler(this.btnEntrarPartida_Click);
            // 
            // btnCriar
            // 
            this.btnCriar.BackColor = System.Drawing.Color.Black;
            this.btnCriar.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCriar.ForeColor = System.Drawing.Color.White;
            this.btnCriar.Location = new System.Drawing.Point(147, 116);
            this.btnCriar.Name = "btnCriar";
            this.btnCriar.Size = new System.Drawing.Size(107, 32);
            this.btnCriar.TabIndex = 9;
            this.btnCriar.Text = "Criar";
            this.btnCriar.UseVisualStyleBackColor = false;
            this.btnCriar.Click += new System.EventHandler(this.btnCriar_Click);
            // 
            // lblNomePartida
            // 
            this.lblNomePartida.AutoSize = true;
            this.lblNomePartida.BackColor = System.Drawing.Color.Transparent;
            this.lblNomePartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomePartida.ForeColor = System.Drawing.Color.White;
            this.lblNomePartida.Location = new System.Drawing.Point(12, 13);
            this.lblNomePartida.Name = "lblNomePartida";
            this.lblNomePartida.Size = new System.Drawing.Size(144, 24);
            this.lblNomePartida.TabIndex = 10;
            this.lblNomePartida.Text = "Nome da Partida";
            // 
            // txtNomePartida
            // 
            this.txtNomePartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomePartida.Location = new System.Drawing.Point(11, 40);
            this.txtNomePartida.Name = "txtNomePartida";
            this.txtNomePartida.Size = new System.Drawing.Size(243, 32);
            this.txtNomePartida.TabIndex = 11;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.BackColor = System.Drawing.Color.Transparent;
            this.lblSenha.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenha.ForeColor = System.Drawing.Color.White;
            this.lblSenha.Location = new System.Drawing.Point(12, 89);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(59, 24);
            this.lblSenha.TabIndex = 12;
            this.lblSenha.Text = "Senha";
            // 
            // txtSenha
            // 
            this.txtSenha.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenha.Location = new System.Drawing.Point(11, 116);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.Size = new System.Drawing.Size(123, 32);
            this.txtSenha.TabIndex = 13;
            // 
            // pnlCriarPartida
            // 
            this.pnlCriarPartida.BackColor = System.Drawing.Color.Black;
            this.pnlCriarPartida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCriarPartida.Controls.Add(this.txtSenha);
            this.pnlCriarPartida.Controls.Add(this.lblSenha);
            this.pnlCriarPartida.Controls.Add(this.txtNomePartida);
            this.pnlCriarPartida.Controls.Add(this.lblNomePartida);
            this.pnlCriarPartida.Controls.Add(this.btnCriar);
            this.pnlCriarPartida.Location = new System.Drawing.Point(391, 74);
            this.pnlCriarPartida.Name = "pnlCriarPartida";
            this.pnlCriarPartida.Size = new System.Drawing.Size(269, 181);
            this.pnlCriarPartida.TabIndex = 14;
            this.pnlCriarPartida.Visible = false;
            // 
            // pnlEntrarNaPartida
            // 
            this.pnlEntrarNaPartida.BackColor = System.Drawing.Color.Black;
            this.pnlEntrarNaPartida.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlEntrarNaPartida.Controls.Add(this.txtSenhaDaPartida);
            this.pnlEntrarNaPartida.Controls.Add(this.lblSenhaDaPartida);
            this.pnlEntrarNaPartida.Controls.Add(this.txtNomeJogador);
            this.pnlEntrarNaPartida.Controls.Add(this.lblNomeJogador);
            this.pnlEntrarNaPartida.Controls.Add(this.btnEntrar);
            this.pnlEntrarNaPartida.Location = new System.Drawing.Point(391, 298);
            this.pnlEntrarNaPartida.Name = "pnlEntrarNaPartida";
            this.pnlEntrarNaPartida.Size = new System.Drawing.Size(269, 181);
            this.pnlEntrarNaPartida.TabIndex = 15;
            this.pnlEntrarNaPartida.Visible = false;
            // 
            // txtSenhaDaPartida
            // 
            this.txtSenhaDaPartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSenhaDaPartida.Location = new System.Drawing.Point(11, 116);
            this.txtSenhaDaPartida.Name = "txtSenhaDaPartida";
            this.txtSenhaDaPartida.Size = new System.Drawing.Size(123, 32);
            this.txtSenhaDaPartida.TabIndex = 13;
            // 
            // lblSenhaDaPartida
            // 
            this.lblSenhaDaPartida.AutoSize = true;
            this.lblSenhaDaPartida.BackColor = System.Drawing.Color.Transparent;
            this.lblSenhaDaPartida.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenhaDaPartida.ForeColor = System.Drawing.Color.White;
            this.lblSenhaDaPartida.Location = new System.Drawing.Point(12, 89);
            this.lblSenhaDaPartida.Name = "lblSenhaDaPartida";
            this.lblSenhaDaPartida.Size = new System.Drawing.Size(147, 24);
            this.lblSenhaDaPartida.TabIndex = 12;
            this.lblSenhaDaPartida.Text = "Senha da Partida";
            // 
            // txtNomeJogador
            // 
            this.txtNomeJogador.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNomeJogador.Location = new System.Drawing.Point(11, 40);
            this.txtNomeJogador.Name = "txtNomeJogador";
            this.txtNomeJogador.Size = new System.Drawing.Size(243, 32);
            this.txtNomeJogador.TabIndex = 11;
            // 
            // lblNomeJogador
            // 
            this.lblNomeJogador.AutoSize = true;
            this.lblNomeJogador.BackColor = System.Drawing.Color.Transparent;
            this.lblNomeJogador.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeJogador.ForeColor = System.Drawing.Color.White;
            this.lblNomeJogador.Location = new System.Drawing.Point(12, 13);
            this.lblNomeJogador.Name = "lblNomeJogador";
            this.lblNomeJogador.Size = new System.Drawing.Size(152, 24);
            this.lblNomeJogador.TabIndex = 10;
            this.lblNomeJogador.Text = "Nome do Jogador";
            // 
            // btnEntrar
            // 
            this.btnEntrar.BackColor = System.Drawing.Color.Black;
            this.btnEntrar.Font = new System.Drawing.Font("Roboto Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.Location = new System.Drawing.Point(147, 116);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(107, 32);
            this.btnEntrar.TabIndex = 9;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // pnlListar
            // 
            this.pnlListar.BackColor = System.Drawing.Color.Transparent;
            this.pnlListar.Controls.Add(this.lblJogadoresTitulo);
            this.pnlListar.Controls.Add(this.lblPartidasTitulo);
            this.pnlListar.Controls.Add(this.lstJogadores);
            this.pnlListar.Controls.Add(this.lstPartidas);
            this.pnlListar.Location = new System.Drawing.Point(666, 1);
            this.pnlListar.Name = "pnlListar";
            this.pnlListar.Size = new System.Drawing.Size(386, 576);
            this.pnlListar.TabIndex = 15;
            this.pnlListar.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImage = global::MagicTrick_Tirana.Properties.Resources.Magic;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1052, 585);
            this.Controls.Add(this.pnlCriarPartida);
            this.Controls.Add(this.pnlListar);
            this.Controls.Add(this.pnlEntrarNaPartida);
            this.Controls.Add(this.btnEntrarPartida);
            this.Controls.Add(this.btnCriarPartida);
            this.Controls.Add(this.lblGrupoProjeto);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.btnListarPartidas);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1070, 632);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1070, 632);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.pnlCriarPartida.ResumeLayout(false);
            this.pnlCriarPartida.PerformLayout();
            this.pnlEntrarNaPartida.ResumeLayout(false);
            this.pnlEntrarNaPartida.PerformLayout();
            this.pnlListar.ResumeLayout(false);
            this.pnlListar.PerformLayout();
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
        private System.Windows.Forms.Button btnCriarPartida;
        private System.Windows.Forms.Button btnEntrarPartida;
        private System.Windows.Forms.Button btnCriar;
        private System.Windows.Forms.Label lblNomePartida;
        private System.Windows.Forms.TextBox txtNomePartida;
        private System.Windows.Forms.Label lblSenha;
        private System.Windows.Forms.TextBox txtSenha;
        private System.Windows.Forms.Panel pnlCriarPartida;
        private System.Windows.Forms.Panel pnlListar;
        private System.Windows.Forms.Panel pnlEntrarNaPartida;
        private System.Windows.Forms.TextBox txtSenhaDaPartida;
        private System.Windows.Forms.Label lblSenhaDaPartida;
        private System.Windows.Forms.TextBox txtNomeJogador;
        private System.Windows.Forms.Label lblNomeJogador;
        private System.Windows.Forms.Button btnEntrar;
    }
}


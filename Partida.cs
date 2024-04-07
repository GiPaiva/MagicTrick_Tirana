using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        //Props
        public string Versao { get; set; }
        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }
        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }

        //Instancias
        Tratamento r = new Tratamento();
        Lobby lobby = new Lobby();
        Cartas c;

        //Variavel de estado do Jogo
        public bool estado = false;

        

        //Inicializador
        public Partida()
        {
            InitializeComponent();
            c = new Cartas(this);
        }

        public void AtualizarTela()
        {
            lblVersao2.Text = Versao;
            _ = ReloAsync();
            //_ = MandarCarta();
        }

        //Verificação de pessoas na Partida
        public async Task ReloAsync()
        {
            int quantidade = 0;
            while (!estado && quantidade < 5)
            {
                lblQJ.Visible = true;
                lblQJogadores.Visible = true;

                lblQJ.Text = Convert.ToString(quantidade);

                lobby.LobbyListarJogadores(PartidaSelecionada);
                if(quantidade != lobby.Jogadores.Length)
                {
                    JogadoresAtuais = lobby.Jogadores;
                    lblParticipantes.Text = "";

                    foreach (string JogadoresAtuais in JogadoresAtuais)
                    {
                        lblParticipantes.Text += JogadoresAtuais + "\n\r";
                    }
                }
                quantidade = JogadoresAtuais.Length;

                await Task.Delay(6000);
            }

            lblQJ.Visible = false;
            lblQJogadores.Visible = false;
            lblParticipantes.Visible = false;
        }
        
        //public async Task MandarCarta() { }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);

            for(int i = 0;  i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
            estado = true;
            btnComecar.Visible = false;
        }

        private void btnConsultarMao_Click(object sender, EventArgs e)
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = true;
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
        }

        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
        {
            bool primeiro = true;

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux = JogadoresAtuais[i].Split(',');

                if (aux[0] != idDoJogador)
                {
                    if (i == 0)
                    {
                        i++;
                        primeiro = false;
                    }

                    c.MostrarCartas(aux, DadosConsultarMao, i, "player");

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    c.MostrarCartas(aux, DadosConsultarMao, 0, idDoJogador);
                }
            }
        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            //IdJogador | senhaJogador | posição
            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(Dadoslist[0]);

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            string retorno = Jogo.Jogar(IdJogador, DadosJogador[1], posicao);
            if (!r.Error(retorno))
            {
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);


                DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                if (decisao == DialogResult.Yes)
                {
                    Apostar();
                }
                else
                {
                    Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                    MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                MessageBox.Show("Tente Novamente", "", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);
            }
        }

        private void Apostar()
        {
            btnApostar.Visible = true;
            if (lsbPlayer1.SelectedItem == null)
            {
                MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            string[] DadosJogador = Jogador.Split(',');
            if (lsbPlayer1.SelectedItem != null)
            {
                string list = lsbPlayer1.Text;
                string[] Dadoslist = list.Split('|');

                //Posição
                int posicao = Convert.ToInt32(Dadoslist[0]);
                int IdJogador = Convert.ToInt32(DadosJogador[0]);


                string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

            }
            else
            {
                Apostar();
            }
        }

        private void pnlCarta1P1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlCarta2P1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pnlCarta3P1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lsbPlayer1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsbPlayer2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsbPlayer4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lsbPlayer3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

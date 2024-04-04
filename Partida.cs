using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        public string Versao { get; set; }

        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }

        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }

        private Tratamento r = new Tratamento();
        private Lobby lobby = new Lobby();

        public bool estado = false;
        public Partida()
        {
            InitializeComponent();
        }

        public void AtualizarTela()
        {
            lblVersao2.Text = Versao;
            ReloAsync();
        }

        public async Task ReloAsync()
        {
            int quantidade = 0;
            while (!estado && quantidade < 5)
            {
                quantidade = JogadoresAtuais.Length;
                lblQJ.Visible = true;
                lblQJogadores.Visible = true;

                lblQJ.Text = Convert.ToString(quantidade);

                lobby.LobbyListarJogadores(PartidaSelecionada);
                JogadoresAtuais = lobby.Jogadores;

                await Task.Delay(6000);
            }

            lblQJ.Visible = false;
            lblQJogadores.Visible = false;
        }

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

            MostrarGalera(DadosJogador[0], DadosConsultarMao);
        }

        private void MostrarGalera(string idDoJogador, string[] DadosConsultarMao)
        {
            bool primeiro = true;

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux = JogadoresAtuais[i].Split(',');
                
                if (aux[0] != idDoJogador)
                {
                    if(i == 0)
                    {
                        i++;
                        primeiro = false;
                    }

                    MostrarCartas(aux, DadosConsultarMao, i);

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    MostrarCartas(aux, DadosConsultarMao, 0);
                }
            }
        }
        
        private void MostrarCartas(string[] aux, string[] DadosConsultarMao, int i)
        {
            List<GroupBox> groupBoxes = new List<GroupBox> { grbPlayer1, grbPlayer2, grbPlayer3, grbPlayer4 };
            List<ListBox> listBoxes = new List<ListBox> { lsbPlayer1, lsbPlayer2, lsbPlayer3, lsbPlayer4 };

            //List<Panel> pictureBoxes = new List<Panel> {pnlCarta1P1, pnlCarta2P1, pnlCarta3P1, pnlCarta4P1, pnlCarta5P1, pnlCarta6P1, pnlCarta7P1, pnlCarta8P1, pnlCarta9P1, pnlCarta10P1, pnlCarta11P1, pnlCarta12P1, pnlCarta13P1, pnlCarta14P1 };
            //List<List<Panel>> nome = new List<List<Panel>>();

            //List<string> imagens = new List<string> {
            //    "./Cartas/Copas1.png",        //C
            //    "./Cartas/Espadas1.png",      //E
            //    "./Cartas/Estrelas1.png",     //S
            //    "./Cartas/Lua1.png",          //L
            //    "./Cartas/Ouros1.png",        //O
            //    "./Cartas/Paus1.png",         //P
            //    "./Cartas/Triângulos1.png",   //T
            //};

            groupBoxes[i].Visible = true;
            listBoxes[i].Items.Clear();

            groupBoxes[i].Text = aux[1];
            listBoxes[i].Items.Add("Posição | Naipe");

            for (int j = 0; j < DadosConsultarMao.Length; j++)
            {
                string[] aux2 = DadosConsultarMao[j].Split(',');
                if (aux2[0] == aux[0])
                {
                    listBoxes[i].Items.Add(aux2[1] + " | " + aux2[2]);
                    //Paneis dinamicos (https://youtu.be/4KGBtGdH7tw?si=ecSoSNZiPwuC7KYO)
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

    }
}

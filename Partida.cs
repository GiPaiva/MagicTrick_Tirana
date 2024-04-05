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
        public string Versao { get; set; }

        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }

        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }

        private Tratamento r = new Tratamento();
        private Lobby lobby = new Lobby();

        public bool estado = false;
        string pasta_imagens = "";
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
            estado = true;
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

                    MostrarCartas(aux, DadosConsultarMao, i, "player");

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    MostrarCartas(aux, DadosConsultarMao, 0, idDoJogador);
                }
            }
        }
        
        private void MostrarCartas(string[] aux, string[] DadosConsultarMao, int i, string idJogador)
        {
            List<GroupBox> groupBoxes = new List<GroupBox> { grbPlayer1, grbPlayer2, grbPlayer3, grbPlayer4 };
            List<ListBox> listBoxes = new List<ListBox> { lsbPlayer1, lsbPlayer2, lsbPlayer3, lsbPlayer4 };

            groupBoxes[i].Visible = true;
            listBoxes[i].Items.Clear();

            groupBoxes[i].Text = aux[1];
            listBoxes[i].Items.Add("Posição | Naipe");

            Dictionary<string, string[]> cartinhas = new Dictionary<string, string[]>();

            pnlCartas.Visible = true;
            panel2.Visible = true;
            for (int j = 0; j < DadosConsultarMao.Length; j++)
            {
                string[] aux2 = DadosConsultarMao[j].Split(',');
                if (aux2[0] == aux[0])
                {
                    if (aux2[0] == idJogador)
                    {
                        cartinhas.Add(aux2[1], MostrarCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i));
                        listBoxes[i].Items.Add(aux2[1] + " | " + cartinhas[aux2[1]][0]);
                    }
                    else
                    {
                        listBoxes[i].Items.Add(aux2[1] + " | " + aux2[2]);
                        MostrarCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i);
                    }
                }
            }
        }

        public string[] MostrarCartasJogador(string naipe, int posicao, int i)
        {
            List<Panel> CartasP1 = new List<Panel> { pnlCarta1P1, pnlCarta2P1, pnlCarta3P1, pnlCarta4P1, pnlCarta5P1, pnlCarta6P1, pnlCarta7P1, pnlCarta8P1, pnlCarta9P1, pnlCarta10P1, pnlCarta11P1, pnlCarta12P1, pnlCarta13P1, pnlCarta14P1 };
            List<Panel> CartasP2 = new List<Panel> { pnlCarta1P2, pnlCarta2P2, pnlCarta3P2, pnlCarta4P2, pnlCarta5P2, pnlCarta6P2, pnlCarta7P2, pnlCarta8P2, pnlCarta9P2, pnlCarta10P2, pnlCarta11P2, pnlCarta12P2, pnlCarta13P2, pnlCarta14P2 };
            List<Panel> CartasP3 = new List<Panel> { pnlCarta1P3, pnlCarta2P3, pnlCarta3P3, pnlCarta4P3, pnlCarta5P3, pnlCarta6P3, pnlCarta7P3, pnlCarta8P3, pnlCarta9P3, pnlCarta10P3, pnlCarta11P3, pnlCarta12P3, pnlCarta13P3, pnlCarta14P3 };
            List<Panel> CartasP4 = new List<Panel> { pnlCarta1P4, pnlCarta2P4, pnlCarta3P4, pnlCarta4P4, pnlCarta5P4, pnlCarta6P4, pnlCarta7P4, pnlCarta8P4, pnlCarta9P4, pnlCarta10P4, pnlCarta11P4, pnlCarta12P4, pnlCarta13P4, pnlCarta14P4 };

            List<List<Panel>> nome = new List<List<Panel>>();
            nome.Add(CartasP1);
            nome.Add(CartasP2);
            nome.Add(CartasP3);
            nome.Add(CartasP4);

            List<string> imagens = new List<string> {
                "Copas1.png",        //C
                "Espadas1.png",      //E
                "Estrelas1.png",     //S
                "Lua1.png",          //L
                "Ouros1.png",        //O
                "Paus1.png",         //P
                "Triângulos1.png",   //T
            };

            string[] aux = new string[2];
            int imagemPosicao = 0;
            pasta_imagens = Path.Combine(Application.StartupPath, "Cartas/");
            switch (naipe)
            {
                case "C":
                    imagemPosicao = 0;
                    break;

                case "E":
                    imagemPosicao = 1;
                    break;

                case "S":
                    imagemPosicao = 2;
                    break;

                case "L":
                    imagemPosicao = 3;
                    break;

                case "O":
                    imagemPosicao = 4;
                    break;

                case "P":
                    imagemPosicao = 5;
                    break;

                case "T":
                    imagemPosicao = 6;
                    break;

                default: 
                    imagemPosicao = 42; 
                    break;

            }
            if(imagemPosicao != 42)
            {
                nome[i][posicao-1].BackgroundImage = Image.FromFile(pasta_imagens + imagens[imagemPosicao]);
                nome[i][posicao-1].BackgroundImageLayout = ImageLayout.Stretch;
                nome[i][posicao - 1].Visible = true;
            }

            aux[0] = naipe;
            aux[1] = imagens[imagemPosicao];
            return aux;
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

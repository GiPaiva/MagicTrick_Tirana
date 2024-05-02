using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lobby;

namespace MagicTrick_Tirana
{
    class Cartas
    {
        string pasta_imagens = "";
        public Dictionary<string, string[]> cartinhasDoJogadorAtual = new Dictionary<string, string[]>();
        public Dictionary<string, int> localNaMesaCadaJogador = new Dictionary<string, int>();
        public Dictionary<string, string> NaipesDasCrtasEImagens = new Dictionary<string, string>();
        public Dictionary<Panel, Label> cartasJogadas = new Dictionary<Panel, Label>();

        public List<List<Panel>> panelsDasCartasDeCadaJogador = new List<List<Panel>>();

        Partida p;
        Lobby lobby = new Lobby();

        public bool PrimeiraEntrada = true;
        public int QuantidadeDeCartasTotal = 0;

        public Cartas(Partida partida)
        {
            this.p = partida;
        }

        public void ExibirCartas()
        {
            _ = p.AtualizarTela();
        }

        public void MostrarCartas(string[] aux, string[] DadosConsultarMao, int i, string idJogador)
        {
            List<GroupBox> groupBoxes = new List<GroupBox> { p.grbPlayer1, p.grbPlayer2, p.grbPlayer3, p.grbPlayer4 };
            List<ListBox> listBoxes = new List<ListBox> { p.lsbPlayer1, p.lsbPlayer2, p.lsbPlayer3, p.lsbPlayer4 };

            groupBoxes[i].Visible = true;
            listBoxes[i].Items.Clear();

            groupBoxes[i].Text = aux[1];
            listBoxes[i].Items.Add("Posição | Naipe");
            this.cartinhasDoJogadorAtual.Clear();
            int k = 0;
            for (int j = 0; j < DadosConsultarMao.Length; j++)
            {
                string[] aux2 = DadosConsultarMao[j].Split(',');
                if (aux2[0] == aux[0])
                {
                    if (aux2[0] == idJogador)
                    {
                        this.cartinhasDoJogadorAtual.Add(aux2[1], ImagemCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i));
                        listBoxes[i].Items.Add(aux2[1] + " | " + cartinhasDoJogadorAtual[aux2[1]][0]);

                    }
                    else
                    {
                        listBoxes[i].Items.Add(aux2[1] + " | " + aux2[2]);
                        ImagemCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i);
                    }

                    if(k == 0)
                    {
                        localNaMesaCadaJogador.Add(aux2[0].Trim(),i);
                        k++;
                    }

                    if (PrimeiraEntrada)
                    {
                        QuantidadeDeCartasTotal = DadosConsultarMao.Length / p.JogadoresAtuais.Length;
                        PrimeiraEntrada = false;
                    }
                }
            }
        }

        public string[] ImagemCartasJogador(string naipe, int posicao, int i)
        {
            List<Panel> CartasP1 = new List<Panel> { p.pnlCarta1P1, p.pnlCarta2P1, p.pnlCarta3P1, p.pnlCarta4P1, p.pnlCarta5P1, p.pnlCarta6P1, p.pnlCarta7P1, p.pnlCarta8P1, p.pnlCarta9P1, p.pnlCarta10P1, p.pnlCarta11P1, p.pnlCarta12P1, p.pnlCarta13P1, p.pnlCarta14P1 };
            List<Panel> CartasP2 = new List<Panel> { p.pnlCarta1P2, p.pnlCarta2P2, p.pnlCarta3P2, p.pnlCarta4P2, p.pnlCarta5P2, p.pnlCarta6P2, p.pnlCarta7P2, p.pnlCarta8P2, p.pnlCarta9P2, p.pnlCarta10P2, p.pnlCarta11P2, p.pnlCarta12P2, p.pnlCarta13P2, p.pnlCarta14P2 };
            List<Panel> CartasP3 = new List<Panel> { p.pnlCarta1P3, p.pnlCarta2P3, p.pnlCarta3P3, p.pnlCarta4P3, p.pnlCarta5P3, p.pnlCarta6P3, p.pnlCarta7P3, p.pnlCarta8P3, p.pnlCarta9P3, p.pnlCarta10P3, p.pnlCarta11P3, p.pnlCarta12P3, p.pnlCarta13P3, p.pnlCarta14P3 };
            List<Panel> CartasP4 = new List<Panel> { p.pnlCarta1P4, p.pnlCarta2P4, p.pnlCarta3P4, p.pnlCarta4P4, p.pnlCarta5P4, p.pnlCarta6P4, p.pnlCarta7P4, p.pnlCarta8P4, p.pnlCarta9P4, p.pnlCarta10P4, p.pnlCarta11P4, p.pnlCarta12P4, p.pnlCarta13P4, p.pnlCarta14P4 };

            panelsDasCartasDeCadaJogador.Add(CartasP1);
            panelsDasCartasDeCadaJogador.Add(CartasP2);
            panelsDasCartasDeCadaJogador.Add(CartasP3);
            panelsDasCartasDeCadaJogador.Add(CartasP4);

            string[] aux = new string[2];
            int imagemPosicao = 0;
            pasta_imagens = Path.Combine(Application.StartupPath, "Cartas/");

            if (!NaipesDasCrtasEImagens.ContainsKey("C"))
            {
                NaipesDasCrtasEImagens.Add("C", "Copas1.png");
                NaipesDasCrtasEImagens.Add("E", "Espadas1.png");
                NaipesDasCrtasEImagens.Add("S", "Estrela1.png");
                NaipesDasCrtasEImagens.Add("L", "Lua1.png");
                NaipesDasCrtasEImagens.Add("O", "Ouros1.png");
                NaipesDasCrtasEImagens.Add("P", "Paus1.png");
                NaipesDasCrtasEImagens.Add("T", "Triângulo1.png");
            }

            if (imagemPosicao != 42)
            {
                panelsDasCartasDeCadaJogador[i][posicao - 1].BackgroundImage = Image.FromFile(pasta_imagens + NaipesDasCrtasEImagens[naipe]);
                panelsDasCartasDeCadaJogador[i][posicao - 1].BackgroundImageLayout = ImageLayout.Stretch;
                panelsDasCartasDeCadaJogador[i][posicao - 1].Visible = true;
            }

            aux[0] = naipe;
            aux[1] = NaipesDasCrtasEImagens[naipe];
            return aux;
        }
        
        public void VerificarJogadaDosPlayers(string idJogador, string naipe, string valorDaCarta, string posicao)
        {
            List<Panel> panelCartasMeio = new List<Panel> { p.pnlCartaP1, p.pnlCartaP2, p.pnlCartaP3, p.pnlCartaP4};
            List<Label> labelCartasMeio = new List<Label> { p.lblCartaP1, p.lblCartaP2, p.lblCartaP3, p.lblCartaP4 };

            int posicaoDoJogador = localNaMesaCadaJogador[idJogador];

            panelCartasMeio[posicaoDoJogador].Visible = true;
            labelCartasMeio[posicaoDoJogador].Visible = true;

            panelCartasMeio[posicaoDoJogador].BackgroundImage = Image.FromFile(pasta_imagens + NaipesDasCrtasEImagens[naipe]);
            panelCartasMeio[posicaoDoJogador].BackgroundImageLayout = ImageLayout.Stretch;
            labelCartasMeio[posicaoDoJogador].Text = valorDaCarta;

            if(!cartasJogadas.ContainsKey(panelCartasMeio[posicaoDoJogador]))
                cartasJogadas.Add(panelCartasMeio[posicaoDoJogador], labelCartasMeio[posicaoDoJogador]);

            Label valor = new Label();
            valor.Text = valorDaCarta;
            valor.Location = new Point(9,18);
            valor.BackColor = Color.White;
            valor.ForeColor = Color.Black;

            int posicaoMao = Convert.ToInt32(posicao) - 1;
            panelsDasCartasDeCadaJogador[posicaoDoJogador][posicaoMao].Controls.Add(valor);

        }
    }
}

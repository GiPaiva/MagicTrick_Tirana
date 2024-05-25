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
    class Cartas : Partida
    {
        string pasta_imagens = "";

        public bool PrimeiraEntrada = true;
        public int QuantidadeDeCartasTotal = 0;

        //public void ExibirCartas()
        //{
        //    _ = AtualizarTela();
        //}

        public void MostrarCartas(string[] aux, string[] DadosConsultarMao, int i, string idJogador, string[] JogadoresAtuais)
        {
            List<GroupBox> groupBoxes = new List<GroupBox> { grbPlayer1, grbPlayer2, grbPlayer3, grbPlayer4 };
            List<ListBox> listBoxes = new List<ListBox> { lsbPlayer1, lsbPlayer2, lsbPlayer3, lsbPlayer4 };

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
                        posicoesCartasMao.Add(aux2[1]);
                    }
                    else
                    {
                        listBoxes[i].Items.Add(aux2[1] + " | " + aux2[2]);
                        ImagemCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i);
                    }

                    if (k == 0)
                    {
                        if (!localNaMesaCadaJogador.ContainsKey(aux2[0].Trim()))
                        {
                            localNaMesaCadaJogador.Add(aux2[0].Trim(), i);
                        }
                        k++;
                    }

                    if (PrimeiraEntrada)
                    {
                        QuantidadeDeCartasTotal = DadosConsultarMao.Length / JogadoresAtuais.Length;
                        PrimeiraEntrada = false;
                    }
                }
            }
        }

        public string[] ImagemCartasJogador(string naipe, int posicao, int i)
        {
            List<Panel> CartasP1 = new List<Panel> { pnlCarta1P1, pnlCarta2P1, pnlCarta3P1, pnlCarta4P1, pnlCarta5P1, pnlCarta6P1, pnlCarta7P1, pnlCarta8P1, pnlCarta9P1, pnlCarta10P1, pnlCarta11P1, pnlCarta12P1, pnlCarta13P1, pnlCarta14P1 };
            List<Panel> CartasP2 = new List<Panel> { pnlCarta1P2, pnlCarta2P2, pnlCarta3P2, pnlCarta4P2, pnlCarta5P2, pnlCarta6P2, pnlCarta7P2, pnlCarta8P2, pnlCarta9P2, pnlCarta10P2, pnlCarta11P2, pnlCarta12P2, pnlCarta13P2, pnlCarta14P2 };
            List<Panel> CartasP3 = new List<Panel> { pnlCarta1P3, pnlCarta2P3, pnlCarta3P3, pnlCarta4P3, pnlCarta5P3, pnlCarta6P3, pnlCarta7P3, pnlCarta8P3, pnlCarta9P3, pnlCarta10P3, pnlCarta11P3, pnlCarta12P3, pnlCarta13P3, pnlCarta14P3 };
            List<Panel> CartasP4 = new List<Panel> { pnlCarta1P4, pnlCarta2P4, pnlCarta3P4, pnlCarta4P4, pnlCarta5P4, pnlCarta6P4, pnlCarta7P4, pnlCarta8P4, pnlCarta9P4, pnlCarta10P4, pnlCarta11P4, pnlCarta12P4, pnlCarta13P4, pnlCarta14P4 };

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

        
    }
}
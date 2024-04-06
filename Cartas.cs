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
        public Dictionary<string, string[]> cartinhas = new Dictionary<string, string[]>();

        Partida p;


        public Cartas(Partida partida)
        {
            this.p = partida;
        }

        public void ExibirCartas()
        {
            p.AtualizarTela();
        }

        public void MostrarCartas(string[] aux, string[] DadosConsultarMao, int i, string idJogador)
        {
            List<GroupBox> groupBoxes = new List<GroupBox> { p.grbPlayer1, p.grbPlayer2, p.grbPlayer3, p.grbPlayer4 };
            List<ListBox> listBoxes = new List<ListBox> { p.lsbPlayer1, p.lsbPlayer2, p.lsbPlayer3, p.lsbPlayer4 };

            groupBoxes[i].Visible = true;
            listBoxes[i].Items.Clear();

            groupBoxes[i].Text = aux[1];
            listBoxes[i].Items.Add("Posição | Naipe");

            for (int j = 0; j < DadosConsultarMao.Length; j++)
            {
                string[] aux2 = DadosConsultarMao[j].Split(',');
                if (aux2[0] == aux[0])
                {
                    if (aux2[0] == idJogador)
                    {
                        this.cartinhas.Add(aux2[1], ImagemCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i));
                        listBoxes[i].Items.Add(aux2[1] + " | " + cartinhas[aux2[1]][0]);
                    }
                    else
                    {
                        listBoxes[i].Items.Add(aux2[1] + " | " + aux2[2]);
                        ImagemCartasJogador(aux2[2], Convert.ToInt32(aux2[1]), i);
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

            List<List<Panel>> nome = new List<List<Panel>>();
            nome.Add(CartasP1);
            nome.Add(CartasP2);
            nome.Add(CartasP3);
            nome.Add(CartasP4);

            List<string> imagens = new List<string> {
                "Copas1.png",        //C
                "Espadas1.png",      //E
                "Estrela1.png",     //S
                "Lua1.png",          //L
                "Ouros1.png",        //O
                "Paus1.png",         //P
                "Triângulo1.png",   //T
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
            if (imagemPosicao != 42)
            {
                nome[i][posicao - 1].BackgroundImage = Image.FromFile(pasta_imagens + imagens[imagemPosicao]);
                nome[i][posicao - 1].BackgroundImageLayout = ImageLayout.Stretch;
                nome[i][posicao - 1].Visible = true;
            }

            aux[0] = naipe;
            aux[1] = imagens[imagemPosicao];
            return aux;
        }
        
        /* Enviar Carta Pelas Imagens
        public bool VerificandoCartas(int x, int y, int width, int height)
        {
            Point mousePos = Control.MousePosition;

            Form meuFormulario = Application.OpenForms["Partida"];
            Point localMousePos = meuFormulario.PointToClient(mousePos);

            if (localMousePos.X >= x && localMousePos.X <= x + width && localMousePos.Y >= y && localMousePos.Y <= y + height)
            {
                return true;
            }
            return false;
        }

        public int CartaSelecionada()
        {
            foreach(var carta in cartinhas)
            {
                
                if (VerificandoCartas())
                {
                    return Convert.ToInt32(carta.Key);
                }
            }
            return 0;
        }
        */
    }
}

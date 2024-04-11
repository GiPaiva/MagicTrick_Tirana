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
        public Dictionary<string, int> local = new Dictionary<string, int>();
        public Dictionary<string, string> dic = new Dictionary<string, string>();

        Partida p;
        Lobby lobby = new Lobby();


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
            this.cartinhas.Clear();
            int k = 0;
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

                    if(k == 0)
                    {
                        local.Add(aux2[0].Trim(),i);
                        k++;
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

            string[] aux = new string[2];
            int imagemPosicao = 0;
            pasta_imagens = Path.Combine(Application.StartupPath, "Cartas/");
            
            this.dic.Clear();
            dic.Add("C", "Copas1.png");
            dic.Add("E", "Espadas1.png");
            dic.Add("S", "Estrela1.png");
            dic.Add("L", "Lua1.png");
            dic.Add("O", "Ouros1.png");
            dic.Add("P", "Paus1.png");
            dic.Add("T", "Triângulo1.png");

            if (imagemPosicao != 42)
            {
                nome[i][posicao - 1].BackgroundImage = Image.FromFile(pasta_imagens + dic[naipe]);
                nome[i][posicao - 1].BackgroundImageLayout = ImageLayout.Stretch;
                nome[i][posicao - 1].Visible = true;
            }

            aux[0] = naipe;
            aux[1] = dic[naipe];
            return aux;
        }
        
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
                
                if (VerificandoCartas(14, 3, 38, 54))
                {
                    MessageBox.Show("foi");
                    return Convert.ToInt32(carta.Key);
                }
            }
            return 0;
        }
        

        public string[] VerificarJogadaDosPlayers(string idJogador)
        {
            List<Panel> panelCartasMeio = new List<Panel> { p.pnlCartaP1, p.pnlCartaP2, p.pnlCartaP3, p.pnlCartaP4};
            List<Label> labelCartasMeio = new List<Label> { p.lblCartaP1, p.lblCartaP2, p.lblCartaP3, p.lblCartaP4 };

            string[] retorno = lobby.LobbyExibirJogadas();

            int posicao = local[idJogador];
            p.grbPlayer4.Visible = true;
            p.lsbPlayer4.Visible = true;
            p.lsbPlayer4.Items.Clear();
            foreach(string jogadas in retorno)
            {
                string[] aux = jogadas.Split(',');
                if (aux[1] == idJogador)
                {
                    panelCartasMeio[posicao].Visible = true;
                    labelCartasMeio[posicao].Visible = true;

                    panelCartasMeio[posicao].BackgroundImage = Image.FromFile(pasta_imagens + dic[aux[2]]);
                    panelCartasMeio[posicao].BackgroundImageLayout = ImageLayout.Stretch;
                    labelCartasMeio[posicao].Text = aux[3];
                }

                p.lsbPlayer4.Items.Add(jogadas);
            }

            return retorno;
        }
    }
}

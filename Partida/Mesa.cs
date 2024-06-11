using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicTrick_Tirana
{
    public partial class Partida : Form
    {
        public void LimparAMesa()
        {
            c.LimparAsCartas();
            c.cartasDaGalera.Clear();
            MesaDistribuirMao();
            RetirarCartasDoMeioDaMesa();
            primeiraVez = true;
            apostar = true;
        }

        public void MesaComecar()
        {
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    _ = MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
            estado = "J";
            btnComecar.Visible = false;
            MesaDistribuirMao();
            grbPlayer1.Visible = true;
            bot = new BotZob(c.cartasDaGalera, idJogador);
            label23.Text = PartidaSelecionada;
        }

        private void MesaDistribuirMao()
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = t.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
        }

        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
        {
            c.localNaMesaCadaJogador.Clear();
            int i = 0;
            foreach(string idJogador in JogadoresAtuais)
            {
                string[] aux = JogadoresAtuais[i].Split(',');
                c.MostrarCartas(aux, DadosConsultarMao, i, idJogador);
                i++;
            }
        }

        private void MesaApostar(string posicaoResposta)
        {           
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
            string[] DadosJogador = Jogador.Split(',');

            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(posicaoResposta);
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
            posicaoDoJogador = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
            labels[posicaoDoJogador].Text = retorno;
        }

        protected void MesaJogadorDaVez(string[] InfoRetorno)
        {
            string[] JogadorInfo = Jogador.Split(',');
            idJogador = JogadorInfo[0];

            foreach (string itens in JogadoresAtuais)
            {
                string[] infoJogador = itens.Split(',');

                if (infoJogador[0] == InfoRetorno[1])
                {
                    lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                }
                if (InfoRetorno[1] == JogadorInfo[0])
                {
                    vez = true;
                }
                else
                {
                    vez = false;
                }
            }
        }

        protected void MesaColocarJogadas(string[] DadosRetornoVez)
        {
            int TamRetorno = DadosRetornoVez.Length;
            string[] InfoRetornoJogada = DadosRetornoVez[TamRetorno - 1].Split(',');
            string[] aux = InfoRetornoJogada[0].Split(':');

            if (aux[0] == "C")
            {
                posicaoDoJogador = c.localNaMesaCadaJogador[aux[1]];
                c.ColocarCartasMeio(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3], posicaoDoJogador);
            }
            else
            {
                foreach (string dados in DadosRetornoVez)
                {
                    string[] InfoRetornoJogada2 = dados.Split(',');
                    string[] aux2 = InfoRetornoJogada2[0].Split(':');

                    if (aux2[0] == "A")
                    {
                        List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                        posicaoDoJogador = c.localNaMesaCadaJogador[aux[1].Trim()];
                        labels[posicaoDoJogador].Text = Convert.ToString(InfoRetornoJogada2[2]);
                    }
                }
            }
        }

        protected void RetirarCartasDoMeioDaMesa()
        {
            foreach (Control control in pnlCartasMeio.Controls)
            {
                control.Visible = false;
            }
            cartasJogadas = 0;
        }

        protected void HouveAposta(string[] DadosRetornoVez)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };

            foreach (string item in DadosRetornoVez)
            {
                string[] aux = item.Split(':');

                if (aux[0] == "A")
                {
                    string[] Id = aux[1].Split(',');
                    posicaoDoJogador = c.localNaMesaCadaJogador[Id[0].Trim()];
                    c.ValorCartasJogador(Id[0], Id[2], Id[4]);
                    labels[posicaoDoJogador].Text = Convert.ToString(Id[2]);
                }
            }
        }

        public void Pontos()
        {
            List<Label> PontosTotais = new List<Label> { lblTotalP1, lblTotalP2, lblTotalP3, lblTotalP4 };
            List<Label> PontosRodada = new List<Label> { lblPontos, lblPontoP2, lblPontosP3, lblPontosP4 };

            lblParticipantes.Visible = false;

            foreach (string JogadoresAtuais in JogadoresAtuais)
            {
                string[] aux = JogadoresAtuais.Split(',');
                posicaoDoJogador = c.localNaMesaCadaJogador[aux[0]];

                PontosTotais[posicaoDoJogador].Text = aux[2];
                PontosRodada[posicaoDoJogador].Text = aux[3];
                if (aux[0] == idJogador)
                {
                    pontos = Convert.ToInt32(aux[3]);
                }
            }
        }
    }
}

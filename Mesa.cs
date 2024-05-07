using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Windows.Forms;

namespace MagicTrick_Tirana
{
    class Mesa : Partida, IPartida
    {
        Tratamento t = new Tratamento();
        Cartas c = new Cartas();
        Bot b = new Bot();
        string pasta_imagens = "";


        public void Comecar()
        {
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
            estado = "J";
            btnComecar.Visible = false;
            ConsultarMao();
        }

        public async Task VerificarVez()
        {
            int TamRetorno = 1;
            int jogadas = 0;

            while (estado == "J")
            {
                int IdPartida = Convert.ToInt32(PartidaAtual[0]);
                string retorno = Jogo.VerificarVez2(IdPartida);

                //status da partida, id do jogador da vez, numero da rodada, status da rodada
                string[] DadosRetorno = t.TratarDadosEmArray(retorno);

                //colocando o nome do jogador da vez
                string[] InfoRetorno = DadosRetorno[0].Split(',');
                estado = InfoRetorno[0];

                foreach(string itens in JogadoresAtuais)
                {
                    string[] infoJogador = itens.Split(',');
                    if (infoJogador[0] == InfoRetorno[1])
                    {
                        lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                    }
                }

                string[] InfoJogador = Jogador.Split(',');
                if (InfoRetorno[1] == InfoJogador[0] && InfoRetorno[3] == "C" && !vez)
                {
                    vez = true;
                    MessageBox.Show("É a sua vez !");

                    int round = Convert.ToInt32(InfoRetorno[2]);
                    string posicao1 = b.BotJogar(round, IdPartida, c.cartinhasDoJogadorAtual, c.cartasJogadas, c.posicoesCartasMao, c.QuantidadeDeCartasTotal, pontos);//
                    string valorCarta = this.Jogar(posicao1);
                }

                //verificando carta jogada
                if(DadosRetorno.Length != TamRetorno && DadosRetorno.Length > 1)
                {
                    TamRetorno = DadosRetorno.Length;
                    if(TamRetorno > 1)
                    {
                        string[] InfoRetornoJogada = DadosRetorno[TamRetorno - 1].Split(',');
                        string[] aux = InfoRetornoJogada[0].Split(',');

                        if (aux[0] == "C")
                        {
                            ColocarNoMeio(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3]);
                        }
                        else //aposta
                        {
                            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                            int posicaoAposta = localNaMesaCadaJogador[aux[1].Trim()];
                            labels[posicaoAposta].Text = Convert.ToString(InfoRetornoJogada[2]);
                        }
                    }
                }

                jogadas = 0;
                foreach (var joga in cartasJogadas)
                {
                    jogadas++;
                }

                await Task.Delay(8000);
            }
        }

        public void Apostar(string senha, string retorno, string IdJogador)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };

            int aux = c.localNaMesaCadaJogador[IdJogador];
            labels[aux].Text = retorno;
        }

        public async void ConsultarMao()
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = t.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = "J";
            c.MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
            await VerificarVez();
        }

        public string Jogar(string pos)
        {
            int posicao = Convert.ToInt32(pos);

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int idJogador = Convert.ToInt32(DadosJogador[0]);

            //Posição
            string retorno = Jogo.Jogar(idJogador, DadosJogador[1], posicao);
            vez = false;
            return retorno;
        }

        public void ColocarNoMeio(string idJogador, string naipe, string valorDaCarta, string posicao)
        {
            List<Panel> panelCartasMeio = new List<Panel> { pnlCartaP1, pnlCartaP2, pnlCartaP3, pnlCartaP4 };
            List<Label> labelCartasMeio = new List<Label> { lblCartaP1, lblCartaP2, lblCartaP3, lblCartaP4 };

            int posicaoDoJogador = localNaMesaCadaJogador[idJogador];

            panelCartasMeio[posicaoDoJogador].Visible = true;
            labelCartasMeio[posicaoDoJogador].Visible = true;

            panelCartasMeio[posicaoDoJogador].BackgroundImage = Image.FromFile(pasta_imagens + NaipesDasCrtasEImagens[naipe]);
            panelCartasMeio[posicaoDoJogador].BackgroundImageLayout = ImageLayout.Stretch;
            labelCartasMeio[posicaoDoJogador].Text = valorDaCarta;

            if (!cartasJogadas.ContainsKey(panelCartasMeio[posicaoDoJogador]))
                cartasJogadas.Add(panelCartasMeio[posicaoDoJogador], labelCartasMeio[posicaoDoJogador]);

            Label valor = new Label();
            valor.Text = valorDaCarta;
            valor.Location = new Point(9, 18);
            valor.BackColor = Color.Black;
            valor.ForeColor = Color.Black;

            int posicaoMao = Convert.ToInt32(posicao) - 1;
            panelsDasCartasDeCadaJogador[posicaoDoJogador][posicaoMao].Controls.Add(valor);
        }

    }
}

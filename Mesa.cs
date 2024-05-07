using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Windows.Forms;
using static Lobby;

namespace MagicTrick_Tirana
{
    class Mesa : IPartida, IProps
    {
        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }
        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }


        Tratamento t = new Tratamento();
        Partida p = new Partida();
        Lobby lobby = new Lobby();
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
            p.estado = "J";
            p.btnComecar.Visible = false;

            ConsultarMao();
        }

        public async void ConsultarMao()
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = t.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            p.estado = "J";
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);

            await Task.Delay(10000);
            await VerificarVez();
        }

        public void MostrarGalera(string IdJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
        {
            bool primeiro = true;
            p.localNaMesaCadaJogador.Clear();

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux1 = JogadoresAtuais[i].Split(',');

                if (aux1[0] != IdJogador)
                {
                    if (i == 0)
                    {
                        i++;
                        primeiro = false;
                    }

                    c.MostrarCartas(aux1, DadosConsultarMao, i, "player", JogadoresAtuais);

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    c.MostrarCartas(aux1, DadosConsultarMao, 0, IdJogador, JogadoresAtuais);
                }
            }
        }

        public async Task VerificarVez()
        {
            int TamRetorno = 1;

            while (p.estado == "J")
            {
                int IdPartida = Convert.ToInt32(PartidaAtual[0]);
                string retorno = Jogo.VerificarVez2(IdPartida);

                //status da partida, id do jogador da vez, numero da rodada, status da rodada
                string[] DadosRetorno = t.TratarDadosEmArray(retorno);

                //colocando o nome do jogador da vez
                string[] InfoRetorno = DadosRetorno[0].Split(',');
                p.estado = InfoRetorno[0];

                //Dizendo de Quem é a vez
                foreach(string itens in JogadoresAtuais)
                {
                    string[] infoJogador = itens.Split(',');
                    if (infoJogador[0] == InfoRetorno[1])
                    {
                        p.lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                    }
                }

                //É a sua vez
                string[] InfoJogador = Jogador.Split(',');
                if (InfoRetorno[1] == InfoJogador[0] && InfoRetorno[3] == "C" && !p.vez)
                {
                    p.vez = true;
                    MessageBox.Show("É a sua vez !");
                    
                    int round = Convert.ToInt32(InfoRetorno[2]);
                    
                    //Jogada
                    await Task.Delay(10000);
                    string posicaoJogar = b.BotJogar(round, IdPartida, c.cartinhasDoJogadorAtual, c.cartasJogadas, c.posicoesCartasMao, c.QuantidadeDeCartasTotal, p.pontos);
                    string valorCarta = this.Jogar(posicaoJogar);

                    //Aposta
                    await Task.Delay(10000);
                    int posicaoApostar = b.BotApostar(c.posicoesCartasMao);
                    this.Apostar(posicaoApostar);
                }

                //Arrumando a Mesa
                await Task.Delay(10000);
                ArrumarMesa(IdPartida);

                /*
                //verificando carta jogada
                //if (DadosRetorno.Length != TamRetorno && DadosRetorno.Length > 1 && !vez)
                //{
                //    TamRetorno = DadosRetorno.Length;
                //    if(TamRetorno > 1)
                //    {
                //        string[] InfoRetornoJogada = DadosRetorno[TamRetorno - 1].Split(',');
                //        string[] aux = InfoRetornoJogada[0].Split(',');

                //        if (aux[0] == "C")
                //        {
                //            ColocarNoMeio(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3]);
                //        }
                //    }
                //}
                */
                await Task.Delay(8000);
            }
        }

        public void ArrumarMesa(int IdPartida)
        {
            string[] RetornoJogadas = lobby.LobbyExibirJogadas(IdPartida);
            if (RetornoJogadas != null || RetornoJogadas != default)
            {
                string[] infoUltimaJogada = RetornoJogadas[RetornoJogadas.Length - 1].Split(',');
                string idJogador = infoUltimaJogada[1];
                string naipe = infoUltimaJogada[2];
                string valorDaCarta = infoUltimaJogada[3];
                string posicaoDaCarta = infoUltimaJogada[4];

                ColocarNoMeio(idJogador, naipe, valorDaCarta, posicaoDaCarta);
            }
        }

        public void Apostar(int posicao)
        {
            List<Label> labels = new List<Label> { p.lblAposta, p.lblAposta2, p.lblAposta3, p.lblAposta4 };

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string SenhaJogador = DadosJogador[1];

            string valor = Jogo.Apostar(IdJogador, SenhaJogador, posicao);

            int aux = c.localNaMesaCadaJogador[DadosJogador[0]];
            labels[aux].Text = valor;
        }

        public string Jogar(string pos)
        {
            int posicao = Convert.ToInt32(pos);

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int idJogador = Convert.ToInt32(DadosJogador[0]);

            //Posição
            string retorno = Jogo.Jogar(idJogador, DadosJogador[1], posicao);
            p.vez = false;
            return retorno;
        }

        public void ColocarNoMeio(string idJogador, string naipe, string valorDaCarta, string posicao)
        {
            List<Panel> panelCartasMeio = new List<Panel> { p.pnlCartaP1, p.pnlCartaP2, p.pnlCartaP3, p.pnlCartaP4 };
            List<Label> labelCartasMeio = new List<Label> { p.lblCartaP1, p.lblCartaP2, p.lblCartaP3, p.lblCartaP4 };

            int posicaoDoJogador = c.localNaMesaCadaJogador[idJogador];

            panelCartasMeio[posicaoDoJogador].Visible = true;
            labelCartasMeio[posicaoDoJogador].Visible = true;

            panelCartasMeio[posicaoDoJogador].BackgroundImage = Image.FromFile(pasta_imagens + c.NaipesDasCrtasEImagens[naipe]);
            panelCartasMeio[posicaoDoJogador].BackgroundImageLayout = ImageLayout.Stretch;
            labelCartasMeio[posicaoDoJogador].Text = valorDaCarta;

            if (!c.cartasJogadas.ContainsKey(panelCartasMeio[posicaoDoJogador]))
                c.cartasJogadas.Add(panelCartasMeio[posicaoDoJogador], labelCartasMeio[posicaoDoJogador]);

            Label valor = new Label();
            valor.Text = valorDaCarta;
            valor.Location = new Point(9, 18);
            valor.BackColor = Color.Black;
            valor.ForeColor = Color.Black;

            int posicaoMao = Convert.ToInt32(posicao) - 1;
            c.panelsDasCartasDeCadaJogador[posicaoDoJogador][posicaoMao].Controls.Add(valor);
        }

    }
}

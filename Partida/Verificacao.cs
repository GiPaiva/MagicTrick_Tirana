using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lobby;

namespace MagicTrick_Tirana
{
    public partial class Partida : Form
    {
        private void VerificarVez()
        {
            string retorno = Jogo.VerificarVez2(IdPartida);

            //label6.Text = retorno;

            string[] DadosRetornoVez = t.TratarDadosEmArray(retorno);
            string[] InfoRetorno = DadosRetornoVez[0].Split(',');
            estado = InfoRetorno[0];
            rodada = Convert.ToInt32(InfoRetorno[2]);
            
            if (vez)
            {
                resposta = bot.Jogar(VerificarJogadasArray); 
                Jogar();
                vez = false;
            }

            if (alteracao != retorno && InfoRetorno[3] == "C")
            {
                alteracao = retorno;

                Pontos();
                HouveAposta(DadosRetornoVez);


                if (DadosRetornoVez.Length > 1)
                {
                    MesaColocarJogadas(DadosRetornoVez);
                    //cartasJogadas++;
                }

                VerificarJogadas(IdPartida);
                VerificarJogadasNoRoundAtual();
                MesaJogadorDaVez(InfoRetorno);

                //Se o round acabou, limpar a mesa se VerificarJogadasArray estiver vazio e não for a primeira vez
                if (VerificarJogadasArray[0] == "" && !primeiraVez)
                {
                    LimparAMesa();
                    bot.BotZobNovaRodada(c.cartasDaGalera, idJogador);
                }
                primeiraVez = false;
            }

            VerificacaoDeCartasNaMesa(InfoRetorno, IdPartida);
        }

        public void VerificarJogadores()
        {
            int quantidade = 0;

            lobby.LobbyListarJogadores2(PartidaSelecionada, "1");

            lblQJogadores.Visible = true;

            if (quantidade != lobby.Jogadores.Length)
            {
                JogadoresAtuais = lobby.Jogadores;
                lblParticipantes.Text = "";
                foreach (string JogadoresAtuais in JogadoresAtuais)
                {
                    lblParticipantes.Text += JogadoresAtuais + "\n\r";
                }
            }
        }

        private void VerificarQuemGanhou()
        {
            int maior = 0;
            int i = 0;
            VerificarJogadores();
            foreach (string JogadoresAtuais in JogadoresAtuais)
            {
                string[] aux = JogadoresAtuais.Split(',');
                int pontuacao = Convert.ToInt32(aux[2]);
                if (i == 0)
                {
                    maior = pontuacao;
                    ganhador = aux[1];
                    i++;
                }
                else if (pontuacao > maior)
                {
                    maior = pontuacao;
                    ganhador = aux[1];
                }
            }

            MessageBox.Show($"O Ganhador é: {ganhador} !!", "Parabéns!!", MessageBoxButtons.OK);
            tmrVerificarVez.Enabled = false;
            tmrVerificarVez.Stop();
            tmrVerificarVez.Dispose();
            acabou = true;
            this.Close();
        }

        public void VerificarJogadas(int IdPartida)
        {
            string jogadas = Jogo.ExibirJogadas2(IdPartida);
            label15.Text = jogadas;
            //label15.Text = "";
            VerificarJogadasArray = t.TratarDadosEmArray(jogadas);
        }

        public void VerificarJogadasNoRoundAtual()
        {
            if (VerificarJogadasArray != null && VerificarJogadasArray.Length != 0)
            {
                label6.Text = "";
                foreach (string s in VerificarJogadasArray)
                {
                    string[] aux = s.Split(',');
                    if (aux[0] == Convert.ToString(rodada))
                    {
                        VerificarJogadasNoRoundAtualArray.Add(s);
                        //label6.Text += s + "\n";
                    }
                }

                //if (cartasJogadas == JogadoresAtuais.Count())
                //{
                //    RetirarCartasMeio();
                //}
            }

        }

        public void VerificacaoDeCartasNaMesa(string[] InfoRetornoVerificarVez, int idPartida)
        {
            string NuemeroDaRodada = InfoRetornoVerificarVez[2];

            if (NuemeroDaRodada == null || VerificarJogadasArray == null)
            {
                return;
            }

            foreach (string jogada in VerificarJogadasArray)
            {
                if (jogada != null && jogada != "")
                {
                    string[] InfoExibirJogadas = jogada.Split(',');
                    posicaoDoJogador = c.localNaMesaCadaJogador[InfoExibirJogadas[1]];
                    c.ColocarCartasMeio(InfoExibirJogadas[1], InfoExibirJogadas[2], InfoExibirJogadas[3], InfoExibirJogadas[4], posicaoDoJogador);
                }
            }
        }
    }
}

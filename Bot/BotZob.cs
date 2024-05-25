using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;

namespace MagicTrick_Tirana
{
    class BotZob
    {
        public Dictionary<string, JogadorInfo> jogadoresInfos = new Dictionary<string, JogadorInfo>();
        JogadorInfo jogador;
        private string Id = "";
        //private string Id = "idDoJogador";
        int quantidadeDeCartasNaMao = 0;
        Partida partida = new Partida();

        public BotZob(Dictionary<string, List<string>> cartas, string IdBot)
        {
            foreach (var item in cartas)
            {
                if (item.Value != null)
                {
                    jogadoresInfos.Add(item.Key, new JogadorInfo(item.Key, item.Value));
                }

                if (item.Key == IdBot)
                {
                    this.quantidadeDeCartasNaMao = item.Value.Count();
                }
            }

            this.Id = IdBot;
        }

        string[] possibilidades = { };

        public string Jogar(string[] Jogadas, string[] JogadasAtuais)
        {
            //bool res;
            int opcao = 1;
            string cartaAJogar = "";

            AtualizarCartasMao(JogadasAtuais);

            if (JogadasAtuais.Length == 0 || JogadasAtuais[0] == "")
            {
                // Você é o primeiro a jogar
                //do
                //{
                opcao--;

                // Conferir Cartas, pega a carta possível retornando o naipe e sua posição
                cartaAJogar = ConferirCarta(jogadoresInfos[Id], opcao);

                //    // Examinar Escolha verifica se a carta selecionada é boa, com base em parâmetros como pontos e se todos têm o naipe
                //    //res = ExaminarEscolha(cartaAJogar);

                //} while (!res);

            }
            else
            {
                // Você não é o primeiro

                // Tem o Naipe verifica se dentro das cartas do jogador tem o naipe requerido (o primeiro naipe jogado na partida), setando uma variável local (string[] possibilidades) e devolvendo se é true (caso possibilidades.Length > 0) ou false
                if (TemONaipe(JogadasAtuais))
                {
                    // Examinar Jogadas verifica se uma carta master (7 do primeiro naipe ou um coração) foi jogada, caso não return true (ou seja, possível ganhar)
                    // NaipesTodosIguais verifica se a cartas jogadas têm todas o mesmo naipe, caso sim true
                    if (!ExaminarJogadas(JogadasAtuais) && NaipesTodosIguais(JogadasAtuais))
                    {
                        opcao--;
                    }
                    else
                    {
                        opcao = possibilidades.Length;
                    }
                    cartaAJogar = ConferirCarta(possibilidades, opcao);
                }
                else
                {
                    opcao--;
                    cartaAJogar = ConferirCarta(jogadoresInfos[Id], opcao);
                }
            }

            return cartaAJogar;
        }

        public string Apostar(int pontos, string[] Jogadas)
        {
            int opcao = 0;
            string cartaAJogar = "";

            if (pontos == 0 && (Jogadas.Count() / jogadoresInfos.Count() + 1 == quantidadeDeCartasNaMao))
            {
                //Joga menor carta
                opcao = jogadoresInfos[Id].Cartas.Count();
            }
            else if (pontos >= 2 && pontos <= 4)
            {
                //Joga a carta do meio
                opcao = jogadoresInfos[Id].Cartas.Count() / 2;
            }
            else if (pontos > 4)
            {
                //Joga a maior carta
                opcao--;
            }
            cartaAJogar = ConferirCarta(jogadoresInfos[Id], opcao);
            return cartaAJogar;
        }


        private void AtualizarCartasMao(string[] JogadasAtuais)
        {
            // Suponha que `JogadasAtuais` contenha as cartas jogadas na rodada atual
            // Atualiza as cartas na mão do jogador com base nas jogadas feitas

            foreach (var jogada in JogadasAtuais)
            {
                string[] aux = jogada.Split(',');
                string juncao = aux[4] + "," + aux[2];

                // Remover a carta jogada da mão do jogador
                if (jogadoresInfos.ContainsKey(aux[1]))
                {
                    foreach (string item in jogadoresInfos[Id].Cartas)
                    {
                        if (item == juncao)
                        {
                            jogadoresInfos[Id].Cartas.Remove(item);
                            break;
                        }

                        partida.label6.Text += item + "\n";
                    }
                    if (Id == aux[1])
                    {
                        quantidadeDeCartasNaMao--;
                    }
                }
            }
        }

        private string ConferirCarta(string[] cartas, int opcao)
        {
            // Conferir uma carta com base na opção fornecida
            return cartas[cartas.Length - opcao];

        }

        private string ConferirCarta(JogadorInfo cartas, int opcao)
        {
            return cartas.Cartas[cartas.Cartas.Count() - 1 - opcao];
        }

        //private bool ExaminarEscolha(string carta)
        //{
        //    // Verificar se a seleção da carta é boa
        //    // Exemplos: verifica valor alto ou critério estratégico

        //    int temNaipe = 0;

        //    foreach(var mao in jogadoresInfos)
        //    {

        //        JogadorInfo jogador = mao.value;
        //    }
        //    //if ()
        //        return false;

        //    string valor = carta.Substring(0, carta.Length - 1); // Extrai o valor da carta
        //    return valor == "5" || valor == "6" || valor == "7";
        //}

        private bool TemONaipe(string[] jogadasAtuais)
        {
            // Definir o array `possibilidades` e retornar true se houver cartas com o naipe requerido
            string[] dadosJogadasAtuais0 = jogadasAtuais[0].Split(',');
            string naipeRequerido = dadosJogadasAtuais0[2];
            List<string> poss = new List<string>();

            JogadorInfo jogadorInfo = jogadoresInfos[Id];

            if (jogadorInfo != null && jogadorInfo.Cartas != null)
            {
                foreach (var carta in jogadorInfo.Cartas)
                {
                    string[] aux = carta.Split(',');
                    if (aux[1] == naipeRequerido)
                    {
                        poss.Add(carta);
                    }
                }
            }

            possibilidades = poss.ToArray();
            return poss.Count > 0;
        }

        private bool ExaminarJogadas(string[] jogadasAtuais)
        {
            // Retornar true se uma carta master foi jogada
            foreach (var jogada in jogadasAtuais)
            {
                if (jogada.Contains("7") || jogada.Contains("C"))
                {
                    return true;
                }
            }
            return false;
        }

        private bool NaipesTodosIguais(string[] jogadasAtuais)
        {
            // Retornar true se todas as cartas jogadas têm o mesmo naipe
            string[] dadosJogadasAtuais0 = jogadasAtuais[0].Split(',');
            string primeiroNaipe = dadosJogadasAtuais0[2];
            int i = 0;

            foreach (var jogada in jogadasAtuais)
            {
                string[] aux = jogada.Split(',');
                if (aux[2] == primeiroNaipe)
                {
                    i++;
                }
            }
            return i == jogadasAtuais.Length;
        }
    }
}
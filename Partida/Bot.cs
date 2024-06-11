using System;
using System.Collections.Generic;

namespace MagicTrick_Tirana
{
    class Bot
    {
        private Dictionary<string, List<string>> jogadoresInfos = new Dictionary<string, List<string>>();
        private string Id = "idDoJogador"; // Suponha que você tenha o ID do jogador atual

        public string Jogar(string[] Jogadas, string[] JogadasAtuais)
        {
            bool res;
            int opcao = 1;
            string cartaAJogar = "";

            AtualizarCartasMao(JogadasAtuais);

            if (JogadasAtuais[0] == "")
            {
                // Você é o primeiro a jogar
                do
                {
                    opcao--;

                    // Conferir Cartas, pega a carta possível retornando o naipe e sua posição
                    cartaAJogar = ConferirCarta(jogadoresInfos[Id].ToArray(), opcao);

                    // Examinar Escolha verifica se a carta selecionada é boa, com base em parâmetros como pontos e se todos têm o naipe
                    res = ExaminarEscolha(cartaAJogar);

                } while (!res);

            }
            else
            {
                // Você não é o primeiro
                string[] possibilidades;

                // Tem o Naipe verifica se dentro das cartas do jogador tem o naipe requerido (o primeiro naipe jogado na partida), setando uma variável local (string[] possibilidades) e devolvendo se é true (caso possibilidades.Length > 0) ou false
                if (TemONaipe(JogadasAtuais, out possibilidades))
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
                    cartaAJogar = ConferirCarta(jogadoresInfos[Id].ToArray(), opcao);
                }
            }

            return cartaAJogar;
        }

        public int Apostar(int pontos)
        {
            int decisao = 0;
            if (pontos == 0 && (Jogadas.Lenght / jogadoresInfo.Lenght) + 1 == quantidaDeCartasNaMao)
            {
                //jogar a menor carta
            }
            else if (pontos >= 2 && pontos <= 4)
            {
                //Joga a carta do meio
            }
            else if (pontos > 4)
            {
                //Joga a maior carta
            }

            return decisao;
        }


        private void AtualizarCartasMao(string[] JogadasAtuais)
        {
            // Suponha que `JogadasAtuais` contenha as cartas jogadas na rodada atual
            // Atualiza as cartas na mão do jogador com base nas jogadas feitas

            foreach (var jogada in JogadasAtuais)
            {
                // Remover a carta jogada da mão do jogador
                if (jogadoresInfos.ContainsKey(Id))
                {
                    jogadoresInfos[Id].Remove(jogada);
                }
            }
        }

        private string ConferirCarta(string[] cartas, int opcao)
        {
            // Conferir uma carta com base na opção fornecida
            if (opcao >= 0 && opcao < cartas.Length)
            {
                return cartas[opcao];
            }
            return null;
        }

        private bool ExaminarEscolha(string carta)
        {
            // Verificar se a seleção da carta é boa
            // Exemplos: verifica valor alto ou critério estratégico

            if (string.IsNullOrEmpty(carta))
                return false;

            // Cartas de valor alto são boas
            string valor = carta.Substring(0, carta.Length - 1); // Extrai o valor da carta
            return valor == "5" || valor == "6" || valor == "7";
        }

        private bool TemONaipe(string[] jogadasAtuais, out string[] possibilidades)
        {
            // Definir o array `possibilidades` e retornar true se houver cartas com o naipe requerido
            string naipeRequerido = jogadasAtuais[0].Substring(jogadasAtuais[0].Length - 1); // Supondo que o último caractere represente o naipe
            List<string> poss = new List<string>();

            if (jogadoresInfos.ContainsKey(Id))
            {
                foreach (var carta in jogadoresInfos[Id])
                {
                    if (carta.EndsWith(naipeRequerido))
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
                if (jogada.Contains("7") || jogada.Contains("C")) // 'C' coração
                {
                    return true;
                }
            }
            return false;
        }

        private bool NaipesTodosIguais(string[] jogadasAtuais)
        {
            // Retornar true se todas as cartas jogadas têm o mesmo naipe
            string naipe = jogadasAtuais[0].Substring(jogadasAtuais[0].Length - 1); // Supondo que o último caractere represente o naipe
            foreach (var jogada in jogadasAtuais)
            {
                if (!jogada.EndsWith(naipe))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
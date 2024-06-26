﻿using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml.Schema;

namespace MagicTrick_Tirana
{
    class BotZob
    {
        public Dictionary<string, JogadorInfo> jogadoresInfos = new Dictionary<string, JogadorInfo>();
        private string Id = "";
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

        public void BotZobNovaRodada(Dictionary<string, List<string>> cartas, string IdBot)
        {
            jogadoresInfos.Clear();
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

        /*Code 2*/
        public string Jogar(string[] Jogadas)
        {
            string cartaAJogar = "";
            AtualizarCartasMao(Jogadas);

            bool primeirajogadadoRound = VerificarSeÉAPrimeiraJogada(Jogadas);

            if (Jogadas.Length == 0 || Jogadas[0] == "" || primeirajogadadoRound)
            {
                // Você é o primeiro a jogar
                cartaAJogar = JogarCartaMaisBaixa(jogadoresInfos[Id].Cartas);
            }
            else
            {
                // Você não é o primeiro
                // Tem o Naipe verifica se dentro das cartas do jogador tem o naipe requerido
                if (TemONaipe(Jogadas))
                {
                    cartaAJogar = JogarCartaMenorPossivel(possibilidades);
                }
                else
                {
                    // Se não tem o naipe, joga o maior trunfo ou a carta mais alta
                    cartaAJogar = JogarMaiorTrunfoOuMaisAlta(jogadoresInfos[Id].Cartas);
                }
            }

            jogadoresInfos[Id].Cartas.Remove(cartaAJogar);
            return cartaAJogar;
        }

        private string JogarCartaMaisBaixa(List<string> cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarCartaMenorPossivel(string[] cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarMaiorTrunfoOuMaisAlta(List<string> cartas)
        {
            string maiorTrunfo = cartas
                .Where(c => c.Contains("C"))
                .OrderByDescending(c => int.Parse(c.Split(',')[0]))
                .FirstOrDefault();

            if (maiorTrunfo != null)
            {
                return maiorTrunfo;
            }

            return cartas.OrderByDescending(c => int.Parse(c.Split(',')[0])).First();
        } /*End Code 2*/

        /*Code 3
        public string Jogar(string[] Jogadas)
        {
            string cartaAJogar = "";

            AtualizarCartasMao(Jogadas);

            bool primeirajogadadoRound = VerificarSeÉAPrimeiraJogada(Jogadas);

            if (Jogadas.Length == 0 || Jogadas[0] == "" || primeirajogadadoRound)
            {
                // Você é o primeiro a jogar, joga a carta mais baixa
                cartaAJogar = JogarCartaMaisBaixa(jogadoresInfos[Id].Cartas);
            }
            else
            {
                // Você não é o primeiro
                if (TemONaipe(Jogadas))
                {
                    // Tem o naipe da jogada
                    cartaAJogar = JogarCartaMenorPossivel(possibilidades);
                }
                else
                {
                    // Não tem o naipe da jogada
                    cartaAJogar = JogarMaiorTrunfoOuMaisAlta(jogadoresInfos[Id].Cartas);
                }
            }

            jogadoresInfos[Id].Cartas.Remove(cartaAJogar);

            return cartaAJogar;
        }

        private string JogarCartaMaisBaixa(List<string> cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarCartaMenorPossivel(string[] cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarMaiorTrunfoOuMaisAlta(List<string> cartas)
        {
            string maiorTrunfo = cartas
                .Where(c => c.Contains("C")) // Supondo que o coração é identificado como "C"
                .OrderByDescending(c => int.Parse(c.Split(',')[0]))
                .FirstOrDefault();

            if (maiorTrunfo != null)
            {
                return maiorTrunfo;
            }

            return cartas.OrderByDescending(c => int.Parse(c.Split(',')[0])).First();
        } End of Code 3*/

        private bool VerificarSeÉAPrimeiraJogada(string[] Jogadas)
        {
            string[] dadosJogadasAtuais0 = Jogadas[Jogadas.Length - 1].Split(',');
            string rodadaAtual = dadosJogadasAtuais0[0];
            int contador = 0;

            if (Jogadas.Length > 1)
            {
                foreach (string rodadasAtuias in Jogadas)
                {
                    string[] auxiliar = rodadasAtuias.Split(',');
                    if (auxiliar[0] == rodadaAtual)
                    {
                        contador++;
                    }
                }
                if (contador == jogadoresInfos.Count())
                {
                    return true;
                }
            }
            return false;
        }

        /*Code 2
        public string Apostar(int pontos, string[] Jogadas)
        {
            int opcao = 0;
            string escolha = "0";

            if (jogadoresInfos[Id].Cartas.Count() == 1)
            {
                return ConferirCarta(jogadoresInfos[Id], opcao);
            }
            else if (pontos == 0 && (Jogadas.Count() / jogadoresInfos.Count() + 1 == quantidadeDeCartasNaMao))
            {
                // Joga a menor carta  
                escolha = JogarCartaMaisBaixa1(jogadoresInfos[Id].Cartas);
            }
            else if (pontos >= 2 && pontos <= 4)
            {
                // Joga a carta do meio
                escolha = JogarCartaDoMeio(jogadoresInfos[Id].Cartas);
            }
            else if (pontos > 4)
            {
                // Joga a maior carta
                escolha = JogarCartaMaisAlta(jogadoresInfos[Id].Cartas);
            }

            jogadoresInfos[Id].Cartas.Remove(escolha);

            return escolha;
        }

        private string JogarCartaMaisBaixa1(List<string> cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarCartaDoMeio(List<string> cartas)
        {
            int meio = cartas.Count / 2;
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).ElementAt(meio);
        }

        private string JogarCartaMaisAlta(List<string> cartas)
        {
            return cartas.OrderByDescending(c => int.Parse(c.Split(',')[0])).First();
        } End Code 2*/

        /*Code 3*/
        public string Apostar(int pontos, string[] Jogadas)
        {
            int opcao = 0;
            string escolha = "0";

            if (jogadoresInfos[Id].Cartas.Count() == 1)
            {
                return ConferirCarta(jogadoresInfos[Id], opcao);
            }
            else if (pontos == 0 && (Jogadas.Count() / jogadoresInfos.Count() + 1 == quantidadeDeCartasNaMao))
            {
                // Joga a menor carta  
                escolha = JogarCartaMaisBaixa1(jogadoresInfos[Id].Cartas);
            }
            else if (pontos >= 2 && pontos <= 4)
            {
                // Joga a carta do meio
                escolha = JogarCartaDoMeio(jogadoresInfos[Id].Cartas);
            }
            else if (pontos > 4)
            {
                // Joga a maior carta
                escolha = JogarCartaMaisAlta(jogadoresInfos[Id].Cartas);
            }

            jogadoresInfos[Id].Cartas.Remove(escolha);

            return escolha;
        }

        private string JogarCartaMaisBaixa1(List<string> cartas)
        {
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).First();
        }

        private string JogarCartaDoMeio(List<string> cartas)
        {
            int meio = cartas.Count / 2;
            return cartas.OrderBy(c => int.Parse(c.Split(',')[0])).ElementAt(meio);
        }

        private string JogarCartaMaisAlta(List<string> cartas)
        {
            return cartas.OrderByDescending(c => int.Parse(c.Split(',')[0])).First();
        } /*End of Code 3*/

        private void AtualizarCartasMao(string[] JogadasAtuais)
        {
            // Atualiza as cartas na mão do jogador com base nas jogadas feitas
            if (JogadasAtuais.Length != 0)
            {
                if(JogadasAtuais[0] != "")
                {
                    foreach (var jogada in JogadasAtuais)
                    {
                        string[] aux = jogada.Split(',');
                        string juncao = aux[4] + "," + aux[2];

                        // Remover a carta jogada da mão do jogador
                        if (jogadoresInfos.ContainsKey(aux[1]))
                        {
                            foreach (string item in jogadoresInfos[aux[1]].Cartas)
                            {
                                if (item == juncao)
                                {
                                    jogadoresInfos[aux[1]].Cartas.Remove(item);
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
            }
        }

        private string ConferirCarta(string[] cartas, int opcao)
        {
            if (opcao == -1) opcao++;
            if (opcao == 0) opcao++;

            // Conferir uma carta com base na opção fornecida
            return cartas[cartas.Length - opcao];
        }

        private string ConferirCarta(JogadorInfo cartas, int opcao)
        {
            return cartas.Cartas[cartas.Cartas.Count() - 1 - opcao];
        }

        private bool TemONaipe(string[] jogadasAtuais)
        {
            // Definir o array `possibilidades` e retornar true se houver cartas com o naipe requerido
            string[] dadosJogadasAtuais0 = jogadasAtuais[jogadasAtuais.Length - 1].Split(',');
            string rodadaAtual = dadosJogadasAtuais0[0];
            string naipeRequerido = dadosJogadasAtuais0[2];
            bool primeirajogadadoRound = true;

            if(jogadasAtuais.Length > 1)
            {
                foreach (string rodadasAtuias in jogadasAtuais)
                {
                    string[] auxiliar = rodadasAtuias.Split(',');
                    if (auxiliar[0] == rodadaAtual && primeirajogadadoRound)
                    {
                        naipeRequerido = auxiliar[2];
                        primeirajogadadoRound = false;
                    }
                }
            }

            List<string> possibilidade = new List<string>();

            JogadorInfo jogadorInfo = jogadoresInfos[Id];

            if (jogadorInfo != null && jogadorInfo.Cartas != null)
            {
                foreach (var carta in jogadorInfo.Cartas)
                {
                    string[] aux = carta.Split(',');
                    if (aux[1] == naipeRequerido)
                    {
                        possibilidade.Add(carta);
                    }
                }
            }
            possibilidades = possibilidade.ToArray();
            return possibilidade.Count > 0;
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
                string[] auxiliar = jogada.Split(',');
                if (auxiliar[2] == primeiroNaipe)
                {
                    i++;
                }
            }
            return i == jogadasAtuais.Length;
        }
    }
}
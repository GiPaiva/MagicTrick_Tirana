﻿using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static Lobby;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        //Props
        public string Versao { get; set; }
        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }
        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }

        //Instancias
        Tratamento r = new Tratamento();
        Lobby lobby = new Lobby();
        Cartas c;
        Bot1 b;

        //Variavel de estado do Jogo
        public string estado = "A";
        public bool apostar = true;
        public int jogadas = 0;
        public bool vez = false;
        public int[] pontos = {0,0,0,0};

        int vezesESuaVez = 0;


        //Inicializador
        public Partida()
        {
            InitializeComponent();
        }

        public Task AtualizarTela()
        {
            lblVersao2.Text = Versao;
            _ = ReloAsync();
            c = new Cartas(this);
            b = new Bot1(this);
            return Task.CompletedTask;
        }

        //Verificação de pessoas na Partida
        public async Task ReloAsync()
        {
            List<Label> lista = new List<Label> { lblTotalP1, lblTotalP2, lblTotalP3, lblTotalP4 };
            int quantidade = 0;

            while (quantidade < 5)
            {
                lobby.LobbyListarJogadores(PartidaSelecionada);
                if (estado == "A")
                {
                    lblQJ.Visible = true;
                    lblQJogadores.Visible = true;

                    lblQJ.Text = Convert.ToString(quantidade);

                    if (quantidade != lobby.Jogadores.Length)
                    {
                        JogadoresAtuais = lobby.Jogadores;
                        lblParticipantes.Text = "";
                        foreach (string JogadoresAtuais in JogadoresAtuais)
                        {
                            lblParticipantes.Text += JogadoresAtuais + "\n\r";
                        }
                    }
                    quantidade = JogadoresAtuais.Length;
                }
                else
                {
                    lblQJ.Visible = false;
                    lblParticipantes.Visible = false;
                    int i = 0;
                    foreach (string JogadoresAtuais in JogadoresAtuais)
                    {
                        string[] aux = JogadoresAtuais.Split(',');
                        lista[i].Text = aux[2];
                        i++;
                    }
                }

                await Task.Delay(6000);
            }

        }

        public async Task VerificarVez()
        {
            int TamRetorno = 1;
            int jogadas = 0;

            while (estado == "J")
            {
                
                int idPartida = Convert.ToInt32(PartidaAtual[0]);
                string retorno = Jogo.VerificarVez2(idPartida);

                // status da partida , id do jogador da vez, numero da rodada, status da rodada
                string[] DadosRetorno = r.TratarDadosEmArray(retorno);

                //Colocando o nome do jogador da vez
                string[] InfoRetorno = DadosRetorno[0].Split(',');
                estado = InfoRetorno[0];

                //Adicionando Ponto
                if (jogadas == JogadoresAtuais.Length)
                {
                    _ = AdicionarPontoAsync(InfoRetorno[1]);
                }

                foreach (string itens in JogadoresAtuais)
                {
                    string[] infoJogador = itens.Split(',');
                    if (infoJogador[0] == InfoRetorno[1])
                    {
                        lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                    }
                }
                string[] InfoJogador = Jogador.Split(',');
                if (InfoRetorno[1] == InfoJogador[0] && InfoRetorno[3] == "C" && vezesESuaVez == 0)
                {
                    vezesESuaVez++;
                    vez = true;
                    MessageBox.Show("É a sua vez");
                    string res = b.Jogar(Convert.ToInt32(InfoRetorno[2]));
                    this.Jogar(res);
                }
                else
                {
                    vez = false;
                }

                //Varificando a Carta Jogada
                if (DadosRetorno.Length != TamRetorno && DadosRetorno.Length > 1)
                {
                    TamRetorno = DadosRetorno.Length;
                    if (TamRetorno > 1)
                    {
                        string[] InfoRetornoJogada = DadosRetorno[TamRetorno - 1].Split(',');
                        string[] aux = InfoRetornoJogada[0].Split(':');

                        //Colocar carta no meio da mesa
                        if (aux[0] == "C")
                        {
                            //idJogador, naipe, valorDaCarta, posicao
                            c.VerificarJogadaDosPlayers(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3]);
                        }
                        //Aposta
                        else
                        {
                            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                            int posicao = c.localNaMesaCadaJogador[aux[1].Trim()];
                            labels[posicao].Text = Convert.ToString(InfoRetornoJogada[2]);
                        }
                    }
                }

                //Verificando Quantas jogadas Feitas
                jogadas = 0;
                foreach (var joga in c.cartasJogadas)
                {
                    jogadas++;
                }

                await Task.Delay(8000);
            }
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            //tmrDoBot.Enabled = true; Liga o timer do Bot
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);

            for(int i = 0;  i < JogadoresAtuais.Length; i++)
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

        private async void btnConsultarMao_Click(object sender, EventArgs e)
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = "J";
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
            await VerificarVez();
        }

        private async void ConsultarMao()
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = "J";
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
            //await Task.Delay(8000);
            await VerificarVez();
        }


        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
        {
            bool primeiro = true;
            c.localNaMesaCadaJogador.Clear();
            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux = JogadoresAtuais[i].Split(',');

                if (aux[0] != idDoJogador)
                {
                    if (i == 0)
                    {
                        i++;
                        primeiro = false;
                    }

                    c.MostrarCartas(aux, DadosConsultarMao, i, "player");

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    c.MostrarCartas(aux, DadosConsultarMao, 0, idDoJogador);
                }
            }
        }

        private void Jogar(string res)
        {
            int posicao = Convert.ToInt32(res);

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            //Posição
            string retorno = Jogo.Jogar(IdJogador, DadosJogador[1], posicao);
        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            //IdJogador | senhaJogador | posição
            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(Dadoslist[0]);
            string retorno = Jogo.Jogar(IdJogador, DadosJogador[1], posicao);
            
            if (!r.Error(retorno))
            {
                //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);

                if (apostar)
                {
                    int valorPosicao = b.Apostar();
                    string retono = Jogo.Apostar(IdJogador, DadosJogador[1], valorPosicao);

                    if (valorPosicao != 0)
                    {
                        Apostar(retono, IdJogador);
                        apostar = false;
                    }

                    //DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                    //if (decisao == DialogResult.Yes)
                    //{
                    //    Apostar();
                    //    apostar = false;
                    //}
                    //else
                    //{
                    //    Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                    //    MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                    //}
                }
            }
            //else
            //{
            //    MessageBox.Show("Tente Novamente", "", MessageBoxButtons.OK);
            //    btnConsultarMao_Click(sender, e);
            //}
            vezesESuaVez = 0;

        }
        private void Apostar(string retorno, int IdJogador)
        {
            //btnApostar.Visible = true;
            //if (lsbPlayer1.SelectedItem == null)
            //{
            //    MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };

            int aux = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
            labels[aux].Text = retorno;
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
            string[] DadosJogador = Jogador.Split(',');
            if (lsbPlayer1.SelectedItem != null)
            {
                string list = lsbPlayer1.Text;
                string[] Dadoslist = list.Split('|');

                //Posição
                int posicao = Convert.ToInt32(Dadoslist[0]);
                int IdJogador = Convert.ToInt32(DadosJogador[0]);


                string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
                int aux = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
                labels[aux].Text = retorno;
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

            }
            else
            {
                //Apostar();
            }
        }

        private async Task AdicionarPontoAsync(string idJogador)
        {
            List<Label> labels = new List<Label> {lblPontos, lblPontoP2, lblPontosP3, lblPontosP4};

            int aux = c.localNaMesaCadaJogador[idJogador.Trim()];
            pontos[aux] += 1;
            labels[aux].Text = Convert.ToString(pontos[aux]);

            await Task.Delay(2000);
            foreach (var cartaMeio in c.cartasJogadas)
            {
                cartaMeio.Key.Visible = false;
                cartaMeio.Value.Visible = false;
            }
            c.cartasJogadas.Clear();
        }

        private void tmrDoBot_Tick(object sender, EventArgs e) //Aqui vai ligar o bot
        {
            //tmrDoBot.Enabled = false; //Coloca o timer pra ele comprir suas açoes 
                                        //Chama o Bot
            //tmrDoBot.Enabled = true;  //Coloca o timer para ele dps de x tempo ele voltar a retornar suas ações
        }
    }
}

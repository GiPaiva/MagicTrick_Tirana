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

        //Variavel de estado do Jogo
        public bool estado = false;
        public bool apostar = true;
        public int jogadas = 0;
        

        //Inicializador
        public Partida()
        {
            InitializeComponent();
            c = new Cartas(this);
        }

        public Task AtualizarTela()
        {
            lblVersao2.Text = Versao;
            _ = ReloAsync();
            return Task.CompletedTask;
        }

        //Verificação de pessoas na Partida
        public async Task ReloAsync()
        {
            int quantidade = 0;
            while (!estado && quantidade < 5)
            {
                lblQJ.Visible = true;
                lblQJogadores.Visible = true;

                lblQJ.Text = Convert.ToString(quantidade);

                lobby.LobbyListarJogadores(PartidaSelecionada);
                if(quantidade != lobby.Jogadores.Length)
                {
                    JogadoresAtuais = lobby.Jogadores;
                    lblParticipantes.Text = "";

                    foreach (string JogadoresAtuais in JogadoresAtuais)
                    {
                        lblParticipantes.Text += JogadoresAtuais + "\n\r";
                    }
                }
                quantidade = JogadoresAtuais.Length;

                await Task.Delay(6000);
            }

            lblQJ.Visible = false;
            lblQJogadores.Visible = false;
            lblParticipantes.Visible = false;
        }

        public async Task VerificarVez()
        {
            string auxx = "a, b, f, g";
            int quantidadeDeJogadas = 0;
            int quant = 1;
            while (estado)
            {
                string retorno = Jogo.VerificarVez(Convert.ToInt32(PartidaAtual[0]));

                if (!r.Error(retorno))
                {
                    string[] auxxx = auxx.Split(',');
                    string[] Dados = retorno.Split(',');

                    if (auxxx[3] != "C" && ( auxx != "a, b, f, g" && auxx != retorno))
                    {
                        quantidadeDeJogadas = c.VerificarJogadaDosPlayers(auxxx[1]).Length;
                    }

                    if (Dados[3].Trim() == "C" && (quant != quantidadeDeJogadas && quant != 0))
                    {

                        quant = quantidadeDeJogadas;
                        jogadas++;
                        foreach (string jogador in JogadoresAtuais)
                        {
                            string[] aux = jogador.Split(',');
                            if (aux[0] == Dados[1])
                            {
                                if (jogadas == JogadoresAtuais.Length)
                                {
                                    jogadas = 0;
                                    //MessageBox.Show($"{aux[1]} venceu a jogada", "Vencedor da Jogada");
                                    AdicionarPonto(Dados[1]);

                                    List<Panel> panelCartasMeio = new List<Panel> { pnlCartaP1, pnlCartaP2, pnlCartaP3, pnlCartaP4 };
                                    List<Label> labelCartasMeio = new List<Label> { lblCartaP1, lblCartaP2, lblCartaP3, lblCartaP4 };

                                    for (int i = 0; i < 4; i++)
                                    {
                                        panelCartasMeio[i].Visible = false;
                                        labelCartasMeio[i].Visible = false;
                                    }

                                }
                                else
                                {
                                    MessageBox.Show(
                                        $"vez do jogador: {aux[1]} ",
                                        "vez do jogador",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information
                                    );
                                }

                                lblQJogadores.Visible = true;
                                lblQJogadores.Text = "Vez: " + aux[1];

                            }
                        }
                    }
                      
                }
                auxx = retorno;
                await Task.Delay(6000);
            }
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
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
            estado = true;
            btnComecar.Visible = false;
        }

        private async void btnConsultarMao_Click(object sender, EventArgs e)
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = true;
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
            await VerificarVez();
        }

        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
        {
            bool primeiro = true;
            c.local.Clear();
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
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);

                if (apostar)
                {
                    DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                    if (decisao == DialogResult.Yes)
                    {
                        Apostar();
                        apostar = false;
                    }
                    else
                    {
                        Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                        MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                MessageBox.Show("Tente Novamente", "", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);
            }
        }

        private void Apostar()
        {
            btnApostar.Visible = true;
            if (lsbPlayer1.SelectedItem == null)
            {
                MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                int aux = c.local[Convert.ToString(IdJogador)];
                labels[aux].Text = retorno;
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

            }
            else
            {
                Apostar();
            }
        }

        private void AdicionarPonto(string idJogador)
        {
            List<Label> labels = new List<Label> {lblPontos, lblPontoP2, lblPontosP3, lblPontosP4};

            int aux = c.local[idJogador.Trim()];
            string aux2 = labels[aux].Text;
            int aux3 = Convert.ToInt32(aux2);
            int aux4 = aux3 + 1;
            labels[aux].Text = Convert.ToString(aux4);
        }

    }
}

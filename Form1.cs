using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MagicTrickServer;

namespace MagicTrick_Tirana
{
    public partial class Form1 : Form
    { 
        private Lobby lobby = new Lobby();

        private bool estado = false;

        public Form1()
        {
            InitializeComponent();
            lblVersao.Text = "Version: " + Jogo.Versao;
            lblGrupoProjeto.Text = "Grupo: Tirana";
        }

        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
            pnlCriarPartida.Visible = false;
            pnlEntrarNaPartida.Visible = false;
            pnlListar.Visible = true;

            if (lobby.LobbyListarPartidas())
            {
                lstPartidas.Items.Clear();
                lstJogadores.Items.Clear();
                for (int i = 0; i < lobby.Partidas.Length; i++)
                {
                    lstPartidas.Items.Add(lobby.Partidas[i]);
                }
                estado = true;
            }
            else
            {
                lstPartidas.Items.Add("PARTIDA NÃO ENCONTRADA");
                lstPartidas.SetSelected(0, false);
                lstJogadores.Visible = false;
                lblJogadoresTitulo.Visible = false;
            }

        }

        private void lstPartidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJogadores.Visible = true;
            lblJogadoresTitulo.Visible = true;
            lstJogadores.Items.Clear();
            string PartidaSelecionada = "";

            if (estado)
            {
                PartidaSelecionada = lstPartidas.SelectedItem.ToString();

                if (lobby.LobbyListarJogadores2(PartidaSelecionada) && PartidaSelecionada != "")
                {
                    for (int i = 0; i < lobby.Jogadores.Length; i++)
                    {
                        lstJogadores.Items.Add(lobby.Jogadores[i]);
                    }
                }
                else
                {
                    lstJogadores.Items.Add("Não há Jogadores na partida: ");
                    lstJogadores.Items.Add(Lobby.Partida.NomePartida);
                }
            }
            
        }

        private void btnCriarPartida_Click(object sender, EventArgs e)
        {
            pnlListar.Visible = false;
            pnlEntrarNaPartida.Visible = false;
            pnlCriarPartida.Visible = true;
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            string NomePartida = txtNomePartida.Text;
            string SenhaPartida = txtSenha.Text;

            if (lobby.LobbyCriarPartida(NomePartida, SenhaPartida))
            {
                txtNomePartida.Clear();
                txtSenha.Clear();
                btnListarPartidas_Click(sender, e);
            }
        }

        private void btnEntrarPartida_Click(object sender, EventArgs e)
        {
            pnlCriarPartida.Visible = false;
            pnlListar.Visible = true;
            pnlEntrarNaPartida.Visible = true;
            if (lstPartidas.SelectedItem == null)
            {
                MessageBox.Show($"Selecione uma partida", "Selecionar Partida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            if (lstPartidas.SelectedItem != null)
            {
                string PartidaEscolhida = lstPartidas.SelectedItem.ToString();
                string NomeDoJogador = txtNomeJogador.Text;
                string SenhaDaPartida = txtSenhaDaPartida.Text;
                string[] DadosPartida = PartidaEscolhida.Split(',');

                int IdPartida = Convert.ToInt32(DadosPartida[0]);
                string NomePartida = DadosPartida[1];

                string retorno = lobby.LobbyEntrarPartida(IdPartida, NomeDoJogador, SenhaDaPartida, NomePartida);
                if(retorno != "")
                {
                    txtNomeJogador.Clear();
                    txtSenhaDaPartida.Clear();

                    lobby.LobbyListarJogadores2(PartidaEscolhida);

                    Partida Partida = new Partida();
                    Partida.Versao = Jogo.Versao;
                    Partida.Jogador = retorno;
                    Partida.JogadoresAtuais = lobby.Jogadores;
                    Partida.PartidaSelecionada = PartidaEscolhida;
                    Partida.PartidaAtual = DadosPartida;
                    Partida.AtualizarTela();
                    Partida.Show();
                }
            }
            else
            {
                btnEntrarPartida_Click(sender, e);
            }
        }

        private void btnTeste_Click(object sender, EventArgs e)
        {
            Partida Partida = new Partida();
            Partida.Versao = Jogo.Versao;
            Partida.AtualizarTela();
            Partida.Show();
        }
    }
}
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
        public Form1()
        {
            InitializeComponent();
            lblVersao.Text = "Version: " + Jogo.Versao;
            lblGrupoProjeto.Text = "Grupo: Tirana";
        }

        private Tratamento r = new Tratamento();

        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
            pnlCriarPartida.Visible = false;
            pnlEntrarNaPartida.Visible = false;
            pnlListar.Visible = true;

            string BuscarPartidas = Jogo.ListarPartidas("T");
            if(BuscarPartidas != "")
            {
                if (BuscarPartidas.Substring(0, 4) == "ERRO")
                {
                    //Tratamento de erro
                    r.Error(BuscarPartidas);
                }
                else
                {
                    //Fazer tratamento de dados
                    string[] Partidas = r.TratarDadosEmArray(BuscarPartidas);

                    lstPartidas.Items.Clear();
                    lstJogadores.Items.Clear();
                    for (int i = 0; i < Partidas.Length; i++)
                    {
                        lstPartidas.Items.Add(Partidas[i]);
                    }
                }
            }
            else
            {
                lstPartidas.Items.Add("NENHUMA PARTIDA ENCONTRADA");
                lstJogadores.Visible = false;
                lblJogadoresTitulo.Visible = false;
            }
            
        }

        private void lstPartidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            lstJogadores.Items.Clear();

            string Partida = lstPartidas.SelectedItem.ToString();
            if (Partida.Substring(0, 4) == "ERRO")
            {
                //Tratamento de erro
                r.Error(Partida);
            }
            else
            {
                if(Partida.IndexOf(',') != -1)
                { 
                    string[] DadosPartida = Partida.Split(',');

                    int IdPartida = Convert.ToInt32(DadosPartida[0]);
                    string NomePartida = DadosPartida[1];
                    string DataPartida = DadosPartida[2];
                    string StatusPartida = DadosPartida[3];

                    string JogadoresDaPartida = Jogo.ListarJogadores(IdPartida);
                    if (JogadoresDaPartida.Length == 0)
                    {
                        lstJogadores.Items.Add("Não há Jogadores na partida: ");
                        lstJogadores.Items.Add(NomePartida);
                    }
                    else
                    {
                        string[] Jogadores = r.TratarDadosEmArray(JogadoresDaPartida);

                        for (int i = 0; i < Jogadores.Length; i++)
                        {
                            lstJogadores.Items.Add(Jogadores[i]);
                        }
                    }
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

            string retorno = Jogo.CriarPartida(NomePartida, SenhaPartida, "Tirana");

            if (retorno.Length > 4 && retorno.Substring(0,4) == "ERRO")
            {
                r.Error(retorno);
            }

            else
            {
                MessageBox.Show("Partida Criada com Sucesso!\nId da Partida: " + retorno, "Partida Criada", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string NomePartida = DadosPartida[1];

                int IdPartida = Convert.ToInt32(DadosPartida[0]);

                string retorno = Jogo.EntrarPartida(IdPartida, NomeDoJogador, SenhaDaPartida);
                if (retorno.Substring(0, 4) == "ERRO")
                {
                    r.Error(retorno);
                }
                else
                {
                    MessageBox.Show($"{NomeDoJogador} entrou na partida: \r\n{NomePartida}! \r\n IdJogador: {retorno}", "Jogador Entrou", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNomeJogador.Clear();
                    txtSenhaDaPartida.Clear();

                    Partida Partida = new Partida();
                    Partida.Versao = Jogo.Versao;
                    Partida.Jogadores = retorno;
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
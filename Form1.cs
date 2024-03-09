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
            if(BuscarPartidas.Substring(0,4) == "ERRO")
            {
                //Tratamento de erro
                r.Error(BuscarPartidas);
            }
            else
            {
                //Fazer tratamento de dados
                string[] Partidas = r.TratarDados(BuscarPartidas);

                lstPartidas.Items.Clear();
                lstJogadores.Items.Clear();
                for (int i = 0; i < Partidas.Length - 1; i++)
                {
                    lstPartidas.Items.Add(Partidas[i]);
                }
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
                    string[] Jogadores = r.TratarDados(JogadoresDaPartida);

                    for (int i = 0; i < Jogadores.Length; i++)
                    {
                        lstJogadores.Items.Add(Jogadores[i]);
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

            if(retorno.Substring(0,4) == "ERRO")
            {
                r.Error(retorno);
            }
            else
            {
                MessageBox.Show("Partida Criada com Sucesso!\nId da Partida", "Partida Criada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnListarPartidas_Click(sender, e);
                txtNomePartida.Clear();
                txtSenha.Clear(); 
            }
        }

        private void btnEntrarPartida_Click(object sender, EventArgs e)
        {
            pnlCriarPartida.Visible = false;
            pnlListar.Visible = true;
            pnlEntrarNaPartida.Visible = true;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            string Partida = lstPartidas.SelectedItem.ToString();
            string[] DadosPartida = Partida.Split(',');

            string NomePartida = DadosPartida[1];

            int IdPartida = Convert.ToInt32(DadosPartida[0]);

            string NomeDoJogador = txtNomeJogador.Text;
            string SenhaDaPartida = txtSenhaDaPartida.Text;

            string retorno = Jogo.EntrarPartida(IdPartida, NomeDoJogador, SenhaDaPartida);
            if (retorno.Substring(0, 4) == "ERRO")
            {
                r.Error(retorno);
            }
            else
            {
                MessageBox.Show($"{NomeDoJogador} entrou na partida: \r\n{NomePartida}! \r\n IdJogador: {retorno}", "Jogador Entrou", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnListarPartidas_Click(sender, e);
                txtNomeJogador.Clear();
                txtSenhaDaPartida.Clear();
            } 
        }
    }
}
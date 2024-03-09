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

        private Tratamento retorno = new Tratamento();

        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
            pnlCriarPartida.Visible = false;
            pnlListar.Visible = true;

            string BuscarPartidas = Jogo.ListarPartidas("T");
            if(BuscarPartidas.Substring(0,4) == "ERRO")
            {
                //Tratamento de erro
                retorno.Error(BuscarPartidas);
            }
            else
            {
                //Fazer tratamento de dados
                string[] Partidas = retorno.TratarDados(BuscarPartidas);

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
                string[] Jogadores = retorno.TratarDados(JogadoresDaPartida);

                for(int i = 0; i < Jogadores.Length; i++)
                {
                    lstJogadores.Items.Add(Jogadores[i]);
                }
            }

        }

        private void btnCriarPartida_Click(object sender, EventArgs e)
        {
            pnlListar.Visible = false;
            pnlCriarPartida.Visible = true;

            
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            Jogo.CriarPartida(txtNomePartida.Text, txtSenha.Text, "Tirana");
        }
    }
}

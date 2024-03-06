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

        private void btnListarPartidas_Click(object sender, EventArgs e)
        {
            lblPartidasTitulo.Visible = true;
            lstPartidas.Visible = true;

            string BuscarPartidas = Jogo.ListarPartidas("T");
            BuscarPartidas = BuscarPartidas.Replace("\r", "");
            BuscarPartidas = BuscarPartidas.Substring(0, BuscarPartidas.Length - 1);
            string[] Partidas = BuscarPartidas.Split('\n');

            lstPartidas.Items.Clear();
            lstJogadores.Items.Clear();
            for (int i = 0; i < Partidas.Length - 1; i++)
            {
                lstPartidas.Items.Add(Partidas[i]);
            }

            lblJogadoresTitulo.Visible = true;
            lstJogadores.Visible = true;
        }

        private void lstPartidas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Partida = lstPartidas.SelectedItem.ToString();
            string[] DadosPartida = Partida.Split(',');

            int IdPartida = Convert.ToInt32(DadosPartida[0]);
            string NomePartida = DadosPartida[1];
            string DataPartida = DadosPartida[2];
            string StatusPartida = DadosPartida[3];

            string JogadoresDaPartida = Jogo.ListarJogadores(IdPartida);
            JogadoresDaPartida = JogadoresDaPartida.Replace("\r", "");
            string[] Jogadores = JogadoresDaPartida.Split('\n');

            lstJogadores.Items.Clear();
            for(int i = 0; i < Jogadores.Length; i++)
            {
                lstJogadores.Items.Add(Jogadores[i]);
            }

        }
    }
}

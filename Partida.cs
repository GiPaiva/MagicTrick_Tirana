using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        public string Versao { get; set; }
        public string Jogadores { get; set; }
        public string[] PartidaAtual { get; set; }

        public Random rnd = new Random();
        public int primeiro;


        private Tratamento r = new Tratamento();

        public Partida()
        {
            InitializeComponent();
        }

        public void AtualizarTela()
        {
            lblVersao2.Text = Versao;
            string [] Jogadoress = Jogadores.Split(',');
        }

        public void JogoAtual(int primeiro, string[] JogadoresAtuais)
        { 
            //if (primeiro == 4) primeiro = 0;
            //else primeiro++;
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogo.ListarJogadores(Convert.ToInt32(PartidaAtual[0])));
            string[] Jogadoress = Jogadores.Split(',');
            int IdJogador = Convert.ToInt32(Jogadoress[0]);

            string retorno = Jogo.IniciarPartida(IdJogador, Jogadoress[1]);

            for(int i = 0;  i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
        }
    }
}

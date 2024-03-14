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
            primeiro = rnd.Next(0,4);
            string[] JogadoresAtuais = r.TratarDados(Jogadores);
            JogoAtual(primeiro, JogadoresAtuais);
        }

        public void JogoAtual(int primeiro, string[] JogadoresAtuais)
        { 
            string[] PrimeiroJogador = JogadoresAtuais[primeiro].Split(',');
            MessageBox.Show($"O primeiro Jogador é \nId:{PrimeiroJogador[0]} \nNome:{PrimeiroJogador[1]}", "Jogada", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (btnJ_Click())
            {
                if (primeiro == 4) primeiro = 0;
                else primeiro++;
            }
        }

        private bool btnJ_Click()
        {
            return true;
        }
    }
}

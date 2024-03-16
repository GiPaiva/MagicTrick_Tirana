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
            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogadores);
            primeiro = rnd.Next(0,JogadoresAtuais.Length);
            string[] PrimeiroJogador = JogadoresAtuais[primeiro].Split(',');
            JogoAtual(primeiro, JogadoresAtuais);
        }

        public void JogoAtual(int primeiro, string[] JogadoresAtuais)
        { 
            //if (primeiro == 4) primeiro = 0;
            //else primeiro++;
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogadores);
            string[] PrimeiroJogador = JogadoresAtuais[primeiro].Split(',');
            MessageBox.Show($"O primeiro Jogador é \nId:{PrimeiroJogador[0]} \nNome:{PrimeiroJogador[1]}", "Jogada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Partida_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogadores);
            for(int i = 0; i < JogadoresAtuais.Length; i++)
            {
                lstJogadores.Items.Add(JogadoresAtuais[i]);
            }
        }
    }
}

using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        public string Versao { get; set; }
        public string Jogador { get; set; }

        public string[] PartidaAtual { get; set; }

        private Tratamento r = new Tratamento();

        public Partida()
        {
            InitializeComponent();
            PictureBox[] cards = new PictureBox[]
            {
                pictureBox1, 
                pictureBox2, 
                pictureBox3, 
                pictureBox4, 
                pictureBox5, 
                pictureBox6, 
                pictureBox7, 
                pictureBox8, 
                pictureBox9, 
                pictureBox10, 
                pictureBox11, 
                pictureBox12, 
                pictureBox13, 
                pictureBox14
            };
        }

        public void AtualizarTela()
        {
            lblVersao2.Text = Versao;
        }


        private void btnComecar_Click(object sender, EventArgs e)
        {
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);
            
            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogo.ListarJogadores(Convert.ToInt32(PartidaAtual[0])));

            for(int i = 0;  i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
        }

        private void btnConsultarMao_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');

            listBox1.Items.Add("Posição | Naipe");
            for (int i = 0; i < DadosConsultarMao.Length; i++)
            {
                string[] aux = DadosConsultarMao[i].Split(',');
                if (aux[0] == DadosJogador[0])
                {
                    listBox1.Items.Add(aux[1] + " | " + aux[2]);
                }
            }

            MostrarGalera(DadosJogador[0], DadosConsultarMao);
        }

        private void MostrarGalera(string idDoJogador, string[] DadosConsultarMao)
        {
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox5.Items.Clear();

            string[] JogadoresAtuais = r.TratarDadosEmArray(Jogo.ListarJogadores(Convert.ToInt32(PartidaAtual[0])));

            List<GroupBox> list = new List<GroupBox> { grpBox1, groupBox2, groupBox1, groupBox3 };
            List<ListBox> list2 = new List<ListBox> { listBox1, listBox2, listBox5, listBox3 };

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux = JogadoresAtuais[i].Split(',');

                if (aux[0] != idDoJogador)
                {
                    list[i].Text = aux[1];
                    list2[i].Items.Add("Posição | Naipe");
                    for (int j = 0; j < DadosConsultarMao.Length; j++)
                    {
                        string[] aux2 = DadosConsultarMao[j].Split(',');
                        if (aux2[0] == aux[0])
                        {
                            list2[i].Items.Add(aux2[1] + " | " + aux2[2]);
                        }
                    }
                }
                else
                {
                    list[0].Text = aux[1];
                }
            }
        }
        
        private void MostrarCartas()
        {

        }

        private void btnJogar_Click(object sender, EventArgs e)
        {
            //IdJogador | senhaJogador | posição

            string list = listBox1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(Dadoslist[0]);

            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            string retorno = Jogo.Jogar(IdJogador, DadosJogador[1], posicao);
            if (!r.Error(retorno))
            {
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);
                btnConsultarMao_Click(sender, e);


                DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                if (decisao == DialogResult.Yes)
                {
                    Apostar();
                }
                else
                {
                    Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                    MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
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
            if (listBox1.SelectedItem == null)
            {
                MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            string[] DadosJogador = Jogador.Split(',');
            if (listBox1.SelectedItem != null)
            {
                string list = listBox1.Text;
                string[] Dadoslist = list.Split('|');

                //Posição
                int posicao = Convert.ToInt32(Dadoslist[0]);
                int IdJogador = Convert.ToInt32(DadosJogador[0]);


                string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

            }
            else
            {
                Apostar();
            }
        }
    }
}

using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        //Inicializador
        public Partida()
        {
            InitializeComponent();
            tmrVerificarVez.Enabled = true;
            c = new Cartas(this);
        }

        public void AtualizarTela()
        {
            lblVersao2.Text = Versao;
            IdPartida = Convert.ToInt32(PartidaAtual[0]);
            DadosJogador = Jogador.Split(',');
            idJogador = DadosJogador[0];
            VerficarJogadores();
        }

        private void tmrVerificarVez_Tick(object sender, EventArgs e)
        {
            tmrVerificarVez.Enabled = false;

            VerficarJogadores();

            if (estado.Trim() == "J")
            {
                VerificarVez();
                tmrVerificarVez.Interval = 6000;
            }
            else if (estado.Trim() == "F")
            {
                VerificarQuemGanhou();
            }

            if (!acabou)
                tmrVerificarVez.Enabled = true;
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            MesaComercar();
            estado = "J";
        }

        private void btnJogar_Click_1(object sender, EventArgs e)
        {
            //IdJogador e Senha
            int IdJogador = Convert.ToInt32(idJogador);
            string senha = DadosJogador[1];
            resposta = bot.Jogar(VerificarJogadasArray, VerificarJogadasNoRoundAtualArray.ToArray());

            //IdJogador | senhaJogador | posição
            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            string[] aux = resposta.Split(',');
            label3.Text = resposta;
            int posicao = Convert.ToInt32(aux[0]);
            string retorno = Jogo.Jogar(IdJogador, senha, posicao);
            lsbPlayer1.Text = "";
            if (!t.Error(retorno))
            {
                //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

                if (apostar)
                {
                    DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                    if (decisao == DialogResult.Yes)
                    {
                        MesaApostar();
                        apostar = false;
                    }
                    else
                    {
                        _ = Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                        //MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
            string[] DadosJogador = Jogador.Split(',');

            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(Dadoslist[0]);
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
            posicaoDoJogador = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
            labels[posicaoDoJogador].Text = retorno;
            //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);,
            //ConsultarMao();
            
        }
    }
}

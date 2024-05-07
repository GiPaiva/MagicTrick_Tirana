using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static Lobby;


namespace MagicTrick_Tirana

{
    public partial class Partida : Form
    {
        //Props
        public string Versao { get; set; }
        public string Jogador { get; set; }
        public string[] JogadoresAtuais { get; set; }
        public string[] PartidaAtual { get; set; }
        public string PartidaSelecionada { get; set; }

        //Instancias
        Tratamento t = new Tratamento();
        Lobby lobby = new Lobby();
        Mesa mesa;

        //Variavel de estado do Jogo
        public string estado = "A";
        public bool apostar = true;
        public int jogadas = 0;
        public bool vez = false;
        public int[] pontos = {0,0,0,0};
        int suaVez = 0;

        public Dictionary<string, string[]> cartinhasDoJogadorAtual = new Dictionary<string, string[]>();
        public Dictionary<string, int> localNaMesaCadaJogador = new Dictionary<string, int>();
        public Dictionary<string, string> NaipesDasCrtasEImagens = new Dictionary<string, string>();
        public Dictionary<Panel, Label> cartasJogadas = new Dictionary<Panel, Label>();

        public List<List<Panel>> panelsDasCartasDeCadaJogador = new List<List<Panel>>();

        public List<string> posicoesCartasMao = new List<string>();

        //List<GroupBox> groupBoxes = new List<GroupBox> { grbPlayer1, grbPlayer2, grbPlayer3, grbPlayer4 };
        //List<ListBox> listBoxes = new List<ListBox> { lsbPlayer1, lsbPlayer2, lsbPlayer3, lsbPlayer4 };


        //Inicializador
        public Partida()
        {
            InitializeComponent();
        }

        public Task AtualizarTela()
        {
            lblVersao2.Text = Versao;
            _ = ReloAsync();
            return Task.CompletedTask;
        }

        //Verificação de pessoas na Partida
        public async Task ReloAsync()
        {
            List<Label> lista = new List<Label> { lblTotalP1, lblTotalP2, lblTotalP3, lblTotalP4 };
            List<Label> labels = new List<Label> { lblPontos, lblPontoP2, lblPontosP3, lblPontosP4 };

            int quantidade = 0;

            while (quantidade < 5)
            {
                if (estado == "A")
                {
                    lobby.LobbyListarJogadores(PartidaSelecionada);

                    lblQJ.Visible = true;
                    lblQJogadores.Visible = true;

                    lblQJ.Text = Convert.ToString(quantidade);

                    if (quantidade != lobby.Jogadores.Length)
                    {
                        JogadoresAtuais = lobby.Jogadores;
                        lblParticipantes.Text = "";
                        foreach (string JogadoresAtuais in JogadoresAtuais)
                        {
                            lblParticipantes.Text += JogadoresAtuais + "\n\r";
                        }
                    }
                    quantidade = JogadoresAtuais.Length;
                }
                else
                {
                    lobby.LobbyListarJogadores2(PartidaSelecionada);

                    lblQJ.Visible = false;
                    lblParticipantes.Visible = false;
                    int i = 0;
                    foreach (string JogadoresAtuais in JogadoresAtuais)
                    {
                        string[] aux = JogadoresAtuais.Split(',');
                        lista[i].Text = aux[2];
                        labels[i].Text = aux[3];
                        pontos[i] = Convert.ToInt32(aux[3]);
                        i++;
                    }
                }

                await Task.Delay(6000);
            }

        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            mesa = new Mesa();
            mesa.Comecar();
        }

        #region Botão Inutilizavel com bot
        private void btnJogar_Click(object sender, EventArgs e)
        {
            //IdJogador e Senha
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);

            //IdJogador | senhaJogador | posição
            string list = lsbPlayer1.Text;
            string[] Dadoslist = list.Split('|');

            //Posição
            int posicao = Convert.ToInt32(Dadoslist[0]);
            string retorno = Jogo.Jogar(IdJogador, DadosJogador[1], posicao);

            if (!t.Error(retorno))
            {
                //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);
                //ConsultarMao();

                if (apostar)
                {
                    //int valorPosicao = b.BotApostar();
                    //string retono = Jogo.Apostar(IdJogador, DadosJogador[1], valorPosicao);

                    //if (valorPosicao != 0)
                    //{
                    //    Apostar(retono, IdJogador);
                    //    apostar = false;
                    //}

                    //DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                    //if (decisao == DialogResult.Yes)
                    //{
                    //    Apostar();
                    //    apostar = false;
                    //}
                    //else
                    //{
                    //    Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                    //    MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                    //}
                }
            }
            suaVez = 0;

        }

        private void btnApostar_Click(object sender, EventArgs e)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
            string[] DadosJogador = Jogador.Split(',');
            if (lsbPlayer1.SelectedItem != null)
            {
                string list = lsbPlayer1.Text;
                string[] Dadoslist = list.Split('|');

                //Posição
                int posicao = Convert.ToInt32(Dadoslist[0]);
                int IdJogador = Convert.ToInt32(DadosJogador[0]);


                string retorno = Jogo.Apostar(IdJogador, DadosJogador[1], posicao);
                //int aux = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
                //labels[aux].Text = retorno;
                MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

            }
            else
            {
                //Apostar();
            }
        }
        #endregion

        
    }
}

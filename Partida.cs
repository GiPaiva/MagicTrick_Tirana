using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


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
        private readonly Tratamento t = new Tratamento();
        private readonly Lobby lobby = new Lobby();
        private readonly Cartas c;

        //Variavel de estado do Jogo
        //Strings
        public string estado = "A";
        private string alteracao = "";
        private string ganhador = "";

        //Arrays
        string[] VerificarJogadasArray = { };
        List<string> VerificarJogadasNoRoundAtualArray = new List<string>();
        
        //Ints
        public int rodada = 0;
        public int IdPartida;
        int posicaoDoJogador = 0;
        int cartasJogadas;

        //Bool
        private bool primeiraVez = true;
        public bool apostar = true;
        public bool vez = false;
        bool acabou = false;

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
            ReloAsync();
        }

        //Verificação de pessoas na Partida

        private void tmrVerificarVez_Tick(object sender, EventArgs e)
        {
            tmrVerificarVez.Enabled = false;

            ReloAsync();

            if (estado.Trim() == "J")
            {
                VerificarVez();
                tmrVerificarVez.Interval = 8000;
            }
            else if (estado.Trim() == "F")
            {
                VerificarQuemGanhou();
            }

            if (!acabou)
                tmrVerificarVez.Enabled = true;
        }

        private void VerificarQuemGanhou()
        {
            int maior = 0;
            int i = 0;
            ReloAsync();
            foreach (string JogadoresAtuais in JogadoresAtuais)
            {
                string[] aux = JogadoresAtuais.Split(',');
                int pontuacao = Convert.ToInt32(aux[2]);
                if (i == 0)
                {
                    maior = pontuacao;
                    ganhador = aux[1];
                    i++;
                }
                else if (pontuacao > maior)
                {
                    maior = pontuacao;
                    ganhador = aux[1];
                }
            }

            MessageBox.Show($"O Ganhador é: {ganhador} !!", "Parabéns!!", MessageBoxButtons.OK);
            tmrVerificarVez.Enabled = false;
            tmrVerificarVez.Stop();
            tmrVerificarVez.Dispose();
            acabou = true;
            this.Close();
        }

        public void ReloAsync()
        {
            int quantidade = 0;

            lobby.LobbyListarJogadores2(PartidaSelecionada);

            lblQJogadores.Visible = true;

            if (quantidade != lobby.Jogadores.Length)
            {
                JogadoresAtuais = lobby.Jogadores;
                lblParticipantes.Text = "";
                foreach (string JogadoresAtuais in JogadoresAtuais)
                {
                    lblParticipantes.Text += JogadoresAtuais + "\n\r";
                }
            }
        }

        public void VerificarVez()
        {
            string retorno = Jogo.VerificarVez2(IdPartida);

            //label6.Text = retorno;

            string[] DadosRetornoVez = t.TratarDadosEmArray(retorno);
            string[] InfoRetorno = DadosRetornoVez[0].Split(',');
            estado = InfoRetorno[0];
            rodada = Convert.ToInt32(InfoRetorno[2]);

            if (alteracao != retorno && InfoRetorno[3] == "C")
            {
                alteracao = retorno;

                Pontos();
                HouveAposta(DadosRetornoVez);
                AtualizarJogadorDaVez(InfoRetorno);

                if (DadosRetornoVez.Length > 1)
                {
                    AtualizarJogadas(DadosRetornoVez);
                    cartasJogadas++;
                }

                VerificarJogadas(IdPartida);
                VerificarJogadasNoRoundAtual();

                //Se o round acabou, limpar a mesa se VerificarJogadasArray estiver vazio e não for a primeira vez
                if (VerificarJogadasArray[0] == "" && !primeiraVez)
                {
                    LimparAMesa();
                }
                primeiraVez = false;

            }

            VerificacaoDeCartasNaMesa(InfoRetorno, IdPartida);
        }

        public void LimparAMesa()
        {
            c.LimparAsCartas();
            ConsultarMao(0);
            RetirarCartasMeio();
            primeiraVez = true;
            apostar = true;
        }

        private void AtualizarJogadorDaVez(string[] InfoRetorno)
        {
            string[] JogadorInfo = Jogador.Split(',');

            foreach (string itens in JogadoresAtuais)
            {
                string[] infoJogador = itens.Split(',');
                if (infoJogador[0] == InfoRetorno[1])
                {
                    lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                }
                if (InfoRetorno[1] == JogadorInfo[0])
                {
                    vez = true;
                    //ConsultarMao(0);
                }
                else
                {
                    vez = false;
                }
            }
        }

        private void AtualizarJogadas(string[] DadosRetornoVez)
        {
            int TamRetorno = DadosRetornoVez.Length;
            string[] InfoRetornoJogada = DadosRetornoVez[TamRetorno - 1].Split(',');
            string[] aux = InfoRetornoJogada[0].Split(':');

            if (aux[0] == "C")
            {
                posicaoDoJogador = c.localNaMesaCadaJogador[aux[1]];
                c.ColocarCartasMeio(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3], posicaoDoJogador);
            }
            else
            {
                foreach(string dados in DadosRetornoVez)
                {
                    string[] InfoRetornoJogada2 = dados.Split(',');
                    string[] aux2 = InfoRetornoJogada2[0].Split(':');
                    
                    if(aux2[0] == "A")
                    {
                        List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                        posicaoDoJogador = c.localNaMesaCadaJogador[aux[1].Trim()];
                        labels[posicaoDoJogador].Text = Convert.ToString(InfoRetornoJogada2[2]);
                    }
                }
            }
        }

        private void VerificarJogadas(int IdPartida)
        {
            string jogadas = Jogo.ExibirJogadas2(IdPartida);
            label15.Text = jogadas;
            VerificarJogadasArray = t.TratarDadosEmArray(jogadas);
        }

        public void VerificarJogadasNoRoundAtual()
        {
            if (VerificarJogadasArray != null && VerificarJogadasArray.Length != 0)
            {
                label6.Text = "";
                foreach (string s in VerificarJogadasArray)
                {
                    string[] aux = s.Split(',');
                    if (aux[0] == Convert.ToString(rodada))
                    {
                        VerificarJogadasNoRoundAtualArray.Add(s);
                        label6.Text += s + "\n";
                    }
                }

                if (cartasJogadas == JogadoresAtuais.Count())
                {
                    RetirarCartasMeio();
                }
            }
            
        }

        public void VerificacaoDeCartasNaMesa(string[] InfoRetornoVerificarVez, int idPartida)
        {
            string NuemeroDaRodada = InfoRetornoVerificarVez[2];

            if (NuemeroDaRodada == null || VerificarJogadasArray == null)
            {
                return;
            }

            foreach (string jogada in VerificarJogadasArray)
            {
                if (jogada != null && jogada != "")
                {
                    string[] InfoExibirJogadas = jogada.Split(',');
                    posicaoDoJogador = c.localNaMesaCadaJogador[InfoExibirJogadas[1]];
                    c.ColocarCartasMeio(InfoExibirJogadas[1], InfoExibirJogadas[2], InfoExibirJogadas[3], InfoExibirJogadas[4], posicaoDoJogador);
                }
            }
        }

        private void RetirarCartasMeio()
        {
            foreach (Control control in pnlCartasMeio.Controls)
            {
                control.Visible = false;
            }
            cartasJogadas = 0;
        }

        private void HouveAposta(string[] DadosRetornoVez)
        {
            List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };

            //string[] InfoRetornoJogada = DadosRetornoVez[DadosRetornoVez.Length - 1].Split(',');
            foreach (string item in DadosRetornoVez)
            {
                string[] aux = item.Split(':');

                if (aux[0] == "A")
                {
                    string[] Id = aux[1].Split(',');
                    posicaoDoJogador = c.localNaMesaCadaJogador[Id[0].Trim()];
                    c.ValorCartasJogador(Id[0], Id[2], Id[4]);
                    labels[posicaoDoJogador].Text = Convert.ToString(Id[2]);
                }
            }
        }

        public void Pontos()
        {
            List<Label> PontosTotais = new List<Label> { lblTotalP1, lblTotalP2, lblTotalP3, lblTotalP4 };
            List<Label> PontosRodada = new List<Label> { lblPontos, lblPontoP2, lblPontosP3, lblPontosP4 };

            lblParticipantes.Visible = false;

            label3.Text = "";

            foreach (string JogadoresAtuais in JogadoresAtuais)
            {
                label3.Text += JogadoresAtuais + "\n";

                string[] aux = JogadoresAtuais.Split(',');
                posicaoDoJogador = c.localNaMesaCadaJogador[aux[0]];

                PontosTotais[posicaoDoJogador].Text = aux[2];
                PontosRodada[posicaoDoJogador].Text = aux[3];
            }
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            Comercar();
            estado = "J";
        }

        public void Comercar()
        {
            string[] DadosJogador = Jogador.Split(',');
            int IdJogador = Convert.ToInt32(DadosJogador[0]);
            string retorno = Jogo.IniciarPartida(IdJogador, DadosJogador[1]);

            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] j = JogadoresAtuais[i].Split(',');

                if (j[0] == retorno)
                {
                    _ = MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
            estado = "J";
            btnComecar.Visible = false;
            ConsultarMao(0);
            grbPlayer1.Visible = true;
        }

        private void ConsultarMao(int k)
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = t.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais, k);
        }

        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais, int k)
        {
            bool primeiro = true;
            c.localNaMesaCadaJogador.Clear();
            for (int i = 0; i < JogadoresAtuais.Length; i++)
            {
                string[] aux = JogadoresAtuais[i].Split(',');

                if (aux[0] != idDoJogador)
                {
                    if (i == 0)
                    {
                        i++;
                        primeiro = false;
                    }

                    c.MostrarCartas(aux, DadosConsultarMao, i, "player");

                    if (!primeiro)
                    {
                        i--;
                    }
                }
                else
                {
                    c.MostrarCartas(aux, DadosConsultarMao, 0, idDoJogador);
                }
            }
        }

        private void Apostar()
        {
            if (lsbPlayer1.SelectedItem == null)
            {
                //MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnJogar_Click_1(object sender, EventArgs e)
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
            lsbPlayer1.Text = "";
            if (!t.Error(retorno))
            {
                //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);

                if (apostar)
                {
                    DialogResult decisao = MessageBox.Show("Apostar?", "", MessageBoxButtons.YesNo);
                    if (decisao == DialogResult.Yes)
                    {
                        Apostar();
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

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
using System.Web.Routing;
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
        Tratamento r = new Tratamento();
        Lobby lobby = new Lobby();
        Cartas c;

        //Variavel de estado do Jogo
        public string estado = "A";
        public bool apostar = true;
        public bool vez = false;
        public int rodada = 0;
        public int round = 1;
        public int IdPartida;
        string[] exibirJogadas = { };
        string alteracao = "";

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
        }

        //Verificação de pessoas na Partida
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
                quantidade = JogadoresAtuais.Length;
        }

        private void tmrVerificarVez_Tick(object sender, EventArgs e)
        {
            tmrVerificarVez.Enabled = false;
            if (estado.Trim() == "A")
            {
                ReloAsync();
            }
            else
            {
                tmrVerificarVez.Interval = 8000;
                VerificarVez();
            }
            tmrVerificarVez.Enabled = true;
        }

        /*
         public async Task VerificarVez()
         {
            int TamRetorno = 1;
            lobby.LobbyListarJogadores2(PartidaSelecionada);

            //_= ReloAsync();
            int idPartida = Convert.ToInt32(PartidaAtual[0]);
            string retorno = Jogo.VerificarVez2(idPartida);

            // status da partida , id do jogador da vez, nuemero da rodada, status da rodada
            string[] DadosRetorno = r.TratarDadosEmArray(retorno);

            //Colocando o nome do jogador da vez
            string[] InfoRetorno = DadosRetorno[0].Split(',');
            estado = InfoRetorno[0];

            foreach(string itens in JogadoresAtuais)
            {
                string[] infoJogador = itens.Split(',');
                if (infoJogador[0] == InfoRetorno[1])
                {
                    lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                }
            }

            string[] exibirJogadas = { };
            //Varificando a Carta Jogada
            if (DadosRetorno.Length != TamRetorno && DadosRetorno.Length > 1)
            {
                int round = Convert.ToInt32(InfoRetorno[2]);
                //string[] ExibirJogadas = ExibirJogadasAtuais(idPartida);
                Pontos();


                TamRetorno = DadosRetorno.Length;
                if (TamRetorno > 1)
                {
                    string[] InfoRetornoJogada = DadosRetorno[TamRetorno - 1].Split(',');
                    string[] aux = InfoRetornoJogada[0].Split(':');

                    //Colocar carta no meio da mesa
                    if (aux[0] == "C")
                    {
                        c.VerificarJogadaDosPlayers(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3]);
                    }
                    //Aposta
                    else
                    {
                        List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                        int posicao = c.localNaMesaCadaJogador[aux[1].Trim()];
                        labels[posicao].Text = Convert.ToString(InfoRetornoJogada[2]);
                    }

                }
                exibirJogadas = ExibirJogadasAtuais(idPartida);
            }

            if(exibirJogadas.Length == JogadoresAtuais.Length && c.cartasJogadas.Count() == JogadoresAtuais.Length)
            {
                await Task.Delay(5000);
                foreach(var item in c.cartasJogadas)
                {
                    item.Value.Visible = false;
                    item.Key.Visible = false;
                }
                c.cartasJogadas.Clear();
            }

            await Task.Delay(6000);
        }*/

        //public void VerificarVez()
        //{
        //    int TamRetorno = 1;

        //    idPartida = Convert.ToInt32(PartidaAtual[0]);
        //    string retorno = Jogo.VerificarVez2(idPartida);

        //    // status da partida , id do jogador da vez, nuemero da rodada, status da rodada
        //    DadosRetornoVez = r.TratarDadosEmArray(retorno);

        //    InfoRetorno = DadosRetornoVez[0].Split(',');
        //    estado = InfoRetorno[0];

        //    if (alteracao != retorno && InfoRetorno[3] == "C")
        //    {
        //        alteracao = retorno;

        //        ReloAsync();
        //        Pontos();
        //        HouveAposta(DadosRetornoVez);

        //        string[] JogadorInfo = Jogador.Split(',');

        //        foreach(string itens in JogadoresAtuais)
        //        {
        //            string[] infoJogador = itens.Split(',');
        //            if (infoJogador[0] == InfoRetorno[1])
        //            {
        //                lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
        //            }
        //            if (InfoRetorno[1] == JogadorInfo[0])
        //            {
        //                vez = true;
        //                ConsultarMao();
        //                vez = false;
        //            }
        //        }

        //        exibirJogadas = null;
        //        //Varificando a Carta Jogada
        //        if (DadosRetornoVez.Length != TamRetorno && DadosRetornoVez.Length > 1)
        //        {
        //            round = Convert.ToInt32(InfoRetorno[2]);
        //            //string[] ExibirJogadas = ExibirJogadasAtuais(idPartida);
        //            Pontos();


        //            TamRetorno = DadosRetornoVez.Length;
        //            if (TamRetorno > 1)
        //            {
        //                string[] InfoRetornoJogada = DadosRetornoVez[TamRetorno - 1].Split(',');
        //                string[] aux = InfoRetornoJogada[0].Split(':');

        //                //Colocar carta no meio da mesa
        //                if (aux[0] == "C")
        //                {
        //                    c.VerificarJogadaDosPlayers(aux[1], InfoRetornoJogada[1], InfoRetornoJogada[2], InfoRetornoJogada[3]);
        //                }
        //                //Aposta
        //                else
        //                {
        //                    List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
        //                    int posicao = c.localNaMesaCadaJogador[aux[1].Trim()];
        //                    labels[posicao].Text = Convert.ToString(InfoRetornoJogada[2]);
        //                }

        //            }
        //            exibirJogadas = ExibirJogadasAtuais(idPartida);
        //        }

        //        RetirarCartasMeio();
        //    }
        //    VerificacaoDeCartasNaMesa(InfoRetorno, idPartida);
        //}

        public void VerificarVez()
        {

            string RetornoVerificarVez = Jogo.VerificarVez2(IdPartida);

            //Desmembramento das linhas
            string[] DadosRetornoVerificarVez = r.TratarDadosEmArray(RetornoVerificarVez);

            //Desmembramento das colunas
            //status da partida , id do jogador da vez, nuemero da rodada, status da rodada
            string[] InfoRetornoVerificarVez = DadosRetornoVerificarVez[0].Split(',');
            estado = InfoRetornoVerificarVez[0];

            string[] JogadorInfo = Jogador.Split(',');

            foreach (string itens in JogadoresAtuais)
            {
                string[] infoJogador = itens.Split(',');
                if (infoJogador[0] == InfoRetornoVerificarVez[1])
                {
                    lblQJogadores.Text = "Jogador da Vez: " + infoJogador[1];
                }
                if (InfoRetornoVerificarVez[1] == JogadorInfo[0])
                {
                    vez = true;
                }
            }

            if (!vez)
            {
                VerificacaoDeCartasNaMesa(InfoRetornoVerificarVez, IdPartida);
                HouveAposta(DadosRetornoVerificarVez);
            }
            
        }


        public void Pontos()
        {
            List<Label> lista = new List<Label> { lblTotalP1, lblTotalP2, lblTotalP3, lblTotalP4 };
            List<Label> labels = new List<Label> { lblPontos, lblPontoP2, lblPontosP3, lblPontosP4 };

            lblParticipantes.Visible = false;

            label3.Text = "";

            int i = 0;
            foreach (string JogadoresAtuais in JogadoresAtuais)
            {
                label3.Text += JogadoresAtuais + "\n";

                string[] aux = JogadoresAtuais.Split(',');
                lista[i].Text = aux[2];

                int pon = c.localNaMesaCadaJogador[aux[0]];
                labels[pon].Text = aux[3];
                i++;
            }
        }

        public void VerificacaoDeCartasNaMesa(string[] InfoRetornoVerificarVez, int idPartida)
        {
            exibirJogadas = ExibirJogadasAtuais(idPartida);

            string NuemeroDaRodada = InfoRetornoVerificarVez[2];

            if (NuemeroDaRodada == null || exibirJogadas == null) return;

            round = Convert.ToInt32(NuemeroDaRodada);
            foreach (string jogada in exibirJogadas)
            {
                if(jogada != null)
                {
                    string[] InfoExibirJogadas = jogada.Split(',');
                    c.VerificarJogadaDosPlayers(InfoExibirJogadas[1], InfoExibirJogadas[2], InfoExibirJogadas[3], InfoExibirJogadas[4]);
                }
            }   
        }

        public void HouveAposta(string[] DadosRetornoVerificarVez)
        {
            string[] InfoRetornoJogada = DadosRetornoVerificarVez[DadosRetornoVerificarVez.Length - 1].Split(',');
            foreach (string item in InfoRetornoJogada)
            {
                string[] aux = item.Split(':');

                if (aux[0] == "A")
                {
                    List<Label> labels = new List<Label> { lblAposta, lblAposta2, lblAposta3, lblAposta4 };
                    int posicao = c.localNaMesaCadaJogador[aux[1].Trim()];
                    labels[posicao].Text = Convert.ToString(InfoRetornoJogada[2]);
                }
            }
        }

        public void RetirarCartasMeio(string exibirJogadas)
        {
            if (exibirJogadas == null && c.cartasJogadas.Count() == JogadoresAtuais.Length)
            {
                foreach (var item in c.cartasJogadas)
                {
                    item.Value.Visible = false;
                    item.Key.Visible = false;
                }
                c.cartasJogadas.Clear();
            }
        }

        public string[] ExibirJogadasAtuais(int IdPartida)
        {
            string retorno1 = Jogo.ExibirJogadas2(IdPartida);
            string[] DadosRetorno1 = r.TratarDadosEmArray(retorno1);
            string[] SplitRetorno1 = DadosRetorno1[0].Split(',');

            if (!r.Error(retorno1) && SplitRetorno1[0] != null && SplitRetorno1[0] != "" && retorno1 != "")
            {
                round = Convert.ToInt32(SplitRetorno1[0]);
                string retorno = Jogo.ExibirJogadas2(IdPartida, round);
                string[] Jogadas = r.TratarDadosEmArray(retorno);

                int i = 0;
                string[] JogadasAtuais = new string[JogadoresAtuais.Length];
                foreach(string s in Jogadas)
                {
                    string[] aux = s.Split(',');
                    if (aux[0] == Convert.ToString(round))
                    {
                        JogadasAtuais[i] = s;
                        i++;
                    }
                }

                return JogadasAtuais;
            }
            return default(string[]);
        }

        private void btnComecar_Click(object sender, EventArgs e)
        {
            Comercar();
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
                    MessageBox.Show("O primeiro jogador é: " + j[1] + "\n Id: " + retorno);
                }
            }
            estado = "J";
            btnComecar.Visible = false;
            ConsultarMao();
        }

        private void ConsultarMao()
        {
            string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
            string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);

            string[] DadosJogador = Jogador.Split(',');
            estado = "J";
            MostrarGalera(DadosJogador[0], DadosConsultarMao, JogadoresAtuais);
        }

        public void MostrarGalera(string idDoJogador, string[] DadosConsultarMao, string[] JogadoresAtuais)
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
            if (!r.Error(retorno))
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
                        Jogo.Apostar(IdJogador, DadosJogador[1], 0);
                        //MessageBox.Show("Pulou aposta", "", MessageBoxButtons.OK);
                    }
                }
            }
            else
            {
                //MessageBox.Show("Tente Novamente", "", MessageBoxButtons.OK);
                //ConsultarMao();
            }
        }

        private void Apostar()
        {
            btnApostar.Visible = true;
            if (lsbPlayer1.SelectedItem == null)
            {
                //MessageBox.Show($"Selecione uma carta", "Apostar", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                int aux = c.localNaMesaCadaJogador[Convert.ToString(IdJogador)];
                labels[aux].Text = retorno;
                //MessageBox.Show(retorno, "Valor da Carta", MessageBoxButtons.OK);,
                //ConsultarMao();
            }
            else
            {
                Apostar();
            }
        }

    }
}

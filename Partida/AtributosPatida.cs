using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected string alteracao = "";
        protected string ganhador = "";

        //Arrays
        protected string[] VerificarJogadasArray = { };
        protected List<string> VerificarJogadasNoRoundAtualArray = new List<string>();

        //Ints
        public int rodada = 0;
        public int IdPartida;
        protected int posicaoDoJogador = 0;
        protected int cartasJogadas;

        //Bool
        protected bool primeiraVez = true;
        protected bool acabou = false;
        public bool apostar = true;
        public bool vez = false;
    }
}

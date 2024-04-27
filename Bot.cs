using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using static Lobby;

namespace MagicTrick_Tirana
{
    class Bot : Partida
    {

        public string idBot;
        public string[] botcartas;

        public static string[] PartidaAtual { get; }

        //Verica
        //string retorno = Jogo.VerificarVez2(Convert.ToInt32(PartidaAtual[0]));
        int JogoQueEstaSendoJogado = Convert.ToInt32(PartidaAtual[0]);


        public void BotJogando()
        {
            string vez = Jogo.VerificarVez2(JogoQueEstaSendoJogado);
            if( vez == idBot )
            {
                
            }

        }
    }
}

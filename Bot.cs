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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static Lobby;

namespace MagicTrick_Tirana
{
    class Bot : Partida
    {
        Tratamento r = new Tratamento();
        //Partida partidaAtual = new Partida();

        //int idBot; 
        //int idPartida = Convert.ToInt32();

        #region
        //public static new string[] PartidaAtual { get; }
        //string[] maoApenasPosicaoENaipe;
        //string[][] maoComTudo;

        //string retorno = Jogo.VerificarVez2(Convert.ToInt32(PartidaAtual[0]));
        //int JogoQueEstaSendoJogado = Convert.ToInt32(PartidaAtual[0]);


        //public void BotConfigurações()
        //{           
        //   string retorno = Jogo.ConsultarMao(Convert.ToInt32(PartidaAtual[0]));
        //   string[] DadosConsultarMao = r.TratarDadosEmArray(retorno);
        //   estado = "J";

        //   MostrarGalera(DadosConsultarMao[0], DadosConsultarMao, JogadoresAtuais);

        //   idBot = Convert.ToInt32(DadosConsultarMao[0]);

        //   string[] MaoBot = DadosConsultarMao[1].Split(',');

        //   VerMaoBot(MaoBot);   
        //}

        //void VerMaoBot(string[] mao)
        //{
        //    for(int i = 0; i < mao.Length; i++)
        //    {
        //        maoApenasPosicaoENaipe[i] = mao[i];
        //    }
        //}

        //public bool VezDoBot()
        //{
        //    string retorno = Jogo.VerificarVez2(idPartida);
        //    string[] DadosRetorno = r.TratarDadosEmArray(retorno);
        //    int EAVezDoBot = Convert.ToInt32(DadosRetorno[1]);
        //    if (EAVezDoBot > idBot)
        //        return true;
        //    else
        //        return false;
        //}

        //public void botJogando()
        //{
        //    if (vez)
        //        idPartida = 0;
        //    else
        //        return;
        //}
        #endregion
    }
}

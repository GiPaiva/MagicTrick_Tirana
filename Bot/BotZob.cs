using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrick_Tirana.Bot
{
    internal class BotZob
    {
        Dictionary<string,JogadorInfo> jogadoresInfos;
        string IdBot;

        public BotZob(List<List<string[]>> cartas, string[] Id, string[] posicaoMesa, string IdBot)
        {
            for(int i = 0; i < Id.Length; i++)
            {
                jogadoresInfos.Add(Id[i], new JogadorInfo(Id[i], cartas[i]));
            }

            this.IdBot = IdBot;
        }

        public string Jogar()
        {


            string jogada = "";
            return jogada;
        }
    }
}

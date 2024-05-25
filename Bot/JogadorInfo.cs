using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrick_Tirana.Bot
{
    internal class JogadorInfo
    {
        public string IdJogador { get; set; }
        public string PosicaoMesa { get; set; }
        public List<string[]> Cartas { get; set; }

        public JogadorInfo(string idJogador, List<string[]> cartas)
        {
            IdJogador = idJogador;
            Cartas = cartas;
        }
    }
}

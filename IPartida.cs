using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrick_Tirana
{
    interface IPartida
    {
        string Jogar(string pos);
        void Apostar(string senha, string posicao, string IdJogador);
        void ConsultarMao();
    }
}

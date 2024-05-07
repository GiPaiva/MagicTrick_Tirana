using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicTrick_Tirana
{
    internal interface IProps
    {
        string Jogador { get; set; }
        string[] JogadoresAtuais { get; set; }
        string[] PartidaAtual { get; set; }
        string PartidaSelecionada { get; set; }
    }
}

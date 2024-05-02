using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace MagicTrick_Tirana
{
    class Bot1 : Cartas
    {
        Partida p = new Partida();
        Tratamento r = new Tratamento();
        Cartas c;


        public Bot1(Partida partida) : base(partida)
        {
            this.c = new Cartas(partida);
        }

        public string Jogar(int Round)
        {
            //É o primeiro a jogar?
            if(c.cartasJogadas.Count == 0)
            {
                return QuantidadeDeCartasNaMao();
            }
            else
            {
                string naipe = VerificarCartasNaMesa(Round);
                string[] cartas = {};
                int i = 0;
                foreach (var item in c.cartinhasDoJogadorAtual)
                {
                    if (item.Value[0] == naipe)
                    {
                        cartas[i] = item.Key; i++;
                    }
                }

                //Não tem o naipe
                if(cartas.Length == 0)
                {
                    return QuantidadeDeCartasNaMao();
                }
                else //Tem o naipe
                {
                    if(cartas.Length == 1)
                    {
                        return cartas[0];
                    }
                    else
                    {
                        return QuantidadeDeCartasNaMao();
                    }
                }
            }
        }

        public int Apostar()
        {
            if (posicoesCartasMao.Count() == 1)
            {
                return Convert.ToInt32(posicoesCartasMao[0]);
            }
            return 0;
        }

        

        private string VerificarCartasNaMesa(int Round)
        {
            int idPartida = Convert.ToInt32(p.PartidaAtual[0]);
            string retorno = Jogo.ExibirJogadas(idPartida, Round);

            string[] DadosRetorno;
            if (!r.Error(retorno) && retorno != null)
            {
                DadosRetorno = r.TratarDadosEmArray(retorno);
                foreach(string dados in DadosRetorno)
                {
                    string[] aux = dados.Split(',',':');
                    if (aux[0].Trim() == "C")
                    {
                        return aux[2];
                    }
                }
            }
            return "";
        }

    }
}

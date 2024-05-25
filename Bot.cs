using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;
using System.Windows.Forms;

namespace MagicTrick_Tirana
{
    class Bot
    {
        Tratamento t = new Tratamento();

        public string BotJogar(int Round, int IdPartida, 
            Dictionary<string, string[]> cartinhasDoJogadorAtual, Dictionary<Panel, Label> cartasJogadas, 
            List<string> posicoesCartasMao, int QuantidadeDeCartasTotal, int[] pontos)
        {
            //É o primeiro a jogar?
            if(cartasJogadas.Count == 0)
            {
                return QuantidadeDeCartasNaMao(posicoesCartasMao, QuantidadeDeCartasTotal, pontos);
            }
            else
            {
                string naipe = VerificarCartasNaMesa(Round, IdPartida);
                string[] cartas = {};
                int i = 0;
                foreach (var item in cartinhasDoJogadorAtual)
                {
                    if (item.Value[0] == naipe)
                    {
                        cartas[i] = item.Key; i++;
                    }
                }

                //Não tem o naipe
                if(cartas.Length == 0)
                {
                    return QuantidadeDeCartasNaMao(posicoesCartasMao, QuantidadeDeCartasTotal, pontos);
                }
                else //Tem o naipe
                {
                    if(cartas.Length == 1)
                    {
                        return cartas[0];
                    }
                    else
                    {
                        return QuantidadeDeCartasNaMao(posicoesCartasMao, cartas, QuantidadeDeCartasTotal, pontos);
                    }
                }
            }
        }

        public int BotApostar(List<string> posicoesCartasMao)
        {
            if (posicoesCartasMao.Count() == 1)
            {
                return Convert.ToInt32(posicoesCartasMao[0]);
            }
            return 0;
        }

        private string VerificarCartasNaMesa(int Round, int IdPartida)
        {
            string retorno = Jogo.ExibirJogadas(IdPartida, Round);

            string[] DadosRetorno;
            if (!t.Error(retorno) && retorno != null)
            {
                DadosRetorno = t.TratarDadosEmArray(retorno);
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

        //public string Jogar(string pos)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Apostar(string senha, string retorno, string IdJogador)
        //{
        //    throw new NotImplementedException();
        //}

        public void BotConsultarMao()
        {
            throw new NotImplementedException();
        }

        //não tem o naipe
        public string QuantidadeDeCartasNaMao(List<string> posicoesCartasMao, int QuantidadeDeCartasTotal, int[] pontos)
        {

            if (posicoesCartasMao.Count() > QuantidadeDeCartasTotal / 2)
            {
                //Jogar Maior Carta
                return posicoesCartasMao[posicoesCartasMao.Count() - 1];
            }
            else
            {
                if (pontos[0] <= 4)
                {
                    //Jogar Maior Carta
                    return posicoesCartasMao[posicoesCartasMao.Count() - 1]; //batendo aqui
                    /*System.ArgumentOutOfRangeException: 'O índice estava fora do intervalo. 
                     * Ele deve ser não-negativo e menor que o tamanho da coleção. 
                     * Arg_ParamName_Name'*/
                }
                else
                {
                    //Jogar Menor Carta
                    return posicoesCartasMao[0];
                }
            }
        }

        //tem o naipe
        public string QuantidadeDeCartasNaMao(List<string> posicoesCartasMao, string []cartas, int QuantidadeDeCartasTotal, int[] pontos)
        {

            if (posicoesCartasMao.Count() > QuantidadeDeCartasTotal / 2)
            {
                //Jogar Maior Carta
                return cartas[cartas.Count() - 1];
            }
            else
            {
                if (pontos[0] <= 2)
                {
                    //Jogar Maior Carta
                    return cartas[cartas.Count() - 1]; //batendo aqui
                    /*System.ArgumentOutOfRangeException: 'O índice estava fora do intervalo. 
                     * Ele deve ser não-negativo e menor que o tamanho da coleção. 
                     * Arg_ParamName_Name'*/
                }
                else
                {
                    //Jogar Menor Carta
                    return cartas[0];
                }
            }
        }
    }
}

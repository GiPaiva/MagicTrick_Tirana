using MagicTrick_Tirana;
using MagicTrickServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lobby;

class Lobby
{
    private Tratamento r = new Tratamento();
    
    public struct Partida
    {
        public static int IdPartida;
        public static string NomePartida;
        //public static string StatusPartida;
    }

    public string[] Partidas;
    public string[] Jogadores;


    public bool LobbyListarPartidas()
    {
        bool ok = false;
        string BuscarPartidas = "";
        try
        {
            BuscarPartidas = Jogo.ListarPartidas("T");
        }
        catch(Exception ex)
        {
            r.Error(ex.Message);
        }
        finally
        {
            if (BuscarPartidas != "")
            {
                if (!r.Error(BuscarPartidas))
                {
                    //Fazer tratamento de dados
                    Partidas = r.TratarDadosEmArray(BuscarPartidas);
                    ok = true;
                }
            }
        }
        return ok;

    }

    public bool LobbyListarJogadores(string PartidaSelecionada)
    {
        bool ok = false;
        if (!r.Error(PartidaSelecionada))
        {
            if (PartidaSelecionada.IndexOf(',') != -1)
            {
                string[] DadosPartida = PartidaSelecionada.Split(',');

                Partida.IdPartida = Convert.ToInt32(DadosPartida[0]);
                Partida.NomePartida = DadosPartida[1];

                string BuscarJogadores = Jogo.ListarJogadores(Partida.IdPartida);

                if (BuscarJogadores.Length != 0)
                {
                    if (!r.Error(BuscarJogadores))
                    {
                        //Fazer tratamento de dados
                        Jogadores = r.TratarDadosEmArray(BuscarJogadores);
                        ok = true;
                    }
                }
                
            }
        }

        return ok;
    }

    public bool LobbyCriarPartida(string NomePartida,string SenhaPartida)
    {
        bool ok = false;
        string retorno = "";
        try
        {
            retorno = Jogo.CriarPartida(NomePartida, SenhaPartida, "Tirana");
        }
        catch(Exception e)
        {
            r.Error(e.Message);
        }
        finally
        {
            if (!r.Error(retorno))
            {
                MessageBox.Show("Partida Criada com Sucesso!\nId da Partida: " + retorno, "Partida Criada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ok = true;
            }
        }
        
        return ok;
    }

    public string LobbyEntrarPartida(int IdPartida, string NomeDoJogador, string SenhaDaPartida, string NomePartida)
    {
        string retorno = "";
        try
        {
            retorno = Jogo.EntrarPartida(IdPartida, NomeDoJogador, SenhaDaPartida);
        }
        catch
        {
            r.Error(retorno);
            return "";
        }
        finally
        {
            if (!r.Error(retorno))
            {
                MessageBox.Show(
                    $"{NomeDoJogador} entrou na partida: \r\n{NomePartida}! \r\n IdJogador: {retorno}",
                    "Jogador Entrou",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }else retorno = "";

        }
        return retorno;
    }

    public string LobbyExibirJogadas(int round)
    {
        string retorno = Jogo.ExibirJogadas2(Partida.IdPartida, round);
        if (!r.Error(retorno) && retorno != null)
        {
            string[] DadosRetorno = r.TratarDadosEmArray(retorno);
            return DadosRetorno[0];
        }
        return default(string);
    }
}


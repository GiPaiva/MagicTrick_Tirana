using MagicTrickServer;
using System;
using System.Windows.Forms;


class Tratamento
{

    public bool Error(string msg)
    {
        if(msg.Length > 4)
        {
            if(msg.Substring(0, 4) == "ERRO" || msg.Substring(0, 4) == "Erro")
            {
                MessageBox.Show("Ocorreu um erro: \n" + msg, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
        }
        return false;
    }

    //Tratamento de string
    public string[] TratarDadosEmArray(string variavel)
    {
        variavel = variavel.Replace("\r", "");
        variavel = variavel.Substring(0, variavel.Length - 1);
        string[] Partidas = variavel.Split('\n');
        return Partidas;
    }
}


using System;
using System.Windows.Forms;


class Tratamento
{
    public void Error(string msg)
    {
        MessageBox.Show("Ocorreu um erro: \n" + msg, "ERRO", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    //Tratamento de string
    public string[] TratarDados(string variavel)
    {
        variavel = variavel.Replace("\r", "");
        variavel = variavel.Substring(0, variavel.Length - 1);
        string[] Partidas = variavel.Split('\n');
        return Partidas;
    }
}


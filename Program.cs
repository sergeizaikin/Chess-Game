using System;
using JogoXadrez;
using JogoXadrez.TabuleiroNS;
using JogoXadrez.Jogo;
using JogoXadrez.Exceptions;


namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();
                Tela.ImprimirTabuleiro(partida.Tab);
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine("Tabuleiro: " + e);
            }
            
        }
    }
}

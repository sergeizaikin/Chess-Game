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
                Tabuleiro tab = new Tabuleiro(8, 8);

                PosicaoXadrez pos = new PosicaoXadrez('c', 7);

                Console.WriteLine(pos);
                Console.WriteLine(pos.toPosicao());
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
            
        }
    }
}

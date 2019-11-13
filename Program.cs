using System;
using JogoXadrez;
using JogoXadrez.TabuleiroNS;
using JogoXadrez.Jogo;


namespace JogoXadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 1));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}

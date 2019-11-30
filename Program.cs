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

                while (!partida.Terminada)
                {
                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);

                        Posicao origem = Tela.LerPosicaoXadrez(true).ToPosicao();
                        partida.ValidarPosicaoDeOrigem(origem);
                        Tela.ImprimirPartida(partida, origem);

                        Posicao destino = Tela.LerPosicaoXadrez(false, origem.ToPosicaoXadrez()).ToPosicao();
                        partida.ValidarPosicaoDeDestino(origem, destino);

                        partida.RealizaJogada(origem, destino);
                    }
                    catch(TabuleiroException tabEx)
                    {
                        Console.WriteLine(tabEx.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(partida);

            }
            catch (TabuleiroException e)
            {
                Console.WriteLine("Tabuleiro: " + e);
            }
            
        }
    }
}

using System;
using JogoXadrez.TabuleiroNS;
using JogoXadrez.Jogo;
using System.Collections.Generic;
using JogoXadrez.Exceptions;

namespace JogoXadrez
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida, Posicao origem = null)
        {
            Console.Clear();
            if (origem != null)
            {
                var movPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                ImprimirTabuleiro(partida.Tab, movPossiveis);
            }
            else
            {
                ImprimirTabuleiro(partida.Tab);
            }
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + partida.Turno);
            if (!partida.Terminada)
            {
                Console.WriteLine("Aguardando jogada: " + partida.JogadorAtual);
                if (partida.Xeque)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Xeque!");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine($"Vencedor: {partida.JogadorAtual}");
            }

        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.GetPecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.GetPecasCapturadas(Cor.Preta));
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                if (peca.Cor == Cor.Preta)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                Console.Write(peca + " ");
                Console.ResetColor();
            }
            Console.Write("]");

        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    ImprimirPeca(tabuleiro.Peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirTabuleiro(Tabuleiro tabuleiro, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOrig = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGreen;

            for (int i = 0; i < tabuleiro.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tabuleiro.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j] == true)
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOrig;
                    }

                    ImprimirPeca(tabuleiro.Peca(i, j));
                    Console.BackgroundColor = fundoOrig;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write("- ");
                Console.ResetColor();
            }
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(peca);
                    Console.ResetColor();
                }
            }
        }

        public static PosicaoXadrez LerPosicaoXadrez(bool isOrigem, PosicaoXadrez origem = null)
        {
            try
            {
                if (isOrigem)
                {
                    Console.Write("Origem: ");
                    string s = Console.ReadLine();
                    char coluna = s[0];
                    int linha = int.Parse(s[1] + "");
                    return new PosicaoXadrez(coluna, linha);
                }
                else
                {
                    Console.WriteLine($"Origem: {origem}");
                    Console.Write("Destino: ");
                    string s = Console.ReadLine();
                    char coluna = s[0];
                    int linha = int.Parse(s[1] + "");
                    return new PosicaoXadrez(coluna, linha);
                }

            }
            catch (IndexOutOfRangeException)
            {
                throw new TabuleiroException("Posição inválida!");
            }
            catch (FormatException)
            {
                throw new TabuleiroException("Coordinatos incorretos");
            }

        }
    }
}

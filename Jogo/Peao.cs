using JogoXadrez.TabuleiroNS;
using System;
using System.Collections.Generic;
using System.Text;

namespace JogoXadrez.Jogo
{
    class Peao : Peca
    {
        private PartidaDeXadrez Partida;
        public Peao(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor) 
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P ";
        }

        private bool ExisteInimigo(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos)
        {
            return Tab.Peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao posicao = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha - 2, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao) && QtdDeMovimentos == 0)
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }
                // Jogada especial en passant
                if (PosicaoPeca.Linha == 3)
                {
                    Posicao esquerda = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VuneravelEnPassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VuneravelEnPassant)
                    {
                        mat[direita.Linha - 1, direita.Coluna] = true;
                    }
                }
            }
            else
            {
                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha + 2, PosicaoPeca.Coluna);
                if (Tab.PosicaoValida(posicao) && Livre(posicao) && QtdDeMovimentos == 0)
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
                if (Tab.PosicaoValida(posicao) && ExisteInimigo(posicao))
                {
                    mat[posicao.Linha, posicao.Coluna] = true;
                }

                // Jogada especial en passant
                if (PosicaoPeca.Linha == 4)
                {
                    Posicao esquerda = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.Peca(esquerda) == Partida.VuneravelEnPassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao direita = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.Peca(direita) == Partida.VuneravelEnPassant)
                    {
                        mat[direita.Linha + 1, direita.Coluna] = true;
                    }
                }

            }
            return mat;
        }
    }
}

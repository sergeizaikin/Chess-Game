﻿using JogoXadrez.Exceptions;
using System;

namespace JogoXadrez.TabuleiroNS
{
    abstract class Peca
    {
        public Posicao PosicaoPeca { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            PosicaoPeca = null;
            Cor = cor;
            Tab = tab;
            QtdDeMovimentos = 0;
        }

        public void IncrementarQtdDeMovimentos()
        {
            QtdDeMovimentos++;
        }

        public void DecrementarQtdDeMovimentos()
        {
            QtdDeMovimentos--;
        }


        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i = 0; i < Tab.Linhas; i++)
            {
                for (int j = 0; j < Tab.Colunas; j++)
                {
                    if (mat[i,j] == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool MovimentoPossivel(Posicao posicaoDestino)
        {
            try
            {
                return MovimentosPossiveis()[posicaoDestino.Linha, posicaoDestino.Coluna];
            }
            catch (IndexOutOfRangeException)
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        protected bool PodeMover(Posicao posicao)
        {
            Peca p = Tab.Peca(posicao);
            return p == null || p.Cor != Cor;
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}

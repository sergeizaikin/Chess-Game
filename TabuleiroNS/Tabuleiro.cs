﻿using System;
using JogoXadrez.Exceptions;

namespace JogoXadrez.TabuleiroNS
{
    class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca Peca(int linha, int coluna)
        {
            return Pecas[linha, coluna];
        }

        public Peca Peca(Posicao pos)
        {
            try
            {
                return Pecas[pos.Linha, pos.Coluna];
            }
            catch (IndexOutOfRangeException)
            {
                throw new TabuleiroException("Posição inválida!");
            }
        }

        public void ColocarPeca(Peca peca, Posicao posicao)
        {
            if (ExistePeca(posicao))
                throw new TabuleiroException("Já existe peça nessa posção!");
            Pecas[posicao.Linha, posicao.Coluna] = peca;
            peca.PosicaoPeca = posicao;
        }

        public Peca RetirarPeca(Posicao pos)
        {
            if (Peca(pos) == null)
                return null;
            Peca aux = Peca(pos);
            aux.PosicaoPeca = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool ExistePeca(Posicao pos)
        {
            ValidarPosicao(pos);
            return Peca(pos) != null;
        }

        public bool PosicaoValida(Posicao pos)
        {
            return pos.Linha <= Linhas - 1 && pos.Coluna <= Colunas - 1 && pos.Linha >= 0 && pos.Coluna >= 0;
        }

        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
                throw new TabuleiroException("Posição inválida!");
        }
    }
}

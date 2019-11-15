using JogoXadrez.TabuleiroNS;

namespace JogoXadrez.Jogo
{
    class Rei : Peca
    {
        private PartidaDeXadrez Partida;
        public Rei(Tabuleiro tab, Cor cor, PartidaDeXadrez partida) : base(tab, cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "R ";
        }


        private bool TesteTorreParaRoque(Posicao pos)
        {
            Peca p = Tab.Peca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QtdDeMovimentos == 0;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao posicao = new Posicao(0, 0);

            //acima
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //esq-dir
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //direita
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //dir - baix
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna + 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //abaixo
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //abaixo - esq
            posicao.DefinirValores(PosicaoPeca.Linha + 1, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //esq
            posicao.DefinirValores(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            //esq - cima
            posicao.DefinirValores(PosicaoPeca.Linha - 1, PosicaoPeca.Coluna - 1);
            if (Tab.PosicaoValida(posicao) && PodeMover(posicao))
            {
                mat[posicao.Linha, posicao.Coluna] = true;
            }

            // Jogada especial - Roque
            if (QtdDeMovimentos == 0 && !Partida.Xeque)
            {
                // Roque pequeno
                Posicao posT1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 3);
                if (TesteTorreParaRoque(posT1))
                {
                    Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 1);
                    Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna + 2);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null)
                    {
                        mat[PosicaoPeca.Linha, PosicaoPeca.Coluna + 2] = true;
                    }
                }

                // Roque grande
                Posicao posT2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 4);
                if (TesteTorreParaRoque(posT2))
                {
                    Posicao p1 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 1);
                    Posicao p2 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 2);
                    Posicao p3 = new Posicao(PosicaoPeca.Linha, PosicaoPeca.Coluna - 3);

                    if (Tab.Peca(p1) == null && Tab.Peca(p2) == null && Tab.Peca(p3) == null)
                    {
                        mat[PosicaoPeca.Linha, PosicaoPeca.Coluna - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}

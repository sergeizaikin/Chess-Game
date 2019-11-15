using System;
using System.Collections.Generic;
using System.Text;
using JogoXadrez.TabuleiroNS;
using JogoXadrez.Exceptions;

namespace JogoXadrez.Jogo
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        public HashSet<Peca> Pecas { get; set; }
        public HashSet<Peca> PecasCapturadas { get; set; }
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            this.JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Xeque = false;
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdDeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public HashSet<Peca> GetPecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var peca in PecasCapturadas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            return aux;
        }

        public HashSet<Peca> GetPecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (var peca in Pecas)
            {
                if (peca.Cor == cor)
                {
                    aux.Add(peca);
                }
            }
            aux.ExceptWith(GetPecasCapturadas(cor));
            return aux;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdDeMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                PecasCapturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Inimigo(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Inimigo(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca peca in GetPecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.PosicaoPeca;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, new Posicao(i, j));
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ValidarPosicaoDeOrigem(Posicao posicaoOrigem)
        {
            if (Tab.Peca(posicaoOrigem) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAtual != Tab.Peca(posicaoOrigem).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tab.Peca(posicaoOrigem).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }
        //Fazer funcionar
        private void ColocarPecas()
        {
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'a', 1);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Branca), 'b', 1);
            ColocarNovaPeca(new Bispo(Tab, Cor.Branca), 'c', 1);
            ColocarNovaPeca(new Dama(Tab, Cor.Branca), 'd', 1);
            ColocarNovaPeca(new Rei(Tab, Cor.Branca), 'e', 1);
            ColocarNovaPeca(new Bispo(Tab, Cor.Branca), 'f', 1);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Branca), 'g', 1);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'h', 1);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'a', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'b', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'c', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'd', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'e', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'f', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'g', 2);
            ColocarNovaPeca(new Peao(Tab, Cor.Branca), 'h', 2);


            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'a', 8);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Preta), 'b', 8);
            ColocarNovaPeca(new Bispo(Tab, Cor.Preta), 'c', 8);
            ColocarNovaPeca(new Dama(Tab, Cor.Preta), 'd', 8);
            ColocarNovaPeca(new Rei(Tab, Cor.Preta), 'e', 8);
            ColocarNovaPeca(new Bispo(Tab, Cor.Preta), 'f', 8);
            ColocarNovaPeca(new Cavalo(Tab, Cor.Preta), 'g', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'h', 8);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'a', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'b', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'c', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'd', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'e', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'f', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'g', 7);
            ColocarNovaPeca(new Peao(Tab, Cor.Preta), 'h', 7);
        }

        public void ColocarNovaPeca(Peca peca, char coluna, int linha)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);

            if (R == null)
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro!");

            foreach (Peca peca in GetPecasEmJogo(Inimigo(cor)))
            {
                bool[,] mat = peca.MovimentosPossiveis();
                if (mat[R.PosicaoPeca.Linha, R.PosicaoPeca.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        private Cor Inimigo(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in GetPecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }


    }
}

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

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            this.JogadorAtual = Cor.Branca;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            PecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdDeMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if (pecaCapturada != null)
            {
                PecasCapturadas.Add(pecaCapturada);
            }
        }

        public HashSet<Peca> GetPecasCapturadas (Cor cor)
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

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            MudaJogador();
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
            if (!Tab.Peca(origem).PodeMoverPara(destino))
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
            ColocarNovaPeca(new Rei(Tab, Cor.Preta), 'd', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'c', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'd', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'e', 7);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'e', 8);
            ColocarNovaPeca(new Torre(Tab, Cor.Preta), 'c', 8);

            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'c', 1);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'c', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'd', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'e', 2);
            ColocarNovaPeca(new Torre(Tab, Cor.Branca), 'e', 1);
            ColocarNovaPeca(new Rei(Tab, Cor.Branca), 'd', 1);
        }

        public void ColocarNovaPeca(Peca peca, char coluna, int linha)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }


    }
}

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

        public bool PodeMoverPara(Posicao posicaoDestino)
        {
            return MovimentosPossiveis()[posicaoDestino.Linha, posicaoDestino.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}

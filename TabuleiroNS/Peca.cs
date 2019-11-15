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

        public abstract bool[,] MovimentosPossiveis();
    }
}

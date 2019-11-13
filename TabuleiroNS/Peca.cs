namespace JogoXadrez.TabuleiroNS
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdDeMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QtdDeMovimentos = 0;
        }

        public void IncrementarQtdDeMovimentos()
        {
            QtdDeMovimentos++;
        }
    }
}

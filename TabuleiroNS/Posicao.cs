using JogoXadrez.Jogo;

namespace JogoXadrez.TabuleiroNS
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public PosicaoXadrez ToPosicaoXadrez()
        {
            return new PosicaoXadrez((char)('a' + Coluna), 8 - Linha);
        }

        public override string ToString()
        {
            return $"{Linha}, {Coluna}";
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }
    }
}

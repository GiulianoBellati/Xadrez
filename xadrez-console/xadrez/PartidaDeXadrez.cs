using tabuleiro;

namespace xadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro tab {  get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool terminada {  get; private set; }

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            colocarPecas();
        }

        public void executaMovimento (Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
        }

        private void colocarPecas() {
            tab.colocarPeca(new Torre(Cor.Amarela, tab), new PosicaoXadrez('c', 1).toPosicao());
            tab.colocarPeca(new Torre(Cor.Amarela, tab), new PosicaoXadrez('c', 2).toPosicao());
            tab.colocarPeca(new Torre(Cor.Amarela, tab), new PosicaoXadrez('d', 2).toPosicao());
            tab.colocarPeca(new Torre(Cor.Amarela, tab), new PosicaoXadrez('e', 2).toPosicao());
            tab.colocarPeca(new Torre(Cor.Amarela, tab), new PosicaoXadrez('e', 1).toPosicao());
            tab.colocarPeca(new Rei(Cor.Amarela, tab), new PosicaoXadrez('d', 1).toPosicao());

            tab.colocarPeca(new Torre(Cor.Verde, tab), new PosicaoXadrez('c', 7).toPosicao());
            tab.colocarPeca(new Torre(Cor.Verde, tab), new PosicaoXadrez('c', 8).toPosicao());
            tab.colocarPeca(new Torre(Cor.Verde, tab), new PosicaoXadrez('d', 7).toPosicao());
            tab.colocarPeca(new Torre(Cor.Verde, tab), new PosicaoXadrez('e', 7).toPosicao());
            tab.colocarPeca(new Torre(Cor.Verde, tab), new PosicaoXadrez('e', 8).toPosicao());
            tab.colocarPeca(new Rei(Cor.Verde, tab), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}

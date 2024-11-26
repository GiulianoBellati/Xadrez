using tabuleiro;

namespace xadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro tab {  get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public void executaMovimento (Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
        }

        public void realizaJogada(Posicao orige, Posicao destino) {
            executaMovimento(orige, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem (Posicao pos) {
            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            if (!tab.peca(origem).podeMoverPara(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            if (jogadorAtual == Cor.Amarela) {
                jogadorAtual = Cor.Verde;
            }
            else {
                jogadorAtual = Cor.Amarela;
            }
        }

        public HashSet<Peca> pecasCapturadas (Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo (Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        public void colocarNovaPeca (char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('c', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('c', 2, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('d', 2, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('e', 2, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('e', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('d', 1, new Rei(Cor.Amarela, tab));

            colocarNovaPeca('c', 7, new Torre(Cor.Verde, tab));
            colocarNovaPeca('c', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('d', 7, new Torre(Cor.Verde, tab));
            colocarNovaPeca('e', 7, new Torre(Cor.Verde, tab));
            colocarNovaPeca('e', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('d', 8, new Rei(Cor.Verde, tab));
        }
    }
}

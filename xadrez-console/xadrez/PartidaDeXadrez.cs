using tabuleiro;

namespace xadrez {
    internal class PartidaDeXadrez {

        public Tabuleiro tab {  get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada {  get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public bool xeque { get; private set; }

        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Amarela;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }

        public Peca executaMovimento (Posicao origem, Posicao destino) {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimento();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimento();
            if (pecaCapturada != null) {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = executaMovimento(origem, destino);

            if (estaEmXeque(jogadorAtual)) {
                desfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }

            if (testeXequeMate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudaJogador();
            }
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
            if (!tab.peca(origem).movimentoPossivel(destino)) {
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

        private Cor adversaria (Cor cor) {
            if (cor == Cor.Amarela) {
                return Cor.Verde;
            }
            else
            {
                return Cor.Amarela;
            }
        }

        private Peca rei (Cor cor) {
            foreach (Peca x in pecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque (Cor cor) {
            Peca R = rei (cor);
            if (R == null) {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequeMate (Cor cor) {
            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo (cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++) {
                    for (int j = 0; j < tab.colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao (i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca (char coluna, int linha, Peca peca) {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void colocarPecas() {
            colocarNovaPeca('a', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Amarela, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Amarela, tab));
            colocarNovaPeca('d', 1, new Dama(Cor.Amarela, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Amarela, tab));
            colocarNovaPeca('f', 1, new Bispo(Cor.Amarela, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Amarela, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('b', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('c', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('d', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('e', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('f', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('g', 2, new Peao(Cor.Amarela, tab));
            colocarNovaPeca('h', 2, new Peao(Cor.Amarela, tab));

            colocarNovaPeca('a', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Verde, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Verde, tab));
            colocarNovaPeca('d', 8, new Dama(Cor.Verde, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Verde, tab));
            colocarNovaPeca('f', 8, new Bispo(Cor.Verde, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Verde, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('b', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('c', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('d', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('e', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('f', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('g', 7, new Peao(Cor.Verde, tab));
            colocarNovaPeca('h', 7, new Peao(Cor.Verde, tab));
        }
    }
}

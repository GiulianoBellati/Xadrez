// Importação de namespace
using tabuleiro;

namespace xadrez {
    internal class Peao : Peca { // Essa classe é uma subclasse da classe Peca, portanto o Peão é uma Peça

        // Atributo:
        private PartidaDeXadrez partida; // Partida necessária para realizar jogada especial en passant

        // Construtor:
        public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab) {
            this.partida = partida;
        }

        // Métodos:
        public override string ToString() {
            // Método que retorna o objeto como string

            return "P";
        }

        private bool existeInimigo(Posicao pos) {
            // Método que valida se há um inimigo em determinada posição

            Peca p = tab.peca(pos);
            return p != null && p.cor != this.cor;
        }

        private bool livre(Posicao pos) {
            // Método que valida se determinada posição está livre de peças

            return tab.peca(pos) == null;
        }

        private bool podeMover(Posicao pos) {
            // Método que valida se uma peça pode mover para determinada posição

            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            // Método que retorna uma matriz booleana de todos o movimentos possíveis para o Rei

            bool[,] mat = new bool[tab.linhas, tab.colunas]; // Criando uma matriz do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); // Instancia uma posição inicialmente na posição 0,0

            // Movimentos:

            if (cor == Cor.Amarela) { // Para a cor amarela:

                // Acima:
                pos.definirValores(posicao.linha - 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Acima x2:
                pos.definirValores(posicao.linha - 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Diagonal superior esquerda:
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Diagonal superior direita:
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Jogada especial en passant:
                if (posicao.linha == 3) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha - 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha - 1, direita.coluna] = true;
                    }
                }
            }
            else { // Para a cor verde:

                // Acima:
                pos.definirValores(posicao.linha + 1, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Acima x2:
                pos.definirValores(posicao.linha + 2, posicao.coluna);
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Diagonal superior direita:
                pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }
                // Diagonal superior esquerda:
                pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
                if (tab.posicaoValida(pos) && existeInimigo(pos)) {
                    mat[pos.linha, pos.coluna] = true;
                }

                // Jogada especial en passant:
                if (posicao.linha == 4) {
                    Posicao esquerda = new Posicao(posicao.linha, posicao.coluna - 1);
                    if (tab.posicaoValida(esquerda) && existeInimigo(esquerda) && tab.peca(esquerda) == partida.vulneravelEnPassant) {
                        mat[esquerda.linha + 1, esquerda.coluna] = true;
                    }
                    Posicao direita = new Posicao(posicao.linha, posicao.coluna + 1);
                    if (tab.posicaoValida(direita) && existeInimigo(direita) && tab.peca(direita) == partida.vulneravelEnPassant) {
                        mat[direita.linha + 1, direita.coluna] = true;
                    }
                }
            }

            return mat; // Retorna a matriz preenchida
        }
    }
}

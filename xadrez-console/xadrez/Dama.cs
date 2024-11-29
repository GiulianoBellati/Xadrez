// Importação de namespace
using tabuleiro;

namespace xadrez {
    internal class Dama : Peca { // Essa classe é uma subclasse da classe Peca, portanto a Dama é uma Peça

        // Construtor:
        public Dama(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        // Métodos:
        public override string ToString() {
            // Método que retorna o objeto como string

            return "D";
        }

        private bool podeMover(Posicao pos) {
            // Método que valida se uma peça pode mover para determinada posição

            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            // Método que retorna uma matriz booleana de todos o movimentos possíveis para a Dama

            bool[,] mat = new bool[tab.linhas, tab.colunas]; // Criando uma matriz do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); // Instancia uma posição inicialmente na posição 0,0

            // Movimentos:

            // Acima:
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna);
            }
            // Abaixo:
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna);
            }
            // Direita:
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha, pos.coluna + 1);
            }
            // Esquerda:
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha, pos.coluna - 1);
            }
            // Diagonal superior esquerda:
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna - 1);
            }
            // Diagonal superior direita:
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha - 1, pos.coluna + 1);
            }
            // Diagonal inferior direita:
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna + 1);
            }
            // Diagonal inferior esquerda:
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            while (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != this.cor) {
                    break;
                }
                pos.definirValores(pos.linha + 1, pos.coluna - 1);
            }

            return mat; // Retorna a matriz preenchida
        }
    }
}

// Importação de namespace
using tabuleiro;

namespace xadrez {
    internal class Rei : Peca { // Essa classe é uma subclasse da classe Peca, portanto o Rei é uma Peça

        // Atributo:
        private PartidaDeXadrez partida; // Partida necessária para realizar jogada especial roque

        // Construtor:
        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab) {
            this.partida = partida;
        }

        // Métodos:
        public override string ToString() {
            // Método que retorna o objeto como string

            return "R";
        }

        private bool podeMover(Posicao pos) {
            // Método que valida se uma peça pode mover para determinada posição

            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        private bool testeTorreParaRoque (Posicao pos) {
            // Método que valida se é possível realizar algum tipo de roque

            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
        }

        public override bool[,] movimentosPossiveis() {
            // Método que retorna uma matriz booleana de todos o movimentos possíveis para o Rei

            bool[,] mat = new bool[tab.linhas, tab.colunas]; // Criando uma matriz do tamanho do tabuleiro

            Posicao pos = new Posicao(0,0); // Instancia uma posição inicialmente na posição 0,0

            // Movimentos:

            // Acima:
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Diagonal superior direita:
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Direita:
            pos.definirValores(posicao.linha, posicao.coluna + 1); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Diagonal inferior direita:
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Abaixo:
            pos.definirValores(posicao.linha + 1, posicao.coluna); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Diagonal inferior esquerda:
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Esquerda:
            pos.definirValores(posicao.linha, posicao.coluna - 1); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }
            // Diagonal superior esquerda:
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1); 
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            // Jogada especial roque:
            if (qteMovimentos == 0 && !partida.xeque) {

                // Roque pequeno:
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                if(testeTorreParaRoque(posT1)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                    if (tab.peca(p1) == null && tab.peca(p2) == null) {
                        mat[posicao.linha, posicao.coluna + 2] = true;
                    }
                }

                // Roque grande:
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                if (testeTorreParaRoque(posT2)) {
                    Posicao p1 = new Posicao(posicao.linha, posicao.coluna - 1);
                    Posicao p2 = new Posicao(posicao.linha, posicao.coluna - 2);
                    Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 3);
                    if (tab.peca(p1) == null && tab.peca(p2) == null && tab.peca(p3) == null) {
                        mat[posicao.linha, posicao.coluna - 2] = true;
                    }
                }
            }

            return mat; // Retorna a matriz preenchida
        }
    }
}

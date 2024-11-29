// Importação de namespace
using tabuleiro;

namespace xadrez {
    internal class Cavalo : Peca { // Essa classe é uma subclasse da classe Peca, portanto o Cavalo é uma Peça

        // Construtor:
        public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        // Métodos:
        public override string ToString() {
            // Método que retorna o objeto como string

            return "C";
        }

        private bool podeMover(Posicao pos) {
            // Método que valida se uma peça pode mover para determinada posição

            Peca p = tab.peca(pos);
            return p == null || p.cor != this.cor;
        }

        public override bool[,] movimentosPossiveis() {
            // Método que retorna uma matriz booleana de todos o movimentos possíveis para o Cavalo

            bool[,] mat = new bool[tab.linhas, tab.colunas]; // Criando uma matriz do tamanho do tabuleiro

            Posicao pos = new Posicao(0, 0); // Instancia uma posição inicialmente na posição 0,0

            // Movimentos:

            /*------------------------------------------------------------------    
     
                          -
                           |
                           |
                           C
             */
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------     
            
                           -
                          |
                          |
                          C
            */
            pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------       
            
                          C
                          |
                          |
                           -
            */
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------  
            
                          C
                          |
                          |
                         -
            */
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------   
            
                         |
                          -- C
            */
            pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------     
            
                          -- C
                         |
            */
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------     
            
                            |
                        C --
            */
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------       
            
                        C --
                            |
            */
            pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) {
                mat[pos.linha, pos.coluna] = true;
            }

            /*------------------------------------------------------------------*/

            return mat; // Retorna a matriz preenchida
        }
    }
}

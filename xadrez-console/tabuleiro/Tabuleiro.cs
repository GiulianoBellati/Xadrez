namespace tabuleiro {
    internal class Tabuleiro {

        // Atributos:
        public int linhas { get; set; } // Quantidade de linhas do tabuleiro
        public int colunas { get; set; } // Quantidade de colunas do tabuleiro
        private Peca[,] pecas; // Matriz de peças do tabuleiro

        // Construtor:
        public Tabuleiro(int linhas, int colunas) {
            this.linhas = linhas; // Quantidade de linhas obrigatório como parâmetro
            this.colunas = colunas; // Quantidade de colunas obrigatório como parâmetro
            pecas = new Peca[linhas, colunas]; // Instanciando matriz de peças do tamanho do tabuleiro zerada
        }

        // Métodos:
        public Peca peca(int linha, int coluna) {
            // Método que retorna peça em determinada linha e coluna

            return pecas[linha, coluna];
        }

        public Peca peca(Posicao pos) {
            // Método que retorna peça em determinada posição

            return pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos) {
            // Método que valida se existe peça em determinada posição

            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void colocarPeca(Peca p, Posicao pos) {
            // Método que coloca uma peça em determinada posição se possível

            if (existePeca(pos)) { // Validando a posição
                throw new TabuleiroException("Já existe uma peça nessa posição!"); // Exceção: já existe uma peça nessa posição
            }
            pecas[pos.linha, pos.coluna] = p; // Inserindo a peça na matriz
            p.posicao = pos; // Atribuindo a posição da matriz para a peça
        }

        public Peca retirarPeca(Posicao pos) {
            // Método que retira uma peça em determinada posição retornando a peça retirada

            if (peca(pos) == null) { // Se não houver peça na posição:
                return null; // Retorna null
            }
            else { // Se houver peça na posição:
                Peca aux = peca(pos); // Obtém a peça na posição
                aux.posicao = null; // Seta a posição da peça para null
                pecas[pos.linha, pos.coluna] = null; // Retira a peça da matriz de peças do tabuleiro
                return aux; // Retorna a peça retirada
            }
        }

        public bool posicaoValida(Posicao pos) {
            // Método para validar se uma posição é válida

            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas) {
                return false;
            }
            else {
                return true;
            }
        }

        public void validarPosicao (Posicao pos) {
            // Método para forçar tratativa caso a posição escolhida seja inválida

            if (!posicaoValida(pos)) {
                throw new TabuleiroException("Posição inválida!");
            }
        }
    }
}

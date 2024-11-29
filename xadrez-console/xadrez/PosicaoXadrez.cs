// Importação de namespace
using tabuleiro;

namespace xadrez {
    internal class PosicaoXadrez {

        // Atributos:
        public char coluna {  get; set; } // Coluna da posição de xadrez, exemplo: 'a', 'b', 'c'...
        public int linha { get; set; } // Linha da posição de xadrez, exemplo: 1, 2, 3...

        // Construtor:
        public PosicaoXadrez(char coluna, int linha) {
            this.coluna = coluna; // Coluna obrigatório como parâmetro
            this.linha = linha; // Linha obrigatório como parâmetro
        }

        // Métodos:
        public Posicao toPosicao() {
            // Método que converte uma posição de xadrez em uma posição de matriz, exemplo: a8 = (0,0)

            return new Posicao(8 - linha, coluna - 'a');
        }

        public override string ToString() {
            // Método que retorna o objeto como string

            return "" + coluna + linha;
        }
    }
}

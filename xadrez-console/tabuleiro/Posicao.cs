namespace tabuleiro {
    internal class Posicao {

        // Atributos:
        public int linha { get; set; } // Linha da posição
        public int coluna { get; set; } // Coluna da posição

        // Construtor:
        public Posicao(int linha, int coluna) {
            this.linha = linha; // Linha obrigatório como parâmetro
            this.coluna = coluna; // Coluna obrigatório como parâmetro
        }

        // Métodos:
        public void definirValores(int linha, int coluna) {
            // Método para definir valores da linha e coluna
            this.linha = linha;
            this.coluna = coluna;
        }

        public override string ToString() {
            // Método que passa o Objeto como string
            return linha + ", " + coluna;
        }
    }
}

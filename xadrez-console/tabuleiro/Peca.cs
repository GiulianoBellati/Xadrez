namespace tabuleiro {
    internal abstract class Peca { // Classe abstrata para ser base das peças de xadrez

        // Atributos:
        public Posicao posicao { get; set; } // Posição da peça
        public Cor cor { get; protected set; } // Cor da peça
        public int qteMovimentos {  get; protected set; } // Quantidade de movimentos da peça
        public Tabuleiro tab { get; protected set; } // Tabuleiro na qual a peça pertence

        // Construtor:
        public Peca(Cor cor, Tabuleiro tab) {
            this.posicao = null; // Posição inicia nula
            this.cor = cor; // Cor obrigatório como parâmetro
            this.tab = tab; // Tabuleiro obrigatório como parâmetro
            this.qteMovimentos = 0; // Quantidade de movimentos da peça inicia zerada
        }

        // Métodos:
        public void incrementarQteMovimento() {
            // Método para incrementar a quantidade de movimentos da peça
            qteMovimentos++;
        }

        public void decrementarQteMovimento() {
            // Método para decrementar a quantidade de movimentos da peça
            qteMovimentos--;
        }

        public bool existeMovimentosPossiveis() {
            // Método para validar se existe movimentos possíveis para a peça percorrendo a matriz "mat"
            bool[,] mat = movimentosPossiveis(); // Obtém matriz de movimentos possíveis
            for (int i = 0; i < tab.linhas; i++) {
                for (int j = 0; j < tab.colunas; j++) {
                    if (mat[i,j]) {
                        return true; // Existe movimentos possíveis
                    }
                }
            }
            return false; // Não existe movimentos possíveis
        }

        public bool movimentoPossivel(Posicao pos) {
            // Método para validar se determinada posição de destino é um movimento possível
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }

        // Método abstrato, cada peça terá seu próprio comportamento para verificar os possíveis movimentos
        public abstract bool[,] movimentosPossiveis();
    }
}

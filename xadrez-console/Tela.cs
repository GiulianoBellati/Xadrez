// Importação de namespaces
using tabuleiro;
using xadrez;

namespace xadrez_console {
    internal class Tela {

        public static void imprimirPartida (PartidaDeXadrez partida) {
            // Método para imprimir partida com todas as informações
            imprimirTabuleiro(partida.tab); // Imprime o tabuleiro da partida baseado no número de linhas e colunas
            Console.WriteLine(); // Pula uma linha
            imprimirPecasCapturadas(partida); // Imprime as peças capturadas pelas cores Amarela e Verde
            Console.WriteLine(); // Pula uma linha
            Console.WriteLine("Turno: " + partida.turno); // Imprime o turno da partida

            if (!partida.terminada) { // Se a partida não terminou:
                Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual); // Imprime qual cor deve jogar

                if (partida.xeque) { // Se a partida está em xeque:
                    Console.WriteLine("XEQUE!"); // Imprime que a partida está em xeque
                }
            }
            else { // Se a partida já terminou:
                Console.WriteLine("XEQUEMATE!"); // Imprime "XEQUEMATE"
                Console.WriteLine("Vencedor: " + partida.jogadorAtual); // Imprime o vencedor da partida
            }
        }

        public static void imprimirPecasCapturadas (PartidaDeXadrez partida) {
            // Método que imprime as peças capturados por ambas equipes
            Console.WriteLine("Peças capturadas:"); // Imprime "Peças capturadas"
            Console.Write("Amarelas: "); // Imprime "Amarelas: "
            ConsoleColor aux = Console.ForegroundColor; // Guarda na variável "aux" a cor de escrever no console, no caso Branco
            Console.ForegroundColor = ConsoleColor.Yellow; // Altera a cor de escrever no console para Amarela
            imprimirConjunto(partida.pecasCapturadas(Cor.Amarela)); // Imprime o conjunto de peças capturadas da cor Amarela
            Console.ForegroundColor = aux;  // Altera a cor de escrever no console para a cor da variável "aux", no caso Branco
            Console.WriteLine(); // Pula uma linha

            Console.Write("Verdes: "); // Imprime "Verdes: "
            Console.ForegroundColor = ConsoleColor.Green; // Altera a cor de escrever no console para Verde
            imprimirConjunto(partida.pecasCapturadas(Cor.Verde)); // Imprime o conjunto de peças capturadas da cor Verde
            Console.ForegroundColor = aux; // Altera a cor de escrever no console para a cor da variável "aux", no caso Branco
            Console.WriteLine(); // Pula uma linha
        }

        public static void imprimirConjunto (HashSet<Peca> conjunto) { 
            // Método que recebe uma coleção de peças e baseado nela imprime cada peça capturada entre colchetes
            Console.Write("[");
            foreach (Peca x in conjunto) {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void imprimirTabuleiro(Tabuleiro tab) {
            // Método que imprime o tabuleiro da partida baseado no número de linhas e colunas desse tabuleiro
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++) {
                    imprimirPeca(tab.peca(i, j)); // Imprime cada peça na sua posição
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis) {
            // Método que imprime o tabuleiro da partida baseado no número de linhas e colunas desse tabuleiro (com possiveís movimentações)
            ConsoleColor fundoOriginal = Console.BackgroundColor; // Guarda na variável "fundoOriginal" o fundo ativo, no caso Preto
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray; // Guarda na variável "fundoAlterado" o fundo da cor Cinza Escuro

            // Imprime o tabuleiro com as posições possíveis de movimentação obtida na matriz booleana "posicoesPossiveis", utilizando o "fundoAlterado" para as possíveis movimentações
            for (int i = 0; i < tab.linhas; i++) {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.colunas; j++) {
                    if (posicoesPossiveis[i,j]) {
                        Console.BackgroundColor = fundoAlterado; // Altera o fundo para Cinza Escuro
                    }
                    else {
                        Console.BackgroundColor = fundoOriginal; // Altera o fundo para Preto
                    }
                    imprimirPeca(tab.peca(i, j)); // Imprime a peça
                    Console.BackgroundColor = fundoOriginal; // Volta o fundo para Preto
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal; // Volta o fundo para Preto
        }

        public static PosicaoXadrez lerPosicaoXadrez() {
            // Método que realiza a leitura de uma posição de xadrez, exemplos: a1, a2, b3, b4, c5, d2...
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
            return new PosicaoXadrez(coluna, linha);
        }

        public static void imprimirPeca(Peca peca) {
            // Método que imprime as peças do tabuleiro ou hífen se não houver peça na posição da peça enviada como argumento
            if (peca == null) {
                Console.Write("- ");
            }
            else {
                ConsoleColor aux = Console.ForegroundColor; // Guarda na variável "aux" a cor de escrever no console, no caso Branco
                if (peca.cor == Cor.Verde) {
                    Console.ForegroundColor = ConsoleColor.Green; // Altera a cor de escrever no console para Verde
                    Console.Write(peca); // Imprime a peça
                    Console.ForegroundColor = aux; // Volta a cor de escrever no console para Branco
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Yellow;// Altera a cor de escrever no console para Amarela
                    Console.Write(peca); // Imprime a peça
                    Console.ForegroundColor = aux; // Volta a cor de escrever no console para Branco
                }
                Console.Write(" "); // Imprime um espaço entre as peças
            }
        }
    }
}

// Importação de namespaces
using tabuleiro;
using xadrez;

namespace xadrez_console {
    internal class Program {
        private static void Main(string[] args) {

            try { // Bloco try catch para tratativa de exceções personalizadas em caso de erro:

                PartidaDeXadrez partida = new PartidaDeXadrez(); // Instanciando uma nova partida de xadrez

                while (!partida.terminada) { // Enquanto a partida não terminar:

                    try { // Bloco try catch para tratativa de exceções personalizadas em caso de erro:

                        Console.Clear(); // Limpa o console
                        Tela.imprimirPartida(partida); // Imprime a partida atual no console

                        Console.Write("\nOrigem: "); // Escreve "Origem: " no console
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao(); // Lê uma posição de xadrez enviada pelo usuário e converte ela para uma posição de matriz, guardando o resultado na variável origem
                        partida.validarPosicaoDeOrigem(origem); // Valida se a posição de origem é válida

                        bool[,] posicoesPossiveis = partida.tab.peca(origem).movimentosPossiveis(); // Cria uma matriz booleana e preenche essa matriz para identificar todos os movimentos possíveis da peça de origem

                        Console.Clear(); // Limpa o console
                        Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis); // Imprime o tabuleiro com as opções possíveis de movimentos para a peça de origem

                        Console.Write("\nDestino: "); // Escreve "Destino: " no console
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao(); // Lê uma posição de xadrez enviada pelo usuário e converte ela para uma posição de matriz, guardando o resultado na variável destino
                        partida.validarPosicaoDeDestino(origem, destino); // Valida se a posição de destino é válida

                        partida.realizaJogada(origem, destino); // Realiza a jogada baseada nas posições de origem e destino que foram enviadas pelo usuário
                    }
                    catch (TabuleiroException e) { // Caso tenha algum erro:
                        Console.WriteLine(e.Message); // Imprime a mensagem de erro personalizada
                        Console.ReadLine(); // Aguarda um Enter para reiniciar a jogada
                    }
                }

                // Após o fim da partida:
                Console.Clear(); // Limpa o console
                Tela.imprimirPartida(partida); // Imprime a partida pela última vez, já com o seu vencedor
            }
            catch (TabuleiroException e) { // Caso tenha algum erro:
                Console.WriteLine(e.Message); // Imprime a mensagem de erro personalizada
            }

            Console.ReadLine(); // Aguarda um Enter para finalizar a execução do programa
        }
    }
}
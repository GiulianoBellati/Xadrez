using tabuleiro;
using xadrez;

namespace xadrez_console {
    internal class Program {
        private static void Main(string[] args) {

            try {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.colocarPeca(new Torre(Cor.Amarela, tab), new Posicao(0, 0));
                tab.colocarPeca(new Torre(Cor.Amarela, tab), new Posicao(1, 3));
                tab.colocarPeca(new Rei(Cor.Amarela, tab), new Posicao(0, 2));

                tab.colocarPeca(new Torre(Cor.Verde, tab), new Posicao(3, 5));

                Tela.imprimirTabuleiro(tab);
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
            
        }
    }
}
// Importação de namespaces
using tabuleiro;

namespace xadrez {
    internal class PartidaDeXadrez {

        // Atributos:
        public Tabuleiro tab { get; private set; } // Tabuleiro da partida
        public int turno { get; private set; } // Turno da partida
        public Cor jogadorAtual { get; private set; } // Cor do jogador atual, que realizará a jogada
        public bool terminada { get; private set; } // Valida se a partida está ou não terminada
        private HashSet<Peca> pecas; // Coleção de peças em jogo da partida
        private HashSet<Peca> capturadas; // Coleção de peças capturadas da partida
        public bool xeque { get; private set; } // Valida se a partida está ou não em xeque
        public Peca vulneravelEnPassant { get; private set; } // Peça vulnerável a receber uma jogada especial en passant

        // Construtor:
        public PartidaDeXadrez() {
            tab = new Tabuleiro(8, 8); // Inicia o tabuleiro com uma dimensão de 8 linhas e 8 colunas
            turno = 1; // Inicia com turno 1
            jogadorAtual = Cor.Amarela; // Inicia pela equipe Amarela
            terminada = false; // Inicia com a partida não terminada
            xeque = false; // Inicia com a partida fora de xeque
            vulneravelEnPassant = null; // Inicia com nenhuma peça vulnerável a receber uma jogada especial en passant
            pecas = new HashSet<Peca>(); // Inicia com uma coleção de peças vazia
            capturadas = new HashSet<Peca>(); // Inicia com uma coleção de peças capturadas vazia
            colocarPecas(); // Coloca as peças em suas posições iniciais
        }

        // Métodos:
        public Peca executaMovimento(Posicao origem, Posicao destino) {
            // Método que executa um movimento de uma posição de origem para uma posição de destino

            Peca p = tab.retirarPeca(origem); // Retira a peça da origem
            p.incrementarQteMovimento(); // Incrementa a quantidade de movimento dessa peça
            Peca pecaCapturada = tab.retirarPeca(destino); // Inserindo na variável pecaCapturada a peça que anteriormente estava na posição de destino ou null
            tab.colocarPeca(p, destino); // Coloca a peça que anteriormente estava na origem na posição de destino
            if (pecaCapturada != null) { // Se alguma peça foi capturada:
                capturadas.Add(pecaCapturada); // Adiciona a peça capturada na coleção capturadas
            }

            // Jogada especial roque pequeno:
            if (p is Rei && destino.coluna == origem.coluna + 2) { // Se for uma jogada roque pequeno:
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); // Instancia uma origem para a torre aliada
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); // Instancia um destino para a torre aliada
                Peca T = tab.retirarPeca(origemT); // Retira a peça da origem
                T.incrementarQteMovimento(); // Incrementa a quantidade de movimento dessa peça
                tab.colocarPeca(T, destinoT); // Coloca a peça que anteriormente estava na origem na posição de destino
            }
            // Jogada especial roque grande:
            if (p is Rei && destino.coluna == origem.coluna - 2) { // Se for uma jogada roque grande:
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); // Instancia uma origem para a torre aliada
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1); // Instancia um destino para a torre aliada
                Peca T = tab.retirarPeca(origemT); // Retira a peça da origem
                T.incrementarQteMovimento(); // Incrementa a quantidade de movimento dessa peça
                tab.colocarPeca(T, destinoT); // Coloca a peça que anteriormente estava na origem na posição de destino
            }

            // Jogada especial en passant:
            if (p is Peao && origem.coluna != destino.coluna && pecaCapturada == null) { // Se for uma jogada en passant:
                Posicao posP;
                if (p.cor == Cor.Amarela) { // Definindo destino da peça que sofreu a jogada especial en passant caso seja Amarela:
                    posP = new Posicao(destino.linha + 1, destino.coluna);
                }
                else { // Definindo destino da peça que sofreu a jogada especial en passant caso seja Verde:
                    posP = new Posicao(destino.linha - 1, destino.coluna);
                }
                pecaCapturada = tab.retirarPeca(posP); // Inserindo na variável pecaCapturada a peça que sofreu a jogada especial en passant
                capturadas.Add(pecaCapturada); // Adiciona a peça capturada na coleção capturadas
            }

            return pecaCapturada; // Retorna a peça que foi capturada ou null
        }

        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            // Método para desfazer o movimento feito caso o jogador esteja se colocando em xeque

            Peca p = tab.retirarPeca(destino); // Retira a peça movida
            p.decrementarQteMovimento(); // Decrementa a quantidade de movimentos
            if (pecaCapturada != null) { // Se capturou alguma peça:
                tab.colocarPeca(pecaCapturada, destino); // Coloca a peça que foi capturada na posição em que ela estava anteriormente
                capturadas.Remove(pecaCapturada); // Remove a peça capturada da coleção capturadas
            }
            tab.colocarPeca(p, origem); // Coloca novamente a peça movida em sua origem

            // Jogada especial roque pequeno:
            if (p is Rei && destino.coluna == origem.coluna + 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3); // Instancia a posição de origem da Torre que foi movida
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1); // Instancia a posição de destino da Torre que foi movida
                Peca T = tab.retirarPeca(destinoT); // Retira a torre movida do destino
                T.decrementarQteMovimento(); // Decrementa a quantidade de movimentos
                tab.colocarPeca(T, origemT); // Coloca novamente a torre movida em sua origem
            }
            // Jogada especial roque grande:
            if (p is Rei && destino.coluna == origem.coluna - 2) {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4); // Instancia a posição de origem da Torre que foi movida
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1); // Instancia a posição de destino da Torre que foi movida
                Peca T = tab.retirarPeca(destinoT); // Retira a torre movida do destino
                T.decrementarQteMovimento(); // Decrementa a quantidade de movimentos
                tab.colocarPeca(T, origemT); // Coloca novamente a torre movida em sua origem
            }

            // Jogada especial en passant:
            if (p is Peao && origem.coluna != destino.coluna && pecaCapturada == vulneravelEnPassant) {
                Peca peao = tab.retirarPeca(destino); // Retira o peão que sofreu o en passant do destino
                Posicao posP;
                if (p.cor == Cor.Amarela) { // Identificando a origem do Peão que sofreu o en passant caso seja Amarela:
                    posP = new Posicao(3, destino.coluna);
                }
                else {  // Identificando a origem do Peão que sofreu o en passant caso seja Verde:
                    posP = new Posicao(4, destino.coluna);
                }
                tab.colocarPeca(peao, posP); // Coloca o peão que sofreu o en passant em sua posição de origem
            }
        }

        public void realizaJogada(Posicao origem, Posicao destino) {
            // Método que realiza a jogada completa

            Peca pecaCapturada = executaMovimento(origem, destino); // Executa o movimento desejado, já armazenando na variável pecaCapturada uma peça capturada ou null

            if (estaEmXeque(jogadorAtual)) { // Se o jogador atual se colocou em xeque:
                desfazMovimento(origem, destino, pecaCapturada); // Desfaz o movimento
                throw new TabuleiroException("Você não pode se colocar em xeque!"); // Executa uma exceção dizendo que o jogador não pode se colocar em xeque
            }

            Peca p = tab.peca(destino); // Armazena a peça que foi movida

            // Jogada especial promoção (transforma o peão que chegou na última casa do lado adversário em uma dama):
            if (p is Peao) {
                if ((p.cor == Cor.Amarela && destino.linha == 0) || (p.cor == Cor.Verde && destino.linha == 7)) {
                    p = tab.retirarPeca(destino); // Retira o peão
                    pecas.Remove(p); // Remove o peão do conjunto de peças
                    Peca dama = new Dama(p.cor, tab); // Instancia uma nova dama
                    tab.colocarPeca(dama, destino); // Coloca a dama na casa em que o peão estava
                    pecas.Add(dama); // Adiciona a dama na coleção de peças
                }
            }

            // Valida se o jogador atual está em xeque
            if (estaEmXeque(adversaria(jogadorAtual))) {
                xeque = true;
            }
            else {
                xeque = false;
            }

            // Valida se o jogador atual está em xequemate e finaliza a partida se verdadeiro
            if (testeXequeMate(adversaria(jogadorAtual))) {
                terminada = true;
            }
            else {
                turno++;
                mudaJogador();
            }

            // Jogada especial en passant:
            // Valida se há uma peça vulnerável en passant e atribui essa peça ou null no atributo vulneravelEnPassant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha == origem.linha + 2)) {
                vulneravelEnPassant = p;
            }
            else {
                vulneravelEnPassant = null;
            }
        }

        public void validarPosicaoDeOrigem(Posicao pos) {
            // Método que verifica se é necessário disparar alguma tratativa de exceção para a peça selecionada para movimentação 

            if (tab.peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino) {
            // Método que verifica se é necessário disparar alguma tratativa de exceção para a posição de destino selecionada 

            if (!tab.peca(origem).movimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void mudaJogador() {
            // Método para alterar o jogador atual ao fim de cada jogada

            if (jogadorAtual == Cor.Amarela) {
                jogadorAtual = Cor.Verde;
            }
            else {
                jogadorAtual = Cor.Amarela;
            }
        }

        public HashSet<Peca> pecasCapturadas(Cor cor) {
            // Método que retorna apenas as peças capturadas da cor passada como parâmetro

            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor) {
            // Método que retorna apenas as peças em jogo da cor passada como parâmetro

            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));
            return aux;
        }

        private Cor adversaria(Cor cor) {
            // Método que identifica qual a cor que está aguardando a jogada, o adversário

            if (cor == Cor.Amarela) {
                return Cor.Verde;
            }
            else {
                return Cor.Amarela;
            }
        }

        private Peca rei(Cor cor) {
            // Método que verifica se há rei na equipe da cor passada como parâmetro

            foreach (Peca x in pecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool estaEmXeque(Cor cor) {
            // Método que verifica se uma cor passada como parâmetro está em xeque

            Peca R = rei(cor);
            if (R == null) {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");
            }

            foreach (Peca x in pecasEmJogo(adversaria(cor))) {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool testeXequeMate(Cor cor) {
            // Método que verifica se uma cor passada como parâmetro está em xequemate

            if (!estaEmXeque(cor)) {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++) {
                    for (int j = 0; j < tab.colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca) {
            // Método que insere no tabuleiro uma peça em determinada coluna e linha

            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao()); // Insere no tabuleiro uma peça convertendo a posição do xadrez para uma posição de matriz
            pecas.Add(peca); // Adiciona a peça inserida na coleção pecas
        }

        private void colocarPecas() {
            // Método para instanciar as peças Amarelas e Verdes com suas posições iniciais do xadrez

            colocarNovaPeca('a', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('b', 1, new Cavalo(Cor.Amarela, tab));
            colocarNovaPeca('c', 1, new Bispo(Cor.Amarela, tab));
            colocarNovaPeca('d', 1, new Dama(Cor.Amarela, tab));
            colocarNovaPeca('e', 1, new Rei(Cor.Amarela, tab, this));
            colocarNovaPeca('f', 1, new Bispo(Cor.Amarela, tab));
            colocarNovaPeca('g', 1, new Cavalo(Cor.Amarela, tab));
            colocarNovaPeca('h', 1, new Torre(Cor.Amarela, tab));
            colocarNovaPeca('a', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('b', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('c', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('d', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('e', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('f', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('g', 2, new Peao(Cor.Amarela, tab, this));
            colocarNovaPeca('h', 2, new Peao(Cor.Amarela, tab, this));

            colocarNovaPeca('a', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('b', 8, new Cavalo(Cor.Verde, tab));
            colocarNovaPeca('c', 8, new Bispo(Cor.Verde, tab));
            colocarNovaPeca('d', 8, new Dama(Cor.Verde, tab));
            colocarNovaPeca('e', 8, new Rei(Cor.Verde, tab, this));
            colocarNovaPeca('f', 8, new Bispo(Cor.Verde, tab));
            colocarNovaPeca('g', 8, new Cavalo(Cor.Verde, tab));
            colocarNovaPeca('h', 8, new Torre(Cor.Verde, tab));
            colocarNovaPeca('a', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('b', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('c', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('d', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('e', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('f', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('g', 7, new Peao(Cor.Verde, tab, this));
            colocarNovaPeca('h', 7, new Peao(Cor.Verde, tab, this));
        }
    }
}

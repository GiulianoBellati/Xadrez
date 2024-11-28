namespace tabuleiro {
    internal class TabuleiroException : Exception { // Classe para tratativas de exceção personalizada
        public TabuleiroException(string msg) : base(msg) { // Construtor passando como parâmetro uma mensagem personalizada da exceção
        }
    }
}

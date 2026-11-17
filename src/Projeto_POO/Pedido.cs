using System.Text;

namespace XulambsFoods {
    public abstract class Pedido {
        private static int s_ultimoPedido=0;
        private int _idPedido;
        private DateOnly _data;
        protected LinkedList<IProduto> _produtos;
        private bool _aberto;
        private double _desconto;

        public Pedido() {
            _produtos = new LinkedList<IProduto>();
            _aberto = true;
            _data =  DateOnly.FromDateTime(DateTime.Now);
            s_ultimoPedido++;
            _idPedido = _data.Day*100 + s_ultimoPedido;
            _desconto = 0d;
        }

        
        protected virtual void PodeAdicionar() {
            if (!_aberto)
                throw new PedidoFechadoException();
        } //virtual: autorização para ser sobrescrito.

        [Obsolete("Prefira usar Adicionar(Produto produto)")]
        public int Adicionar(Pizza pizza) {
            if (pizza == null)
                throw new ArgumentNullException("Pizza não foi criada");
            
            PodeAdicionar();
            _produtos.AddLast(pizza);
            return _produtos.Count;
        }

        public int Adicionar(IProduto produto) {
            if (produto == null)
                throw new ArgumentNullException("Produto não foi escolhido");

            PodeAdicionar();
            _produtos.AddLast(produto);
            return _produtos.Count;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public abstract double PrecoAPagar();

        protected double GetDesconto()
        {
            return _desconto;
        }

        public double AplicarDesconto(double valor)
        {
            if (valor < 0)
                throw new ArgumentOutOfRangeException();
            if (_desconto != 0)
                throw new InvalidOperationException();

            _desconto = valor;
            return _desconto;
        }

        protected double ValorItens(){
            double preco = 0d;
            foreach (IProduto prod in _produtos) {
                preco += prod.CalcularValorFinal();
            }
            return preco;
        }

        protected string CabecalhoPedido()
        {
            StringBuilder relat = new StringBuilder($"Pedido nº{_idPedido} - {_data}\n");
            string estado = "fechado";
            if (_aberto)
                estado = "aberto";
            relat.AppendLine($"Pedido {estado}.");
            int i = 0;
            foreach (IProduto prod in _produtos)
            {
                relat.AppendLine($"{++i:D2} {prod.ToString()}");
            }
            relat.AppendLine($"\nITENS: {ValorItens():C2}");
            relat.AppendLine($"DESCONTO: {_desconto:C2}");
            return relat.ToString();
        }

        protected abstract string RodapePedido();
        
        public override string ToString() {
            return $"{CabecalhoPedido()}\n{RodapePedido()}";
        }

        public override bool Equals(object? obj) {
            Pedido outro = obj as Pedido;
            return this._idPedido == outro._idPedido;
        }

        public override int GetHashCode() {
            return _idPedido;
        }
    }
}

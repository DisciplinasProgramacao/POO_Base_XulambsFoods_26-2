using System.Text;

namespace XulambsFoods {
    public class Pedido {
        private static int s_ultimoPedido=0;
        private int _idPedido;
        private DateOnly _data;
        protected LinkedList<Pizza> _pizzas;
        private bool _aberto;

        public Pedido() {
            _pizzas = new LinkedList<Pizza>();
            _aberto = true;
            _data =  DateOnly.FromDateTime(DateTime.Now);
            s_ultimoPedido++;
            _idPedido = _data.Day*100 + s_ultimoPedido;
        }

        protected virtual bool PodeAdicionar() {
            return _aberto;
        } //virtual: autorização para ser sobrescrito.

        public int GetID() {
            return _idPedido;
        }

        public int Adicionar(Pizza pizza) {
            if (PodeAdicionar() && pizza != null)
                _pizzas.AddLast(pizza);
            return _pizzas.Count;
        }

        public void FecharPedido() {
            _aberto = false;
        }

        public virtual double PrecoAPagar() {
            double preco = 0d;
            foreach (Pizza pizza in _pizzas) {
                preco += pizza.CalcularValorFinal();
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
            foreach (Pizza pizza in _pizzas)
            {
                relat.AppendLine($"{++i:D2} {pizza.GerarCupom()}");
            }
            return relat.ToString();
        }

        protected virtual string RodapePedido()
        {
            return $"\nTOTAL DO PEDIDO: {PrecoAPagar():C2}";
        }

        public string Relatorio() {
            return $"{CabecalhoPedido()}\n{RodapePedido()}";
        }
    }
}

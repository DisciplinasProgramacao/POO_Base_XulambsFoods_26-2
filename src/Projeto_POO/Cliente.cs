using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class Cliente
    {
        private int _id;
        private string _nome;
        private IFidelidade _categoria;
        private Stack<Pedido> _pedidos;

        public Cliente(string nome, int id)
        {
            _id = id;
            _nome = nome;
            _pedidos = new Stack<Pedido>();
            _categoria = new XulambsJunior();
        }

        public int RegistrarPedido(Pedido novo)
        {
            _pedidos.Push(novo);
            double desconto = _categoria.DefinirDesconto(novo);
            novo.AplicarDesconto(desconto);
            return _pedidos.Count;
        }
        
        public double TotalGasto()
        {
            double valor = 0d;
            foreach (Pedido p in _pedidos)
            {
                valor += p.PrecoAPagar();
            }
            return valor;
        }

        public string RelatorioPedidos()
        {
            StringBuilder relat = new StringBuilder(ToString());
            
            foreach (Pedido p in _pedidos)
            {
                relat.Append($"\n{p}");
            }
            return relat.ToString();
        }

        public override string ToString()
        {
            return $"{_nome} ({_id}) - Cliente {_categoria} - Total Gasto: {TotalGasto():C2}";
        }

        public override int GetHashCode()
        {
            return _id;
        }

        public void AtualizarCategoria()
        {
            _categoria = IFidelidade.DefinirCategoria(_pedidos);
        }
    }
}

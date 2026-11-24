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
            if (!nome.Contains(" "))
                throw new ArgumentException("Nome e sobrenome são obrigatórios");
            if (id <= 0)
                throw new ArgumentOutOfRangeException("Só números positivos");

            _nome = nome;
            _id = id;
            _categoria = new XulambsJunior();
            _pedidos = new Stack<Pedido>();
        }

        public void AtualizarCategoria()
        {
            _categoria = .....
        }

        public int RegistrarPedido(Pedido novo)
        {
            if (novo == null)
                throw new ArgumentNullException();
            _pedidos.Push(novo);
            double desconto = _categoria.DescontoPedido(novo);
            novo.AplicarDesconto(desconto);
            return _pedidos.Count;
        }

        public double TotalGasto()
        {
            double valor = 0;
            foreach (Pedido p in _pedidos)
            {
                valor += p.PrecoAPagar();
            }
            return valor;
        }

        public string RelatorioPedidos()
        {
            StringBuilder relatorio = new StringBuilder(ToString());
            foreach (Pedido p in _pedidos)
            {
                relatorio.Append($"\n{p}");
            }
            return relatorio.ToString();
        }

        public override string ToString()
        {
            return $"{_nome} ({_id}) - Total Gasto: {TotalGasto():C2}";
        }

        public override int GetHashCode()
        {
            return _id;
        }
    }
}

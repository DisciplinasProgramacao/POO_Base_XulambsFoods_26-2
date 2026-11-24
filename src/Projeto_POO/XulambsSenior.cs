using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsSenior : IFidelidade
    {
        private const double Proporcao = 0.15;
        private const int PedidosParaCupom = 6;
        private int quantPedidos;
        private const double ValorCupom = 10;

        public double DescontoPedido(Pedido pedido)
        {
            double desconto = pedido.PrecoAPagar() * Proporcao;
            if (quantPedidos % PedidosParaCupom == 0)
                desconto += ValorCupom;
            quantPedidos++;
            return desconto;
        }
    }
}

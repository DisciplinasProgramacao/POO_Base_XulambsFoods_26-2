using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsSenior : IFidelidade
    {
        private const double DescontoProporcional = 0.15;
        private const int PedidosParaCupom = 6;
        private const double ValorCupom = 10;

        private int quantPedidos = 0;

        public double DefinirDesconto(Pedido pedido)
        {
            double desconto = pedido.PrecoAPagar() * DescontoProporcional;
            if (quantPedidos % PedidosParaCupom == 0)
                desconto += ValorCupom;
            quantPedidos++;
            return desconto;
        }
    }
}

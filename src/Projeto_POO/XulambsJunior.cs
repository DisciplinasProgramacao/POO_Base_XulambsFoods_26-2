using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsJunior : IFidelidade
    {
        private const double MinPedido = 50;
        private const double DescontoFixo = 2.5;

        public double DefinirDesconto(Pedido pedido)
        {
            double preco = pedido.PrecoAPagar();
            return preco > MinPedido ? DescontoFixo : 0d;   
        }
    }
}

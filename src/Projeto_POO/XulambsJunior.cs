using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsJunior : IFidelidade
    {
        private const double ValorMinimo = 50d;
        private const double DescontoFixo = 2.5;

        public double DescontoPedido(Pedido pedido)
        {
            return (pedido.PrecoAPagar() > ValorMinimo) ? DescontoFixo : 0d;
        }
    }
}

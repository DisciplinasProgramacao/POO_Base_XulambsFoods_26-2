using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsPleno : IFidelidade
    {
        private const double ProporcaoDesconto = 0.1;

        public double DescontoPedido(Pedido pedido)
        {
            return pedido.PrecoAPagar() * ProporcaoDesconto;
        }
    }
}

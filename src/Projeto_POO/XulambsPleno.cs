using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class XulambsPleno : IFidelidade
    {
        private const double DescontoProporcional = 0.1;

        public double DefinirDesconto(Pedido pedido)
        {
            return pedido.PrecoAPagar() * DescontoProporcional;
        }

        public override string ToString() {
            return "Xulambs Pleno";
        }
    }
}

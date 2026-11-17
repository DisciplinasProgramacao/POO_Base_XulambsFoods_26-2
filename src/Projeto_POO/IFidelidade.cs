using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public interface IFidelidade
    {
        public abstract double DefinirDesconto(Pedido pedido);

        public static IFidelidade DefinirCategoria(Stack<Pedido> pedidos)
        {
            double[] valores =  { 0, 29, 44, Double.PositiveInfinity };
            int[] quantidades = { 0, 10, 20, Int32.MaxValue};

            double quantidade = pedidos.Count;
            double valorMedio = 0;
            foreach (Pedido p in pedidos)
            {
                valorMedio += p.PrecoAPagar();
            }
            valorMedio = valorMedio / quantidade;
            int pos = 3;
            while(quantidade < quantidades[pos] ||
                  valorMedio < valores[pos])
            {
                pos--;
            }
            return pos switch
            {
                0 => new XulambsJunior(),
                1 => new XulambsPleno(),
                2 => new XulambsSenior()
            };
        }
    }
}

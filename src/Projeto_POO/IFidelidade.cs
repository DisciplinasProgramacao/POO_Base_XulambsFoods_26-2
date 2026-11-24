using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public interface IFidelidade
    {
        public abstract double DescontoPedido(Pedido pedido);

        public static IFidelidade DefinirCategoria(Stack<Pedido> ped)
        {
            int quantidade = ped.Count;
            double media = 0;
            foreach (Pedido p in ped)
            {
                media += p.PrecoAPagar();
            }
            media = media / quantidade;
            return classificacao(quantidade, media) switch
            {
                0 => new XulambsJunior(),
                1 => new XulambsPleno(),
                2 => new XulambsSenior()
            };
        }
        private static int classificacao(int quant, double media)
        {
            double[] valores =  { 0, 29, 44, Double.MaxValue };
            int[] quantidades = { 0, 10, 20, Int32.MaxValue };

            int pos = 3;
            while (quant < quantidades[pos] || media < valores[pos])
                pos--;
            return pos;
        }

    }
}

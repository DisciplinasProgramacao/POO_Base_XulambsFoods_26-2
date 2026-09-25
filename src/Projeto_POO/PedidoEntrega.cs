using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class PedidoEntrega : Pedido {
        private const int MaxEntrega = 8;
        private static readonly SortedList<double, double>
            ValorEntrega;
        private double _distanciaEntrega;

        static PedidoEntrega() {
            //chave: distância máxima // valor: taxa para aquela distância
            ValorEntrega = new SortedList<double, double>();
            ValorEntrega.Add(8, 5);
            ValorEntrega.Add(4, 0);
            ValorEntrega.Add(double.PositiveInfinity, 8);
        }

        public PedidoEntrega(double distancia) : base()
        {
            if (distancia <= 0)
                distancia = 0.01;
            _distanciaEntrega = distancia;
        }

        protected override bool PodeAdicionar() { //override: modificação da regra original
            return base.PodeAdicionar() && _pizzas.Count < MaxEntrega;
        }

        private double ValorTaxa() {
            int posicao = 0;
            while (_distanciaEntrega > ValorEntrega.GetKeyAtIndex(posicao))
                posicao++;
            return ValorEntrega.GetValueAtIndex(posicao);
        }

        public override double PrecoAPagar() {
            double valorPizzas = base.PrecoAPagar();
            return valorPizzas + ValorTaxa();
        }

        public override string Relatorio() {
            string relatorioOriginal = base.Relatorio();
            return relatorioOriginal + $"\nTAXA DE ENTREGA: {ValorTaxa():C2} ({_distanciaEntrega}km).";
        }

    }
}
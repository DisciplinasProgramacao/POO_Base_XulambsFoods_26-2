using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class PedidoEntrega : Pedido {
        private const int MaxEntrega = 8;

        private ETaxaEntrega _taxaEntrega;
        private double _distanciaEntrega;

       
        public PedidoEntrega(double distancia) : base()
        {
            if (distancia <= 0)
                distancia = 0.01;
            _distanciaEntrega = distancia;
            _taxaEntrega = _distanciaEntrega.DefinirEntrega();
        }

        protected override bool PodeAdicionar() { //override: modificação da regra original
            return base.PodeAdicionar() && _pizzas.Count < MaxEntrega;
        }

        private double ValorTaxa() {
            return _taxaEntrega.ValorTaxa();
        }

        public override double PrecoAPagar() {
            double valorPizzas = base.PrecoAPagar();
            return valorPizzas + ValorTaxa();
        }

        protected override string RodapePedido()
        {
            StringBuilder relat = new StringBuilder();
            relat.AppendLine($"TAXA DE ENTREGA: {ValorTaxa():C2} ({_distanciaEntrega}km).");
            relat.AppendLine($"\nTOTAL DO PEDIDO: {PrecoAPagar():C2}");
            return relat.ToString();
        }
    }
}
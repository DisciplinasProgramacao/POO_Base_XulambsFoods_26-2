using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace XulambsFoods
{
    public class Sanduiche : IProduto, IPersonalizavel
    {
        private const double ValorCombo = 7d;
        private const double PrecoBase = 18d;
        private const double ValorPorAdicional = 3d;
        private const int MaxAdicionais = 5;


        private int _adicionais;
        private bool _combo;

        private double ValorAdicionais()
        {
            return _adicionais * ValorPorAdicional;
        }
        public int AdicionarIngredientes(int quantos)
        {
            if (quantos < 0)
                throw new ArgumentException("Adicionais não podem ser negativos.");
            if (_adicionais + quantos > MaxAdicionais)
                throw new InvalidOperationException($"Máximo de adicionais: {MaxAdicionais}");
          
            _adicionais += quantos;
            return _adicionais;
        }

        public double CalcularValorFinal()
        {
            double precoBase = PrecoBase + ValorAdicionais();
            if (_combo)
                precoBase += ValorCombo;
            return precoBase;
        }

        public bool SetCombo(bool combo)
        {
            _combo = combo;
            return _combo;
        }

        public override string ToString()
        {
            StringBuilder cupom = new StringBuilder();

            cupom.AppendLine($"Sanduíche " + (_combo ? "com fritas." : "."));
            cupom.AppendLine($"\tValor: {PrecoBase:C2}");
            if(_combo)
                cupom.AppendLine($"\tFritas: {ValorCombo:C2}");

            cupom.AppendLine($"\t{_adicionais} adicionais: {ValorAdicionais():C2}");
            
            cupom.AppendLine($"\n\tVALOR: {CalcularValorFinal():C2}");
            cupom.Append("================");
            return cupom.ToString();
        }
    }
}

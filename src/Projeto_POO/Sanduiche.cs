using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class Sanduiche : IProduto, IPersonalizavel
    {
        private int _adicionais;
        public int AdicionarIngredientes(int quantos)
        {
            _adicionais += quantos;
            return _adicionais;
        }

        public double CalcularValorFinal()
        {
            return 18 + 3 * _adicionais;
        }
    }
}

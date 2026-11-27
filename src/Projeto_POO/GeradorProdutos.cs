using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods {
    public class GeradorProdutos {
        static Random sorteio = new Random(42);

        public  static IProduto criar(int tipo) {
            return tipo switch  {
                 1 => criarPizza(),
                 2 => criarSanduiche(),              
                 3 or 4 => criarSobremesa(),
                _ => null,
            };
        }

        static Pizza criarPizza() {
            int maxIngredientes = 8;
            Pizza p = new Pizza();
            EBorda[] valores = Enum.GetValues<EBorda>();
            int borda = sorteio.Next(valores.Length);
            p.AdicionarBorda(valores[borda]);
            int adicionais = sorteio.Next(0, maxIngredientes + 1);
            p.AdicionarIngredientes(adicionais);
            return p;
        }

        static Sanduiche criarSanduiche() {
            int maxIngredientes = 5;
            Sanduiche s = new Sanduiche();
            int combo = sorteio.Next(1000);
            bool ehCombo = combo > 728;
            int adicionais = sorteio.Next(0, maxIngredientes + 1);
            s.AdicionarIngredientes(adicionais);
            s.SetCombo(ehCombo);
            return s;
        }

        static Sobremesa criarSobremesa() {
            ESobremesa[] valores = Enum.GetValues<ESobremesa>();
            int pos = sorteio.Next(valores.Length);
            return new Sobremesa(valores[pos]);
        }
    }
}

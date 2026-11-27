using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods
{
    public class Dados<T>
    {
        private LinkedList<T> meusDados;

        public Dados()
        {
            meusDados = new LinkedList<T>();
        }

        public Dados(ICollection<T> lista) {
            meusDados = new LinkedList<T>(lista);
        }

        public int Adicionar(T novoElemento)
        {
            if (novoElemento == null)
                throw new ArgumentNullException();
            meusDados.AddLast(novoElemento);
            return meusDados.Count;
        }

        public T Localizar(int codigo)
        {
            T localizado = default;
            for (int i = 0; i < meusDados.Count && localizado == null; i++)
            {
                T candidato = meusDados.ElementAt(i);
                if (candidato.GetHashCode() == codigo)
                    localizado = candidato;
            }
            return localizado;
        }

        public override string ToString()
        {
            return Relatorio((t) => t.ToString());
        }

        public void Atualizar(Action<T> metodo)
        {
            foreach (T dado in meusDados)
            {
                metodo.Invoke(dado);
            }
        }

        public double Somar(Func<T, double> funcao) {
            return meusDados.Sum(funcao);

            //double valor = 0d;
            //foreach(T dado in meusDados) {
            //    valor += funcao.Invoke(dado);
            //}
            //return valor;
        }

        public LinkedList<T> Filtrar(Predicate<T> condicao) {
            LinkedList<T> filtro = new LinkedList<T>();
            foreach (T dado in meusDados) {
                if (condicao.Invoke(dado))
                    filtro.AddLast(dado);
            }
            return filtro;
        }

        public string Relatorio(Func<T, string> relatorio) {
            StringBuilder sb = new StringBuilder(); ;
            foreach (T dado in meusDados) {
                sb.AppendLine($"{relatorio.Invoke(dado)}");
            };
            return sb.ToString(); ;
        }

        public string RelatorioFiltrado(Predicate<T> condicao, 
                                        Func<T, string> relatorio
                                        ) {
            Dados<T> listaFiltrada = new Dados<T>(Filtrar(condicao));
            return Relatorio(relatorio);

            //StringBuilder sb = new StringBuilder();
            //foreach (T dado in meusDados) {
            //    if (condicao.Invoke(dado)) {
            //        string pedaco = relatorio.Invoke(dado);
            //        sb.AppendLine(pedaco);
            //    }
            //}
            //return sb.ToString();
        }

        public T Maior(Comparison<T> comparacao) {
            T maior = meusDados.ElementAt(0);
            foreach (T dado in meusDados) {
                if (comparacao.Invoke(dado, maior) > 0)
                    maior = dado;
            }
            return maior;
        }

        public T Maiorzao(Comparison<T> comparacao) {
            Comparer<T> comparador = Comparer<T>.Create(comparacao);

            return meusDados.Max(comparador);
        }
    }
}

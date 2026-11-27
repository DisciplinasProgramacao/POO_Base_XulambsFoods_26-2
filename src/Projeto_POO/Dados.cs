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
            StringBuilder sb = new StringBuilder(); ;
            foreach(T dado in meusDados){
                sb.AppendLine($"{dado}");
            };
            return sb.ToString(); ;
        }

        public void Atualizar(Action<T> metodo)
        {
            foreach (T dado in meusDados)
            {
                metodo.Invoke(dado);
            }
        }
    }
}

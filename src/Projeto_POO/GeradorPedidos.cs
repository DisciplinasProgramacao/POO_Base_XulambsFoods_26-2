using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class GeradorPedidos {
        static Random sorteio = new Random(42);

        public static List<Pedido> gerarPedidos(DateTime dataInicio, DateTime dataFim, List<Cliente> clientes, int fator) {
            int quantidade = clientes.Count * fator;
            int periodo = (int)dataFim.Subtract(dataInicio).TotalDays;
            int porDia = (quantidade / periodo) + 1;

            List<Pedido> pedidos = new List<Pedido>((int)Math.Ceiling(quantidade * 1.1));
            DateTime dataPedido = dataInicio;
            for (int i = 0; i < quantidade; i++) {
                Pedido novo = new PedidoLocal(dataPedido);
                int produtos = sorteio.Next(1, 6);
                for (int j = 0; j < produtos; j++) {
                    int qualProduto = sorteio.Next(1, 5);
                    IProduto produtoNovo = GeradorProdutos.criar(qualProduto);
                    novo.Adicionar(produtoNovo);
                }
                int posicao = sorteio.Next(clientes.Count);
                Cliente cliente = clientes.ElementAt(posicao);
                cliente.RegistrarPedido(novo);
                novo.FecharPedido();
                pedidos.Add(novo);
                if (pedidos.Count() % porDia == 0)
                    dataPedido = dataPedido.AddDays(1);
            }

            return pedidos;
        }
    }
}

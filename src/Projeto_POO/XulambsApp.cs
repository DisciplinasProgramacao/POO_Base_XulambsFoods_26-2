

using System.ComponentModel;
using System.Text;

namespace XulambsFoods
{
    public class XulambsApp
    {
        static Dados<Pedido> pedidos;
        static Dados<Cliente> clientes;
        static string versao = "0.8";

        #region utilidades
        static void Pausa()
        {
            Console.WriteLine("\nDigite enter para continuar.");
            Console.ReadKey();
        }

        static int LerInteiro()
        {
            int opcao;
            Console.Write("Código: ");
            try
            {
                opcao = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Somente valores numéricos");
                opcao = 99;
            }
            return opcao;
        }
        
        static void Cabecalho()
        {
            Console.Clear();
            Console.WriteLine($"Xulambs Pizza v{versao}");
            Console.WriteLine("===================");
        }
        #endregion

        #region configuração
        static void Config()
        {
            List<Cliente> cli = GeradorClientes.gerarClientes();
            List<Pedido> ped = GeradorPedidos.gerarPedidos(
                                DateTime.Now.AddDays(-20),
                                DateTime.Now,
                                cli,
                                9);                
            pedidos = new Dados<Pedido>(ped);
            clientes = new Dados<Cliente>(cli);
        }
        #endregion

        #region menus
        static int MenuPrincipal()
        {
            Cabecalho();
            
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Abrir um pedido");
            menu.AppendLine("2 - Alterar pedido");
            menu.AppendLine("3 - Relatório de pedido");
            menu.AppendLine("4 - Fechar pedido");
            menu.AppendLine("===================");
            menu.AppendLine("5 - Listar clientes");
            menu.AppendLine("6 - Resumo do cliente");
            menu.AppendLine("7 - Atualizar clientes");
            menu.AppendLine("8 - Total vendido");
            menu.AppendLine("9 - Pedidos de um dia");
            menu.AppendLine("10 - Cliente que mais gastou");

            menu.AppendLine("0 - Finalizar");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());
            
            return LerInteiro();
        }

        static int MenuProdutos() {
            Cabecalho();
            Console.WriteLine("Quer comprar o quê?");
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Pizzas");
            menu.AppendLine("2 - Sanduíches");
            menu.AppendLine("3 - Sobremesas");
            menu.AppendLine("0 - Desistir");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());

            return LerInteiro();
        }

        static int MenuSobremesas() {
            Cabecalho();
            Console.WriteLine("Escolha sua sobremesa");
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("1 - Brigadeiro de Colher");
            menu.AppendLine("2 - Pudim de Leite Condensado");
            menu.AppendLine("3 - Doce de Leite no Pires");
            menu.AppendLine("0 - Desistir");
            menu.Append("Sua opção: ");
            Console.Write(menu.ToString());

            return LerInteiro();
        }
        #endregion

        #region relatorios
        static void ImprimirDados(object objeto)
        {
            Cabecalho();

            //null safety para impressão: se não existir, pega uma string "padrão"
            string msg = objeto?.ToString() ?? "Objeto não encontrado.";

            Console.WriteLine(msg);
        }

        #endregion

        #region borda
        public static void EscolherBorda(Pizza pizza)
        {
            string[] bordas = Enum.GetNames<EBorda>();
            Console.WriteLine("Escolha uma borda:");
            int i = 1;
            foreach (string nome in bordas)
            {
                Console.WriteLine($"{i} - {nome}");
                i++;
            }
            Console.Write("Sua escolha: ");
            int escolha = LerInteiro();
            EBorda borda = Enum.GetValues<EBorda>()[escolha - 1];

            pizza.AdicionarBorda(borda);
        }
        #endregion

        #region sobremesa
        static IProduto ComprarSobremesa() {
            int opcao = MenuSobremesas();
            ESobremesa tipo = Enum.GetValues<ESobremesa>()[opcao - 1];
            return new Sobremesa(tipo);
        }

        #endregion

        #region pizza
        static int EscolherIngredientes()
        {
            Console.Write("Quantos ingredientes você deseja (0-8)? ");
            return LerInteiro();
        }

        static IProduto ComprarPersonalizavel(int opcao)
        {
            IPersonalizavel item = opcao switch
            {
                1 => ComprarPizza(),
                2 => ComprarSanduiche()
            };
            return (IProduto)EscolherIngredientes(item);
        }

       static IPersonalizavel EscolherIngredientes(IPersonalizavel item)
        {
            try
            {
                int quantos = EscolherIngredientes();
                item.AdicionarIngredientes(quantos);
            }
            catch (ArgumentException excecao)
            {
                Console.WriteLine(excecao.Message);
                Pausa();
                return EscolherIngredientes(item);
            }
            catch (InvalidOperationException excecao)
            {
                Console.WriteLine(excecao.Message);
            }
            return item;

        }

        static Pizza ComprarPizza()
        {
            Cabecalho();
            Console.WriteLine("Comprando uma pizza:");

            Pizza novaPizza = new Pizza();
            EscolherBorda(novaPizza);
            
            return novaPizza;
        }
        #endregion

        #region produto
        static IProduto? ComprarProduto() {
                   
            int opcao = MenuProdutos();
            return opcao switch {
                1 or 2 => ComprarPersonalizavel(opcao),
                3 => ComprarSobremesa(),
                _ => null
            };


        }

        private static IPersonalizavel ComprarSanduiche()
        {
            Cabecalho();
            Console.WriteLine("Comprando um sanduíche");
            Sanduiche sanduba = new Sanduiche();
            Console.Write("Quer fritas? (s/n)");
            string combo = Console.ReadLine().ToLower();
            if (combo.Equals("s"))
                sanduba.SetCombo(true);
            return sanduba;
        }

        #endregion

        #region pedido

        static PedidoEntrega CriarPedidoEntrega()
        {
            Console.Write("Qual a distância? ");
            double distancia = double.Parse(Console.ReadLine());
            return new PedidoEntrega(distancia);
        }

        static Pedido EscolherTipoPedido()
        {
            Cabecalho();
            Console.WriteLine("Escolha o tipo de pedido: ");
            Console.WriteLine("1 - Local");
            Console.WriteLine("2 - Para entrega");
            Console.Write("Digite sua escolha: ");
            int opcao = LerInteiro();
            return opcao switch
            {
                1 => new PedidoLocal(DateTime.Now),
                2 => CriarPedidoEntrega()
            };

        }
        
        static void CriarPedido()
        {
            string resp = "s";
            Pedido novoPedido = EscolherTipoPedido();
            do
            {
                IProduto produto = ComprarProduto();
                novoPedido.Adicionar(produto);
                ImprimirDados(produto);
                Console.Write("\n\nAlgo mais (s/n)? ");
                resp = Console.ReadLine();
            } while (resp.ToLower().Equals("s"));

            ImprimirDados(novoPedido);
            pedidos.Adicionar(novoPedido);
        }

        static void RelatorioPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();
            Pedido pedido = pedidos.Localizar(codigo);   //conversão segura: objeto ou nulo
            ImprimirDados(pedido);
        }

        static void FecharPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();

            Pedido pedido = pedidos.Localizar(codigo);   //conversão segura: objeto ou nulo
            Cliente cliente= clientes.Localizar(1);
          
            pedido?.FecharPedido();             //null safety: só executa se o pedido existir
            cliente.RegistrarPedido(pedido);
            ImprimirDados(pedido);
        }

        static void AlterarPedido()
        {
            Cabecalho();
            Console.Write("Número do pedido: ");
            int codigo = LerInteiro();
            Pedido pedido = pedidos.Localizar(codigo);//conversão segura: objeto ou nulo
            
            if (pedido != null)
            {
                try
                {
                    IProduto produto = ComprarProduto();
                    pedido.Adicionar(produto);
                    ImprimirDados(pedido);
                }
                catch (PedidoFechadoException fechado)
                {
                    Console.WriteLine(fechado.Message);
                }
                catch(PedidoCheioException cheio)
                {
                    Console.WriteLine(cheio.Message);
                    Console.Write("Quer fechar o pedido? ");
                    string resp = Console.ReadLine();
                    if (resp.Equals("s"))
                        cheio.FecharPedido();
                }
                catch (InvalidOperationException operacao)
                {
                    Console.WriteLine(operacao.Message);
                    AlterarPedido();
                }
            };
        }

         #endregion

        static void Main(string[] args)
        {
            int opcao;
            Config();
            do
            {
                opcao = MenuPrincipal();
                Action ac =
                        opcao switch
                        {
                            1 => () => CriarPedido(),
                            2 => () => AlterarPedido(),
                            3 => () => RelatorioPedido(),
                            4 => () => FecharPedido(),
                            5 => () => ListarClientes(),
                            6 => () => RelatorioCliente(),
                            7 => () => AtualizarClientes(),
                            8 => () => TotalVendido(),
                            9 => () => PedidosDoDia(),
                            10 => () => MaiorGastador(),
                            _ => () => Pausa()
                        };
                ac.Invoke();
                Pausa();
            } while (opcao != 0);
        }

        private static void MaiorGastador() {
            Cabecalho();
            Console.WriteLine("Maior cliente do Xulambs Foods:");
          
            Comparison<Cliente> comparacao =
                (cli1, cli2) => {
                    double diferenca = cli1.TotalGasto() - cli2.TotalGasto();
                    if (diferenca == 0)
                        return 0;
                    return diferenca > 0 ? 1 : -1;
                };
           
            Cliente maior = clientes.Maior(comparacao);
            Console.WriteLine(maior);
        }

        private static void PedidosDoDia() {
            Cabecalho();
            Console.WriteLine("Pedidos filtrados por uma data:");
            Console.Write("Digite a data (dd/mm/aaaa): ");
            string data = Console.ReadLine();

            Predicate<Pedido> condicao = (ped) => ped.ToString().Contains(data);
            Func<Pedido, string> relat =
                (p) => $"Pedido nº{p.GetHashCode()} - Valor: {p.PrecoAPagar():C2}";

            Console.WriteLine(pedidos.RelatorioFiltrado(condicao, relat));
        }

        private static void TotalVendido() {
            Cabecalho();
            Func<Pedido, double> funcao = (ped) => ped.PrecoAPagar();
            double valor = pedidos.Somar(funcao);
            Console.WriteLine($"Total vendido pelo restaurante: {valor:C2}");
        }

        private static void ListarClientes() {
            Cabecalho();
            Console.WriteLine("Clientes Xulambs");
            Console.WriteLine(clientes.ToString());
        }
                

        private static void AtualizarClientes()
        {
            Cabecalho();
            Action<Cliente> metodo = (cli) => cli.AtualizarCategoria();
            clientes.Atualizar(metodo);
            Console.WriteLine("Clientes atualizados");
        }

        private static void EncerrarExpediente()
        {
            Cabecalho();
            Action<Pedido> metodo = (ped) => ped.FecharPedido();
            pedidos.Atualizar(metodo);
            Console.WriteLine("Pedidos fechados.");
        }

        private static void RelatorioCliente()
        {
            Cabecalho();
            int cod = LerInteiro();
            Cliente cli = clientes.Localizar(cod);
            Console.WriteLine(cli.RelatorioPedidos());
        }
    }
}
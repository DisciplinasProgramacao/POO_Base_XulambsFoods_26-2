using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace XulambsFoods {
    public class GeradorClientes {
        static Random sorteio = new Random(42);

        static List<string> nomes = new List<string>{"Ana", "Bia", "Carla", "Daniela", "Elisa", "Fabiana", "Gabriela", "Heloisa", "Ivana", "Joana", "Katia", "Luisa", "Mariana" };

        static List<string> sobrenomes = new List<string>{"Andrade", "Bastos", "Coelho", "Diniz", "Esteves", "Frota", "Galvão","Horta", "Ibanez", "Jobim", "Keller", "Lopes", "Matos" };

        public static List<Cliente> gerarClientes() {
            List<Cliente> clientes = new List<Cliente>(nomes.Count* sobrenomes.Count);
            List<string> nomesCompletos = new List<string>(nomes.Count * sobrenomes.Count);
            int id = 1;
            foreach (string n in nomes) {
                foreach (string s in sobrenomes) {
                    nomesCompletos.Add(n + " " + s);
                }
            }
            
            foreach (string n in nomesCompletos) {
                id += sorteio.Next(1, 7);
                clientes.Add(new Cliente(n, id));
            }

            return clientes;
        }
    }
}

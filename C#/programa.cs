using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;

#nullable enable


namespace ExemploDotNet
{

    public class produto
    {
        public string nome { get; set; }
        public decimal preco { get; set; }
        public int Estoque { get; set; }

        public produto(string nome, decimal preco, int estoque)
        {
            this.nome = nome;
            this.preco = preco;
            this.Estoque = estoque;     
        }

        public void exibirDetalhes()
        {
            Console.WriteLine($"Produto: {nome}, Preço: {preco:C}, Estoque: {Estoque}");
        }


    }
    /*
        class Program
        {
            static void Main(string[] args)
            {
                List<produto> listaProdutos = new List<produto>             {
                    new produto("Camiseta", 29.99m, 100),
                    new produto("Calça Jeans", 79.99m, 50),
                    new produto("Tênis", 149.99m, 30)
                };

                Console.WriteLine("--- Todos os produtos ---");
                foreach (var produto in listaProdutos)
                {
                    produto.exibirDetalhes();
                }

                Console.WriteLine("\n--- filtrando ---");
                var produtosDisponiveis = listaProdutos.Where(p => p.preco > 50).OrderByDescending(p => p.preco);

                foreach (var p in produtosDisponiveis)
                {
                    Console.WriteLine($"Disponível: {p.nome} -> {p.preco:C}");
                }
            }
        }
    */

    public class venda
    {
        public int id { get; set; }
        public produto produto { get; set; }
        public int quantidade { get; set; }
        public decimal precoUnitario { get; set; }
        public string categoria { get; set; }

        public venda(int id, produto produto, int quantidade, decimal precoUnitario, string categoria)
        {
            this.id = id;
            this.produto = produto;
            this.quantidade = quantidade;
            this.precoUnitario = this.produto.preco;
            this.categoria = categoria;
        }

        public void exibirDetalhes()
        {
            Console.WriteLine($"id: {id} | produto: {produto} | quantidade: {quantidade} | preço unitário: {precoUnitario:C} | categoria: {categoria}");
        }

    }

    public record Usuario(int id, string nome, string email, string cargo);

    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public Cliente(int id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }
    }

    public class RespostaApi<T>
    {
        public T? Dados { get; set; }
        public string Mensagem { get; set; }
        public bool Sucesso { get; set; }  

        public RespostaApi(T? dados, string mensagem, bool sucesso)
        {
            Dados = dados;
            Mensagem = mensagem;
            Sucesso = sucesso;
        }       

    }

    class Program
    {
        public static List<Cliente> listaClientes = new List<Cliente>
        {
            new Cliente(1, "João Silva", "email")
         };

        public static Cliente? buscarCliente(int id){
            var cliente = listaClientes.FirstOrDefault(c => c.Id == id);
            return cliente;
        }

        static async Task<string> BuscarDadosApiAsync()
        {
            Console.WriteLine("1   Consultando servidor...");
            await Task.Delay(3000);

            Console.WriteLine("4   processando dados...");

            return "Dados recebidos do servidor!";  

        }

        static async Task ExemploReal()
        {
            Task<string> tarefaSendoFeita = BuscarDadosApiAsync();

            Console.WriteLine("2   já estou rodando aqui");

            string resultado = await tarefaSendoFeita;
        }

        static async Task Main(string[] args)
        {
            // Dispara a tarefa mas NÃO espera
            Task tarefa = ExemploReal();

            Console.WriteLine("3   O Main continuou enquanto o ExemploReal ainda está esperando a API...");

            // Se você não der await aqui em algum momento, 
            // o programa pode fechar antes do ExemploReal terminar!
            await tarefa;

            RespostaApi<produto> respostaProduto = new RespostaApi<produto>(new produto("Camiseta", 29.99m, 100), "Produto encontrado com sucesso!", true);
            RespostaApi<String> respostaString = new RespostaApi<string>("conteúdo", "Nenhum dado encontrado!", false);


            Console.WriteLine($"respostaProduto: Sucesso: {respostaProduto.Sucesso}, Mensagem: {respostaProduto.Mensagem}, Dados:" +
                $"\n    {respostaProduto.Dados?.nome}"
                + $"\n    {respostaProduto.Dados?.preco}"
                + $"\n     {respostaProduto.Dados?.Estoque}\n");

            Console.WriteLine($"respostaString: Sucesso: {respostaString.Sucesso}, Mensagem: {respostaString.Mensagem}, Dados: {respostaString.Dados}");



        }




        /*
         * EXERCÍCIOS DE LINQ, RECORDS E IMUTABILIDADE E TRATAMENTO DE NULOS
         * 
         * 
         * static void Main(string[] args)
        {

            var listaProdutos = new List<produto>
            {
                new produto("Smartphone Samsung", 2500.00m, 2),
                new produto("Notebook Dell", 4500.00m, 1),
                new produto("Arroz 5kg", 30.00m, 10),
                new produto("Monitor 24p", 900.00m, 4),
                new produto("Detergente", 2.50m, 20),
                new produto("Mouse Gamer", 150.00m, 3),
                new produto("Feijão Preto", 8.00m, 15),
                new produto("Desinfetante", 12.00m, 5),
                new produto("Teclado Mecânico", 350.00m, 2),
                new produto("Azeite de Oliva", 45.00m, 8)
            };
            var listaVendas = new List<venda>
            {
                new venda(1, listaProdutos[0], 2, 2500.00m, "Eletrônicos"),
                new venda(2, listaProdutos[1], 1, 4500.00m, "Eletrônicos"),
                new venda(3, listaProdutos[2], 10, 30.00m, "Alimentos"),
                new venda(4, listaProdutos[3], 1, 900.00m, "Eletrônicos"),
                new venda(5, listaProdutos[4], 290, 2.50m, "Limpeza"),
                new venda(6, listaProdutos[5], 3, 150.00m, "Eletrônicos"),
                new venda(7, listaProdutos[6], 15, 8.00m, "Alimentos"),
                new venda(8, listaProdutos[7], 5, 12.00m, "Limpeza"),
                new venda(9, listaProdutos[8], 2, 350.00m, "Eletrônicos"),
                new venda(10, listaProdutos[9], 4, 45.00m, "Alimentos")
            };

            Console.WriteLine("--- Vendas Eletrônicos ---");
            var vendasEletronicos = listaVendas.Where(v => v.categoria == "Eletrônicos");

            foreach (var v in vendasEletronicos)
            {
                Console.WriteLine($" {v.produto.nome}");
            }

            Console.WriteLine("\n valor total das Vendas de Eletrônicos");

            var totalEletronicos = vendasEletronicos.Sum(v => v.quantidade * v.precoUnitario);
            Console.WriteLine($"{totalEletronicos:C}");

            Console.WriteLine("\n Vendas com faturamento acima de R$500,00");

            var vendasAcima500 = listaVendas.Where(venda => (venda.quantidade * venda.precoUnitario) > 500);

            foreach (var v in vendasAcima500)
            {
                Console.WriteLine($"id: {v.id} | produto: {v.produto.nome} | quantidade: {v.quantidade} | preço unitário: {v.precoUnitario:C} | categoria: {v.categoria}");

            }

            var produtoMaisCaro = listaProdutos.MaxBy(p => p.preco);

            Console.WriteLine($"\n Produto mais caro: {produtoMaisCaro?.nome} ->  {produtoMaisCaro?.preco:C}");

            var usuario1 = new Usuario(1, "João Silva", "joao@email.com", "Gerente de Vendas");
            var usuario2 = usuario1 with { email = "Silva@email.com" };

            

            Console.WriteLine($"\n usuario 1: {usuario1.nome}, {usuario1.email}, {usuario1.cargo}");
            Console.WriteLine($" usuario 2: {usuario2.nome}, {usuario2.email}, {usuario2.cargo}");

            Cliente cliente1 = buscarCliente(2);
            Console.WriteLine($"\n cliente encontrado: {cliente1?.Nome ?? "Nenhum Cliente cadastrado com esse ID"}");


        }*/





    }
}






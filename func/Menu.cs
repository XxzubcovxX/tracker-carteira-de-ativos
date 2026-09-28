using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using trecker.API.Publics.Brapi;
using trecker.func.login;

namespace trecker.func
{
    internal class Menu
    {



        public async Task MostrarMenu()
        {
            while (true)
            {
            Login login = new Login();
            login.FazerLogin();

                bool loop = true;
                while (loop)
                {
                    string opcao_escolhida = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("[bold yellow]Selecione uma opção:[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                        {
                        "1. Adicionar Ativo (Compra/Venda)",
                        "2. Ver Minha Carteira",
                        "3. Consultar Cotação na BraPI",
                        "4. Sair"
                         }));

                    switch (opcao_escolhida)
                    {
                        case "1. Adicionar Ativo (Compra/Venda)":
                            AnsiConsole.MarkupLine("[bold green]Opção 1 selecionada: Adicionar Ativo (Compra/Venda)[/]");
                            // Chame o método correspondente para adicionar ativo
                            break;
                        case "2. Ver Minha Carteira":
                            AnsiConsole.MarkupLine("[bold green]Opção 2 selecionada: Ver Minha Carteira[/]");
                            // Chame o método correspondente para ver a carteira
                            break;
                        case "3. Consultar Cotação na BraPI":
                            AnsiConsole.MarkupLine("[bold green]Opção 3 selecionada: Consultar Cotação[/]");
                            AnsiConsole.MarkupLine("[bold yellow]Digite o código do ativo que deseja consultar:[/]");
                            string ticker = Console.ReadLine();
                            BrapiService brapiService = new BrapiService();
                            await brapiService.GetAtivosAsync(ticker);
                            ContinuarAPP();
                            break;
                        case "4. Sair":
                            Console.Clear();
                            loop = false;
                            break;
                    }


                }

            }
        }
        private void ContinuarAPP()
        {
            AnsiConsole.MarkupLine("\n[bold yellow]Pressione qualquer tecla para continuar...[/]");
            Console.ReadKey(true);
            Console.Clear();
        }
        
    }
}

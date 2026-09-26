using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using trecker.func.login;

namespace trecker.func
{
    internal class Menu
    {
        public int Opcao { get; set; }


        public int MostrarMenu()
        {
            Login login = new Login();

            login.FazerLogin();

            AnsiConsole.MarkupLine("[bold yellow]Selecione uma opção:[/] \n");
            AnsiConsole.MarkupLine("[bold cyan]1.[/] Adicionar tarefa");
            AnsiConsole.MarkupLine("[bold cyan]2.[/] Listar tarefas");
            AnsiConsole.MarkupLine("[bold cyan]3.[/] Sair");

            Console.Write(" \n Digite uma opção: ");
            int opcao_escolhida = Convert.ToInt32(Console.ReadLine());

            while (opcao_escolhida < 1 || opcao_escolhida > 3)
            {
                Console.Clear();
                AnsiConsole.MarkupLine("[bold red]Opção inválida! Por favor, selecione uma opção válida (1, 2 ou 3):[/] \n");

                AnsiConsole.MarkupLine("[bold yellow]Selecione uma opção:[/] \n");
                AnsiConsole.MarkupLine("[bold cyan]1.[/] Adicionar tarefa");
                AnsiConsole.MarkupLine("[bold cyan]2.[/] Listar tarefas");
                AnsiConsole.MarkupLine("[bold cyan]3.[/] Sair");

                Console.Write(" \n Digite uma opção: ");  opcao_escolhida = Convert.ToInt32(Console.ReadLine());

            }

            return Opcao = opcao_escolhida;
        }

    }
}

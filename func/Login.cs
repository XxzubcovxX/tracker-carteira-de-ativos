using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace trecker.func.login
{
    internal class Login
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public void FazerLogin()
        {
            var titulo =new Panel(new FigletText("TRACKER CLI BR").Centered().Color(Color.Green))
            {
                Border = BoxBorder.Double,
                Padding = new Padding(2, 1, 2, 1),
                Header = new PanelHeader(" [bold yellow]B3 PORTFOLIO[/] ")
            };

            AnsiConsole.Write(titulo);
            AnsiConsole.MarkupLine("\n [bold yellow]Digite seu nome de usuário:[/]");
            string username = Console.ReadLine();
            AnsiConsole.MarkupLine("[bold yellow]Digite sua senha:[/]");
            string password = AnsiConsole.Prompt( new TextPrompt<string>("Senha:").Secret());

            Console.Clear();

            AnsiConsole.MarkupLine($"[bold green]Bem-vindo, {username}![/] \n");
        }
    }
}

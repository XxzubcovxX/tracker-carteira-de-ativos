using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using trecker.API.api.DTO;

namespace trecker.func.login
{
    internal class Login
    {
        private  readonly HttpClient clientLogin;

        public Login()
        {
            clientLogin = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/")
            };
        }


   

        public async Task<bool> FazerLogin()
        {
            string username;
            string password;
            var titulo = new Panel(new FigletText("TRACKER CLI BR").Centered().Color(Color.Green))
            {
                Border = BoxBorder.Double,
                Padding = new Padding(2, 1, 2, 1),
                Header = new PanelHeader(" [bold yellow]B3 PORTFOLIO[/] ")
            };

            AnsiConsole.Write(titulo);

            string loginAcesso = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("[bold yellow]Selecione uma opção de login:[/]")
                .PageSize(10)
                .AddChoices(new[]
                {
                    "1. Login com nome de usuário e senha",
                    "2. Criar novo usuário",
                    "3. Sair"
                }));
            switch (loginAcesso)
            {
                case "1. Login com nome de usuário e senha":
                    AnsiConsole.MarkupLine("\n [bold yellow]Digite seu nome de usuário:[/]");
                    username = AnsiConsole.Prompt(new TextPrompt<string>("Nome de usuário:"));


                    AnsiConsole.MarkupLine("[bold yellow]Digite sua senha:[/]");
                    password = AnsiConsole.Prompt(new TextPrompt<string>("Senha:").Secret());

                    var resultado = await FazerLoginAsync(username, password);

                    bool login = true;


                    while (login){

                        if (resultado != null && resultado.Sucesso == true)
                        {
                            AnsiConsole.MarkupLine($"[green]Bem vindo {resultado.UserName}[/]");
                            login = false;
                            Console.Clear();

                        }
                        else
                        {

                            AnsiConsole.MarkupLine("[red]Usuário ou senha inválidos. Tente novamente.[/]");
                            AnsiConsole.MarkupLine("\n [bold yellow]Aperte a tecla ESC para voltar a tela de login ou qualquer outra tecla para tentar novamente:[/]");
                            var key = Console.ReadKey(intercept: true);
                            if (key.Key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                FazerLogin();

                            }
                            else
                            {

                                username = AnsiConsole.Prompt(new TextPrompt<string>("Nome de usuário:"));
                                password = AnsiConsole.Prompt(new TextPrompt<string>("Senha:").Secret());
                                resultado = await FazerLoginAsync(username, password);
                                Console.Clear();

                            }



                        }
                    }

                    break;
                        
                case "2. Criar novo usuário":
                    AnsiConsole.MarkupLine("\n [bold yellow]Digite seu nome de usuário para criar uma conta:[/]");
                    username = AnsiConsole.Prompt(new TextPrompt<string>("Nome de usuário:"));
                    AnsiConsole.MarkupLine("[bold yellow]Digite sua senha para criar uma conta:[/]");
                    password = AnsiConsole.Prompt(new TextPrompt<string>("Senha:").Secret());
                    Console.Clear();
                    AnsiConsole.MarkupLine($"[bold green]Bem-vindo, {username}![/] \n");

                    break;
                case "3. Sair":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }

            return true;
        }
        public async Task<LoginResponseDTO?> FazerLoginAsync(string usuario, string senha)
        {
            var payload = new LoginDTO
            {
                UsuarioNome = usuario,
                Senha = senha
            };

            // Envia o POST para http://localhost:5000/api/auth/login
            HttpResponseMessage response = await clientLogin.PostAsJsonAsync("api/auth/login", payload);

            // Lê a resposta deserializada no DTO
            var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            return resultado;
        }

        // Método para fazer Requisição de Cadastro
        public async Task<LoginResponseDTO?> CadastrarUsuarioAsync(string usuario, string senha)
        {
            var payload = new LoginDTO
            {
                UsuarioNome = usuario,
                Senha = senha
            };

            // Envia o POST para http://localhost:5000/api/auth/cadastrar
            HttpResponseMessage response = await clientLogin.PostAsJsonAsync("api/auth/cadastrar", payload);

            var resultado = await response.Content.ReadFromJsonAsync<LoginResponseDTO>();
            return resultado;
        }
    }
}

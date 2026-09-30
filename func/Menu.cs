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
            await login.FazerLogin();

                bool loop = true;
                while (loop)
                {
                 ExibirDashboardResumo();
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
        private void ExibirDashboardResumo()
        {
            // Banner de Cabeçalho
            AnsiConsole.Write(
                new Rule("[bold yellow]⚡ B3 PORTFOLIO TRACKER | DASHBOARD ⚡[/]")
                    .LeftJustified()
                    .RuleStyle("grey")
            );
            AnsiConsole.WriteLine();

            // ==========================================
            // LINHA 1: CARTÕES SUPERIORES DE MÉTRICAS
            // ==========================================

            // 1. Cartão de Patrimônio
            decimal patrimonioTotal = 15450.80m;
            decimal valorizacaoTotal = 1250.30m;
            double porcentagemValorizacao = 8.79;

            var painelPatrimonio = new Panel(
                new Markup(
                    $"[grey]Patrimônio Total:[/]\n[bold white]R$ {patrimonioTotal:N2}[/]\n\n" +
                    $"[grey]Lucro/Prejuízo:[/]\n[bold green]+R$ {valorizacaoTotal:N2} (+{porcentagemValorizacao:N2}%)[/]"
                )
            )
            {
                Header = new PanelHeader("[bold cyan] 💼 Resumo [/]"),
                Border = BoxBorder.Rounded,
                Expand = true
            };

            // 2. Cartão de Meta Financeira (Progresso)
            var graficoMeta = new BreakdownChart()
                .Width(30)
                .AddItem("Alcançado", 77, Color.Green)
                .AddItem("Falta", 23, Color.Grey);

            var painelMeta = new Panel(
                new Rows(
                    new Markup("[bold yellow]Meta: R$ 20.000,00[/] [grey](77%)[/]"),
                    new Text(" "),
                    graficoMeta
                )
            )
            {
                Header = new PanelHeader("[bold yellow] 🎯 Meta de Aporte [/]"),
                Border = BoxBorder.Rounded,
                Expand = true
            };

            // 3. Cartão de Destaques
            var tabelaAtivos = new Table()
                .Border(TableBorder.None)
                .HideHeaders()
                .AddColumn("Ticker")
                .AddColumn("Rendimento");

            tabelaAtivos.AddRow("[bold white]CPTS11[/]", "[green]+12.4% 🚀[/]");
            tabelaAtivos.AddRow("[bold white]PETR4[/]", "[green]+5.2%  📈[/]");
            tabelaAtivos.AddRow("[bold white]VALE3[/]", "[red]-2.1%  📉[/]");

            var painelDestaques = new Panel(tabelaAtivos)
            {
                Header = new PanelHeader("[bold magenta] 📊 Destaques [/]"),
                Border = BoxBorder.Rounded,
                Expand = true
            };

            // Grid Linha 1 (3 Colunas)
            var gridSuperior = new Grid();
            gridSuperior.AddColumn();
            gridSuperior.AddColumn();
            gridSuperior.AddColumn();
            gridSuperior.AddRow(painelPatrimonio, painelMeta, painelDestaques);

            // ==========================================
            // LINHA 2: GRÁFICOS (PROPORCIONAL E TORRES)
            // ==========================================

            // 1. Gráfico Proporcional de Alocação (% - Estilo Pizza)
            var graficoProporcional = new BreakdownChart()
                .Width(35)
                .ShowPercentage()
                .AddItem("Ações", 45, Color.Green)
                .AddItem("FIIs", 35, Color.Blue)
                .AddItem("Renda Fixa", 15, Color.Yellow)
                .AddItem("Cripto", 5, Color.Purple);

            var painelAlocacao = new Panel(graficoProporcional)
            {
                Header = new PanelHeader("[bold yellow] 🍕 Distribuição da Carteira (%) [/]"),
                Border = BoxBorder.Rounded,
                Expand = true
            };

            // 2. Gráfico de Torres (Aportes dos últimos meses)
            var graficoTorres = new BarChart()
                .Width(35)
                .AddItem("Jan", 1200, Color.Green)
                .AddItem("Fev", 1500, Color.Green)
                .AddItem("Mar", 800, Color.Yellow)
                .AddItem("Abr", 2000, Color.Blue);

            var painelAportes = new Panel(graficoTorres)
            {
                Header = new PanelHeader("[bold cyan] 📊 Histórico de Aportes (R$) [/]"),
                Border = BoxBorder.Rounded,
                Expand = true
            };

            // Grid Linha 2 (2 Colunas)
            var gridGraficos = new Grid();
            gridGraficos.AddColumn();
            gridGraficos.AddColumn();
            gridGraficos.AddRow(painelAlocacao, painelAportes);

            // Renderiza os dois Grids no Terminal
            AnsiConsole.Write(gridSuperior);
            AnsiConsole.Write(gridGraficos);
            AnsiConsole.WriteLine();
        }

    }
}

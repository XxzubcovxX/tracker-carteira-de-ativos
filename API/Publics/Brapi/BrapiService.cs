using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static trecker.API.Publics.Brapi.Brapi;

namespace trecker.API.Publics.Brapi
{
    internal class BrapiService
    {
         private readonly IConfiguration _configuration;
        private readonly string _token;
        private static readonly HttpClient client = new HttpClient();

        public BrapiService()
        {
            _configuration = new ConfigurationBuilder()
          .SetBasePath(AppContext.BaseDirectory)
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();

            _token = _configuration["Brapi:Token"] ?? "SEU_TOKEN_FALLBACK";
        }
        public async Task GetAtivosAsync(string ticker)
        {

            try
            {
                string url = $"https://brapi.dev/api/quote/{ticker}";

                using var request= new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Authorization= new AuthenticationHeaderValue("Bearer", _token);

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync();

                Brapi? brapiResponse =JsonSerializer.Deserialize<Brapi>(jsonResponse);

                var ativos = brapiResponse?.Results?[0];

                if (ativos == null)
                {
                    AnsiConsole.MarkupLine("[bold red]Ativo não encontrado.[/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"\nAtivo: {ativos.Nome}");
                    AnsiConsole.MarkupLine($"Nome: {ativos.Nome}");
                    AnsiConsole.MarkupLine($"Nome Completo: {ativos.NomeCompleto}");
                    AnsiConsole.MarkupLine($"Preço Atual: R$ {ativos.PrecoMercado:N2}");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter ativos: {ex.Message}");
            }
        }
    }
}

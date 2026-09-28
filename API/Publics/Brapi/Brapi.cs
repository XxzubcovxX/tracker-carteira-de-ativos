using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;
namespace trecker.API.Publics.Brapi
{
    internal class Brapi
    {
        [JsonPropertyName("results")]
        public List<Ativos>? Results { get; set; }

        public class Ativos
        {
            [JsonPropertyName("shortName")]
            public string? Nome { get; set; }
            [JsonPropertyName("longName")]
            public string? NomeCompleto { get; set; }
            [JsonPropertyName("regularMarketPrice")]
            public decimal? PrecoMercado { get; set; }



        }
    }
}

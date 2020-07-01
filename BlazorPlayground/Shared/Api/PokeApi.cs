using BlazorPlayground.Shared.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPlayground.Shared
{
    public class PokeApi
    {
        private HttpClient client;
        public PokeApi(HttpClient client)
        {
            this.client = client;
        }

        public async Task<Pokemon> GetPokemon(string Pokemon)
        {
            return await client.GetFromJsonAsync<Pokemon>(Pokemon);
        }
    }
}

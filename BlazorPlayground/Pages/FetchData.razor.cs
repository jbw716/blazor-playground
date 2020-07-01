using BlazorPlayground.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorPlayground.Pages
{
    public partial class FetchData
    {

        [Inject]
        private IJSRuntime js { get; set; }
        [Inject]
        private HttpClient Http { get; set; }

        [Inject]
        private PokeApi poke { get; set; }

        private List<WeatherForecast> forecasts;

        protected override async Task OnInitializedAsync()
        {
            var PokeData = await poke.GetPokemon("ditto");

            await js.InvokeVoidAsync("console.log", PokeData);

            forecasts = await Http.GetFromJsonAsync<List<WeatherForecast>>("sample-data/weather.json");

            Chart chart = new Chart
            {
                Type = "LineChart",
                Data = new List<List<object>>(),
                ElementId = "weather-chart",
                Axes = new string[][] { new string[] { "string", "Day" }, new string[] { "number", "Temperature" } }
            };

            chart.Data = forecasts.Select((forecast) => new List<object> { forecast.Date.ToString("M/d/yy"), forecast.TemperatureF }).ToList();

            await js.InvokeVoidAsync("console.log", chart.Data);

            await js.InvokeVoidAsync("chart.drawChart", chart);
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}

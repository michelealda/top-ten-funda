using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Dtos;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Object = Infrastructure.Dtos.Object;

namespace Infrastructure
{
    public class FundaApiReader : ISourceReader
    {
        private readonly HttpClient _httpClient;

        private const string Key = "ac1b0b1572524640a0ecc54de453ea9f";

        public FundaApiReader(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress =
                new Uri($"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{Key}/");
        }

        public Task<IEnumerable<House>> GetGardenHouses(CancellationToken cancellationToken)
            => GetHousesImpl("?type=koop&zo=/amsterdam/tuin/", cancellationToken);
        
        public Task<IEnumerable<House>> GetHouses(CancellationToken cancellationToken)
            => GetHousesImpl("?type=koop&zo=/amsterdam/", cancellationToken);
        

        private async Task<IEnumerable<House>> GetHousesImpl(string url, CancellationToken cancellationToken)
        {
            var page = 1;
            var houses = Enumerable.Empty<House>();
            while (page > 0)
            {
                var response = await GetContentAsync<RootObject>(
                    $"{url}&page={page}&pagesize=500",
                    cancellationToken);

                houses = houses
                    .Concat(
                        response
                            .Objects
                            .Select(o =>
                                House.CreateWithGarden(
                                    o.Id, o.MakelaarId, o.MakelaarNaam)));

                page = response.Paging.HuidigePagina != response.Paging.AantalPaginas
                    ? page + 1
                    : -1;
            }
            return houses;
            
        }

        private async Task<T> GetContentAsync<T>(string endpoint, CancellationToken cancellationToken)
            where T : class
        {
            var response = await _httpClient.GetAsync(endpoint, cancellationToken);
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var payload = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(payload);
        }

    }
}
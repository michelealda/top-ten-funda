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
        private readonly IRecordRepository _recordRepository;

        private const string Key = "ac1b0b1572524640a0ecc54de453ea9f";

        public FundaApiReader(HttpClient httpClient,
            IRecordRepository recordRepository
        )
        {
            
            _httpClient = httpClient;
            _recordRepository = recordRepository;
            _httpClient.BaseAddress =
                new Uri($"http://partnerapi.funda.nl/feeds/Aanbod.svc/json/{Key}/");
            
            
            var queryString = "?type=koop&zo=/amsterdam/tuin/&page=1&pagesize=500";
        }
        
        public async Task<IEnumerable<House>> GetGardenHouses(CancellationToken cancellationToken)
        {
            // load houses with garden
            var page = 1;
            var response = await GetContentAsync<RootObject>(
                $"?type=koop&zo=/amsterdam/tuin/&page={page}&pagesize=500",
                cancellationToken);
            
            response
                .Objects
                .Select(o => House.CreateWithGarden(o.Id, o.MakelaarId, o.MakelaarNaam));
            
            
        }

        private TResult MapToHouseWithGarden<TResult>(Object o)
        {
            throw new NotImplementedException();
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

    public interface IRecordRepository
    {
    }
}
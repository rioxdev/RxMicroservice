using Microsoft.Extensions.Configuration;
using PlateformeService.Dtos;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlateformeService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {


        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendPlateformToCommand(PlateformReadDto dto)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.PostAsync(_config["CommandService"], httpContent);
        
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("==> Sync post to CommandService : ");

                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine(json);
            }
            
        }
    }
}

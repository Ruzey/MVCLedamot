using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using WebApplication1.Models;

public class PersonService
{
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "PersonData";
    private readonly TimeSpan CacheDuration = TimeSpan.FromHours(6); // Cacha i 6 timmar
    private const string LocalFilePath = "persondata.json"; // Lokalt filnamn för att spara data

    public PersonService(HttpClient httpClient, IMemoryCache cache)
    {
        _httpClient = httpClient;
        _cache = cache;
    }

    public async Task<Temp> GetPersonsAsync()
    {
        // Först, kontrollera om filen finns lokalt
        if (File.Exists(LocalFilePath))
        {
            Console.WriteLine("✅ Data hämtad från lokal fil!");
            var json = await File.ReadAllTextAsync(LocalFilePath);
            return JsonSerializer.Deserialize<Temp>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        else 
        { 
            string url = "https://data.riksdagen.se/personlista/?utformat=json";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("❌ API-anrop misslyckades!");
                return new Temp { personlista = new personlista { person = new List<Person>() } };
            }

            // Läs och deserialisera data från API
            string apiJson = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Temp>(apiJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


            if (data == null || data.personlista == null)
            {
                Console.WriteLine("❌ API returnerade ingen giltig data!");
                return new Temp { personlista = new personlista { person = new List<Person>() } };
            }


            // Spara datan till lokal fil
            await File.WriteAllTextAsync(LocalFilePath, apiJson);
            Console.WriteLine("✅ Data sparad till lokal fil!");

            return data;
        }
    }
}

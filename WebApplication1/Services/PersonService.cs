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
    private const string LocalFilePath = "persondata.json";

    public PersonService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Temp> GetPersonsAsync()
    {
        // Först, kontrollera om filen finns lokalt
        if (File.Exists(LocalFilePath))
        {
            var json = await File.ReadAllTextAsync(LocalFilePath);
            return JsonSerializer.Deserialize<Temp>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
        else 
        { 
            string url = "https://data.riksdagen.se/personlista/?utformat=json";
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return new Temp { personlista = new personlista { person = new List<Person>() } };
            }

            string apiJson = await response.Content.ReadAsStringAsync();
            var data = JsonSerializer.Deserialize<Temp>(apiJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (data == null || data.personlista == null)
            {
                return new Temp { personlista = new personlista { person = new List<Person>() } };
            }

            // Spara datan till lokal fil
            await File.WriteAllTextAsync(LocalFilePath, apiJson);
            return data;
        }
    }
}

using System.Net;
using System.Text.Json;
using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Entities.Rest;

namespace CounTrivia.Services.CountryProvider;

public class RestApiCountryProvider : ICountryProvider
{
    // TODO: Make configurable
    private static readonly string BaseUri = Environment.GetEnvironmentVariable("BASE_URI");
    private readonly HttpClient _httpClient;
    private readonly IMapper _mapper;

    public RestApiCountryProvider(HttpClient httpClient, IMapper mapper)
    {
        _httpClient = httpClient;
        _mapper = mapper;
    }

    public async Task<List<Country>?> GetAllCountriesAsync()
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{BaseUri}/all"),
            Method = HttpMethod.Get
        };

        var response = await _httpClient.SendAsync(request);

        if (response.StatusCode != HttpStatusCode.OK)
        {
            return null;
        }

        List<ApiResponseCountry>? apiResponseCountries = DeserializeRestCountries(await response.Content.ReadAsStringAsync());
        return _mapper.Map<List<Country>>(apiResponseCountries);
    }

    public async Task<Country?> GetCountryByCca3Async(string cca3)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri($"{BaseUri}/alpha/{cca3}"),
            Method = HttpMethod.Get
        };

        var response = await _httpClient.SendAsync(request);

        // TODO: Check response status

        var content = await response.Content.ReadAsStringAsync();
        List<ApiResponseCountry>? countries = DeserializeRestCountries(content);

        return _mapper.Map<Country>(countries?.FirstOrDefault());
    }

    public async Task<Country?> GetRandomCountryAsync()
    {
        List<Country>? allCountries = await GetAllCountriesAsync();

        if (allCountries is null || allCountries.Count == 0)
        {
            return null;
        }

        Random randomNumberGenerator = new Random();
        int randomCountryIndex = randomNumberGenerator.Next(0, allCountries.Count);

        return allCountries[randomCountryIndex];
    }

    private List<ApiResponseCountry>? DeserializeRestCountries(string jsonString)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<ApiResponseCountry>>(jsonString, options);
    }
}
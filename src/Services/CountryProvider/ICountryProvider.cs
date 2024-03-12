using CounTrivia.Entities;

namespace CounTrivia.Services.CountryProvider;

public interface ICountryProvider
{
    public Task<Country?> GetCountryByCca3Async(string cca3);
    public Task<List<Country>?> GetAllCountriesAsync();
    public Task<Country?> GetRandomCountryAsync();
}

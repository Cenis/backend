using AutoMapper;
using CounTrivia.Entities;
using CounTrivia.Services.CountryProvider;
using Microsoft.AspNetCore.Mvc;

namespace CounTrivia.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICountryProvider _countryProvider;
    private readonly ILogger<CountryController> _logger;

    public CountryController(IMapper mapper, ICountryProvider countryProvider, ILogger<CountryController> logger)
    {
        _mapper = mapper;
        _countryProvider = countryProvider;
        _logger = logger;
    }

    [HttpGet("{cca3}")]
    public async Task<ActionResult<Country?>> GetCountryByCca3(string cca3)
    {
        Country? country = await _countryProvider.GetCountryByCca3Async(cca3);

        if (country is null)
        {
            return NoContent();
        }

        return country;
    }
}

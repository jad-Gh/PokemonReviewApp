using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries();

        Country GetCountry(int id);

        bool CountryExists(int id);

        Country GetCountrybyOwner(int ownerId);

        ICollection<Owner> GetOwnersFromCounrty(int countryId);

        bool CreateCountry(Country country);
        bool UpdateCountry(Country country);

        bool Save();

    }
}

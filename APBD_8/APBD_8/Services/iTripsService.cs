using APBD_8.Models.DTOs;

namespace APBD_8.Services;

public interface iTripsService
{
    Task<List<TripDTO>> GetTripsAsync();
    Task<List<CountryDTO>> GetTripCountries(int idTrip);

    Task<List<TripDTO>> GetTripsByClientIdAsync(int clientId);

    Task<string> RegisterClientToTripAsync(int clientId, int tripId);
    
    Task<string> RemoveClientFromTripAsync(int clientId, int tripId);

}
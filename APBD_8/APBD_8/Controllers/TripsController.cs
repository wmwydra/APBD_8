using APBD_8.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APBD_8.Services;

namespace APBD_8.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly iTripsService _tripsService;

        public TripsController(iTripsService tripsService)
        {
            _tripsService = tripsService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetTrips()
        {
            var trips = await _tripsService.GetTripsAsync();
            foreach (var trip in trips)
            {
                var tripCountries = await _tripsService.GetTripCountries(trip.IdTrip);
                trip.Countries = tripCountries;
            }
            return Ok(trips);
        }
        
    }
}
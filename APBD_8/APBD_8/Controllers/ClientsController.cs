using APBD_8.Models;
using APBD_8.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APBD_8.Services;

namespace APBD_8.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly iClientService _clientService;
        private readonly iTripsService _tripsService;

        public ClientsController(iClientService clientService)
        {
            _clientService = clientService;
        }
        
        [HttpGet("{id}/trips")]
        public async Task<IActionResult> GetTripsByClientId(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound("Client not found.");
            }
            var trips = await _tripsService.GetTripsByClientIdAsync(id);
            Console.WriteLine(trips == null ? "Trips is NULL" : $"Trips found: {trips.Count}");

    
            client.Trips = await _tripsService.GetTripsByClientIdAsync(id);

            if (client.Trips == null || !client.Trips.Any())
            {
                return Ok("Client has no registered trips.");
            }

            return Ok(client.Trips);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);
            if (client == null)
                return NotFound("Client not found.");
            return Ok(client);
        }
        
        [HttpPost()]
        public async Task<IActionResult> AddClient([FromBody] ClientCreateDTO body)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            var client = await _clientService.AddClientAsync(body);
            return Created($"/clients/{client.IdClient}", client);
        }

        [HttpPut("{id}/trips/{tripId}")]
        public async Task<IActionResult> RegisterClientForTrip(int id, int tripId)
        {
            var result = await _tripsService.RegisterClientToTripAsync(id, tripId);

            return result switch
            {
                "Client not found" => NotFound("Client not found"),
                "Trip not found" => NotFound("Trip not found"),
                "Trip is full" => BadRequest("Trip is full"),
                "Client already registered to this trip" => BadRequest("Client already registered"),
                "OK" => Ok("Client successfully registered to trip"),
                _ => StatusCode(500, "Unknown error")
            };
        }

        [HttpDelete("{id}/trips/{tripId}")]
        public async Task<IActionResult> RemoveClientFromTrip(int id, int tripId)
        {
            var result = await _tripsService.RemoveClientFromTripAsync(id, tripId);
            return result switch
            {
                "Registration not found" => NotFound("Registration not found"),
                "Error" => BadRequest("Error"),
                "OK" => Ok("Client successfully removed from trip"),
                _ => StatusCode(500, "Unknown error")
            };
        }
    }
}


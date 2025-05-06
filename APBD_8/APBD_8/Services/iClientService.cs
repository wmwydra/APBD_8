using APBD_8.Models;
using APBD_8.Models.DTOs;

namespace APBD_8.Services;

public interface iClientService
{
    Task<List<ClientGetDTO>> GetClientsAsync();
    
    Task<ClientGetDTO> GetClientByIdAsync(int clientId);
    
    Task<ClientGetDTO> AddClientAsync(ClientCreateDTO clientCreateDto);
    
    
}
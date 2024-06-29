using EvilCorp.DTOs.ClientDTOs;

namespace EvilCorp.Services.ClientService;

public interface IClientService
{
    public Task<bool> DoesIndividualExistsAsync(string pesel);
    
    public Task<bool> DoesCompanyExistsAsync(string pesel);
    
    public Task<bool> AddClientAsync(NewIndividualDto request);

    public Task<bool> AddCompanyAsync(NewCompanyDto request);
    
    public Task<bool> DoesIndividualExistsAsync(int id);

    public Task<bool> DeleteIndividualAsync(int id);
}
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

    public Task<bool> DoesCompanyExistsAsync(int id);

    public Task<bool> UpdateIndividualAsync(UpdateIndividualDto request, int id);

    public Task<bool> UpdateCompanyAsync(UpdateCompanyDto request, int id);
    
    public Task<bool> DoesClientExistsAsync(int id);
    
    public Task<bool> IsPrevClientAsync(int id);
}
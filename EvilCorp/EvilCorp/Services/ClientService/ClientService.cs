using EvilCorp.Context;
using EvilCorp.DTOs.ClientDTOs;
using EvilCorp.Models;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Services.ClientService;

public class ClientService : IClientService
{
    private readonly EvilCorpContext _context;

    public ClientService(EvilCorpContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesIndividualExistsAsync(string pesel)
    {
        return await _context.Individual.FirstOrDefaultAsync(e => e.Pesel == pesel) != null;
    }
    
    public async Task<bool> DoesCompanyExistsAsync(string krs)
    {
        return await _context.Company.FirstOrDefaultAsync(e => e.Krs == krs) != null;
    }

    public async Task<bool> AddClientAsync(NewIndividualDto request)
    {
        int clientid;

        Client client = new Client()
        {
            Address = request.Address,
            PhoneNum = request.PhoneNum,
            Email = request.Email,
            IsDeleted = "N",
            PrevClient = "N"
        };

        var entry = await _context.AddAsync(client);
        await _context.SaveChangesAsync();
        
        clientid = entry.Entity.IdClient;

        Individual individual = new Individual()
        {
            FName = request.FName,
            LName = request.LName,
            Pesel = request.Pesel,
            ClientId = clientid
        };

        await _context.AddAsync(individual);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCompanyAsync(NewCompanyDto request)
    {
        int clientid;

        Client client = new Client()
        {
            Address = request.Address,
            PhoneNum = request.PhoneNum,
            Email = request.Email,
            IsDeleted = "N",
            PrevClient = "N"
        };

        var entry = await _context.AddAsync(client);
        await _context.SaveChangesAsync();
        
        clientid = entry.Entity.IdClient;

        Company company = new Company()
        {
            Name = request.Name,
            Krs = request.Krs,
            ClientId = clientid
        };

        await _context.AddAsync(company);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DoesIndividualExistsAsync(int id)
    {
        return await _context.Individual.FirstOrDefaultAsync(e => e.ClientId == id) == null;
    }

    public async Task<bool> DeleteIndividualAsync(int id)
    {
        var client = await _context.Client.FirstOrDefaultAsync(e => e.IdClient == id);

        if (client == null)
        {
            return false;
        }
        client.IsDeleted = "Y";

        await _context.SaveChangesAsync();

        return true;
    }
}
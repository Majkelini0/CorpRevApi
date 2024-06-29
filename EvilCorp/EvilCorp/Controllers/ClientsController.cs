using EvilCorp.DTOs.ClientDTOs;
using EvilCorp.Services.ClientService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

[ApiController]
[Route("EvilCorp/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _service;
    
    public ClientsController(IClientService service)
    {
        _service = service;
    }
    
    [HttpPost("NewIndividual")]
    public async Task<IActionResult> AddNewIndividualAsync([FromBody] NewIndividualDto request)
    {
        if(request.Pesel.Length != 11 || !request.Pesel.All(char.IsDigit))
        {
            return BadRequest("PESEL doesn't have 11 characters or contains non digit characters");
        }
        
        if (await _service.DoesIndividualExistsAsync(request.Pesel))
        {
            return BadRequest("Person with this PESEL already exists in the database");
        }

        if (await _service.AddClientAsync(request) == false)
        {
            return BadRequest("Something went wrong while adding the individual to the database");
        }
        
        return Ok("Individual created");
    }
    
    [HttpPost("NewCompany")]
    public async Task<IActionResult> AddNewCompanyAsync([FromBody] NewCompanyDto request)
    {
        if(request.Krs.Length != 10 || !request.Krs.All(char.IsDigit))
        {
            return BadRequest("KRS number doesn't have 10 characters or contains non digit characters");
        }
        
        if (await _service.DoesCompanyExistsAsync(request.Krs))
        {
            return BadRequest("Company with this KRS number already exists in the database");
        }

        if (await _service.AddCompanyAsync(request) == false)
        {
            return BadRequest("Something went wrong while adding the Company to the database");
        }
        
        return Ok("Company created");
    }

    [HttpDelete("DeleteClient/{id:int}")]
    public async Task<IActionResult> DeleteIndividual(int id)
    {
        if (await _service.DoesIndividualExistsAsync(id))
        {
            return BadRequest("Person with this ID doesn't exist in the database");
        }

        if (await _service.DeleteIndividualAsync(id) == false)
        {
            return NotFound("Sth went wrong. Person could not be found");
        }

        return Ok("Individual deleted");
    }

    [HttpPut("UpdateIndividual/{id:int}")]
    public async Task<IActionResult> UpdateIndividual([FromBody] UpdateIndividualDto request, int id)
    {
        if (await _service.DoesIndividualExistsAsync(id))
        {
            return BadRequest("Person with this ID doesn't exist in the database");
        }

        if (await _service.UpdateIndividualAsync(request, id) == false)
        {
            return NotFound("Sth went wrong. Person could not be found");
        }
        
        return Ok("Individual updated");
    }
    
    [HttpPut("UpdateCompany/{id:int}")]
    public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyDto request, int id)
    {
        if (await _service.DoesCompanyExistsAsync(id))
        {
            return BadRequest("Company with this ID doesn't exist in the database");
        }
        
        if (await _service.UpdateCompanyAsync(request, id) == false)
        {
            return NotFound("Sth went wrong. Company could not be found");
        }
        
        return Ok("Company updated");
    }
}
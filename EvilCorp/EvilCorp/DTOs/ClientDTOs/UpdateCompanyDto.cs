using System.ComponentModel.DataAnnotations;

namespace EvilCorp.DTOs.ClientDTOs;

public class UpdateCompanyDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string PhoneNum { get; set; } = null!;
    
    public string Email { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace EvilCorp.DTOs.ClientDTOs;

public class NewIndividualDto
{
    [Required]
    public string FName { get; set; } = null!;
    
    [Required]
    public string LName { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string PhoneNum { get; set; } = null!;
    
    public string Email { get; set; } = null!;
    
    [Required]
    public string Pesel { get; set; } = null!;
}
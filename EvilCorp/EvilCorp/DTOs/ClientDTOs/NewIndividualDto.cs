using System.ComponentModel.DataAnnotations;

namespace EvilCorp.DTOs.ClientDTOs;

public class NewIndividualDto
{
    [MaxLength(200)]
    [Required]
    public string FName { get; set; } = null!;

    [MaxLength(200)]
    [Required]
    public string LName { get; set; } = null!;
    
    [MaxLength(300)]
    public string Address { get; set; } = null!;
    
    [MaxLength(50)]
    public string PhoneNum { get; set; } = null!;
    
    [MaxLength(200)]
    public string Email { get; set; } = null!;
    
    [MaxLength(11)]
    [Required]
    public string Pesel { get; set; } = null!;
}
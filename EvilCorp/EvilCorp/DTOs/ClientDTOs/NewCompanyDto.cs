using System.ComponentModel.DataAnnotations;

namespace EvilCorp.DTOs.ClientDTOs;

public class NewCompanyDto
{
    [MaxLength(300)]
    [Required]
    public string Name { get; set; } = null!;
    
    [MaxLength(10)]
    [Required]
    public string Krs { get; set; } = null!;
    
    [MaxLength(300)]
    public string Address { get; set; } = null!;
    
    [MaxLength(50)]
    public string PhoneNum { get; set; } = null!;
    
    [MaxLength(200)]
    public string Email { get; set; } = null!;
}
﻿using System.ComponentModel.DataAnnotations;

namespace EvilCorp.DTOs.ClientDTOs;

public class NewCompanyDto
{
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public string Krs { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string PhoneNum { get; set; } = null!;
    
    public string Email { get; set; } = null!;
}
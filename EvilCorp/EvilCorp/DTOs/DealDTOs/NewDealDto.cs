﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.DTOs.DealDTOs;

public class NewDealDto
{
    [Required]
    public DateTime ExpiresAt { get; set; } // min + 3 , max + 30 from .Now
    
    [Required]
    public string UpdatesInfo { get; set; } = null!;
    
    [Required]
    public int SupportPeriod { get; set; } // Years
    
    public int ClientId { get; set; }
    
    public int SoftwareId { get; set; }
}
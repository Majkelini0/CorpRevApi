﻿using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EvilCorp.Models;

[PrimaryKey(nameof(ClientId))]
public class Individual
{
    [MaxLength(200)]
    [Required]
    public string FName { get; set; } = null!;

    [MaxLength(200)]
    [Required]
    public string LName { get; set; } = null!;
    
    [MaxLength(11)]
    [Required]
    public string Pesel { get; set; } = null!;
    
    // foreign key property
    public int ClientId { get; set; }
    
    // reference navigation to principal entity
    public Client Client { get; set; } = null!;
}
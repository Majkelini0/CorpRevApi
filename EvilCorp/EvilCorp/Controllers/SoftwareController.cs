using EvilCorp.Services.SoftwareService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

public class SoftwareController : ControllerBase
{
    private readonly ISoftwareService _softwareService;
    
    public SoftwareController(ISoftwareService softwareService)
    {
        _softwareService = softwareService;
    }
    
    
}
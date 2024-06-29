using EvilCorp.Services.SoftwareService;
using Microsoft.AspNetCore.Mvc;

namespace EvilCorp.Controllers;

public class SoftwareController : ControllerBase
{
    private readonly ISoftwareService _service;
    
    public SoftwareController(ISoftwareService service)
    {
        _service = service;
    }
    
    
}
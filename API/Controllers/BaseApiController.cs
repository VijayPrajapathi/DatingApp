using System;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]

public class BaseApiController : ControllerBase
{
    // This class can be used to define common functionality for all API controllers.
    // For example, you can add common error handling, logging, or other cross-cutting concerns here.
    // Currently, it is empty but can be extended in the future as needed.


}

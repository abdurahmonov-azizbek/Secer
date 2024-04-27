using Microsoft.AspNetCore.Mvc;
using Secer.Models;
using Secer.Services;

namespace Secer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExampleController(IRequestService requestService) : ControllerBase
{
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUpAsync()
    {
        var requestModel = new RequestModel
        {
            IpAddress = HttpContext.Connection.RemoteIpAddress!.ToString(),
            MethodName = nameof(SignUpAsync),
            RequestTime = DateTime.UtcNow,
        };

        var requestLimitValidationResult = await requestService.IsValid(requestModel, 5);

        if (requestLimitValidationResult)
        {
            //logic code
            return Ok("Just imagine, code successfully executed :)");
        }
        else
        {
            return BadRequest("An error occurred about the limit");
        }
    }

}

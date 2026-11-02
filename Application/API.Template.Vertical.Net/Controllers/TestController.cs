using Microsoft.AspNetCore.Mvc;

namespace API.Template.Vertical.Net.Controllers;

/// <summary>
/// Provides sample API responses for testing the application.
/// </summary>
[Route("api/test")]
public sealed class TestController : BaseApiController
{
    /// <summary>
    /// Returns a sample JSON response.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<SampleResponse>(StatusCodes.Status200OK)]
    public ActionResult<SampleResponse> Get(CancellationToken cancellationToken)
    {
        return Ok(new SampleResponse("Test controller is working."));
    }
}

/// <summary>
/// Represents the sample response returned by the test endpoint.
/// </summary>
public sealed record SampleResponse(string Message);

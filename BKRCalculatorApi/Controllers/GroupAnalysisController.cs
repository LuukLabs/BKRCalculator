using KDVManager.BKRCalculator;
using Microsoft.AspNetCore.Mvc;

namespace BKRCalculatorApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupAnalysisController : ControllerBase
{
    private readonly ILogger<GroupAnalysisController> _logger;

    public GroupAnalysisController(ILogger<GroupAnalysisController> logger)
    {
        _logger = logger;
    }

    [HttpPost(Name = "CalculateBKR")]
    public ActionResult<GroupAnalysisResult> CalculateBKR([FromBody] AgeGroupCounts counts)
    {
        GroupAnalyzer groupAnalyzer = new GroupAnalyzer();

        try
        {
            var result = groupAnalyzer.CalculateBKR(counts);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

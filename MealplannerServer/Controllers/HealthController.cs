using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MealplannerServer.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        [Produces("application/json")]
        [HttpGet("ping")]
        public ActionResult Ping()
        {
            return Ok(new { Message = "pong!" });

            return Ok();
        }
    }
}
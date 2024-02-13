using Microsoft.AspNetCore.Mvc;

namespace ImoveisApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class CorretorController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new List<object>
            {
                new {Nome = "Monalisa Brito"},
                new {Nome = "Danuzio Saraiva"}
            });
        }
    }
}

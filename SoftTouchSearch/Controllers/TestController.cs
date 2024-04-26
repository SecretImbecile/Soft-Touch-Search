using Microsoft.AspNetCore.Mvc;

namespace SoftTouchSearch.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return StatusCode(StatusCodes.Status302Found);
        }
    }
}

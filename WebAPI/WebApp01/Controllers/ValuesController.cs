using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApp01.Models;

namespace WebApp01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
      
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Content("hello world!");
        }

    }
}

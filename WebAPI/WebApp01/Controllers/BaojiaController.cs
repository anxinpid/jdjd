using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApp01.Models;

namespace WebApp01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaojiaController : ControllerBase
    {
        private readonly ICache _cache;
        public BaojiaController(ICache cache)
        {
            _cache = cache;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var res = new AjaxResult
            {
                Success = true,
                Data = ((_cache.GetCache("hellojack") as List<BaojiaModel>) ?? new List<BaojiaModel>()).OrderByDescending(x => x.Id)
            };

            return Content(res.ToJson(), "application/json", Encoding.UTF8);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BaojiaModel model)
        {
            var res = new AjaxResult();

            var list = (_cache.GetCache("hellojack") as List<BaojiaModel>) ?? new List<BaojiaModel>();

            if (list.Any(x => x.Id == model.Id))
            {
                list.Remove(list.FirstOrDefault(x => x.Id == model.Id));
                list.Add(model);
                _cache.SetCache("hellojack", list);
                //_cache.SetCache("hellojack_bak", list);
            }
            else
            {
                model.Id = list.Count > 0 ? list.Max(x => x.Id) + 1 : 1;
                list.Add(model);
                _cache.SetCache("hellojack", list);
            }

            return Content(res.ToJson(), "application/json", Encoding.UTF8);
        }

    }
}

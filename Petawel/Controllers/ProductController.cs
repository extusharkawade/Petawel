using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Petawel.Controllers.Models;

namespace Petawel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public Response Test()
        {
            return new Response();
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using NtpApi.Services.Mongo;

namespace NtpApi.Controllers
{
    [Route("transform")]
    public class TransformationsController : Controller
    {
        [HttpGet("Fixtures")]
        public ActionResult Fixtures()
        {
            
           
            return StatusCode(200, new JsonAdapter().getPreparedForInsert("head2head.json"));
        }
    }
}
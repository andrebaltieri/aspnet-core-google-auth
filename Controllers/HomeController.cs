using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace GAuth.Controllers
{
    [Route("")]
    public class HomeController : ControllerBase
    {
        [Route("")]
        [HttpGet]
        public ActionResult<dynamic> Get()
        {
            return Thread.CurrentPrincipal?.Identity.Name;
        }
    }
}
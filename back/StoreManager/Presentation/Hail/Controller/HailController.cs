using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StoreManager.Presentation.Hail.Controller
{
    [AllowAnonymous]
    [ApiController]
    [Route("/api/hail")]
    public class HailController:ControllerBase
    {
        public ActionResult HailApp()
        {
            return Ok();
        }
    }
}

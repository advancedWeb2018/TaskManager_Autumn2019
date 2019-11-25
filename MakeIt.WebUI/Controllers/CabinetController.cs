using AutoMapper;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    [Authorize]
    public class CabinetController : BaseController
    {
        public CabinetController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }       
    }
}
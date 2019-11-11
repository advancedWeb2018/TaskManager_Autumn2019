using AutoMapper;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IMapper _mapper;

        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
using AutoMapper;
using System.Web.Mvc;

namespace MakeIt.WebUI.Controllers
{
    public class BaseController : Controller
    {
        private IMapper _mapper = null;
        protected IMapper Mapper
        {
            get
            {
                if (_mapper == null) _mapper = MvcApplication.MapperConfiguration.CreateMapper();
                return _mapper;
            }
        }
    }
}
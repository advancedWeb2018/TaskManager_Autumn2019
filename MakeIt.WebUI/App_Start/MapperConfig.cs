using AutoMapper;

namespace MakeIt.WebUI.App_Start
{
    public class MapperConfig
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(_ =>
            {
                _.AddProfile(new MapperProfile());
            });
        }
    }
}
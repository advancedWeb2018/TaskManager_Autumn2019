using AutoMapper;
using MakeIt.BLL.Common;
using MakeIt.BLL.DTO;
using MakeIt.DAL.EF;
using MakeIt.Repository.Repository;
using MakeIt.Repository.UnitOfWork;

namespace MakeIt.BLL.Service.Authorithation
{
    public interface IAuthorizationService : IEntityService<User>
    {
        UserAuthDTO MappingTest(UserAuthDTO userDto);
    }

    public class AuthorizationService : EntityService<User>, IAuthorizationService
    {
        IUnitOfWork _unitOfWork;

        public AuthorizationService(IMapper mapper, IUnitOfWork uow) 
            : base(mapper, uow)
        {
            _unitOfWork = uow;
        }

        public UserAuthDTO MappingTest(UserAuthDTO userDto)
        {
            using (_unitOfWork)
            {
                var foo = _unitOfWork.Users.Add(new User { Email = "dfsfsf", UserName = "dsfsffs"});
            }
            var testEntityObject = _mapper.Map<User>(userDto);

            var testDtoObject = _mapper.Map<UserAuthDTO>(testEntityObject);

            return testDtoObject;
        }
    }
}

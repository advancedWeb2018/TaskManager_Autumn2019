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
        IUserRepository _userRepository;

        public AuthorizationService(IMapper mapper, IUnitOfWork uow, IUserRepository userRepository) 
            : base(mapper, uow, userRepository)
        {
            _unitOfWork = uow;
            _userRepository = userRepository;
        }

        public UserAuthDTO MappingTest(UserAuthDTO userDto)
        {
            var testEntityObject = _mapper.Map<User>(userDto);

            var testDtoObject = _mapper.Map<UserAuthDTO>(testEntityObject);

            return testDtoObject;
        }
    }
}

using AutoMapper;
using MakeIt.BLL.Service.Authorithation;
using MakeIt.DAL.EF;
using MakeIt.Repository.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace MakeIt.Test.Services
{
    [TestClass]
    public class AuthorizationServiceTest
    {
        IAuthorizationService _service;
        Mock<IUnitOfWork> _mockUnitWork;
        Mock<IMapper> _mockMapper;
        List<User> listUser;

        [TestInitialize]
        public void Initialize()
        {
            _mockUnitWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _service = new AuthorizationService(_mockMapper.Object, _mockUnitWork.Object);
            listUser = new List<User>() {
                new User {UserName = "User1", PasswordHash="123456", Email="User1@gmail.com"},
                new User {UserName = "User2", PasswordHash="234567", Email="User2@gmail.com"},
                new User {UserName = "User3", PasswordHash="345678", Email="User3@gmail.com"}
            };
        }

        [TestMethod]
        public void User_Get_All()
        {
            //Arrange
            _mockUnitWork.Setup(x => x.GetRepository<User>().GetAll()).Returns(listUser);

            //Act
            List<User> results = _service.GetAll() as List<User>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Can_Add_User()
        {
            //Arrange
            int Id = 1;
            User user = new User() { UserName = "User4", PasswordHash = "654321", Email = "User4@gmail.com" };
            _mockUnitWork.Setup(m => m.GetRepository<User>().Add(user)).Returns((User e) =>
            {
                e.Id = Id;
                return e;
            });


            //Act
            _service.Create(user);

            //Assert
            Assert.AreEqual(Id, user.Id);
            _mockUnitWork.Verify(m => m.SaveChanges(), Times.Once);
        }
    }
}

using MakeIt.DAL.EF;
using MakeIt.Repository.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MakeIt.Test.Repositories
{
    [TestClass]
    public class UserRepositoryTestWithDB
    {
        TestContext databaseContext;
        UserRepository objRepo;

        [TestInitialize]
        public void Initialize()
        {
            databaseContext = new TestContext();
            objRepo = new UserRepository(databaseContext);
        }

        [TestMethod]
        public void User_Repository_Get_ALL()
        {
            // Act
            var result = objRepo.GetAll().ToList();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual("User1", result[0].UserName);
            Assert.AreEqual("User2", result[1].UserName);
            Assert.AreEqual("User3", result[2].UserName);
        }

        [TestMethod]
        public void User_Repository_Create()
        {
            // Arrange
            User user = new User() { UserName = "User4", PasswordHash = "654321", Email = "User4@gmail.com" };

            // Act
            objRepo.Add(user);
            databaseContext.SaveChanges();

            var lst = objRepo.GetAll().ToList();

            // Assert
            Assert.AreEqual(4, lst.Count);
            Assert.AreEqual("User4", lst.Last().UserName);
        }
    }
}

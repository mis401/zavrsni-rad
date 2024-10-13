using Castle.Core.Logging;
using event_driven_backend.Controllers;
using event_driven_backend.DTOs;
using event_driven_backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace EventDrivenTest
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController controller;
        [SetUp]
        public void Setup()
        {
            this.controller = new UserController();
        }

        [Test]
        public void CreateUserTest()
        {
            var newUser = new NewUserDTO { Name = "Test", Surname = "Test", Email = "test@mail.com", Password = "test" };
            var result = controller.CreateUser(newUser).Result;
            Console.WriteLine(result);
            Assert.Equals(result, "true");
        }

        [TearDown]
        public void TearDown()
        {
        }
    }
}
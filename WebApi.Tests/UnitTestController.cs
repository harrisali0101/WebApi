using DAL;
using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests
{
    public class UnitTestController
    {
        private readonly ApplicantDetailsRepository applicantDetailsRepository;
        public static DbContextOptions<ApplicantDetailsContext> dbContextOptions { get; }
        public static string connectionString = "Server=(local)\\sqlexpress;Database=DBtest;Trusted_Connection=True;MultipleActiveResultSets=True;";
        public ILogger<ApplicantDetailsController> logger;
        
         
        static UnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<ApplicantDetailsContext>()
                .UseInMemoryDatabase("Applicants")
                .Options;
        }

        public UnitTestController()
        {
            var context = new ApplicantDetailsContext(dbContextOptions);
            DummyDataInitilizer db = new DummyDataInitilizer();
            db.Seed(context);

            applicantDetailsRepository = new ApplicantDetailsRepository(context);
            logger = new Mock<ILogger<ApplicantDetailsController>>().Object;

        }
        [Fact]
        public  void Task_GetApplicantById_Return_OkResult()
        {
            //Arrange  
            var controller = new ApplicantDetailsController(applicantDetailsRepository,logger);
            var postId = 7;

            //Act  
            var data =  controller.GetById(postId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        

        [Fact]
        public  void Task_GetAllApplicants_Return_OkResult()
        {
            //Arrange  
            var controller = new ApplicantDetailsController(applicantDetailsRepository,logger);

            //Act  
            var result =  controller.GetAllApplicants();

            //Assert  
            Assert.Equal(6,result.Count());

        }
        [Fact]
        public async void Task_Add_ValidData_Return_CreatedAtActionResult()
        {
            //Arrange  
            var controller = new ApplicantDetailsController(applicantDetailsRepository,logger);
            var applicant = new ApplicantDetails() {
                Name = "Bilal Awan",
                FamilyName = "Malik",
                Address = "Dha phase 2 q block house 171",
                CountryOfOrigin = "Pakistan",
                EmailAddress = "bilal@gmai.com",
                Age = 31,
                Hired = true
            };

            //Act  
            var data = await controller.CreateAsync(applicant);

            //Assert  
            Assert.IsType<CreatedAtActionResult>(data);
        }
        

        [Fact]
        public async void Task_Update_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new ApplicantDetailsController(applicantDetailsRepository,logger);
            var postId = 2;

            //Act  
            var applicant = new ApplicantDetails();
            applicant.Name = "Harris";
            applicant.FamilyName="Malik Awan" ;
            applicant.Address ="dha phase 2 q block lahore" ;
            applicant.EmailAddress ="abc@gmail.com" ;
            applicant.Age = 24;
            applicant.CountryOfOrigin = "Pakistan";
            applicant.Hired =true;

            var updatedData = await controller.EditAsync(postId, applicant);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }

        [Fact]
        public async void Task_Delete_Applicant_Return_OkResult()
        {
            //Arrange  
            var controller = new ApplicantDetailsController(applicantDetailsRepository,logger);
            var postId = 2;

            //Act  
            var data = await controller.DeleteAsync(postId);

            //Assert  
            Assert.IsType<OkResult>(data);
        }





    }
}

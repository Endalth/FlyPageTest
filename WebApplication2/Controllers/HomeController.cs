using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            TestClass testClass = new TestClass();
            testClass.Id = 123;
            testClass.Name = "asdas";
            testClass.SimpleList = new List<string>() {"asdas","dfdggfh","fhggh","hjtye" };
            testClass.ClassList = new List<ListTest>()
            {
                new ListTest(){Id = 4321, Name = "inside list"},
                new ListTest(){Id = 4321345, Name = "inside list 123"}
            };

            //TestClass testClass2 = new TestClass();
            //testClass.Id = 1234546;
            //testClass.Name = "asdassfsdfs";

            //List<TestClass> list = new List<TestClass>() { testClass, testClass2 };

            GenericModel genericModel = new GenericModel();
            genericModel.ActualModel = testClass;

            return View(genericModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
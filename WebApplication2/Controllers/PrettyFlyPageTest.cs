using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Web;
using WebApplication2.FlyPageFiles;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PrettyFlyPageTest : Controller
    {
        PageFactory pageFactory;

        public PrettyFlyPageTest(PageFactory pageFactory)
        {
            this.pageFactory = pageFactory;
        }

        [HttpGet]
        public IActionResult Index(string requestType)
        {

            GenericModel genericModel = new GenericModel();
            genericModel.ActualModel = pageFactory.GetDTO(requestType);
            genericModel.RequestedType = requestType;

            return View(genericModel);
        }

        [HttpGet]
        public IActionResult Delete()
        {
            NameValueCollection queryStringCollection = HttpUtility.ParseQueryString(Request.QueryString.Value);

            string requestType = queryStringCollection.Get("requestType");
            queryStringCollection.Remove("requesttype");
            var queryString = queryStringCollection.ToString();

            GenericModel genericModel = new GenericModel();
            genericModel.ActualModel = pageFactory.GetDTO(requestType, PageFactory.OperationTypes.Delete, queryString);
            genericModel.RequestedType = requestType;

            return RedirectToAction("Index", new { requestType= requestType });
        }

        [HttpGet]
        public IActionResult Add(string requestType)
        {
            GenericModel genericModel = new GenericModel();
            genericModel.ActualModel = pageFactory.GetDTO(requestType, PageFactory.OperationTypes.AddGet);
            genericModel.RequestedType = requestType;

            return View(genericModel);
        }

        [HttpPost]
        public IActionResult Add(IFormCollection formCol)
        {
            //TODO Converting to queryString may not be safe if the values are not checked
            //Send as collection by adding another method to Factory and constructor DAL that accepts collections?
            //Or convert queryString to collection everywhere else also?
            string queryString = string.Join("&", formCol.Select(x => x.Key + "=" + x.Value));
            string reqType = formCol["RequestedType"];
            GenericModel genericModel = new GenericModel();
            genericModel.ActualModel = pageFactory.GetDTO(reqType, PageFactory.OperationTypes.AddPost, queryString);
            genericModel.RequestedType = reqType;

            return RedirectToAction("Index", new { requestType = reqType });
        }
    }

    
}

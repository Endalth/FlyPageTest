using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;
using WebApplication2.ApiAccess;

namespace WebApplication2.FlyPageFiles
{
    public class PageFactory
    {
        public enum OperationTypes
        {
            List,
            AddGet,
            AddPost,
            UpdateGet,
            UpdatePost,
            Delete
        }
        public static Dictionary<string, Type> PageCodes { get; set; } = new Dictionary<string, Type>
        {
            ["status"] = typeof(StatusDAL),
            ["corp"] = typeof (CorpDAL),
        };

        BaseApi _api;

        public PageFactory(BaseApi api)
        {
            _api = api;
        }

        public object GetDTO(string pageCode, OperationTypes opType = OperationTypes.List, string queryString = "")
        {
            if (pageCode == null || !PageCodes.ContainsKey(pageCode))
                return null;

            var requestedTypeInstance = Activator.CreateInstance(PageCodes[pageCode]);
            MethodInfo methodToBeInvoked = requestedTypeInstance.GetType().GetMethod(opType.ToString());

            if(methodToBeInvoked != null)
            {
                methodToBeInvoked.Invoke(requestedTypeInstance, new object[] { _api, queryString });

                var dto = requestedTypeInstance.GetType().GetProperty("DTO");
                var data = dto.GetValue(requestedTypeInstance);
                return data;
            }
            else
            {
                return null;
            }

        }
    }
}

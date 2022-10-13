using System.ComponentModel.DataAnnotations;
using WebApplication2.ApiAccess;
using static WebApplication2.FlyPageFiles.PageFactory;

namespace WebApplication2.FlyPageFiles
{
    public class StatusDAL
    {
        public object DTO { get; set; }

        //public StatusDAL(BaseApi api, OperationTypes opType, string queryString)
        //{
        //    switch (opType)
        //    {
        //        case OperationTypes.List:
        //            DTO = api.GetListStatus();
        //            break;
        //        case OperationTypes.AddGet:
        //            break;
        //        case OperationTypes.AddPost:
        //            break;
        //        case OperationTypes.UpdateGet:
        //            break;
        //        case OperationTypes.UpdatePost:
        //            break;
        //        case OperationTypes.Delete:
        //            DTO = api.FakeDeleteStatus(queryString);
        //            break;

        //    }
        //}

        public void List(BaseApi api, string queryString = "")
        {
            DTO = api.GetListStatus();
        }

        public void Delete(BaseApi api, string queryString = "")
        {
            DTO = api.FakeDeleteStatus(queryString);
        }
    }

    public class StatusListDTO
    {
        public List<StatusListTableRowDTO> ClassList { get; set; }
    }

    public class StatusListTableRowDTO
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }

        [Display(Name = "Durum Bilgisi")]
        public string StatusName { get; set; }
    }
}

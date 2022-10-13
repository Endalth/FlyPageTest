using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using WebApplication2.ApiAccess;
using static WebApplication2.FlyPageFiles.PageFactory;

namespace WebApplication2.FlyPageFiles
{
    public class CorpDAL
    {
        public object DTO { get; set; }

        //public CorpDAL(BaseApi api, OperationTypes opType, string queryString)
        //{
        //    switch (opType)
        //    {
        //        case OperationTypes.List:
        //            DTO = api.GetListCorps();
        //            break;
        //        case OperationTypes.AddGet:
        //            DTO = AddGet(api);
        //            break;
        //        case OperationTypes.AddPost:
        //            DTO = AddPost(api, queryString);
        //            break;
        //        case OperationTypes.UpdateGet:
        //            break;
        //        case OperationTypes.UpdatePost:
        //            break;
        //        case OperationTypes.Delete:
        //            break;
        //    }
        //}

        public void List(BaseApi api, string queryString = "")
        {
            DTO = api.GetListCorps();
        }

        public void AddGet(BaseApi api, string queryString = "")
        {
            CorpAddGetDTO dto = api.CorpAddGet();

            CorpAddViewModel vm = new CorpAddViewModel();
            vm.Id = 23;
            vm.Addresses = dto.Addresses.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            vm.PackageTypes = dto.PackageTypes.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            DTO = vm;
        }

        public void AddPost(BaseApi api, string queryString = "")
        {
            var values = HttpUtility.ParseQueryString(queryString);
            CorpAddSendDTO sendDTO = new CorpAddSendDTO();
            foreach (var prop in sendDTO.GetType().GetProperties())
            {
                if(prop.PropertyType == typeof(bool))
                {
                    bool val = values.Get(prop.Name) == "on";

                    prop.SetValue(sendDTO, val);
                }
                else
                {
                    prop.SetValue(sendDTO, Convert.ChangeType(values.Get(prop.Name), prop.PropertyType));
                }
            }

            DTO = api.CorpAddPost(sendDTO);
        }
    }

    public class CorpListDTO
    {
        public List<CorpListTableRowDTO> ClassList { get; set; }
    }

    public class CorpListTableRowDTO
    {
        [Display(Name = "Id")]
        [Key]
        public int Id { get; set; }
        [Display(Name = "Şirket Adı")]
        public string CompanyName { get; set; }
        [Display(Name = "Adres Bilgisi")]
        public string AddressName { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Şirket Tipi")]
        public string CorporationTypeName { get; set; }
        [Display(Name = "Paket Adı")]
        public string PackageName { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }
    }

    public class CorpAddGetDTO
    {
        public List<IdNameDTO> Addresses { get; set; }
        public List<IdNameDTO> PackageTypes { get; set; }
    }

    public class CorpAddSendDTO
    {
        public int Id { get; set; }
        public string CorpName { get; set; }
        public string Password { get; set; }
        public bool International { get; set; }
        public DateTime FoundingDate { get; set; }
        public int AddressId { get; set; }
        public int PackageTypeId { get; set; }
    }

    public class CorpAddViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [Display(Name = "Şirket Adı")]
        public string CorpName { get; set; }
        [PasswordPropertyText]
        [Display(Name = "Şirket Şifre")]
        public string Password { get; set; }
        [Display(Name = "Uluslararası Şirket?")]
        public bool International { get; set; }
        [Display(Name = "Kuruluş Tarihi")]
        public DateTime FoundingDate { get; set; }
        [NotMapped]
        public int AddressId { get; set; }
        [NotMapped]
        public int PackageTypeId { get; set; }
        [Column("AddressId")]
        public List<SelectListItem> Addresses { get; set; }
        [Column("PackageTypeId")]
        public List<SelectListItem> PackageTypes { get; set; }
    }

    public class IdNameDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

using System.Data.SqlClient;
using WebApplication2.FlyPageFiles;

namespace WebApplication2.ApiAccess
{
    public class BaseApi
    {
        public StatusListDTO GetListStatus()
        {
            StatusListDTO testListDTO = new StatusListDTO();
            testListDTO.ClassList = new List<StatusListTableRowDTO>();
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarSaleDB;Integrated Security=true;"))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select Id, StatusName from StatusValue", connection))
                {
                    using(SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testListDTO.ClassList.Add(new StatusListTableRowDTO()
                            {
                                Id = (int)reader["id"],
                                StatusName = (string)reader["StatusName"],
                            });
                        }
                    }
                }
            }

            return testListDTO;
        }

        public CorpListDTO GetListCorps()
        {
            CorpListDTO testListDTO = new CorpListDTO();
            testListDTO.ClassList = new List<CorpListTableRowDTO>();
            using (SqlConnection connection = new SqlConnection("Data Source=.;Initial Catalog=CarSaleDB;Integrated Security=true;"))
            {
                connection.Open();

                string sql = "select c.Id, CompanyName, ai.AddressName, c.PhoneNumber, ct.CorporationTypeName, cpt.PackageName, bu.Username from Corporation c join AddressInfo ai on c.AddressInfoId = ai.Id join CorporationType ct on c.CorporationTypeId = ct.Id join CorporatePackageType cpt on c.CorporatePackageTypeId = cpt.Id join BaseUser bu on c.CreatedBy = bu.Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            testListDTO.ClassList.Add(new CorpListTableRowDTO()
                            {
                                Id = (int)reader["id"],
                                CompanyName = (string)reader["CompanyName"],
                                AddressName = (string)reader["AddressName"],
                                PhoneNumber = (string)reader["PhoneNumber"],
                                CorporationTypeName = (string)reader["CorporationTypeName"],
                                PackageName = (string)reader["PackageName"],
                                Username = (string)reader["Username"]
                            });
                        }
                    }
                }
            }

            return testListDTO;
        }

        public bool FakeDeleteStatus(string queryString)
        {
            return false;
        }

        public CorpAddGetDTO CorpAddGet()
        {
            CorpAddGetDTO dto = new CorpAddGetDTO();
            dto.Addresses = new List<IdNameDTO>();
            dto.Addresses.Add(new IdNameDTO()
            {
                Id = 234,
                Name = "Address 1"
            });
            dto.Addresses.Add(new IdNameDTO()
            {
                Id = 35,
                Name = "Address 2"
            });

            dto.PackageTypes = new List<IdNameDTO>();
            dto.PackageTypes.Add(new IdNameDTO()
            {
                Id = 3,
                Name = "Package 1"
            });
            dto.PackageTypes.Add(new IdNameDTO()
            {
                Id = 54,
                Name = "Package 2"
            });

            return dto;
        }

        public bool CorpAddPost(CorpAddSendDTO dto)
        {
            return true;
        }
    }
}

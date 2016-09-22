using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class AddressesServices : BaseService, IAddressesServices
    {
        private IAmentityService _AmentityService;
        private IUserService _UserService;
        public AddressesServices(IAmentityService AmentityService, IUserService UserService)
        {
            _AmentityService = AmentityService;
            _UserService = UserService;
        }
        public List<Address> GetAll()
        {
            List<Address> list = null;

            //DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetAllAddresses", inputParamMapper: null, map: delegate (IDataReader reader, short set)
            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetAllWithGeoInfo", inputParamMapper: null, map: delegate (IDataReader reader, short set)
            {
                Address p = AddressMapper(reader);

                if (list == null)
                {
                    list = new List<Address>();
                }
                list.Add(p);
            }
            );
            return list;

        }

        public Address GetById(int id)
        {
            Address Item = null;

            // DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetByIdAddress", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetByIdVer2", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }
            , map: delegate (IDataReader reader, short set)
            {
                switch (set)
                {
                    case 0:
                        Item = AddressMapper(reader);
                        break;
                    case 1:
                        Amentity am = _AmentityService.AmentityMapper(reader);

                        if (Item.Amentities == null)
                        {
                            Item.Amentities = new List<Amentity>();
                        }

                        Item.Amentities.Add(am);
                        break;
                    default:
                        break;
                }
            });

            return Item;
        }


        public List<AddressV2> GetByDistance(DistanceRequest model)
        {
            List<AddressV2> list = null;

            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetByDistanceV2", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Lat", model.Lat);
                paramCollection.AddWithValue("@Long", model.Lng);
                paramCollection.AddWithValue("@Distance", model.Distance);
                paramCollection.AddWithValue("@ItemsOnMap", model.ItemsOnMap);
            }
            , map: delegate (IDataReader reader, short set)
            {
                switch (set)
                {
                    case 0:
                        AddressV2 mar = AddressMapperWithStateName(reader);

                        if (list == null)
                        {
                            list = new List<AddressV2>();
                        }

                        list.Add(mar);
                        break;
                    default:
                        break;
                }
            });

            return list;
        }


        public int Insert(AddressesInsertRequest model)
        {
            int id = 0;

            // DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_InsertAddress", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_InsertAddressVer2", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Address1", model.Address1);
                paramCollection.AddWithValue("@Address2", model.Address2);
                paramCollection.AddWithValue("@City", model.City);
                paramCollection.AddWithValue("@StateId", model.StateId);
                paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                paramCollection.AddWithValue("@UserId", _UserService.GetCurrentUserId());
                paramCollection.AddWithValue("@Lat", model.Lat);
                paramCollection.AddWithValue("@Long", model.Long);

                SqlParameter p = new SqlParameter("@Id", System.Data.SqlDbType.Int);
                p.Direction = System.Data.ParameterDirection.Output;
                paramCollection.Add(p);

                p = new SqlParameter("@Amentities", System.Data.SqlDbType.Structured);

                if (model.Amentities != null && model.Amentities.Any())
                {
                    p.Value = new Sabio.Data.IntIdTable(model.Amentities);
                }

                paramCollection.Add(p);

            }, returnParameters: delegate (SqlParameterCollection param)
            {
                int.TryParse(param["@Id"].Value.ToString(), out id);
            }
             );
            return id;
        }
        public void Update(AddressesUpdateRequest model)
        {
            // DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_UpdateAddress"
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.Addresses_UpdateVer2"
           , inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Address1", model.Address1);
                paramCollection.AddWithValue("@Address2", model.Address2);
                paramCollection.AddWithValue("@City", model.City);
                paramCollection.AddWithValue("@StateId", model.StateId);
                paramCollection.AddWithValue("@ZipCode", model.ZipCode);
                paramCollection.AddWithValue("@UserId", _UserService.GetCurrentUserId());
                paramCollection.AddWithValue("@Id", model.Id);
                paramCollection.AddWithValue("@Lat", model.Lat);
                paramCollection.AddWithValue("@Long", model.Long);

                SqlParameter p = new SqlParameter("@Amentities", System.Data.SqlDbType.Structured);

                if (model.Amentities != null && model.Amentities.Any())
                {
                    p.Value = new Sabio.Data.IntIdTable(model.Amentities);
                }

                paramCollection.Add(p);

            }, returnParameters: delegate (SqlParameterCollection param) { }

           );
        }


        public void Delete(int id)
        {
            // DataProvider.ExecuteNonQuery(GetConnection, "dbo.addresses_DeleteAddress", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.addresses_DeleteVer2", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }
            );
        }


        private Address AddressMapper(IDataReader reader)
        {
            Address p = new Address();
            int startingIndex = 0;

            p.Id = reader.GetSafeInt32(startingIndex++);
            p.Address1 = reader.GetSafeString(startingIndex++);
            p.Address2 = reader.GetSafeString(startingIndex++);
            p.City = reader.GetSafeString(startingIndex++);
            p.StateId = reader.GetSafeInt32(startingIndex++);
            p.ZipCode = reader.GetSafeString(startingIndex++);
            p.Lat = reader.GetSafeDouble(startingIndex++);
            p.Long = reader.GetSafeDouble(startingIndex++);
            p.MaximumAllowableRent = reader.GetSafeInt32(startingIndex++);



            return p;
        }

        public AddressV2 AddressMapperWithStateName(IDataReader reader)
        {
            AddressV2 p = new AddressV2();
            int startingIndex = 0;

            p.ListingId = reader.GetSafeInt32(startingIndex++);
            p.AddressId = reader.GetSafeInt32(startingIndex++);

            // p.Id = reader.GetSafeInt32(startingIndex++);
            p.Address1 = reader.GetSafeString(startingIndex++);
            // p.Address2 = reader.GetSafeString(startingIndex++);
            p.City = reader.GetSafeString(startingIndex++);
            p.StateName = reader.GetSafeString(startingIndex++);
            p.ZipCode = reader.GetSafeString(startingIndex++);
            p.Lat = reader.GetSafeDouble(startingIndex++);
            p.Long = reader.GetSafeDouble(startingIndex++);
            p.Apn = reader.GetSafeString(startingIndex++);
            p.Status = reader.GetSafeBool(startingIndex++);
            p.AvailableDate = reader.GetSafeDateTime(startingIndex++);
            p.FilePath = reader.GetSafeString(startingIndex++);


            return p;
        }

        public Address AddressMapper1(IDataReader reader)
        {
            Address p = new Address();
            int startingIndex = 0;

            p.Id = reader.GetSafeInt32(startingIndex++);
            p.Address1 = reader.GetSafeString(startingIndex++);
            p.Address2 = reader.GetSafeString(startingIndex++);
            p.City = reader.GetSafeString(startingIndex++);
            p.StateId = reader.GetSafeInt32(startingIndex++);
            p.StateName = reader.GetSafeString(startingIndex++);
            p.ZipCode = reader.GetSafeString(startingIndex++);
            p.Lat = reader.GetSafeDouble(startingIndex++);
            p.Long = reader.GetSafeDouble(startingIndex++);

            return p;
        }



        public List<Address> searchAddress(string address)
        {
            List<Address> list = null;

            // DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_GetByIdAddress", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            DataProvider.ExecuteCmd(GetConnection, "dbo.Addresses_searchAddress", inputParamMapper: delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@variable", address);
            }
            , map: delegate (IDataReader reader, short set)
            {
                Address p = new Address();
                int startingIndex = 0;

                p.Address1 = reader.GetSafeString(startingIndex++);
                p.Id = reader.GetSafeInt32(startingIndex++);
                p.Address2 = reader.GetSafeString(startingIndex++);
                p.City = reader.GetSafeString(startingIndex++);
                p.StateId = reader.GetSafeInt32(startingIndex++);
                p.ZipCode = reader.GetSafeString(startingIndex++);
                p.Lat = reader.GetSafeDouble(startingIndex++);
                p.Long = reader.GetSafeDouble(startingIndex++);

                if (list == null)
                {
                    list = new List<Address>();
                }
                list.Add(p);
            }
            );
            return list;
        }

    }
}

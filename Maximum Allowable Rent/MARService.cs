using Sabio.Data;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Sabio.Web.Services.Interfaces;

namespace Sabio.Web.Services
{
    public class MARService : BaseService, IMARService
    {


        public  int Insert(MarAddRequest  model)
        {
            int Id = 0;

            DataProvider.ExecuteNonQuery(GetConnection, "dbo.MARStaging_Insert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {


                   paramCollection.AddWithValue("@ain", model.ain);
                   paramCollection.AddWithValue("@id", model.id);
                   paramCollection.AddWithValue("@street_number", model.street_number);
                   paramCollection.AddWithValue("@street_name", model.street_name);
                   paramCollection.AddWithValue("@unit", model.unit);
                   paramCollection.AddWithValue("@map_point_zip", model.map_point_zip);
                   paramCollection.AddWithValue("@map_point_city", model.map_point_city);
                   paramCollection.AddWithValue("@map_point_state", model.map_point_state);
                   paramCollection.AddWithValue("@maximum_allowable_rent", model.maximum_allowable_rent);

                   paramCollection.AddWithValue("@map_point_address", model.map_point_address);
                   paramCollection.AddWithValue("@zip_code", model.zip_code);

                   SqlParameter p = new SqlParameter("@Keyid", System.Data.SqlDbType.Int);
                   p.Direction = System.Data.ParameterDirection.Output;

                   paramCollection.Add(p);

               }, returnParameters: delegate (SqlParameterCollection param)
               {
                   int.TryParse(param["@id"].Value.ToString(), out Id);
               }
               );
            return Id;
        }


        public  void BulkUpload(MarAddRequest [] model)
        {
            DataProvider.ExecuteNonQuery(GetConnection, "dbo.MARStaging_BulkInsert"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)
               {
                   SqlParameter p = new SqlParameter("@MARStagingBulk", System.Data.SqlDbType.Structured);

                   p.Value = new Sabio.Data.MARStagingBulk_Structured(model);
                   

                   paramCollection.Add(p);
               }
                   );
        }
    }
}
    

    
          
           
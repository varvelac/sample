using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services.Interfaces;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers
{


    [RoutePrefix("api/mar")]
    public class MarApiController : ApiController
    {


        private IMARService _marService;

        public MarApiController(IMARService marService)
        {
            _marService = marService;
        }



        [Route, HttpPost]
        public HttpResponseMessage Insert(MarAddRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _marService.Insert(model);

            return Request.CreateResponse(HttpStatusCode.OK);
        }


        [Route("bulk"), HttpPost]
        public HttpResponseMessage BulkUpload(MarAddRequest[] model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _marService.BulkUpload(model);

            return Request.CreateResponse(new SuccessResponse()); 
        }



    }
}
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/addresses")]

    public class AddressesApiController : ApiController
    {
        private IAddressesServices _addressesservice;

        public AddressesApiController(IAddressesServices addressesservice)
        {
            _addressesservice = addressesservice;
        }

        [Route, HttpGet]
        public HttpResponseMessage GetAll()
        {

            ItemsResponse<Address> response = new ItemsResponse<Address>();

            response.Items = _addressesservice.GetAll();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [Route("{id:int}")]

        public HttpResponseMessage GetById(int id)
        {
            ItemResponse<Address> response = new ItemResponse<Address>();

            response.Item = _addressesservice.GetById(id);
            if (response.Item == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, response);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [Route("{distance}")]

        public HttpResponseMessage GetByDistance(DistanceRequest model)
        {
            ItemsResponse<AddressV2> response = new ItemsResponse<AddressV2>();

            response.Items = _addressesservice.GetByDistance(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [HttpPost]
        [Route]
        public HttpResponseMessage Insert(AddressesInsertRequest model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            ItemResponse<int> response = new ItemResponse<int>();
            response.Item = _addressesservice.Insert(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPut]
        [Route("{id:int}")]

        public HttpResponseMessage Update(AddressesUpdateRequest model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            _addressesservice.Update(model);


            return Request.CreateResponse(new SuccessResponse());
        }

        [HttpDelete]
        [Route("{id:int}")]

        public HttpResponseMessage Delete(int id)
        {
            _addressesservice.Delete(id);

            return Request.CreateResponse(new SuccessResponse());
        }


        [HttpGet]
        [Route("search/{address}")]

        public HttpResponseMessage searchAddress(string address)
        {
            ItemsResponse<Address> response = new ItemsResponse<Address>();

            response.Items = _addressesservice.searchAddress(address);
            if (response.Items == null)

            {
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

    }

}

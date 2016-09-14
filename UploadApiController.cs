
using Sabio.Web.Classes;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

[RoutePrefix("api/upload")]
public class UploadApiController : ApiController
{
    private IStorageService _storageService;
    private IFileServices _fileServices;
    private IUserService _userService;


    public UploadApiController(IStorageService storageService, IFileServices fileServices, IUserService userService)
    {
        _storageService = storageService;
        _fileServices = fileServices;
        _userService = userService;

    }

    [Route("local")]
    public async Task<HttpResponseMessage> Post()
    {
        if (!Request.Content.IsMimeMultipartContent())
        {
            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        }

        //string root = HttpContext.Current.Server.MapPath("~/App_Data"); <<---use for MultiPartform..etc string param to store code here

        var provider = new CustomMultipartFormDataStreamProvider("c:/SF.code/");

        try
        {
            await Request.Content.ReadAsMultipartAsync(provider);


            ItemsResponse<Sabio.Web.Domain.FileUpload> response = new ItemsResponse<Sabio.Web.Domain.FileUpload>();
            response.Items = _storageService.local(provider);

            return Request.CreateResponse(response);

        }
        catch (System.Exception e)
        {
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
        }

    }







    /////////////////////// AMAZON API//////////////////////////////////

    [HttpPost]
    [Route()]
    public async Task<HttpResponseMessage> Upload()
    {
        //StorageService storageService = new StorageService();

        if (!Request.Content.IsMimeMultipartContent())
        {
            this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        }

        InMemoryMultipartStreamProvider provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartStreamProvider>(new InMemoryMultipartStreamProvider());

        NameValueCollection formData = provider.FormData;

        IList<HttpContent> files = provider.Files;


        List<Sabio.Web.Domain.FileUpload> List = new List<Sabio.Web.Domain.FileUpload>();

        foreach (HttpContent file in files)
        {

            Stream fileStream = await file.ReadAsStreamAsync();

            Sabio.Web.Domain.FileUpload fileDesc = new Sabio.Web.Domain.FileUpload();
            fileDesc.Name = file.Headers.ContentDisposition.FileName;
            string fileName = fileDesc.Name.Replace("\"", string.Empty);
            fileDesc.Path = "/C19/" + fileName;
            

            

            _storageService.UploadFile(fileName, fileStream);

            FileServicesAddRequest model = new FileServicesAddRequest();
            model.FileName = fileName;
            model.FilePath = fileDesc.Path;


            var id = _fileServices.Insert(model);
            fileDesc.id = id;

            List.Add(fileDesc);
        }

        ItemsResponse<Sabio.Web.Domain.FileUpload> response = new ItemsResponse<Sabio.Web.Domain.FileUpload>();
        response.Items = List;

        return this.Request.CreateResponse(response);
    }
          


    ///uploadAvatar endpoint
    [HttpPost]
    [Route("avatar")]
    public async Task<HttpResponseMessage> UploadAvatar()
    {
        if (!Request.Content.IsMimeMultipartContent())
        {
            this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
        }

        InMemoryMultipartStreamProvider provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartStreamProvider>(new InMemoryMultipartStreamProvider());

        Sabio.Web.Domain.FileUpload file = await _storageService.UploadAvatar(provider);


        ItemResponse<Sabio.Web.Domain.FileUpload> response = new ItemResponse<Sabio.Web.Domain.FileUpload>();
        response.Item = file;

        return Request.CreateResponse(response);

    }


}





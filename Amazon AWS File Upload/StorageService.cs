using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Sabio.Web.Classes;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

public class StorageService : IStorageService
{
    private IAmazonS3 client = null;

    private const string awsBucketName = "sabio/C19";

    private IUserService _userServices;
    private IFileServices _fileServices;


    public StorageService(IUserService userService, IFileServices fileServices)
    {
        _fileServices = fileServices;
        _userServices = userService;
        string accessKey = ConfigurationManager.AppSettings["AWSAccessKey"];
        string secretKey = ConfigurationManager.AppSettings["AWSSecretKey"];
        if (this.client == null)
        {
            this.client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USWest2);
        }
    }

    public bool UploadFile(string key, Stream stream)
    {
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = stream,
            BucketName = awsBucketName,
            CannedACL = S3CannedACL.AuthenticatedRead,
            Key = key
        };

        TransferUtility fileTransferUtility = new TransferUtility(this.client);
        fileTransferUtility.Upload(uploadRequest);
        return true;
    }



    public List<Sabio.Web.Domain.FileUpload> local(CustomMultipartFormDataStreamProvider provider)
    {

        List<Sabio.Web.Domain.FileUpload> List = new List<Sabio.Web.Domain.FileUpload>();

        foreach (MultipartFileData file in provider.FileData)
        {
            Sabio.Web.Domain.FileUpload fileDesc = new Sabio.Web.Domain.FileUpload();
            fileDesc.Name = file.Headers.ContentDisposition.FileName;
            List.Add(fileDesc);
        }
        return List;
    }




    public async Task<FileUpload> UploadAvatar(InMemoryMultipartStreamProvider provider)
    {

        HttpContent file = provider.Files[0];

        Stream fileStream = await file.ReadAsStreamAsync();

        FileUpload fileDesc = new FileUpload();
        string fileName = file.Headers.ContentDisposition.FileName.Replace("\"", string.Empty);
        fileDesc.Name = fileName;
        fileDesc.Path = "/C19/" + fileName;
        UploadFile(fileName, fileStream);

        FileServicesAddRequest model = new FileServicesAddRequest();
        model.FileName = fileName;
        model.FilePath = fileDesc.Path;
        int fileId = _fileServices.Insert(model);

        var x = (provider.FormData.GetValues("userId"));
        int userId = Int32.Parse(x[0]);

        _userServices.InsertAvatarId(fileId, userId);

        return fileDesc;

    }

}

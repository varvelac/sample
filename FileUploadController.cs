using RentApp.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sabio.Web.Controllers
{
  
        [RoutePrefix("FileUpload")]
        public class FileUploadController : Controller
        {

        [Route()]
        public ActionResult upload()
        {
            return View();
        }

    }
}

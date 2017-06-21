using FineUploaderTest.Web.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FineUploaderTest.Web.Controllers
{
    public class UploaderController : Controller
    {
        // GET: Uploader
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public FineUploaderResult UploadFile(FineUpload upload)
        {
            var dir = Server.MapPath("~/Upload");
            int pos1 = upload.Filename.LastIndexOf("\\");
            string fileName = pos1 == -1 ? upload.Filename : upload.Filename.Substring(pos1 + 1, upload.Filename.Length - pos1 - 1);
            string storeName = Guid.NewGuid() + fileName;
            var filePath = Path.Combine(dir, storeName);
            try
            {
                upload.SaveAs(filePath);
                ViewBag.SourcePath = filePath;
            }
            catch (Exception ex)
            {
                //To Do
                return new FineUploaderResult(false, error: ex.Message);
            }
            // the anonymous object in the result below will be convert to json and set back to the browser
            return new FineUploaderResult(true, new { extraInformation = filePath });
        }
    }
}
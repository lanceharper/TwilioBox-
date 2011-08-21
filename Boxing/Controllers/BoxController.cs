using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Boxing.Models;
using RestSharp;
using Simple.Data;

namespace Boxing.Controllers
{
    public class BoxController : Controller
    {
        private readonly dynamic _db = Database.OpenConnection(@"Server=db002.appharbor.net;Database=db4010;User ID=db4010;Password=Bbt6ZF7bbhwiXDGNjQuPhRow5DeA4wseDHzePym7MQguy25bq8Rgbx2SU5avBDfL;");

        private const string BaseUrl = "https://www.box.net/api/1.0/rest";
        private const string UploadUrl = "https://upload.box.net/api/1.0/";
        private const string DownloadUrl = "https://www.box.net/api/1.0/";
        private const string AuthUrl = "https://www.box.net/api/1.0/auth/";

        private readonly string _boxApiKey = ConfigurationManager.AppSettings["BoxApiKey"];
        private readonly string _boxAuthToken = ConfigurationManager.AppSettings["BoxAuthToken"];

         public ActionResult Try()
        {
            
            var client = new RestClient
                             {
                                 BaseUrl = BaseUrl
                             };

            var request = new RestRequest();

            request.AddParameter("action", "get_ticket");
            request.AddParameter("api_key", "62kyzbstqic8fudqxc2n404mxmjhn5yf");


            var response = client.Execute<TicketResponse>(request);

            var authUrl = String.Format("{0}{1}", AuthUrl,response.Data.Ticket);

            return Redirect(authUrl);
        }

        public ActionResult Callback(string ticket, string auth_token)
        {
            _db.Log.Insert(Text: "ticket: " + ticket + ", authtoken:" + auth_token);

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult Tree()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;

            var request = new RestRequest();
            request.AddParameter("action", "get_account_tree");
            request.AddParameter("api_key", _boxApiKey);
            request.AddParameter("auth_token", _boxAuthToken);
            request.AddParameter("folder_id", 102866931);
            request.AddParameter("params[0]", "nozip");
            request.AddParameter("params[1]", "simple");

            var response = client.Execute<TreeResponse>(request);
            var tree = response.Data;
            
            return View();
        }
       
        public ActionResult Download(int id)
        {
            var client = new RestClient(DownloadUrl);
                             
            var request = new RestRequest
            {
                Resource = string.Format("{0}/{1}/{2}", "download", "tx38aasgob0lq860k4ntu8q7p8fhu2yb", id)
            };

            //TODO: Make call to get real file name.
            var downloadedBytes = client.DownloadData(request);

            return File(downloadedBytes, "text/plain", "file.txt");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var client = new RestClient(BaseUrl);

            var request = new RestRequest();

            request.AddParameter("action", "delete");
            request.AddParameter("api_key", _boxApiKey);
            request.AddParameter("auth_token", _boxAuthToken);
            request.AddParameter("target", "file");
            request.AddParameter("target_id", id);

            client.Execute<BaseResponse>(request);

            return Json(true);
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {
            
            var filesResult = new UploadFilesResult[1];
            var ms = new MemoryStream();
            files.InputStream.CopyTo(ms);

            byte[] array = ms.GetBuffer();

            var client = new RestClient
                             {
                                 BaseUrl = UploadUrl
                             };

            var request = new RestRequest
                              {
                                  Method = Method.POST,
                                  Resource = string.Format("{0}/{1}/{2}", "upload", _boxAuthToken, 102866931)
                              };
          
            request.AddParameter("share", "1");
           // request.AddParameter("message", "hey");
           // request.AddParameter("emails[0]", "lance@sagemage.com");
        
            
            request.AddFile("file", array, files.FileName);
            
            var response = client.Execute<UploadResponse>(request).Data;
            
            filesResult[0] = new UploadFilesResult
                                { 
                                    delete_type = "DELETE",
                                    delete_url = Url.Action("Delete", new { id = response.Files[0].Id }),
                                    name = files.FileName,
                                    size = files.ContentLength,
                                    thumbnail_url = "",
                                    url = Url.Action("Download", new { id = response.Files[0].Id })
                                };

            return Json(filesResult);
        }

    }
}

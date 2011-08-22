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
using Twilio;

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

        private readonly string _twilioSID = ConfigurationManager.AppSettings["TwilioSID"];
        private readonly string _twilioAuthToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string _twilioSandBoxNumber = ConfigurationManager.AppSettings["TwilioSandBoxNumber"];
        private readonly string _twilioSandBoxPIN = ConfigurationManager.AppSettings["TwilioSandBoxPIN"];

        private readonly string _myPhoneNumber = ConfigurationManager.AppSettings["MyApprovedPhoneNumber"];

        /// <summary>
        /// Use to generate a new authorization ticket.  Will require configuration updates if called.
        /// </summary>
        /// <returns></returns>
        public ActionResult NewTicket()
        {
            
            var client = new RestClient
                             {
                                 BaseUrl = BaseUrl
                             };

            var request = new RestRequest();

            request.AddParameter("action", "get_ticket");
            request.AddParameter("api_key", _boxApiKey);


            var response = client.Execute<TicketResponse>(request);

            var authUrl = String.Format("{0}{1}", AuthUrl,response.Data.Ticket);

            return Redirect(authUrl);
        }

        /// <summary>
        /// Receive ticket and auth_token back from Box.net
        /// </summary>
        /// <param name="ticket"></param>
        /// <param name="auth_token"></param>
        /// <returns></returns>
        public ActionResult Callback(string ticket, string auth_token)
        {
            _db.Log.Insert(Text: "ticket: " + ticket + ", authtoken:" + auth_token);

            return RedirectToAction("Index", "Home");
        }


        /// <summary>
        /// Upload playground.
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            return View();
        }

        /// <summary>
        /// Use to get folder Ids
        /// </summary>
        /// <returns></returns>
        public ActionResult Tree()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;

            var request = new RestRequest();
            request.AddParameter("action", "get_account_tree");
            request.AddParameter("api_key", _boxApiKey);
            request.AddParameter("auth_token", _boxAuthToken);
            request.AddParameter("folder_id", 0);
            request.AddParameter("params[0]", "nozip");
            request.AddParameter("params[1]", "simple");

            var response = client.Execute<TreeResponse>(request);
            var tree = response.Data;
            
            return View();
        }
       
        /// <summary>
        /// Use public id to call Box.net and return file specified.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Remove file from Box.net
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Upload multiple files to Box.net
        /// </summary>
        /// <param name="files"></param>
        /// <param name="tag"></param>
        /// <param name="folderId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase[] files, string tag, int folderId)
        {
            var filesResult = new List<UploadFilesResult>(files.Length);

            int i = 0;
            foreach (var file in files)
            {
                var ms = new MemoryStream();
                file.InputStream.CopyTo(ms);

                byte[] array = ms.GetBuffer();

                var client = new RestClient
                                 {
                                     BaseUrl = UploadUrl
                                 };

                var request = new RestRequest
                                  {
                                      Method = Method.POST,
                                      Resource = string.Format("{0}/{1}/{2}", "upload", _boxAuthToken, folderId)
                                  };

                request.AddParameter("share", "1");

                request.AddFile("file", array, file.FileName);

                var response = client.Execute<UploadResponse>(request).Data;

                var twilioClient = new TwilioRestClient(_twilioSID, _twilioAuthToken);
                    
                // Need absolute Url for text message.

                var relativeUrl = Url.Action("Download", new { id = response.Files[i].Id });
                var absoluteUrl = Url.ToPublicUrl(relativeUrl);
                var message = String.Format("A new file is available for {0}: {1}", tag, absoluteUrl);

                // Notify the number in the config of the uploaded number.
                twilioClient.SendSmsMessage(_twilioSandBoxNumber, _myPhoneNumber, String.Format("{0} {1}", _twilioSandBoxPIN, message));

                filesResult.Add(new UploadFilesResult
                                    {
                                        delete_type = "DELETE",
                                        delete_url = Url.Action("Delete", new {id = response.Files[i].Id}),
                                        name = file.FileName,
                                        size = file.ContentLength,
                                        thumbnail_url = "",
                                        url = Url.Action("Download", new {id = response.Files[i].Id})
                                    });
                i++;
            }
            return Json(filesResult);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BoxSync.Core;
using Boxing.Models;
using RestSharp;

namespace Boxing.Controllers
{
    public class HomeController : Controller
    {
        private const string BaseUrl = "https://www.box.net/api/1.0/rest";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Try()
        {
            //return
            //    Redirect("https://www.box.net/api/1.0/rest?action=get_ticket&api_key=62kyzbstqic8fudqxc2n404mxmjhn5yf");

            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            
            var request = new RestRequest();

            //request.RequestFormat = DataFormat.Xml;
            request.AddParameter("action", "get_ticket");
            request.AddParameter("api_key", "62kyzbstqic8fudqxc2n404mxmjhn5yf");


            var response = client.Execute<BoxResponse>(request);
            var authUrl = String.Format("https://www.box.net/api/1.0/auth/{0}", response.Data.Ticket);
            return Redirect(authUrl);
        }

        public ActionResult Callback(string ticket, string authtoken)
        {
            //new BoxManager().
            return RedirectToAction("Index");
        }

        public ActionResult Upload()
        {
            return View();
        }

        public ActionResult FullMap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase files)
        {

            var filesResult = new UploadFilesResult[1];
            
            filesResult[0] = new UploadFilesResult
                                { 
                                    delete_type = "",
                                    delete_url = "",
                                    name = files.FileName,
                                    size = files.ContentLength,
                                    thumbnail_url = "",
                                    url = ""
                                };

            return Json(filesResult);
        }

    }
}

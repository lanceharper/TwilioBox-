using System;
using System.Configuration;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Boxing.Models;
using RestSharp;
using Simple.Data;

namespace Boxing.Controllers
{
    public class HomeController : Controller
    {

        private readonly dynamic _db =
            Database.OpenConnection(
                @"Server=db002.appharbor.net;Database=db4010;User ID=db4010;Password=Bbt6ZF7bbhwiXDGNjQuPhRow5DeA4wseDHzePym7MQguy25bq8Rgbx2SU5avBDfL;");

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FullMap()
        {
            return View();
        }

    }
}

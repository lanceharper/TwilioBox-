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

        private readonly dynamic _db = Database.Open();

        public ActionResult Index()
        {
            _db.Log.Insert(Text: "New visitor");
            return View();
        }

        public ActionResult FullMap()
        {
            return View();
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Simple.Data;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace Boxing.Controllers
{
    public class SmsController : TwilioController
    {
        private readonly string _twilioSID = ConfigurationManager.AppSettings["TwilioSID"];
        private readonly string _twilioAuthToken = ConfigurationManager.AppSettings["TwilioAuthToken"];
        private readonly string _twilioSandBoxNumber = ConfigurationManager.AppSettings["TwilioSandBoxNumber"];
        private readonly string _twilioSandBoxPIN = ConfigurationManager.AppSettings["TwilioSandBoxPIN"];

        private readonly dynamic _db = Database.Open();

   
        [HttpPost]
        public TwiMLResult HandleSms(string Sid, string From, string To, string Body, string Status)
        {
            _db.Log.Insert(Text: String.Format("From: {0}, To: {1}, Body: {2}", From, To, Body));
            var response = new TwilioResponse();
            response.Say("Received");
            return TwiML(response);
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string message)
        {
            var twilioClient = new TwilioRestClient(_twilioSID, _twilioAuthToken);

            twilioClient.SendSmsMessage(_twilioSandBoxNumber, "+18168041829", String.Format("{0} {1}", _twilioSandBoxPIN, message));

            return RedirectToAction("Index");
        }
    }
}

using OfxApplication.Library;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace OfxApplication.Controllers
{
    public class OfxController : Controller
    {
        public ActionResult Index()
        {
            String filePath = Server.MapPath("extrato_itau");
            Lib lib = new Lib();
            String[] data = lib.ReadFile(filePath);
            List<STMTTRN> result = lib.LoadData(data);
            //SaveData(result);
            ViewBag.Result = lib.printData(result);
                
            return View();
        }

    }
}

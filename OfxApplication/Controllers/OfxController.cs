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
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                String filePath = Path.GetFileName(file.FileName);
                var uploadPath = Server.MapPath("~/Content/Uploads");
                filePath = Path.Combine(uploadPath, filePath);
                file.SaveAs(filePath);
                try
                {
                    Lib lib = new Lib();
                    String[] data = lib.ReadFile(filePath);
                    List<STMTTRN> result = lib.LoadData(data);

                    Database.Database db = new Database.Database();
                    db.SaveData(result);

                    ViewBag.Result = "Arquivo '" + file.FileName + "' salvo com sucesso.";
                    return View("Index", result);
                }
                catch (Exception ex)
                {
                    ViewBag.Result = "Erro inexperado. " + ex.Message;
                }
            }
            return View();
        }
    }
}

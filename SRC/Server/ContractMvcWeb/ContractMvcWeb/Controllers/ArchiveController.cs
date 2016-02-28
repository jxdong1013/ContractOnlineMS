using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContractMvcWeb.Attributes;

namespace ContractMvcWeb.Controllers
{
    [MyAuthorize]
    public class ArchiveController : Controller
    {
        //
        // GET: /Archive/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddArchive( int pageidx=0)
        {
            ContractMvcWeb.Models.archive ar = new Models.archive();
            ar.pageidx = pageidx;
            return View(ar);
        }

        public ActionResult EditArchive( int aid , int pageidx )
        {                   
            ContractMvcWeb.Models.ArchiveDBContext db = new Models.ArchiveDBContext();
            ContractMvcWeb.Models.archive ar = db.GetArchiveById(aid);
            ar.pageidx = pageidx;
            return View("addarchive" , ar);
        }

        public ActionResult DeleteArchive(int id)
        {
            ContractMvcWeb.Models.ArchiveDBContext db = new Models.ArchiveDBContext();
            db.DeleteArchive(id);
            return new RedirectResult("~/archive/archivelist");
        }

        [HttpPost]
        public ActionResult AddArchive(ContractMvcWeb.Models.archive model , int pageidx = 0)
        {
            ContractMvcWeb.Models.ArchiveDBContext db = new Models.ArchiveDBContext();
            if (model.archiveid > 0)
            {
                db.UpdateArchive(model);
            }
            else
            {
                db.AddArchive(model);
            }
            return new RedirectResult("~/archive/archivelist?pageidx="+pageidx );
        }

        public ActionResult ArchiveList( int pageidx =0 )
        {
            ViewData["pageidx"] = pageidx;
            ContractMvcWeb.Models.ArchiveDBContext db = new Models.ArchiveDBContext();
            ContractMvcWeb.Models.ArchivePage p = db.GetArchives(string.Empty, pageidx, 3);
            return View( p );
        }

        public JsonResult GetArchiveList(int pageidx = 0, int pagesize = 5)
        {
            ContractMvcWeb.Models.ArchiveDBContext db = new Models.ArchiveDBContext();
            ContractMvcWeb.Models.ArchivePage p = db.GetArchives(string.Empty, pageidx, pagesize);
            JsonResult jsonresult = new JsonResult();
            jsonresult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            jsonresult.Data = p;
            return jsonresult;
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            HttpPostedFileBase file = Request.Files["file1"];

            file.SaveAs( Request.MapPath("~/uploadfiles/")+ DateTime.Now.ToString("yyyyMMddHhmmss") );

            return new RedirectResult("~/archive/archivelist");
        }

    }
}

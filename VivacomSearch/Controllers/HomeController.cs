using API.Entities;
using API.Implementations;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VivacomSearch.Controllers
{
    public class HomeController : Controller
    {
        IFileOperations theFileOperations = null;

        public HomeController()
        {

        }

        public HomeController(IFileOperations fileOperations)
        {
            theFileOperations = fileOperations;
        }

        public ActionResult Index()
        {
            theFileOperations = new FileOperations();
            IEnumerable<Folder> directories = theFileOperations.GetLocations(Server.MapPath("~/Logs"));
            return View(directories);
        }
    }
}
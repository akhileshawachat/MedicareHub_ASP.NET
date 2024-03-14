using Microsoft.AspNetCore.Mvc;
using WebAppDemoStudent.Models;

namespace WebAppDemoStudent.Controllers
{
    public class HospitalController : Controller
    {
        public ActionResult Index()
        {
            List<Hospital> hospitals = new List<Hospital>
        {
            new Hospital { Name = "Municipal Hospital", Lat = 19.01389335636671, Lng = 73.03307942275397 },
            new Hospital { Name = "Fortis Hospital", Lat = 19.1617, Lng = 72.9419 },
            new Hospital { Name = "Lilavati Hospital", Lat = 19.0509, Lng = 72.8289 },
            new Hospital { Name = "A.S Hospital", Lat = 18.9410, Lng = 72.8274 },
            new Hospital { Name = "H.N.R.F Hospital", Lat = 18.958744, Lng = 72.819850 },
            new Hospital { Name = "Sancheti Hospital", Lat = 18.5300, Lng = 73.8530 },
            new Hospital { Name = "Ruby Hall Clinic", Lat = 18.5336, Lng = 73.8772 }
        };

            ViewBag.Hospitals = hospitals;

            return View();
        }
    }
}




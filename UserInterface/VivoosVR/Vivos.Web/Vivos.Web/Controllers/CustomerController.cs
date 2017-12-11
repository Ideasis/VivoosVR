using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace VivoosVR.Web.Controllers
{
    [Authorize]
    public class CustomerController : BaseController
    {
        // GET: Kargo
        public ActionResult Index()
        {
            return View();
        }

        
    }
}
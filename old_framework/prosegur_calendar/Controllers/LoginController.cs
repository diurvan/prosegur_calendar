using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prosegur_calendar.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login()
        {
            List<string> Retorno = new List<string>();
            try
            {
                if (Request.Form["Correo"] == null || string.IsNullOrEmpty(Request.Form["Correo"]))
                    Retorno.Add("Debe ingresar su correo");

                if (Retorno.Count == 0)
                {
                    Session["correo"] = Request.Form["Correo"].ToString();
                }
            }
            catch (Exception ex)
            {
                Retorno.Add(ex.Message);
            }
            return Json(new { Data = Retorno }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void Logout()
        {
            Session["correo"] = string.Empty;
            //return Json(new { Data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}
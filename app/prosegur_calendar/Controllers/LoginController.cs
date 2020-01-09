using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace prosegur_calendar.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            List<string> Retorno = new List<string>();
            try
            {
                if (string.IsNullOrEmpty(Request.Form["Correo"]))
                    Retorno.Add("Debe ingresar su correo");

                if (Retorno.Count == 0)
                {
                    HttpContext.Session.SetString("correo", Request.Form["Correo"].ToString());
                }
            }
            catch (Exception ex)
            {
                Retorno.Add(ex.Message);
            }
            return Json(new { Data = Retorno });
        }

        [HttpPost]
        public void Logout()
        {
            HttpContext.Session.SetString("correo", string.Empty);
            //return Json(new { Data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}
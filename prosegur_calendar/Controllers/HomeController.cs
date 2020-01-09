using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using e_prosegur;
using n_prosegu;

namespace prosegur_calendar.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new EventModel());
        }

        public JsonResult GetEvents(DateTime start, DateTime end, string correotest="")
        {
            string correo = string.IsNullOrEmpty(correotest)?(string)Session["correo"]: correotest;
            NEvento nEvento = new NEvento();
            var events = new List<EventModel>();
            events = nEvento.listaEventos(new EventModel() { email = correo });
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-11);
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GrabarEvento()
        {
            List<string> Retorno = new List<string>();
            int IdEvento = 0;
            try
            {
                if (Request.Form["Fecha"] == null || string.IsNullOrEmpty(Request.Form["Fecha"]))
                    Retorno.Add("Debe seleccionar una Fecha");
                if (Request.Form["Nombre"] == null || string.IsNullOrEmpty(Request.Form["Nombre"]))
                    Retorno.Add("Debe especificar un Nombre");
                if (Request.Form["Evaluacion"] == null || string.IsNullOrEmpty(Request.Form["Evaluacion"]))
                    Retorno.Add("Debe especificar una Evaluacion");

                if (Retorno.Count == 0)
                {

                    string Correo = (string)(Session["correo"]);
                    NEvento bCarga = new NEvento();
                    int iRetorno = bCarga.Create(new EventModel()
                    {
                        id = IdEvento,
                        start = Request.Form["Fecha"].ToString(),
                        email = Correo,
                        nombre = Request.Form["Nombre"].ToString(),
                        evaluacion = int.Parse(Request.Form["Evaluacion"].ToString())
                    });
                    if(iRetorno == -99)
                        Retorno.Add("Ya existe una caliicación para el día seleccionado");
                }
            }
            catch (Exception ex)
            {
                Retorno.Add(ex.Message);
            }
            return Json(new { Data = Retorno }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
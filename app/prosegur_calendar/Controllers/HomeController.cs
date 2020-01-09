using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using e_prosegur;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n_prosegur;
using prosegur_calendar.Models;
using Newtonsoft.Json;

namespace prosegur_calendar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        public JsonResult GetEvents(DateTime start, DateTime end, string correotest = "")
        {
            string correo = string.IsNullOrEmpty(correotest) ? (string)HttpContext.Session.GetString("correo") : correotest;
            NEvento nEvento = new NEvento();
            var events = new List<EventModel>();
            events = nEvento.listaEventos(new EventModel() { email = correo });
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-11);
            HttpContext.Session.SetString("correo", correo);
            return Json(events.ToArray());
        }

        [HttpPost]
        public ActionResult GrabarEvento()
        {
            List<string> Retorno = new List<string>();
            int IdEvento = 0;
            try
            {
                if (string.IsNullOrEmpty(Request.Form["Fecha"]))
                    Retorno.Add("Debe seleccionar una Fecha");
                if (string.IsNullOrEmpty(Request.Form["Nombre"]))
                    Retorno.Add("Debe especificar un Nombre");
                if (string.IsNullOrEmpty(Request.Form["Evaluacion"]))
                    Retorno.Add("Debe especificar una Evaluacion");

                if (Retorno.Count == 0)
                {

                    string Correo = (string)HttpContext.Session.GetString("correo");
                    NEvento bCarga = new NEvento();
                    int iRetorno = bCarga.Create(new EventModel()
                    {
                        id = IdEvento,
                        start = Request.Form["Fecha"].ToString(),
                        email = Correo,
                        nombre = Request.Form["Nombre"].ToString(),
                        evaluacion = int.Parse(Request.Form["Evaluacion"].ToString())
                    });
                    if (iRetorno == -99)
                        Retorno.Add("Ya existe una caliicación para el día seleccionado");
                }
            }
            catch (Exception ex)
            {
                Retorno.Add(ex.Message);
            }
            return Json(new { Data = Retorno });
        }

    }
}

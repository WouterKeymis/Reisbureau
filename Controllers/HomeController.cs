using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Reisbureau.Models;
using Reisbureau.Services;

namespace Reisbureau.Controllers
{
    public class HomeController : Controller
    {
        /* Dit stukje code mag je negeren, dit dient enkel
           om me eraan te herinneren een vraag over services te stellen bij de feedback
        
        private KlantenService _klantenService;

        public HomeController(KlantenService klantenService)
        {
            this._klantenService = klantenService;
        }
        */

        public IActionResult Index()
        {
            //Als er geen cookie bestaat met de klantgegevens -> voeg een nieuwe klant toe
            if (Request.Cookies["klantenCookie"] == null)
            {
                return RedirectToAction("KlantToevoegen");
            }
            else
            {
                //haal voornaam klant uit cookie
                string voornaam = Request.Cookies["klantenCookie"];
                ViewBag.Voornaam = voornaam;
                //haal lijst brochures uit sessionvariabele (indien aanwezig)
                var aangevraagdeBrochures = HttpContext.Session.GetString("brochures");
                if (!string.IsNullOrEmpty(aangevraagdeBrochures))
                    ViewBag.Brochures = JsonConvert.DeserializeObject<List<String>>(aangevraagdeBrochures);
                return View(new ReisBestemming());
            }
        }

        public IActionResult KlantToevoegen()
        {
            return View(new Klant());
        }

        [HttpPost]
        public IActionResult KlantToevoegen(Klant klant)
        {
            //Als alles goed is ingevuld
            if (this.ModelState.IsValid)
            {
                /* Dit stukje code mag je even negeren, ik heb dit hier gezet zodat ik tijdens
                   de feedback hierover een vraag kan stellen 
                   _klantenService.Add(klant);
                */

                //Voornaam van de klant in de klantenCookie opslaan
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(365);
                Response.Cookies.Append("klantenCookie", klant.Voornaam, option);
                return RedirectToAction("Index");
            }
            else
                return View(klant);
        }
        [HttpPost]
        public IActionResult BestemmingToevoegen(ReisBestemming bestemming) 
        {
            List<String> brochures;
            var aangevraagdeBrochures = HttpContext.Session.GetString("brochures");
            //als geen aangevraagde brochure -> Nieuwe lijst, anders opgevraagde string converteren naar List
            if (string.IsNullOrEmpty(aangevraagdeBrochures))
                brochures = new List<String>();
            else
                brochures = JsonConvert.DeserializeObject<List<String>>(aangevraagdeBrochures);

            brochures.Add(bestemming.Naam);
            var JsonList = JsonConvert.SerializeObject(brochures);
            HttpContext.Session.SetString("brochures", JsonList);
            return RedirectToAction("Index");
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
    }
}

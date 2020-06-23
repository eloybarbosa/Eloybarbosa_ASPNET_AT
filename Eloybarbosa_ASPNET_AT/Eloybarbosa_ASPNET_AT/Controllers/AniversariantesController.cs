using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eloybarbosa_ASPNET_AT.Models;
using Eloybarbosa_ASPNET_AT.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eloybarbosa_ASPNET_AT.Controllers
{
    public class AniversariantesController : Controller
    {

        private AniversarianteRepository AniversarianteRepository { get; set; }

        public AniversariantesController(AniversarianteRepository aniversarianteRepository)
        {
            this.AniversarianteRepository = aniversarianteRepository;
        }
        // GET: Aniversariantes
        public ActionResult Index()
        {
            var model = this.AniversarianteRepository.GetAll();
            
            return View(model);
        }

        // GET: Aniversariantes/Details/5
        public ActionResult Details(int id)
        {
            var model = this.AniversarianteRepository.GetById(id);
            return View(model);
        }

        // GET: Aniversariantes/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Search([FromQuery] string query)
        {

            var model = this.AniversarianteRepository.Search(query);

            return View("Index", model);

        }

        // POST: Aniversariantes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Aniversariante aniversariante)
        {
            try
            {
                this.AniversarianteRepository.Save(aniversariante);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: Aniversariantes/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.AniversarianteRepository.GetById(id);
            return View(model);
        }

        // POST: Aniversariantes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Aniversariante model)
        {
            try
            {
                model.Id = id;
                this.AniversarianteRepository.Update(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Aniversariantes/Delete/5
        public ActionResult Delete(int id)
        {
            var model = this.AniversarianteRepository.GetById(id);

            return View(model);
        }

        // POST: Aniversariantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Aniversariante model)
        {
            try
            {
                model.Id = id;
                this.AniversarianteRepository.Delete(model);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AniversarianteDia()
        {
            DateTime hj = DateTime.Today;
            var model = this.AniversarianteRepository.GetAll();
            var aniversariantesdodia = new List<Aniversariante>();


            foreach (var aniversariante in model)
            {
                if (aniversariante.Nascimento.Day == hj.Day && aniversariante.Nascimento.Month == hj.Month)
                {
                    aniversariantesdodia.Add(aniversariante);
                }
            }

            return View(aniversariantesdodia);

        }
    }
}
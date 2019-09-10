using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASDAQ.Repos;
using NASDAQ.Models;
using NASDAQ.ViewModels;

namespace NASDAQ.Controllers
{
    public class PriceController : Controller
    {
        PricesRepository pricesRepository = new PricesRepository();
        SecuritiesRepository securitiesRepository = new SecuritiesRepository();

        // GET: Prices
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(pricesRepository.getPrices());
        }


        // GET: Prices/Create
        public ActionResult Create()
        {
            PriceEditViewModel priceEditViewModel = new PriceEditViewModel();
            PopulateSelections(priceEditViewModel);

            return View(priceEditViewModel);
        }

        // POST: Prices/Create
        [HttpPost]
        public ActionResult Create(PriceEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    pricesRepository.addPrice(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Prices/Edit/5
        public ActionResult Edit(int id)
        {
            PriceEditViewModel priceEditViewModel = pricesRepository.getPrice(id);
            PopulateSelections(priceEditViewModel);

            return View(priceEditViewModel);
        }

        // POST: Prices/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, PriceEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    pricesRepository.updatePrice(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Prices/Delete/5
        public ActionResult Delete(int id)
        {
            PriceEditViewModel priceEditViewModel = pricesRepository.getPrice(id);
            return View(priceEditViewModel);
        }

        // POST: Prices/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                PriceEditViewModel priceEditViewModel = pricesRepository.getPrice(id);
                bool used = false;

                /*if(pricesRepository.getPriceCount(id)>0)
                {
                    used = true;
                    ViewBag.used = "Negalima pašalinti akcijos, kuri yra naudojama kitose lentelėse.";

                }*/

                if (!used)
                    pricesRepository.deletePrice(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(PriceEditViewModel priceEditViewModel)
        {
            var securities = securitiesRepository.getSecurities();
            List<SelectListItem> selectListSecurities = new List<SelectListItem>();

            foreach (var item in securities)
                selectListSecurities.Add(new SelectListItem() { Value = Convert.ToString(item.ticker), Text = item.ticker });

            priceEditViewModel.SecuritiesList = selectListSecurities;
        }
    }
}

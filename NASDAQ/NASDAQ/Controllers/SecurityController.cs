using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASDAQ.Repos;
using NASDAQ.ViewModels;
using NASDAQ.Models;

namespace NASDAQ.Controllers
{
    public class SecurityController : Controller
    {
        SecuritiesRepository securitiesRepository = new SecuritiesRepository();
        CompaniesRepository companiesRepository = new CompaniesRepository();

        // GET: Security
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(securitiesRepository.getSecurities());
        }

        // GET: Security/Create
        public ActionResult Create()
        {
            SecurityEditViewModel securityEditViewModel = new SecurityEditViewModel();

            PopulateSelections(securityEditViewModel);

            return View(securityEditViewModel);
        }

        // POST: Security/Create
        [HttpPost]
        public ActionResult Create(SecurityEditViewModel collection)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    securitiesRepository.addSecurity(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Security/Edit/5
        public ActionResult Edit(string ticker)
        {
            SecurityEditViewModel securityEditViewModel = securitiesRepository.getSecurity(ticker);
            PopulateSelections(securityEditViewModel);

            return View(securityEditViewModel);
        }

        // POST: Security/Edit/5
        [HttpPost]
        public ActionResult Edit(string ticker, SecurityEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    securitiesRepository.updateSecurity(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: Security/Delete/5
        public ActionResult Delete(string ticker)
        {
            SecurityEditViewModel securityEditViewModel = securitiesRepository.getSecurity(ticker);
            return View(securityEditViewModel);
        }

        // POST: Security/Delete/5
        [HttpPost]
        public ActionResult Delete(string ticker, FormCollection collection)
        {
            try
            {
                SecurityEditViewModel securityEditViewModel = securitiesRepository.getSecurity(ticker);
                bool used = false;

                if(securitiesRepository.getSecurityCount(ticker)>0)
                {
                    used = true;
                    ViewBag.used = "Negalima pašalinti akcijos, kuri yra naudojama kitose lentelėse.";
                    return View(securitiesRepository.getSecurity(ticker));
                }

                if (!used)
                {
                    securitiesRepository.deleteSecurity(ticker);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(SecurityEditViewModel securityEditViewModel)
        {
            var companies = companiesRepository.getCompanies();
            List<SelectListItem> selectListCompanies = new List<SelectListItem>();

            foreach (var item in companies)
                selectListCompanies.Add(new SelectListItem() { Value = Convert.ToString(item.code), Text = item.name });

            securityEditViewModel.CompaniesList = selectListCompanies;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASDAQ.Repos;
// using NASDAQ.ViewModels;
using NASDAQ.Models;


namespace NASDAQ.Controllers
{
    public class CompanyController : Controller
    {
        CompaniesRepository companiesRepository = new CompaniesRepository();


        // GET: Company
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(companiesRepository.getCompanies());
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            Company company = new Company();
            return View(company);
        }

        // POST: Company/Create
        [HttpPost]
        public ActionResult Create(Company collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    companiesRepository.addCompany(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int code)
        {
            return View(companiesRepository.getCompany(code));
        }

        // POST: Company/Edit/5
        [HttpPost]
        public ActionResult Edit(int code, Company collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    companiesRepository.updateCompany(collection);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int code)
        {
            Company company = companiesRepository.getCompany(code);
            return View(company);
        }

        // POST: Company/Delete/5
        [HttpPost]
        public ActionResult Delete(int code, FormCollection collection)
        {
            try
            {
                // Company company = companiesRepository.getCompany(code);
                bool used = false;

                if (companiesRepository.getCompanyCount(code) > 0)
                {
                    used = true;
                    ViewBag.used = "Negalima pašalinti kompanijos, kuri yra naudojama kitose lentelėse.";
                    return View(companiesRepository.getCompany(code));
                }

                if (!used)
                {
                    companiesRepository.deleteCompany(code);
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

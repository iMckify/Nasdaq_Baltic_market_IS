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
    public class Financial_ReportController : Controller
    {
        FinancialReportsRepository financialReportsRepository = new FinancialReportsRepository();
        CompaniesRepository companiesRepository = new CompaniesRepository();

        // GET: FinancialReport
        public ActionResult Index()
        {
            ModelState.Clear();
            return View(financialReportsRepository.getReports());
        }

        // GET: FinancialReport/Create
        public ActionResult Create()
        {
            FinancialReportEditViewModel financialReportEditViewModel = new FinancialReportEditViewModel();

            PopulateSelections(financialReportEditViewModel);

            return View(financialReportEditViewModel);
        }

        // POST: FinancialReport/Create
        [HttpPost]
        public ActionResult Create(FinancialReportEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    financialReportsRepository.addReport(collection);

                return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: FinancialReport/Edit/5
        public ActionResult Edit(string ticker)
        {
            var financialReportEditViewModel = financialReportsRepository.getReport(ticker);
            PopulateSelections(financialReportEditViewModel);

            return View(financialReportEditViewModel);
        }

        // POST: FinancialReport/Edit/5
        [HttpPost]
        public ActionResult Edit(string ticker, FinancialReportEditViewModel collection)
        {
            try
            {
                if (ModelState.IsValid)
                    financialReportsRepository.updateReport(collection);

                    return RedirectToAction("Index");
            }
            catch
            {
                PopulateSelections(collection);
                return View(collection);
            }
        }

        // GET: FinancialReport/Delete/5
        public ActionResult Delete(string ticker)
        {
            var financialReportEditViewModel = financialReportsRepository.getReport(ticker);

            return View(financialReportEditViewModel);
        }

        // POST: FinancialReport/Delete/5
        [HttpPost]
        public ActionResult Delete(string ticker, FormCollection collection)
        {
            try
            {
                var financialReportEditViewModel = financialReportsRepository.getReport(ticker);
                bool used = false;

                if(financialReportsRepository.getReportCount(ticker)>0)
                {
                    used = true;
                    ViewBag.used = "Negalima pašalinti finansinės ataskaitos, kuri yra naudojama kitose lentelėse.";
                    return View(financialReportsRepository.getReport(ticker));
                }

                if (!used)
                    financialReportsRepository.deleteReport(ticker);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public void PopulateSelections(FinancialReportEditViewModel financialReportEditViewModel)
        {
            var companies = companiesRepository.getCompanies();
            List<SelectListItem> selectListCompanies = new List<SelectListItem>();

            foreach (var item in companies)
                selectListCompanies.Add(new SelectListItem() { Value = Convert.ToString(item.code), Text = item.name });

            financialReportEditViewModel.CompaniesList = selectListCompanies;
        }
    }
}

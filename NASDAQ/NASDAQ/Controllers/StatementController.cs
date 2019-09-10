using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NASDAQ.ViewModels;
using NASDAQ.Repos;

namespace NASDAQ.Controllers
{
    public class StatementController : Controller
    {
        StatementRepository statementRepository = new StatementRepository();

        // GET: Statement
        public ActionResult Index(DateTime? from, DateTime? to)
        {
            StatementViewModel statementViewModel = statementRepository.getPricesTotal(from, to);
            statementViewModel.prices = statementRepository.getPricesConsolidated(from, to);

            statementViewModel.from = from == null ? null : from;
            statementViewModel.to = to == null ? null : to;

            return View(statementViewModel);
        }
    }
}

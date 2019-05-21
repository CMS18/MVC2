using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AjaxLoadMore.Models;
using Microsoft.AspNetCore.Mvc;

namespace AjaxLoadMore.Controllers
{
    public class LongListController : Controller
    {
        public IActionResult Index(int page = 1)
        {
            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            const int pageSize = 10;


            var repo = new TransactionRepository();

            var totalNumber = 134; // Kommer från databasen;
            var transactions = repo.GetTransactions(pageSize, (page - 1) * pageSize);


            var model = new LongListViewModel
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfItems = totalNumber,
                CanShowMore = page * pageSize < totalNumber,
                Transactions = transactions
            };


            if (isAjax)
            {
                return PartialView("_TransactionRows", model);
            }
            else
            {
                return View(model);
            };
        }
    }
}
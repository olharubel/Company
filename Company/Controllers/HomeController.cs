using Bogus;
using Company.Data;
using Company.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly EmployeesCatalogueDbContext _employeesCatalogueDbContext;

        public HomeController(EmployeesCatalogueDbContext employeesCatalogueDbContext)
        {
            _employeesCatalogueDbContext = employeesCatalogueDbContext;
        }

        public IActionResult Index()
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
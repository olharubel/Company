using Bogus;
using Company.Data;
using Company.Models;
using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Company.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeesCatalogueDbContext _employeesCatalogueDbContext;

        public EmployeesController(EmployeesCatalogueDbContext catalogueDbContext)
        {
            this._employeesCatalogueDbContext = catalogueDbContext;
            SeedData();

        }

       private bool SeedData()
        {
            if (!_employeesCatalogueDbContext.Employees.Any())
            {
                int i = 1;
                var employee = new Faker<Employee>()
                     .RuleFor(e => e.EmployeeId, f => i++)
                  .RuleFor(e => e.Name, e => e.Person.FirstName)
                   .RuleFor(e => e.Surname, e => e.Person.LastName)
                    .RuleFor(e => e.JobTitle, e => e.Name.JobTitle())
                    .RuleFor(e => e.EmploymentDate, e => e.Date.Past())
                     .RuleFor(e => e.Salary, e => e.Random.Number(4000, 300000));


                var employees = employee.Generate(5000);
                _employeesCatalogueDbContext.BulkInsert(employees);
                _employeesCatalogueDbContext.SaveChanges();
                return true;
            }
            return false;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var employees = await _employeesCatalogueDbContext.Employees.ToListAsync();
            int level = 3;
            var result = CreateEmployeeHierarchy(employees, level);

            IList<JsTreeModel> nodes = new List<JsTreeModel>();


            foreach (var emp in result)
            {

                nodes.Add(new JsTreeModel
                {
                    id = emp.EmployeeId.ToString(),
                    parent = emp.HeadId == null ? "#" : emp.HeadId.ToString(),
                    text = emp.Name + " " + emp.Surname + " " + emp.JobTitle
                }
                    );
            }


            ViewBag.Json = JsonSerializer.Serialize(nodes);


            return View();

        }

        private static List<Employee> CreateEmployeeHierarchy(List<Employee> employees, int levels)
        {
            Random rnd = new();

            int count = employees.Count;
            int parentId = 0;

            List<Employee> result = new();

            for (int i = 0; i < count; ++i)
            {
                int childIndex = rnd.Next(employees.Count);

                int childId;
                if (i < levels)
                {
                    childId = employees[childIndex].EmployeeId;
                }
                else
                {
                    childId = result.ElementAt(rnd.Next(result.Count)).EmployeeId;
                }

                if (i != 0)
                {
                    employees[childIndex].HeadId = parentId;
                }

                parentId = childId;

                result.Add(employees[childIndex]);

                employees.RemoveAt(childIndex);

            }

            return result;
        }

    }
}

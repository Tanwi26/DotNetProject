using DotNetProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetProject.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Customer Customer { get; set; }

        public CustomersController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Customer = new Customer();
            if (id == null)
            {
                //create
                return View(Customer);
            }
            //update
            Customer = _db.Customers.FirstOrDefault(u => u.Id == id);
            if (Customer == null)
            {
                return NotFound();
            }
            return View(Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if(ModelState.IsValid)
            {
                if (Customer.Id == 0)
                {
                    _db.Customers.Add(Customer);
                }
                else
                {
                    _db.Customers.Update(Customer);
                }
                _db.SaveChanges();
                return RedirectToAction("Home");
            }
            return View(Customer);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
          JsonResult json =  Json(new { data = await _db.Customers.ToListAsync() });
          return json;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var customerFromDb = await _db.Customers.FirstOrDefaultAsync(u => u.Id == id);
            if (customerFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Customers.Remove(customerFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}

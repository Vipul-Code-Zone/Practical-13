using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticalThirteenTest2.DatabaseContext;
using PracticalThirteenTest2.Models;
using PracticalThirteenTest2.ViewModel;
using System.Diagnostics.CodeAnalysis;

namespace PracticalThirteenTest2.Controllers
{
    public class EmployeesController : Controller
    {
        readonly EmployeeDbContext db = new EmployeeDbContext();

        // GET: Employees
        public async Task<ActionResult> Index()
        {
            var employees = db.Employees.Include(e => e.Designations);
            return View(await employees.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,FirstName,MiddleName,LastName,DOB,PhoneNumber,Address,Salary,DesignationId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
				var test = Convert.ToDateTime(employee.DOB) - DateTime.Now;
				var test1 = new DateTime(1900, 01, 01) - Convert.ToDateTime(employee.DOB);
				if (test.Days > 1 || test1.Days > 1)
				{
					ViewBag.Message = "Date Should Be between 1900-01-01 to Today's Date";
					ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", employee.DesignationId);
					return View(employee);
				}
				db.Employees.Add(employee);
                await db.SaveChangesAsync();
			    TempData["AddSuccess"] = "Employee Added Successfully!";
                return RedirectToAction("Index");
            }

            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", employee.DesignationId);
			return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", employee.DesignationId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,FirstName,MiddleName,LastName,DOB,PhoneNumber,Address,Salary,DesignationId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
				var test = Convert.ToDateTime(employee.DOB) - DateTime.Now;
				var test1 = new DateTime(1900, 01, 01) - Convert.ToDateTime(employee.DOB);
				if (test.Days > 1 || test1.Days > 1)
				{
					ViewBag.Message = "Date Should Be between 1900-01-01 to Today's Date";
					return View();
				}
				db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.DesignationId = new SelectList(db.Designations, "Id", "Designations", employee.DesignationId);
			TempData["UpdateSuccess"] = "Employee Updated Successfully!";
			return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
			TempData["deletedSuccess"] = "Employee Deleted Successfully!";
			return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
			List<EmployeeDesignation> employees = await db.Employees.Join(db.Designations, emp => emp.DesignationId, desg => desg.Id, (emp, desg) => new {emp, desg}).Select((e) => new EmployeeDesignation() { employees = e.emp,designations = e.desg }).ToListAsync();
            
			return View(employees);
        }

        public async Task<ActionResult> GetEmployeeCount()
        {
			List<EmployeeCountByDesg> empBydesgCount = await db.Employees.Join(db.Designations, emp => emp.DesignationId, desg => desg.Id, (emp, desg) => new { emp, desg }).GroupBy(x=> x.desg.Designations).Select(s=> new EmployeeCountByDesg() { Designation = s.Key, EmpCount = s.Count()}).ToListAsync();
            return View(empBydesgCount);
		}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

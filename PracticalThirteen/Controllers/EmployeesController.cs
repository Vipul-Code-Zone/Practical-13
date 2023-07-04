using PracticalThirteen.DatabaseContext;
using PracticalThirteen.Models;
using System;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PracticalThirteen.Controllers
{
	public class EmployeesController : Controller
	{
		readonly EmployeeDbContext db = new EmployeeDbContext();

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			return View(await db.Employees.ToListAsync());
		}

		[HttpGet]
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

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Name,DOB,Age")] Employee employee)
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
				db.Employees.Add(employee);
				await db.SaveChangesAsync();
				TempData["AddSuccess"] = "Employee Added Successfully!";
				return RedirectToAction("Index");
			}

			return View(employee);
		}

		[HttpGet]
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
			return View(employee);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Name,DOB,Age")] Employee employee)
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
				TempData["UpdateSuccess"] = "Employee Updated Successfully!";
				return RedirectToAction("Index");
			}
			return View(employee);
		}


		public async Task<bool> Delete(int? id)
		{
			if (id == null)
			{
				return false;
			}
			Employee employee = await db.Employees.FindAsync(id);
			if (employee != null)
			{
				db.Employees.Remove(employee);
				await db.SaveChangesAsync();
				return true;
			}
			return false;
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

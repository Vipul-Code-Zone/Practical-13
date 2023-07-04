using PracticalThirteenTest2.DatabaseContext;
using PracticalThirteenTest2.Models;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PracticalThirteenTest2.Controllers
{
	public class DesignationsController : Controller
	{
		private EmployeeDbContext db = new EmployeeDbContext();

		[HttpGet]
		public async Task<ActionResult> Index()
		{
			return View(await db.Designations.ToListAsync());
		}

		[HttpGet]
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Designation designation = await db.Designations.FindAsync(id);
			if (designation == null)
			{
				return HttpNotFound();
			}
			return View(designation);
		}

		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "Id,Designations")] Designation designation)
		{
			if (ModelState.IsValid)
			{
				db.Designations.Add(designation);
				await db.SaveChangesAsync();
				TempData["AddSuccess"] = "Designation Added Successfully!";
				return RedirectToAction("Index");
			}

			return View(designation);
		}

		[HttpGet]
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Designation designation = await db.Designations.FindAsync(id);
			if (designation == null)
			{
				return HttpNotFound();
			}
			return View(designation);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "Id,Designations")] Designation designation)
		{
			if (ModelState.IsValid)
			{
				db.Entry(designation).State = EntityState.Modified;
				await db.SaveChangesAsync();
				TempData["UpdateSuccess"] = "Designation Updated Successfully!";
				return RedirectToAction("Index");
			}
			return View(designation);
		}

		[HttpGet]
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Designation designation = await db.Designations.FindAsync(id);
			if (designation == null)
			{
				return HttpNotFound();
			}
			return View(designation);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			Designation designation = await db.Designations.FindAsync(id);
			db.Designations.Remove(designation);
			await db.SaveChangesAsync();
			TempData["deletedSuccess"] = "Designation Deleted Successfully!";
			return RedirectToAction("Index");
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

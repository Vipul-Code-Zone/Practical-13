using PracticalThirteenTest2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticalThirteenTest2.DatabaseContext
{
	public class EmployeeDbContext : DbContext
	{
		public EmployeeDbContext() : base("EmpDbEntities")
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EmployeeDbContext>());
		}

		public DbSet<Employee> Employees { get; set; }
		public DbSet<Designation> Designations { get; set; }
	}
}
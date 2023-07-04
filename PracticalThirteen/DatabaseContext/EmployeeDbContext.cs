using PracticalThirteen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PracticalThirteen.DatabaseContext
{
	public class EmployeeDbContext : DbContext
	{
		public EmployeeDbContext() : base("EmployeeDbEntities")
		{
			Database.SetInitializer(new DropCreateDatabaseIfModelChanges<EmployeeDbContext>());
		}
		public DbSet<Employee> Employees { get; set; }
	}
}
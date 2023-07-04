using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalThirteenTest2.Models
{
	public class Designation
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }


		[Required]
		[StringLength(50)]
		public string Designations { get; set; }

		public ICollection<Employee> Employees { get; set; }
	}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PracticalThirteenTest2.Models
{
	public class Employee
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[StringLength(50)]
		[Display(Name = "Middle Name")]
		public string MiddleName { get; set; }

		[Required]
		[StringLength(50)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		[Required]
		[Column(TypeName = "Date")]
		[Display(Name = "Joining Date")]
		[DataType(DataType.Date, ErrorMessage = "Only Date is Required"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime DOB { get; set; }

		[Required]
		[StringLength(10), MaxLength(10), MinLength(10)]
		[Phone]
		[DataType(DataType.PhoneNumber, ErrorMessage = "Please Enter Valid Phone Number")]
		public string PhoneNumber { get; set; }

		[StringLength(50)]
		public string Address { get; set; }

		[Required]
		public decimal Salary { get; set; }

		[ForeignKey("Designations")]
		public int DesignationId { get; set; }

		public Designation Designations { get; set; }
	}
}
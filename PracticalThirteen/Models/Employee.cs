using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticalThirteen.Models
{
	public class Employee
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		[MaxLength(50, ErrorMessage = "Name should be max 50 charcter only!")]
		public string Name { get; set; }

		[Required]
		[Column(TypeName = "Date")]
		[Display(Name = "JoiningDate")]
		[DataType(DataType.Date, ErrorMessage = "Date is Required"), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
		public DateTime DOB { get; set; }

		public int? Age { get; set; }
	}
}
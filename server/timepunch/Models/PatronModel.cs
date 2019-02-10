using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace timepunch.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeeId { get; set; }
        [ForeignKey("CompanyId")]
        public CompanyModel EmployeeCompany { get; set; }
        public string EmployeePosition { get; set; }
        public double EmployeeSalary { get; set; }
        [ForeignKey("ShiftId")]
        public ShiftModel Shift { get; set; }

    }

    public class CompanyModel
    {
        [Key]
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyDescription { get; set; }
        public string CompanyLocation { get; set; }
        public string CompanyContact { get; set; }
    }
}
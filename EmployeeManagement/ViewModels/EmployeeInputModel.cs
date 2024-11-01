using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeInputModel
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50)]
        public string EmployeeName { get; set; } = default!;
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime? JoiningDate { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal? Salary { get; set; }
        
        public IFormFile? Picture { get; set; } = default!;
        public bool IsActive { get; set; }
        public virtual List<Attendance> Attendances { get; set; } = [];
    }
}

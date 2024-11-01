using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public enum Gender { Male=1, Female}
    public class Employee
    {
        public int EmployeeId { get; set; }
        [Required, StringLength(50)]
        public string EmployeeName { get; set; } = default!;
        [Required, EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        [Required, Column(TypeName ="date")]
        public DateTime? JoiningDate { get; set; }
        [Required, Column(TypeName ="money")]
        public decimal? Salary { get; set; }
        [Required, StringLength(50)]
        public string Picture { get; set; } = default!;
        public bool IsActive {  get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; } = [];


    }
    public class Attendance
    {
        public int AttendanceId { get; set; }
        [Required, Column(TypeName ="date")]
        public DateTime? AttendanceDate { get; set; }
        [Required, Column(TypeName = "time")]
        public TimeSpan? InTime { get; set; }
        [Required, Column(TypeName ="time")]
        public TimeSpan? OutTime { get; set; }
        [Required, ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee? Employees { get; set; }
    }
    public class EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : DbContext(options)
    {
       // public EmployeeDbContext(DbContextOptions<EmployeeDbContext>options) { }
       public DbSet<Employee> Employees { get; set;}
       public DbSet<Attendance> Attendances { get; set; }
        
    }
}

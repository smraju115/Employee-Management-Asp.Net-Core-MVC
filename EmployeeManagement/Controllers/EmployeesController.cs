using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeDbContext db;
        private readonly IWebHostEnvironment env;
        public EmployeesController(EmployeeDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }
        public IActionResult Index()
        {
            //Eager Loading
            var data = db.Employees.Include(x=>x.Attendances).ToList();
            return View(data);
        }
        public IActionResult Aggregates()
        {
            return View(db.Employees.Include(x=>x.Attendances).ToList());
        }
        public IActionResult Grouping()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Grouping(string groupby)
        {
            if (groupby == "gender")
            {
                var data = db.Employees.GroupBy(x => x.Gender).ToList().Select(g => new GroupData<Employee> { Key = g.Key.ToString(), Persons = g }).ToList();
                return View("GroupingResult", data);
            }
            else if (groupby == "year-month")
            {
                var data = db.Employees.ToList().GroupBy(x => new { x.JoiningDate.Value.Year, x.JoiningDate.Value.Month }).Select(g => new GroupData<Employee> { Key = $"{g.Key.Month}/{g.Key.Year}", Persons = g }).ToList();
                return View("GroupingResult", data);
            }
            return NoContent();
        }
        public IActionResult Create() 
        {
            var model = new EmployeeInputModel();
            model.Attendances.Add(new Attendance { });
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(EmployeeInputModel model, string operation)
        {
            if (operation == "add")
            {
                model.Attendances.Add(new Attendance { });
                foreach(var item in ModelState.Values)
                {
                    item.Errors.Clear();
                    item.RawValue = null;
                }
            }
            if (operation.StartsWith("del"))
            {
                int index =int.Parse(operation.Substring(operation.IndexOf("_") + 1));
                model.Attendances.RemoveAt(index);
                foreach(var item in ModelState.Values)
                {
                    item.Errors.Clear();
                    item.RawValue = null;
                }
            }
            if (operation == "insert")
            {
                if(ModelState.IsValid)
                {
                    var e = new Employee
                    {
                        EmployeeName = model.EmployeeName,
                        Gender = model.Gender,
                        JoiningDate = model.JoiningDate,
                        Salary = model.Salary,
                        IsActive = model.IsActive,
                    };
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName())+ ext;
                    string savePath = Path.Combine(env.WebRootPath, "Pictures", f);
                    FileStream fs = new FileStream(savePath, FileMode.Create);
                    model.Picture.CopyTo(fs);
                    fs.Close();
                    e.Picture = f;
                    db.Employees.Add(e);
                    db.SaveChanges();
                    foreach (var a in model.Attendances)
                    {
                        db.Database.ExecuteSqlInterpolated($"EXECUTE InsertAttendance {a.AttendanceDate}, {a.InTime}, {a.OutTime}, {e.EmployeeId}");
                    }
                    return RedirectToAction("Index");
                }
            }


            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var e =db.Employees.FirstOrDefault(x=>x.EmployeeId == id);
            if (e == null) return NotFound();
            //Explicit Loading
            db.Entry(e).Collection(x=>x.Attendances).Load();
            var data = new EmployeeEditModel
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                Gender = e.Gender,
                JoiningDate = e.JoiningDate,
                Salary = e.Salary,
                IsActive = e.IsActive,

            };
            data.Attendances = e.Attendances.ToList();
            ViewBag.CurrentPic = e.Picture;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditModel model, string operation)
        {
            var e = db.Employees.FirstOrDefault(x=>x.EmployeeId == model.EmployeeId);
            if (e == null) return NotFound();
            if (operation == "add")
            {
                model.Attendances.Add(new Attendance { });
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                    item.RawValue = null;
                }
            }
            if (operation.StartsWith("del"))
            {
                int index = int.Parse(operation.Substring(operation.IndexOf("_") + 1));
                model.Attendances.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                    item.RawValue = null;
                }
            }
            if (operation == "update")
            {
                if (ModelState.IsValid)
                {

                    e.EmployeeName = model.EmployeeName;
                    e.Gender = model.Gender;
                    e.JoiningDate = model.JoiningDate;
                    e.Salary = model.Salary;
                    e.IsActive = model.IsActive;

                    if(model.Picture != null)
                    {
                        string ext = Path.GetExtension(model.Picture.FileName);
                        string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Path.Combine(env.WebRootPath, "Pictures", f);
                        FileStream fs = new FileStream(savePath, FileMode.Create);
                        model.Picture.CopyTo(fs);
                        fs.Close();
                        e.Picture = f;
                    }
                   
                   
                    
                    db.SaveChanges();
                    db.Database.ExecuteSqlInterpolated($"EXEC DeleteAttendanceOfEmployee {e.EmployeeId}");
                    foreach (var a in model.Attendances)
                    {
                        db.Database.ExecuteSqlInterpolated($"EXECUTE InsertAttendance {a.AttendanceDate}, {a.InTime}, {a.OutTime}, {e.EmployeeId}");
                    }
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = e.Picture;
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            if(!db.Employees.Any(x=>x.EmployeeId == id)) return NotFound();
            db.Database.ExecuteSqlInterpolated($"EXEC DeleteEmployee {id}");
            return Json(new {success= true});
        }
    }
}

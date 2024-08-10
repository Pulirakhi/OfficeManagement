using OfficeManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OfficeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            Employee _Employee = new Employee();
            List<Employee> list = _Employee.GetEmployees();
            return View(list);
        }
        public ActionResult GetEmployees()
        {
            Employee _Employee = new Employee();
            List<Employee> list = _Employee.GetEmployees();
            return PartialView("List", list);
        }
        public ActionResult NewEmployee()
        {
            Employee _Employee = new Employee();
            _Employee.DateOfJoin = DateTime.Now;
            return PartialView(_Employee);
        }
        public ActionResult EditEmployee(int id)
        {
            Employee _Employee = new Employee();
            var emplist = _Employee.GetEmployees();
            if (emplist.Count > 0 && emplist.Where(x => x.Id == id).ToList().Count > 0)
            {
                _Employee = emplist.Where(x => x.Id == id).SingleOrDefault();
            }
            return PartialView("NewEmployee", _Employee);
        }
        public ActionResult SaveEmployee(int Id, string Name, string Designation, string DateOfJoin, string Salary, string Gender, string State)
        {
            try
            {
                Employee _Employee = new Employee();
                _Employee.Id = Id;
                _Employee.Name = Name;
                _Employee.Designation = Designation;
                _Employee.DateOfJoin = Convert.ToDateTime(DateOfJoin);
                _Employee.Salary = Convert.ToDecimal(Salary);
                _Employee.Gender = Gender;
                _Employee.State = State;
                _Employee.SaveEmployee(_Employee);
                if (_Employee.DateOfJoin > DateTime.Now)
                {
                    return Json(new { status = false, Message = "You can't select a future date." });
                }
                return Json(new { status = true, Message = "Success" });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, Message = "Failed : " + ex.Message });
            }
        }
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                if (id > 0)
                {
                    new Employee().DeleteEmployee(id);
                    return Json(new { status = true, Message = "Succesfully deleted." });
                }
                else
                {
                    return Json(new { status = true, Message = "Invalid employee id." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, Message = "Failed to delete : " + ex.Message });
            }
        }

        //public ActionResult EmployeeReport()
        //{
            
        //}
    }
}
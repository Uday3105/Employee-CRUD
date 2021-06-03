using EmployeeCRUD.Models;
using EmployeeCRUD.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeRepository empRepository = new EmployeeRepository();

        // GET: Employee/GetAllEmpDetails    
        public ActionResult GetAllEmpDetails()
        {
            ModelState.Clear();
            var employees = empRepository.GetAllEmployees();
            foreach (var employee in employees)
            {
                employee.Address = empRepository.GetAddressById(employee.AddressId);
            }
            return View(employees);
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddEmployee()
        {
            Country_Bind();
            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddEmployee(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository empRepo = new EmployeeRepository();
                        var countryId= Country_Bind().FirstOrDefault().Value;
                    var countryName= Country_Bind().FirstOrDefault().Text;
                
                    if (empRepo.AddEmployee(emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult EditEmpDetails(int id)
        {
            EmployeeRepository empRepo = new EmployeeRepository();
            return View(empRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditEmpDetails(int id, Employee obj)
        {
            try
            {
                EmployeeRepository EmpRepo = new EmployeeRepository();

                EmpRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch( Exception ex)
            {
                //throw ex;
                return RedirectToAction("GetAllEmpDetails");
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmployeeRepository EmpRepo = new EmployeeRepository();
                if (EmpRepo.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";
                }
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return RedirectToAction("GetAllEmpDetails");
            }
        }
        public ActionResult ViewEmpDetails(int id)
        {
            EmployeeRepository empRepo = new EmployeeRepository();
            return View(empRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));
        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult ViewEmpDetails(int id, Employee obj)
        {
            try
            { 
                
                return RedirectToAction("GetAllEmpDetails");
            }
            catch (Exception ex)
            {
                return RedirectToAction("GetAllEmpDetails");
                //throw ex;
            }
        }

        public List<SelectListItem> Country_Bind()
        {
            DataSet ds = empRepository.Get_Country();
            List<SelectListItem> coutrylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                coutrylist.Add(new SelectListItem {  Value = dr["Id"].ToString(),Text = dr["Country_Name"].ToString() });
            }
            ViewBag.Country = coutrylist;
            return coutrylist;
        }
        public JsonResult State_Bind(string country_id)
        {
            DataSet ds = empRepository.Get_State(country_id);
            List<SelectListItem> statelist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                statelist.Add(new SelectListItem {  Value = dr["Id"].ToString(), Text = dr["State_Name"].ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult District_Bind(string state_id)
        {
            DataSet ds = empRepository.Get_District(state_id);
            List<SelectListItem> citylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                citylist.Add(new SelectListItem {  Value = dr["Id"].ToString(), Text = dr["District_Name"].ToString() });
            }
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }
    }
}

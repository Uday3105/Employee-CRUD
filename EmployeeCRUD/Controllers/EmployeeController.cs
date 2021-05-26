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
        EmpRepository empRepository = new EmpRepository();

        // GET: Employee/GetAllEmpDetails    
        public ActionResult GetAllEmpDetails()
        {

            EmpRepository EmpRepo = new EmpRepository();
            ModelState.Clear();
            return View(EmpRepo.GetAllEmployees());
        }
        // GET: Employee/AddEmployee    
        public ActionResult AddEmployee()
        {
            Country_Bind();

            return View();
        }

        // POST: Employee/AddEmployee    
        [HttpPost]
        public ActionResult AddEmployee(EmpModel emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EmpRepository empRepo = new EmpRepository();
                    var countryId= Country_Bind().FirstOrDefault().Value;
                    var countryName= Country_Bind().FirstOrDefault().Text;
                    //ToDO: Need to modify logic to retrieve state and city values

                    //var ststeId= JsonConvert.DeserializeObject<List<string>>(State_Bind(countryId).ToString());
                    //var cityId = City_Bind(ststeId);
                    emp.Country = countryName;

                    if (empRepo.AddEmployee(emp))
                    {
                        ViewBag.Message = "Employee details added successfully";
                    }
                }

                return View();
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Employee/EditEmpDetails/5    
        public ActionResult EditEmpDetails(int id)
        {
            EmpRepository empRepo = new EmpRepository();



            return View(empRepo.GetAllEmployees().Find(Emp => Emp.Empid == id));

        }

        // POST: Employee/EditEmpDetails/5    
        [HttpPost]

        public ActionResult EditEmpDetails(int id, EmpModel obj)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();

                EmpRepo.UpdateEmployee(obj);
                return RedirectToAction("GetAllEmpDetails");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/DeleteEmp/5    
        public ActionResult DeleteEmp(int id)
        {
            try
            {
                EmpRepository EmpRepo = new EmpRepository();
                if (EmpRepo.DeleteEmployee(id))
                {
                    ViewBag.AlertMsg = "Employee details deleted successfully";

                }
                return RedirectToAction("GetAllEmpDetails");

            }
            catch
            {
                return View();
            }
        }

        
        public List<SelectListItem> Country_Bind()
        {
            DataSet ds = empRepository.Get_Country();
            List<SelectListItem> coutrylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                coutrylist.Add(new SelectListItem { Text = dr["Country_name"].ToString(), Value = dr["Country_id"].ToString() });
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
                statelist.Add(new SelectListItem { Text = dr["State"].ToString(), Value = dr["State_id"].ToString() });
            }
            return Json(statelist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult City_Bind(string state_id)
        {
            DataSet ds = empRepository.Get_City(state_id);
            List<SelectListItem> citylist = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                citylist.Add(new SelectListItem { Text = dr["City"].ToString(), Value = dr["City_id"].ToString() });
            }
            return Json(citylist, JsonRequestBehavior.AllowGet);
        }
    }
}

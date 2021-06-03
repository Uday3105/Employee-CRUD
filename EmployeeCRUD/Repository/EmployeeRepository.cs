using EmployeeCRUD.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeCRUD.Repository
{
    public class EmployeeRepository
    {

        private SqlConnection con;
        //To Handle connection related activities  
        public EmployeeRepository()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);
        }
        //To Add Employee details    
        public bool AddEmployee(Employee obj)
        {
            SqlCommand com = new SqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@First_Name", obj.FirstName);
            com.Parameters.AddWithValue("@Last_Name", obj.LastName);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@Languages_known", obj.LanguagesKnown);
            com.Parameters.AddWithValue("@Address_Id", obj.AddressId);
            com.Parameters.AddWithValue("@Salary", obj.Salary);
            com.Parameters.AddWithValue("@Is_Active", obj.IsActive);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //To view employee details with generic list     
        public List<Employee> GetAllEmployees()
        {
            List<Employee> EmpList = new List<Employee>();

            SqlCommand com = new SqlCommand("GetEmployees", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            //Bind EmpModel generic list using dataRow     
            foreach (DataRow dr in dt.Rows)
            {
                EmpList.Add(

                    new Employee
                    {

                        Empid = Convert.ToInt32(dr["Id"]),
                        FirstName = Convert.ToString(dr["First_Name"]),
                        LastName = Convert.ToString(dr["Last_Name"]),
                        Gender = Convert.ToInt32(dr["Gender"]),
                        LanguagesKnown = Convert.ToString(dr["Languages_known"]),
                        AddressId = Convert.ToInt32(dr["Address_Id"]),
                        Salary = Convert.ToInt32(dr["Salary"]),
                        IsActive = Convert.ToBoolean(dr["Is_Active"]),

                    }
                    );
            }

            return EmpList;
        }
        //To Update Employee details    
        public bool UpdateEmployee(Employee obj)
        {

            SqlCommand com = new SqlCommand("UpdateEmpDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Id", obj.Empid);
            com.Parameters.AddWithValue("@First_Name", obj.FirstName);
            com.Parameters.AddWithValue("@Last_Name", obj.LastName);
            com.Parameters.AddWithValue("@Gender", obj.Gender);
            com.Parameters.AddWithValue("@Languages_known", obj.LanguagesKnown);
            com.Parameters.AddWithValue("@Address_Id", obj.AddressId);
            com.Parameters.AddWithValue("@Salary", obj.Salary);
            com.Parameters.AddWithValue("@Is_Active", obj.IsActive);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //To delete Employee details    
        public bool DeleteEmployee(int Id)
        {

            SqlCommand com = new SqlCommand("DeleteEmpById", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Get all Countries
        public DataSet Get_Country()
        {
            SqlCommand com = new SqlCommand("Select * from Country_Name", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
        //Get all States
        public DataSet Get_State(string country_id)
        {
            SqlCommand com = new SqlCommand("Select * from State_Name where CountryId=" + country_id, con);
            com.Parameters.AddWithValue("@CountryId", country_id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
        //Get all Districts
        public DataSet Get_District(string state_id)
        {
            SqlCommand com = new SqlCommand("Select * from District_Name where StateId=" + state_id, con);
            com.Parameters.AddWithValue("@StateId", state_id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public string GetAddressById(int addressId)
        {
            string address = "";
            var countries = GetCountries();
            var states = GetStates();
            var districts = GetDistricts();
                      
            SqlCommand command = new SqlCommand("Select * from Address1 where Id=" + addressId, con);
            command.Parameters.AddWithValue("@Id", addressId);
            SqlDataReader reader;
            con.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    address = reader.GetString(1) +  "," + reader.GetString(2) +  "," + countries[reader.GetInt32(3)-1] + "," + states[reader.GetInt32(4)-1] + "," + districts[reader.GetInt32(5)-1] + ',' + reader.GetString(6);
                }
            }
            reader.Close();
            con.Close();
            return address;
        }
        public List<string> GetCountries()
        {
            List<string> countries = new List<string>();
            SqlCommand command = new SqlCommand("Select * from Country_Name " , con);
            SqlDataReader reader;
            con.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    countries.Add(reader.GetString(1));
                }
            }
            reader.Close();
            con.Close();
            return countries;
        }
        public List<string> GetStates()
        {
            List<string> states = new List<string>();
            SqlCommand command = new SqlCommand("Select * from State_Name " , con);
            SqlDataReader reader;
            con.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    states.Add( reader.GetString(1));
                }
            }
            reader.Close();
            con.Close();
            return states;
        }
        public List<string> GetDistricts()
        {
            List<string> districts = new List<string>();
            SqlCommand command = new SqlCommand("Select * from District_Name ", con);
            SqlDataReader reader;
            con.Open();
            reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    districts.Add(reader.GetString(1));
                }
            }
            reader.Close();
            con.Close();
            return districts;
        }
    }
}
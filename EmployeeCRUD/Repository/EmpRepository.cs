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
    public class EmpRepository
    {

        private SqlConnection con;
        //To Handle connection related activities  
        public EmpRepository()
        {
            string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
            con = new SqlConnection(constr);

        }
        //private void connection()
        //{
        //    string constr = ConfigurationManager.ConnectionStrings["getconn"].ToString();
        //    con = new SqlConnection(constr);

        //}

        //To Add Employee details    
        public bool AddEmployee(EmpModel obj)
        {

            //connection();
            SqlCommand com = new SqlCommand("AddNewEmpDetails", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Country", obj.Country);
            //com.Parameters.AddWithValue("@State", obj.State);
            //com.Parameters.AddWithValue("@City", obj.City);

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
        public List<EmpModel> GetAllEmployees()
        {
            //connection();
            List<EmpModel> EmpList = new List<EmpModel>();


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

                    new EmpModel
                    {

                        Empid = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                        //Country = Convert.ToString(dr["Country"]),
                        //State = Convert.ToString(dr["State"]),
                        //City = Convert.ToString(dr["City"])
                    }
                    );
            }

            return EmpList;
        }
        //To Update Employee details    
        public bool UpdateEmployee(EmpModel obj)
        {

            //connection();
            SqlCommand com = new SqlCommand("UpdateEmpDetails", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@EmpId", obj.Empid);
            com.Parameters.AddWithValue("@Name", obj.Name);
            com.Parameters.AddWithValue("@Country", obj.Country);
            com.Parameters.AddWithValue("@State", obj.State);
            com.Parameters.AddWithValue("@City", obj.City);
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

            //connection();
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

        public DataSet Get_Country()
        {
            //connection();

            SqlCommand com = new SqlCommand("Select * from Country", con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }

        //Get all State
        public DataSet Get_State(string country_id)
        {
            //connection();

            SqlCommand com = new SqlCommand("Select * from State where Country_id=@catid", con);
            com.Parameters.AddWithValue("@catid", country_id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        //Get all City
        public DataSet Get_City(string state_id)
        {
            //connection();

            SqlCommand com = new SqlCommand("Select * from City where State_id=@stateid", con);
            com.Parameters.AddWithValue("@stateid", state_id);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;

        }
    }
}
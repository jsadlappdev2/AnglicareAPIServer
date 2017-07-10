using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AnglicareAPIServer.Models;
using System.Data;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using AnglicareAPIServer.Context;
using AnglicareAPIServer.Filters;


namespace AnglicareAPIServer.Controllers
{

    //---------------------------------------using TOKEN to authentication------------------------------------------------------------------------- 
    // [APIAuthorizeAttribute]
    public class EmployeedetailsController : ApiController
    {

        //-----------------------------------Query Operation using SQL-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryByID")]
        public HttpResponseMessage QueryByID(int id)
        {
            try
            {
                SqlDataReader reader = null;
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "Select id_number,given_name,family_name,preferred_name,gender from dbo.employee_for_test where id_number=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                Employeedetails emp = null;
                while (reader.Read())
                {
                    emp = new Employeedetails();
                    emp.id_number = Convert.ToInt32(reader.GetValue(0));
                    emp.given_name = reader.GetValue(1).ToString();
                    emp.family_name = reader.GetValue(2).ToString();
                    emp.preferred_name = reader.GetValue(3).ToString();
                    emp.gender = reader.GetValue(4).ToString();
                }
                if (emp == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee " + id.ToString() + " Not Found!");
                }
                else
                {
                    return Request.CreateResponse<Employeedetails>(HttpStatusCode.OK, emp);
                }
            }
            catch(Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing GetEmployee");

            }
        }


       //------------------------------------------delete  operation using SQL -----------------------------------------------------------------------------
        [HttpDelete]
        [ActionName("DeleteEmployeeByID")]
        public HttpResponseMessage DeleteEmployeeByID(int id)
        {

            //check employee number
            int count = 0;
            count = check_employee_number(id);
            if (count >= 1)
            {

                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from employee_for_test where id_number=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                try
                {
                    int rowDeleted = sqlCmd.ExecuteNonQuery();

                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Employee " + id.ToString() + " has been deleted!");

                }
                catch
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing DeleteEmployeeByID.");

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Employee " + id.ToString() + " Not Found and Delete didn't execute.");

            }

        }



        //---------------------------------------------update operation using SQL-----------------------------------------------------------------------
        [HttpPut]
        [ActionName("UpdateEmployee")]
        public HttpResponseMessage UpdateEmployeebyID([FromUri]int id, [FromBody]Employeedetails employee)
        {

            //check employee number
            int count = 0;
            count = check_employee_number(id);
            if (count >= 1)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "update  employee_for_test set given_name=@givenname,family_name=@familyname,preferred_name=@preferredname,gender=@gender  where id_number=" + id + "";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@givenname", employee.given_name);
                sqlCmd.Parameters.AddWithValue("@familyname", employee.family_name);
                sqlCmd.Parameters.AddWithValue("@preferredname", employee.preferred_name);
                sqlCmd.Parameters.AddWithValue("@gender", employee.gender);
                myConnection.Open();
                try
                {
                    int rowInserted = sqlCmd.ExecuteNonQuery();                    
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Employee " + id.ToString() + " has been udpated!");
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing UpdateEmployee.");

                }
                finally
                {
                    myConnection.Close();
                }

            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Employee "+ id.ToString() + " Not Found and Update didn't execute.");

            }


        }

    

      //--------------------------------------function to check employee numbers------------------------------------------------------------------
      public int check_employee_number(int id)
      {
            int count = 0;
            string sql = "Select *  from dbo.employee_for_test where id_number=" + id + "";
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connsql = new SqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            SqlCommand Cmd = new SqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            try
           {
            count = dt.Rows.Count;
           }
            catch (Exception)
           {
            count = -1;
           }
           finally
           {
            connsql.Close();
           }

          return count;


    }

        //-----------------------------------------insert operation using SQL----------------------------------------------------------------------    
        //insert
        [HttpPost]
        [ActionName("AddEmployee")]
        public HttpResponseMessage AddEmployee([FromBody] Employeedetails employee)
        {
      


            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlCommand sqlCmd = new SqlCommand("INSERT INTO tblEmployee (EmployeeId,Name,ManagerId) Values (@EmployeeId,@Name,@ManagerId)", myConnection);   
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO employee_for_test (id_number,given_name,family_name,preferred_name,gender) Values (@idnumber,@givenname,@familyname,@preferredname,@gender)";
            sqlCmd.Connection = myConnection;


            sqlCmd.Parameters.AddWithValue("@idnumber", employee.id_number);
            sqlCmd.Parameters.AddWithValue("@givenname", employee.given_name);
            sqlCmd.Parameters.AddWithValue("@familyname", employee.family_name);
            sqlCmd.Parameters.AddWithValue("@preferredname", employee.preferred_name);
            sqlCmd.Parameters.AddWithValue("@gender", employee.gender);
            try
            {
                myConnection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                var message = new HttpResponseMessage(HttpStatusCode.Created);
                message.Content = new StringContent("Insert new record successfully");
                return new HttpResponseMessage { StatusCode = HttpStatusCode.Created };
            }
            catch (Exception)
            {

                var message = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
                message.Content = new StringContent("Insert new record faild");
                return new HttpResponseMessage { StatusCode = HttpStatusCode.NotAcceptable };

            }
            finally
            {
                myConnection.Close();
            }
        }

    }
 
}

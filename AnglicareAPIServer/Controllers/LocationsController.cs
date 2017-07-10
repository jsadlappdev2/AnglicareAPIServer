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
using Newtonsoft.Json;



namespace AnglicareAPIServer.Controllers
{
    //---------------------------------------using TOKEN to authentication------------------------------------------------------------------------- 
    // [APIAuthorizeAttribute]

  
    public class LocationsController : ApiController
    {

        //---------------------------------------------------------Insert operation using SQL---------------------------------------------
        [HttpPost]
        [ActionName("AddLocation")]
        public HttpResponseMessage AddLocation([FromBody] Locations location)
        {

            //check exist or not 
      
            string sql = "Select *  from user_locations where employee_id='" + location.employee_id + "'";
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connsql = new SqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            SqlCommand Cmd = new SqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            if (dt.Rows.Count.ToString() == "0")
            {

                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO user_locations (employee_id,name,locations) Values (@idnumber,@name,@location)";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@idnumber", location.employee_id);
                sqlCmd.Parameters.AddWithValue("@name", location.name);
                sqlCmd.Parameters.AddWithValue("@location", location.locations);

                try
                {
                    myConnection.Open();
                    int rowInserted = sqlCmd.ExecuteNonQuery();

                    // var message = new HttpResponseMessage(HttpStatusCode.Created);
                    // message.Content = new StringContent("Location is created");
                    // return new HttpResponseMessage { StatusCode = HttpStatusCode.Created };
                    return Request.CreateErrorResponse(HttpStatusCode.Created, "Location for " + location.name + " has been created!");
                }
                catch (Exception)
                {

                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing AddLocation.");

                }
                finally
                {
                    myConnection.Close();
                }
            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " EmployeeID " + location.employee_id.ToString() + "'s location has exited and cannot be created again.");



            }
        }

        //-----------------------------------QuerybyID Operation using SQL-----------------------------------------------------------------------------
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
                sqlCmd.CommandText = "Select employee_id,name,locations from user_locations where employee_id='" + id + "' ";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                reader = sqlCmd.ExecuteReader();
                Locations loc = null;
                while (reader.Read())
                {
                    loc = new Locations();
                    loc.employee_id = reader.GetValue(0).ToString();
                    loc.name = reader.GetValue(1).ToString();
                    loc.locations = reader.GetValue(2).ToString();
             
                }
                if (loc == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Location for " + id.ToString() + " Not Found!");
                }
                else
                {
                    return Request.CreateResponse<Locations>(HttpStatusCode.OK, loc);
                }
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing QueryByID");

            }
        }      



        //-----------------------------------Way1: Query all Operation using SQL-----------------------------------------------------------------------------
        [HttpGet]
        [ActionName("QueryAll")]
        public string QueryAll()
        {
            

                string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                var con = new SqlConnection(conString);
                con.Open();
                var cmd = new SqlCommand("Select employee_id,name,locations from user_locations order by  employee_id asc ", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = cmd;
                sda.Fill(dt);
                //check if there are results
                List<string> result = new List<string>();
                if (dt.Rows.Count > 0)
                {

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            var o = dt.Rows[i].ItemArray[j];
                            result.Add(o.ToString());
                        }
                    }
                }
                //put the array to JSON and output the results

                string jsonString = string.Empty;
                jsonString = JsonConvert.SerializeObject(dt);
                return jsonString;
           
          
        }


        //-----------------------------------Way2: Query all Operation using SQL-----------------------------------------------------------------------------
        //Better way: can get JSON data in a good format.
        [HttpGet]
        [ActionName("QueryAll2")]
        public List<Locations> QueryAll2()
        {



            string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            var con = new SqlConnection(conString);

            SqlCommand cmd = new SqlCommand("Select employee_id,name,locations from user_locations order by  employee_id asc ", con);
            cmd.CommandType = CommandType.Text;
            // Create a DataAdapter to run the command and fill the DataTable
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<Locations> alllocations = new List<Locations>();
            foreach (DataRow row in dt.Rows)
            {
                alllocations.Add(new Locations() {
                                                 employee_id =row["employee_id"].ToString(),
                                                 name =row["name"].ToString(),
                                                 locations =row["locations"].ToString()
                                                 });
            }

            if (alllocations == null)
            {
                return null;

            }
            else
            {
                return alllocations;
            }


        }

        //------------------------------------------delete  operation using SQL -----------------------------------------------------------------------------
        [HttpDelete]
        [ActionName("DeleteByID")]
        public HttpResponseMessage DeleteByID(int id)
        {

            //check employee number
            int count = 0;
            string sql = "Select *  from user_locations where employee_id='" + id.ToString()  + "'";
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connsql = new SqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            SqlCommand Cmd = new SqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            count = dt.Rows.Count;
            if (count >= 1)
            {

                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from user_locations where employee_id=" + id + "";
                sqlCmd.Connection = myConnection;
                myConnection.Open();
                try
                {
                    int rowDeleted = sqlCmd.ExecuteNonQuery();

                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Employee " + id.ToString() + " has been deleted!");

                }
                catch
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing DeleteByID.");

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
        [ActionName("UpdateLocation")]
        public HttpResponseMessage UpdateLocation([FromUri]int id, [FromBody]Locations location)
        {

            //check employee number
            int count = 0;
            string sql = "Select *  from user_locations where employee_id='" + id.ToString() + "'";
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connsql = new SqlConnection(connStr);
            if (connsql.State.ToString() == "Closed") connsql.Open();
            SqlCommand Cmd = new SqlCommand(sql, connsql);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = Cmd;
            sda.Fill(dt);
            count = dt.Rows.Count;
            if (count >= 1)
            {
                SqlConnection myConnection = new SqlConnection();
                myConnection.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "update  user_locations set name=@name,locations=@locations where employee_id=" + id + "";
                sqlCmd.Connection = myConnection;


                sqlCmd.Parameters.AddWithValue("@name", location.name);
                sqlCmd.Parameters.AddWithValue("@locations", location.locations);
    
                myConnection.Open();
                try
                {
                    int rowInserted = sqlCmd.ExecuteNonQuery();
                    return Request.CreateErrorResponse(HttpStatusCode.OK, "Employee " + id.ToString() + "'s location has been udpated!");
                }
                catch (Exception)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing UpdateLocation.");

                }
                finally
                {
                    myConnection.Close();
                }

            }
            else
            {

                return Request.CreateErrorResponse(HttpStatusCode.NotFound, " Employee " + id.ToString() + " Not Found and Update didn't execute.");

            }


        }


    }
}

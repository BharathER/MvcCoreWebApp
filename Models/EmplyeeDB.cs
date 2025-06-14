using System.Data;
using System.Data.SqlClient;

namespace CoreWebMvc1stApp.Models
{
    public class EmplyeeDB
    {
        SqlConnection con;
        public EmplyeeDB()
        {
            con = new SqlConnection(@"server=222K25\SQLEXPRESS;database=VsNewDB;Integrated Security=True;");
        }

        public string InsertDB(Employee objcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insertDB", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", objcls.Name);
                cmd.Parameters.AddWithValue("@addr", objcls.Address);
                cmd.Parameters.AddWithValue("@sal", objcls.Salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Data Inserted Successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }

        public string LoginDb(Employee objcls)
        {
            try
            {
                string cid = "", msg = "";
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", objcls.Id);
                cmd.Parameters.AddWithValue("@ena", objcls.Name);
                con.Open();
                cid = cmd.ExecuteScalar().ToString();
                con.Close();
                if (cid == "1")
                {
                    msg = "Login Successful";
                }
                else
                {
                    msg = "Invalid Credentials";
                }
                return msg;
                /*SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    con.Close();
                    return "Login Successful";
                }
                else
                {
                    con.Close();
                    return "Invalid Credentials";
                }*/
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
        }
        public Employee Profile(int Id)
        {
            var getdata = new Employee();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_profile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", Id);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    getdata = new Employee
                    {
                        Id = Convert.ToInt32(dr["Emp_Id"]),
                        Name = dr["Emp_Name"].ToString(),
                        Address = dr["Emp_Address"].ToString(),
                        Salary = dr["Emp_Salary"].ToString()
                    };
                }
                con.Close();

                return getdata;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
                //getdata.Name = "SQL Error: " + ex.Message;
            }

        }
        public string UpdateDB(int id,Employee objcls)
        {
            string res = "";
            try
            {
                SqlCommand cmd = new SqlCommand("sp_update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@addr", objcls.Address);
                cmd.Parameters.AddWithValue("@sal", objcls.Salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                res = "Data Updated Successfully";
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message;
            }
            return res;
        }

        public List<Employee> ProfileList()
        {
            var list = new List<Employee>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectAll", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var emp = new Employee
                    {
                        Id = Convert.ToInt32(dr["Emp_Id"]),
                        Name = dr["Emp_Name"].ToString(),
                        Address = dr["Emp_Address"].ToString(),
                        Salary = dr["Emp_Salary"].ToString()
                    };
                    list.Add(emp);
                }
                con.Close();
                return list;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}

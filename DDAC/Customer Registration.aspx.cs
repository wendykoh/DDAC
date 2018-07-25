using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace DDAC
{
    public partial class Customer_Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string query = "select * from Customer where customerid = @id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", cid.Value);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Response.Write("<script>alert('Company ID exist.Please try again.');</script>");
                                        
                    }
                    else
                    {
                        reader.Close();
                        string query1 = "Insert into Customer (customerid,name,personincharge,phoneno,email,password) values(@id,@name,@person,@phone,@email,@password)";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Parameters.AddWithValue("@id", cid.Value);
                        cmd1.Parameters.AddWithValue("@name", name.Value);
                        cmd1.Parameters.AddWithValue("@person", person.Value);
                        cmd1.Parameters.AddWithValue("@phone", phone.Value);
                        cmd1.Parameters.AddWithValue("@email", email.Value);
                        cmd1.Parameters.AddWithValue("@password", password.Value);

                        cmd1.ExecuteNonQuery();
                        Response.Write("<script>alert('Registered successfully.');</script>");
                        Response.Redirect("Login.aspx");
                    }

                    con.Close();
                    }
                }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }
    }
    

    
}
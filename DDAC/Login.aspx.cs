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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string query = "select * from Customer where customerid = @id and password = @password";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", username.Value);
                cmd.Parameters.AddWithValue("@password", password.Value);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Session["id"] = username.Value;
                        Session["name"] = reader["name"].ToString(); 
                        string longurl = "http://tp034714ddac.azurewebsites.net/Home(Customer).aspx";
                        //var uriBuilder = new UriBuilder(longurl);
                        //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                        //qry["id"] = username.Value;
                        //qry["name"] = reader["name"].ToString();
                        //uriBuilder.Query = qry.ToString();
                        //longurl = uriBuilder.ToString();
                        Response.Write("<script>alert('Login successfully.');</script>");
                        Response.Redirect(longurl);

                    }
                    else
                    {
                        reader.Close();
                        string query1 = "select * from Admin where adminid = @id and password = @password"; ;
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Parameters.AddWithValue("@id", username.Value);
                        cmd1.Parameters.AddWithValue("@password", password.Value);
                        using (SqlDataReader reader1 = cmd1.ExecuteReader())
                        {
                            if(reader1.Read())
                            {
                                Session["pid"] = reader1["portid"].ToString();
                                Session["name"] = reader1["name"].ToString();
                                string longurl = "http://tp034714ddac.azurewebsites.net/Home(Admin).aspx";
                                //var uriBuilder = new UriBuilder(longurl);
                                //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                                //qry["name"] = reader1["name"].ToString();
                                //qry["pid"] = reader1["portid"].ToString();
                                //uriBuilder.Query = qry.ToString();
                                //longurl = uriBuilder.ToString();
                                Response.Write("<script>alert('Login successfully.');</script>");
                                Response.Redirect(longurl);
                            }
                            else
                            {
                                Response.Write("<script>alert('Wrong username or password.Please try again.');</script>");
                            }
                        }




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
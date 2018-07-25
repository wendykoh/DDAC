using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace DDAC
{
    public partial class Check_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["id"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
                try
                {
                    con.Open();
                    string query = "select * from Shipping where shippingid = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["sid"]);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            shippingid.Text = reader["shippingid"].ToString();
                            status.Text = reader["status"].ToString();
                            departureport.Text = "Port " + reader["departureport"].ToString();
                            arrivalport.Text = "Port " + reader["arrivalport"].ToString();
                            DateTime dt = (DateTime)reader["shippingdate"];
                            shippingdate.Text = dt.ToString("MM/dd/yyyy");
                            weight.Text = reader["weight"].ToString();
                            remarks.Text = reader["remarks"].ToString();
                            cost.Text = reader["cost"].ToString();

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

        protected void Submit1_Click(object sender, EventArgs e)
        {
            string longurl = "http://tp034714ddac.azurewebsites.net/Home(Customer).aspx";
            //var uriBuilder = new UriBuilder(longurl);
            //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
            //qry["id"] = Request.QueryString["id"] ;
            //qry["name"] = Request.QueryString["name"];
            //uriBuilder.Query = qry.ToString();
            //longurl = uriBuilder.ToString();
            Response.Redirect(longurl);
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
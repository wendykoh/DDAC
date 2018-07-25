using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace DDAC
{
    public partial class Add_Shipping_Request : System.Web.UI.Page
    {
        String id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["id"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                id = (String)Session["id"];
            }
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string query = "select max(shippingid) as maxvalue from Shipping";
                SqlCommand cmd = new SqlCommand(query, con);
                int max = 0;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        String m = reader["maxvalue"].ToString();
                        max = Int32.Parse(m);
                        max++;
                    }
                    reader.Close();
                        string query1 = "Insert into Shipping (shippingid,departureport,arrivalport,shippingdate,weight,cost,status,remarks,customerid) values(@id,@dport,@aport,@date,@weight,@cost,@status,@remarks,@customerid)";
                        SqlCommand cmd1 = new SqlCommand(query1, con);
                        cmd1.Parameters.AddWithValue("@id", max.ToString("000000"));
                        cmd1.Parameters.AddWithValue("@dport", dport.SelectedValue);
                        cmd1.Parameters.AddWithValue("@aport", aport.SelectedValue);
                        cmd1.Parameters.AddWithValue("@date", date.Text);
                        cmd1.Parameters.AddWithValue("@weight", weight.Text);
                        cmd1.Parameters.AddWithValue("@cost", cost.Text);
                        cmd1.Parameters.AddWithValue("@status", "Pending");
                        cmd1.Parameters.AddWithValue("@remarks", remarks.Text);
                        cmd1.Parameters.AddWithValue("@customerid", id);

                        cmd1.ExecuteNonQuery();
                        string longurl = "http://tp034714ddac.azurewebsites.net/Home(Customer).aspx";
                        //var uriBuilder = new UriBuilder(longurl);
                        //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                        //qry["id"] = id;
                        //qry["name"] = Request.QueryString["name"];
                        //uriBuilder.Query = qry.ToString();
                        //longurl = uriBuilder.ToString();
                        Response.Redirect(longurl);

                    


                    con.Close();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        protected void weight_TextChanged(object sender, EventArgs e)
        {
            String kg = weight.Text;
            
            if (Regex.IsMatch(kg, @"^[\+\-]?\d*\.?[Ee]?[\+\-]?\d*$"))
            {
                double kg1 = Double.Parse(kg);
                if (kg1 > 10)
                {
                    cost.Text = (kg1 * 8).ToString();
                }
                else
                {
                    cost.Text = (kg1 * 12).ToString();
                }
                
            }
            else
            {
                Response.Write("<script>alert('Weight only can be number.');</script>");
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }
    }
}
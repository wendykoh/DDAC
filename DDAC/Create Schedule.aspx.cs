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
    public partial class Create_Schedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(Session["pid"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
        }   


        protected void Submit1_Click1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);

            try
            {
                con.Open();
                string query = "select max(scheduleid) as maxvalue from Schedule";
                SqlCommand cmd = new SqlCommand(query, con);
                int max = 0;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        String m = reader["maxvalue"].ToString();
                        if (!m.Equals(""))
                        { 
                            
                            max = Int32.Parse(m);
                            max++;
                        }
                        else
                        {
                            max++;
                        }

                    }
                    else
                    {
                        max++;
                    }
                    reader.Close();
                    string query1 = "Insert into Schedule (scheduleid,departureport,arrivalport,departuredate,arrivaldate,shipid,status) values(@id,@dport,@aport,@ddate,@adate,@sid,@status)";
                    SqlCommand cmd1 = new SqlCommand(query1, con);
                    cmd1.Parameters.AddWithValue("@id", max.ToString("000000"));
                    cmd1.Parameters.AddWithValue("@dport", dport.SelectedValue);
                    cmd1.Parameters.AddWithValue("@aport", aport.SelectedValue);
                    cmd1.Parameters.AddWithValue("@ddate", ddate.Text);
                    cmd1.Parameters.AddWithValue("@adate", adate.Text);
                    cmd1.Parameters.AddWithValue("@sid", shipid.SelectedValue);
                    cmd1.Parameters.AddWithValue("@status", "Pending");

                    cmd1.ExecuteNonQuery();

                    String qry2 = "Update Ship Set status = 'Booked' where shipid = @sid";
                    SqlCommand cmd2 = new SqlCommand(qry2, con);
                    cmd2.Parameters.AddWithValue("@sid", shipid.SelectedValue);


                    cmd2.ExecuteNonQuery();
                    Session["sid"] = max.ToString("000000");
                   
                    string longurl = "http://tp034714ddac.azurewebsites.net/Assign%20Container.aspx";
                    //var uriBuilder = new UriBuilder(longurl);
                    //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                    //qry["sid"] = 
                    //qry["pid"] = Request.QueryString["pid"];
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

        protected void Logout(object sender, EventArgs e)
        {
            Session.Abandon();
            Response.Redirect("Login.aspx");
        }

    }
}
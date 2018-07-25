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
    public partial class Update_Schedule : System.Web.UI.Page
    {
        DateTime dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["pid"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
                try
                {
                    con.Open();
                    string query = "select * from Schedule where scheduleid = @id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", Request.QueryString["sid"]);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            scheduleid.Text = reader["scheduleid"].ToString();
                            dport.Text = "Port " + reader["departureport"].ToString();
                            aport.Text = "Port " + reader["arrivalport"].ToString();
                            dt = (DateTime)reader["departuredate"];
                            ddate.Text = dt.ToString("MM/dd/yyyy");
                            dt = (DateTime)reader["arrivaldate"];
                            adate.Text = dt.ToString("MM/dd/yyyy");
                            shipid.Text = reader["shipid"].ToString();


                            if (reader["status"].ToString().Equals("Pending"))
                            {
                                status.Items.Add(new ListItem("Pending", "Pending"));
                                status.Items.Add(new ListItem("Shipping", "Shipping"));
                            }
                            else if (reader["status"].ToString().Equals("Shipping"))
                            {
                                status.Items.Add(new ListItem("Shipping", "Shipping"));
                                status.Items.Add(new ListItem("Delivered", "Delivered"));

                            }
                            else if (reader["status"].ToString().Equals("Delivered"))
                            {
                                status.Items.Add(new ListItem("Delivered", "Delivered"));
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
                

        protected void Submit_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
            try
            {
                String query1;
                con2.Open();

                if (status.SelectedValue.Equals("Shipping"))
                {
                    query1 = "Update Schedule Set status = 'Shipping' where scheduleid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con2);
                    cmd1.Parameters.AddWithValue("@id", scheduleid.Text);
                    cmd1.ExecuteNonQuery();
                }
                else if (status.SelectedValue.Equals("Delivered"))
                {
                    query1 = "Update Schedule Set status = 'Delivered' where scheduleid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con2);
                    cmd1.Parameters.AddWithValue("@id", scheduleid.Text);
                    cmd1.ExecuteNonQuery();

                    String query2 = "Update Container Set status = 'available' where containerid in (select containerid from Schedule_Container where scheduleid = @id)";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    cmd2.Parameters.AddWithValue("@id", scheduleid.Text);
                    cmd2.ExecuteNonQuery();

                    String query3 = "Update Ship Set status = 'available' where shipid =  @sid";
                    SqlCommand cmd3 = new SqlCommand(query3, con2);
                    cmd3.Parameters.AddWithValue("@sid", shipid.Text);
                    cmd3.ExecuteNonQuery();
                }

               

                con2.Close();
                string longurl = "http://tp034714ddac.azurewebsites.net/Check%20Schedule.aspx";
                //var uriBuilder = new UriBuilder(longurl);
                //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                //qry["name"] = Request.QueryString["name"];
                //qry["pid"] = Request.QueryString["pid"];
                //uriBuilder.Query = qry.ToString();
                //longurl = uriBuilder.ToString();

                Response.Redirect(longurl);
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.ToString());
            }
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            string longurl = "http://tp034714ddac.azurewebsites.net/Home(Admin).aspx";
            //var uriBuilder = new UriBuilder(longurl);
            //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
            //qry["name"] = Request.QueryString["name"];
            //qry["pid"] = Request.QueryString["pid"];
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
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
    public partial class Update_details : System.Web.UI.Page
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
                if (!this.IsPostBack)
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
                    try
                    {
                        con.Open();
                        string query = "select * from Shipping where shippingid = @id";
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@id", Request.QueryString["sid"]);
                        String status1 = null;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                shippingid.Text = reader["shippingid"].ToString();
                                departureport.Text = "Port " + reader["departureport"].ToString();
                                arrivalport.Text = "Port " + reader["arrivalport"].ToString();
                                dt = (DateTime)reader["shippingdate"];
                                shippingdate.Text = dt.ToString("MM/dd/yyyy");
                                weight.Text = reader["weight"].ToString();
                                remarks.Text = reader["remarks"].ToString();
                                cost.Text = reader["cost"].ToString();
                                status1 = reader["status"].ToString();
                                if (reader["status"].ToString().Equals("Pending"))
                                {
                                    status.Items.Add(new ListItem("Pending", "Pending"));
                                    status.Items.Add(new ListItem("Approved", "Approved"));
                                    status.Items.Add(new ListItem("Rejected", "Rejected"));
                                }
                                else if (reader["status"].ToString().Equals("Approved"))
                                {
                                    status.Items.Add(new ListItem("Approved", "Approved"));
                                    status.Items.Add(new ListItem("Shipping", "Shipping"));
                                    scheduleid.Items.Add(new ListItem(reader["scheduleid"].ToString(), reader["scheduleid"].ToString()));

                                    containerid.Items.Add(new ListItem(reader["containerid"].ToString(), reader["containerid"].ToString()));
                                }
                                else if (reader["status"].ToString().Equals("Shipping"))
                                {
                                    status.Items.Add(new ListItem("Shipping", "Shipping"));
                                    status.Items.Add(new ListItem("Delivered", "Delivered"));

                                    scheduleid.Items.Add(new ListItem(reader["scheduleid"].ToString(), reader["scheduleid"].ToString()));

                                    containerid.Items.Add(new ListItem(reader["containerid"].ToString(), reader["containerid"].ToString()));
                                }
                                else if (reader["status"].ToString().Equals("Rejected"))
                                {
                                    status.Items.Add(new ListItem("Rejected", "Rejected"));
                                }
                                else if (reader["status"].ToString().Equals("Delivered"))
                                {
                                    status.Items.Add(new ListItem("Delivered", "Delivered"));

                                    containerid.Items.Add(new ListItem(reader["containerid"].ToString(), reader["containerid"].ToString()));
                                    scheduleid.Items.Add(new ListItem(reader["scheduleid"].ToString(), reader["scheduleid"].ToString()));
                                }



                            }
                            reader.Close();
                            if (status1.Equals("Pending"))
                            {
                                string query1 = "select scheduleid from Schedule where departuredate = @date";
                                SqlCommand cmd1 = new SqlCommand(query1, con);
                                cmd1.Parameters.AddWithValue("@date", dt);

                                scheduleid.Items.Add(new ListItem("Please Select", "Please Select"));
                                using (SqlDataReader reader1 = cmd1.ExecuteReader())
                                {
                                    while (reader1.Read())
                                    {
                                        String sid = reader1["scheduleid"].ToString();
                                        scheduleid.Items.Add(new ListItem(sid, sid));

                                    }
                                }
                                con.Close();
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.ToString());
                    }
                }
            }
        }

        protected void schedule_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (scheduleid.SelectedValue.Equals("Please Select"))
            {
                containerid.Items.Clear();
            }
            else
            {
                SqlConnection con1 = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
                try
                {
                    con1.Open();
                    string query1 = "select * from Schedule_Container where scheduleid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con1);
                    cmd1.Parameters.AddWithValue("@id", scheduleid.SelectedValue);

                    using (SqlDataReader reader = cmd1.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            String cid = reader["containerid"].ToString();
                            containerid.Items.Add(new ListItem(cid, cid));

                        }
                    }
                    con1.Close();
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.ToString());
                }
            }
        }

        protected void status_SelectedIndexChanged(object sender, EventArgs e)
        {
            String value = status.SelectedValue;
            if(value.Equals("Rejected"))
            {
                scheduleid.Visible = false;
                containerid.Visible = false;
                Label9.Visible = false;
                Label10.Visible = false;
            }
            else
            {
                scheduleid.Visible = true;
                containerid.Visible = true;
                Label9.Visible = true;
                Label10.Visible = true;
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
            try
            {
                String query1;
                con2.Open();
                
                if(status.SelectedValue.Equals("Rejected"))
                {
                    query1 = "Update Shipping Set status = 'Rejected' where shippingid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con2);
                    cmd1.Parameters.AddWithValue("@id", shippingid.Text);
                    cmd1.ExecuteNonQuery();
                }
                else if (status.SelectedValue.Equals("Approved"))
                {
                    query1 = "Update Shipping Set status = 'Approved', scheduleid = @sid, containerid = @cid where shippingid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con2);
                    cmd1.Parameters.AddWithValue("@id", shippingid.Text);
                    cmd1.Parameters.AddWithValue("@sid", scheduleid.SelectedValue);
                    cmd1.Parameters.AddWithValue("@cid", containerid.SelectedValue);
                    cmd1.ExecuteNonQuery();
                }
                else
                {
                    query1 = "Update Shipping Set status = @status where shippingid = @id";
                    SqlCommand cmd1 = new SqlCommand(query1, con2);
                    cmd1.Parameters.AddWithValue("@status",status.SelectedValue);
                    cmd1.Parameters.AddWithValue("@id", shippingid.Text);
                    cmd1.ExecuteNonQuery();
                }
                
                
                con2.Close();
                string longurl = "http://tp034714ddac.azurewebsites.net/Home(Admin).aspx";
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
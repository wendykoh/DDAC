using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;


namespace DDAC
{
    public partial class Home_Admin_ : System.Web.UI.Page
    {
        String name;
        String pid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["pid"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                pid = (String)Session["pid"];
                name = (String)Session["name"];
                if (!this.IsPostBack)
                {
                    DataTable dt = this.GetData();
                    StringBuilder html = new StringBuilder();
                    html.Append("<table class='w3-table-all w3-margin-top w3-hoverable' id='table1' >");
                    html.Append("<tr>");
                    html.Append("<th>Shipping ID</th>");
                    html.Append("<th>Customer ID</th>");
                    html.Append("<th>Departure Port</th>");
                    html.Append("<th>Arrival Port</th>");
                    html.Append("<th>Shipping Date</th>");
                    html.Append("<th>Status</th>");
                    html.Append("</tr>");

                    foreach (DataRow row in dt.Rows)
                    {
                        html.Append("<tr>");

                        string longurl = "http://tp034714ddac.azurewebsites.net/Update%20details.aspx";
                        var uriBuilder = new UriBuilder(longurl);
                        var qry = HttpUtility.ParseQueryString(uriBuilder.Query);

                        qry["sid"] = row["shippingid"].ToString();
                        //qry["name"] = name;
                        //qry["pid"] = Request.QueryString["pid"];
                        //qry["ddate"] = row["shippingdate"].ToString();
                        uriBuilder.Query = qry.ToString();
                        longurl = uriBuilder.ToString();
                        html.Append("<td><a href='" + longurl + "'>");
                        html.Append(row["shippingid"]);
                        html.Append("</a></td>");

                        html.Append("<td>");
                        html.Append(row["customerid"]);
                        html.Append("</td>");

                        html.Append("<td> Port ");
                        html.Append(row["departureport"]);
                        html.Append("</td>");

                        html.Append("<td> Port ");
                        html.Append(row["arrivalport"]);
                        html.Append("</td>");

                        html.Append("<td>");

                        html.Append(row["shippingdate"]);
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(row["Status"]);
                        html.Append("</td>");

                        html.Append("</tr>");
                    }

                    html.Append("</table>");
                    PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                }
            }

        }

        private DataTable GetData()
        {
            String constr = ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Shipping where departureport = @id or arrivalport = @id"))
                {
                    cmd.Parameters.AddWithValue("@id", pid);
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        protected void add_Click(object sender, EventArgs e)
        {
            string longurl = "http://tp034714ddac.azurewebsites.net/Check%20Schedule.aspx";
            //var uriBuilder = new UriBuilder(longurl);
            //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
            //qry["name"] = name;
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
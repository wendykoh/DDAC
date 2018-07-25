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
    public partial class Check_Schedule : System.Web.UI.Page
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
                    html.Append("<th>Schedule ID</th>");
                    html.Append("<th>Departure Port</th>");
                    html.Append("<th>Arrival Port</th>");
                    html.Append("<th>Departure Date</th>");
                    html.Append("<th>Arrival Date</th>");
                    html.Append("<th>Status</th>");
                    html.Append("</tr>");

                    foreach (DataRow row in dt.Rows)
                    {
                        html.Append("<tr>");

                        string longurl = "http://tp034714ddac.azurewebsites.net/Update%20Schedule.aspx";
                        var uriBuilder = new UriBuilder(longurl);
                        var qry = HttpUtility.ParseQueryString(uriBuilder.Query);

                        qry["sid"] = row["scheduleid"].ToString();

                        uriBuilder.Query = qry.ToString();
                        longurl = uriBuilder.ToString();
                        html.Append("<td><a href='" + longurl + "'>");
                        html.Append(row["scheduleid"]);
                        html.Append("</a></td>");

                        html.Append("<td> Port ");
                        html.Append(row["departureport"]);
                        html.Append("</td>");

                        html.Append("<td> Port ");
                        html.Append(row["arrivalport"]);
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(row["departuredate"]);
                        html.Append("</td>");

                        html.Append("<td>");
                        html.Append(row["arrivaldate"]);
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
                using (SqlCommand cmd = new SqlCommand("select * from Schedule where departureport = @port or arrivalport = @port"))
                {
                    cmd.Parameters.AddWithValue("@port",pid);
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
            string longurl = "http://tp034714ddac.azurewebsites.net/Create%20Schedule.aspx";
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
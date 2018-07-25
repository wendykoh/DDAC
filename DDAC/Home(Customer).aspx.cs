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
    public partial class Home_Customer_ : System.Web.UI.Page
    {
        String id;
        String name;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["id"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                id = (String)Session["id"];
                name = (String)Session["name"];
                if (!this.IsPostBack)
                {
                    DataTable dt = this.GetData();
                    StringBuilder html = new StringBuilder();
                    html.Append("<table class='w3-table-all w3-margin-top w3-hoverable' id='table1' >");
                    html.Append("<tr>");
                    html.Append("<th>Shipping ID</th>");
                    html.Append("<th>Departure Port</th>");
                    html.Append("<th>Arrival Port</th>");
                    html.Append("<th>Shipping Date</th>");
                    html.Append("<th>Status</th>");
                    html.Append("</tr>");

                    foreach (DataRow row in dt.Rows)
                    {
                        html.Append("<tr>");

                        string longurl = "http://tp034714ddac.azurewebsites.net/Check%20details.aspx";
                        var uriBuilder = new UriBuilder(longurl);
                        var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
                        //qry["id"] = id;
                        qry["sid"] = row["shippingid"].ToString();
                        //qry["name"] = name;
                        uriBuilder.Query = qry.ToString();
                        longurl = uriBuilder.ToString();
                        html.Append("<td><a href='" + longurl + "'>");
                        html.Append(row["shippingid"]);
                        html.Append("</a></td>");

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
                
                using (SqlCommand cmd = new SqlCommand("select * from Shipping where customerid = @id"))
                {
                    cmd.Parameters.AddWithValue("@id",id);
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
            string longurl = "http://tp034714ddac.azurewebsites.net/Add%20Shipping%20Request.aspx";
            //var uriBuilder = new UriBuilder(longurl);
            //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
            //qry["id"] = id;
            //qry["name"] = name;
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
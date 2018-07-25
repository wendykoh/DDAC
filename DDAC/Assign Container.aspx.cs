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
    public partial class Assign_Container : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["pid"] as string))
            {
                Response.Redirect("SessionExpired.aspx");
            }
            else
            {
                scheduleid.Text = (String)Session["sid"];
            }
        }

        protected void Submit1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ddacdatabaseConnectionString"].ConnectionString);
            con.Open();
            string query1 = "Insert into Schedule_Container (scheduleid,containerid) values(@sid,@cid)";
            SqlCommand cmd1 = new SqlCommand(query1, con);
            cmd1.Parameters.AddWithValue("@sid", scheduleid.Text);
            cmd1.Parameters.AddWithValue("@cid", containerid.Text);

            cmd1.ExecuteNonQuery();

            string query2 = "Update Container Set status = 'Booked' where containerid = @cid";
            SqlCommand cmd2 = new SqlCommand(query2, con);
            cmd2.Parameters.AddWithValue("@cid", containerid.Text);

            cmd2.ExecuteNonQuery();

            string longurl = "http://tp034714ddac.azurewebsites.net/Assign%20Container.aspx";
            //var uriBuilder = new UriBuilder(longurl);
            //var qry = HttpUtility.ParseQueryString(uriBuilder.Query);
            //qry["sid"] = scheduleid.Text;
            //qry["pid"] = Request.QueryString["pid"];
            //qry["name"] = Request.QueryString["name"];
            //uriBuilder.Query = qry.ToString();
            //longurl = uriBuilder.ToString();
            Response.Redirect(longurl);
            con.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string longurl = "http://tp034714ddac.azurewebsites.net/Check%20Schedule.aspx";
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
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Add Shipping Request.aspx.cs" Inherits="DDAC.Add_Shipping_Request" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css"/>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css"/>
    <script type="text/javascript">
        var appInsights=window.appInsights||function(a){
        function b(a){c[a]=function(){var b=arguments;c.queue.push(function(){c[a].apply(c,b)})}}var c={config:a},d=document,e=window;setTimeout(function(){var b=d.createElement("script");b.src=a.url||"https://az416426.vo.msecnd.net/scripts/a/ai.0.js",d.getElementsByTagName("script")[0].parentNode.appendChild(b)});try{c.cookie=d.cookie}catch(a){}c.queue=[];for(var f=["Event","Exception","Metric","PageView","Trace","Dependency"];f.length;)b("track"+f.pop());if(b("setAuthenticatedUserContext"),b("clearAuthenticatedUserContext"),b("startTrackEvent"),b("stopTrackEvent"),b("startTrackPage"),b("stopTrackPage"),b("flush"),!a.disableExceptionTracking){f="onerror",b("_"+f);var g=e[f];e[f]=function(a,b,d,e,h){var i=g&&g(a,b,d,e,h);return!0!==i&&c["_"+f](a,b,d,e,h),i}}return c
        }({
           instrumentationKey:"c2ee6034-e8d6-40c0-bc11-997f2da918ff"
        });
    
        window.appInsights = appInsights, appInsights.queue && 0 === appInsights.queue.length && appInsights.trackPageView();

        </script>
    <title>Add New Shipping Request</title>
    <style type="text/css">
        .auto-style1 {
            width: 177px;
            float: left;
        }
        .auto-style3 {
            width: 194px;
        }
        .auto-style8 {
            margin-left: 0;
        }
        .auto-style9 {
            float: left;
            width: 200px;
            margin-left: 1px;
        }
        .auto-style11 {
            float: left;
            width: 200px;
            margin-left: 0px;
        }
    </style>
</head>
<body>
     <div class=" w3-container w3-padding w3-hide-small" style="background-color:#ccccff;width:100%">
            <div class="w3-container" id="logo1" style="max-width:40%;max-height:200px">
                <a href="Home(Customer).aspx" title="Home" style="font-size:large">
                    Container Management System
                </a>
            <br/><br/>
            <label class="w3-hide-small" style="font-size:medium;margin-right:2%">Welcome, <%= (String)Session["name"] %>.</label>    

            </div>
            <a href="Logout.aspx" runat="server" class="w3-display-topright w3-hide-small" style="font-size:medium;margin-right:2%">Logout</a>       
      </div>
    <form id="form1" runat="server">
         <div class="w3-container w3-centered w3-border-purple w3-content" style="max-width:95%">
            <div class="w3-border" style="margin-top:50px">
                <div class="w3-container w3-text-deep-purple" style="text-align:left">
                    <h3>Shipping Request</h3>
                </div><br/>

                 <div>
            <table style="margin-left:15px">
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label2" runat="server" Text="Departure Port" style="float:left"></asp:Label></td>
                    <td class="auto-style3"> 
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ddacdatabaseConnectionString %>" SelectCommand="SELECT * FROM [Port]"></asp:SqlDataSource>
                        <asp:DropDownList ID="dport" runat="server" CssClass="auto-style8" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="portid" Height="24px" Width="206px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label3" runat="server" Text="Arrival Port" style="float:left"></asp:Label></td>
                    <td class="auto-style3"> <asp:DropDownList ID="aport" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="portid" Height="20px" Width="207px"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label4" runat="server" Text="Shipping Date" style="float:left"></asp:Label></td>
                    <td class="auto-style3"> <asp:TextBox ID="date" name="date" type="date" class="auto-style2" required="required" runat="server" CssClass="auto-style9"/></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label5" runat="server" Text="Weight(kg)" style="float:left"></asp:Label></td>
                    <td class="auto-style3"><asp:TextBox ID="weight" runat="server" name="weight" type="text" class="auto-style2" required="required" CssClass="auto-style9" OnTextChanged="weight_TextChanged" AutoPostBack="True"></asp:TextBox> </td> 
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label6" runat="server" Text="Details" style="float:left"></asp:Label></td>
                    <td class="auto-style3">
                        <asp:TextBox ID="remarks" type="text" class="auto-style2" runat="server" CssClass="auto-style11"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label7" runat="server" Text="Estimated Cost(RM)" CssClass="w3-left" Width="153px"></asp:Label></td>
                    <td class="auto-style3"><asp:TextBox id="cost" name="cost" type="text" class="auto-style2" required="required" runat="server" CssClass="auto-style11" ReadOnly="true" AutoPostBack="True"/></td>
                </tr>
           </table>
           <br/><br/>
            <input id="Reset1" type="reset" value="Reset" style="width:80px;margin-left:15px"/> 
            <asp:Button ID="Submit1" runat="server" Text="Submit" Width="80px" style="margin-left:15px" OnClick="Submit1_Click"/>
            <br/><br/>
        </div>
        </div>
             </div>
    </form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer Registration.aspx.cs" Inherits="DDAC.Customer_Registration" %>

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
    
        window.appInsights=appInsights,appInsights.queue&&0===appInsights.queue.length&&appInsights.trackPageView();
    </script>
    <title>Customer Registration</title>
    <style type="text/css">
        .auto-style1 {
            width: 136px;
            float:left;
        }
        .auto-style2 {
            width: 221px;
        }
        .auto-style3 {
            width: 478px;
        }
    </style>
</head>
<body>
    <div class="w3-container w3-mobile">
 	 <h3 style="float:left">Customer Registration</h3>	 
	</div><br/>
	<div class="w3-border w3-container" style="max-width:35%;margin-left:20px"><br/>
        <form class="w3-container" id="form1" runat="server" style="margin-bottom:8px">
        <div class="auto-style3">
            <table>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label2" runat="server" Text="Company name"></asp:Label></td>
                    <td> <input id="name" name="name" type="text" placeholder="company name" class="auto-style2" required="required" itemid="name" runat="server"/></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label3" runat="server" Text="Person in-charge"></asp:Label></td>
                    <td> <input id="person" name="person" type="text" placeholder="person in-charge" class="auto-style2" required="required" itemid="person" runat="server"/></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label4" runat="server" Text="Phone No."></asp:Label></td>
                    <td> <input id="phone" name="phone" type="text" placeholder="ex: +60123456789" class="auto-style2" required="required" itemid="phone" runat="server"/></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label5" runat="server" Text="Email"></asp:Label></td>
                    <td> <input id="email" name="email" type="text" placeholder="email" class="auto-style2" required="required" itemid="email" runat="server" /></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label6" runat="server" Text="Company ID."></asp:Label></td>
                    <td> <input id="cid" name="cid" type="text" placeholder="id for login" class="auto-style2" required="required" itemid="cid" runat="server"/></td>
                </tr>
                <tr>
                    <td class="auto-style1"><asp:Label ID="Label7" runat="server" Text="Password"></asp:Label></td>
                    <td> <input id="password" name="password" type="password" placeholder="password for login" class="auto-style2" required="required" itemid="password" runat="server"/></td>
                </tr>
           </table>
           <br/><br/>
           
            <asp:Button ID="Submit1" runat="server" Text="Register" Width="80px" OnClick="Submit1_Click"/>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ddacdatabaseConnectionString %>" SelectCommand="SELECT * FROM [Customer]"></asp:SqlDataSource>
        </div>
    </form>
        </div></div></div>
</body>
</html>

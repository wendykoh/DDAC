<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="DDAC.Login" %>

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
    <title>Login</title>
</head>
<body class="w3-content" style="max-width:300px">
<div class="w3-border" style="margin-top:50px">
	<div class="w3-container" style="text-align:center">
 	 <h3 style="font-weight:bold">Login</h3>
	</div><br/>

    <form class="w3-container" runat="server" style="margin-bottom:8px">
  	 <label  style="font-size:small"><b>Username:</b></label>
 	 <input class="w3-input w3-border w3-light-grey" type="text" id="username" name="username" required="required" runat="server"/>

  	<label  style="font-size:small"><b>Password:</b></label>
  	<input class="w3-input w3-border w3-light-grey" type="password" id="password" name="password" required="required" runat="server"/>
	
 	 <p> <a href="Customer Registration.aspx" style="color:blue"><label  style="font-size:smaller;margin-top:30px;color:blue">Customer Registration</label><a/>
         <asp:Button id= "Login1" style="float:right;width:80px" runat="server" Text="Login" OnClick="Login1_Click"></asp:Button>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ddacdatabaseConnectionString %>" SelectCommand="SELECT * FROM [Customer]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ddacdatabaseConnectionString %>" SelectCommand="SELECT * FROM [Admin]"></asp:SqlDataSource>
        </p>
	</form>
</div>
</body>
</html>

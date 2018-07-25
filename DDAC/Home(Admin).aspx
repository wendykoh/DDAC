<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home(Admin).aspx.cs" Inherits="DDAC.Home_Admin_" %>

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

        function search() {
                var x1 = document.getElementById("searchvia");
                var x = x1.options[x1.selectedIndex].text;
                if (x == "Shipping ID") {
                    searchid();
                } else if (x == "Status") {
                    searchstatus();
                } 
            }

            function searchid() {
                var input, filter, table, tr, td, i;
                input = document.getElementById("searchinput");
                filter = input.value.toUpperCase();
                table = document.getElementById("table1");
                tr = table.getElementsByTagName("tr");
                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[0];
                    if (td) {
                        if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            tr[i].style.display = "none";
                        }
                    }
                }
            }

            function searchstatus() {
                var input, filter, table, tr, td, i;
                input = document.getElementById("searchinput");
                filter = input.value.toUpperCase();
                table = document.getElementById("table1");
                tr = table.getElementsByTagName("tr");
                for (i = 0; i < tr.length; i++) {
                    td = tr[i].getElementsByTagName("td")[5];
                    if (td) {
                        if (td.innerHTML.toUpperCase().indexOf(filter) > -1) {
                            tr[i].style.display = "";
                        } else {
                            tr[i].style.display = "none";
                        }
                    }
                }
            }

    </script>
    <title>Home</title>
</head>
<body>
    <div class=" w3-container w3-padding w3-hide-small" style="background-color:#ccccff;width:100%">
            <div class="w3-container" id="logo1" style="max-width:40%;max-height:200px">
                <a href="Home(Admin).aspx" title="Home" style="font-size:large">
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
                                   
                <div class="w3-row-padding w3-mobile" style="width:80%">
                    <div class=" w3-quarter">
                        <label>Search via</label><br/>
                        <select class="w3-select w3-border" id="searchvia" style="margin-top:7px;margin-bottom:7px" required="required">	      
                            <option value="1">Shipping ID</option>
                            <option value="2">Status</option>            
                        </select>
                    </div>
                    <div class="w3-quarter">
                        <label></label><br/>
                        <input class="w3-input w3-border" type="text" id="searchinput" style="margin-top:7px;margin-bottom:7px;max-width:100%"  placeholder="Search value" onkeyup="search()"/>
                    </div>
                    <div class="w3-quarter">
                        <label></label><br/>
                        
                    </div>	
                    <div class="w3-quarter">
                        <label></label><br/><br/>
                        <asp:Button ID="add" Text="Check Schedule" runat="server" style="float:right;margin-right:15px" OnClick="add_Click"/>
                        </div>
                </div>
                <div class="w3-responsive w3-container">
                <asp:PlaceHolder id="PlaceHolder1" runat="server"/>
                </div>
                <br/><br/>
                </div></div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="LaunchLocation, App_Web_v0kkuoan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href=default.css type="text/css" rel="stylesheet" />
        <style type="text/css">
    .btn
    {
    background-color: GREEN;
	font-weight: bold;
	font-size: 8pt;
	width: 60pt;
	color: white;
	height: 16pt;
	text-align: center;
	cursor: pointer;
	z-index: 1000;
}
</style>
<link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
    <table>
    <tr>
                    
                    <td  style="background-image: url(images/green-noise-background.png); width: 971px">
                        <ol id="toc">
                            <li id="miLoc" runat="server">
                                <asp:LinkButton ID="lnkLoc"  runat="server" TabIndex = "4"  Text="Location Master" OnClick="lnkLoc_Click" /></li>
                            <li id="miCustLoc" runat="server">
                                <asp:LinkButton ID="lnkCustLoc" TabIndex = "4" runat="server"  Text="Customer Locations" OnClick="lnkCustLoc_Click" /></li>
                           <li id="miLocTimeZone" runat="server">
                                <asp:LinkButton ID="lnkTimeZone" TabIndex = "4" runat="server"  Text="Time Zone" OnClick="lnkTimeZone_Click"  /></li>
                           </ol>
                           <div class="content" id="tabLoc" runat="server">
     <div>
     <div class="dptitle">Location Master</div>
    <table align="center" width="35%" class="bordertable">
       <tr><td ><asp:Label ID="Label1" runat="server" Text="Location Name" ></asp:Label></td>
       <td>
           <asp:DropDownList ID="Droplocname" DataTextField="Location_name" DataValueField="location_id" runat="server">
           </asp:DropDownList></td><td>
           <%--<asp:TextBox ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>--%>
            <asp:Button ID="btnsearch1" runat="server" CssClass="btn" Text="Search" OnClick="btnsearch1_Click"  /></td>
           </tr>
    </table>&nbsp;
    </div>
    <div>
    <table align="center" width="45%" class="bordertable" id="TABLE1">
    <tr><td>
        <asp:Label ID="Label2" runat="server" Text="Location Name" ></asp:Label></td>
        <td><asp:textbox ID="txtLocname"  runat="server" Width="161px"></asp:textbox></td></tr>
        
      
        <tr><td colspan="4" align="center">
        <asp:Button ID="btncreate1" CssClass="btn" runat="server" Text="Create" OnClick="btncreate1_Click" />&nbsp;
        <asp:Button ID="btncancel1" runat="server" CssClass="btn" Text="Cancel" OnClick="btncancel1_Click"  />&nbsp;
        </td></tr>
        <tr><td colspan="4" align="center" style="height: 18px"><asp:Label ID="lblresults" runat="server" CssClass="error"  Font-Bold=true></asp:Label></td></tr>
        </table>
    </div>
    </div>
    <div class="content" id="tabCustLoc" runat="server">
    <div class="dptitle">Customer Locations</div>
    <table align="center" width="75%" class="bordertable">
    <tr>
    <td style="width: 65px">
    Customer
    </td>
    <td style="width: 179px">
        <asp:DropDownList ID="DropCust" runat="server" Width="236px" AutoPostBack="True" OnSelectedIndexChanged="DropCust_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
    <td style="width: 36px">
    Location
    </td>
    <td style="width: 95px">
        <asp:DropDownList ID="DropLoc" runat="server" Width="154px" >
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td colspan="4" align="center">
        <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="btn" OnClick="btnadd_Click"/>
    </td>
   
    </tr>
    <tr>
     <td colspan="4" align="center">
    <asp:Label ID="CustLoc" runat="server" CssClass="error"  Font-Bold=true></asp:Label>
    </td>
    </tr>
    <tr>
    <td colspan="4" align="center">
                      <asp:GridView ID="GvCust" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                      CssClass="lightbackground" width="60%"  >
                          <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
                          <AlternatingRowStyle BackColor="#F2F2F2" />
                          <Columns>
                                
                                <asp:TemplateField SortExpression="Loc_Name" HeaderText="Location Name" >
                                    <ItemTemplate>
                                       <asp:Label ID="lblgvLocname" runat="server" Text='<%# Eval("Location_name") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                  <asp:TemplateField SortExpression="Loc_Time" HeaderText="Time Zone" >
                                      <ItemTemplate>
                                         <asp:Label ID="lblLocid" runat="server" Text='<%# Eval("Time_Zone") %>' ></asp:Label>
                                      </ItemTemplate>
                                </asp:TemplateField>
                          </Columns>
                       </asp:GridView>
    </td>
    </tr>
    </table>
    </div>
    <div class="content" id="tabLocTimeZone" runat="server">
        <div class="dptitle">TIme Zone</div>
    <table align="center" width="45%" class="bordertable" id="timezone">
    <tr><td>
        <asp:Label ID="Label4" runat="server" Text="Location Name" ></asp:Label></td>
        <td><asp:DropDownList ID="dropLoczone"  runat="server" Width="161px"></asp:DropDownList></td></tr>
        
        <tr><td><asp:Label ID="Label5" runat="server" Text="Time Zone"></asp:Label></td>
        <td>
            <asp:DropDownList ID="DropTimeZone" runat="server" Width="80px">
             <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="IST">IST</asp:ListItem>
                                        <asp:ListItem Value="PST">PST</asp:ListItem>
                                        <asp:ListItem Value="GMT">GMT</asp:ListItem>
                                        <asp:ListItem Value="CET">CET</asp:ListItem>
                                        <asp:ListItem Value="CST">CST</asp:ListItem>
                                        <asp:ListItem Value="HKT">HKT</asp:ListItem>
                                        <asp:ListItem Value="EST">EST</asp:ListItem>
            </asp:DropDownList>
        </td>
        </tr>
        <tr><td>Time Difference IST</td><td>Hrs.  
            <asp:TextBox ID="txtHrs" runat="server" Width="47px"></asp:TextBox> Mins.  
            <asp:TextBox ID="txtMins" runat="server" Width="47px"></asp:TextBox></td></tr>
        <tr><td colspan="4" align="center">
        <asp:Button ID="createzone" CssClass="btn" runat="server" Text="Create" OnClick="createzone_Click"  />&nbsp;
        <asp:Button ID="clearzone" runat="server" CssClass="btn" Text="Cancel" OnClick="clearzone_Click"  />&nbsp;
        </td></tr>
        <tr><td colspan="4" align="center" style="height: 18px"><asp:Label ID="Label6" runat="server" CssClass="error"  Font-Bold=true></asp:Label></td></tr>
        </table>
    </div>
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>

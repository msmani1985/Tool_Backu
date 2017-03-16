<%@ page language="C#" autoeventwireup="true" inherits="Teamcreation, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

   
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Team Creation</title>
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
    <style type="text/css">
    .tab
{
	z-index: 1000;
	font-size: 8pt
}
  <style type="text/css">
  .error
{
	color: Red;
	font-weight: bold;
	font-size: 10pt;
	text-align: center;
}
 </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">TEAM CREATION</div>
    <div>
    <table align="center" width="40%" class="bordertable">
       <tr><td ><asp:Label ID="Label1" runat="server" Text="Team Name" ></asp:Label></td>
       <td>
           <asp:DropDownList ID="Dropsearch" DataTextField ="employee_team_name" DataValueField="employee_id" runat="server">
           </asp:DropDownList>
           <asp:Button ID="btnsearch" runat="server" CssClass="btn" Text="Search" OnClick="btnsearch_Click" /></td>
           </tr>
    </table>
    </div>
    <br />
    <div >
    
    <table align="center" width="80%" class="bordertable">
    <tr><td colspan="4" align="center"><font color="red">* mandatory field</font></td></tr>
    <tr><td style="width: 161px">
        <asp:Label ID="Label2" runat="server" Text="Team Name" ></asp:Label><font color="red">*</font></td>
        <td><asp:textbox ID="txttname"  runat="server"></asp:textbox></td>
         <td ><asp:Label ID="Label5" runat="server" Text="Team Leads"></asp:Label></td>
        <td rowspan="5" valign="top">
            <%--<asp:DropDownList ID="Dropgroupby" DataTextField="fname" DataValueField="employee_id"  runat="server" >
            </asp:DropDownList>--%>
            <asp:ListBox ID="lb_groupby_team" DataTextField="fname" DataValueField="employee_id" runat="server" SelectionMode="Multiple" Height="300px" Width="200px"></asp:ListBox>
        </td>
        </tr>
       <%-- <tr><td ><asp:Label ID="Label4" runat="server" Text="Lead"></asp:Label></td>
        <td><asp:DropDownList ID="Droplead" DataTextField="fname" DataValueField="employee_id" runat="server"  >
        </asp:DropDownList></td>
        <td>&nbsp;</td>
       </tr>--%>
       <tr><td>
           <asp:Label ID="Label4" runat="server" Text="Location"></asp:Label><font color="red">*</font></td>
           <td><asp:DropDownList ID="dd_location" runat="server" DataTextField="location_name" DataValueField="location_id"></asp:DropDownList></td>
           </tr>
        <tr><td ><asp:Label ID="Label6" runat="server" Text="Order Index" ></asp:Label><font color="red">*</font>
        </td><td><asp:TextBox ID="txtindexorder" runat="server"></asp:TextBox></td>
        <td>&nbsp;</td></tr>
        <tr><td style="height: 53px"><asp:Label ID="Label3" runat="server" Text="Department"></asp:Label><font color="red">*</font></td>
        <td style="height: 53px"><asp:DropDownList ID="dropdepart"  DataTextField="department_name" DataValueField="department_id"  runat="server" >
        </asp:DropDownList></td><td style="height: 53px">&nbsp;</td></tr>
        <tr><td colspan="3" align="center"><asp:Button ID="btncreate" CssClass="btn" runat="server" Text="Create" OnClick="btncreate_Click"/>&nbsp;
        <asp:Button ID="btncancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btncancel_Click" />&nbsp;
        <asp:Button ID="btnupdate" runat="server" CssClass="btn" Text="Update" OnClick="btnupdate_Click" /></td></tr>
        <tr><td colspan="4" align="center"><asp:Label ID="lblresults" runat="server" CssClass="error"  Font-Bold=true></asp:Label></td></tr>
        </table>
        <table>
        
    </table>
           </div>
        
        
    </form>
          
</body>
</html>


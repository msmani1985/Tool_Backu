<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TeamBuilder.aspx.cs" Inherits="TeamBuilder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Team Builder</title>
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
 <style type="text/css">
 .lblhead
 {
 color:green;
 font-weight:bold;
 } 
 </style>
</head>
<body>
    <form id="form1" runat="server">
     <div class="dptitle">TEAM BUILDER</div>
     
    <div>
        <table align="center" class="bordertable" style="width:10%; height: 313px">
         <tr>
         <td style="width: 171px">
         <asp:Label ID="lblTeamname" runat="server" CssClass="lblhead" Text="Team Name">
         </asp:Label></td>
         <td  colspan="2" style="width: 172px">
         <asp:DropDownList ID="ddl_teamname"  DataTextField="employee_team_name" DataValueField="team_owner_id" runat="server"  AutoPostBack="true" OnSelectedIndexChanged="ddl_teamname_SelectedIndexChanged" >
         </asp:DropDownList></td>
         </tr>
         
         <tr>
         <td style="width: 164px"> 
         <asp:Label ID="lbllisiofemployees" runat="server"  Text="List Of Employees">
         </asp:Label></td>
                
         <td>
         </td> 
         <td>
         <asp:Label ID="lblteammembers" runat="server" Text="Team Members"></asp:Label>
         </td>
         </tr>
         
         <tr>
         <td align="left" style="width: 171px">
         <asp:ListBox ID="List_employee" DataTextField="fname" DataValueField="employee_id" SelectionMode="Multiple" runat="server" Height="257px" Width="161px" >
         </asp:ListBox>
         </td>
         
         <td  align="center" style="width:300">
         <asp:ImageButton ID="imgbtnadd" ImageUrl="images\tools\arrow1.jpg" runat="server" OnClick="imgbtnadd_Click" /><br />
             &nbsp;</td>
         <td align="right" style="width: 164px">
         <asp:ListBox ID="lb_teammembers" runat="server" SelectionMode="Multiple" Height="257px" Width="161px">
         </asp:ListBox>
         </td>
         </tr>
                     
         <tr>
         <td colspan="4" align="center" style="height: 23px">
         <asp:Button ID="btnSave" runat="server" CssClass="btn" Text="save" OnClick="btnSave_Click" />&nbsp;
         <asp:Button ID="btncancel" runat="server" CssClass="btn" Text="Cancel" OnClick="btncancel_Click"/></td>
         </tr>
         <tr>
         <tr><td colspan="4" align="center">
         <asp:Label ID="lblresult" CssClass="error" runat="server">
         </asp:Label></td></tr>
                   
     </table>
     </div>
     </form>
</body>
</html>
 
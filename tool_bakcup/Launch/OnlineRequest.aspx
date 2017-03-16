<%@ page language="C#" autoeventwireup="true" inherits="OnlineRequest, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
   <title>ONLINE REQUEST</title>
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
    <style type="text/css">
    .tab
{
	z-index: 1000;
	font-size: 8pt
}
</style>
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
    <div>
    <div class="dptitle">Online Request</div>
    <table align="center" class="bordertable" style="width: 266px; height: 327px" id="TABLE1">
    <tr>
    <td> <asp:Label ID="Label2" runat="server" CssClass="lblhead" Text="Team Name"></asp:Label></td>
    <td>
        <asp:DropDownList ID="ddl_teamname" DataTextField="employee_team_name" DataValueField="employee_team_id" runat="server">
        </asp:DropDownList>
    </td>
    <tr>
    <td> <asp:Label ID="Label3" runat="server" CssClass="lblhead" Text="Task"></asp:Label>
    </td>
    <td> <asp:DropDownList ID="ddl_task" DataTextField="task_name" DataValueField="task_id" runat="server">
         </asp:DropDownList>
    </td></tr>
    <tr>
    <td>
     <asp:Label ID="Label4" runat="server" CssClass="lblhead" Text="Priority"></asp:Label></td>
     <td><asp:dropdownlist ID="ddl_priority" DataTextField="priority_name"  DataValueField="priority_id"  runat="server">
     </asp:dropdownlist>
     </td>
     </tr>
      <tr><td><asp:Label ID="lbl_request_title" CssClass="lblhead" runat="server" Text="Title"></asp:Label></td>
     <td><asp:TextBox ID="txt_title" runat="server" Width="360px"></asp:TextBox></td>
     </tr>
     <tr><td align="left">
     <asp:Label ID="Label1" runat="server" CssClass="lblhead" Text="Description"></asp:Label></td>
     <td  align="left">
     <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Width="362px" Height="200px"></asp:TextBox> </td></tr>
     <tr>
     <td colspan="8" align="center">
     <asp:Button ID="btnrequest" runat="server" CssClass="btn" Text="Send" OnClick="btnrequest_Click" />
     <asp:Button ID="btnclear" runat="server" CssClass="btn" Text="Clear" OnClick="btnclear_Click"/></td><td>
     </td>
     </tr>
     <tr>
     <td colspan="7" align="center">
     <asp:Label ID="lblalert" runat="server" CssClass="error"></asp:Label>
     </td>
     </tr>
     </table>
    </div>
    </form>
</body>
</html>


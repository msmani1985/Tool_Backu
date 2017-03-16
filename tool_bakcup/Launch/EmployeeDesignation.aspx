<%@ page language="C#" autoeventwireup="true" inherits="EmployeeDesignation, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Employee Designation</title>
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
    <div class="dptitle">Employee Designation</div>
    <div>
    <table align="center" width="35%" class="bordertable">
       <tr><td ><asp:Label ID="Label1" runat="server" Text="Designation Name" ></asp:Label></td>
       <td>
           <asp:DropDownList ID="Dropdesigname" DataTextField="designation_name" DataValueField="designation_id" runat="server">
           </asp:DropDownList></td><td>
           <%--<asp:TextBox ID="txtsearch" runat="server" OnTextChanged="txtsearch_TextChanged"></asp:TextBox>--%>
            <asp:Button ID="btnsearch1" runat="server" CssClass="btn" Text="Search" OnClick="btnsearch1_Click"  /></td>
           </tr>
    </table>&nbsp;
    </div>
    <div>
    <table align="center" width="45%" class="bordertable" id="TABLE1">
    <tr><td>
        <asp:Label ID="Label2" runat="server" Text="Designation Name" ></asp:Label></td>
        <td><asp:textbox ID="txtdesigname"  runat="server"></asp:textbox></td></tr>
        
        <tr><td><asp:Label ID="Label3" runat="server" Text="Time Sheet"></asp:Label></td>
        <td>
            <asp:DropDownList ID="dd_timesheet" runat="server">
            <asp:ListItem Text="--select--" Value="2"></asp:ListItem>
            <asp:ListItem Text="True" Value="0"></asp:ListItem>
            <asp:ListItem Text="False" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </td>
        </tr>
        <tr><td colspan="4" align="center">
        <asp:Button ID="btncreate1" CssClass="btn" runat="server" Text="Create" OnClick="btncreate1_Click" />&nbsp;
        <asp:Button ID="btncancel1" runat="server" CssClass="btn" Text="Cancel" OnClick="btncancel1_Click" />&nbsp;
        <asp:Button ID="btnupdate1" runat="server" CssClass="btn" Text="Update" OnClick="btnupdate1_Click"/></td></tr>
        <tr><td colspan="4" align="center"><asp:Label ID="lblresults" runat="server" CssClass="error"  Font-Bold=true></asp:Label></td></tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="tempactions, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">

<title>Temp Actions</title>

<link href="default.css" type="text/css" rel="stylesheet" />

</head>

<body>

<form id="form1" runat="server">

<div id="divMovePDF" runat="server" style="display:none" >

<div class="dptitle">Move PDFs</div>

<div>

<table align="center" cellpadding="1" cellspacing="1" class="bordertable" width="500">

<tr><td>Job Number (4 digit barcode)<br /><span style="color:red">Don't use ManuscriptID</span></td>

<td><asp:TextBox ID="txtjobid" runat="server" Text=""></asp:TextBox> </td>

<td>Job Type</td>

<td><asp:DropDownList ID="ddljobtypeid" runat="server" >

<asp:ListItem Value="5" Text ="Article" Selected="True" ></asp:ListItem>

<asp:ListItem Value="6" Text="Issue"></asp:ListItem>

</asp:DropDownList>

</td>


</tr>

<tr><td colspan="4" align="center" ><asp:Button CssClass="dpbutton" ID="btnsubmit" Text="submit" runat="server" OnClick="btnsubmit_Click" /> </td></tr>

</table>

</div>

</div>

<div id="divMessage" align="center" class="errorMsg" runat="server" ></div>

</form>

</body>

</html>



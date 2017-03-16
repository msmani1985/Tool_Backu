<%@ page language="C#" autoeventwireup="true" inherits="DisplayInfo, App_Web_xuje0h3i" %>

 <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />    
    <style type="text/css">
        .auto-style1 {
            height: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="250" cellpadding="1" cellspacing="1" border="0" align="center">
<tr><td colspan="2" align="center" style="background-color: #006600"><font color="white">  <strong>Contact Details </strong> </font></td></tr>
<tr><td colspan="2" align="left" height="1px" bgcolor="orange">
    <asp:Label ID="lblname" runat="server" Font-Bold="True"></asp:Label>
    </td></tr>
<tr><td> 
    <asp:Label ID="lbldesignation" runat="server"></asp:Label>
    </td></tr>
<tr><td> </td></tr>
<tr><td>&nbsp;</td></tr>
<tr><td><b>Phone:</b> 
    <asp:Label ID="lblphone" runat="server"></asp:Label>
    </td></tr>
<tr><td><b>Fax:&nbsp;</b>
    <asp:Label ID="lblfax" runat="server"></asp:Label>
    </td></tr>
<tr><td><b>Email: </b>
    <asp:Label ID="lblemail1" runat="server"></asp:Label>
    <asp:Label ID="lblemail2" runat="server"></asp:Label>
    </td></tr>
<tr><td class="auto-style1"> 
    <asp:Label ID="lblurl" runat="server"></asp:Label>
    </td></tr>
<tr><td>&nbsp;</td></tr>
<tr><td><b>Address:</b>
    <asp:Label ID="lbladdress" runat="server"></asp:Label>
    </td></tr>
<tr><td> 
    <asp:Label ID="lblcity" runat="server"></asp:Label>
    </td></tr>
<tr><td> 
    <asp:Label ID="lblstate" runat="server"></asp:Label>
    </td></tr>
<tr><td> 
    <asp:Label ID="lblpin" runat="server"></asp:Label>
    </td></tr>
<tr><td> 
    <asp:Label ID="lblcountry" runat="server"></asp:Label>
    </td></tr>
<tr><td colspan="2" align="center" height="1px" bgcolor="orange">
     
    </td></tr>
<tr><td colspan="2" align="center"><input type="button" value="Close" onclick="javascript: window.close();" /></td></tr>
</table>
    </div>
    </form>
 
</body>
</html>

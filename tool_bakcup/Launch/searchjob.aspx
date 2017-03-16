<%@ page language="C#" autoeventwireup="true" inherits="searchjob, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Search Job Page</title>
    <link href="default.css" rel="stylesheet"  type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Search Job</div>
    <div>
        <table align="center" width="60%" class="bordertable">
            <tr><td align="right">Job Type</td>
                <td><asp:DropDownList ID="ddljobtype" runat="server"></asp:DropDownList></td>
                <td><asp:CheckBox ID="cbxcjobs" Text="Show completed jobs" runat="server" /></td>
            </tr>
            <tr>
                <td align="right" >Cutomer Reference</td>
                <td><asp:TextBox ID="Txtjob" runat="server" Text=""></asp:TextBox></td>
                <td><asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="dpbutton" OnClick="btnsearch_Click" /></td>
            </tr>
        </table>    
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="General_PopUp, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="default.css" type="text/css" rel="stylesheet" />
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    <table  class="bordertable" align="center" cellpadding="2" cellspacing="2" >
    <tr>
    <td>
        <asp:Label ID="Label1" Visible="false" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:DropDownList  Visible="false" ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label2" Visible="false" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label3" Visible="false" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:TextBox  ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="Label4" Visible="false" runat="server" Text="Label"></asp:Label>
    </td>
    <td>
        <asp:DropDownList ID="DropDownList2" Visible="false" runat="server">
            <asp:ListItem>Large</asp:ListItem>
            <asp:ListItem>Small</asp:ListItem>
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td colspan="2" align="center">
        <asp:Button ID="Create" CssClass="dpbutton" runat="server" Text="Create" OnClick="Create_Click" />
        <asp:Button ID="Update" CssClass="dpbutton" runat="server" Text="Update" OnClick="Update_Click" />
        <asp:Button ID="Delete" CssClass="dpbutton" runat="server" Text="Delete" OnClick="Delete_Click" />
    </td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>

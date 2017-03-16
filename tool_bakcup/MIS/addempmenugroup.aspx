<%@ page language="C#" autoeventwireup="true" CodeFile="addempmenugroup.aspx.cs" inherits="addempmenugroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="titlediv" class="dptitle">Employee Menu Groups</div>
    <div>
        <table align="center">
            <tr>
                <td align="right" style="width: 65px; height: 138px;">
                    <asp:Label ID="Label1" runat="server" Text="Employees"></asp:Label>
                </td>
                <td align="left" style="height: 138px">
                    <asp:ListBox ID="EmployeeList" DataTextField="Empname" DataValueField="employee_id" runat="server" Rows="15" AutoPostBack="True" OnSelectedIndexChanged="EmployeeList_SelectedIndexChanged"></asp:ListBox>
                </td>
                <td align="right" style="width: 65px; height: 138px;">
                    <asp:Label ID="Label2" runat="server" Text="Groups"></asp:Label>
                </td>
                <td align="left" style="height: 138px">
                    <asp:ListBox ID="groupsList" DataTextField="menu_group_name" DataValueField="menu_group_id" runat="server" Rows="15" SelectionMode="Multiple"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="BtnAdd" runat="server" Text="Add" CssClass="dpbutton" OnClick="BtnAdd_Click" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

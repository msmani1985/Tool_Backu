<%@ page language="C#" autoeventwireup="true" inherits="JobLogReport, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datapage - Job Log Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .Ucase
    {
        text-transform:uppercase;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="invreport" class="dptitle">
            Job Log Report</div>
        <br />
        <table style="width: 559px" align="center" class="bordertable">
            <tr>
                <td>
                    Type:</td>
                <td style="width: 132px">
                    <asp:DropDownList ID="drpCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCategory_SelectedIndexChanged" Width="92px">
                        <asp:ListItem Value="0">Journal</asp:ListItem>
                        <asp:ListItem Value="1">Book</asp:ListItem>
                        <asp:ListItem Value="2">Project</asp:ListItem>
                    </asp:DropDownList></td>
                <td>
                    Job Code:</td>
                <td align="left" valign="top">
                    <asp:TextBox ID="txtJobcode" CssClass="Ucase" runat="server"></asp:TextBox>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Category:</td>
                <td style="width: 132px">
                    <asp:RadioButtonList ID="rblstJournal" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="A" Selected="True">Article</asp:ListItem>
                        <asp:ListItem Value="I">Issue</asp:ListItem>
                    </asp:RadioButtonList><asp:RadioButtonList ID="rblstBook" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="B" Selected="True">Book</asp:ListItem>
                        <asp:ListItem Value="C">Chapter</asp:ListItem>
                    </asp:RadioButtonList><asp:RadioButtonList ID="rblstProject" runat="server" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Value="P" Selected="True">Project</asp:ListItem>
                        <asp:ListItem Value="M">Module</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                        Width="61px" CssClass="dpbutton" /></td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <div id="divError" runat="server" style="color: red">
                    </div>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

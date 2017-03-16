<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductivityReport.aspx.cs" Inherits="ProductivityReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Datapage - Productivity Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="invreport" class="dptitle">
                    Productivity Report</div>
        <br />
        <table style="width: 718px" align="center" class="bordertable">
            <tr>
                <td>
                    Type:</td>
                <td style="width: 182px">
                    <asp:RadioButtonList ID="rblstType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rblstType_SelectedIndexChanged"
                        RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0">
                        <asp:ListItem Selected="True" Value="0">Operator</asp:ListItem>
                        <asp:ListItem Value="1">Team</asp:ListItem>                        
                    </asp:RadioButtonList></td>
                <td style="width: 115px">
                    <asp:Label ID="lblType" runat="server" Font-Bold="False"></asp:Label></td>
                    <td style="width: 249px">
                        <asp:DropDownList ID="drpEmployee" runat="server">
                        </asp:DropDownList><asp:DropDownList ID="drpDept" runat="server">
                        <asp:ListItem Value="0">Journal</asp:ListItem>
                        <asp:ListItem Value="1">Books</asp:ListItem>
                        <asp:ListItem Value="2">Projects</asp:ListItem>
                    </asp:DropDownList><asp:DropDownList ID="drpTeam" runat="server">
                    </asp:DropDownList></td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date:</td>
                <td style="width: 182px">
                    <strong>
                    <asp:TextBox ID="txtStartdate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></strong></td>
                <td style="width: 115px">
                    End Date:</td>
                <td style="width: 249px">
                    <asp:TextBox ID="txtEnddate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtEnddate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="61px" OnClick="btnSubmit_Click" CssClass="dpbutton" /></td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="width: 182px">
                </td>
                <td style="width: 115px">
                </td>
                <td style="width: 249px">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                <div id="divError" style="color:Red;" runat="server"></div>
                </td>
                <td align="center" colspan="1">
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

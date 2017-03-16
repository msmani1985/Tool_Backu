<%@ page language="C#" autoeventwireup="true" inherits="announcements, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>View Announcements</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">
            News and Announcements&nbsp;</div>
        <table align="center" cellpadding="2" cellspacing="5" class="bordertable" width="620">
            <tr>
                <td colspan="6">
                    </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Title</td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:Label ID="lblMessageTitle" runat="server" Width="420px" Font-Bold="True" ForeColor="DodgerBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Posted By</td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:Label ID="lblPostedby" runat="server" Width="420px" Font-Bold="True" ForeColor="DodgerBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Posted To</td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:Label ID="lblPostedto" runat="server" Width="420px" Font-Bold="True" ForeColor="DodgerBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px" valign="top">
                    Message<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width: 12px">
                    :<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td colspan="4" valign="top">
                    <asp:Label ID="lblMessage" runat="server" Height="160px" Width="420px" Font-Bold="True" ForeColor="DodgerBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Expires On&nbsp;</td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:Label ID="lblExpireson" runat="server" Width="420px" Font-Bold="True" ForeColor="DodgerBlue"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px">
                </td>
                <td style="width: 12px">
                </td>
                <td colspan="4">
                    </td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="Button1" runat="server" CssClass="dpbutton" Text="Okay" OnClick="Button1_Click" />&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

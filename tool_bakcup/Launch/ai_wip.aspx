<%@ page language="C#" autoeventwireup="true" inherits="ai_wip, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Datapage - Daily Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="invreport" class="dptitle">
                Daily Report</div>
            <br />
            <table style="width: 718px" align="center" class="bordertable">
                <tr>
                    <td>Customer
                    </td>
                    <td>
                        <asp:DropDownList ID="drpCustomer" runat="server">
                        </asp:DropDownList>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                            Text="Submit" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="wip_projection, App_Web_w6b3pav3" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job Status Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
       Job Status Report
    </div>
    <div>
        <table style="width: 718px" align="center" class="bordertable">
        <tr><td>customer</td>
        <td><asp:DropDownList ID="ddl_customer" runat="server" DataValueField="CUSTNO" DataTextField="CUSTNAME"></asp:DropDownList></td>
        <td><asp:Button ID="btn_submit" runat="server" Text="Submit" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
        </tr>
        </table>
    </div>
    <br />
    <div>
        <CR:CrystalReportViewer ID="CRV_wipprojection" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="false" DisplayGroupTree="false" 
         HasExportButton="true" HasGotoPageButton="false" HasSearchButton="false" HasToggleGroupTreeButton="false" HasViewList="false"
           />
    </div>
    </form>
</body>
</html>

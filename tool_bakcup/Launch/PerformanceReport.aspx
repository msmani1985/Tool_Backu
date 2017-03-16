<%@ page language="C#" autoeventwireup="true" inherits="PerformanceReport, App_Web_lruasnqi" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Performance Report
    </div>
    <div>
        <table width="450px" align="center" class="bordertable">
        <tr><td>Job Type</td><td><asp:DropDownList ID="dd_jobtype" runat="server">
        <asp:ListItem Text="Journal" Value="1"></asp:ListItem>
<%--        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
        <asp:ListItem Text="Project" Value="3"></asp:ListItem>--%>
        </asp:DropDownList></td>
        <td>Month</td><td><asp:DropDownList ID="dd_monthlist" runat="server">
        <asp:ListItem Text="Jan" Value="1"></asp:ListItem>
        <asp:ListItem Text="Feb" Value="2"></asp:ListItem>
        <asp:ListItem Text="March" Value="3"></asp:ListItem>
        <asp:ListItem Text="Apirl" Value="4"></asp:ListItem>
        <asp:ListItem Text="May" Value="5"></asp:ListItem>
        <asp:ListItem Text="June" Value="6"></asp:ListItem>
        <asp:ListItem Text="July" Value="7"></asp:ListItem>
        <asp:ListItem Text="Aug" Value="8"></asp:ListItem>
        <asp:ListItem Text="Sep" Value="9"></asp:ListItem>
        <asp:ListItem Text="Oct" Value="10"></asp:ListItem>
        <asp:ListItem Text="Nov" Value="11"></asp:ListItem>
        <asp:ListItem Text="Dec" Value="12"></asp:ListItem>
        </asp:DropDownList></td>
        <td>Year</td><td><asp:DropDownList ID="dd_yearlist" runat="server">
        <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
        </asp:DropDownList></td>
        <td><asp:Button ID="btn_submit" Text="Submit" runat="server" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
        </tr>
        </table>
    </div>
    <div>
    <asp:Button ID="btn_Excelexport" Text="ExcelExport" runat="server" OnClick="btn_Excelexport_Click" /><br />
    <CR:CrystalReportViewer ID="CV_performancerpt" runat="server" AutoDataBind="true" HasToggleGroupTreeButton="False"
         DisplayGroupTree="false" HasExportButton="false" EnableDatabaseLogonPrompt="false" />
    </div>
        
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="invoice_sales_analysis_preview, App_Web_olx2vwmy" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Sales Analysis - Preview</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
        <style type="text/css">
        .button
        {
	        border: 1px solid;
	        font-family:Verdana;
	        font-size: 10pt;
        }
        </style>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body bgcolor="#D4D0C8">
    <form id="form1" runat="server">
    <div>
        &nbsp;<table align="center">
            <tr>
                <td align="left" colspan="2">
                    <asp:Button ID="btnExport" CssClass="button" runat="server" Enabled="False" OnClick="btnExport_Click"
                        OnClientClick="alert('Exporting to excel...');return;" Text="Export to Excel" Width="158px" />
                    <asp:Button ID="btnExportData" CssClass="button" runat="server" Enabled="False" OnClick="btnExportData_Click"
                        OnClientClick="alert('Exporting to excel...');return;" Text="Export to Excel (Data Only)" Width="211px" />
                    <input id="Button1" class="button" type="button" value="Close[X]" onclick="javascript:self.close();" size=""; /></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="background:white;"><tr><td align="left">
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" DisplayGroupTree="False" Height="50px" Width="350px" ReuseParameterValuesOnRefresh="True" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" HasCrystalLogo="False" HasDrillUpButton="False" HasGotoPageButton="False" HasSearchButton="False" HasToggleGroupTreeButton="False" HasViewList="False" HasZoomFactorList="False" HyperlinkTarget="_blank"></cr:crystalreportviewer>
                    </td></tr></table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="invoice_sales_analysis_preview.aspx.cs" Inherits="invoice_sales_analysis_preview" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register assembly="PdfViewer" namespace="PdfViewer" tagprefix="cc1" %>

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
	        border-style: solid;
                border-color: inherit;
                border-width: 1px;
                font-family:Verdana;
	            font-size: 10pt;
                width: 101px;
            }
            .auto-style1 {
                width: 149px;
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
                    <input id="Button1" class="button" type="button" value="Close[X]" onclick="javascript:self.close();" size=""; /><asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="background:white;"><tr><td align="left" class="auto-style1">
                        <cc1:ShowPdf ID="ShowPdf1" Width="1200px" Height="600px" runat="server" />
        <cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true"  Height="50px" Width="350px" HyperlinkTarget="_blank"></cr:crystalreportviewer>
                    </td></tr><tr><td align="left" class="auto-style1">
                            &nbsp;</td></tr></table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
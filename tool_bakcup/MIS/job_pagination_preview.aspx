<%@ Page Language="C#" AutoEventWireup="true" CodeFile="job_pagination_preview.aspx.cs" Inherits="job_pagination_preview" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job Pagination Preview</title>
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
</head>
<body bgcolor="#D4D0C8">
    <form id="form1" runat="server">
    <div>
        &nbsp;<table width="100%">
            <tr>
                <td align="right" style="width: 742px">
                </td>
                <td align="left">
                    &nbsp;<input id="Button1" class="button" type="button" value="Close[X]" onclick="javascript:self.close();" style="cursor: pointer"; /></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="background:white;"><tr><td align="left">
                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
                            AutoDataBind="true" ToolPanelView="None" />
                    </td></tr></table>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="TimeSheetReport, App_Web_lruasnqi" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Time Sheet Report</title>
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style type="text/css" media="print">
        .button
        {
	        border: 1px solid;
	        font-family:Verdana;
	        font-size: 10pt;	        
        }
        .hide_print {display: none;}
    </style>
</head>
<body leftmargin="15">
    <form id="form1" runat="server">
        <div>
            <table width="800px">
                <tr>
                    <td align="right" class="hide_print">
                        <asp:Button ID="btnExport" CssClass="" runat="server" Enabled="true" 
                         Text="Export to Excel" OnClick="btnExport_Click" Width="123px" BorderWidth="1px" />
                         <input id="btnClose" class="" type="button" value="Close [x]" onclick="javascript:self.close();" style="border-right: 1px solid; border-top: 1px solid; border-left: 1px solid; border-bottom: 1px solid" />
                    </td>
                </tr>
            </table>
            <table width="100%" align="left">
                <tr>
                    <td>
                        <table width="400px">
                            <tr>
                                <td></td>
                                <td>
                                    <h3>
                                        Time Sheet Report</h3>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px" align="right">
                                    <b>Employee Name :</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px" align="right">
                                    <b>Start Date :</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblStartdate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 150px" align="right">
                                    <b>End Date :</b>
                                </td>
                                <td>
                                    <asp:Label ID="lblEnddate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divRep" align="left" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lblError" runat="server" Visible="false"></asp:Label>
            <CR:CrystalReportViewer ID="CrystalReportViewer1" CssClass="lightbackground" runat="server"
                ToolbarStyle-CssClass="dullbackground" AutoDataBind="true" DisplayGroupTree="False"
                DisplayToolbar="True" />                
        </div>
    </form>
</body>
</html>

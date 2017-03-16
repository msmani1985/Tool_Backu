<%@ page language="C#" autoeventwireup="true" inherits="ai_wip_projections, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Datapage - Articles and Issues in progress Report</title>
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
<body>
    <form id="form1" runat="server">
        <div style="padding:0 0 0 10px;">
            <table width="800px">
                <tr>
                    <td align="right" class="hide_print">
                        <asp:Button ID="btnExport" CssClass="button" runat="server" Enabled="true" Text="Export to Excel" OnClick="btnExport_Click" />
                        <input id="btnClose" type="button" onclick="javascript:self.close();" value="Close[x]"/>
                    </td>
                </tr>
            </table>
            <table width="100%" align="left">
                <tr>
                    <td>
                        <h3>
                            Articles and Issues Projection Report</h3>
                            <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divRep" align="left" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

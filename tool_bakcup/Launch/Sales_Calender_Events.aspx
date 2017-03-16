<%@ page language="C#" autoeventwireup="true" inherits="Sales_Calender_Events, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <link href="default.css" rel="stylesheet" type="text/css" /> 
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divCalender">
            <table  id="tabRunTime" runat="server" cellpadding="0" cellspacing="0" border="1"  width="820px" style="font-family: Verdana; font-weight: normal;font-size:12px; border-right: green 1px solid; border-top: green 1px solid; border-left: green 1px solid; border-bottom: green 1px solid;table-layout: fixed;">
                <tr>
                    <td colspan="7" align="center" style="height:40px;text-align:center;background-color:honeydew;" >
                        <asp:LinkButton ID="lnkPrevious"  style="color:Black;text-decoration:none;font-size:11px;font-weight:bold;"  runat="server" OnClick="lnkPrevious_Click" CssClass="link1"><< Prev </asp:LinkButton>&nbsp;
                        <asp:DropDownList ID="drpCalender" AutoPostBack="true" runat="server"  Width="200px" OnSelectedIndexChanged="drpCalender_SelectedIndexChanged">
                        <asp:ListItem Text="select" Value="0"></asp:ListItem>
                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                        </asp:DropDownList>&nbsp;
                        <asp:LinkButton ID="lnkNexr" style="color:Black;text-decoration:none;font-size:11px;font-weight:bold;" runat="server" OnClick="lnkNexr_Click" CssClass="link1"> Next >></asp:LinkButton>
                    </td>
                </tr>
                <tr  style="height:20px;font-weight:bold;font-size:11px;text-align:center;background-color:green;color:White">
                    <td style="width:120px;" bordercolor="#dddddd">
                        Sun
                    </td>
                    <td style="width:120px;"  bordercolor="#dddddd">
                        Mon
                    </td>
                    <td style="width:120px;"  bordercolor="#dddddd">
                        Tue
                    </td>
                    <td style="width:120px;" bordercolor="#dddddd">
                        Wed
                    </td>
                    <td style="width:120px;" bordercolor="#dddddd">
                        Thu
                    </td>
                    <td style="width:120px;"  bordercolor="#dddddd">
                        Fri
                    </td>
                    <td style="width:120px;"  bordercolor="#dddddd">
                        Sat
                    </td>
                </tr>                
            </table>
        </div>
    </form>
</body>
</html>

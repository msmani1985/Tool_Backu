<%@ page language="C#" autoeventwireup="true" inherits="weeklysummary, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Summary Report</title>
    <link href="default.css" type="text/css" rel="stylesheet" />        
</head>
<body>
    <form id="form1" runat="server">
    <div id="Options" runat="server">
    <div id="invreport" class="dptitle">
                    Summary Report</div>
        <br />        
    <table align="center" class="bordertable" style="width:718px;">
    <tr>
        <td>
            <asp:Label ID="lblOperator" runat="server" Text="Team : " Font-Size="10pt" ></asp:Label>
            <asp:DropDownList ID="ddlOperator" runat="server"  >
            </asp:DropDownList>
        </td>
        <td>
            <asp:Label runat="server" ID="lblStartDate" Text="Start Date :" Font-Size="10pt" ></asp:Label>        
            <asp:TextBox id="FromDate" runat="server" />
            <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=fromdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
        </td>                
        <td>
            <asp:Label id="lblEndDate" runat="server" Text="End Date :" Font-Size="10pt" ></asp:Label>
            <asp:TextBox id="ToDate" runat="server" />
            <img style="cursor:pointer; border:none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=todate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
        </td>
    </tr>
    <tr>
        <td colspan="5" align="center">
                <asp:Button ID="btnShowReport" runat="server" Text="Submit" Width="61px" OnClick="btnShowReport_Click" CssClass="dpbutton" />
        </td>
    </tr>
    </table>
    </div>


    </form>
</body>
</html>

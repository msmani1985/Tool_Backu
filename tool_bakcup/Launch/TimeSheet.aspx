<%@ page language="C#" autoeventwireup="true" inherits="TimeSheet, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TimeSheet Report</title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
    <div id="Options" runat="server">
    <div id="invreport" class="dptitle">
                    Time Sheet Report</div>
        <br />        
    <table align="center" class="bordertable" >
    <tr>
        <td colspan="3">
            <asp:Label ID="lblOperator" runat="server" Text="Employee : " Font-Size="10pt" ></asp:Label>
            <asp:DropDownList ID="ddlOperator" runat="server" Width="264px"  >
            </asp:DropDownList>
            <asp:Label runat="server" ID="lblStartDate" Text="Start Date :" Font-Size="10pt" ></asp:Label>        
            <asp:TextBox id="FromDate" runat="server" CssClass="TxtBox" MaxLength="10" Width="80px" />
            <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=FromDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" align="absMiddle" />
            <asp:Label id="lblEndDate" runat="server" Text="End Date :" Font-Size="10pt" ></asp:Label>
            <asp:TextBox id="ToDate" runat="server" CssClass="TxtBox" MaxLength="10" Width="80px" />
            <img style="cursor:pointer; border:none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=ToDate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" align="absMiddle" />
                    <asp:Button ID="btnShowReport" runat="server" Text="Submit" Width="61px" OnClick="btnShowReport_Click" CssClass="dpbutton" /></td>
    </tr>
    <tr>
        <td colspan="5" align="center">
            <!--<a href="timesheetreport.aspx" target="_blank" style="border:none" title="Click here to view the Time Sheet."></a>-->
            &nbsp;</td>
    </tr>
    </table>
    </div>
    </form>
</body>
</html>

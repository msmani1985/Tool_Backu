<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveReport.aspx.cs" Inherits="LeaveReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 735px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="TitleDiv" runat="server" class="dptitle">
            Leave Report
        </div>
        <div align="center" style="overflow:auto;WIDTH:100%; height: 60px;" >
            <table align="center" border="1">
                
                <tr>
                    <td align="center" class="auto-style1">
                       Leave Applied    (From):
                        <asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; 
                        <img style="cursor:pointer; border: none" alt="Calendar" 
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
    
                        (To):
                        <asp:TextBox ID="ToTxt" runat="server"  ></asp:TextBox>&nbsp; 
                        <img style="cursor:pointer; border: none" alt="Calendar" 
                            onclick="javascript:calendar_window=window.open('calendar.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                        <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="dpbutton" OnClick="SearchBtn_Click"   />
                    </td>
                    <td align="right" >
                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  />
                    </td>
                </tr>
                <tr>
                    
                </tr>
            </table>
        </div>
        <div id="divMessage" class="errorMsg" runat="server"></div>
        <div align="center" style="overflow:auto;WIDTH:100% " >
            <asp:GridView ID="LeaveApproveGridMgr" runat="server" AutoGenerateColumns="False"
             AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
             HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false">
            <Columns>
                <asp:BoundField DataField="sl" HeaderText="Sl.No." SortExpression="sl" />
                <asp:BoundField DataField="refno" HeaderText="EmpID" SortExpression="refno" />
                <asp:BoundField DataField="EMPNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="createdDate" HeaderText="Applied Date" SortExpression="createdDate" />
                <asp:BoundField DataField="LEAVE_IN" HeaderText="From Date" SortExpression="LEAVE_IN" />
                <asp:BoundField DataField="LEAVE_OUT" HeaderText="To Date" SortExpression="LEAVE_OUT" />
                <asp:BoundField DataField="DAYS" HeaderText="No.of Days/Mins" SortExpression="DAYS" />
                <asp:BoundField DataField="LEAVE_TYPE_NAME" HeaderText="Leave Type" SortExpression="LEAVE_TYPE_NAME" />
                <asp:BoundField DataField="DATESDETAILS" HeaderText="Dates" SortExpression="DATESDETAILS" />
                <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="comment" HeaderText="Reason" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
            </Columns>
        </asp:GridView>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" enableeventvalidation="false" inherits="signoffemp, App_Web_mjsvsc11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css">
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="invtitle" runat="server" >
        My Profile
    </div>
    <div id="employeetimesheet">
        <table width="700px" class="bordertable"  style="padding-top:20px; padding-bottom:20px;" align="center" >
            <tr>
                <td align="right">
                    <b>Employee Name:</b>  
                </td>
                <td >
                    &nbsp;<asp:Label Text="" ID="Lbl_EmpName" runat="server"></asp:Label>
                </td>
                 <td align="right" valign="middle">
                    Start Time
                </td>
                <td>
                    <asp:TextBox ID="SDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=SDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
                <td align="right">
                    End Time
                </td>
                <td>
                    <asp:TextBox ID="EDateTxt" runat="server"></asp:TextBox>&nbsp;
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=EDateTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" />
                </td>
            </tr>
            <tr>
            <td colspan="6" align="center">
                <asp:Button ID="Submit_Btn" runat="server"  CssClass="dpbutton" Text="Submit" OnClick="Submit_Btn_Click" />&nbsp;
            </td>
            </tr>
        </table>  
    </div>
    <br />
    <div id="DivError" class="errorMsg" runat="server"></div>
    <div id="DivEmp" runat="server">
        <table align="center" width="750px">
        <tr><td align="right"><asp:ImageButton ImageUrl="~/images/Excel.jpg" runat="server" ID="Img_Excel" OnClick="Img_Excel_Click" /></td></tr>
        <tr><td>
            <asp:GridView ID="GVEmployeeTask" runat="server" AutoGenerateColumns="false" CssClass="lightbackground"
             HeaderStyle-CssClass="darkbackground"  AlternatingRowStyle-CssClass="dullbackground" 
             BorderColor="Black" BorderWidth="1px"  
              width="750px"
            >
            <Columns>
                <asp:BoundField HeaderText="JobType" DataField="job_type_name" SortExpression="job_type_name" />
                <asp:BoundField HeaderText="Code" DataField="code" SortExpression="code" />
                <asp:BoundField HeaderText="Stage" DataField="job_stage_name" SortExpression="job_stage_name" />
                <asp:BoundField HeaderText="Event" DataField="task_name" SortExpression="task_name" />
                <asp:BoundField HeaderText="Start Date" DataField="start_time" SortExpression="start_time" />
                <asp:BoundField HeaderText="End Date" DataField="end_time" SortExpression="end_time" />
                <asp:BoundField HeaderText="Pages" DataField="achived_value" SortExpression="achived_value" />
                <asp:BoundField HeaderText="Comments" DataField="comments" SortExpression="comments" />
            </Columns>
            </asp:GridView>
        </td></tr>
        </table>
    </div>
    </form>
</body>
</html>

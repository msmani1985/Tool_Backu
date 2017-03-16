<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports_Apporved.aspx.cs" Inherits="Reports_Apporved" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="center" style="overflow:auto;WIDTH:100% " >
    <table>
    <tr>
    <td>
    From:<asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
    
    To:<asp:TextBox ID="ToTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
    <asp:Button ID="SearchBtn" runat="server" Text="Search" CssClass="dpbutton" OnClick="SearchBtn_Click"   />
    </td>
    </tr>
    <tr>
    <td>
        <asp:Label ID="lblempname" runat="server" Text="Employee Name: " Visible="False"></asp:Label>
        <asp:DropDownList ID="dropEmpName" runat="server" Width="177px" Visible="False" AutoPostBack="True" OnSelectedIndexChanged="dropEmpName_SelectedIndexChanged">
        </asp:DropDownList></td>
    </tr>
       
    </table></div>
    <table style="width: 878px">
     <tr>
                <td style="width:700px">
               
                </td>
                <td align="right" style="width: 61px"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></td>
            </tr>
    </table>
     <div id="TitleDiv" runat="server" class="dptitle">
        Leave Reports
    </div>

    <div id="divMessage" class="errorMsg" runat="server"></div>
    <div align="center" style="overflow:auto;WIDTH:100% " >
       &nbsp;<asp:GridView ID="LeaveApproveGridMgr" runat="server" AutoGenerateColumns="False"
        
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                <asp:BoundField DataField="sl" HeaderText="Sl.No." SortExpression="sl" />
                <asp:BoundField DataField="refno" HeaderText="EmpID" SortExpression="refno" />
                <asp:BoundField DataField="EMPNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="LEAVE_IN" HeaderText="From Date" SortExpression="LEAVE_IN" />
                <asp:BoundField DataField="LEAVE_OUT" HeaderText="To Date" SortExpression="LEAVE_OUT" />
                <asp:BoundField DataField="DAYS" HeaderText="No.of Days/Hrs" SortExpression="DAYS" />
                <asp:BoundField DataField="LEAVE_TYPE_NAME" HeaderText="Leave Type" SortExpression="LEAVE_TYPE_NAME" />
                <asp:BoundField DataField="DATESDETAILS" HeaderText="Dates" SortExpression="DATESDETAILS" />
               
                <asp:BoundField DataField="comment" HeaderText="Reason" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
<%--                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnApprove" AlternateText="Approve" ToolTip="Approve" ImageUrl="~/images/tools/yes.png" runat="server" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_LEAVE_HISTORY_ID") %>' CommandName="Approve"
                          />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnReject" ToolTip="Reject" AlternateText="Reject" ImageUrl="~/images/tools/no.png" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_LEAVE_HISTORY_ID") %>' CommandName="Reject"
                        runat="server"
                         />
                    </ItemTemplate>
                </asp:TemplateField>--%>
               <%-- <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnRedirect" ToolTip="Redirect" AlternateText="Redirect" ImageUrl="~/images/tools/arrow_right.png" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_LEAVE_HISTORY_ID") %>' CommandName="Redirect"
                        runat="server"
                        />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:DropDownList ID="RedirectToDDList" runat="server" DataTextField="empname" DataValueField="employee_id"></asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>&nbsp;&nbsp;&nbsp;

    </div>
    <div>
    </div>
              <table>
        <tr>
                <td style="width:700px; height: 20px;">
               
                </td>
                <td align="right" style="width: 170px; height: 20px;">
                    <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="ImageButton1"  ToolTip="(V) OT Report" OnClick="ImageButton1_Click"/>
                    <asp:ImageButton ImageUrl="images/icon-excel2010.gif" runat="server" ID="imgOT" ToolTip="(H) OT Report" OnClick="imgOT_Click"/>
                    <asp:ImageButton ImageUrl="images/QMS/excel.png" Width="30" runat="server" ID="imgNS" ToolTip="(H) NS Report" OnClick="imgNS_Click"/>
                </td>

            </tr>
    </table>
    <div id="Div1" runat="server" class="dptitle">
        OT Reports</div>
    <div align="center" style="overflow:auto;WIDTH:100% " >

    <div id="diverrorMsg2" class="errorMsg" runat="server"></div>
    
         <asp:GridView ID="OTApproveL2" runat="server" AutoGenerateColumns="False"
        
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
            <asp:BoundField DataField="sl" HeaderText="Sl.No." SortExpression="sl" />
                <asp:BoundField DataField="refno" HeaderText="EmpID" SortExpression="refno" />
                 <asp:BoundField DataField="EMPNAME" HeaderText="Name" SortExpression="FNAME" />
                 <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                 <asp:BoundField DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn" />
                  <asp:BoundField DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut" />
                   <asp:BoundField DataField="OT" HeaderText="Overtime" SortExpression="OT" />
                   <asp:BoundField DataField="OTBreak" HeaderText="Break" SortExpression="Break" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
<%--                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnApprove" AlternateText="Approve" ToolTip="Approve" ImageUrl="~/images/tools/yes.png" runat="server" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMP_OT_ID") %>' CommandName="Approve"
                          />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnReject" ToolTip="Reject" AlternateText="Reject" ImageUrl="~/images/tools/no.png" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMP_OT_ID") %>' CommandName="Reject"
                        runat="server"
                         />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>&nbsp;&nbsp;&nbsp;
        </div>
                  <table style="width: 885px">
        <tr>
                <td style="width:700px">
               
                </td>
                <td align="right" style="width: 77px"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="ImageButton2" OnClick="ImageButton2_Click"  /></td>
            </tr>
    </table>
        <div id="Div2" runat="server" class="dptitle">
        Shift Reports</div>
    <div align="center" style="overflow:auto;WIDTH:100% " >

    <div id="diverrorMsg3" class="errorMsg" runat="server"></div>
         &nbsp;<asp:GridView ID="ShiftApproveL2" runat="server" AutoGenerateColumns="False"
       
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
            <asp:BoundField DataField="sl" HeaderText="Sl.No." SortExpression="sl" />
                <asp:BoundField DataField="refno" HeaderText="EmpID" SortExpression="refno" />
                 <asp:BoundField DataField="EMPNAME" HeaderText="Name" SortExpression="FNAME" />
                 <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="FromDate" HeaderText="Date" SortExpression="FromDate" />
                
                <asp:BoundField DataField="PrvShift" HeaderText="ShiftFrom" SortExpression="ShiftFrom" />
                <asp:BoundField DataField="Shift" HeaderText="ShiftTo" SortExpression="ShiftTo" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
    <%--            <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnApprove" AlternateText="Approve" ToolTip="Approve" ImageUrl="~/images/tools/yes.png" runat="server" 
                         CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMP_Shift_ID") %>' CommandName="Approve"
                          />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnReject" ToolTip="Reject" AlternateText="Reject" ImageUrl="~/images/tools/no.png" 
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"EMP_Shift_ID") %>' CommandName="Reject"
                        runat="server"
                         />
                    </ItemTemplate>
                </asp:TemplateField>--%>
            </Columns>
        </asp:GridView>&nbsp;&nbsp;&nbsp;
        </div>
        <div>
                <table>
        <tr>
            <td>
               <asp:TextBox ID="txtFrom" Visible="false" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtTo" Visible="false" runat="server"></asp:TextBox>
               
            </td>
            </tr>
            <tr>
             <td>
              <asp:TextBox ID="txtSub" Visible="false" runat="server"></asp:TextBox>
             </td>
            </tr>
            <tr>
            <td>
            <asp:TextBox Visible="false" id="txtBody" cols="50" rows = "10"  style="border:solid 1px gray;Height:100px;Width:100%;" TextMode = "MultiLine" runat="server"></asp:TextBox>
            </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>

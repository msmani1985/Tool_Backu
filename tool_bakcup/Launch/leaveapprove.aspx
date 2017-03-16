<%@ page language="C#" autoeventwireup="true" inherits="leaveapprove, App_Web_cj058sae" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="overflow:auto;WIDTH:100% " >
     <table>
    <tr>
    <td>
        <asp:RadioButtonList ID="Status" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="Status_SelectedIndexChanged" Visible="False">
            <asp:ListItem Value="1">Team Leader</asp:ListItem>
            <asp:ListItem Value="2">Manager</asp:ListItem>
        </asp:RadioButtonList>
    </td>
    </tr> 
   
    </table>
    </div>
   
    <div id="TitleDiv" runat="server" class="dptitle">
        Pending Leave Approvals
    </div>
    <div id="divMessage" class="errorMsg" runat="server"></div>
    <div align="center" style="overflow:auto;WIDTH:100% " >
    
        <asp:GridView ID="LeaveApproveGrid" runat="server" AutoGenerateColumns="False"
        OnRowCommand="LeaveApprove_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="LEAVE_IN" HeaderText="From Date" SortExpression="LEAVE_IN" />
                <asp:BoundField DataField="LEAVE_OUT" HeaderText="To Date" SortExpression="LEAVE_OUT" />
                <asp:BoundField DataField="DAYS" HeaderText="No Of Days" SortExpression="DAYS" />
                <asp:BoundField DataField="LEAVE_TYPE_NAME" HeaderText="Leave Type" SortExpression="LEAVE_TYPE_NAME" />
                <asp:BoundField DataField="DATESDETAILS" HeaderText="Dates" SortExpression="DATESDETAILS" />
                <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="Report_To" HeaderText="Report To" SortExpression="Report_To" />
                
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
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
        </asp:GridView>
         <asp:GridView ID="LeaveApproveGridMgr" runat="server" AutoGenerateColumns="False"
        OnRowCommand="LeaveApprove1_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="LEAVE_IN" HeaderText="From Date" SortExpression="LEAVE_IN" />
                <asp:BoundField DataField="LEAVE_OUT" HeaderText="To Date" SortExpression="LEAVE_OUT" />
                <asp:BoundField DataField="DAYS" HeaderText="No Of Days" SortExpression="DAYS" />
                <asp:BoundField DataField="LEAVE_TYPE_NAME" HeaderText="Leave Type" SortExpression="LEAVE_TYPE_NAME" />
                <asp:BoundField DataField="DATESDETAILS" HeaderText="Dates" SortExpression="DATESDETAILS" />
                <asp:BoundField DataField="DESIGNATION_NAME" HeaderText="Designation" SortExpression="DESIGNATION_NAME" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
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
        </asp:GridView>

    </div>
    <div>
    </div>
    <div id="Div1" runat="server" class="dptitle">
        Pending OT Approvals
    </div>
    <div align="center" style="overflow:auto;WIDTH:100% " >
      
    <div id="diverrorMsg2" class="errorMsg" runat="server"></div>
        <asp:GridView ID="OTApproveL1" runat="server" AutoGenerateColumns="False"
        OnRowCommand="OTApproveL1_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                 <asp:BoundField DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn" />
                  <asp:BoundField DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut" />
                <asp:BoundField DataField="OT" HeaderText="OT" SortExpression="OT" />
                <asp:BoundField DataField="OTBreak" HeaderText="Break" SortExpression="Break" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Report_To" HeaderText="Report To" SortExpression="Report_To" />
                
                <asp:TemplateField HeaderText="L1 Remarks" SortExpression="TL_Remarks" >
                   <ItemTemplate>
                      <asp:TextBox ID="txt_report"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TL_Remarks") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <asp:GridView ID="OTApproveL2" runat="server" AutoGenerateColumns="False"
        OnRowCommand="OTApproveL2_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                 <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
                 <asp:BoundField DataField="TimeIn" HeaderText="TimeIn" SortExpression="TimeIn" />
                  <asp:BoundField DataField="TimeOut" HeaderText="TimeOut" SortExpression="TimeOut" />
                <asp:BoundField DataField="OT" HeaderText="OT" SortExpression="OT" />
                <asp:BoundField DataField="OTBreak" HeaderText="Break" SortExpression="Break" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:TemplateField HeaderText="L1 Remarks" SortExpression="TL_Remarks" >
                   <ItemTemplate>
                      <asp:TextBox ID="txt_report"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"TL_Remarks") %>'></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateField>
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <div id="Div2" runat="server" class="dptitle">
        Pending Shift Approvals
    </div>
    <div align="center" style="overflow:auto;WIDTH:100% " >
      
    <div id="diverrorMsg3" class="errorMsg" runat="server"></div>
        <asp:GridView ID="ShiftApproveL1" runat="server" AutoGenerateColumns="False"
        OnRowCommand="ShiftApproveL1_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="FromDate" HeaderText="Date" SortExpression="FromDate" />
                
                <asp:BoundField DataField="PrvShift" HeaderText="ShiftFrom" SortExpression="ShiftFrom" />
                <asp:BoundField DataField="Shift" HeaderText="ShiftTo" SortExpression="ShiftTo" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Report_To" HeaderText="Report To" SortExpression="Report_To" />
                
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
         <asp:GridView ID="ShiftApproveL2" runat="server" AutoGenerateColumns="False"
        OnRowCommand="ShiftApproveL2_RowCommand"  
         AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
         HeaderStyle-CssClass="darkbackground"  Width="90%" RowStyle-Wrap="false" PagerStyle-Wrap="false"
        >
            <Columns>
                 <asp:BoundField DataField="FNAME" HeaderText="Name" SortExpression="FNAME" />
                <asp:BoundField DataField="FromDate" HeaderText="Date" SortExpression="FromDate" />
               
                <asp:BoundField DataField="PrvShift" HeaderText="ShiftFrom" SortExpression="ShiftFrom" />
                <asp:BoundField DataField="Shift" HeaderText="ShiftTo" SortExpression="ShiftTo" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                <asp:BoundField DataField="Status" HeaderText="Level 1" SortExpression="Report_To" />
                <asp:BoundField DataField="Status1" HeaderText="Level 2" SortExpression="Manager" />
                <asp:TemplateField ShowHeader="False">
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
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
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
    </form>
</body>
</html>



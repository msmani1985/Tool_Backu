<%@ page language="C#" autoeventwireup="true" inherits="Shift_Details, App_Web_4raqxvsr" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="dptitle">
        Shift Change:
    </div>

    <br />
    <div id="LeaveDetaildiv" runat="server" align="center"></div>
    <br />
        <table align="center" class="bordertable" width="700px" cellpadding="2" cellspacing="5">
            <tr>
                <td>
                    Emp Code 
                </td>
                <td>:</td>
                <td colspan="2">
                    <asp:Label ID="EmpcodeLbl" runat="server" Text=""></asp:Label>
                </td>
                <td align="right" >
                    Date:
                </td>
                <td >
                    <asp:Label ID="DateLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Name of Applicant 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="NameLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Department 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DepartmentLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td   >
                    Designation 
                </td>
                <td>:</td>
                <td  colspan="4">
                    <asp:Label ID="DesignationLbl" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        <tr>
                <td   >
                    Shift Date
                </td>
                <td>:</td>
               
                <td>
                    <asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
                </td>
                <td >
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
              <tr>
                <td   >
                    Shift Change</td>
                <td>:</td>
               <td>
                   From: &nbsp; &nbsp;
                   <asp:Label ID="DropEmpShift" runat="server" Width="130px">
                    </asp:Label>
                </td>
                <td >
                    To:
                </td>
                <td >
                    <asp:DropDownList ID="DropShift" runat="server" Width="130px">
                    </asp:DropDownList>
                </td>
               
            </tr>
                  <tr>
                <td   >
                    Remarks 
                </td>
                <td>:</td>
               
                <td colspan="3">
                    <asp:TextBox ID="txtRemarks" runat="server" Width="315px"  ></asp:TextBox>
                </td>
               
            </tr>
             <tr>
                <td colspan="6" align="center">
                    <asp:Button ID="SaveBtn" runat="server" Text="Save" CssClass="dpbutton"  OnClick="SaveBtn_Click" />
                </td>
            </tr>
            <tr>
            <td></td>
            </tr>
      
            </table>
            </div>
     
            <div>
            <table align="center" class="bordertable" width="700px" cellpadding="2" cellspacing="5">
                  <tr>
            <td colspan="7" align="center">
             <asp:DataGrid ID="ShiftDetailsGrid" runat="server" AutoGenerateColumns="False" 
                    OnItemCommand="ShiftDetailsGrid_ItemCommand"  OnItemDataBound="ShiftDetailsGrid_ItemDataBound"
                    CellPadding="3"
                     AlternatingItemStyle-CssClass="dullbackground"
                        HeaderStyle-CssClass="darkbackground"  
                        CssClass="lightbackground" 
                        BorderColor="Black" BorderWidth="1px" ItemStyle-Wrap="false" >
                        <Columns>
                            <asp:BoundColumn DataField="EMPNAME"  HeaderText="Name" SortExpression="EMPNAME" />
                            <asp:BoundColumn DataField="FromDate" HeaderText="Date" SortExpression="FDate" />
                            
                            <asp:BoundColumn  DataField="PrvShift" HeaderText="ShiftFrom" SortExpression="Shift" />
                            <asp:BoundColumn  DataField="Shift" HeaderText="ShiftTo" SortExpression="Shift" />
                           <asp:BoundColumn DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                            <asp:BoundColumn DataField="STATUS" HeaderText="Level 1" SortExpression="STATUS_L1" />
                            <asp:BoundColumn DataField="STATUS_L2" HeaderText="Level 2" SortExpression="STATUS_L2" />
                            <asp:TemplateColumn Visible="false">
                            <ItemTemplate>
                                <asp:ImageButton AlternateText="Remove" ImageUrl="~/images/tools/no.png" id="btnDelete"  AccessKey="D" style="cursor :pointer"
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "EMP_Shift_ID")%>' CommandName="Delete1"  runat="server" ToolTip="Remove" />
                            </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <AlternatingItemStyle CssClass="dullbackground" />
                        <ItemStyle Wrap="False" />
                        <HeaderStyle CssClass="darkbackground" />
                    </asp:DataGrid>
            </td>
            </tr>
            </table>
    </div>
    </form>
</body>
</html>

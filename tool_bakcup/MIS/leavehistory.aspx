<%@ Page Language="C#" AutoEventWireup="true" CodeFile="leavehistory.aspx.cs" Inherits="leavehistory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href=default.css type="text/css" rel=stylesheet />
    <script language="javascript" type="text/javascript">
        function Validation()
        {
            if(isNaN(document.form1.txt_employeeno.value))
            {
                alert("Please Enter Number only");
                document.form1.txt_employeeno.focus();
                return false;                
            }
            if(document.form1.txt_employeeno.value=="")
            {
                alert("Plese Give EmployeeNo.");
                document.form1.txt_employeeno.focus();
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle" id="LeaveTitleDiv" runat="server">
        Leave History
    </div>
    <div id="LeaveHeaderDiv" runat="server"></div>
    <div style="padding-top:20px;width:98%"  align="center" id="LeaveDetailsDiv" runat="server">
        <table width="100%">
            <tr>
            <td colspan="2" visible="false" style="height: 15px">
             Month\Year:
                   <asp:DropDownList ID="DropMonth" runat="server">
                   <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                   </asp:DropDownList>
                   <asp:DropDownList ID="DropYear" runat="server">
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                   </asp:DropDownList>
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
            </td>
            </tr>
            <tr>
                <td>
                    <div id="div_Teamdetails" runat="server">
                        <table><tr><td visible="false" align="right"></td><td align="left"><asp:TextBox ID="txt_employeeno" runat="server" Visible="False"></asp:TextBox></td>
                        <td><asp:Button ID="Btn_submit" Text="Submit" runat="server" OnClick="Btn_submit_Click" OnClientClick="return Validation();" Visible="False" /></td>
                        </tr></table>
                    </div>
                </td>
                <td align="right" style="width: 50px"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" /></td>
            </tr>
            <tr>
                <td style="width:100% " align="center" colspan="2">
                
                    <asp:DataGrid ID="LeaveHistoryGrid" runat="server" AutoGenerateColumns="False" 
                    OnItemCommand="LeaveHistoryGrid_ItemCommand"  OnItemDataBound="LeaveHistory_ItemDataBound"
                    CellPadding="3"
                     AlternatingItemStyle-CssClass="dullbackground"
                        HeaderStyle-CssClass="darkbackground"  
                        CssClass="lightbackground" 
                        BorderColor="Black" BorderWidth="1px" ItemStyle-Wrap="false" >
                        <Columns>
                            <asp:BoundColumn DataField="EMPNAME"  HeaderText="Name" SortExpression="EMPNAME" />
                            <asp:BoundColumn DataField="LEAVE_IN" HeaderText="From Date" SortExpression="LEAVE_IN" />
                            <asp:BoundColumn DataField="LEAVE_OUT" HeaderText="To Date" SortExpression="LEAVE_OUT" />
                           <asp:BoundColumn DataField="DATESDETAILS" HeaderText="Dates" SortExpression="DATESDETAILS" />
                            <asp:BoundColumn DataField="days" HeaderText="No of Days/Hrs" SortExpression="days" />
                            <asp:BoundColumn DataField="LEAVE_TYPE_NAME" HeaderText="Leave Type" SortExpression="LEAVE_TYPE_NAME" />
                            <asp:BoundColumn DataField="COMMENT" HeaderText="Reason" SortExpression="COMMENT" />
                            <asp:BoundColumn DataField="STATUS" HeaderText="Status_L1" SortExpression="STATUS_L1" />
                            <asp:BoundColumn DataField="STATUS_L2" HeaderText="Status_L2" SortExpression="STATUS_L2" />
                            <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:ImageButton  AlternateText="Remove" ImageUrl="~/images/tools/no.png" id="btnDelete"  AccessKey="D" style="cursor :pointer"
                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LHISID")%>' CommandName="Delete1"  runat="server" ToolTip="Remove" />
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
      <div id="div_errormsg" runat="server">
      </div>
    </form>
</body>
</html>


<%@ page language="C#" autoeventwireup="true" inherits="EMP_OT_Late, App_Web_vlobbbje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
         <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<style type="text/css">
.FixedHeader {
            font-weight:bold; color:White; background-color:Green;
        }   
</style>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grvrpt.ClientID %>').Scrollable({
        ScrollHeight: 350
    });
});
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="dptitle">OverTime & Late Details</div>
    <div class="content" id="divReports" runat="server" >
       <table class="bordertable" >
           <tr>
               <td >
                   Start Date:</td>
               <td >
                   <asp:TextBox ID="Txtsdate" runat="server" Text=""></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /></td>
               <td align="center" >
                   End Date:</td>
               <td >
                   <asp:TextBox ID="Txtedate" runat="server"></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
               </td>
               <td rowspan="1" valign="middle" style="height: 2px">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
                       <asp:Button ID="btnClear" runat="server" CssClass="dpbutton"
                       Text="Clear" OnClick="btnClear_Click"   /></td>
               <td rowspan="1" valign="middle" style="height: 2px">
            <asp:ImageButton ID="imgbtnEventExport" runat="server" ImageUrl="~/images/QMS/excel.png" 
            ToolTip="Export Excel" Width="25px" OnClick="imgbtnEventExport_Click" /></td>
          </tr>
                    <tr >
                        
                        <td align="center" colspan="2" style="height: 23px" >
                            <asp:RadioButton ID="EmpnoRBtn" runat="server" GroupName="Employee" Text="ID"  OnCheckedChanged="EmpnoRBtn_CheckedChanged" AutoPostBack="true" Width="69px"/>
                          
                          <asp:RadioButton ID="EmpNameRBtn2" runat="server" GroupName="Employee" Text="Name" OnCheckedChanged="EmpnoRBtn_CheckedChanged"  AutoPostBack="true" Width="87px" />
                                </td>
                                <td  align="center" style="height: 23px" >
                            <asp:Label ID="EmployeeLbl" runat="server" Text="Employee No." Width="111px"></asp:Label>
                        </td>
                        <td style="height: 23px"   > 
                            <asp:TextBox ID="EmpNoNameTxt" runat="server"></asp:TextBox>
                        </td>
                                <td style="height: 23px">
                                    &nbsp;</td>
                                
                                 </tr>
                                 <tr>
                                 <td colspan="6" ></td>
                                 </tr>
                                <tr >
                                <td ></td><td></td>
                                <td align="right">
                                    <asp:Label ID="lblTotalHrs"  runat="server" Text="Total Working Hours:" Visible="False" Height="21px" Width="141px" Font-Bold="True"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="txtTotalHrs" runat="server" Visible="False" Width="72px" Font-Bold="True" Height="23px" ></asp:Label>
                                </td>
                                </tr>
                   <tr >
                                <td colspan="8" align="center" >
                                <table>
                                <tr>
                                <td style="width: 796px"  >
                                <asp:GridView ID="grvrpt" runat="server"  autogeneratecolumns="false" 
                                    EmptyDataText="No data available."  HeaderStyle-CssClass="FixedHeader" Font-Size="9pt" Width="725px" >
                                     
                                     <AlternatingRowStyle Width="100px" BackColor="#F2F2F2" />
                                     <Columns>
                                         <asp:TemplateField HeaderText="Sl.No" >
                                         <ItemTemplate>
                                             <asp:Label ID="sl" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"sl") %>'></asp:Label>
                                         </ItemTemplate>
                                              <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="UserID" >
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserID") %>'></asp:Label>
                                         </ItemTemplate>
                                             <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UserName"  >
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"UserName") %>'></asp:Label>
                                         </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Processdate" >
                                         <ItemTemplate>
                                             <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProcessDate") %>'></asp:Label>
                                         </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        </asp:TemplateField>                        
                                        <asp:boundfield datafield="OT"  headertext="Overtime"/>
                                        <asp:boundfield datafield="LateIn_HHMM"  headertext="LateIn"/>
                                        <asp:boundfield datafield="EarlyOut_HHMM"  headertext="EarlyOut"/>
                                          <asp:boundfield datafield="SFTName"   headertext="Shift Name"/>
                                    </Columns>
                                    <HeaderStyle CssClass="FixedHeader" />
                                    </asp:GridView>
                                </td>
                                </tr>
                                </table>
                                    </td>
                                </tr>
                                
       </table>
    </div>
    </div>
    </form>
</body>
</html>

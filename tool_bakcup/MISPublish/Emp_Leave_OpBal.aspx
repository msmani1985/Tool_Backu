<%@ page language="C#" autoeventwireup="true" inherits="Emp_Leave_OpBal, App_Web_vlobbbje" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
   
     <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grvLvl.ClientID %>').Scrollable({
        ScrollHeight: 400
    });
});
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="dptitle">Leave Balance</div>
    <div class="content" id="divReports" runat="server" >
    <table>
         <tr>
            <td colspan="7">
            <span style="color: Red">*</span>Excel file
            
            <asp:FileUpload ID="fileBrowse" runat="server" />
            <asp:Button ID="btnSave"  CssClass="dpbutton" runat="server" Text="Upload" OnClick="btnSave_Click" />&nbsp;
            </td>
        </tr>
    <tr>
    <td align="right" style="width: 901px">
    <asp:ImageButton ID="cmd_New" ImageUrl="~/images/tools/j_new.png"  runat="server"
                                                ToolTip="New" TabIndex = "24" OnClick="cmd_New_Click" Visible="false" />
    <asp:ImageButton ToolTip="Leave Details" ImageUrl="~/Images/grid6.jpg" AlternateText="Leave Details" ID="ibtngridview" runat="server" OnClick="ibtngridview_Click" Width="27px" />
    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save"  TabIndex="41" OnClick="cmd_Save_Click" />
    </td>
    </tr>
    <tr>
                                <td align="center" style="width: 901px">
                                <table>
                                <tr>
                                <td align="left" style="width: 893px">
                                 <asp:GridView ID="grvLvl" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt" OnRowDataBound="grvLvl_RowDataBound" Width="1100px"  >
                                     <HeaderStyle CssClass="GVFixedHeader" />
                                     <AlternatingRowStyle BackColor="#F2F2F2" />
                                     <Columns>
                                     <asp:TemplateField Visible="False" HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Employee ID">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Refno") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="EmpName">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Designation" >
                                            <ItemTemplate>
                                                <asp:Label ID="Designation_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation_name") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="PL" runat="server" Width="50" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     <asp:TemplateField HeaderText="CL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="CL" runat="server" Width="50">
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="SL" runat="server" Width="50" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="PL Taken">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TakPL" runat="server" Width="50" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     <asp:TemplateField HeaderText="CL Taken">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TakCL" runat="server" Width="50">
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="SL Taken">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TakSL" runat="server" Width="50" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="Closing PL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ClsPL" runat="server" Width="60" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Closing CL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ClsCL" runat="server" Width="60">
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Closing SL">
                                            <ItemTemplate>
                                                <asp:TextBox ID="ClsSL" runat="server" Width="60" >
                                                </asp:TextBox>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Prv.Leave Expired" HeaderStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Check" runat="server" />
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Location">
                                            <ItemTemplate>
                                                <asp:Label ID="Location" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location_name") %>'></asp:Label> 
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
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

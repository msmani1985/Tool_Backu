<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveApp_Stage.aspx.cs" Inherits="LeaveApp_Stage" %>

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
    <div class="dptitle">Leave Reporting Level</div>
    <div class="content" id="divReports" runat="server" >
    <table>
    <tr>
    <td align="right" >
    <asp:ImageButton ToolTip="Reporting Details" ImageUrl="~/Images/grid6.jpg" AlternateText="Reporting Details" ID="ibtngridview" runat="server" OnClick="ibtngridview_Click" Width="27px" />
    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save" OnClick="cmd_Save_Click" TabIndex="41" />
    </td>
    </tr>
    <tr>
                                <td align="center" >
                                <table>
                                <tr>
                                <td align="left">
                                <asp:GridView ID="grvLvl" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt" OnRowDataBound="grvLvl_RowDataBound" Width="1000px" >
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
                                                <asp:Label ID="Refno" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Refno") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="EmpName">
                                            <ItemTemplate>
                                                <asp:Label ID="EmpName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EmpName") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Designation_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation_name") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Level l">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="level1" runat="server" >
                                                </asp:DropDownList>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Level 2">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="level2" runat="server" >
                                                </asp:DropDownList>  
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

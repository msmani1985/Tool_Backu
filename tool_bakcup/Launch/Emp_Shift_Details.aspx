<%@ page language="C#" autoeventwireup="true" inherits="Emp_Shift_Details, App_Web_opkjwe8t" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div class="dptitle">Shift Details</div>
    <div class="content" id="divReports" runat="server" >
    <table>
         
    <tr>
    <td align="right">
        &nbsp;<asp:ImageButton ToolTip="GridView Details" ImageUrl="~/Images/grid6.jpg" AlternateText="GridView Details" ID="ibtngridview" runat="server" OnClick="ibtngridview_Click" Width="27px" />
    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Article"  TabIndex="41" OnClick="cmd_Save_Click" />
                                                <asp:ImageButton id="cmd_Excel" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" ToolTip="Excel" OnClick="cmd_Excel_Click"></asp:ImageButton>
    </td>
    </tr>
    <tr>
    <td align="center">
    Shift Change Date:<asp:TextBox ID="FromTxt" runat="server"  ></asp:TextBox>&nbsp; <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />
    </td>
    </tr>
    <tr>
                                <td align="center" style="width: 850px">
                                    <asp:GridView ID="grvLvl" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  
                                    Font-Size="9pt" OnRowDataBound="grvLvl_RowDataBound" 
                                    BorderWidth="3px" CellPadding="2" BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black" >
                                     <HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
                                     <AlternatingRowStyle BackColor="#F2F2F2" />
                                     <Columns>
                                     <asp:TemplateField Visible="false" HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="ID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"EMPLOYEE_ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="SID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"sl") %>'></asp:Label>
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
                                   <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="Designation_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Designation_name") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Department">
                                            <ItemTemplate>
                                                <asp:Label ID="Department" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Department") %>'></asp:Label>
                                            </ItemTemplate>
                                     </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift Details">
                                            <ItemTemplate>
                                                <asp:DropDownList  ID="DropShift" runat="server">
                                                </asp:DropDownList>  
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Check">
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
                                    <RowStyle BackColor="White" />
        <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView></td>
                                </tr>
               
    </table>
    </div>
    </div>
    </form>
</body>
</html>

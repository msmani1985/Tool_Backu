<%@ page language="C#" autoeventwireup="true" inherits="Emp_Shift_Details, App_Web_25d24vps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" language="javascript">
        function CheckAllEmp(Checkbox) {
            var grvShift = document.getElementById("<%=grvShift.ClientID %>");
            for (i = 1; i < grvShift.rows.length; i++) {
                grvShift.rows[i].cells[6].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
        function view_Data(ID) {
            window.open("EmpShiftMonthWise.aspx?Employee_ID=" + ID , 'ViewChange',
          'height=470,width=800,left=250,top=150,screenX=0,screenY=100');

            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">Shift Details</div>
        <div class="content" id="divReports" runat="server" >
            <table>
                <tr>
                    <td align="center">
                    
                    </td>

                    <td align="right">
                        <asp:ImageButton ImageUrl="~/Images/grid6.jpg"  Width="27px" 
                            ToolTip="View" ID="ibtngridview" runat="server" OnClick="ibtngridview_Click"/>
                        <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                            ToolTip="Save"  TabIndex="41" OnClick="cmd_Save_Click" />
                        <asp:ImageButton id="cmd_Excel" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" 
                            ToolTip="Excel" OnClick="cmd_Excel_Click"></asp:ImageButton>
                        <asp:ImageButton id="cmd_Help" tabIndex="41"  runat="server" ImageUrl="~/images/wysiwyg_images/help_on.gif" 
                            ToolTip="Help" OnClick="cmd_Help_Click" Height="30px"></asp:ImageButton>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <asp:RadioButtonList ID="rbLocation" runat="server" RepeatDirection="Horizontal" Height="22px" Width="217px" >
                        <asp:ListItem Selected="True" Value="2">Chennai</asp:ListItem>
                        <asp:ListItem Value="3">Coimbatore</asp:ListItem>
                    </asp:RadioButtonList>
                        From:
                        <asp:TextBox ID="FromTxt" runat="server" Height="16px" Width="100px"></asp:TextBox>&nbsp; 
                        <img style="cursor:pointer; border: none" alt="Calendar" src="images/Calendar.jpg" height="20px" border="0" 
                            onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=FromTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                        &nbsp; &nbsp; 
                        To:
                        <asp:TextBox ID="ToTxt" runat="server" Height="16px" Width="100px"></asp:TextBox>&nbsp; 
                        <img style="cursor:pointer; border: none" alt="Calendar" src="images/Calendar.jpg" height="20px" border="0" 
                            onclick="javascript:calendar_window=window.open('calendar1.aspx?formname=ToTxt','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                    </td>
                </tr>
                <tr>
                    <td align="center" style="width: 850px" colspan="2">
                        <asp:GridView ID="grvShift" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  
                            Font-Size="9pt" OnRowDataBound="grvShift_RowDataBound" BorderWidth="3px" CellPadding="2" 
                            BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black" >
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
                                <asp:TemplateField >
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkboxSelectAll" runat="server" onclick="CheckAllEmp(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Check" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="Location" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Location_name") %>'></asp:Label> 
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton CssClass="l_link" Text="View" ID="lnkView" runat="server" 
                                            ToolTip="View"  OnClientClick='<%#string.Format("return view_Data(\"{0}\");",
                                              DataBinder.Eval(Container.DataItem, "EMPLOYEE_ID")) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="White" />
                            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
            <script type="text/javascript" src="Scripts/jquery.min.js"></script>
            <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
            <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    gridviewScroll();
                });

                function gridviewScroll() {
                    $('#<%=grvShift.ClientID%>').gridviewScroll({
                        width: 1075,
                        height: 400,
                        startHorizontal: 0,
                        barhovercolor: "#848484",
                        barcolor: "#848484"
                    });
                }
            </script>
        </div>
    </div>
    </form>
</body>
</html>

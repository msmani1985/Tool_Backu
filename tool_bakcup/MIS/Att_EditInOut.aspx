<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Att_EditInOut.aspx.cs" Inherits="Att_EditInOut" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
    <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>
    <link href="style/calendar-blue.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">Edited In/Out Punchs</div>
        <div class="content" id="divReports" runat="server" >
        <table width="800px" align="center">
            <tr>
                <td  class="auto-style1">
                    <asp:Label ID="EmployeeLbl" runat="server" Text="Emp ID." Width="100px" Height="16px"></asp:Label>
                    <asp:TextBox ID="EmpNoTxt"  Height="16px" Width="150px" runat="server"></asp:TextBox>
               </td>
                <td>Date: </td>
                <td>
                    <asp:TextBox ID="txtPdate" runat="server" ></asp:TextBox>
                    <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtPdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
                </td>
                <td>
                    <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click"  />
                       &nbsp;
                       <asp:Button ID="btnClear" runat="server" CssClass="dpbutton"
                       Text="Clear" OnClick="btnClear_Click"   />
                </td>
            </tr>
        </table>
          <%--  </div>
        <div class="content" id="div1" runat="server" >--%>
        <table width="800px" align="center">
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="ReProcess Emp ID." Width="100px" Height="16px"></asp:Label>
                    <asp:TextBox ID="ReProEmpID"  Height="16px" Width="150px" runat="server"></asp:TextBox>
                </td>
                <td align="right">
                    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
                             ToolTip="Save" OnClick="cmd_Save_Click" TabIndex="41" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvEmployeeDetails" runat="server" Width="80%" AutoGenerateColumns="false"
                        emptydatatext="No data available."  Font-Size="9pt" OnRowDataBound="gvEmployeeDetails_RowDataBound" >
                        <HeaderStyle CssClass="GVFixedHeader" />
                        <AlternatingRowStyle BackColor="#F2F2F2" />
                        <Columns>
                            <asp:TemplateField HeaderText="UserID"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UserID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EmpName"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmpname" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Empname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPdate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Pdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Punch"  ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPunch" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Edatetime") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:DropDownList ID="dropInOut" runat="server">
                                        <asp:ListItem Value="0">In</asp:ListItem>
                                        <asp:ListItem Value="1">Out</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        </div>
    </div>
    </form>
</body>
</html>

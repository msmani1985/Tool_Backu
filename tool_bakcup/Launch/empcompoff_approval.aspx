<%@ page language="C#" autoeventwireup="true" inherits="empcompoff_approval, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Compoff Approval</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="scripts/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                CompOff Approval</div>
            <table align="center" cellpadding="2" cellspacing="5" class="bordertable" width="800">
                <tr>
                    <td align="center" colspan="6">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="center" colspan="6">
                                    Employee Number:<asp:TextBox ID="txtEmpcode" onkeypress="javascript:return OnlyAllowNumbers(this,event);" runat="server" Width="68px" MaxLength="10"></asp:TextBox>&nbsp; Status:<asp:DropDownList ID="drpStatus" runat="server">
                                        <asp:ListItem Value="All"></asp:ListItem>
                                        <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                        <asp:ListItem Value="Approved">Approved</asp:ListItem>
                                        <asp:ListItem Value="Rejected">Rejected</asp:ListItem>
                                        <asp:ListItem Value="Leave taken">Leave taken</asp:ListItem>
                                    </asp:DropDownList>&nbsp; From:<asp:TextBox ID="txtfDate" runat="server" onclick="javascript:this.value='';"
                                        Width="70px" MaxLength="10" CssClass=""></asp:TextBox><img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtfDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                                        border-left-style: none; border-bottom-style: none" align="absMiddle" />&nbsp; To:<asp:TextBox ID="txteDate" runat="server" onclick="javascript:this.value='';"
                                        Width="70px" MaxLength="10" CssClass=""></asp:TextBox><img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txteDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                                        border-left-style: none; border-bottom-style: none" align="absMiddle" />&nbsp;
                                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnNew" runat="server" CssClass="dpbutton" OnClick="btnNew_Click"
                                        Text="New" /></td>
                            </tr>
                            <tr>
                                <td align="right" colspan="6">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <div id="divGridview" runat="server">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right" colspan="6">
                                        <div style="padding:2px 0 2px 0">
                                            <span id="trcrtl1" runat="server">
                                                <asp:ImageButton ID="imgbtnapprov1" runat="server" ImageUrl="~/images/tools/accept.png"
                                                    OnClick="imgbtnapprov1_Click" ToolTip="Approve" OnClientClick="return confirm('Confirm Approve?');" />
                                                <asp:ImageButton ID="imgbtnreject1" runat="server" ImageUrl="~/images/tools/reject.png"
                                                    OnClick="imgbtnreject1_Click" ToolTip="Reject" OnClientClick="return confirm('Confirm Reject?');" />
                                            </span>
                                            <asp:ImageButton ID="imgbtnExport1" runat="server" ImageUrl="~/images/tools/doc_excel.png"
                                                OnClick="imgbtnExport1_Click" ToolTip="Export to Excel" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCompoff" runat="server" AutoGenerateColumns="False" OnRowCommand="gvCompoff_RowCommand" OnRowDataBound="gvCompoff_RowDataBound"
                                            Width="800px" CssClass="lightbackground">
                                            <RowStyle HorizontalAlign="Left" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sno.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblsno" runat="server" Text="<%# id=id+1 %>"></asp:Label>
                                                        <asp:HiddenField ID="gvHfcompoffid" runat="server" Value='<%# Eval("compoff_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblName" runat="server" Text='<%# Eval("employee_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblEmpCode" runat="server" Text='<%# Eval("employee_number") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Compoff Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblCOdate" runat="server" Text='<%# Eval("Compoff_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hours Worked">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblCOhours" runat="server" Text='<%# Eval("compoff_hours") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Leave taken on">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblCOLeavedate" runat="server" Text='<%# Eval("leave_taken_on") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="gvlblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <img class="CursorAdd" title='<%# Eval("comments") %>' src="images/tools/comment.png" alt="info" runat="server"/><%--<asp:Label ID="gvlblCOComments" runat="server" Text='<%# Eval("comments") %>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="gvchkCO" runat="server" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" Width="10px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle HorizontalAlign="Left" CssClass="darkbackground" />
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="6">
                                        <div style="padding:4px 0 2px 0">
                                            <span id="trcrtl2" runat="server">
                                                <asp:ImageButton ID="imgbtnapprov2" runat="server" ImageUrl="~/images/tools/accept.png"
                                                    OnClick="imgbtnapprov1_Click" ToolTip="Approve" OnClientClick="return confirm('Confirm Approve?');" />
                                                <asp:ImageButton ID="imgbtnreject2" runat="server" ImageUrl="~/images/tools/reject.png"
                                                    OnClick="imgbtnreject1_Click" ToolTip="Reject" OnClientClick="return confirm('Confirm Reject?');" />
                                            </span>
                                            <asp:ImageButton ID="imgbtnExport2" runat="server" ImageUrl="~/images/tools/doc_excel.png"
                                                OnClick="imgbtnExport1_Click" ToolTip="Export to Excel" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

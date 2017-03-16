<%@ Page Language="C#" AutoEventWireup="true" CodeFile="task_department.aspx.cs"
    Inherits="task_department" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Department Tasks</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                <asp:Label ID="lblTitle" runat="server" Text="Department Task"></asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 443px">
            <tr>
                <td align="center">
                    <strong>Department: </strong>&nbsp;<asp:DropDownList ID="drpDepartment" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpDepartment_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td align="right">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/tools/j_save.png"
                        OnClick="ImageButton1_Click" ToolTip="Save" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvDeptTast" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                        Width="100%" ShowFooter="True" OnRowDataBound="gvDeptTast_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.no">
                                <ItemTemplate>
                                    <%# id++ %>
                                    <asp:HiddenField ID="hfgvTaskID" Value='<%# Eval("task_id")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Task Name">
                                <ItemTemplate>
                                    <%# Eval("task_name")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkgvAssign" runat="server" Checked='<%# Eval("checked")%>' />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                No records found.</div>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="darkbackground" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="task_jobtype, App_Web_znvsjrxn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job Type Task</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Job Type Task
    </div>
    <div>
        <table style="width: 443px" class="bordertable" align="center"><tr>
            <td>Job Type</td><td><asp:DropDownList ID="dd_jobtype" runat="server" OnSelectedIndexChanged="dd_jobtype_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem>
            <asp:ListItem Text="Articles" Value="5"></asp:ListItem>
            <asp:ListItem Text="Issues" Value="6"></asp:ListItem>
            <asp:ListItem Text="Books" Value="2"></asp:ListItem>
            <asp:ListItem Text="Chapters" Value="7"></asp:ListItem>
            <asp:ListItem Text="Projects" Value="4"></asp:ListItem>
            <asp:ListItem Text="Modules" Value="8"></asp:ListItem>
            </asp:DropDownList></td>
            <td align="right">
                    <asp:ImageButton ID="img_save" runat="server" ImageUrl="~/images/tools/j_save.png"
                        OnClick="img_save_Click" ToolTip="Save" /></td>
        </tr>
        <tr><td colspan="3">
            <asp:GridView ID="gv_jobtypetask" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" Width="100%"
            OnRowDataBound="gv_jobtypetask_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="Sl.no">
                    <ItemTemplate>
                    <%# id++ %>
                    <asp:HiddenField ID="hf_taskid" runat="server" Value='<%# Eval("task_id") %>' /> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Task Name">
                    <ItemTemplate>
                        <%# Eval("task_name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_assign" runat="server" Checked='<%# Eval("checked") %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="center" />
                </asp:TemplateField>
            </Columns>
            <HeaderStyle CssClass="darkbackground" />
            </asp:GridView>
        </td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

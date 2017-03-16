<%@ page language="C#" autoeventwireup="true" inherits="project_pagination, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagination Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Project Pagination</div>
    <div>
    <table width="100%" align="center" class="bordertable"><tr><td align="right">Parent Project</td><td><asp:DropDownList ID="dd_parentproject" DataTextField="pcode" DataValueField="projectno" runat="server" OnSelectedIndexChanged="dd_parentproject_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td></tr>
    </table>
    </div>
    <div id="div_childproject" runat="server">
    <table class="bordertable" width="100%">
    <tr><td >Child Project</td></tr>
    <tr><td ><asp:GridView ID="gv_childproject" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground"
    AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" >
    <Columns>
    <asp:BoundField DataField="pcode" HeaderText="Project Code" />
    <asp:BoundField DataField="ptitle" HeaderText="Project Title" />
    <asp:BoundField DataField="pdispatchdate" HeaderText="Dispatch Date" DataFormatString="{0: MM/dd/yyyy}" />
    <asp:BoundField DataField="cname" HeaderText="Customer Name" />
    <asp:TemplateField>
    <ItemTemplate>
        <asp:CheckBox ID="cb_project" runat="server" />
        <asp:HiddenField ID="hf_projectno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"PROJECTNO") %>' />
    </ItemTemplate>
    <HeaderTemplate>
        <asp:ImageButton ID="img_assign" runat="server" ImageUrl="~/images/tools/j_save.png" OnClick="img_assign_click" />
    </HeaderTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView></td></tr>
    </table>
    </div>
    </form>
</body>
</html>

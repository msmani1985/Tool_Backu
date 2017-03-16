<%@ page language="C#" autoeventwireup="true" inherits="project_grouping, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project Grouping</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">Project Grouping - Global Language Solutions.</div>
    <div>
    <table width="100%" align="center" class="bordertable">
    <tr>
    <td align="right"><strong>Parent Project</strong></td>
    <td><asp:DropDownList ID="dd_parentproject" runat="server" OnSelectedIndexChanged="dd_parentproject_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList></td>
    </tr>
    </table>
    </div>
    <div id="div_childproject" runat="server">
    <table class="bordertable" width="100%">
    <tr><td> <strong> Child Project </strong></td></tr>
    <tr><td ><asp:GridView ID="gv_childproject" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground"
    AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground" >
    <Columns>
    <asp:BoundField DataField="cust_name" HeaderText="Customer Name" Visible="false"/>
    <asp:BoundField DataField="NAME" HeaderText="Project Code"/>
    <asp:BoundField DataField="parent_name" HeaderText="Grouped Project"/>
    <asp:BoundField DataField="TITLE" HeaderText="Project Title" Visible="false"/>
    <asp:BoundField DataField="despatch_date" HeaderText="Dispatch Date" DataFormatString="{0: MM/dd/yyyy}"/>
    
    <asp:TemplateField>
    <ItemTemplate>
        <asp:CheckBox ID="chkbox_project" runat="server" />
        <asp:HiddenField ID="hf_parentjobid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"parent_job_id") %>' />
    </ItemTemplate>
    <HeaderTemplate>
        <asp:ImageButton ID="img_assign" runat="server" ImageUrl="~/images/tools/j_save.png" OnClick="img_assign_click" ToolTip="Save"/>
    </HeaderTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView></td></tr>
    </table>
    </div>
    </form>
</body>
</html>

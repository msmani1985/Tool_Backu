<%@ page language="C#" autoeventwireup="true" inherits="employee_inbox, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Job Group</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        Employee Job Group
    </div>
    <div id="div_empjobgroup" runat="server">
    <asp:GridView ID="gv_empjob_details" runat="server" AutoGenerateColumns="false"
    CssClass="dullbackground" AlternatingRowStyle-CssClass="lightbackground" HeaderStyle-CssClass="darkbackground" Width="800px">
    <Columns>
        <asp:BoundField DataField="sales_group_name" HeaderText="Group Name" />
        <asp:BoundField DataField="job_type_name" HeaderText="Type" />
        <asp:BoundField DataField="job_Name" HeaderText="Job" />
        <asp:BoundField DataField="name" HeaderText="Job Name" />
        <asp:BoundField DataField="received_date"  HeaderText="Received Date" />
        <asp:BoundField DataField="due_date" HeaderText="Due Date" />
        <asp:BoundField DataField="half_due_date" HeaderText="Half Due Date" />
        <asp:BoundField DataField="cats_due_date" HeaderText="CATs Due Date" />
        
    </Columns>
    </asp:GridView>
    </div>
    <div id="div_error" runat="server" class="errorMsg"></div>
    </form>
</body>
</html>

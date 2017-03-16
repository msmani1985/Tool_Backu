<%@ page language="C#" autoeventwireup="true" inherits="archived_issues_sql, App_Web_opij0lkt" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Archived Issues</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="float:left;">
        <table width="99%" ><tr>
        <td class="textheader" >List of archived Issue</td><td  align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel_archivedlist" OnClick="exportExcel_archivedlist_Click" /></td>
        <td class="textheader" >List of published articles by issue</td><td align="right"><asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportexcel_publishedlist" OnClick="exportexcel_publishedlist_Click" /></tr>
        
        <tr><td colspan="2" width="30%" style="vertical-align:top;" ><asp:GridView ID="gv_archivedissue" runat="server" AutoGenerateColumns="false" Width="100%" 
         OnRowCommand="onRowCommand_archivedlist"  RowStyle-Height="20" >
         <HeaderStyle CssClass="GVFixedHeader" />
            <Columns>
                <asp:TemplateField HeaderText="Issues">
                    <ItemTemplate>
                        <asp:LinkButton  ID="lb_job_name" runat="server" ForeColor="black"
                        Text='<%# DataBinder.Eval(Container.DataItem,"job_name") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"parent_job_id") %>' CommandName="archived_issue" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView></td>
        <td colspan="2" style=" vertical-align:top;" ><asp:GridView ID="gv_publishedarticles" runat="server" 
         RowStyle-Font-Names="sans-serif" RowStyle-Font-Size="8pt" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="gridalternaterow"  CssClass="lightbackground">
        <HeaderStyle CssClass="GVFixedHeader"  />
        <Columns>
            <asp:BoundField DataField="title" SortExpression="title" HeaderText="Title" />
            <asp:BoundField DataField="author" SortExpression="author" HeaderText="Author" />
            <asp:BoundField DataField="author_email" SortExpression="author_email" HeaderText="Email" ItemStyle-Width="125px" ItemStyle-Wrap="true" ItemStyle-CssClass="wraptxt"  />
        </Columns>
        </asp:GridView><asp:Label Text="" CssClass="error" ID="lbl_error" runat="server"></asp:Label></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="job_dispatch_cust, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Despatch Summary</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
    function previewJob(jobid, jobtypeid){
        var w=window.open('jobbag.aspx?jobid=' + jobid + '&jobtypeid='+jobtypeid+'&print=1','Preview','width=900,height=600,left=25,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">    
        <div>
            <div id="divTitle" class="dptitle" runat="server">
                Despatch Summary</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="background-color: Honeydew;
            font-weight: bold">
            <tr>
                <td align="right">
                    &nbsp;&nbsp;Journal:&nbsp;<asp:DropDownList ID="drpJournal" runat="server">
                    </asp:DropDownList><span style="color: #ff0000"></span></td>
                <td align="right">
                &nbsp;&nbsp;Job Type:&nbsp;<asp:DropDownList ID="drpJobType" runat="server">
                    <asp:ListItem Value="6">Issue</asp:ListItem>
                    <asp:ListItem Value="5">Article</asp:ListItem>
                </asp:DropDownList>
                </td>
                
                <td align="left">
                    &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <div id="divReport" align="center" runat="server">
            <table class="bordertable" style="width: 95%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgbtnExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                            ToolTip="Export to Excel" OnClick="imgbtnExport_Click" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvDispatchSummary" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                            Width="100%" CellPadding="2" AllowSorting="True" OnRowDataBound="gvDispatchSummary_RowDataBound" OnSorting="gvDispatchSummary_Sorting">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle ForeColor="white" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="journal_code" HeaderText="Journal" SortExpression="journal_code" />
                                <asp:TemplateField HeaderText="Issue" SortExpression="Issue">
                                    <ItemTemplate>
                                        <a href="#preview" onclick="javascript:previewJob('<%#Eval("parent_job_id")%>','6')">
                                            <%#Eval("Issue")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manuscript" SortExpression="manuscriptid">
                                    <ItemTemplate>
                                        <a href="#preview" onclick="javascript:previewJob('<%#Eval("job_id")%>','5')">
                                            <%#Eval("manuscriptid")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Article Title" DataField="title" SortExpression="title">
                                </asp:BoundField>                                
                                <asp:BoundField HeaderText="Author" DataField="author" SortExpression="author">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Email" DataField="author_email" SortExpression="author_email">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Received Date" DataField="received_date" SortExpression="received_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Revised Date" DataField="revised_date" SortExpression="revised_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Accepted Date" DataField="accepted_date" SortExpression="accepted_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Due Date" DataField="due_date" SortExpression="due_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Despatched Date" DataField="despatch_date" SortExpression="despatch_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Invoice No." DataField="invoice_no" SortExpression="invoice_no">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Invoice Date" DataField="invoice_date" SortExpression="invoice_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Stage" DataField="job_stage_name" SortExpression="job_stage_name">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="status" SortExpression="status">
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="darkbackground" />
                            <EmptyDataTemplate>
                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                    No records found.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </div>
    </form>
</body>
</html>

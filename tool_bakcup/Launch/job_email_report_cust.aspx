<%@ page language="C#" autoeventwireup="true" inherits="job_email_report_cust, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Job Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>
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
                WIP Issue Summary</div>
        </div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="background-color: Honeydew;
            font-weight: bold">
            <tr>
                <td align="right" colspan="12">
                    &nbsp;&nbsp;Journal:&nbsp;<asp:DropDownList ID="drpJournal" runat="server" OnSelectedIndexChanged="drpJournal_SelectedIndexChanged"
                        AutoPostBack="True">
                    </asp:DropDownList><span style="color: #ff0000"></span></td>
                <td align="left" colspan="1">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="12">
                        <div id="divStatus" style="float: right" runat="server">
                            &nbsp;&nbsp;Status:
                            <asp:DropDownList ID="drpStatus" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div id="divStage" style="float: right" runat="server">
                            &nbsp;&nbsp;Stage:
                            <asp:DropDownList ID="drpJobStage" runat="server">
                            </asp:DropDownList>
                        </div>
                        <div id="divIssueList" style="float: right" runat="server">
                            Issue:
                            <asp:DropDownList ID="drpJob" runat="server">
                            </asp:DropDownList>
                        </div>
                </td>
                <td align="left" colspan="1">
                    <asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" OnClick="btnSearch_Click"
                        Text="Submit" /></td>
            </tr>
        </table>
        <br />
        <div id="divReport" align="center" runat="server">
            <table class="bordertable" style="width: 95%">
                <tr>
                    <td align="right">
                        <asp:ImageButton ID="imgbtnExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                            OnClick="imgbtnExport_Click" ToolTip="Export to Excel" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvEventHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                            Width="100%" CellPadding="2" AllowSorting="True" OnRowDataBound="gvEventHistory_RowDataBound"
                            OnSorting="gvEventHistory_Sorting">
                            <RowStyle HorizontalAlign="Left" />
                            <AlternatingRowStyle CssClass="dullbackground" />
                            <HeaderStyle ForeColor="white" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.no">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlno" runat="server" Text='<%#id++ %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Journal" DataField="journal_code" SortExpression="journal_code">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Issue" SortExpression="title">
                                    <ItemTemplate>
                                        <a href="#preview" onclick="javascript:previewJob('<%#Eval("parent_job_id")%>','6')">
                                            <%#Eval("title")%>
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
                                <asp:TemplateField HeaderText="Article Title" SortExpression="topics">
                                    <ItemTemplate>
                                        <div id="divgvEmail" style="width: 200px; word-wrap: break-word;"
                                            title='<%#Eval("topics")%>' runat="server">
                                            <%#Eval("topics")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField HeaderText="Author" DataField="display_name" SortExpression="display_name">
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Email" SortExpression="email1">
                                    <ItemTemplate>
                                        <div id="divgvEmail" style="width: 150px; word-wrap: break-word;"
                                            title='<%#Eval("email1")%>' runat="server">
                                            <%#Eval("email1")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Received Date" DataField="received_date" SortExpression="received_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Due Date" DataField="due_date" SortExpression="due_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Despatch Date" DataField="despatch_date" SortExpression="despatch_date">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Reviewers" DataField="sent_contact_name" SortExpression="sent_contact_name">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Stage" DataField="job_stage_name" SortExpression="job_stage_name">
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Status" DataField="email_letter_name" SortExpression="email_letter_name">
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

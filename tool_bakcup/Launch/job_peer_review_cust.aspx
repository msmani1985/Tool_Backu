<%@ page language="C#" autoeventwireup="true" inherits="job_peer_review_cust, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script language="javascript">    
    function previewJob(jobid, jobtypeid){
        var width = 800,height = 600;
        var left = (screen.width  - width)/2;
        var top = (screen.height - height)/2;
        var w=window.open('jobbag.aspx?jobid=' + jobid + '&jobtypeid='+jobtypeid+'&print=1','Preview','width='+width+',height='+height+',left='+left+',top='+top+',toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;
    }
    function popStages(jobid, jobtypeid, jobstageid){
        var width = 500,height = 400;
        var left = (screen.width  - width)/2;
        var top = (screen.height - height)/2;
        var w=window.open('job_switch_stage.aspx?jobid=' + jobid + '&jobtypeid='+jobtypeid+'&jobstageid='+jobstageid,'Preview','width='+width+',height='+height+',left='+left+',top='+top+',toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">    
        <div id="divTitle" runat="server" class="dptitle">
            Summary</div>
        <table align="center" cellpadding="1" cellspacing="1" class="bordertable" style="font-weight: bold;
            background-color: honeydew">
            <tr>
                <td align="right" colspan="12">
                    &nbsp; Journal:&nbsp;<asp:DropDownList ID="drpJournal" runat="server"
                        OnSelectedIndexChanged="drpJournal_SelectedIndexChanged">
                    </asp:DropDownList><span style="color: #ff0000"></span></td>
                <td align="left" colspan="1">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="12">
                    <div id="divStage" runat="server" style="float: right">
                        &nbsp; Stage:
                        <asp:DropDownList ID="drpJobStage" runat="server">
                            <asp:ListItem Value="59">Peer Review 1</asp:ListItem>
                            <asp:ListItem Value="60">Peer Review 2</asp:ListItem>
                            <asp:ListItem Value="76">Author modifications</asp:ListItem>
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
                        <asp:ImageButton ID="imgbtnExport" runat="server" ImageUrl="~/images/tools/j_excel.png"
                            OnClick="imgbtnExport_Click" ToolTip="Export to Excel" /></td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvEventHistory" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            CellPadding="2" CssClass="lightbackground" Width="100%" OnRowDataBound="gvEventHistory_RowDataBound" OnSorting="gvEventHistory_Sorting">
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
                                <asp:TemplateField HeaderText="Manuscript" SortExpression="manuscriptid">
                                    <ItemTemplate>
                                        <a href="#preview" onclick="javascript:previewJob('<%#Eval("job_id")%>','5')">
                                            <%#Eval("manuscriptid")%>
                                        </a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Article Title" SortExpression="topics">
                                    <ItemTemplate>
                                        <div style="width: 200px; word-wrap: break-word;"
                                            title='<%#Eval("topics")%>'>
                                            <%#Eval("topics")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Author" DataField="display_name" SortExpression="display_name" />
                                <asp:TemplateField HeaderText="Email" SortExpression="email1">
                                    <ItemTemplate>
                                        <div style="width: 150px; word-wrap: break-word;"
                                            title='<%#Eval("email1")%>'>
                                            <%#Eval("email1")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="received_date" HeaderText="Received Date" SortExpression="received_date" />
                                <asp:BoundField DataField="due_date" HeaderText="Due Date" SortExpression="due_date" />
                                <asp:BoundField DataField="despatch_date" HeaderText="Despatch Date" SortExpression="despatch_date" />
                                <asp:BoundField DataField="reviewers" HeaderText="Reviewers" SortExpression="reviewers" />
                                <asp:BoundField DataField="job_stage_name" HeaderText="Stage" SortExpression="job_stage_name" />
                                <asp:BoundField DataField="email_letter_name" HeaderText="Status" SortExpression="email_letter_name" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <img src="images/tools/edit.png" align="middle" title="Edit Stage" alt="Edit" onclick="javascript:popStages('<%#Eval("job_id")%>','<%#Eval("job_type_id")%>','<%#Eval("job_stage_id")%>');" style="cursor:pointer" />                                        
                                    </ItemTemplate>
                                </asp:TemplateField>
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

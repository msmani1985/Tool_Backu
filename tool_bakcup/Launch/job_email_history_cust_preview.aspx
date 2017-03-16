<%@ page language="C#" autoeventwireup="true" inherits="job_email_history_cust_preview, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Email History Preview</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style>
    #btnClose{border: solid 1px #000}
    </style>
    <script language="javascript">
        function previewJob(jobid, jobtypeid){
            var width = 800,height = 600;
            var left = (screen.width  - width)/2;
            var top = (screen.height - height)/2;
            var w=window.open('jobbag.aspx?jobid=' + jobid + '&jobtypeid='+jobtypeid+'&print=1','Preview','width='+width+',height='+height+',left='+left+',top='+top+',toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server"><br />
        <div id="divTitle" runat="server" class="dptitle">
            Email History <div style="float:right;"><input id="btnClose" type="button" value="Close[x]" onclick="javascript:self.close();" style="font-size: 11px; background-color: Green; color: White; cursor: pointer" /></div></div>
        <br />
        <div id="divReport" align="center" runat="server">
            <table class="bordertable" style="width: 95%">
                <tr class="">
                    <td align="left">
                        <div id="divJobName" style="float:left; width:95%" runat="server">Job: XXXX XXXXXXXX</div>
                        <div style="float:right">
                            <asp:ImageButton ID="imgbtnExport" runat="server" ImageUrl="~/images/tools/j_excel.png"
                                OnClick="imgbtnExport_Click" ToolTip="Export to Excel" />                                   
                        </div>
                    </td>
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
                                <asp:TemplateField HeaderText="Article Title" SortExpression="title">
                                    <ItemTemplate>
                                        <div style="width: 250px; word-wrap: break-word;" title='<%#Eval("title")%>'>
                                            <%#Eval("title")%>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email To" SortExpression="display_name">
                                    <ItemTemplate>
                                            <%#Eval("display_name")%>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                                <asp:BoundField HeaderText="Email Address" DataField="email" SortExpression="email" />
                                <asp:BoundField DataField="job_stage_name" HeaderText="Stage" SortExpression="job_stage_name" />
                                <asp:BoundField DataField="email_letter_name" HeaderText="Status" SortExpression="email_letter_name" />
                                <asp:BoundField DataField="email_sent_date" HeaderText="Sent on" SortExpression="email_sent_date" />
                                <asp:BoundField DataField="hasattachments" HeaderText="Attachment" SortExpression="hasattachments" /><asp:TemplateField>
                                    <ItemTemplate>                                        
                                        <strong><a id="lnkPreview" class="link1" href="#" runat="server">Preview</a></strong>
                                        <asp:HiddenField ID="hfEmailSentID" runat="server" Value='<%# Eval("email_sent_id") %>' />
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

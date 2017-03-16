<%@ page language="C#" autoeventwireup="true" inherits="followupauthor, App_Web_w6b3pav3" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
 .RedColored  {     background: FF0000; } </style>
    <script language="javascript">
        function mailconfirmation()
        {
            if(confirm("Are you sure, you want to send mail?"))
                return true;
            else
                return false;
        }
        
       
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divtitle" class="dptitle">SAM Author FollowUp</div>
    <div>
        <table align="center" width="650px">
            <tr>
            <%--<td><asp:Image ImageUrl="~/images/mailsent.bmp" ID="Imgmailsent" runat="server" /> Mail Sent</td>--%>
            <td align="right">
            <asp:ImageButton ID="Ibtn_Excel" runat="server" ImageUrl="~/images/Excel.jpg" OnClick="Ibtn_Excel_Click" /></td></tr>
            <tr>
            <td colspan="2" align="left">
                <asp:GridView  Width="650px" ID="GV_samauthorfollow" runat="server" AutoGenerateColumns="false" 
                OnRowCommand="GV_sam_Rowcommand"  OnRowDataBound="GV_sam_RowDataBound" OnSorting="GV_OnSortdata" 
                AllowSorting="true" >
                 <HeaderStyle CssClass="GVFixedHeader" />
                 <AlternatingRowStyle BackColor="#F2F2F2" />
                 <Columns>
                    <asp:BoundField HeaderText="Journal Code" DataField="journal_code" SortExpression="journal_code" />
                    <asp:BoundField HeaderText="Name" DataField="name" SortExpression="name" />
                    <asp:BoundField HeaderText="Stage Name" DataField="job_stage_name" SortExpression="job_stage_name" />
                    <asp:BoundField HeaderText="Despatch Date" DataField="despatch_date" SortExpression="despatch_date" />
                    <asp:BoundField HeaderText="Received Date" DataField="Received_Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                    <asp:BoundField HeaderText="Due Date" DataField="cats_due_date" SortExpression="cats_due_date" />
                    <asp:BoundField HeaderText="Follow Date" DataField="follow_date" SortExpression="follow_date" />
                    <asp:BoundField HeaderText="Follow Days" DataField="follow_days" SortExpression="follow_days" />
                    
                    <asp:TemplateField HeaderText="Author Email Sent">
                     <ItemTemplate>
                     
                        <%--<asp:Button ID="btn_mailsend" runat="server" Text="Mail Send" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        
                        <a target="_blank" title="Click to Email SAM Follow up" href="FollowupAuthorEmailPreview.aspx?&mailstring=Your article proofs&category=1&JOBID=<%# DataBinder.Eval(Container.DataItem,"job_id")%> &EMPID=<%# DataBinder.Eval(Container.DataItem,"username")%>">
                        <img id="Author_mail_sent"  runat="server" style="cursor:pointer;border:none "  src="images/temail.jpg" height="20" alt="Email" title="Email" onclick="btnEmailSent" visible='<%# (DataBinder.Eval(Container.DataItem,"follow_email_sent")==null || DataBinder.Eval(Container.DataItem,"follow_email_sent").ToString()=="" )? true : false %>' />
                        
                        
                        
                       <%--<asp:ImageButton ID="btnEmailSent" runat="server" ImageUrl="images/temail.jpg" height="20" CommandName="MailSent" />--%>
                       
                        </a>  
                        <asp:Label ID="lbl_authormailsent" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"follow_email_sent")!=null && DataBinder.Eval(Container.DataItem,"follow_email_sent").ToString()!="") ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"follow_email_sent")).ToShortDateString() : "" %>'></asp:Label>     
                       <asp:HiddenField ID="hf_jobid_Mailsent" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />                        
                        <%--<asp:HiddenField ID="hdn_emailsent" Value='<%#DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server" />--%>
                        <%--<asp:ImageButton ID="ibtn_mailsend" runat="server" height="20" ImageUrl="~/images/temail.jpg" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        
                    </ItemTemplate>
                    
                    
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Issue No" DataField="IssueNo" />
                   <asp:BoundField HeaderText="Editor Correction" DataField="correction_type" />
                    <asp:BoundField HeaderText="Author Reminder Followup Email" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                    <%--DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false"  />--%>
                    <%--<asp:BoundField DataField="AUTHORMAILSENT" HeaderText="Author Reminder Followup Email" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />--%>
                    <asp:TemplateField HeaderText="Author Reminder Mail Sent">
                    <ItemTemplate>
                       
                        <a target="_blank" title="Click to Email SAM Follow up" href="FollowupAuthorEmailPreview.aspx?&mailstring=Your article proofs&category=2&JOBID=<%# DataBinder.Eval(Container.DataItem,"job_id")%> &EMPID=<%# DataBinder.Eval(Container.DataItem,"username")%>">
                        <img id="Author_Follow_mail_sent"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# ( DataBinder.Eval(Container.DataItem,"follow_email_reminder_sent").ToString()=="" && DataBinder.Eval(Container.DataItem,"follow_email_sent").ToString()!="")? true : false %>' />
                        </a>
                        <asp:Label ID="lbl_authorRemaindermailsent" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"follow_email_reminder_sent")!=null && DataBinder.Eval(Container.DataItem,"follow_email_reminder_sent").ToString()!="") ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"follow_email_reminder_sent")).ToShortDateString() : "" %>'></asp:Label>     
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                    <ItemTemplate>
                    <asp:HiddenField ID="HF_femailsent" Value='<%#DataBinder.Eval(Container.DataItem,"follow_email_sent") %>' runat="server" />
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remove" >
                    <ItemTemplate>
                     <%--<asp:Button ID="btn_remove" runat="server" Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="Remove" />--%>
                        <asp:ImageButton ID="ibtn_remove" runat="server" ToolTip="Remove" ImageUrl="~/images/tools/no.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"job_history_id") %>' CommandName="Remove" />
                        <asp:HiddenField ID="hf_jobid1" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 </Columns>
                </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

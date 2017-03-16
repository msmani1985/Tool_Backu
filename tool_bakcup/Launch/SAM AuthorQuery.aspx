<%@ page language="C#" autoeventwireup="true" inherits="SAMAuthorQuery, App_Web_pvnuy-kx" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SAM Author/Query</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
     <style type="text/css">
 .RedColored  {     background: FF0000; } </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
       SAM Author Query
    </div>
    <div align="center" style="visibility:hidden; display:none;"><table class="bordertable"><tr><td>Despatched Date - </td><td>Start Date:</td><td><asp:TextBox ID="txt_startdate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_startdate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
                        <td>End Date</td><td><asp:TextBox ID="txt_enddate" runat="server"></asp:TextBox><img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txt_enddate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
    <td><asp:Button ID="btn_submit" Text="Submit" runat="server" CssClass="dpbutton" OnClick="btn_submit_Click" /></td>
    </tr></table></div>
    <br />
    <div id="div_samdetails" runat="server">
        <table align="center">
        <tr><td align="right"><asp:ImageButton ID="ibtn_save" runat="server" ImageUrl="~/images/tools/j_save.png" OnClick="ibtn_save_Click" /> <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click"  /></td></tr>
        <tr><td>
        
         <asp:GridView  Width="650px" ID="gv_samfollowup" runat="server" AutoGenerateColumns="false" 
                OnRowCommand="gridview_rowcommand"  OnRowDataBound="gridview_rowdatabound" OnSorting="GV_OnSortdata" 
                AllowSorting="true" >
                 <HeaderStyle CssClass="GVFixedHeader" />
                 <AlternatingRowStyle BackColor="#F2F2F2" />
                 <Columns>
                    <asp:BoundField HeaderText="Journal Code" DataField="journal_code" SortExpression="journal_code" />
                    <asp:BoundField HeaderText="Name" DataField="name" SortExpression="name" />
                    <asp:BoundField HeaderText="Stage Name" DataField="job_stage_name" SortExpression="job_stage_name" />
                    <asp:BoundField HeaderText="Despatch Date" DataField="despatch_date" SortExpression="despatch_date" Visible="false" />
                    <asp:BoundField HeaderText="Received Date" DataField="Received_Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                    <asp:BoundField HeaderText="Due Date" DataField="cats_due_date" SortExpression="cats_due_date" />
                    <asp:BoundField HeaderText="Follow Date" DataField="follow_date" SortExpression="follow_date" />
                    <asp:BoundField HeaderText="Follow Days" DataField="follow_days_author_query" SortExpression="follow_days" />
            
                <asp:TemplateField HeaderText="Author Email Sent">
                     <ItemTemplate>
                     
                        <%--<asp:Button ID="btn_mailsend" runat="server" Text="Mail Send" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        
                        <a target="_blank" title="Click to Email SAM Follow up" href="SAM AuthorQueryemailpreview.aspx?&mailstring=Author Query&category=1&JOBID=<%# DataBinder.Eval(Container.DataItem,"job_id")%> &EMPID=<%# DataBinder.Eval(Container.DataItem,"username")%>">
                        <img id="Author_mail_sent"  runat="server" style="cursor:pointer;border:none "  src="images/temail.jpg" height="20" alt="Email" title="Email" onclick="btnEmailSent" visible='<%# (DataBinder.Eval(Container.DataItem,"follow_email_sent")==null || DataBinder.Eval(Container.DataItem,"follow_email_sent").ToString()=="" )? true : false %>' />
                        
                        
                        
                       <%--<asp:ImageButton ID="btnEmailSent" runat="server" ImageUrl="images/temail.jpg" height="20" CommandName="MailSent" />--%>
                       
                        </a>  
                        <asp:Label ID="lbl_authormailsent" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem,"follow_email_sent")!=null && DataBinder.Eval(Container.DataItem,"follow_email_sent")!="") ? (DataBinder.Eval(Container.DataItem,"follow_email_sent")) : "" %>'></asp:Label>     
                       <asp:HiddenField ID="hf_jobid_Mailsent" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' />                        
                        <%--<asp:HiddenField ID="hdn_emailsent" Value='<%#DataBinder.Eval(Container.DataItem,"job_id") %>' runat="server" />--%>
                        <%--<asp:ImageButton ID="ibtn_mailsend" runat="server" height="20" ImageUrl="~/images/temail.jpg" CommWSSSSSSandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        
                    </ItemTemplate>
                    
                    
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Issue No" DataField="IssueNo" />
                
            
               
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <%--<asp:Button ID="btn_remove" runat="server" Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="Remove" />--%>
                        <asp:ImageButton ID="ibtn_remove" runat="server" ToolTip="Remove" ImageUrl="~/images/tools/no.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"job_id") %>' CommandName="Remove" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            </asp:GridView>
        </td></tr></table>
    </div>
    <div id="div_error" runat="server" class="errorMsg">No Records Found</div>
    </form>
</body>
</html>

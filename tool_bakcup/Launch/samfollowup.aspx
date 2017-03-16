<%@ page language="C#" autoeventwireup="true" inherits="samfollowup, App_Web_bsm-ipe9" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>SAM Author/PE Follow Up</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
     <style type="text/css">
 .RedColored  {     background: FF0000; } </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="dptitle">
        SAM Author/PE Follow Up
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
            <asp:GridView ID="gv_samfollowup" runat="server" AutoGenerateColumns="false" AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
    HeaderStyle-CssClass="darkbackground"  OnRowDataBound="gridview_rowdatabound" OnRowCommand="gridview_rowcommand" >
            <Columns>
                <asp:BoundField DataField="ARTICLECODE" HeaderText="Article Code" />
                <asp:BoundField DataField="STYPENAME" HeaderText="Article Stage" />
                <asp:BoundField DataField="ADESPATCHDATE" HeaderText="Despatch Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="RECEIVEDATE" HeaderText="Receive Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="CATSDUEDATE" HeaderText="Cats Due Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false"/>
                <asp:BoundField DataField="FOLLOWUPDATE" HeaderText="Followup Date" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false"  />
                <asp:BoundField DataField="FOLLOWDAYS" HeaderText="Follow Days" />
                <asp:TemplateField HeaderText="Author Email Sent">
                    <ItemTemplate>
                        <%--<asp:Button ID="btn_mailsend" runat="server" Text="Mail Send" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        <a target="_blank" title="Click to Email SAM Follow up" href="samfollowupemailpreview.aspx?AFIRSTAUTHOR=<%# DataBinder.Eval(Container.DataItem,"AFIRSTAUTHOR")%>&AEMAIL=<%# DataBinder.Eval(Container.DataItem,"AEMAIL") %>&AMNSTITLE=<%# DataBinder.Eval(Container.DataItem,"AMNSTITLE") %>&INVDISPLAYNAME=<%# DataBinder.Eval(Container.DataItem,"INVDISPLAYNAME") %>
                        &INVCONEMAIL=<%# DataBinder.Eval(Container.DataItem,"INVCONEMAIL") %>&AUTHORCORRECTION=<%# DataBinder.Eval(Container.DataItem,"AUTHORCORRECTION") %>&EDITORCORRECTION=<%# DataBinder.Eval(Container.DataItem,"EDITORCORRECTION") %>&AMANUSCRIPTID=<%# DataBinder.Eval(Container.DataItem,"AMANUSCRIPTID") %>
                        &JOURNAL_TITLE=<%#  DataBinder.Eval(Container.DataItem,"JOURNAL_TITLE").ToString().Replace("&","_")%>&CUSTNO=<%# DataBinder.Eval(Container.DataItem,"CUSTNO") %>&mailtype=1&mailstring=Your article proofs&ano=<%# DataBinder.Eval(Container.DataItem,"ano") %>&ARTICLECODE=<%# DataBinder.Eval(Container.DataItem,"ARTICLECODE") %>">
                            <img id="Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />
                        </a>                                
                        <%--<asp:ImageButton ID="ibtn_mailsend" runat="server" height="20" ImageUrl="~/images/temail.jpg" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="AuthorMail" Visible='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")==null || DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()=="" )? true : false %>' />--%>
                        <asp:Label ID="lbl_authormailsent" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")!=null && DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()!="") ? Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT")).ToShortDateString() : "" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AUTHORMAILSENT" HeaderText="" Visible="false" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                <asp:BoundField DataField="ISSUENO" HeaderText="Issue No" />
                <asp:BoundField DataField="BATCHSTYPENAME"  Visible="false"  HeaderText="Issue Stage" />
                <asp:BoundField DataField="" Visible="false" HeaderText="Author Correction" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false"  />
                <asp:BoundField DataField="EDITORCORRECTION" HeaderText="Editor Correction" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false"  />
                <asp:BoundField DataField="AUTHORMAILSENT" HeaderText="Author Reminder Followup Email" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                <asp:TemplateField HeaderText="Author Reminder Mail Sent">
                    <ItemTemplate>
                        <%--<asp:Button ID="btn_remindermail" runat="server" Text="Reminder Mail" CommandName="ReminderMail" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' Visible='<%# ( DataBinder.Eval(Container.DataItem,"AUTHOR_REMINDER_MAILSENT").ToString()!="")? false : true %>' />--%>
                        <a target="_blank" title="Click to Email SAM Follow up" href="samfollowupemailpreview.aspx?AFIRSTAUTHOR=<%# DataBinder.Eval(Container.DataItem,"AFIRSTAUTHOR")%>&AEMAIL=<%# DataBinder.Eval(Container.DataItem,"AEMAIL") %>&AMNSTITLE=<%# DataBinder.Eval(Container.DataItem,"AMNSTITLE") %>&INVDISPLAYNAME=<%# DataBinder.Eval(Container.DataItem,"INVDISPLAYNAME") %>
                        &INVCONEMAIL=<%# DataBinder.Eval(Container.DataItem,"INVCONEMAIL") %>&AUTHORCORRECTION=<%# DataBinder.Eval(Container.DataItem,"AUTHORCORRECTION") %>&EDITORCORRECTION=<%# DataBinder.Eval(Container.DataItem,"EDITORCORRECTION") %>&AMANUSCRIPTID=<%# DataBinder.Eval(Container.DataItem,"AMANUSCRIPTID") %>
                        &JOURNAL_TITLE=<%# DataBinder.Eval(Container.DataItem,"JOURNAL_TITLE").ToString().Replace("&","_")%>&CUSTNO=<%# DataBinder.Eval(Container.DataItem,"CUSTNO") %>&mailtype=2&mailstring=Reminder for your article proofs&ano=<%# DataBinder.Eval(Container.DataItem,"ano") %>&ARTICLECODE=<%# DataBinder.Eval(Container.DataItem,"ARTICLECODE") %>">
                            <img id="reminder_Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# ( DataBinder.Eval(Container.DataItem,"AUTHOR_REMINDER_MAILSENT").ToString()=="" && DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()!="")? true : false %>' />
                        </a>   
                        <%--<asp:ImageButton ID="ibtn_remindermail" runat="server" Height="20" ImageUrl="~/images/temail.jpg" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="ReminderMail" Visible='<%# ( DataBinder.Eval(Container.DataItem,"AUTHOR_REMINDER_MAILSENT").ToString()=="" && DataBinder.Eval(Container.DataItem,"AUTHORMAILSENT").ToString()!="")? true : false %>' />--%>
                        <asp:Label ID="lbl_reminder_mailsent"  runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"AUTHOR_REMINDER_MAILSENT").ToString()!="")?Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"AUTHOR_REMINDER_MAILSENT")).ToShortDateString() : "" %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="AUTHOR_REMINDER_MAILSENT" HeaderText="Author Reminder Mail Sent" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />--%>
                <asp:BoundField DataField="PEMAILSENT" Visible="false" HeaderText="PE Mail Sent on" DataFormatString="{0: MM/dd/yyyy}" HtmlEncode="false" />
                <asp:TemplateField HeaderText="PE Mail Sent" Visible="false" >
                <ItemTemplate>
                    <asp:DropDownList ID="dd_pemailsent" runat="server" ><asp:ListItem Text="No" Value="No" ></asp:ListItem><asp:ListItem Text="Yes" Value="Yes"></asp:ListItem></asp:DropDownList>
                    <asp:HiddenField ID="hf_ano" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"ano") %>' />
                </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <%--<asp:Button ID="btn_remove" runat="server" Text="Remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="Remove" />--%>
                        <asp:ImageButton ID="ibtn_remove" runat="server" ImageUrl="~/images/tools/no.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ano") %>' CommandName="Remove" />
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

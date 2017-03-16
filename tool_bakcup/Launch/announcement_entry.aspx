<%@ page language="C#" autoeventwireup="true" inherits="announcement_entry, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <title>New Announcement</title>
    <link href="default.css" rel="stylesheet" type="text/css" /> 
    <link href="scripts/messages.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/messages.js"></script>
    <script type="text/javascript">
      function validateForm() {
          var msgtitle;
          var mainmsg;
          var stdate; 
          var endate;
          if(document.form1.txtMessageTitle!=null)msgtitle = document.form1.txtMessageTitle.value;
          if(document.form1.txtMessage!=null)mainmsg = document.form1.txtMessage.value;
          if(document.form1.txtStartdate!=null)stdate = document.form1.txtStartdate.value;          
          if(document.form1.txtEnddate!=null)endate = document.form1.txtEnddate.value;
          if(msgtitle == "") {
            inlineMsg('txtMessageTitle','You must enter Announcement Title.',2);
            return false;
          }    
          if(mainmsg == "") {
            inlineMsg('txtMessage','You must enter Announcement message.',2);
            return false;
          }     
          if(stdate == "") {
            inlineMsg('txtStartdate','You must select Start Date.',2);
            return false;
          }
          if(endate == "") {
            inlineMsg('txtEnddate','You must select Expiry date.',2);
            return false;
          }
          return true;
        }  
        
        function confirmDelete(){
        if (!confirm('Confirm Delete?')) return false;
        }
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="dptitle">
            New Announcement&nbsp;</div>
    
    </div>
        <table align="center" cellpadding="2" cellspacing="5" class="bordertable" width="750">
            <tr>
                <td colspan="6" align="center">
                <span id="Messagebox" runat="server" style="font-size: 8pt; background-color: Yellow;
                                            color: Black;"></span>
                </td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Title<span style="font-size: 9pt; color: #ff0000">*</span></td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:TextBox ID="txtMessageTitle" runat="server" Width="420px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Type<span style="font-size: 9pt; color: #ff0000">*</span></td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:DropDownList ID="drpAnnType" runat="server" Width="425px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Post By</td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:Label ID="lblPostedby" runat="server" Width="422px" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Posting To<span style="font-size: 9pt; color: #ff0000">*</span></td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:DropDownList ID="drpTeam" runat="server" Width="425px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 120px" valign="top">
                    Message<span style="font-size: 9pt; color: #ff0000">*</span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td style="width: 12px">
                    :<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
                <td colspan="4" valign="top">
                    <asp:TextBox ID="txtMessage" runat="server" CssClass="TxtBox" Height="135px" TextMode="MultiLine"
                        Width="420px" MaxLength="1000"></asp:TextBox>
                    (max 1000 chars.)</td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Start Date<span style="font-size: 9pt; color: #ff0000">*</span></td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:TextBox ID="txtStartdate" runat="server" CssClass="TxtBox" Width="99px" MaxLength="10"></asp:TextBox>
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtStartdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
            </tr>
            <tr>
                <td style="width: 120px">
                    Expiry Date<span style="font-size: 9pt; color: #ff0000">*</span></td>
                <td style="width: 12px">
                    :</td>
                <td colspan="4">
                    <asp:TextBox ID="txtEnddate" runat="server" CssClass="TxtBox" Width="99px" MaxLength="10"></asp:TextBox>
                    <img alt="Calendar" border="0"
                        height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtEnddate','calendar_window','width=180,height=170,left=450,top=400,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                        src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                        border-left-style: none; border-bottom-style: none" /></td>
            </tr>
            <tr>
                <td style="width: 120px">
                </td>
                <td style="width: 12px">
                </td>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td align="right" colspan="6">
                    <!--<input id="Button1" class="dpbutton" type="button" value="Show Summary" onclick="javascript:document.all.dgrdAnnounList.focus();" style="width: 91pt" />-->
                    <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" OnClick="btnSave_Click"
                        Text="Save" OnClientClick="return validateForm();" /></td>
            </tr>
        </table>
        <div class="dptitle">
            Announcement Summary</div>        
        <table align="center" cellpadding="2" cellspacing="5" class="bordertable" width="750">
            <tr>
                <td colspan="6">
                    <asp:DataGrid ID="dgrdAnnounList" runat="server" CellPadding="4" ForeColor="#333333"
                        GridLines="None" Width="100%" AutoGenerateColumns="False" CellSpacing="1">
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <EditItemStyle BackColor="#7C6F57" />
                        <SelectedItemStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <ItemStyle BackColor="#E3EAEB" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Size="X-Small" Font-Strikeout="False" Font-Underline="False" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Sno.">
                                <ItemTemplate>
                                    <asp:Label ID="lblSno" runat="server" Text="<%# id=id+1 %>"></asp:Label>
                                    <asp:HiddenField ID="hfAnnounceID" runat="server" Value='<%# Eval("announcement_id") %>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Title">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnntitle" runat="server" Text='<%# Eval("ANNOUNCEMENT_TITLE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Posted By">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedbygrd" runat="server" Text='<%# Eval("POSTED_BY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Posted To">
                                <ItemTemplate>
                                    <asp:Label ID="lblPostedto" runat="server" Text='<%# Eval("POSTED_TO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Start Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblstdate" runat="server" Text='<%# Eval("STARTDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Expiry Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblexpdate" runat="server" Text='<%# Eval("EXPIRYDATE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>                                    
                                    <asp:CheckBox ID="chkDelete" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td colspan="6" align="right"><asp:Button ID="btnDelete" runat="server" CssClass="dpbutton"
                        Text="Delete" OnClientClick="return confirmDelete()" OnClick="btnDelete_Click"/></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>

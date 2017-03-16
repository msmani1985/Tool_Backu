<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Lead_Followup.aspx.cs" Inherits="Lead_Followup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" type="text/css" rel="stylesheet" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <div id="invreport" class="dptitle">
                <asp:Label ID="lblTitle" runat="server">Lead FollowUp</asp:Label></div>
        </div>
        <table align="center" cellpadding="2" cellspacing="2" class="bordertable" style="width: 70%">
            <tr>
                <td align="center" colspan="10" rowspan="1">
                    Country: 
                    <asp:DropDownList ID="drpCountry" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click"
                        Text="Submit" />
                    </td>
            </tr>
        </table>
        <br />
    <div id="div_samdetails" runat="server">
        <table align="center">
            <tr>
                <td>           
                    <asp:GridView ID="gv_Leadfollowup" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Left" 
                            CellPadding="4" Font-Names="Segoe UI" Font-Size="11px" ForeColor="#333333" 
                            GridLines="Vertical"  Width="100%" ShowHeaderWhenEmpty="True" OnRowDataBound="gv_Leadfollowup_rowdatabound" OnRowCommand="gv_Leadfollowup_rowcommand">
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" /> 
                            <rowstyle backcolor="white" />
                            <alternatingrowstyle backcolor="#F0FFF0" />
                            <Columns>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hf_lno" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"salesleadno") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="slcompany" HeaderText="Customer Name" />
                                <asp:BoundField DataField="ctyname" HeaderText="Country"/>
                                <asp:BoundField DataField="ci_name" HeaderText="Contact Person"/>
                                <asp:TemplateField HeaderText="Introduction Letter">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img1"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"Email_sent_Date")==null || DataBinder.Eval(Container.DataItem,"Email_sent_Date").ToString()=="" )? true : false %>' /> </a>                                
                                        <asp:Label ID="lbl_authormailsent" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Email_sent_Date")!=null && DataBinder.Eval(Container.DataItem,"Email_sent_Date").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Email_sent_Date1") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowUp 1">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email Follow up" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img11"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email"  visible='<%# (DataBinder.Eval(Container.DataItem,"Followup1_Flag").ToString()=="N" && DataBinder.Eval(Container.DataItem,"EmailSent_Flag").ToString()=="Y" )? true : false %>' /> </a>                                  
                                        <asp:Label ID="lbl_authormailsent1" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Followup1")!=null && DataBinder.Eval(Container.DataItem,"Followup1").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Followup11") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowUp 2">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email Follow up" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img12"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"Followup2_Flag").ToString()=="N" && DataBinder.Eval(Container.DataItem,"Followup1_Flag").ToString()=="Y" )? true : false %>' /> </a>                                
                                        <asp:Label ID="lbl_authormailsent2" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Followup2")!=null && DataBinder.Eval(Container.DataItem,"Followup2").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Followup12") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowUp 3">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email Follow up" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img13"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"Followup3_Flag").ToString()=="N" && DataBinder.Eval(Container.DataItem,"Followup2_Flag").ToString()=="Y" )? true : false %>' /> </a>                                
                                        <asp:Label ID="lbl_authormailsent3" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Followup3")!=null && DataBinder.Eval(Container.DataItem,"Followup3").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Followup13") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowUp 4">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email Follow up" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img14"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"Followup4_Flag").ToString()=="N" && DataBinder.Eval(Container.DataItem,"Followup3_Flag").ToString()=="Y" )? true : false %>' /> </a>                                
                                        <asp:Label ID="lbl_authormailsent4" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Followup4")!=null && DataBinder.Eval(Container.DataItem,"Followup4").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Followup14") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FollowUp 5">
                                    <ItemTemplate>
                                        <a target="_blank" title="Click to Email Follow up" href="Lead_EmailPreview.aspx?ContactPerson=<%# DataBinder.Eval(Container.DataItem,"ci_name")%>&EMAIL=<%# DataBinder.Eval(Container.DataItem,"ci_email") %>&company=<%# DataBinder.Eval(Container.DataItem,"slcompany") %>&Salesleadno=<%# DataBinder.Eval(Container.DataItem,"Salesleadno") %>">
                                            <img id="Img15"  style="cursor:pointer;border:none " runat="server" src="images/temail.jpg" height="20" alt="Email" title="Email" visible='<%# (DataBinder.Eval(Container.DataItem,"Followup5_Flag").ToString()=="N" && DataBinder.Eval(Container.DataItem,"Followup4_Flag").ToString()=="Y" )? true : false %>' /> </a>                                
                                        <asp:Label ID="lbl_authormailsent5" runat="server" Text='<%# (DataBinder.Eval(Container.DataItem,"Followup5")!=null && DataBinder.Eval(Container.DataItem,"Followup5").ToString()!="") ? DataBinder.Eval(Container.DataItem,"Followup15") : "" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remove">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ibtn_remove" runat="server" ImageUrl="~/images/tools/no.png" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"salesleadno") %>' CommandName="Remove" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="Contacts, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contacts</title>
    <link href="default.css" rel="stylesheet" type="text/css" />    
    <script language="javascript">
    function validEmail(elem){
    var reg = /^([A-Za-z0-9_\-\.\+])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    document.getElementById(elem).value = document.getElementById(elem).value.replace(',',';');
    var addr = document.getElementById(elem).value;
    var address = addr.split(";");   
    if(address!=null && address!="" && address.length>0){
        for(var i=0;i<address.length;i++){
           if(reg.test(address[i]) == false){
              alert('Invalid Email Address: '+address[i]);
              //alert(document.getElementById('lblError'));
              document.getElementById(elem).select();
              return false;
    }}}
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="tabSummary" runat="server">
            <br />
            <div class="dptitle">
                Contact Summary</div>
            <table align="center" cellpadding="1" cellspacing="1" class="bordertable" width="750">
                <tr>
                    <td colspan="6" valign="top">
                        Customer: &nbsp;<asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan="6" valign="top">
                        First
                        Name:
                        <asp:TextBox ID="txtFname" runat="server" CssClass="TxtBox" Width="317px"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnSearch" runat="server" CssClass="dpbutton" Text="Search"
                            OnClick="btnSearch_Click" />&nbsp;<asp:Button ID="btnNew" runat="server" CssClass="dpbutton"
                                OnClick="btnNew_Click" Text="New" />&nbsp;
                        <asp:PlaceHolder ID="phSummary" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:DataGrid ID="dgrdContacts" runat="server" AutoGenerateColumns="False" Width="100%" OnItemDataBound="dgrdContacts_ItemDataBound" OnItemCommand="dgrdContacts_ItemCommand" CssClass="lightbackground">
                            <SelectedItemStyle
                                Font-Size="X-Small" />
                            <AlternatingItemStyle
                                Font-Size="X-Small" CssClass="dullbackground" />
                            <ItemStyle
                                Font-Size="X-Small" />
                            <HeaderStyle CssClass="darkbackground" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Sno.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text="<%# id=id+1 %>"></asp:Label>
                                        <asp:HiddenField ID="hfContactID" runat="server" Value='<%# Eval("contact_id") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactDispName" runat="server" Text='<%# Eval("display_name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblContactEmail1" runat="server" Text='<%# Eval("email1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnEdit" ImageUrl="~/images/tools/edit.png" CommandName="Edit" ToolTip="Edit" runat="server" />
                                    &nbsp;
                                        <img id="imgCheck" src="images/tools/yes.png" border="0" runat="server" alt="Select"
                                            style="cursor: pointer" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid></td>
                </tr>
            </table>
        </div>
        <div id="tabMaster" runat="server">
            <br />
            <div class="dptitle">
                Add/Edit Contact</div>
            <table align="center" cellpadding="1" cellspacing="1" class="bordertable" width="650">
                <tr>
                    <td colspan="6" align="right">
                        <span style="font-size: 8pt; color: #ff0000">* mandatory fields</span></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Customer <span style="font-size: 9pt; color: #ff0000">*</span></td>
                    <td style="width: 12px">
                    </td>
                    <td colspan="4">
                        <asp:DropDownList ID="drpCustomer" runat="server">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Title</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtTitle" runat="server" CssClass="TxtBox"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Display Name<span style="font-size: 9pt; color: #ff0000">*</span></td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtDisplayName" BackColor="#FFFFC0" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        First Name<span style="font-size: 9pt; color: #ff0000">*</span></td>
                    <td style="width: 12px; font-size: 8pt;">
                        <span style="color: #000000">:</span></td>
                    <td colspan="4" style="font-size: 8pt; color: #ff0000">
                        <asp:TextBox ID="txtFirstName" BackColor="#FFFFC0" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Last Name</td>
                    <td style="width: 12px; font-size: 8pt;">
                        <span style="color: #000000">:</span></td>
                    <td colspan="4" style="font-size: 8pt; color: #ff0000">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Sur Name<span style="font-size: 9pt; color: #ff0000">*</span></td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtSurName" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Address</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="TxtBox" Width="300px" Height="56px" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Email 1<span style="font-size: 9pt; color: #ff0000">*</span></td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmail1" BackColor="#FFFFC0" runat="server" onblur="javascript:validEmail('txtEmail1');" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Email 2</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmail2" runat="server" CssClass="TxtBox" onblur="javascript:validEmail('txtEmail2');" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Email 3</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtEmail3" runat="server" CssClass="TxtBox" onblur="javascript:validEmail('txtEmail3');" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Fax 1</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtFax1" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Fax 2</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtFax2" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Phone 1</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPhone1" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Phone 2</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPhone2" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Phone 3</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtPhone3" runat="server" CssClass="TxtBox" Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 120px">
                        Comments</td>
                    <td style="width: 12px">
                        :</td>
                    <td colspan="4">
                        <asp:TextBox ID="txtComments" runat="server" CssClass="TxtBox" Height="56px" TextMode="MultiLine"
                            Width="300px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right" colspan="6">
                        <!--<input id="Button1" class="dpbutton" type="button" value="Show Summary" onclick="javascript:document.all.dgrdAnnounList.focus();" style="width: 91pt" />--><asp:Button ID="Button1" runat="server" CssClass="dpbutton"
                                OnClick="btnNew_Click" Text="New" />
                        <asp:Button ID="btnSave" runat="server" CssClass="dpbutton" OnClick="btnSave_Click" Text="Save" />
                        <asp:Button ID="btnFind" runat="server" CssClass="dpbutton" OnClick="btnFind_Click"
                            Text="Search" />&nbsp;
                        <asp:PlaceHolder ID="phMaster" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

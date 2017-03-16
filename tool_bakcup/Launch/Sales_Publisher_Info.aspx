<%@ page language="C#" autoeventwireup="true" inherits="Sales_Publisher_Info, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" rel="stylesheet" type="text/css" />
    <script language="javascript">
    function show_web()
    {
        if (document.getElementById("txtWeb").value != "")
        {
            var url = document.getElementById("txtWeb").value;
            if(url.indexOf('http://')==-1 && url.indexOf('https://')==-1) 
                url = "http://"+url;
            window.open(url, "_blank");
         }
            
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Publisher Information
                <asp:Button ID="btndrpPostback" runat="server" OnClick="btndrpPostback_Click" BackColor="white"
                    Text="Button" ForeColor="Transparent" Width="0px" Height="0px" Style="background-color: white;
                    border: 0px 0px 0px 0px; width: 0px; height: 0px" BorderStyle="None" BorderWidth="0px" />
                    
                    </div>
            <table cellspacing="0" cellpadding="1" border="0" width="100%" class="bordertable">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="1" border="0" width="100%" id="PubInfo" runat="server">
                            <tr class="dpJobGreenHeader">
                                <td  colspan="4" style="height: 25px">
                                    Publisher Information </td>
                                    
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                    Publisher Name<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td colspan="3">
                                    <asp:DropDownList ID="drpPublisher" runat="server" Width="560px" OnPreRender="drpPublisher_PreRender">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgPubNewInfo" runat="server" ImageUrl="~/images/tools/new.png"
                                        OnClick="ImgPubNewInfo_Click" ToolTip="New Publisher" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                    Publisher Name <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td colspan="3" valign="top">
                                    <asp:TextBox ID="txtCompany" Width="556px" runat="server" MaxLength="200" CssClass="TxtBox"
                                        BackColor="#FFFFC0"></asp:TextBox><span style="font-size: 9pt; color: #ff0000"></span>&nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                    Status<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td colspan="3" valign="top">
                                    <asp:DropDownList ID="drpStatus" runat="server" Width="560px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Type<span style="font-size: 9pt; color: #ff0000">*<br />
                                        <br />
                                        <br />
                                    </span>
                                </td>
                                <td valign="top" style="width: 306px">
                                    <asp:ListBox ID="lstJTNO" runat="server" Width="300px" SelectionMode="Multiple" CssClass="TxtBox">
                                        <asp:ListItem Value="0">-- Choose a Job --</asp:ListItem>
                                        <asp:ListItem Value="1">Books</asp:ListItem>
                                        <asp:ListItem Value="2">Journals</asp:ListItem>
                                        <asp:ListItem Value="3">Projects</asp:ListItem>
                                    </asp:ListBox>
                                </td>
                                <td valign="top">
                                    Category<span style="font-size: 9pt; color: #ff0000">*<br />
                                        <br />
                                        <br />
                                    </span>
                                </td>
                                <td valign="top" style="width: 289px">
                                    <asp:ListBox ID="lstCategory" runat="server" Width="270px" SelectionMode="Multiple"
                                        CssClass="TxtBox"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Description<br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                                <td colspan="3" class="value">
                                    <asp:TextBox ID="txtDescription" Width="680px" Height="70px" TextMode="MultiLine"
                                        runat="server" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Address<br />
                                    <br />
                                    <br />
                                </td>
                                <td colspan="3" class="value">
                                    <asp:TextBox ID="txtAddress" Width="300px" Height="42px" runat="server" CssClass="TxtBox"
                                        TextMode="MultiLine"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    City</td>
                                <td style="width: 306px">
                                    <asp:TextBox ID="txtCity" Width="300px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                                <td>
                                    Post Code</td>
                                <td style="width: 289px">
                                    <asp:TextBox ID="txtPocode" Width="266px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    State</td>
                                <td style="width: 306px">
                                    <asp:TextBox ID="txtState" Width="300" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                                <td style="width: 75px">
                                    Country<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td style="width: 289px">
                                    <asp:DropDownList ID="drpcountry" runat="server" Width="270px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table border="0" width="100%" cellspacing="0" cellpadding="1" id="ConInfo" runat="server">
                            <tr class="dpJobGreenHeader">
                                <td colspan="4" style="border-top-color: Green; border-top-style: solid; border-top-width: 1px;
                                    height: 25px;">
                                    Contact Information</td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Contact Name</td>
                                <td colspan="3" class="value">
                                    <asp:DropDownList ID="drpcontactName" runat="server" Width="304" OnSelectedIndexChanged="drpcontactName_SelectedIndexChanged"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgConNewInfo" runat="server" ImageUrl="~/images/tools/new.png"
                                        OnClick="ImgConNewInfo_Click" ToolTip="New Contact" /></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Contact Name</td>
                                <td style="width: 335px">
                                    <asp:TextBox ID="txtcontact" Width="300" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                                <td>
                                    Contact Title</td>
                                <td>
                                    <asp:TextBox ID="txtConTitle" Width="266px" runat="server" MaxLength="100" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px; height: 21px;">
                                    Phone</td>
                                <td style="width: 335px; height: 21px;">
                                    <asp:TextBox ID="txtPhone" Width="300" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                                <td style="width: 75px; height: 21px;">
                                    Fax</td>
                                <td style="height: 21px">
                                    <asp:TextBox ID="txtFax" Width="266px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    E-Mail</td>
                                <td style="width: 335px">
                                    <asp:TextBox ID="txtEmail" Width="300" runat="server" MaxLength="100" CssClass="TxtBox"></asp:TextBox></td>
                                <td>
                                    Web</td>
                                <td>
                                    <asp:TextBox ID="txtWeb" Width="266px" runat="server" MaxLength="200" CssClass="TxtBox"></asp:TextBox>
                                    <img id="imgWeb" src ="images/tools/refresh.png" style="cursor:pointer" onclick="javascript:show_web();" title="Go" /> </td>
                            </tr>
                        </table>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSaveDetails" runat="server" Text="Save Details" CssClass="dpbutton"
                            Width="110px" OnClick="btnSaveDetails_Click" /></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

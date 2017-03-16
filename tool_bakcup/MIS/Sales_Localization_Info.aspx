<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sales_Localization_Info.aspx.cs"
    Inherits="Sales_Localization_Info" %>

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
    <link href="jquery/jquery-ui.css" rel="stylesheet" type="text/css" />  
    <script src="jquery/jquery.min.js" type="text/javascript"></script>  
    <script src="jquery/jquery-ui.min.js" type="text/javascript"></script>  
      
    <script type="text/javascript">
        $(document).ready(function () {
            SearchText();
        });
        function SearchText() {
            $("#txtState").autocomplete({
                source: function (request, response) {
                    var obj = {};
                    obj.ID = $.trim($("[id*=drpcountry]").val());
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Sales_Localization_Info.aspx/Article?",
                        data: JSON.stringify(obj),//"{'ID':'" + document.getElementById('txtArtCode').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        }
                    });
                }
            });
        }
    </script> 
    <script type="text/javascript">
        function Click() {
            var t = document.getElementById("txtPhone").value;
            var s_a = document.getElementById("mylink");
            s_a.href = "tel:" + t;
        }
    </script>
    <%--<script type="text/javascript">
        function UpdateLink() {
                    var NavValue = document.getElementById("<%=txtPhone.ClientID%>").value;
            document.getElementById("myLnk").href = "tel:" + NavValue;
        }
    </script> --%> 
    <style type="text/css">
        .auto-style1 {
            width: 106px;
            height: 19px;
        }
        .auto-style2 {
            height: 19px;
        }
        #ctl00_XXX {
            width: 16px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="dptitle">
                Lead Information
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
                                    Lead Information </td>
                                    
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                    Customer Name<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td colspan="2">
                                    <asp:DropDownList ID="drpPublisher" runat="server" OnPreRender="drpPublisher_PreRender" Height="20px" Width="400px">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImgPubNewInfo" runat="server" ImageUrl="~/images/tools/new.png"
                                        OnClick="ImgPubNewInfo_Click" ToolTip="New Publisher" />
                                </td>
                                <td align="left">

                                    <asp:RadioButtonList ID="rbSorting" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbSorting_SelectedIndexChanged">
                                        <asp:ListItem>CustomerWise</asp:ListItem>
                                        <asp:ListItem Selected="True">CountryWise</asp:ListItem>
                                    </asp:RadioButtonList>

                                </td>
                            </tr>
                            <tr>
                                <td style="width: 106px">
                                    Customer Name <span style="font-size: 9pt; color: #ff0000">*</span></td>
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
                                    Notes<br />
                                    <br />
                                    <br />
                                    <br />
                                </td>
                                <td colspan="3" class="value">
                                    <asp:TextBox ID="txtDescription" Width="680px" Height="70px" TextMode="MultiLine"
                                        runat="server" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" >
                                    <span style="font-size: 9pt"><b>Address:</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 75px">
                                    Country<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td style="width: 289px">
                                    <asp:DropDownList ID="drpcountry" runat="server" Width="270px" AutoPostBack="true" OnSelectedIndexChanged="drpcountry_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td valign="top" style="width: 106px">
                                    State</td>
                                <td style="width: 306px">
                                    <asp:DropDownList ID="dropState" runat="server" Width="270px">
                                    </asp:DropDownList>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/tools/new.png"
                                        ToolTip="New State" OnClick="ImageButton1_Click" />
                                    </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Line 1</td>
                                <td class="value">
                                    <asp:TextBox ID="txtAddress" runat="server" Width="300" CssClass="TxtBox"></asp:TextBox>
                                </td>
                                <td colspan="2" align="center">
                                    <asp:TextBox ID="txtNewState" runat="server" Width="165px" CssClass="TxtBox" Visible="false"></asp:TextBox>
                                    <asp:Button ID="btnAddState" runat="server" Text="Add" CssClass="dpbutton" Visible="false"
                                        Width="37px" OnClick="btnAddState_Click"  />
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Line 2</td>
                                <td colspan="3" class="value">
                                    <asp:TextBox ID="txtAddress1" runat="server" Width="300" CssClass="TxtBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" class="auto-style1">
                                    Line 3</td>
                                <td class="auto-style2">
                                    <asp:TextBox ID="txtAddress2" runat="server" Width="300" CssClass="TxtBox"></asp:TextBox>
                                </td>
                                 <td valign="top" style="width: 106px">
                                    City</td>
                                <td style="width: 306px">
                                    <asp:TextBox ID="txtCity" Width="300px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td valign="top" style="width: 106px">
                                    Line 4</td>
                                <td  class="value">
                                    <asp:TextBox ID="txtAddress3" runat="server" Width="300" CssClass="TxtBox"></asp:TextBox>
                                </td>
                                <td>
                                    Post Code</td>
                                <td style="width: 289px">
                                    <asp:TextBox ID="txtPocode" Width="266px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
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
                                    <asp:TextBox ID="txtPhone" Width="249px" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox>
                                    <a id="mylink" href="javascript:void(0)" onclick="Click()"><img id="ctl00_XXX" src="images/skype.png" /></a>
                                </td>
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
                            <tr>
                                <td valign="top" style="width: 106px; height: 21px;">
                                    Skype ID</td>
                                <td style="width: 335px; height: 21px;">
                                    <asp:TextBox ID="txtSkype" Width="300" runat="server" MaxLength="50" CssClass="TxtBox"></asp:TextBox></td>
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

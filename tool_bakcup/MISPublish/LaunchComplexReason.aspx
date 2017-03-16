<%@ page language="C#" autoeventwireup="true" inherits="LaunchComplexReason, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href=default.css type="text/css" rel="stylesheet" />
     <script type="text/javascript">

    function imgBD_editor_onclick() {

            window.open("Launchcontacts.aspx?form=Projects&type=0&trgname=txtProjectEditor&trgid=hfprojectEditorId&cid="+document.form1.drpProjectcustomer.value+"&lid="+document.form1.DropLocation.value,"Contacts","width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer"); 
    }
  
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div class="content" id="tabLoc" runat="server">
     
     <div class="dptitle">Complex Reason</div>
     <div >
<table align="center" class="bordertable" cellpadding="2" cellspacing="0" border="0" style="width: 59%" >
                                        
                                                <tr>
                                                <td style="width: 164px; height: 23px">
                                                Complexity Level:
                                                </td>
                                                <td style="width: 141px; height: 23px">
                                                <asp:DropDownList ID="DropComReason" runat="server">
                                                </asp:DropDownList>
                                                </td>
                                                <td style="height: 23px">
                                                </td>
                                                <td style="height: 23px"></td>
                                                </tr>
                                                <tr>
                                                <td>
                                                Task:
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lboxtaskReason" runat="server" OnSelectedIndexChanged="lboxtaskReason_SelectedIndexChanged"  AutoPostBack="True"></asp:ListBox>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblFormatreason" runat="server" Text="Format:" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:ListBox ID="lboxformatreason" runat="server" Visible="False"></asp:ListBox>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <asp:Label ID="lblSourcetypereason" runat="server" Text="Source Type:" Visible="False"></asp:Label>
                                                </td>
                                                <td>
                                                <asp:DropDownList ID="DropSourceTypeReason" runat="server" Width="91px" Visible="False" >
                                                <asp:ListItem ></asp:ListItem>
                                                <asp:ListItem>Editable</asp:ListItem>
                                                <asp:ListItem>Scanned</asp:ListItem>
                                                <asp:ListItem Value="Editable and Scanned">Editable and Scanned </asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                New Reason:
                                                </td>
                                                <td colspan="2">
                                                <asp:TextBox ID="txtNewReason" runat="server" Width="183px"></asp:TextBox>
                                                </td>
                                                </tr>
                                                <tr>
                                                <td colspan="4" align="center">
                                                <asp:Button ID="btnSaveReason" runat="server" Text="Save" CssClass="dpbutton" OnClick="btnSaveReason_Click" />
                                                </td>
                                                </tr>
                                            </table></div>
    </div>
    </div>
    </form>
</body>
</html>

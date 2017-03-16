<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FollowupAuthorEmailPreview.aspx.cs" Inherits="FollowupAuthorEmailPreview" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Follow Up Author Email Preview</title>
    
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    
    <script type="text/javascript">
			WYSIWYG.attach('lblBody',small);			
    </script>
    <script>
    function CloseWindow()
    {
    window.close();
    }
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                    <tr>
                        <td colspan="3" style="text-indent:5px; background:green;color:White;font-weight:bold;height:30px;vertical-align:middle; ">Follow up Author Emailing System</td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="nathiya@datapage.org"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessage"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="lblToAddress" runat="server" Text=""></asp:TextBox><asp:Label ID="Label1" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="lblCCAddress" runat="server" Width="250px" Text=""></asp:TextBox></td> </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="lblBCCAddress" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="lblSubject" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                        <%--<tr><td style="vertical-align:top;" valign="top"  colspan="3"><asp:TextBox Height="250px" Width="550px" TextMode="MultiLine" ID="lblBody1" runat="server" Text=""></asp:TextBox> </td></tr>--%>
                        <tr><td style="vertical-align:top;" valign="top" colspan="3">
                            <%--<div style="border:solid 1px gray;Height:250px;Width:550px;overflow:auto;" id="lblBody" runat="server" contenteditable="true" enableviewstate="true" ></div> --%>
                            <textarea id="lblBody" style="border:solid 1px gray;Height:250px;Width:100%;" runat="server"></textarea>
                            </td></tr>
                    </table>
                </td>
                <td>
                    <div id="div_notupdateinvoice" runat="server">
                    </div>    
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

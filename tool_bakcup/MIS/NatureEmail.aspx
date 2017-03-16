<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NatureEmail.aspx.cs" Inherits="NatureEmail"  validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
        <title>Nature Email Preview</title>
        <%--  <link href="default.css" rel="stylesheet" type="text/css" />--%>
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    <script type="text/javascript" language="javascript">
        WYSIWYG.attach('lblBody', small);

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
         <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top; height: 454px;" width="650px">
                    <tr>
                        <td colspan="3" style="text-indent:5px; background:green;color:White;font-weight:bold;height:30px;vertical-align:middle; ">Nature Emailing System</td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="scientificreportsproofs@nature.com"></asp:label></td>
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
                           <tr>
                        <td>
                            &nbsp;</td>
                        
                        <td colspan="2">
 
                              <asp:HyperLink   
             ID="hyperlink1"  
             runat="server"  
          
           
             >  
        </asp:HyperLink> </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top" colspan="3">
                            <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>
                            </td></tr>
                    </table>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

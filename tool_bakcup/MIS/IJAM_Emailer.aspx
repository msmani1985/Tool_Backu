<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IJAM_Emailer.aspx.cs" Inherits="IJAM_Emailer" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
        <title>IJAM Email Preview</title>
        <%--  <link href="default.css" rel="stylesheet" type="text/css" />--%>
        <link href="default.css" rel="stylesheet" type="text/css" />    
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    <script type="text/javascript" language="javascript">
        WYSIWYG.attach('lblBody', small);

    </script>
    <script type="text/javascript" language="javascript">
        WYSIWYG.attach('lblBody1', small);

    </script>
     <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        

        .auto-style2 {
            width: 852px;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr  class="dpJobGreenHeader">
                <td colspan="3" style="text-indent:5px;" class="auto-style2">IJAM Emailing System</td>

            </tr>
        </table>
            <br />
        <br />
        <br />
                                        <ol id="toc">
                                    <li id="mieProof" runat="server">
                                        <asp:LinkButton ID="lnkeProof" runat="server" OnClick="lnkeProof_Click" TabIndex="1">eProof</asp:LinkButton></li>
                                  
                                   
                                    
                                </ol>
    
           <div class="content" id="tabeProofs" runat="server">
            <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top; height: 454px;" width="850px">
                   <tr class="dpJobGreenHeader">
                        
                          <td colspan="4" style="height: 32px">
                                                <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="lblIJAMeProofs" runat="server" Text="IJAM eProofs mailer"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="IJAM@charlesworth-group.com"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"   />
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
 
                              <asp:TextBox ID="txtArticleId" runat="server"></asp:TextBox>
                              <asp:Button ID="btnGenLink" runat="server" OnClick="btnGenLink_Click" Text="Generate Link" />
                               </td>
                        </tr>
                           <tr>
                        <td>
                            &nbsp;</td>
                        
                        <td colspan="2">
 
                              <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                               </td>
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
   
        
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Columbia_Emailer.aspx.cs" Inherits="Columbia_Emailer" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    
        <title>Columbia Email Preview</title>
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
                <td colspan="3" style="text-indent:5px;" class="auto-style2">Columbia Emailing System</td>

            </tr>
        </table>
            <br />
        <br />
        <br />
                                        <ol id="toc">
                                    <li id="mieProof" runat="server">
                                        <asp:LinkButton ID="lnkeProof" runat="server" OnClick="lnkeProof_Click" TabIndex="1">eProof</asp:LinkButton></li>
                                    <li id="miRemainder" runat="server">
                                        <asp:LinkButton ID="lnkRemainder" runat="server" OnClick="lnkRemainder_Click" TabIndex="2">Remainder eProof</asp:LinkButton></li>
                                   
                                    
                                </ol>
    
           <div class="content" id="tabeProofs" runat="server">
            <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top; height: 454px;" width="850px">
                   <tr class="dpJobGreenHeader">
                        
                          <td colspan="4" style="height: 32px">
                                                <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="lblColumbiaeProofs" runat="server" Text="Columbia eProofs mailer"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="columbia@charlesworth-group.com"></asp:label></td>
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
   
          <div class="content" id="tabeProofsRemainder" runat="server">
            <table >
            <tr>
                <td>
                    <table id="Table1" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top; height: 454px;" width="850px">
                   <tr class="dpJobGreenHeader">
                        
                           <td colspan="4" style="height: 32px">
                                                <img id="Img1" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="Label5" runat="server" Text="Columbia eProofs Remainder mailer"></asp:Label></td>
                        
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFrom1" runat="server" Text="columbia@charlesworth-group.com"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit_Remainder" runat="server" OnClick="btnSubmit_Remainder_Click"   />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessage1"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="txtTo1" runat="server" Text=""></asp:TextBox><asp:Label ID="Label4" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="txtCC1" runat="server" Width="250px" Text=""></asp:TextBox></td> </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="txtBCC1" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="txtSubject1" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                           <tr>
                        <td>
                            <strong>Attachment: </strong>
                               </td>
                        
                        <td colspan="2">
 
                            <asp:FileUpload ID="File_upload_1" runat="server" Width="390px"  />
                               </td>
                        </tr>
                           <tr>
                        <td>
                            &nbsp;</td>
                        
                        <td colspan="2">
 
                        <asp:LinkButton ID="HlnkloadFile_1" Text="View 1'st Attachment" runat="server"  OnClick="HlnkloadFile_Click_1"></asp:LinkButton>
                               </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top" colspan="3">
                            <textarea id="lblBody1" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>

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

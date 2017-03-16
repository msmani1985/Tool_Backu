
<%@ page language="C#" autoeventwireup="true" inherits="Decker_Email, App_Web_vlobbbje" validaterequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 
        <title>Decker journals Email Preview</title>
      <%--    <link href="default_Demo.css" rel="stylesheet" type="text/css" />    --%>
        <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    <script type="text/javascript" language="javascript">
        WYSIWYG.attach('lblBody');

    </script>
         <script type="text/javascript" language="javascript">
             WYSIWYG.attach('lblBody1');

    </script>
           <script type="text/javascript" language="javascript">
               WYSIWYG.attach('lblBodySAE');

    </script>
    <script type="text/javascript" language="javascript">
        WYSIWYG.attach('lblBodySAM');

    </script>
        <style type="text/css">
            .auto-style1 {
                width: 651px;
            }
        </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
<div>
            <table>
            <tr  class="dpJobGreenHeader">
                <td colspan="3" style="text-indent:5px; background-color: #008000; font-size: 10pt; font-weight: bold; color: #FFFFFF; font-family: 'Arial Unicode MS';" class="auto-style1">Decker journals Email Preview</td>

            </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        Journals : <asp:DropDownList ID="dropJournals" runat="server" AutoPostBack="true" OnSelectedIndexChanged="dropJournals_SelectedIndexChanged">
                            <asp:ListItem>SAGHE</asp:ListItem>
                            <asp:ListItem>MI</asp:ListItem>
                            <asp:ListItem>SAE</asp:ListItem>
                            <asp:ListItem>SAM</asp:ListItem>
                            <asp:ListItem>SAN</asp:ListItem>
                            <asp:ListItem>SAS</asp:ListItem>
                            <asp:ListItem>SAVS</asp:ListItem>
                            <asp:ListItem>MGEN</asp:ListItem>
                            <asp:ListItem>MGEN With Images</asp:ListItem>
                            <asp:ListItem>JMMCR</asp:ListItem>
                            <asp:ListItem>JMMCR With Images</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
        </table>
            <ol id="toc">
                        <li id="miSAGHE" runat="server" visible="false">
                            <asp:LinkButton ID="lnkSAGHE" runat="server" OnClick="lnkSAGHE_Click" TabIndex="1">SAGHE</asp:LinkButton></li>
                        <li id="miMI" runat="server" visible="false">
                            <asp:LinkButton ID="lnkMI" runat="server" OnClick="lnkMI_Click" TabIndex="2">MI</asp:LinkButton></li>
                        <li id="miSAE" runat="server" visible="false">
                            <asp:LinkButton ID="lnkSAE" runat="server" OnClick="lnkSAE_Click" TabIndex="3">SAE</asp:LinkButton></li>
                       <li id="miSAM" runat="server" visible="false">
                            <asp:LinkButton ID="lnkSAM" runat="server" OnClick="lnkSAM_Click" TabIndex="3">SAE</asp:LinkButton></li>
                    </ol>
      <div class="content" id="tabSAGHE" runat="server">
       <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                        <tr class="dpJobGreenHeader" style="background-color: #F0FFF0; color: #008031; font-size: 9pt; font-weight: bold;">
                        
                          <td colspan="4" style="height: 32px">
                                                <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="lblDeckerSAGHE" runat="server" Text="SAGHE"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="deckerproofing@charlesworth-group.com"></asp:label></td>
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
                        <tr><td>
                            <strong>Attachment: </strong>
                        </td>
                            <td> 
                            <asp:FileUpload ID="File_upload_1" runat="server" Width="390px"  />
                                <br />
                            <asp:FileUpload ID="File_upload_2" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_3" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_4" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_5" runat="server" Width="390px" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                        <td>
                          <asp:Label Text="" runat="server" ID="LblMsg"></asp:Label></td>
                        
                        <td colspan="2">
                        <asp:LinkButton ID="HlnkloadFile_1" Text="View 1'st Attachment" runat="server"  OnClick="HlnkloadFile_Click_1"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_2" Text="View 2'nd Attachment" runat="server"  OnClick="HlnkloadFile_Click_2"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_3" Text="View 3'rd Attachment" runat="server"  OnClick="HlnkloadFile_Click_3"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_4" Text="View 4'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_4"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_5" Text="View 5'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_5"></asp:LinkButton>
                         </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3">
                             <%-- <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%; font-family: 'Courier New'; font-size: 10pt;" runat="server"></textarea>--%>
                                      <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>
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

       <div class="content" id="tabMI" runat="server">
       <table >
            <tr>
                <td>
                    <table id="Table1" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                        <tr class="dpJobGreenHeader">
                        
                          <td colspan="4" style="height: 32px; color: #008000; background-color: #F0FFF0; font-size: 9pt; font-weight: bold; font-style: normal;">
                                                <img id="Img1" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="Label2" runat="server" Text="MI"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress1" runat="server" Text="deckerproofing@charlesworth-group.com"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit1" runat="server" OnClick="btnSubmit1_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessage1"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="lblToAddress1" runat="server" Text=""></asp:TextBox><asp:Label ID="Label5" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="lblCCAddress1" runat="server" Width="250px" Text=""></asp:TextBox></td> </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="lblBCCAddress1" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="lblSubject1" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                        <tr><td>
                            <strong>Attachment: </strong>
                        </td>
                            <td> 
                            <asp:FileUpload ID="File_upload_11" runat="server" Width="390px"  />
                                <br />
                            <asp:FileUpload ID="File_upload_12" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_13" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_14" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_15" runat="server" Width="390px" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                        <td>
                          <asp:Label Text="" runat="server" ID="Label6"></asp:Label></td>
                        
                        <td colspan="2">
                        <asp:LinkButton ID="HlnkloadFile_11" Text="View 1'st Attachment" runat="server"  OnClick="HlnkloadFile_Click_11"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_12" Text="View 2'nd Attachment" runat="server"  OnClick="HlnkloadFile_Click_12"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_13" Text="View 3'rd Attachment" runat="server"  OnClick="HlnkloadFile_Click_13"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_14" Text="View 4'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_14"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_15" Text="View 5'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_15"></asp:LinkButton>
                         </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3">
                             <%-- <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%; font-family: 'Courier New'; font-size: 10pt;" runat="server"></textarea>--%>
                                      <textarea id="lblBody1" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>
                            </td></tr>
                    </table>
                </td>
                <td>
                    <div id="div2" runat="server">
                    </div>    
                </td>
            </tr>
        </table>
          </div>

     <div class="content" id="tabSAE" runat="server">
       <table >
            <tr>
                <td>
                    <table id="Table2" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                        <tr class="dpJobGreenHeader">
                        
                          <td colspan="4" style="height: 32px; color: #008000; background-color: #F0FFF0; font-size: 9pt; font-weight: bold; font-style: normal;">
                                                <img id="Img2" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="lblDeckerSAE" runat="server" Text="SAE"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddressSAE" runat="server" Text="deckerproofing@charlesworth-group.com"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmitSAE" runat="server" OnClick="btnSubmitSAE_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessageSAE"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="lblToAddressSAE" runat="server" Text=""></asp:TextBox><asp:Label ID="Label8" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="lblCCAddressSAE" runat="server" Width="250px" Text=""></asp:TextBox></td> </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="lblBCCAddressSAE" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="lblSubjectSAE" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                        <tr><td>
                            <strong>Attachment: </strong>
                        </td>
                            <td> 
                            <asp:FileUpload ID="File_upload_111" runat="server" Width="390px"  />
                                <br />
                            <asp:FileUpload ID="File_upload_112" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_113" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_114" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_115" runat="server" Width="390px" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                        <td>
                          <asp:Label Text="" runat="server" ID="LblMsgSAE"></asp:Label></td>
                        
                        <td colspan="2">
                        <asp:LinkButton ID="HlnkloadFile_111" Text="View 1'st Attachment" runat="server"  OnClick="HlnkloadFile_Click_111"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_112" Text="View 2'nd Attachment" runat="server"  OnClick="HlnkloadFile_Click_112"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_113" Text="View 3'rd Attachment" runat="server"  OnClick="HlnkloadFile_Click_113"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_114" Text="View 4'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_114"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_115" Text="View 5'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_115"></asp:LinkButton>
                         </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3">
                             <%-- <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%; font-family: 'Courier New'; font-size: 10pt;" runat="server"></textarea>--%>
                                      <textarea id="lblBodySAE" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>
                            </td></tr>
                    </table>
                </td>
                <td>
                    <div id="div3" runat="server">
                    </div>    
                </td>
            </tr>
        </table>
          </div>

      <div class="content" id="tabSAM" runat="server">
       <table >
            <tr>
                <td>
                    <table id="Table3" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                        <tr class="dpJobGreenHeader">
                        
                          <td colspan="4" style="height: 32px; color: #008000; background-color: #F0FFF0; font-size: 9pt; font-weight: bold; font-style: normal;">
                                                <img id="Img3" align="absmiddle" src="images/tools/information.png" runat="server" />
                                                <asp:Label ID="lblDeckerSAM" runat="server" Text="SAM"></asp:Label></td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddressSAM" runat="server" Text="deckerproofing@charlesworth-group.com"></asp:label></td>
                        <td rowspan="3"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmitSAM" runat="server" OnClick="btnSubmitSAM_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessageSAM"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="lblToAddressSAM" runat="server" Text=""></asp:TextBox><asp:Label ID="Label3" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="lblCCAddressSAM" runat="server" Width="250px" Text=""></asp:TextBox></td> </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="lblBCCAddressSAM" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="lblSubjectSAM" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                        <tr><td>
                            <strong>Attachment: </strong>
                        </td>
                            <td> 
                            <asp:FileUpload ID="File_upload_116" runat="server" Width="390px"  />
                                <br />
                            <asp:FileUpload ID="File_upload_117" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_118" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_119" runat="server" Width="390px" />
                                <br />
                            <asp:FileUpload ID="File_upload_120" runat="server" Width="390px" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                        <td>
                          <asp:Label Text="" runat="server" ID="LblMsgSAM"></asp:Label></td>
                        
                        <td colspan="2">
                        <asp:LinkButton ID="HlnkloadFile_116" Text="View 1'st Attachment" runat="server"  OnClick="HlnkloadFile_Click_116"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_117" Text="View 2'nd Attachment" runat="server"  OnClick="HlnkloadFile_Click_117"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_118" Text="View 3'rd Attachment" runat="server"  OnClick="HlnkloadFile_Click_118"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_119" Text="View 4'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_119"></asp:LinkButton>
                            <br />
                        <asp:LinkButton ID="HlnkloadFile_120" Text="View 5'th Attachment" runat="server"  OnClick="HlnkloadFile_Click_120"></asp:LinkButton>
                         </td>
                        </tr>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3">
                             <%-- <textarea id="lblBody" style="border:solid 1px gray;Height:266px; Width:100%; font-family: 'Courier New'; font-size: 10pt;" runat="server"></textarea>--%>
                                      <textarea id="lblBodySAM" style="border:solid 1px gray;Height:266px; Width:100%;" runat="server"></textarea>
                            </td></tr>
                    </table>
                </td>
                <td>
                    <div id="div1" runat="server">
                    </div>    
                </td>
            </tr>
        </table>
          </div>
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" CodeFile="emailpreview.aspx.cs" inherits="emailpreview" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Datapage PDF Emailer</title>
    <link href="default.css" type="text/css" rel="stylesheet" />    
    <style>
    .headerbackcolor
        {
            background-color:#EAFEE2; /*WhiteSmoke;*/
            border-bottom:solid 2px Gray;
            color:Green;
            font-size:10pt;font-weight:bold;
        }
    </style>  
</head>
<script language="javascript" >
function reloadlocation(oType)
{
    if (oType == 'xls')
        location.href = "preview.aspx?filename=" + document.getElementById('lblAttachment2').value + "&filetype=xls";  
        //window.open("preview.aspx?filename=" + document.getElementById('lblAttachment2').value + "&filetype=xls");  
    else
        window.open("preview.aspx?filename=" + document.getElementById('lblAttachment1').value + "&filetype=pdf");  
    
}

function Showdiv()
{

    document.getElementById('img_hidediv').style.display='';
    document.getElementById('div_attachbrowser').style.display='';
    document.getElementById('img_showdiv').style.display='none';
}
function hidediv()
{
    document.getElementById('img_showdiv').style.display='';
    document.getElementById('div_attachbrowser').style.display='none';
    document.getElementById('img_hidediv').style.display='none';
}
</script>
<body>
    <form id="form1" style="text-align:left" runat="server">
    <div id="Div_emailpreview" runat="server">
        <table >
            <tr>
                <td>
                    <table id="emailtable" runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="650px">
                    <tr>
                        <td colspan="3" class="dpGreenHeader" style="text-indent:5px;">Invoice Emailing System</td>
                    </tr>
                    <tr>
                        <td><b>From: </b></td><td><asp:label ID="lblFromAddress" runat="server" Text="accounts@datapage.org"></asp:label></td>
                       
                        <td rowspan="2"  align="right">
                            <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblMessage"></asp:Label>
                        </td>
                    </tr>
                    <tr><td><b>To: </b></td>
                        <td><asp:TextBox ID="lblToAddress" runat="server" Text=""></asp:TextBox><asp:Label ID="Label1" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label></td>
                        
                    </tr>
                    <tr><td><b>CC: </b></td>
                        <td><asp:TextBox ID="lblCCAddress" runat="server" Width="250px" Text=""></asp:TextBox></td> 
                        <td rowspan="2"  align="right">
                            <asp:ImageButton AlternateText="upload_PDF" Height="30" ImageUrl="images/pdf1.png" ToolTip="UploadPDF" ID="btnUpload_PDF" runat="server" OnClick="btnUploadPDF_Click" />
                            <asp:ImageButton AlternateText="upload_XML" Height="30" ImageUrl="images/xml1.gif" ToolTip="UploadXML" ID="btnUpload_XML" runat="server" OnClick="btnUploadXML_Click" />
                            <br />
                            <asp:Label Text="" runat="server" ID="lblmsg_upload"></asp:Label>
                        </td>
                        </tr>
                        <tr><td><b>BCC: </b></td><td><asp:TextBox ID="lblBCCAddress" runat="server" Width="250px" ></asp:TextBox></td><td></td></tr>
                        <tr><td><b>Subject: </b></td><td colspan="2"><asp:TextBox ID="lblSubject" Width="450px" runat="server" Text=""></asp:TextBox></td></tr>
                        <tr><td colspan="3"><div id="div_Attachemt_Details" runat="server"></div></td></tr>
                        <tr><td colspan="3"><b>Attachment(s): </b></td></tr>
                        <tr><td colspan="3"><div id="div_Attachments" runat="server" align="left"></div><asp:Label ID="Lbl_attachtype" Text="" runat="server" Visible="false"></asp:Label>
                            </td></tr>
                        <tr><td colspan="3"><asp:HyperLink ID="HlnkloadFile" Text="file name" NavigateUrl="" Target="_blank" runat="server"></asp:HyperLink></td></tr>
                        <tr><td colspan="3">
                            
                            <table width="100%"><tr><td class="headerbackcolor"><div id="div_newattachheader" runat="server"><font size="2" color="#006600"><b>New Attachment</b></font><img src="images/plus_symbol_up.gif" ID="img_showdiv" style="cursor:pointer;" onClick="Showdiv();" runat="server" /><img ID="img_hidediv" src="images/minus2.jpg" onClick="hidediv();" style="display:none; cursor:pointer;" runat="server" /></div></td></tr>
                            <tr><td><div id="div_attachbrowser" style="display:none;" runat="server"><asp:FileUpload ID="file_upload" runat="server" />&nbsp;
                            <asp:Button ID="Btn_ok" CssClass="dpbutton" Text="Ok" runat="server" OnClick="Btn_ok_Click" /></div></td></tr>
                            </table>
                            
                            </td></tr>
                        <tr><td colspan="3"><div id="div_newattachment" runat="server"></div></td></tr>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3"><asp:TextBox Height="250px" Width="550px" TextMode="MultiLine" ID="lblBody" runat="server" Text=""></asp:TextBox> </td></tr>
                    </table>
                </td>
                <td>
                    <div id="div_notupdateinvoice" runat="server">
                    </div>    
                </td>
            </tr>
        </table>
        
    
    </div>
    <div id="Div_error" runat="server"></div>
    <%--<table  runat="server" cellpadding="3" cellspacing="3" class="divwithborder" style="vertical-align:top;" width="550px">
    <tr><td colspan="5" class="dpGreenHeader" style="text-indent:5px;">Invoice Emailing System</td></tr>
    <tr><td colspan="4"><b>From:</b> <asp:label ID="lblFromAddress1" runat="server" Text="accounts@datapage.org"></asp:label></td>
    <td rowspan="3" align="right" >
    <asp:ImageButton AlternateText="Send Email" Height="30" ImageUrl="images/emailsend.jpg" ToolTip="Send Email" ID="btnSubmit1" runat="server" OnClick="btnSubmit_Click" />
    <br />
    <asp:Label Text="" runat="server" ID="lblMessage1"></asp:Label>
    </td>
    </tr>
    <tr><td colspan="4"><b>To: &nbsp;&nbsp;</b><asp:TextBox ID="lblToAddress1" runat="server" Text=""></asp:TextBox>
        <asp:Label ID="Label11" runat="server"  ForeColor="red" Text="Use ';' as Mail Separator"></asp:Label>
     </td></tr>
    <tr><td colspan="4"><b>CC: &nbsp;&nbsp;</b><asp:TextBox ID="lblCCAddress1" runat="server" Width="250px" Text=""></asp:TextBox> </td></tr>
    <tr><td colspan="5"><b>BCC: </b><asp:TextBox ID="lblBCCAddress1" runat="server" Width="250px" ></asp:TextBox> </td></tr>
    <tr><td colspan="5"><b>Subject: </b><asp:TextBox ID="lblSubject1" Width="450px" runat="server" Text=""></asp:TextBox> </td></tr>
    <tr><td colspan="5"><div id="div_Attachemt_Details1" runat="server"></div></td></tr>
    <tr><td colspan="5"><b>Attachment(s): </b>
    <div id="div_Attachments1" runat="server" align="left"></div>
    <asp:Label ID="Lbl_attachtype1" Text="" runat="server" Visible="false"></asp:Label>
    </td></tr>
    <tr><td><asp:HyperLink ID="HlnkloadFile1" Text="file name" NavigateUrl="" Target="_blank" runat="server"></asp:HyperLink></td></tr>
    <tr><td style="vertical-align:top;" valign="top"  colspan="5"><asp:TextBox Height="250px" Width="550px" TextMode="MultiLine" ID="lblBody1" runat="server" Text=""></asp:TextBox> </td></tr>
    </table>--%>

    <div id="divInvoiceHTML" runat="server" ></div>
    <script language="javascript" >
        //after saving attachment save a copy to client
    var xmlhttp;
    var sFileContent;
    function GetXmlHttpObject()
    {
      if (window.XMLHttpRequest)
      {
      // code for IE7+, Firefox, Chrome, Opera, Safari
      return new XMLHttpRequest();
      }
      if (window.ActiveXObject)
      {
      // code for IE6, IE5
      return new ActiveXObject("Microsoft.XMLHTTP");
      }
      return null;
    }

    function stateChanged()
    {
      if (xmlhttp.readyState==4)
      {
      alert(xmlhttp.responseText);
      //WriteFile(xmlhttp.responseText.toString());
      }
    }
            
        
    function SaveContent()
    {
        xmlhttp=GetXmlHttpObject();
        if (xmlhttp==null)
          {
          alert ("Your browser does not support XMLHTTP!");
          return;
          }
        var url="preview.aspx";
        url=url+"?filename=test.txt" ; //+ document.getElementById("lblAttach1").innerText ;
        xmlhttp.onreadystatechange=stateChanged;
        xmlhttp.open("GET",url,true);
        xmlhttp.send(null);
    }   
    function WriteFile(sContent) 
    {
        try
        {
           var fso  = new ActiveXObject("Scripting.FileSystemObject"); 
           var fh = fso.CreateTextFile("c:\\ooooohtest.xls", true); 
           fh.WriteLine(sContent); 
           fh.Close(); 
        }
        catch(oErr)
           {alert(oErr.description );}
    }        
    </script>
    
    </form>
</body>
</html>

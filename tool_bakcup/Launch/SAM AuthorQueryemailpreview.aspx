<%@ page language="C#" autoeventwireup="true" inherits="SAM_AuthorQueryemailpreview, App_Web_ucbfjbcc" validaterequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SAM Follow Up Email Preview</title>
  <%--  <link href="default.css" rel="stylesheet" type="text/css" />--%>
    <link href="scripts/wysiwyg.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="scripts/wysiwyg.js"></script>
    <script type="text/javascript" src="scripts/wysiwyg-settings.js"></script>
    <script type="text/javascript" language="javascript">
			WYSIWYG.attach('lblBody',small);		
			
    </script>
    
    <script type="text/javascript"> 
tinyMCE.init({ 
// General options 
mode: "textbox", 
theme: "advanced", 
plugins: "pagebreak,style,layer,table,save,advhr,advimage,advlink,emotions,iespell,
inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,
directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template,
wordcount,advlist,autosave", 
setup: function(ed) { 
ed.onKeyPress.add( 
function(ed, evt) { 
} 
); 
}, 
// Theme options 
theme_advanced_buttons1: "save,newdocument,|,bold,italic,underline,strikethrough,|,
justifyleft,justifycenter,justifyright,justifyfull,styleselect,formatselect,
fontselect,fontsizeselect", 
theme_advanced_buttons2: "cut,copy,paste,pastetext,pasteword,|,search,replace,|,
bullist,numlist,|,outdent,indent,blockquote,|,undo,redo,|,link,unlink,anchor,
image,cleanup,help,code,|,insertdate,inserttime,preview,|,forecolor,backcolor", 
theme_advanced_buttons3: "tablecontrols,|,hr,removeformat,visualaid,|,sub,sup,|,
charmap,emotions,iespell,media,advhr,|,print,|,ltr,rtl,|,fullscreen", 
theme_advanced_buttons4: "insertlayer,moveforward,movebackward,absolute,|,
styleprops,|,cite,abbr,acronym,del,ins,attribs,|,visualchars,nonbreaking,
template,pagebreak,restoredraft", 
theme_advanced_toolbar_location: "top", 
theme_advanced_toolbar_align: "left", 
theme_advanced_statusbar_location: "bottom", 
theme_advanced_resizing: true, 
// Example content CSS (should be your site CSS) 
content_css: "css/content.css", 
// Drop lists for link/image/media/template dialogs 
template_external_list_url: "lists/template_list.js", 
external_link_list_url: "lists/link_list.js", 
external_image_list_url: "lists/image_list.js", 
media_external_list_url: "lists/media_list.js", 
// Style formats 
style_formats: [ 
{ title: 'Bold text', inline: 'b' }, 
{ title: 'Red text', inline: 'span', styles: { color: '#ff0000'} }, 
{ title: 'Red header', block: 'h1', styles: { color: '#ff0000'} }, 
{ title: 'Example 1', inline: 'span', classes: 'example1' }, 
{ title: 'Example 2', inline: 'span', classes: 'example2' }, 
{ title: 'Table styles' }, 
{ title: 'Table row 1', selector: 'tr', classes: 'tablerow1' } 
], 
// Replace values for the template plugin 
template_replace_values: { 
username: "Some User", 
staffid: "991234" 
} 
}); 
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
                        <td colspan="3" style="text-indent:5px; background:green;color:White;font-weight:bold;height:30px;vertical-align:middle; ">SAM Author Query Email Preview</td>
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
                        <tr><td>
                            <strong>Attachment: </strong>
                        </td>
                            <td> 
                            <asp:FileUpload ID="File_upload" runat="server" Width="390px"  />
                            <asp:FileUpload ID="File_upload2" runat="server" Width="390px" />
                            </td>
                        </tr>
                        
                        
                        <tr>
                        <td>
                          <asp:Label Text="" runat="server" ID="LblMsg"></asp:Label></td>
                        
                        <td colspan="3"><asp:LinkButton ID="HlnkloadFile" Text="View Attachment1" runat="server"  OnClick="HlnkloadFile_Click"></asp:LinkButton>
                        <asp:LinkButton ID="HlnkloadFile1" Text="View Attachment2" runat="server"  OnClick="HlnkloadFile_Click1"></asp:LinkButton>
                         </td>
                        </tr>
                        
                        
                        <%--<tr><td style="vertical-align:top;" valign="top"  colspan="3"><asp:TextBox Height="250px" Width="550px" TextMode="MultiLine" ID="lblBody1" runat="server" Text=""></asp:TextBox> </td></tr>--%>
                        <tr><td style="vertical-align:top;" valign="top"  colspan="3">
                            <%--<div style="border:solid 1px gray;Height:250px;Width:550px;overflow:auto;" id="lblBody" runat="server" contenteditable="true" enableviewstate="true" ></div> --%>
                            <asp:TextBox id="lblBody" cols="100" rows = "50"  style="border:solid 1px gray;Height:250px;Width:100%;" TextMode = "MultiLine" runat="server"></asp:TextBox>
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

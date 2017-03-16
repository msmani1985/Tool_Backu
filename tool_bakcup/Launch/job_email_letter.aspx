<%@ page language="C#" autoeventwireup="true" inherits="job_email_letter, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MIS - Email Preview</title>    
    <style>
    .dptitle{
	font-family: Verdana, Tahoma, Arial;
	font-size: 12pt;
	font-weight: bold;
	text-align: left;
	margin-left: 10px;
	margin-bottom: 15px;
	border-bottom: 1px solid GREEN;
	color: Green;
	width: 90%;
	height: 30px;
    }
    #divHeader{word-wrap: break-word; word-break:break-all; font-size:14px; padding:8px; border: 1px solid green; background-color: honeydew;}
    #divBody{font-size:14px;padding:10px; border: 1px solid green;}
    #btnClose,#btnBack{border: solid 1px #000}
</style>
</head>
<body>
    <form id="form1" runat="server">
     <div id="divTitle" runat="server" class="dptitle" style="">
            Email Sent Preview</div>
        <table align="center" width="90%">
            <tr>
                <td align="right">
                    <input id="btnBack" class="dpbutton" value="<< Back" type="button" onclick="javascript:history.back();"
                        style="font-size: 11px; background-color: Green; color: White; cursor: pointer"
                        runat="server" visible="false" />
                    <input id="btnClose" class="dpbutton" value="Close [x]" type="button" onclick="javascript:self.close();"
                        style="font-size: 11px; background-color: Green; color: White; cursor: pointer" />
                </td>
            </tr>
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center">
                        <tr>
                            <td>
                                <div id="divHeader" runat="server">
                                </div>
                                <br />
                                <div id="divBody" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>

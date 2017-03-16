<%@ page language="C#" autoeventwireup="true" inherits="customerinformation, App_Web_lruasnqi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Customer Information Page</title>
    <link href="default.css" language="javascript" type="text/css"  rel="stylesheet"/>
    <style>
    div{word-wrap:break-word;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="headerdiv" class="dptitle" runat="server">Customer Information</div>
    <div >
         <table cellpadding="1" cellspacing="1" width="90%" class="bordertable"  align="center" >
            <tr><td colspan="2"><div id="divaccount" runat="server">&nbsp;</div></td></tr>
            <%--<tr><td colspan="2"><div id="divfinance" runat="server">&nbsp;</div></td></tr>--%>
         </table>
    </div>
    
    </form>
</body>
</html>

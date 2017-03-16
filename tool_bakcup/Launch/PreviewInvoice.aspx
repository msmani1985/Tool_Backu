<%@ page language="C#" autoeventwireup="true" inherits="PreviewInvoice, App_Web_qizamw-n" %>



<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Invoice Report</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body id="Body">
    <form id="formReport" runat="server">
    <div>&nbsp;</div>
    <div runat="server" id="divInvoiceHTML"></div>
    </form>
</body>
</html>


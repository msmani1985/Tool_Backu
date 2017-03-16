<%@ page language="C#" autoeventwireup="true" inherits="agedanalysis, App_Web_opij0lkt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Aged Ananlysis Report</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
<style>
table{
font-size: 10px;
font-family: verdana, arial;
}
td{
border-collapse:collapse;
border:0px solid lightgrey;
}
.currency{
text-align:right;
}
.cellhead{text-align:center; vertical-align:middle;font-weight:bold;background:green}
</style>

</head>
<body>
    <form id="form1" runat="server">
    <div id="TitleDiv" runat="server" class="dptitle">
       Aged Analysis Report
    </div>
    
    <div style="width:90%">
        <asp:RadioButtonList runat="server" style="float:left " ID="getType" Font-Italic="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="getType_SelectedIndexChanged" AutoPostBack="True" >
            <asp:ListItem Selected="True" Text="By Currency" Value="0"></asp:ListItem>
            <asp:ListItem Text="By EURO" Value="1"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="btnSubmit" Text="Submit" style="float:left " runat="server" CssClass="dpbutton" OnClick="btnSubmit_Click" />
    </div>
    <div id="msg" runat="server"></div>
    <div style="margin-left:20px;width:90%;text-align: left;border:1px solid green;float:left;">
        <div id="excelTable" runat="server"></div>
    </div>    
    <asp:ImageButton style="margin-left:10px;float:left" AlternateText="Export to Excel" ToolTip="Export to Excel" ImageUrl="~/images/Excel.jpg" ID="btnExport" runat="server" OnClick="btnExport_Click" Visible="False" />    
    </form>
</body>
</html>

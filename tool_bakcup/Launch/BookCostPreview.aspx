<%@ page language="C#" autoeventwireup="true" inherits="BookCostPreview, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview</title>
    <style>
    #btnClose
    {border: solid 1px #000}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width: 100%">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblTitle" runat="server" Font-Bold="True" ForeColor="Green"></asp:Label>
                    </td>
                    <td>
                        <div style="float: right">
                            <input id="btnClose" class="dpbutton" value="Close [x]" type="button" onclick="javascript:self.close();"
                                style="font-size: 11px; background-color: Green; color: White; cursor: pointer" /></div>
                    </td>
                </tr>
            </table>
            <div align="center" style="border: solid 1px #ddd; width: 100%">
                <asp:Image ID="imgPreview" runat="server" ImageUrl="~/images/costing/dummy.jpg" />
            </div>
        </div>
    </form>
</body>
</html>

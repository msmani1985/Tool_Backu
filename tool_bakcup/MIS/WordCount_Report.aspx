<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WordCount_Report.aspx.cs" Inherits="WordCount_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="TitleDiv" runat="server" class="dptitle">
        Word Count Report
    </div>
    <div>
    <table>
        <tr>
            <td align="center">
                From:&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="SDate" Width="70px"  runat="server" CssClass="auto-feature"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar3.aspx?formname=SDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                To:&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="EDate" Width="70px"  runat="server" CssClass="auto-feature"></asp:TextBox>
                    <img style="cursor:pointer; border: none" alt="Calendar" onclick="javascript:calendar_window=window.open('calendar3.aspx?formname=EDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" src="images/Calendar.jpg" height="20px" border="0" />&nbsp;&nbsp;&nbsp;&nbsp;
               <asp:Button CssClass="dpbutton" ID="submit" Text="Submit" runat="server" OnClick="submit_Click" />  
                
            </td>

        </tr>
        <tr>
            <td align="right">
                <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExl"  ToolTip="Export Exl" OnClick="exportExl_Click"  />
            </td>
        </tr>
        <tr>
            <td>
                 <asp:GridView ID="WordCount" runat="server" AutoGenerateColumns="False"
                     AlternatingRowStyle-CssClass="dullbackground" CssClass="lightbackground"
                     HeaderStyle-CssClass="darkbackground" RowStyle-Wrap="false" PagerStyle-Wrap="false">
                    <Columns>
                        <asp:BoundField DataField="JOURCODE" HeaderText="Journal" SortExpression="Journal" />
                        <asp:BoundField DataField="AMANUSCRIPTID" HeaderText="Article" SortExpression="AMANUSCRIPTID" />
                        <asp:BoundField DataField="ms_pages" HeaderText="MS Pages" SortExpression="ms_pages" />
                        <asp:BoundField DataField="AREALNOOFPAGES" HeaderText="Typeset Pages" SortExpression="AREALNOOFPAGES" />
                        <asp:BoundField DataField="WCWithRef" HeaderText="Word Count without References" SortExpression="WCWithRef" />
                        <asp:BoundField DataField="WCWithOutRef" HeaderText="Word Count with References" SortExpression="WCWithOutRef" />
                        <asp:BoundField DataField="UpdatedDate" HeaderText="Uploaded Date" SortExpression="UpdatedDate" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

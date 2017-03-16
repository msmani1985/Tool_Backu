<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DSR.aspx.cs" Inherits="DSR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            border-bottom: solid 1px Green;
            background: #f0fff0;
            color: GREEN;
            font-size: 10pt;
            font-weight: bold;
            height: 20px;
            vertical-align: middle;
            text-align: left;
            width: 844px;
        }
        .auto-style2 {
            width: 844px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <table>
            <tr bgcolor="#f0fff0">
                <td class="auto-style1" colspan="4">
                    <img id="imgNLHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                        ID="lblNLHeader" runat="server" Text="Label">Daily Status Report</asp:Label></td>
            </tr>
            <tr>
                <td align="right" class="auto-style2">
                    <asp:ImageButton ImageUrl="images/icon-excel2010.gif" runat="server" 
                        ID="exportExcel_selectedcolumns" OnClick="exportExcel_selectedcolumns_Click" 
                        Height="20px"/>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" align="center">
                    <asp:GridView ID="grdDSR" runat="server" AllowSorting="True" 
                        AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Left" 
                        CellPadding="4" Font-Names="Segoe UI" Font-Size="11px" ForeColor="#333333" 
                        GridLines="Vertical"  Width="44%" ShowHeaderWhenEmpty="True">
                        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" /> 
                        <rowstyle backcolor="white" />
                        <alternatingrowstyle backcolor="#F0FFF0" />
                        <Columns>
                            <asp:BoundField DataField="Overall_Summary_Status" HeaderText="Overall Summary Status"
                                SortExpression="Overall_Summary_Status"></asp:BoundField>
                            <asp:BoundField DataField="Total_Pages_in_hand" HeaderText="Total Pages in hand"
                                SortExpression="Total_Pages_in_hand"></asp:BoundField>
                            <asp:BoundField DataField="Today_Deliverable_Pages" HeaderText="Today's Deliverable Pages"
                                SortExpression="Today_Deliverable_Pages"></asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

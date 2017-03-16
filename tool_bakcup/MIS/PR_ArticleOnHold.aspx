<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PR_ArticleOnHold.aspx.cs" Inherits="PR_ArticleOnHold" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
</head>
<body class="LightBackGound" style="background-repeat:no-repeat; width:100%; height:100%">
    <form id="form1" runat="server">
    <div>
        
    </div>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="UpdatePanel1"
        runat="server">
            <ProgressTemplate>           
              <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" 
                      ImageUrl="~/images/animation.PNG" AlternateText="Loading ..." 
                      ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" 
                      Height="40px" />
              </div>         
            </ProgressTemplate>
        </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <table width="100%">
                <tr >
                     <td align="center" width="90%" class="darkTitle" colspan="4" height="25px" valign="middle">
                         <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Article On Hold"></asp:Label>
                     </td>
                    <td align="right" width="10%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" width="90%">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="11px" ForeColor="Red"></asp:Label>
                    </td>
                    <td align="right" width="10%">
                        <asp:Button ID="btnExcel" runat="server" CssClass="dpbutton" 
                            onclick="btnExcel_Click" Text="Excel" ToolTip="Save" Width="70px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="500px" width="100%">
                        <asp:GridView ID="grdProductionReport" runat="server" AllowSorting="True" 
                            AutoGenerateColumns="False" BorderStyle="Solid" CaptionAlign="Left" 
                            CellPadding="4" Font-Names="Segoe UI" Font-Size="11px" ForeColor="#333333" 
                            GridLines="Vertical"  Width="100%" ShowHeaderWhenEmpty="True">
                            <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" /> 
                            <rowstyle backcolor="white" />
                            <alternatingrowstyle backcolor="#F0FFF0" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate>
                                        <%--<asp:Label ID="Label1" runat="server" Text="<%# iRowId++ %>"></asp:Label>--%>
                                        <asp:Label ID="Label66" runat="server" Text='<%# Eval("ROWID") %>' Width="30px"></asp:Label>
                                        <br />
                                        <%--<asp:HiddenField ID="hfgvInvoiceID" runat="server" Value='<%# Eval("ROWID") %>' />--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="30px" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="AARTICLECODE" HeaderText="Article" 
                                    SortExpression="AARTICLECODE"></asp:BoundField>
                                <asp:BoundField DataField="ADATEONHOLD" HeaderText="Put on Hold" 
                                    SortExpression="ADATEONHOLD"></asp:BoundField>
                                <asp:BoundField DataField="STDESCRIPTION" SortExpression="STDESCRIPTION" 
                                    HeaderText="Status" />
                                <asp:BoundField DataField="ACOMMENTS" HeaderText="Reason why Ms. is on hold" 
                                    SortExpression="ACOMMENTS"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    </td>
                </tr>
            </table>
           
            <%--<table>
            <tr>
            <td><b>Select Date:</b></td>
            <td>
            <asp:TextBox ID="txtDate" runat="server" />
            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtDate" Format="dd/MM/yyyy" runat="server">
            </asp:CalendarExtender> 
            </td>
            </tr>
            <tr>
            <td><b>Select Date</b></td>
            <td>
            <asp:TextBox ID="txtDate1" runat="server"/> <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Calendar.png" />
            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtDate1" PopupButtonID="imgbtnCalendar" runat="server" />
            </td>
            </tr>
            </table> --%>  
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>

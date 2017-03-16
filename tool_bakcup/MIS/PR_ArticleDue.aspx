<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PR_ArticleDue.aspx.cs" Inherits="PR_ArticleDue" %>

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
            <table align="center" cellpadding="0" cellspacing="0" 
                style="border:1px solid green;align:center;">
                 <tr>
                    <td align="center" class="darkTitle" height="25px" valign="middle">
                        &nbsp;</td>
                     <td align="center" class="darkTitle" colspan="4" height="25px" valign="middle">
                         <asp:Label ID="Label63" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                             Font-Size="13px" Text="Article Due - Search"></asp:Label>
                     </td>
                     <td align="center" class="darkTitle" height="25px" valign="middle">
                         &nbsp;</td>
                </tr>
                 
                
                
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td width="8px">
                        &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                    <td width="5px">
                        
                        &nbsp;</td>
                    <td width="5px">
                        &nbsp;</td>
                </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Start Date" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label58" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left">
                         <table cellpadding="0" cellspacing="0">
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txtStartDate" runat="server" class="txtBoxMedium"></asp:TextBox>
                                     <asp:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" 
                                         Enabled="True" PopupButtonID="imgCalendar" TargetControlID="txtStartDate">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td>
                                     <asp:ImageButton ID="imgCalendar" runat="server" Height="20px" 
                                         ImageUrl="~/images/Calender.jpg" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     <td width="5px">
                     </td>
                     <td width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                     <td width="5px">
                         &nbsp;</td>
                     <td width="5px">
                         &nbsp;</td>
                 </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="Label59" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="End Date" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label60" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left">
                         <table cellpadding="0" cellspacing="0">
                             <tr>
                                 <td align="left">
                                     <asp:TextBox ID="txtEndDate" runat="server" class="txtBoxMedium"></asp:TextBox>
                                     <asp:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" 
                                         Enabled="True" PopupButtonID="imgEndCalendar" TargetControlID="txtEndDate">
                                     </asp:CalendarExtender>
                                 </td>
                                 <td align="left" width="5px">
                                     <asp:ImageButton ID="imgEndCalendar" runat="server" Height="20px" 
                                         ImageUrl="~/images/Calender.jpg" />
                                 </td>
                             </tr>
                         </table>
                     </td>
                     <td width="5px" align="left">
                       <%--  <asp:ImageButton ID="imgEndCalendar" runat="server" Height="20px" 
                             ImageUrl="~/images/Calender.jpg" />--%>
                     </td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                     <td align="left" width="5px">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         <asp:Label ID="Label64" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text="Customer" Width="75px"></asp:Label>
                     </td>
                     <td width="8px">
                         <asp:Label ID="Label65" runat="server" Font-Bold="False" Font-Names="Segoe UI" 
                             Font-Size="11px" Text=":"></asp:Label>
                     </td>
                     <td align="left" colspan="2">
                           <asp:ListBox ID="lstCustomer" runat="server" Width="350px" 
                               SelectionMode="Multiple"></asp:ListBox>
                         </td>
                   
                     <td align="left">
                         &nbsp;</td>
                   
                 </tr>
                
                
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         <asp:CheckBox ID="chkIndia" runat="server" Text="India" />
                     </td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                
                                    <asp:Button ID="btnReport" runat="server" CssClass="dpbutton" 
                                        onclick="btnReport_Click" Text="Report" ToolTip="Save" Width="70px" />
                                
                                </td>
                                <td width="5px">
                                
                                </td>
                                <td>
                                
                                    <asp:Button ID="btnClear" runat="server" CssClass="dpbutton" 
                                        onclick="btnClear_Click" Text="Clear" ToolTip="Save" Width="70px" />
                                
                                </td>
                                <td width="5px">
                                
                                </td>
                                <td>
                                   <asp:Button ID="btnExcel" runat="server" CssClass="dpbutton" 
                                        onclick="btnExcel_Click" Text="Excel" ToolTip="Save" Width="70px" />
                                </td>
                            </tr>
                        </table>    
                     </td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
                 <tr>
                     <td>
                         &nbsp;</td>
                     <td>
                         &nbsp;</td>
                     <td width="8px">
                         &nbsp;</td>
                     <td align="left" colspan="2">
                         &nbsp;</td>
                     <td align="left">
                         &nbsp;</td>
                 </tr>
            </table>   
            
            <table width="100%">
                <tr>
                    <td align="center">
                        
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" 
                            Font-Size="11px" ForeColor="Red"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td>
                    <asp:Panel ID="Panel2" runat="server" ScrollBars="Both" Height="340px" width="100%">
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
                                <asp:BoundField DataField="aarticlecode" HeaderText="Article" 
                                    SortExpression="aarticlecode"></asp:BoundField>
                                <asp:BoundField DataField="acorrespondingauthor" HeaderText="Author" 
                                    SortExpression="acorrespondingauthor"></asp:BoundField>
                                <asp:BoundField DataField="creationdate" HeaderText="Received Date" 
                                    SortExpression="creationdate"></asp:BoundField>
                                <asp:BoundField DataField="aduedate" 
                                    HeaderText="Due Date" SortExpression="aduedate">
                                </asp:BoundField>
                                <asp:BoundField DataField="adespatchdate" HeaderText="Despatch Date" 
                                    SortExpression="adespatchdate"></asp:BoundField>
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

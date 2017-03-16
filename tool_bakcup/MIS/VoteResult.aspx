<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VoteResult.aspx.cs" Inherits="VoteResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<meta http-equiv="refresh" content="5">
<head runat="server">
    <title></title>
     <link href="defaultNew.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grdEmployeeMale.ClientID %>').Scrollable({
                ScrollHeight: 350
            });
            
             $('#<%=grdEmployeeFemale.ClientID %>').Scrollable({
                ScrollHeight: 350
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="100%">
        <tr>
            <td colspan="3" align="center">
                <asp:Image ID="DpMrMs" runat="server" ImageUrl="~/images/mr_ms_Datapage_1.jpg" Width="200px" />
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <asp:GridView ID="grdEmployeeMale"  CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                            Font-Names="Segoe UI" 
                            Font-Size="11px"  >
                        <rowstyle backcolor="White" Height="25px"/>
                        <alternatingrowstyle backcolor="Honeydew"/>
                        <Columns>
        
                        <asp:TemplateField HeaderText="S.No.">

                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="EMPID" Visible="False">
                             <ItemTemplate>
                            <asp:Label ID="lblMaleId" runat="server" Text='<%# Eval("EMPID") %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
        
                        <asp:BoundField DataField="EMPNAME" HeaderText="Name" 
                                SortExpression="EMPNAME" />

                         <asp:TemplateField HeaderText="Vote">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoteMale" runat="server" Text='<%# Eval("VOTE") %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                        </asp:TemplateField>

                        </Columns>
                         <HeaderStyle BackColor="Green" CssClass="darkbackground" Font-Bold="True" 
                            ForeColor="White" />
                        </asp:GridView>
      
            </td>
            <td width="50%" valign="top">

                 <asp:GridView ID="grdEmployeeFemale"  CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                            Font-Names="Segoe UI" 
                            Font-Size="11px" >
                     <rowstyle backcolor="White" Height="25px"/>
                     <alternatingrowstyle backcolor="Honeydew"/>
                     <Columns>
                         <asp:TemplateField HeaderText="S.No.">

                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="EMPID" Visible="False">
                             <ItemTemplate>
                            <asp:Label ID="lblMaleId" runat="server" Text='<%# Eval("EMPID") %>' Width="20px"></asp:Label>
                        </ItemTemplate>
                        </asp:TemplateField>
        
                        <asp:BoundField DataField="EMPNAME" HeaderText="Name" 
                                SortExpression="EMPNAME" />

                        <asp:TemplateField HeaderText="Vote">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoteMale" runat="server" Text='<%# Eval("VOTE") %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                        </asp:TemplateField>

                     </Columns>
                     <HeaderStyle BackColor="Green" CssClass="darkbackground" Font-Bold="True" 
                            ForeColor="White" />
                 </asp:GridView>
            </td>

        </tr>
    </table>
    </div>
    </form>
</body>
</html>

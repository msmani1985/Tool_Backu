<%@ page language="C#" autoeventwireup="true" inherits="Vote, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="defaultNew.css" rel="stylesheet" type="text/css" />
      <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grdEmployeeMale.ClientID %>').Scrollable({
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
            <td width="45%" valign="top">
               <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="340px" Width="100%">
                <asp:GridView ID="grdEmployeeMale"  CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                            Font-Names="Segoe UI" 
                            Font-Size="11px" >
        
                        <HeaderStyle CssClass="header" />
                        <rowstyle backcolor="white"/>
                        <alternatingrowstyle backcolor="#F0FFF0"/>
                        <Columns>
        
                        <asp:TemplateField HeaderText="S.No.">

                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                                <br />
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
        
       
                        <asp:BoundField DataField="EMPID" HeaderText="EMPID" 
                                SortExpression="EMPID" Visible="False"/>
                        
                            <asp:BoundField DataField="EMPNAME" HeaderText="Name" 
                                SortExpression="EMPNAME" />
                            <asp:BoundField DataField="REFNO" HeaderText="Reference No" 
                                SortExpression="REFNO" Visible="False" />
                            <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" 
                                SortExpression="DESIGNATION" Visible="False" />
                            <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" 
                                SortExpression="DEPARTMENT" />
                            <asp:BoundField DataField="SEX" HeaderText="Gender" SortExpression="SEX" 
                                Visible="False" />
                           
                        <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ImagePath") %>' Height="40px" Width="40px" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" />
                        </asp:TemplateField>

                            <asp:TemplateField HeaderText="Vote">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkMaleVote" runat="server" />
                                </ItemTemplate>
                                <ItemStyle Width="50px" Wrap="True" />
                            </asp:TemplateField>

                        </Columns>
                         <HeaderStyle BackColor="Green" CssClass="darkbackground" Font-Bold="True" 
                            ForeColor="White" />
                        </asp:GridView>
                    </asp:Panel>        
            </td>
            <td width="1%" valign="top"></td>
            <td width="45%" valign="top">
                 <asp:GridView ID="grdEmployeeFemale"  CaptionAlign="Left" runat="server" 
                            AutoGenerateColumns="False" CellPadding="4" 
                            ForeColor="#333333" BorderStyle="Solid"  
                            GridLines="Vertical" AllowSorting="True" Width="100%" 
                            Font-Names="Segoe UI" 
                            Font-Size="11px" >
                     <HeaderStyle CssClass="header" />
                     <rowstyle backcolor="white"/>
                     <alternatingrowstyle backcolor="#F0FFF0"/>
                     <Columns>
                         <asp:TemplateField HeaderText="S.No.">
                             <ItemTemplate>
                                 <asp:Label ID="lblSlNo0" runat="server" Text='<%# Eval("ROWID") %>' 
                                     Width="20px"></asp:Label>
                                 <br />
                             </ItemTemplate>
                             <ItemStyle Width="30px" />
                         </asp:TemplateField>
                         <asp:BoundField DataField="EMPID" HeaderText="EMPID" 
                                SortExpression="EMPID" Visible="False"/>
                         <asp:BoundField DataField="EMPNAME" HeaderText="Name" 
                                SortExpression="EMPNAME" />
                         <asp:BoundField DataField="REFNO" HeaderText="Reference No" 
                                SortExpression="REFNO" Visible="False" />
                         <asp:BoundField DataField="DESIGNATION" HeaderText="Designation" 
                                SortExpression="DESIGNATION" Visible="False" />
                         <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" 
                                SortExpression="DEPARTMENT" />
                         <asp:BoundField DataField="SEX" HeaderText="Gender" SortExpression="SEX" 
                                Visible="False" />
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:Image ID="Image2" runat="server" 
                                        ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ImagePath") %>' Height="40px" 
                                        Width="40px" />
                             </ItemTemplate>
                             <ItemStyle Width="50px" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vote">
                             <ItemTemplate>
                                 <asp:CheckBox ID="chkMaleVote0" runat="server" />
                             </ItemTemplate>
                             <ItemStyle Width="50px" Wrap="True" />
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

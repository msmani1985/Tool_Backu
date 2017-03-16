<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vote.aspx.cs" Inherits="Vote" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="defaultNew.css" rel="stylesheet" type="text/css" />
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grdEmployeeMale.ClientID %>').Scrollable({
                ScrollHeight: 310
            });
            
             $('#<%=grdEmployeeFemale.ClientID %>').Scrollable({
                ScrollHeight: 310
             });
           
        });
    </script>
    
    <script type = "text/javascript">
    <!-- Disable
    function disableselect(e) {
        return false
    }

    function reEnable() {
        return true
    }

    //if IE4+
    document.onselectstart = new Function("return false")
    document.oncontextmenu = new Function("return false")
    //if NS6
    if (window.sidebar) {
        document.onmousedown = disableselect
        document.onclick = reEnable
    }
    //-->
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table width="100%">
        <tr>
            <td colspan="3" align="center">
                <asp:Image ID="DpMrMs" runat="server" ImageUrl="~/images/mr_ms_Datapage_1.jpg" Width="150px" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="left" style="font-weight: bold; font-size: 12px; color: #009900; font-family: 'Segoe UI'">
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;Hi Datapagians,<br />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;You get a chance to vote for your role model. Select a male and female employee you feel
                can be identified as 
                <br />
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; Mr. & Ms. Datapage, respectively.
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
                        <rowstyle backcolor="White"/>
                        <alternatingrowstyle backcolor="Honeydew"/>
                        <Columns>
        
                        <asp:TemplateField HeaderText="S.No.">

                            <ItemTemplate>
                                <asp:Label ID="lblSlNo" runat="server" Text='<%# Eval("ROWID") %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="EMPID" Visible="false">
                             <ItemTemplate>
                            <asp:Label ID="lblMaleId" runat="server" Text='<%# Eval("EMPID") %>' Width="20px"></asp:Label>
                        </ItemTemplate>
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
                                    <asp:Image ID="Image1" runat="server" draggable="false" ondragstart="return false" onselectstart="return false" onmousedown="return false" onmouseup="return this.onclick() || this.click()" style="-webkit-tap-highlight-color: transparent; -webkit-touch-callout: none; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none;" ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ImagePath") %>' Height="60px" Width="60px" />
                                </ItemTemplate>
                                <ItemStyle Width="70px" />
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
      
            </td>
            <td width="50%" valign="top">

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
                          <asp:TemplateField HeaderText="EMPID" Visible="false">
                             <ItemTemplate>
                            <asp:Label ID="lblFemaleId" runat="server" Text='<%# Eval("EMPID") %>' Width="20px"></asp:Label>
                        </ItemTemplate>
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
                                 <asp:Image ID="Image2" runat="server" draggable="false" ondragstart="return false" onselectstart="return false" onmousedown="return false" onmouseup="return this.onclick() || this.click()" style="-webkit-tap-highlight-color: transparent; -webkit-touch-callout: none; -webkit-user-select: none; -moz-user-select: none; -ms-user-select: none; user-select: none;"
                                        ImageUrl='<%# DataBinder.Eval(Container.DataItem,"ImagePath") %>' Height="60px" 
                                        Width="60px" />
                             </ItemTemplate>
                             <ItemStyle Width="70px" />
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Vote">
                             <ItemTemplate>
                                 <asp:CheckBox ID="chkFemaleVote" runat="server" />
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
    <table width="100%">
        <tr>
            <td align="right">
                <asp:Button ID="btnVote" CssClass="dpbutton" runat="server" Text="Vote"  ToolTip="Vote"  Width="90px" OnClick="btnVote_Click"/>
           </td>
           <td align="left">
                <asp:Button ID="btnClear" CssClass="dpbutton" runat="server" Text="Clear"  ToolTip="Vote"  Width="90px" OnClick="btnClear_Click"/>
            </td>
          </tr>
    </table>
    <table width="100%">
        <tr>
           <td align="center" style="height: 15px">
               <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Names="Segoe UI" Font-Size="11px" ForeColor="Red"></asp:Label></td>
        </tr>
    </table>
    
    
    </div>
    </form>
</body>
</html>

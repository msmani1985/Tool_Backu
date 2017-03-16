<%@ page language="C#" autoeventwireup="true" inherits="ProductionStatusReport, App_Web_zfrrxy20" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">    
     <div class="dptitle" id="divTitle" align="left" runat="server">Production Status Report</div>

    <div>
    
        <%--<table  style="border-top:solid 1px green;width:90%;">--%>
        <table  width="100%" >
            <tr>
            <td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
                &nbsp;</td>
            <td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="btn_refresh" runat="server" AlternateText="Refresh" 
                    ImageUrl="~/images/refresh.jpg" ToolTip = "Refresh" OnClick="ibtnRefrsh_Click" 
                    Height="20px" />
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" AlternateText="Excel Export" 
                    ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" Height="20px" />
             </td>
            </tr>
        </table>
        
        <table cellpadding="0" cellspacing="0">
        <tr> 
        <td >
            <asp:Label ID="Label1" style="text-align:center;" CssClass="darkbackground" runat="server" Text="Label" Width="100%" Height="20px"></asp:Label>
        </td>
        <td width="5px">
        </td>
        <td >
            <asp:Label ID="Label2" style="text-align:center;" CssClass="darkbackground" runat="server" Text="Label" Width="100%" Height="20px"></asp:Label>
        </td>
        <td width="5px">
        </td>
        <td >
            <asp:Label ID="Label3" style="text-align:center;" CssClass="darkbackground" runat="server" Text="Label" Width="100%" Height="20px"></asp:Label>
        </td>
        </tr>
        
        
        <tr> 
        <td >
        <asp:GridView Width="100%" ID="ProductionStatusReport_Report" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                BorderColor="green" Height="215px" Font-Names="Segoe UI" Font-Size="11px" >
                <rowstyle backcolor="white"/>
               <alternatingrowstyle backcolor="#F0FFF0"/>
        <Columns>
        
        <asp:BoundField DataField = 'STAGE' HeaderText="STAGE" ItemStyle-ForeColor="#333333"  ItemStyle-BorderColor="green" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField = 'CE/QA' HeaderText="CE/QA" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
         <asp:BoundField DataField = 'PRE' HeaderText="PRE" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="TAGGING" HeaderText="TAGGING">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="3B2" HeaderText="3B2" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="QC" HeaderText="QC">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="ADMIN" HeaderText="ADMIN" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField= "TOTAL" HeaderText = "TOTAL" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        
        </Columns>
        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </td>
        <td width="5px">
        </td>
        <td >
        <asp:GridView Width="100%" ID="ProductionStatusReport_Report1" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                BorderColor="green" Height="215px" Font-Names="Segoe UI" Font-Size="11px" >
                <rowstyle backcolor="white"/>
               <alternatingrowstyle backcolor="#F0FFF0"/>
        <Columns>
        
        <asp:BoundField DataField = 'STAGE' HeaderText="STAGE"  ItemStyle-ForeColor="#333333"   ItemStyle-BorderColor="green" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField = 'CE/QA' HeaderText="CE/QA" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
         <asp:BoundField DataField = 'PRE' HeaderText="PRE" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="TAGGING" HeaderText="TAGGING">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="3B2" HeaderText="3B2" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="QC" HeaderText="QC">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="ADMIN" HeaderText="ADMIN" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField= "TOTAL" HeaderText = "TOTAL" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        
        </Columns>
        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </td>
        <td width="5px">
        </td>
        <td >
        <asp:GridView Width="100%" ID="ProductionStatusReport_Report2" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                BorderColor="green" Height="215px" Font-Names="Segoe UI" Font-Size="11px" >
                <rowstyle backcolor="white"/>
               <alternatingrowstyle backcolor="#F0FFF0"/>
        <Columns>
        
        <asp:BoundField DataField = 'STAGE' HeaderText="STAGE"   ItemStyle-ForeColor="#333333"  ItemStyle-BorderColor="green" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField = 'CE/QA' HeaderText="CE/QA" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
         <asp:BoundField DataField = 'PRE' HeaderText="PRE" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="TAGGING" HeaderText="TAGGING">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="3B2" HeaderText="3B2" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="QC" HeaderText="QC">
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField="ADMIN" HeaderText="ADMIN" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        <asp:BoundField DataField= "TOTAL" HeaderText = "TOTAL" >
            <HeaderStyle HorizontalAlign="Left" />
        </asp:BoundField>
        
        </Columns>
        <HeaderStyle CssClass="darkbackground" BackColor="Green" Font-Bold="True" ForeColor="White" />
        </asp:GridView>
        </td>
        </tr>
        
        
        </table>
    </div>
       
        <div id="div_Error" runat="server" class="error"></div>
        
    </form>
</body>
</html>

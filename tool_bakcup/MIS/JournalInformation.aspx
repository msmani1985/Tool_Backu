<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JournalInformation.aspx.cs" Inherits="JournalInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="default.css" rel="stylesheet" type="text/css" />    
    
        <style>
           
           .GVFixedHeader
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
       <div align="center" id="pagetitle" style="height:30px;color:green;font-size:14pt;font-family:arial; font-weight:bold;">Datapage &ndash; Journal Information</div>
        <table class="bordertable" align="center" cellspacing="0" cellpadding="10" width="550px" bgcolor="silver">
 
<tr>
<td class="title" valign="middle">
Select Customer:&nbsp;</td><td class="title" >
    <asp:ListBox ID="lstCustomer" runat="server" DataTextField="custname" DataValueField="custno" SelectionMode="Multiple">
        
    </asp:ListBox>

 
 
</td>
<td align='left' class="title">

    <asp:Button  ID="btnSubmit" runat="server" Text="Fetch Records" OnClick="btnSubmit_Click"/>
</td>
<td>
<table border="3">
<tr>
<td>
OverAll Report
</td>
<td>Live Journal</td>
</tr>
<tr>
<td align="center">
<asp:ImageButton ID="cmd_Excel_Export" runat="server" ImageUrl="~/images/tools/j_excel.png" OnClick="cmd_Excel_Export_Click" ToolTip="OverAll Report" />
</td>
<td align="center">
<asp:ImageButton ID="ImageButton1" runat="server" 
        ImageUrl="~/images/tools/j_excel.png" ToolTip="Live Journal" 
        onclick="ImageButton1_Click" />
</td>
</tr>
</table>

</td>
</tr>
</table>
    
        <br />
       <div style="align-content:center"> 
       </div>
    </div>
        <div>
            <asp:GridView ID="grdJournal" runat="server"    AutoGenerateColumns="false" >
                                            <HeaderStyle  CssClass="GVFixedHeader" />
                                            <AlternatingRowStyle BackColor="#F2F2F2" /> 
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Customer Name" />
                                                <asp:BoundField DataField="JOURCODE" HeaderText="Journal Acronym" />
                                                <asp:BoundField DataField="JOURNAME" HeaderText="Journal Title" />
                                                
                                                <asp:BoundField DataField="JPRODEDITOR" HeaderText="Production Editor" />
                                                <asp:BoundField DataField="PEEMAIL" HeaderText="PE Email(s)" />
                                                <asp:BoundField DataField="TRIMSIZE" HeaderText="Trim Size" />
                                                <asp:BoundField DataField="FORMAT" HeaderText="Format" />

                                                <asp:BoundField DataField="TRIMCODE" HeaderText="Trim Code" />
                                                <asp:BoundField DataField="PCODE" HeaderText="Price Code" />
                                                <asp:BoundField DataField="ISCOPYEDIT" HeaderText="Is CopyEdit" />
                                                <asp:BoundField DataField="ISSENSITIVE" HeaderText="Is Sensitive" />
                                                <asp:BoundField DataField="ISSAM" HeaderText="Is SAM" />
                                                <asp:BoundField DataField="FOLLOW_DAYS" HeaderText="SAM Follow Days" />
                                                <asp:BoundField DataField="ISFPM" HeaderText="FPM Journal" />
                                                <asp:BoundField DataField="Live_Journal" HeaderText="Live Journal" />
                                                </Columns>
                
            </asp:GridView>

        </div>
    </form>
</body>
</html>

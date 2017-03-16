<%@ page language="C#" autoeventwireup="true" inherits="RFT, App_Web___zsna4m" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

    <head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
     <div class="dptitle" id="divTitle" align="left" runat="server">Report First Time</div>

    <div>
    
        <table align="center" class="bordertable" width="70%"  id="TABLE1">
            
               
                <tr>
                <td style="width: 195px; height: 30px;">
                    <asp:Label ID="Label3" runat="server" Text="From:"></asp:Label>
                </td>
                <td style="height: 30px"><asp:TextBox ID="FromDate" runat="server" ></asp:TextBox>
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton1" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=FromDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" /></td>
                <td style="height: 30px">
                    <asp:Label ID="Label4" runat="server" Text="To:"></asp:Label>
                    
                </td>
                <td style="height: 30px"> <asp:TextBox ID="ToDate" runat="server" ></asp:TextBox>
                    
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton2" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=ToDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                    </td>
                
                </tr>
                <tr style="border-bottom:solid 1px green;border-bottom-width:thick;">
            
                
                    <%--<td><asp:Button ID="ViewReport" runat="server" Text="View Report" CssClass="dpbutton" OnClick="ViewReport_Click" /></td>--%>
                    <td ><asp:Button ID="ViewReport" Width="100px"  CssClass="dpbutton" Text="View Report" runat="server" OnClick="ViewReport_Click"/></td>
                </tr>
            <tr style="border-bottom: green thick solid">
            </tr>
                
            
        </table>
         <br />
        
        <%--<table  style="border-top:solid 1px green;width:90%;">--%>
        <table  width="100%" >
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" AlternateText="Excel Export" ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" />
             </td>
            </tr>
        </table>
        
        <table width="100%">
        <tr> 
        <td colspan="2" >
        <asp:GridView Width="100%" ID="RFT_Report" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
        GridLines="Vertical" BorderColor="green" >
        <HeaderStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
        <%--<asp:GridView ID="jobreceived" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" OnRowCommand="jobreceived_RowCommand" OnRowDataBound="jobreceived_RowDataBound" OnSelectedIndexChanged="jobreceived_SelectedIndexChanged" >--%>
        <%--<AlternatingRowStyle CssClass="dullbackground" />--%>
        <Columns>
        
        <asp:BoundField DataField = 'TYPESETTER' HeaderText="TYPESETTER"/>
        <asp:BoundField DataField="SAM/NON-SAM" HeaderText="SAM/NON-SAM"/>
        <asp:BoundField DataField="JOURNAL ACRONYM" HeaderText="JOURNAL ACRONYM"/>
        <asp:BoundField DataField="CATS MAN.ID" HeaderText="CATS MAN.ID"/>
        <asp:BoundField DataField="TOTAL NO.REVISES" HeaderText="TOTAL NO.REVISES"/>
        <asp:BoundField  DataField = "TOTAL NO.PREVIEW FILES" HeaderText="TOTAL NO.PREVIEW FILES"/>
        <asp:BoundField DataField= "TOTAL" HeaderText = "TOTAL" />
        
        </Columns>
        <HeaderStyle CssClass="darkbackground" />
        </asp:GridView>
        </td>
        </tr> 
        </table>
    </div>
       
        <div id="div_Error" runat="server" class="error"></div>
        
    </form>
</body>
</html>

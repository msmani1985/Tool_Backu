<%@ page language="C#" autoeventwireup="true" inherits="RFT, App_Web_mjsvsc11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

    <head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
     <div class="dptitle" id="divTitle" align="left" runat="server">Right First Time</div>

    <div>
    
        <table align="center" class="bordertable" width="50%"  id="TABLE1" cellpadding="0" cellspacing="0" >
                <tr>
                <td >
                    <asp:Label ID="Label3" runat="server" Text="From:" Font-Names="Segoe UI"></asp:Label>
                </td>
                <td style="padding-top:5px;" valign="top" ><asp:TextBox ID="FromDate" 
                        runat="server" ></asp:TextBox>
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" 
                        ID="ImageButton1" Height="24px" Width="25px" ImageAlign="Middle" 
                        OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=FromDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" /></td>
                <td >
                    <asp:Label ID="Label4" runat="server" Text="To:" Font-Names="Segoe UI"></asp:Label>
                    
                </td>
                <td  style="padding-top:5px;" > <asp:TextBox ID="ToDate" runat="server" ></asp:TextBox>
                    
                    <asp:ImageButton ImageUrl="~/Images/calendar.jpg"  runat="server" ID="ImageButton2" Height="24px" Width="25px" ImageAlign="Middle" OnClientClick="javascript:calendar_window=window.open('calendar.aspx?formname=ToDate','calendar_window','width=160,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()" />
                    </td>
                <td  > 
                    <asp:Button ID="Button1" Width="80px"  CssClass="dpbutton" Text="View Report" 
                        runat="server" OnClick="ViewReport_Click"/>
                    </td>
                </tr>
                                 
            </table>
         <br />
        
        <%--<table  style="border-top:solid 1px green;width:90%;">--%>
        <table  width="100%" >
            <tr><td style="color:Crimson;font-weight:bold;font-size:10pt;" align="right">
             <asp:ImageButton ID="ibtnExcel_Export" runat="server" AlternateText="Excel Export" 
                    ImageUrl="~/images/Excel.jpg" OnClick="ibtnExcel_Export_Click" Height="15px" />
             </td>
            </tr>
        </table>
        
        <table width="100%" cellpadding="0" cellspacing="0">
        <tr> 
        <td >
            <asp:Label ID="Label1" style="text-align:center;" CssClass="darkbackground" 
                runat="server" Text="Label" Width="100%" Height="20px" Font-Names="Segoe UI" 
                Font-Size="12px"></asp:Label>
        </td>
        </tr> 
        <tr> 
        <td >
        <asp:GridView Width="100%" ID="RFT_Report" CaptionAlign="left" runat="server" 
        AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" 
                BorderColor="green" Font-Names="Segoe UI" Font-Size="11px" >
        <HeaderStyle BackColor="Green" Font-Bold="True" ForeColor="White" />
        <%--<asp:GridView ID="jobreceived" runat="server" AutoGenerateColumns="false" CssClass="lightbackground" OnRowCommand="jobreceived_RowCommand" OnRowDataBound="jobreceived_RowDataBound" OnSelectedIndexChanged="jobreceived_SelectedIndexChanged" >--%>
        <%--<AlternatingRowStyle CssClass="dullbackground" />--%>
        <rowstyle backcolor="white"/>
        <alternatingrowstyle backcolor="#F0FFF0"/>
        <Columns>
        
        <asp:BoundField DataField = 'TYPESETTER' HeaderText="TYPESETTER" HeaderStyle-HorizontalAlign = "Left" />
        <asp:BoundField DataField="SAM/NON-SAM" HeaderText="SAM NON-SAM" HeaderStyle-HorizontalAlign = "Left"  HtmlEncode = "false" />
        <asp:BoundField DataField="JOURNAL ACRONYM" HeaderText="JOURNAL ACRONYM"  HeaderStyle-HorizontalAlign = "Left" HtmlEncode = "false" />
        <asp:BoundField DataField="CATS MAN.ID" HeaderText="CATS MAN.ID"  HeaderStyle-HorizontalAlign = "Left"/>
        <asp:BoundField DataField="TOTAL NO.REVISES" HeaderText="TOTAL NO OF REVISES" HeaderStyle-HorizontalAlign = "Left" HtmlEncode = "false" />
        <asp:BoundField  DataField = "TOTAL NO.PREVIEW FILES" HeaderText="TOTAL NO OF PREVIEW FILES"  HeaderStyle-HorizontalAlign = "Left"  HtmlEncode = "false" />
        <asp:BoundField DataField= "TOTAL" HeaderText = "TOTAL"  HeaderStyle-HorizontalAlign = "Left" />
        
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

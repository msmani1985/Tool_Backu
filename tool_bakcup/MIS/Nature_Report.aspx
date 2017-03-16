<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nature_Report.aspx.cs" Inherits="Nature_Report" %>

<!DOCTYPE html>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="titlediv" class="dptitle">
        SREP&nbsp; Report
    </div>
                                                <asp:ImageButton ID="cmd_Excel_Export" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                                    ToolTip="Export Excel" OnClick="cmd_Excel_Export_Click" />
        <br />
     <div align="center">
         <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
 
        <asp:GridView ID="grdProdReport" runat="server"   AutoGenerateColumns="false" Font-Size="8pt" CssClass="lightbackground"     emptydatatext="No data available." >

         <rowstyle backcolor="white" Height="22px"/>
                <HeaderStyle CssClass="darkbackground" />
        <alternatingrowstyle backcolor="#F0FFF0" Height="22px"/>
            <Columns>
                 <asp:TemplateField >
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
                <asp:BoundField HeaderText="ArticleName" DataField="AARTICLECODE"/>
                <asp:BoundField HeaderText="Author‎" DataField="Author" />
                <asp:BoundField HeaderText="Email‎"  DataField="AEMAIL"/>
                <asp:BoundField HeaderText="Name"  DataField="fname"/>
                <asp:BoundField HeaderText="Mail Sent‎"  DataField="SREP_Sent_On"/>
                <asp:BoundField HeaderText="File name‎"  DataField="pdf_name" />
            
                     </Columns>
                <EmptyDataTemplate>
        <label style="color:Red;font-weight:bold">No records found !</label>
        </EmptyDataTemplate>
         
        </asp:GridView>
 
     </div>
       
        <br />
    <br />
 
    <div runat="server" id="errMessage" class="errorMsg" ></div>
    </form>
</body>
</html>
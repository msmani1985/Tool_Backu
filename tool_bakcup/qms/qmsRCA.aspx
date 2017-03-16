<%@ Page Language="C#" AutoEventWireup="true" CodeFile="qmsRCA.aspx.cs" Inherits="qmsRCA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>

    <style type="text/css">
  .Initial
  {
    display: block;
    padding: 4px 18px 4px 18px;
    float: left;
    background: green no-repeat right top;
    color: Black;
    font-weight: bold;
  }
  .Initial:hover
  {
    color: White;
    background: green no-repeat right top;
  }
  .Clicked
  {
    float: left;
    display: block;
    background: green  no-repeat right top;
    padding: 4px 18px 4px 18px;
    color: Black;
    font-weight: bold;
    color: White;
  }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="80%" align="center">
      <tr>
        <td>
          <asp:Button Text="Internal" BorderStyle="None" ID="Tab1" CssClass="Initial" runat="server"
              OnClick="Tab1_Click" />
          <asp:Button Text="External" BorderStyle="None" ID="Tab2" CssClass="Initial" runat="server"
              OnClick="Tab2_Click" />

          <asp:MultiView ID="MainView" runat="server">
            <asp:View ID="View1" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                <tr>
                  <td>
                    <h3>
                      <span>View 1 </span>
                    </h3>
                  </td>
                </tr>
              </table>
            </asp:View>
            <asp:View ID="View2" runat="server">
              <table style="width: 100%; border-width: 1px; border-color: #666; border-style: solid">
                
                <tr>
                  <td><asp:Label ID="lblFileBrowse" Text="Select Excel File" runat="server"></asp:Label></td><td style="width: 363px"><asp:FileUpload ID="fileBrowse" runat="server" /></td><td> <asp:Button id="btnFileSubmit" runat="server" Text="Load" OnClick="btnFileSubmit_Click" Width="73px" /> </td>
                </tr>
                <tr>
   
                </tr>
              </table>
  <div>
              
                              <asp:Panel ID="pnlSAMProfile" runat="server" ScrollBars="Both" Height="400" Width="850px" Visible="false">
   <asp:GridView ID="gvSAMProfileNew"  EnableViewState="true" runat="server" OnRowCreated="gvSAMProfileNew_RowCreated"   OnRowDataBound="gvSAMProfileNew_RowDataBound" OnRowCommand="gvSAMProfileNew_RowCommand" OnRowEditing="gvSAMProfileNew_RowEditing" Height="42px" Width="882px"  >
   <%--<Columns>--%>
   <%--<asp:BoundField  ReadOnly="True" HeaderText="Name" />
   <asp:BoundField  ReadOnly="True" HeaderText="Description" />--%>
  <%-- </Columns>--%>


   </asp:GridView>
  </asp:Panel>
  </div>              
            </asp:View>
            
          </asp:MultiView>
        </td>
      </tr>
    </table>

    
    
   <%-->  <marquee behavior="slide" direction="left">Your slide-in text goes here</marquee> --%>
    
    </form>
</body>
</html>

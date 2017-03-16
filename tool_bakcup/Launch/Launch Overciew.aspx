<%@ page language="C#" autoeventwireup="true" inherits="Launch_Overciew, App_Web_unmairk5" enableeventvalidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />  
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
            <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grdLaunchView.ClientID %>').Scrollable({
        ScrollHeight: 400
       
    });
});
</script>
 <style type="text/css">
.gridP
{
background:Orange;
font-weight:bold;
color:White;
}
.gridC
{
background:Gray;
font-weight:bold;
color:White;
}
.gridDel
{
background:Green;
font-weight:bold;
color:White;
}
.gridWIP
{
background:LightGreen;
font-weight:bold;
color:White;
}
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        
    <div>
            <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
       <div class="dptitle">Launch Overview</div>
       <table>
         <tr>
         <td  align="right">
             <asp:Label ID="Label1" BackColor="orange" runat="server" Text="P = Pending" Height="17px" Width="80px"></asp:Label>
             <asp:Label ID="Label2" BackColor="LightGreen" runat="server" Text="WIP = in progress" Height="17px" Width="166px"></asp:Label>
         </td>
         <td></td>
         </tr>
           <tr>
         <td  align="right">
             <asp:Label ID="Label7" BackColor="Gray" runat="server" Text="C = cancelled" Height="17px" Width="80px"></asp:Label>
             <asp:Label ID="Label8" BackColor="green" runat="server" Text="Del. = finished and delivered" Height="17px" Width="166px"></asp:Label>
         </td>
         <td></td>
         </tr>
              <tr>
                                    <td align="center">
                                        <strong>Customer</strong>
                                  
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="200px" TabIndex = "1" OnSelectedIndexChanged="drpCustomerSearch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <strong>Location</strong>
                                  
                                        <asp:DropDownList ID="DropLocation" runat="server" Width="150px" TabIndex = "1">
                                        </asp:DropDownList>
                                        <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click"  />
                                        </td></tr>
         <tr>
         <td align="center" style="width: 1100px">
            Month&nbsp;
                    <asp:DropDownList ID="DDMonthList" runat="server">
                    <asp:ListItem Value="0">---All---</asp:ListItem>
                        <asp:ListItem Value="1">January</asp:ListItem>
                        <asp:ListItem Value="2">February</asp:ListItem>
                        <asp:ListItem Value="3">March</asp:ListItem>
                        <asp:ListItem Value="4">April</asp:ListItem>
                        <asp:ListItem Value="5">May</asp:ListItem>
                        <asp:ListItem Value="6">June</asp:ListItem>
                        <asp:ListItem Value="7">July</asp:ListItem>
                        <asp:ListItem Value="8">August</asp:ListItem>
                        <asp:ListItem Value="9">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:DropDownList>
                &nbsp;&nbsp;Year&nbsp;
                    <asp:DropDownList ID="DDYearList" runat="server">
                        <asp:ListItem Value="0">---All---</asp:ListItem>
                        <asp:ListItem Value="2014">2014</asp:ListItem>
                        <asp:ListItem Value="2015">2015</asp:ListItem>
                        <asp:ListItem Value="2016">2016</asp:ListItem>
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                        <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
         </tr>
    <tr>
    <td align="right">
        
    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
    ToolTip="Save"  TabIndex="41" OnClick="cmd_Save_Click" />
    <asp:ImageButton id="cmd_Excel" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" ToolTip="Excel" OnClick="cmd_Excel_Click"></asp:ImageButton>
    </td>
    <td></td>
    </tr>
    <tr >
    <td  colspan="2">
        <asp:GridView ID="grdLaunchView" runat="server"  AutoGenerateColumns="false"
           EmptyDataText="No data available." Font-Size="8pt"  AllowSorting="True" OnSorting="grdLaunchView_Sorting"
          PageSize="7" OnRowCommand="grdLaunch_RowCommand" OnRowDataBound="grdLaunch_RowDataBound" DataKeyNames="Pro_name">
            <HeaderStyle CssClass="GVFixedHeader" />
            <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
            <asp:TemplateField ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>Serial No.</HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="jobid" HeaderText="Job ID" SortExpression="jobid" ItemStyle-HorizontalAlign="Left"/>
                <asp:BoundField DataField="Cust_name" HeaderText="Customer" SortExpression="Cust_name" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Location" HeaderText="Location" SortExpression="Location" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="PROJECTNAME" HeaderStyle-Width="300" SortExpression="PROJECTNAME" ItemStyle-Width="300" HeaderText="Job Name"  />
               <asp:TemplateField SortExpression="projecteditor" ItemStyle-Width="5px" ItemStyle-HorizontalAlign="Center">
                     <HeaderTemplate> Project Editor
                            <asp:DropDownList ID="drpPEName" Width="100" runat="server" OnSelectedIndexChanged="drpPEName_SelectedIndexChanged" AutoPostBack="True" >
                            </asp:DropDownList>
                     </HeaderTemplate>
                     <ItemTemplate>
                            <asp:Label ID="lblPEName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"projecteditor") %>'></asp:Label>
                     </ItemTemplate>
              </asp:TemplateField>
               
                
               <%-- <asp:BoundField DataField="projecteditor" HeaderText="Project Editor" SortExpression="projecteditor" />--%>
                <asp:BoundField DataField="Pages" HeaderText="No.of Pages" SortExpression="Pages" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Rate" HeaderText="Cost" SortExpression="Rate"   ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Cur" HeaderText="Currency"  SortExpression="Cur"   ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="cost" HeaderText="Page or Hour" SortExpression="cost"  ItemStyle-HorizontalAlign="Center"/>
               
                
                   <asp:TemplateField HeaderText="Status"    ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:DropDownList ID="DropStatus"  runat="server">
                     </asp:DropDownList>
                 </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Reports"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                   <asp:ImageButton ID="imglaunchReport" runat="server"  ImageUrl="~/images/QMS/pdf.png" CommandName="Launch Report"/>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quote"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                   <asp:ImageButton ID="imglaunchQuote" runat="server"  ImageUrl="~/images/QMS/pdf.png" CommandName="Launch Quote"/>
                 </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Mail" HeaderStyle-Width="30"  ItemStyle-Width="30"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                 <asp:ImageButton ID="imgMail" runat="server" Width="30" CommandName="Mail Details" ImageUrl="~/images/temail.jpg" />
                  
                 </ItemTemplate>
                </asp:TemplateField>
                 <asp:BoundField DataField="Desc1" HeaderText="Comments"  />
                 <asp:BoundField DataField="Pro_id" HeaderText="Pro_ID"  Visible="false"  />
                  <asp:TemplateField Visible="false" >
                 <ItemTemplate>
                     <asp:Label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"pro_id") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
          </Columns>
        </asp:GridView>
        </td></tr></table>
        &nbsp;&nbsp;&nbsp;
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" />
     </div>
     
    </form>
</body>
</html>

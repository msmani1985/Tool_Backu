<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Launch Overciew.aspx.cs" Inherits="Launch_Overciew" EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/CSS.css" rel="stylesheet" type="text/css" /> 
    <script src="scripts/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="scripts/jquery.blockUI.js" type="text/javascript"></script>
    <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grdLaunchView.ClientID %>').Scrollable({
        ScrollHeight: 400
       
    });
});
</script>
    <script type = "text/javascript">
        $(document).ready(function () {
            $('#<%=grdFilewiseAmends.ClientID %>').Scrollable({
        ScrollHeight: 30

    });
        });
        function Hidepopup() {
            $find("ModalPopupExtender1").hide();
            return false;
        }
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
     .auto-style1 {
         width: 754px;
     }
     .auto-style2 {
         width: 473px;
     }
     .auto-style3 {
         width: 180px;
     }
     .auto-style4 {
         width: 114px;
     }
 </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        
    <div>
            <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
       <div class="dptitle">Launch Overview</div>
       <table>
         <tr>
         
         <td class="auto-style1"></td>
         </tr>
           <tr>
         
         <td class="auto-style1"></td>
         </tr>
           <tr>
               <td colspan="4">
                    <table>
                        <tr>
                            <td class="auto-style4">

                            </td>
                            <td align="center" class="auto-style2">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <strong>Customer</strong>
                                  
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="200px" TabIndex = "1" OnSelectedIndexChanged="drpCustomerSearch_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <strong>Location</strong>
                                  
                                        <asp:DropDownList ID="DropLocation" runat="server" Width="150px" TabIndex = "1">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                            </td>
                            <td align="left" class="auto-style3">
                                <asp:Button Text="Submit" CssClass="dpbutton" runat="server"  ID="btnSubmit" OnClick="btnSubmit_Click"  />
                            </td>
                            <td  align="right">
                                <asp:Label ID="Label1" BackColor="orange" runat="server" Text="P = Pending" Height="17px" Width="80px"></asp:Label>
                                <asp:Label ID="Label2" BackColor="LightGreen" runat="server" Text="WIP = in progress" Height="17px" Width="166px"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">

                            </td>
                            <td align="center" class="auto-style2">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                            Month&nbsp;
                                            <asp:DropDownList ID="DDMonthList" runat="server" >
                                            <%--<asp:ListItem Value="0">---All---</asp:ListItem>--%>
                                               <%-- <asp:ListItem Value="1">January</asp:ListItem>
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
                                                <asp:ListItem Value="12">December</asp:ListItem>--%>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;Year&nbsp;
                                            <asp:DropDownList ID="DDYearList" runat="server" >
                                                <asp:ListItem Value="0">---All---</asp:ListItem>
                                                <%--<asp:ListItem Value="2014">2014</asp:ListItem>--%>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            <td class="auto-style3">

                            </td>
                                <td  align="right">
                                    <asp:Label ID="Label7" BackColor="Gray" runat="server" Text="C = cancelled" Height="17px" Width="80px"></asp:Label>
                                    <asp:Label ID="Label8" BackColor="green" runat="server" Text="Del. = finished and delivered" Height="17px" Width="166px"></asp:Label>
                                </td>
                        </tr>
                    </table>
               </td>
           </tr>
        
    <tr>
        <td  align="CENTER">ProjectName / JobID : <asp:TextBox ID="txtProjectName" runat="server" Width="448px"></asp:TextBox></td>
    <td align="right" colspan="2">
        
    <asp:ImageButton ID="cmd_Save" ImageUrl="~/images/tools/j_save.png" runat="server"
    ToolTip="Save"  TabIndex="41" OnClick="cmd_Save_Click" Visible="false" />
    <asp:ImageButton id="cmd_Excel" tabIndex="41"  runat="server" ImageUrl="~/images/tools/j_excel.png" ToolTip="Launch Overview" OnClick="cmd_Excel_Click"></asp:ImageButton>&nbsp;
    </td>
    
    </tr>
    <tr >
    <td  colspan="2">
        <asp:GridView ID="grdLaunchView" runat="server"  AutoGenerateColumns="false"
           EmptyDataText="No data available." Font-Size="8pt"  AllowSorting="True" OnSorting="grdLaunchView_Sorting"
          PageSize="7" OnRowCommand="grdLaunch_RowCommand" OnRowDataBound="grdLaunch_RowDataBound" DataKeyNames="Pro_name">
            <HeaderStyle CssClass="GVFixedHeader" />
            <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                <HeaderTemplate>Serial No.</HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date"  ItemStyle-HorizontalAlign="Center" />
                 <asp:TemplateField SortExpression="JOBID" HeaderText="JOBID"  ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                            <asp:Label ID="lbljobid"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"jobid") %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
                <asp:BoundField DataField="Cust_name" HeaderText="Customer" SortExpression="Cust_name" ItemStyle-HorizontalAlign="Left" />
                <%--<asp:BoundField DataField="PROJECTNAME" SortExpression="PROJECTNAME" HeaderText="Job Name"  />--%>
                <asp:TemplateField SortExpression="PROJECTNAME" HeaderText="PROJECTNAME"  ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                            <asp:Label ID="lblPROJECTNAME"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PROJECTNAME") %>' ></asp:Label>
                     </ItemTemplate>
                 </asp:TemplateField>
               <asp:TemplateField SortExpression="projecteditor"  ItemStyle-HorizontalAlign="Center">
                     <HeaderTemplate> Project Editor
                            <asp:DropDownList ID="drpPEName" Width="100" runat="server" OnSelectedIndexChanged="drpPEName_SelectedIndexChanged" AutoPostBack="True" >
                            </asp:DropDownList>
                     </HeaderTemplate>
                     <ItemTemplate>
                            <asp:Label ID="lblPEName"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"projecteditor") %>'></asp:Label>
                     </ItemTemplate>
              </asp:TemplateField>
                <asp:BoundField DataField="Pages" HeaderText="No.of Pages" SortExpression="Pages" ItemStyle-HorizontalAlign="Center"/>
                <%--<asp:BoundField DataField="Rate" HeaderText="Cost" SortExpression="Rate"   ItemStyle-HorizontalAlign="Center"/>--%>
                <asp:TemplateField HeaderText="Cost"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="lblCost"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cost") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoiced Cost"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:LinkButton ID="lblInvCost" CommandName="InvCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ICost") %>'></asp:LinkButton>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Cur" HeaderText="Currency"  SortExpression="Cur"   ItemStyle-HorizontalAlign="Center"/>
               <asp:TemplateField HeaderText="Page or Hour"   ItemStyle-HorizontalAlign="Center">
                 <ItemTemplate>
                            <asp:Label ID="lblRate"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CostTypeID") %>'></asp:Label>
                 </ItemTemplate>
              </asp:TemplateField>
                <asp:TemplateField HeaderText="Status"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="lblStatus"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Status") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Stage"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="lblStage"  runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"AmendName") %>'></asp:Label>
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
                
                <asp:TemplateField HeaderText="Mail"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                 <asp:ImageButton ID="imgMail" runat="server" Width="30" CommandName="Mail Details" ImageUrl="~/images/temail.jpg" />
                  <asp:Label ID="lblMailDetails" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"MailDetails") %>' Visible="false"></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Comments"  ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:LinkButton ID="lblComments"  CommandName="FileDetails" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Desc1") %>'></asp:LinkButton>
                 </ItemTemplate>
                </asp:TemplateField>
                 <%--<asp:BoundField DataField="Desc1" HeaderText="Comments"  />--%>
                 <asp:BoundField DataField="Pro_id" HeaderText="Pro_ID"  Visible="false"  />
                  <asp:TemplateField Visible="false" >
                 <ItemTemplate>
                     <asp:Label ID="lblid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"pro_id") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WO / PO Number">
                 <ItemTemplate>
                     <asp:Label ID="lblJobNo" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Jobno") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NonLaunch ID">
                 <ItemTemplate>
                     <asp:Label ID="lblNLID" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"LaunchID") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quote Created">
                 <ItemTemplate>
                     <asp:Label ID="lblQuoteCreated" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"CREATED_Quote") %>'></asp:Label>
                  </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Invoiced POs"  ItemStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" >
                 <ItemTemplate>
                     <asp:Label ID="lblInvPO" Width="300" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"inv_PoNumber") %>'></asp:Label>
                 </ItemTemplate>
                </asp:TemplateField>
          </Columns>
        </asp:GridView>
        <asp:Label ID="lblmsg" runat="server"/>
        <asp:Button ID="modelPopup" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server"  DropShadow="false" TargetControlID="modelPopup" 
            PopupControlID="updatePanel" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="updatePanel" Width="700" Height="350" runat="server" CssClass="modalPopup" style = "display:none">
            
                <table align="right">
                    <tr>
                        <td>
                            <asp:ImageButton ImageUrl="images/tools/no.png" runat="server" ID="imgFQ"  ToolTip="Save"/>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label5" runat="server" Text="Cost Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                   
                <table align = "center">
	            <tr>
                    <td>
                        <asp:GridView ID="grdFilewiseAmends" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                            EmptyDataText="No Data Found.." 
                            CssClass="lightbackground" ClientIDMode="Static" Width="500px" >
                            <HeaderStyle CssClass="darkbackground"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                             <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Serial No.</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Description</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MPTITLE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Quantity</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"NUMPAGES") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Rate</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Rate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Category</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Category") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
	            </tr>
            </table>
        </asp:Panel>
        <asp:Label ID="Label3" runat="server"/>
        <asp:Button ID="btninvSummary" runat="server" style="display:none" />
        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server"  DropShadow="false" TargetControlID="btninvSummary" 
            PopupControlID="Panel1" BackgroundCssClass="modalBackground">
        </asp:ModalPopupExtender>
        <asp:Panel ID="Panel1" Width="700" Height="350" runat="server" CssClass="modalPopup" style = "display:none">
            
                <table align="right">
                    <tr>
                        <td>
                            <asp:ImageButton ImageUrl="images/tools/no.png" runat="server" ID="ImageButton1"  ToolTip="Save"/>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="Label4" runat="server" Text="Invoice Details:" Font-Bold="true" Font-Size="Medium" ForeColor="Green"></asp:Label>
                   
                <table align = "center">
	            <tr>
                    <td>
                        <asp:GridView ID="gvInvSummary" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                            EmptyDataText="No Data Found.." 
                            CssClass="lightbackground" ClientIDMode="Static" Width="500px" >
                            <HeaderStyle CssClass="darkbackground"  />
                            <AlternatingRowStyle BackColor="#F2F2F2" />
                             <Columns>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Serial No.</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Invno</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Invno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Inv Date</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Idate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>Cost</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Cost") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <HeaderTemplate>PO Number</HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:Label ID="lblRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PONumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
	            </tr>
            </table>
        </asp:Panel>
        </td></tr></table>
     </div>
        <link rel="stylesheet" href ="Scripts/GridviewScroll.css" type="text/" />
        <script type="text/javascript" src="Scripts/jquery.min.js"></script>
        <script type="text/javascript" src="Scripts/jquery-ui.min.js"></script>
        <script type="text/javascript" src="Scripts/gridviewScroll.min.js"></script>
<script type="text/javascript">
    $(document).ready(function () {
        gridviewScroll();
    });

    function gridviewScroll() {
        $('#<%=grdLaunchView.ClientID%>').gridviewScroll({
            width: window.innerWidth - 10,
            height: window.innerHeight - 200,
            startHorizontal: 0,
            barhovercolor: "#848484",
            barcolor: "#848484"
        });
    }
</script>
    </form>
</body>
</html>

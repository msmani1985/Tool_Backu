<%@ page language="C#" autoeventwireup="true" inherits="qmssamprofile, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <link href="default.css" type="text/css" rel="stylesheet" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    function dvHistry_Popup(divhistry,state)
    {
    document.getElementById(divhistry).style.display=state;
    }
    function closeDiv(divhistry,state)
    {
    document.getElementById(divhistry).style.display=state;
    }
    </script>
    <style>
    .tblstyle td
    {
    padding:2px;
    border-top-style:none;
    border-bottom-style:solid;
    border-left-style:solid;
    border-right-style:none;
    border-color:Black;
    border-width:thin;


    }
 .tblstyle th
 {
    height:10px;
    width: 544px;
    position:relative;
    text-align:center;
    border-top-style:solid;
    border-bottom-style:solid;
    border-left-style:solid;
    border-right-style:none;
    border-color:Black;
    border-width:thin;
    background-color:Honeydew;


}


 }
        
    </style>
</head>

<body>
    <form id="form1" runat="server">
    
    <%-- <table width="100%" >
    <tr></tr>
    <tr>
    <td style="width: 1097px; height: 457px">--%>
    <ol id="toc">
    <li id="SAMProfile" runat="server">
    <asp:LinkButton ID="lbSAMProfile" runat="server" OnClick="lbSAMProfile_Click" TabIndex="1" >SAMProfile View</asp:LinkButton>
    </li>
    <li id="SAMProfileDetails" runat="server">
    <asp:LinkButton ID="lbSAMProfileEdit" runat="server" OnClick="lbSAMProfileEdit_Click" TabIndex="2">SAMProfile New</asp:LinkButton>
    </li>
    <li id="SAMProfileUpdate" runat="server">
    <asp:LinkButton ID ="lbSAMProfileUpdate" runat="server" OnClick="lgSAMProfileUpdate_Click" TabIndex="3">SAMProfile Edit</asp:LinkButton>
    </li>
    </ol>
   
   <div class="content" id="dvSAMProfile" style="width:1092px; height:500px;" runat="server" >
        <table width="100%">
    <tr>
    <td style="width: 41px"></td>
    <td style="width: 138px"></td>
    <td style="width: 270px"></td>
    <td style="width: 8px"></td>
    <td style="width: 108px"></td>
    <td style="width: 304px"></td>
    <td></td>
    </tr>
    <tr>
    <td style="width: 41px; height: 15px"></td>
        <td style="width: 138px; height: 15px">
        <strong>Select Customer</strong></td>
    <td style="width: 270px; height: 15px"><asp:DropDownList ID="ddlCustomer" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomer_OnSelectedIndexChanged"  runat="server" Width="267px"></asp:DropDownList></td>
    <td style="height: 15px; width: 8px;"></td>
    <td style="width: 108px">
        <strong>Select Journal</strong></td>
        <td style="width: 304px"><asp:DropDownList ID="ddlJournal" AutoPostBack="true" OnSelectedIndexChanged="ddlJournal_OnSelectedIndexChanged" runat="server" Width="299px"></asp:DropDownList></td>
        <td></td>
        <td></td>
        <td><asp:Button ID="btnView" Visible="false" Text="Submit" runat="server" BorderWidth="1" Font-Bold="True" OnClick="btnView_Click" Width="89px" /></td>
    </tr>
    <tr>
    <td style="width: 41px; height: 6px;"></td>
    <td style="width: 138px; height: 6px;"></td>
    <td style="width: 270px; height: 6px;"></td>
    <td style="width: 8px; height: 6px;"></td>
    <td style="width: 108px; height: 6px;"></td>
        <td style="width: 304px; height: 6px;"></td>
        <td style="height: 6px"></td>
    </tr>
    </table>
    <table id="tblHistry">
   <tr>
   <td style="width: 183px"></td>
   
   <td valign="top" width="50%"  style="width: 249px; height: 120px; vertical-align:top">
<asp:Panel ID="pnlSAMProfileView" runat="server" ScrollBars="Both" Height="400" Width="850px" Visible="false">
   <asp:GridView ID="gvSAMProfile" DataKeyNames="SAMProfile_code" runat="server" BorderWidth="1" AutoGenerateColumns="false" Width="630px" Height="102px" EnableViewState="true" OnRowCommand="gvSAMProfile_RowCommand" OnRowDataBound="gvSAMProfile_DataBound" >
<%--<Columns>
<asp:BoundField DataField="SAMProfile_code" />
<asp:BoundField DataField="SAMProfile_Title_Name" />
<asp:BoundField DataField="SAMProfile_Title_Desc" />
<asp:BoundField DataField="Updated_Date" />    
</Columns>--%>
 <Columns>
   <asp:TemplateField HeaderText="SLNO">
   <ItemTemplate>
   <%# Container.DataItemIndex + 1%>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="ProfileCode" Visible="false">
   <ItemTemplate>
   
   <%# DataBinder.Eval(Container.DataItem, "SAMProfile_code")%>
   <asp:HiddenField ID="hdnSAMProfileid" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SAMProfile_code")%>' /> 
   
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Name">
  
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem, "SAMProfile_Title_Name")%>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Description"> 
 
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem,"SAMProfile_Title_Desc") %>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Updated Date">
   <ItemTemplate>
   <asp:HiddenField  ID="hdnHistry" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Updated_Date") %>' />
  <asp:LinkButton EnableViewState="true" ID="lbViewHistry" runat="server" CommandName="cmdHistry" CommandArgument='<%# Container.DataItemIndex %>'  OnClick="lbViewHistry_Click">
 <%--<asp:ImageButton ID="imgindiabutton" ToolTip="Click to view IndiaInvoice" ImageUrl="images/te-proof.gif"  runat="server" style="cursor:pointer;border:none" CommandArgument='<%# Container.DataItemIndex %>' Height="28" CommandName="cmdHistry"    />--%>
               
   <%# DataBinder.Eval(Container.DataItem, "Updated_Date")%>
   
  </asp:LinkButton>
   </ItemTemplate>
   </asp:TemplateField>
   </Columns>
   </asp:GridView>
       &nbsp;
       </asp:Panel>
   </td>
<td style="width: 208px">
    &nbsp;</td>
   
   <td valign="top" style=" vertical-align:top; width: 850px;">
   
  
    <div id="dvHistry" runat="server" style=" vertical-align:top; background-color:Honeydew; border:1px solid black; display:none"   >
   <table style="width:500px; height: 210px" align="center">
   <tr>
   <td align="center"><b> SAMProfile Histry</b></td>
   <td align="right" style="background-color:Honeydew; color: black; font-weight: bold">
       <a href="#" onclick="closeDiv('dvHistry','none'); return false" title="Close" style="color: black;">
       [x]</a>
       </td>
       
   </tr>
   <tr>
   <td align="center" style="height: 260px;">
   
   <asp:GridView ID="gvSAMProfileHistry" runat="server" AutoGenerateColumns="false" EnableViewState="true"  BorderWidth="1px" BorderColor="black" CssClass="tblstyle" BackColor="Honeydew">
   <Columns>
   <asp:TemplateField HeaderText="SLNO" >
   <ItemTemplate>
   <%# Container.DataItemIndex + 1%>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="SAMProfile Title Name">
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem,"SAMProfile_Title_Name") %>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="SAMProfile Title Description">
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem, "SAMProfile_Title_Desc")%>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Updated By">
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem, "Updated_By")%>
   </ItemTemplate>
   </asp:TemplateField>
   <asp:TemplateField HeaderText="Updated Date">
   <ItemTemplate>
   <%# DataBinder.Eval(Container.DataItem, "Updated_Date")%>
   </ItemTemplate>
   
   </asp:TemplateField>
   </Columns>
   </asp:GridView>
   </td>
   </tr>
   </table>
   
   </div>
   </td>
   </tr>
  </table>
  <table>
  <tr>
  <td>
  <span id="ErrMsg1" runat="server" style="font-size: 8pt; background-color: Yellow;color: Black;"></span>
  </td>
  </tr>
  </table>
    </div>
    
   <div class="content" id="dvSAMProfileEdit" style=" width:1155px;height:500px; " runat="server" Visible="false">
   <div>
   <table width="100%">
   <tr>
   <td style="width: 216px; height: 38px;"></td>
   <td style="width: 314px; height: 38px;">
       <strong>Select Customer </strong>
   </td>
   <td style="width: 45px; height: 38px;"></td>
   
   <td style="width: 747px; height: 38px;"><asp:DropDownList ID="ddlSAMProfileCustomer" AutoPostBack="true" runat="server" Width="326px" OnSelectedIndexChanged="ddlSAMProfileCustomer_SelectedIndexChanged"></asp:DropDownList> </td>
 
   <td style="height: 38px"></td>
   <td style="width: 357px; height: 38px;">
       <strong>Select Journal </strong>
   </td>
   <td style="width: 63px; height: 38px;"></td>
   
   <td style="width: 658px; height: 38px;"><asp:DropDownList ID="ddlSAMProfileJournal" AutoPostBack="true" runat="server" Width="330px" OnSelectedIndexChanged="ddlSAMProfileJournal_SelectedIndexChanged"></asp:DropDownList></td>
   <td style="width: 62px; height: 38px;"></td>

  
   
   </tr>
</table>
</div>
<div id="dvFileUpload" visible="false" runat="server">

<table width="100%">
   <tr>
   <td style="height: 31px"></td>
   <td style="height: 31px">
       &nbsp; &nbsp; &nbsp;&nbsp;</td>
   <td style="width: 98px; height: 31px;"><asp:Label ID="lblfileupload" runat="server">Select excel file</asp:Label> </td>
   <td style="width: 203px; height: 31px;">
   <asp:FileUpload ID="ctlFileUpload" runat="server" />
   
   </td>
<td style="width: 75px; height: 31px;"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmitClick" Width="89px"/></td>
<td style="height: 31px">
<strong><asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click1" Width="88px" /></strong>
</td>
   </tr>
</table>
       &nbsp;
       </div>
       <div>
       <table style="width: 70%; height: 103px">
  <tr>
  <td>
      &nbsp; &nbsp; &nbsp;&nbsp;
  </td>
<td style="width: 1051px">
  <asp:Panel ID="pnlSAMProfile" runat="server" ScrollBars="Both" Height="400" Width="850px" Visible="false">
   <asp:GridView ID="gvSAMProfileNew"  EnableViewState="true" runat="server" OnRowCreated="gvSAMProfileNew_RowCreated"   OnRowDataBound="gvSAMProfileNew_RowDataBound" OnRowCommand="gvSAMProfileNew_RowCommand" OnRowEditing="gvSAMProfileNew_RowEditing" Height="42px" Width="882px"  >
   <%--<Columns>--%>
   <%--<asp:BoundField  ReadOnly="True" HeaderText="Name" />
   <asp:BoundField  ReadOnly="True" HeaderText="Description" />--%>
  <%-- </Columns>--%>

   </asp:GridView>
  </asp:Panel>
</td>

<td>
    
    </td> 
    
   </tr>
   <tr>
   <td colspan="3" align="center">
   <asp:Button runat="server" ID="btnSave1" Text="Save" Font-Bold="True" Font-Size="Larger" OnClick="btnSave_Click" Visible="False" Width="112px" />
   
   </td>
   </tr>
    <tr>
    <td colspan="5" align="center" style="height: 15px">
    <span id="Messagebox" runat="server" style="font-size: 8pt; background-color: Yellow;color: Black;"></span>
                                </td>
                            </tr>
    </table> 
  </div>
  <table>
  <tr>
  <td>
  <span id="ErrMsg2" runat="server" style="font-size: 8pt; background-color: Yellow;color: Black;"></span>
  </td>
  </tr>
  </table>
  </div>
   <div class="content" id="dvSAMProfileUpdate" style="width:1092px; height:500px; "  runat="server"  Visible="false">
   <div>
   <table width="100%">
   <tr>
   <td style="width: 56px"></td>
   <td style="width: 118px">
       <strong>Select Customer </strong>
   </td>
   <td></td>
   <td style="width: 333px"><asp:DropDownList ID="ddlUptCustomer" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlUptCustomer_OnSelectedIndexChanged" Width="210px" ></asp:DropDownList></td>
   <td></td>
   <td style="width: 112px"><strong>Salect Journal</strong></td>
   <td></td>
   <td><asp:DropDownList ID="ddlUptJournal" AutoPostBack="true" runat="server" Width="210px" OnSelectedIndexChanged="ddlUptJournal_SelectedIndexChanged"></asp:DropDownList></td>
   <td></td>
   <td><asp:Button ID="btnUPDSubmit" runat="server" Text="Submit" Font-Bold="True" Font-Size="Larger" Width="113px" OnClick="btnUPDSubmit_Click" Visible="False" /></td>
   <td></td>
   </tr>
   
   
   </table>
</div>
<div>
<table width="100%">
<tr>
 <td style="width: 22px; height: 164px;">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
</td>
<td style="height: 164px; width: 949px;">
<asp:Panel ID="pnlSAMPProfileupdate" runat="server" ScrollBars="Both" Height="400" Width="1050px" Visible="false">
<asp:GridView ID="gvSAMProfileUpdate" runat="server" AutoGenerateColumns="false" Height="156px" Width="1050px" OnRowEditing=" gvSAMProfileUpdate_RowEditing" OnSelectedIndexChanged="gvSAMProfileUpdate_SelectedIndexChanged" OnRowCommand="gvSAMProfileUpdate_RowCommand" OnRowUpdating="gvSAMProfileUpdate_RowUpdating" OnRowCancelingEdit="gvSAMProfileUpdate_RowCancelingEdit" OnRowDataBound="gvSAMProfileUpdate_DataBound"  >
<Columns>
<asp:BoundField DataField="SAMProfile_Title_Name"   />
<asp:BoundField DataField="SAMProfile_Title_Desc"  />
<asp:ButtonField ButtonType="Link" Text="EDIT" CommandName="Edit" />
<asp:ButtonField ButtonType="Link" Text="UPDATE" CommandName="Update" />
<asp:ButtonField ButtonType="Link" Text="CANCEL" CommandName="Cancel" />
<asp:TemplateField>
<ItemTemplate>
<asp:HiddenField ID="hdnSAMProfileID" runat="server" Value='<%# Eval("SAMProfile_code") %>' />
<asp:TextBox ID="TextBox1" Visible="false" runat="server"></asp:TextBox>
<asp:TextBox ID="TextBox2" Visible="false" runat="server"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
<%--<asp:TemplateField>--%>
<%--<EditItemTemplate>

<asp:TextBox ID="txtUptDesc" runat="server" TextMode="MultiLine" ></asp:TextBox>
</EditItemTemplate>
</asp:TemplateField>--%>
<%--<ItemTemplate >
<%--<asp:LinkButton Text="Edit" ID="lbtnEdit" runat="server" OnClick="btnEdit_Click" Font-Bold="true" Width="200px" CommandName="Edit" ></asp:LinkButton>
<asp:LinkButton Text="Update" ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" Font-Bold="true" Width="200px" CommandName="Update" Visible="false"></asp:LinkButton>
<asp:LinkButton Text="Cancel" ID="lbtnCancel" runat="server" OnClick="btnCancel_Click" Font-Bold="true" Width="200px"  CommandName="Cancel"></asp:LinkButton>
<%--<asp:PlaceHolder ID="ph" runat=server></asp:PlaceHolder>
</ItemTemplate>--%>
<%--</asp:TemplateField>--%>
</Columns>
</asp:GridView>
 
</asp:Panel>
</td>
<td style="height: 164px; width: 137px;">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;</td>
</tr>
<%--<tr>
<td>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
</td>
<td style="width: 949px" >
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
<asp:Button ID="btnUpdate" Text="Save" runat="server" Height="30px" Width="125px" Font-Bold="True" Font-Size="Larger" Visible="false"   />

</td>
<td style="width: 137px">
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp;
</td>
</tr>--%>

</table>

<div id="dvMSG">
<table width="100%" >
  <tr>
  <td style="height: 15px"></td>
  <td style="height: 15px">
  <span id="ErrMsg3" runat="server" style="font-size: 8pt; background-color: Yellow;color: Black;"></span>
  </td>
  <td style="height: 15px"></td>
  </tr>
  </table>

</div>
</div>

   </div>

     
    </form>
</body>
</html>

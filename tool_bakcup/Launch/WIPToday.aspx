<%@ page language="C#" autoeventwireup="true" inherits="WIPToday, App_Web_lruasnqi" enablesessionstate="True" enableviewstate="true" enableviewstatemac="true" maintainscrollpositiononpostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div runat="server" id="runControls" visible="false" >
    <b><i>Apply Filter(s)</i></b><br />
Job Type:      
<asp:DropDownList ID="_ddlJobType" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddljobstage_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>
job Stage:
<asp:DropDownList ID="_ddlstage" runat="server" 
        AutoPostBack="true" onselectedindexchanged="ddljobstage_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>

Assigned: <asp:DropDownList ID="_ddlparentjobid" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlparentjobid_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>
Department: 
<asp:DropDownList ID="_ddldept" runat="server"  AutoPostBack="true" onselectedindexchanged="ddldept_SelectedIndexChanged">
<asp:ListItem Text="0" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="1" Value="one" ></asp:ListItem>
<asp:ListItem Text="2" Value="two" ></asp:ListItem>
</asp:DropDownList>
<br />
On Hold: 
<asp:DropDownList ID="_ddlonhold" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlonhold_SelectedIndexChanged">
<asp:ListItem Text="--NO FILTER--" Value="0" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList>
Customer: 
<asp:DropDownList ID="ddlcustomer" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlcustomer_SelectedIndexChanged">
<asp:ListItem Text="--NO FILTER--" Value="zero" Selected="true" ></asp:ListItem>
<asp:ListItem Text="YES" Value="1" ></asp:ListItem>
<asp:ListItem Text="NO" Value="2" ></asp:ListItem>
</asp:DropDownList>
</div>

<asp:GridView runat="server" ID="gvWIPtoday" 
        OnRowDataBound="gvWIPtoday_RowDataBound" EnableViewState="true" AutoGenerateColumns="false" AllowSorting="true" 
        OnSorting="gvWIPtoday_OnSort" OnSelectedIndexChanged="gvWIPtoday_SelectedIndexChanged"  >
        <Columns>
        <asp:TemplateField HeaderText="SNO">
        <ItemTemplate>
        <%# Container.DataItemIndex +1 %>
        </ItemTemplate>
        </asp:TemplateField>
       <%-- <asp:BoundField HeaderText="S. No." DataField="SNO" />--%>
        <asp:TemplateField>     
        <HeaderTemplate>
        <asp:DropDownList runat="server" ID="ddlJobType" OnSelectedIndexChanged="oList_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        </HeaderTemplate>
       <ItemTemplate>
       <%# DataBinder.Eval(Container.DataItem, "JOB_NUMBER")%>
       </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText="JOB_STAGE">
        <HeaderTemplate>
        <asp:DropDownList runat="server" ID="ddlstage" OnSelectedIndexChanged="oList_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Text="0" Value="0" ></asp:ListItem>
        </asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
       <%# DataBinder.Eval(Container.DataItem, "JOB_STAGE")%>
        </ItemTemplate>
        </asp:TemplateField>
<%--<asp:BoundField HeaderText="JOB_STAGE" DataField="JOB_STAGE" />--%>

        <asp:BoundField HeaderText="NAME" DataField="NAME" />
        <asp:BoundField HeaderText="SAM JOB" DataField="SAM_JOURNAL" />
        <asp:BoundField HeaderText="COPYEDIT" DataField="CE_JOURNAL" />
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList ID="ddlparentjobid"  OnSelectedIndexChanged="oList_SelectedIndexChanged" runat="server" AutoPostBack="true" >
        <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
       <%# DataBinder.Eval(Container.DataItem, "ASSIGNED")%>
        </ItemTemplate>
        </asp:TemplateField>
     <%--  <asp:BoundField HeaderText="ASSIGNED" DataField="ASSIGNED" />--%>
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList ID="ddldept"  OnSelectedIndexChanged="oList_SelectedIndexChanged" runat="server" AutoPostBack="true" Visible="false">
        <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
       <%# DataBinder.Eval(Container.DataItem, "DEPARTMENT")%>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="DEPARTMENT" DataField="DEPARTMENT" />
        <asp:BoundField HeaderText="EMPLOYEE" DataField="EMPLOYEE" />
        <asp:BoundField HeaderText="REC. DATE" DataField="RECEIVED_DATE" />
        <asp:BoundField HeaderText="CATS DUE DATE" DataField="CATS_DUE_DATE"  />
       <asp:TemplateField>
       <HeaderTemplate>
       <asp:DropDownList ID="ddlonhold" runat="server" OnSelectedIndexChanged="oList_SelectedIndexChanged" AutoPostBack="true" >
       <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList> 
       </HeaderTemplate>
       <ItemTemplate>
      <%# DataBinder.Eval(Container.DataItem, "ONHOLD")%>
       </ItemTemplate>
       </asp:TemplateField>
      <%--  <asp:BoundField HeaderText="HOLD DETAILS" DataField="ONHOLD"  />--%>
        </Columns>
       
        
</asp:GridView>    


    </div>
    </form>
</body>
</html>

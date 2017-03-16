<%@ page language="C#" autoeventwireup="true" inherits="Ratha_WIPTtoday, App_Web_w6b3pav3" enablesessionstate="True" enableviewstate="true" enableviewstatemac="true" maintainscrollpositiononpostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form2" runat="server">
        <div runat="server" id="runControls">
        <asp:GridView runat="server" ID="gridWIPTODAY" 
        OnRowDataBound="gridWIPTODAY_RowDataBound" EnableViewState="true" AutoGenerateColumns="false" AllowSorting="true" 
        OnSorting="gridWIPTODAY_OnSort" OnSelectedIndexChanged="gridWIPTODAY_SelectedIndexChanged" >
        <Columns>
        <asp:BoundField HeaderText="S. No." DataField="SNO" />
        <asp:TemplateField>     
        <HeaderTemplate>
        <asp:DropDownList runat="server" ID="ddlJobType" OnSelectedIndexChanged="ddlJobType_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem, "JOB_NUMBER")%>
        </ItemTemplate>
        </asp:TemplateField>
       
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList runat="server" ID="ddlstage" OnSelectedIndexChanged="ddlstage_SelectedIndexChanged" AutoPostBack="true">
            <asp:ListItem Text="0" Value="0"></asp:ListItem>
        </asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem, "JOB_STAGE")%>
        </ItemTemplate>
        </asp:TemplateField>


        <asp:BoundField HeaderText="NAME" DataField="NAME" />
        <asp:BoundField HeaderText="SAM JOB" DataField="SAM_JOURNAL" />
        <asp:BoundField HeaderText="COPYEDIT" DataField="CE_JOURNAL" />
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList ID="ddlparentjobid" OnSelectedIndexChanged="ddlparentjobid_SelectedIndexChanged" runat="server" AutoPostBack="true">
        <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem, "ASSIGNED")%>
        </ItemTemplate>
        </asp:TemplateField>
       <%-- <asp:BoundField HeaderText="ASSIGNED" DataField="ASSIGNED" />--%>
        <asp:TemplateField>
        <HeaderTemplate>
        <asp:DropDownList ID="ddldept" OnSelectedIndexChanged="ddlparentjobid_SelectedIndexChanged" runat="server" AutoPostBack="true">
        <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList>
        </HeaderTemplate>
        <ItemTemplate>
        <%# DataBinder.Eval(Container.DataItem, "DEPARTMENT")%>
        </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:BoundField HeaderText="DEPARTMENT" DataField="DEPARTMENT" />--%>
        <asp:BoundField HeaderText="EMPLOYEE" DataField="EMPLOYEE" />
        <asp:BoundField HeaderText="REC. DATE" DataField="RECEIVED_DATE" />
        <asp:BoundField HeaderText="CATS DUE DATE" DataField="CATS_DUE_DATE" SortExpression="CATS_DUE_DATE" />
       <asp:TemplateField>
       <HeaderTemplate>
       <asp:DropDownList ID="ddlonhold" runat="server" OnSelectedIndexChanged="ddlonhold_SelectedIndexChanged" AutoPostBack="true">
       <asp:ListItem Text="0" Value="0"></asp:ListItem></asp:DropDownList> 
       </HeaderTemplate>
       <ItemTemplate>
         <%# DataBinder.Eval(Container.DataItem, "ONHOLD")%>
       </ItemTemplate>
       </asp:TemplateField>
        <%--<asp:BoundField HeaderText="HOLD DETAILS" DataField="ONHOLD" SortExpression="ONHOLD" />--%>
        </Columns>
       
        
</asp:GridView>    
    </div>
    </form>
</body>
</html>

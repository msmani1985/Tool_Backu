<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MonthYearPicker.ascx.cs"
    Inherits="Custom_Controls_MonthYearPicker" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:TextBox ID="txtValue" runat="server" Enabled="false" BackColor="White">
        </asp:TextBox>
        <asp:Button ID="btnSelect" runat="server" Text="..." OnClick="btnSelect_Click" />
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                loading...
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:Panel ID="pnlDate" runat="server" Visible="false" CssClass="DatePanel">
            <asp:DropDownList ID="ddlMonth" runat="server">
            </asp:DropDownList>
            <asp:DropDownList ID="ddlYear" runat="server">
            </asp:DropDownList>
            <asp:Button ID="btnSet" runat="server" Text="Set" OnClick="btnSet_Click" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

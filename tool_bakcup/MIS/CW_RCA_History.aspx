<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CW_RCA_History.aspx.cs" Inherits="CW_RCA_History" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
          <link href="default.css" type="text/css" rel="stylesheet" />
<link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="scripts/common.js"></script>
                <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle">CW Journal Details</div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label Text="Journal Name:" runat="Server"></asp:Label>
                        <asp:TextBox runat="server" Width="200" ID="txtSearch"></asp:TextBox>
                        <asp:Button runat="server" Text="Search"  CssClass="dpbutton" ID="btnSearch" OnClick="btnSearch_Click"/>
                    </td>
                </tr>
            </table>

        </div>
    <div>
    <table>
        <tr>
            <td>
            <asp:GridView ID="gv_CWDetails" runat="server" ShowFooter="True" 
            PageSize="5" AutoGenerateColumns="False" onrowcancelingedit="gv_CWDetails_RowCancelingEdit" 
            onrowcommand="gv_CWDetails_RowCommand"  onrowediting="gv_CWDetails_RowEditing" 
            onrowupdating="gv_CWDetails_RowUpdating" BorderWidth="3px" CellPadding="4" 
            CaptionAlign="Right" Width="269px"  BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black">
				<FooterStyle BackColor="#CCCCCC"></FooterStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
            <Columns>
            <asp:TemplateField HeaderText="Sl.No" >
            <ItemTemplate >
              <asp:Label ID="lblid" runat="server" Text='<%#Eval("Slno") %>'></asp:Label>
            </ItemTemplate>
            <FooterTemplate>
              <asp:Label ID="lblAdd" runat="server"></asp:Label>
            </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Journal Name">
            <ItemTemplate>
              <asp:Label ID="lblName" runat="server" Text='<%#Eval("JournalName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtName"  runat="server" Text='<%#Eval("JournalName") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddName"  Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Number of issues per year">
            <ItemTemplate>
              <asp:Label ID="lblNoIssue" runat="server" Text='<%#Eval("NoofIssueYear") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtNoIssue" runat="server" Text='<%#Eval("NoofIssueYear") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddNoIssue"  Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Full journal title">
            <ItemTemplate>
              <asp:Label ID="lblTitle" runat="server" Text='<%#Eval("JournalTitle") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtTitle" runat="server" Text='<%#Eval("JournalTitle") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddTitle" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Software to be Used">
            <ItemTemplate>
              <asp:Label ID="lblSoftware" runat="server" Text='<%#Eval("Software") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtSoftware" runat="server" Text='<%#Eval("Software") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddSoftware" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ISSNs-print">
            <ItemTemplate>
              <asp:Label ID="lblPrint" runat="server" Text='<%#Eval("ISSNPrint") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtPrint" runat="server" Text='<%#Eval("ISSNPrint") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddPrint" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="ISSNs-online">
            <ItemTemplate>
              <asp:Label ID="lblOnline" runat="server" Text='<%#Eval("ISSNOnline") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtOnline" runat="server" Text='<%#Eval("ISSNOnline") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddOnline" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue publication months">
            <ItemTemplate>
              <asp:Label ID="lblPubMonth" runat="server" Text='<%#Eval("PubMonth") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtPubMonth" runat="server" Text='<%#Eval("PubMonth") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddPubMonth" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="No. of pages per issue and volume">
            <ItemTemplate>
              <asp:Label ID="lblNoofPages" runat="server" Text='<%#Eval("NoofPages") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtNoofPages" runat="server" Text='<%#Eval("NoofPages") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddNoofPages" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Trim page size">
            <ItemTemplate>
              <asp:Label ID="lblTrimSize" runat="server" Text='<%#Eval("TrimSize") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtTrimSize" runat="server" Text='<%#Eval("TrimSize") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddTrimSize" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Workflow (i.e. Batch; Flow – first proofs on Advance Articles; Flow – revised proofs on Advance Articles; Issue in Progress)">
            <ItemTemplate>
              <asp:Label ID="lblWorkFlow" runat="server" Text='<%#Eval("WorkFlow") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtWorkFlow" runat="server" Text='<%#Eval("WorkFlow") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddWorkFlow" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Whether the journal is online only or printed also">
            <ItemTemplate>
              <asp:Label ID="lblOnlinePrinted" runat="server" Text='<%#Eval("Online_Printed") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtOnlinePrinted" runat="server" Text='<%#Eval("Online_Printed") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddOnlinePrinted" Width="80" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Colour requirements – e.g. online colour only; full printed colour, etc">
            <ItemTemplate>
              <asp:Label ID="lblColor" runat="server" Text='<%#Eval("Color") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtColor" runat="server" Text='<%#Eval("Color") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddColor" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Template, e.g. A, B, C, D, E, A1, non-standard – inc.:">
            <ItemTemplate>
              <asp:Label ID="lblTemplate" runat="server" Text='<%#Eval("Template") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtTemplate" runat="server" Text='<%#Eval("Template") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddTemplate" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If the journal has special front end material arrangements">
            <ItemTemplate>
              <asp:Label ID="lblSplMaterial" runat="server" Text='<%#Eval("SplMaterial") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtSplMaterial" runat="server" Text='<%#Eval("SplMaterial") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddSplMaterial" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Template leading">
            <ItemTemplate>
              <asp:Label ID="lblTempLead" runat="server" Text='<%#Eval("TempLead") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtTempLead" runat="server" Text='<%#Eval("TempLead") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddTempLead" Width="70" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Copyright Line">
            <ItemTemplate>
              <asp:Label ID="lblCopyright" runat="server" Text='<%#Eval("Copyright") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtCopyright" runat="server" Text='<%#Eval("Copyright") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddCopyright" Width="100" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If copyedited by Charlesworth: House and reference style details, inc. UK or US spelling">
            <ItemTemplate>
              <asp:Label ID="lblCopyedit" runat="server" Text='<%#Eval("Copyedit") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtCopyedit" runat="server" Text='<%#Eval("Copyedit") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddCopyedit" Width="80" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If printed, where the print deliverables are supplied to (i.e. printer details)">
            <ItemTemplate>
              <asp:Label ID="lblPrinted" runat="server" Text='<%#Eval("Printed") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtPrinted" runat="server" Text='<%#Eval("Printed") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddPrinted" Width="80" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="DTD:">
            <ItemTemplate>
              <asp:Label ID="lblDTD" runat="server" Text='<%#Eval("DTD") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtDTD" runat="server" Text='<%#Eval("DTD") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddDTD" Width="50" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Need to know what DTD the customer is using for each journal.">
            <ItemTemplate>
              <asp:Label ID="lblDTDUsed" runat="server" Text='<%#Eval("DTDUsed") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtDTDUsed" runat="server" Text='<%#Eval("DTDUsed") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddDTDUsed" Width="150" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="What are we providing them with at first proof stage?">
            <ItemTemplate>
              <asp:Label ID="lblProofStage" runat="server" Text='<%#Eval("ProofStage") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtProofStage" runat="server" Text='<%#Eval("ProofStage") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddProofStage" Width="90" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Atypon-Yes ?">
            <ItemTemplate>
              <asp:Label ID="lblAtypon" runat="server" Text='<%#Eval("Atypon") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:TextBox ID="txtAtypon" runat="server" Text='<%#Eval("Atypon") %>'></asp:TextBox>
            </EditItemTemplate>
            <FooterTemplate>
              <asp:TextBox ID="txtAddAtypon" Width="60" runat="server"></asp:TextBox>
            </FooterTemplate>
            </asp:TemplateField>
                

              <asp:TemplateField HeaderText="Edit">
            <ItemTemplate>
              <asp:LinkButton ID="btnEdit" Text="Edit" runat="server" CommandName="Edit" />
            <br />
            <%--<span onclick="return confirm('Are you sure you want to delete this record?')">
              <asp:LinkButton ID="btnDelete" Text="Delete" runat="server" CommandName="Delete"/>
             </span>--%>
            </ItemTemplate>
            <EditItemTemplate>
              <asp:LinkButton ID="btnUpdate" Text="Update" runat="server" CommandName="Update" />
            <br />
              <asp:LinkButton ID="btnCancel" Text="Cancel" runat="server" CommandName="Cancel" />
            </EditItemTemplate>
            <FooterTemplate>
              <asp:Button ID="btnAddRecord" runat="server" Text="Add" CssClass="dpbutton" CommandName="Add"></asp:Button>
            </FooterTemplate>
            </asp:TemplateField>
            </Columns>
                    <RowStyle BackColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            </td>
        </tr>
                <tr>
            <td>
            <asp:GridView ID="GridView1" runat="server" ShowFooter="True" 
            PageSize="5" AutoGenerateColumns="False" BorderWidth="3px" CellPadding="4" 
            CaptionAlign="Right" Width="269px"  BorderColor="#999999" BorderStyle="Solid" CellSpacing="2" ForeColor="Black">
				<FooterStyle BackColor="#CCCCCC"></FooterStyle>
				<HeaderStyle Font-Names="Tahoma" Font-Bold="True" Height="23px" ForeColor="White" CssClass="darkbackground1" ></HeaderStyle>
            <Columns>
            <asp:TemplateField HeaderText="Sl.No" >
            <ItemTemplate>
              <asp:Label ID="lblid" runat="server" Text='<%#Eval("Slno") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Journal Name">
            <ItemTemplate>
              <asp:Label ID="lblName" runat="server" Text='<%#Eval("JournalName") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Number of issues per year">
            <ItemTemplate>
              <asp:Label ID="lblNoIssue"  runat="server" Width="75" Text='<%#Eval("NoofIssueYear") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Full journal title">
            <ItemTemplate>
              <asp:Label ID="lblTitle" runat="server" Width="100" Text='<%#Eval("JournalTitle") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Software to be Used">
            <ItemTemplate>
              <asp:Label ID="lblSoftware" Width="70" runat="server" Text='<%#Eval("Software") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ISSNs-print">
            <ItemTemplate>
              <asp:Label ID="lblPrint" runat="server" Width="90" Text='<%#Eval("ISSNPrint") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>

                <asp:TemplateField HeaderText="ISSNs-online">
            <ItemTemplate>
              <asp:Label ID="lblOnline" runat="server" Width="90" Text='<%#Eval("ISSNOnline") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Issue publication months">
            <ItemTemplate>
              <asp:Label ID="lblPubMonth" runat="server" Width="90" Text='<%#Eval("PubMonth") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="No. of pages per issue and volume">
            <ItemTemplate>
              <asp:Label ID="lblNoofPages" runat="server" Width="80" Text='<%#Eval("NoofPages") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Trim page size">
            <ItemTemplate>
              <asp:Label ID="lblTrimSize" runat="server" Width="105" Text='<%#Eval("TrimSize") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Workflow (i.e. Batch; Flow – first proofs on Advance Articles; Flow – revised proofs on Advance Articles; Issue in Progress)">
            <ItemTemplate>
              <asp:Label ID="lblWorkFlow" runat="server" Width="90" Text='<%#Eval("WorkFlow") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Whether the journal is online only or printed also">
            <ItemTemplate>
              <asp:Label ID="lblOnlinePrinted" runat="server" Width="90" Text='<%#Eval("Online_Printed") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Colour requirements – e.g. online colour only; full printed colour, etc">
            <ItemTemplate>
              <asp:Label ID="lblColor" runat="server" Width="90" Text='<%#Eval("Color") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Template, e.g. A, B, C, D, E, A1, non-standard – inc.:">
            <ItemTemplate>
              <asp:Label ID="lblTemplate" runat="server" Width="90" Text='<%#Eval("Template") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If the journal has special front end material arrangements">
            <ItemTemplate>
              <asp:Label ID="lblSplMaterial" runat="server" Text='<%#Eval("SplMaterial") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Template leading">
            <ItemTemplate>
              <asp:Label ID="lblTempLead" runat="server" Text='<%#Eval("TempLead") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Copyright Line">
            <ItemTemplate>
              <asp:Label ID="lblCopyright" runat="server" Width="90"  Text='<%#Eval("Copyright") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If copyedited by Charlesworth: House and reference style details, inc. UK or US spelling">
            <ItemTemplate>
              <asp:Label ID="lblCopyedit" runat="server" Width="90" Text='<%#Eval("Copyedit") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="If printed, where the print deliverables are supplied to (i.e. printer details)">
            <ItemTemplate>
              <asp:Label ID="lblPrinted" runat="server" Width="100" Text='<%#Eval("Printed") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="DTD:">
            <ItemTemplate>
              <asp:Label ID="lblDTD" runat="server" Width="50" Text='<%#Eval("DTD") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Need to know what DTD the customer is using for each journal.">
            <ItemTemplate>
              <asp:Label ID="lblDTDUsed" runat="server" Text='<%#Eval("DTDUsed") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="What are we providing them with at first proof stage?">
            <ItemTemplate>
              <asp:Label ID="lblProofStage" Width="75" runat="server" Text='<%#Eval("ProofStage") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Atypon-Yes ?">
            <ItemTemplate>
              <asp:Label ID="lblAtypon" Width="60" runat="server" Text='<%#Eval("Atypon") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
                
            </Columns>
                    <RowStyle BackColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            </asp:GridView>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

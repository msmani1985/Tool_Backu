<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProdInfoJL.aspx.cs" Inherits="ProdInfoJL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="default.css" rel="stylesheet" type="text/css" />    
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <title></title>
        <style type="text/css">
            .auto-style5 {
                width: 351px;
            }
            .auto-style8 {
                width: 196px;
            }
            .auto-style10 {
                width: 235px;
            }
            .auto-style11 {
                width: 13px;
            }
            .auto-style12 {
                width: 423px;
            }
            .auto-style14 {
                width: 412px;
            }
            .auto-style15 {
                width: 7px;
            }
            </style>
    <script lang="javascript" type="text/javascript">
        function reset() {
            document.forms["Form1"].reset()
        }
    </script>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div  class="dpJobGreenHeader" style="align-content:flex-start">
<strong>Journal Infomation</strong>
        </div>
    <div>
        
        <br />
        <br />
    <div>
        <table  id="tblCust" border="0" width="100%" cellpadding="2" cellspacing="0">
            <tr class="dpJobGreenHeader">
               <td>
                                            Account:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlAccount" runat="server"  AutoPostBack="True" TabIndex="10" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                                                <asp:ListItem value="0">-- Select an option --</asp:ListItem>
                                            </asp:DropDownList></td>
                 <td>
                                            Category: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="ddlCategory" runat="server"   TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"  >
                                                <asp:ListItem value="0">-- Select an option --</asp:ListItem>
                                                <asp:ListItem value="1">Journals</asp:ListItem>
                                                <asp:ListItem value="2">Books</asp:ListItem>
                                                <asp:ListItem value="3">Projects</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td>

            </tr>

        </table>

    </div>
        <br />
         <table id="tblJourDet" border="0" class="content" width="100%" cellpadding="2" cellspacing="0" runat="server" style="border: 1px solid #008000; padding: 1px; border-image-width:1px">
                                
                            
                                        <tr width="100%" >
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Name</td>
                                        <td class="auto-style11">
                                            :<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="6" class="auto-style5" >
                                            <asp:DropDownList ID="ddlJournalName" runat="server"  DataTextField="journame" DataValueField="journo"  AutoPostBack="True" TabIndex="10" OnSelectedIndexChanged="ddlJournalName_SelectedIndexChanged">
                                                			<asp:ListItem value="0">-- Select a journal --</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtJournalName" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
                                            <asp:ImageButton ID="btn_NewJouranl" ImageUrl="~/images/newjournal.jpg" runat="server" OnClientClick="return reset();"
                                                ToolTip="New Article" OnClick="cmd_New_Article_Click" TabIndex="40" />
                                            </td>
                                        <td class="auto-style5" >
                                            &nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Code</td>
                                        <td class="auto-style11">
                                            :<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td colspan="6" class="auto-style5">
                                            <asp:DropDownList ID="ddlJourCode" runat="server" DataValueField="journo"  DataTextField="jourcode"  TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlJourCode_SelectedIndexChanged"  >
                                                <asp:ListItem value="0">-- Select a journal --</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtJourCode" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                      
                                      
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Style</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlJourStyle" runat="server"   TabIndex="11" AutoPostBack="True"  DataTextField="typestyle" DataValueField="typestyleno" >
                                                	<asp:ListItem value="0">-- Select a style --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Vol. Year</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtVolYear" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Prev Page Budget</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtPrevPageBudget" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Vol. No</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtVolNo" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td >
                                            &nbsp;</td>
                                                
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Cover Stock</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlCoverstock" runat="server" DataTextField="material"  DataValueField="covmatno"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">-- Select a material --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Curr Page Budget</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtCurrPageBudget" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                               
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Trim Size</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlTrimSize" runat="server" DataValueField="pagetrimno" DataTextField="trimsize"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a size --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Next Page Budget</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtNextPageBudget" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Paper Type</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlPaperType" runat="server" DataTextField="papertype" DataValueField="papertypeno"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">-- Select a type --</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Page starts</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlPageStarts" runat="server"  DataTextField="psname" DataValueField="psno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                    <td class="auto-style15">
                                    
                                        &nbsp;</td>
                                    <td class="auto-style12">
                                    
                                        Production Editor</td>
                                    <td class="auto-style11">
                                    
                                        :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlProdEditor" runat="server"  DataTextField="displayname" DataValueField="conno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0" Selected="True">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            <%--  <img ID="btn_NewJouranl0" src="images/info.gif"  
                                                onclick="javascript:calendar_window=window.open('DisplayInfo.aspx?formname=txtArticleSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');
                                                 TabIndex="40"  />--%>
                                          <asp:ImageButton ID="btnPEContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                 TabIndex="40" OnClick="btnPEContactDet_Click" />
                                        </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            T&amp;F PE Email</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtProdEds" runat="server" CssClass="TxtBox" MaxLength="300" ReadOnly="True" TabIndex="13" Width="176px"></asp:TextBox>
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Supervisor</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlSupervisor" runat="server"  DataTextField="displayname" DataValueField="conno"  AutoPostBack="True"  TabIndex="19">
                                                  	<asp:ListItem value="0">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            <asp:ImageButton ID="btnSupContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                 TabIndex="40" OnClick="btnSupContactDet_Click" />
                                            </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            T&amp;F Supervisor Email</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtSupervisor" runat="server" CssClass="TxtBox" MaxLength="300" ReadOnly="True" TabIndex="13" Width="178px"></asp:TextBox>
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Production Manager</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlProdMgr" runat="server"  DataTextField="displayname" DataValueField="conno"   TabIndex="11" AutoPostBack="True"  >
                                                  	<asp:ListItem value="0">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            <asp:ImageButton ID="btnPmgrContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                   TabIndex="40" OnClick="btnPmgrContactDet_Click" />
                                            </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Paper GSM</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlPageGSM" runat="server" AutoPostBack="True" DataTextField="gsmweight" DataValueField="papergsmno" TabIndex="11">
                                                <asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td class="auto-style8"  >
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            <span style="BACKGROUND-COLOR: yellow">SAM Price Code(T&amp;F)<br />
                                            CE price code (others)</span></td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtSAMPriceCode" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Trim Code</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlTrimCode" runat="server" AutoPostBack="True" DataTextField="trimcode" DataValueField="pagetrimno" TabIndex="11">
                                                <asp:ListItem value="0">-- Select a code --</asp:ListItem>
                                            </asp:DropDownList>
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Printer Code</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlPrinterCode" runat="server"   TabIndex="11" AutoPostBack="True" DataTextField="printname" DataValueField="printno" >
                                                	<asp:ListItem value="0">-- Select a printer --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Data Format <font color="red" size="2">*</font></td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlDataformat" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="3">Disk edited</asp:ListItem>
                                                <asp:ListItem value="21">Unedited</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">&nbsp;</td>
                                        <td class="auto-style12">ISSN (Print)</td>
                                        <td class="auto-style11">:</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtIssnPrint" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            TAT&nbsp; <font color="red" size="2">*</font></td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlTAT" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">&nbsp;</td>
                                        <td class="auto-style12">ISSN (Online)</td>
                                        <td class="auto-style11">:</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtIssnOnline" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Price Code</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtPriceCode" runat="server" CssClass="TxtBox" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
                                            </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Press Type</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlPressType" runat="server" DataTextField="presstypename" DataValueField="presstypeno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0" Selected="True">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Sensitive Journal</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlSensitiveJournal" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">N</asp:ListItem>
                                                <asp:ListItem value="1">Y</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                        
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Is SAM</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlSAM" runat="server"  TabIndex="11" AutoPostBack="True"  > 
                                                <asp:ListItem value="0">N</asp:ListItem>
                                                <asp:ListItem value="1">Y</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Urgent TAT <font color="red" size="2">*</font></td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlUrgentTat" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                        
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Is FPM</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlFPM" runat="server"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">N</asp:ListItem>
                                                <asp:ListItem value="1">Y</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Issue TAT <font color="red" size="2">*</font></td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlIssueTAT" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                        
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Is Copyedit</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:DropDownList ID="ddlCopyedit" runat="server" AutoPostBack="True" TabIndex="11">
                                                <asp:ListItem value="0">N</asp:ListItem>
                                                <asp:ListItem value="1">Y</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td class="auto-style5">
                                            &nbsp;</td>
                                        <td class="auto-style14">
                                            Created On</td>
                                        <td>
                                            :</td>
                                        <td class="auto-style5">
                                            <asp:TextBox ID="txtCreatedOn" runat="server" CssClass="TxtBox" MaxLength="300" ReadOnly="True" TabIndex="13"></asp:TextBox>
                                        </td>
                                        <td class="auto-style10">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                            
                                        
                                            
                                    </tr>
                                        <tr>
                                            <td class="auto-style15">&nbsp;</td>
                                            <td class="auto-style12">
                                                <span style="BACKGROUND-COLOR: yellow">PDF-QC price code</span></td>
                                            <td class="auto-style11">:</td>
                                            <td class="auto-style5">
                                                <asp:TextBox ID="txtPDFQCValue" runat="server" CssClass="TxtBox" MaxLength="300" TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td class="auto-style14"><span style="BACKGROUND-COLOR: yellow">Vendor Mgt price code</span></td>
                                            <td>:</td>
                                            <td class="auto-style5">
                                                <asp:TextBox ID="txtVendorMgtValue" runat="server" CssClass="TxtBox" MaxLength="300"  TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td class="auto-style10">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style15">&nbsp;</td>
                                            <td class="auto-style12"><span style="BACKGROUND-COLOR: yellow">Cust. Service price code</span></td>
                                            <td class="auto-style11">&nbsp;</td>
                                            <td class="auto-style5">
                                                <asp:TextBox ID="txtCustService" runat="server" CssClass="TxtBox" MaxLength="300" TabIndex="14"></asp:TextBox>
                                            </td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td class="auto-style14">&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td class="auto-style5">&nbsp;</td>
                                            <td class="auto-style10">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            Journal Folder path</td>
                                        <td class="auto-style11">
                                            :</td>
                                        <td  colspan="7">
                                            <asp:Label ID="lblFolderpath" runat="server" BackColor="Yellow"></asp:Label>
                                            </td>
                                            
                                    </tr>
                                        <tr>
                                            <td class="auto-style15">&nbsp;</td>
                                            <td class="auto-style12">Comments</td>
                                            <td class="auto-style11">:</td>
                                            <td colspan="7">
                                                <asp:TextBox ID="txtComments" runat="server" Height="51px" TextMode="MultiLine" Width="606px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            &nbsp;</td>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td  colspan="7">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit entries" OnClick="btnSubmit_Click"  CssClass="dpbutton" Width="82px"/>
                                        </td>
                                            
                                    </tr>
                                    <tr>
                                        <td class="auto-style15">
                                            &nbsp;</td>
                                        <td class="auto-style12">
                                            &nbsp;</td>
                                        <td class="auto-style11">
                                            &nbsp;</td>
                                        <td  colspan="7">
                                            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="#CC3300"></asp:Label>
                                        </td>
                                            
                                    </tr>
                                    </table>
    
    </div>
            
            </ContentTemplate>
  
    </asp:UpdatePanel>
    <%--   <asp:UpdateProgress id="updateProgress" runat="server">
    <ProgressTemplate>
        <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #ABBCAB; opacity: 0.7;">
            <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax_loader.gif" AlternateText="Loading ..." ToolTip="Loading ..." style="padding: 10px;position:fixed;top:45%;left:50%;" />
        </div>
    </ProgressTemplate>
</asp:UpdateProgress>--%>
    </form>
</body>
</html>

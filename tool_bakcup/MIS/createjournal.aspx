<%@ Page Language="C#" AutoEventWireup="true" CodeFile="createjournal.aspx.cs" Inherits="ProdInfoJL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="default.css" type="text/css" rel="stylesheet" />  
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
    <title></title>
       
    <script lang="javascript" type="text/javascript">
        function reset() {
            document.forms["Form1"].reset()
        }
    </script>
</head>
<body>
<form id="form1" runat="server" class="LightBackGound">
    <table cellpadding="0" cellspacing="0"  width="100%">
        <tr>
            <td colspan="7">
                <div id="hdrCustDetails" class="darkbackground" style="width:100%;height:25px">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" valign="middle" width="100%" style="padding-top:3px">
                                     <asp:Label ID="lblCustomerDetails1" Text="JOURNAL INFORMATION" style="text-align:center"
                                        runat="server" Font-Names="Segoe UI" Font-Bold="True" Font-Size="12px" ></asp:Label> 
                                </td>
                                
                            </tr>
                        </table>
                     </div>
            </td>
        </tr>
    </table>
    <table width="100%" class="LightBackGound">
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label1" Text="Account" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
            </td>
            <td>
            
                                        <asp:Label ID="Label58" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
            </td>
            <td colspan="3" width="120px">
            
                                            <asp:DropDownList ID="ddlAccount" runat="server" class="drpBoxLonger"  AutoPostBack="True" TabIndex="10" OnSelectedIndexChanged="ddlAccount_SelectedIndexChanged">
                                                <asp:ListItem value="0">-- Select an option --</asp:ListItem>
                                            </asp:DropDownList>
            
            </td>
            <td>
            
                                           </td>
            <td>
            
                                           </td>
            <td>
            
                                        <asp:Label ID="Label119" Text="Category" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label120" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
            </td>
            <td>
                                            <asp:DropDownList ID="ddlCategory" class="drpBoxLonger" runat="server"   TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"  >
                                                <asp:ListItem value="0">-- Select an option --</asp:ListItem>
                                                <asp:ListItem value="1">Journals</asp:ListItem>
                                                <asp:ListItem value="2">Books</asp:ListItem>
                                                <asp:ListItem value="3">Projects</asp:ListItem>
                                            </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label59" Text="Name" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label74" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlJournalName" class="drpBoxLargeMedium" runat="server"  DataTextField="journame" DataValueField="journo"  AutoPostBack="True" TabIndex="10" OnSelectedIndexChanged="ddlJournalName_SelectedIndexChanged">
                                                			<asp:ListItem value="0">-- Select a journal --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtJournalName" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13" Height="16px"></asp:TextBox>                                       
            
                </td>
            <td>
            
                                            <asp:ImageButton ID="btn_NewJouranl" ImageUrl="~/images/Files-New-File-icon.png"  runat="server" OnClientClick="return reset();"
                                                ToolTip="New Article" OnClick="cmd_New_Article_Click" TabIndex="40" Height="20px"  />
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label89" Text="Vol. Year" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label104" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtVolYear" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True"></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label60" Text="Code" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label75" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlJourCode" class="drpBoxLargeMedium" runat="server" DataValueField="journo"  DataTextField="jourcode"  TabIndex="11" AutoPostBack="True" OnSelectedIndexChanged="ddlJourCode_SelectedIndexChanged"  >
                                                <asp:ListItem value="0">-- Select a journal --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtJourCode"  class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label90" Text="Vol. No" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label105" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtVolNo" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True"></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label61" Text="Style" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label76" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlJourStyle" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  DataTextField="typestyle" DataValueField="typestyleno" >
                                                	<asp:ListItem value="0">-- Select a style --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label91" Text="Curr Page Budget" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label106" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtCurrPageBudget" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label62" Text="Prev Page Budget" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label77" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtPrevPageBudget" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label92" Text="Next Page Budget" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label107" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtNextPageBudget"  class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label63" Text="Cover Stock" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label78" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlCoverstock" class="drpBoxLargeMedium" runat="server" DataTextField="material"  DataValueField="covmatno"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">-- Select a material --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label93" Text="Page starts" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label108" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlPageStarts" class="drpBoxLargeMedium" runat="server"  DataTextField="psname" DataValueField="psno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label64" Text="Trim Size" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label79" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlTrimSize" class="drpBoxLargeMedium" runat="server" DataValueField="pagetrimno" DataTextField="trimsize"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a size --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label94" Text="Trim Code" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label109" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlTrimCode" class="drpBoxLargeMedium" runat="server" DataTextField="trimcode" DataValueField="pagetrimno"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">-- Select a code --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label65" Text="Paper Type" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label80" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlPaperType" class="drpBoxLargeMedium" runat="server" DataTextField="papertype" DataValueField="papertypeno"  TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">-- Select a type --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label95" Text="Paper GSM" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label110" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlPageGSM" class="drpBoxLargeMedium" runat="server"  DataTextField="gsmweight" DataValueField="papergsmno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label66" Text="Production Editor" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label81" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlProdEditor" class="drpBoxLargeMedium" runat="server"  DataTextField="displayname" DataValueField="conno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0" Selected="True">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
            <td width="120px">
            
                                          <asp:ImageButton ID="btnPEContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                 TabIndex="40" OnClick="btnPEContactDet_Click" />
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label96" Text="T&amp;F prod eds.txt" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label111" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtProdEds" class="txtBoxLonger" runat="server" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True" ></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label67" Text="Supervisor" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label82" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlSupervisor" class="drpBoxLargeMedium" runat="server"  DataTextField="displayname" DataValueField="conno"  AutoPostBack="True"  TabIndex="19">
                                                  	<asp:ListItem value="0">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
            <td width="120px">
            
                                            <asp:ImageButton ID="btnSupContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                 TabIndex="40" OnClick="btnSupContactDet_Click" />
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label97" Text="T&amp;F Supervisor.txt" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label112" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtSupervisor" class="txtBoxLonger" runat="server" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True" ></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label68" Text="Production Manager" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label83" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlProdMgr" class="drpBoxLargeMedium" runat="server"  
                    DataTextField="displayname" DataValueField="conno"   TabIndex="11" 
                    AutoPostBack="True" >
                                                  	<asp:ListItem value="0">-- Select a contact --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
            <td width="120px">
            
                                            <asp:ImageButton ID="btnPmgrContactDet"  ImageUrl="~/images/info.gif" runat="server"
                                                   TabIndex="40" OnClick="btnPmgrContactDet_Click" />
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label98" Text="Data Format" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label113" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlDataformat" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="3">Disk edited</asp:ListItem>
                                                <asp:ListItem value="21">Unedited</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label69" 
                    Text="SAM Price Code(T&amp;F) CE price code (others)" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label84" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtSAMPriceCode" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label99" Text="TAT" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label114" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlTAT" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label70" Text="Printer Code" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label85" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlPrinterCode" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True" DataTextField="printname" DataValueField="printno" >
                                                	<asp:ListItem value="0">-- Select a printer --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label100" Text="Price Code" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label115" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:TextBox ID="txtPriceCode" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>                                       
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label71" Text="ISSN (Print)" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label86" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtIssnPrint" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label101" Text="Sensitive Journal" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label116" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlSensitiveJournal" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                <asp:ListItem value="0">N</asp:ListItem>
                                                <asp:ListItem value="1">Y</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label72" Text="ISSN (Online)" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label87" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtIssnOnline" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13"></asp:TextBox>
            
                </td>
            <td width="120px">
            
                </td>
            <td>
            
                </td>
            <td>
            
                </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label102" Text="Urgent TAT" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label117" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlUrgentTat" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label73" Text="Press Type" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label88" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:DropDownList ID="ddlPressType" class="drpBoxLargeMedium" runat="server" DataTextField="presstypename" DataValueField="presstypeno"  TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0" Selected="True">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
            <td width="120px">
            
               </td>
            <td>
            
               </td>
            <td>
            
               </td>
            <td>
            
               </td>
            <td>
            
                                        <asp:Label ID="Label103" Text="Issue TAT" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label118" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                            <asp:DropDownList ID="ddlIssueTAT" class="drpBoxLargeMedium" runat="server"   TabIndex="11" AutoPostBack="True"  >
                                                	<asp:ListItem value="0">-- Select a value --</asp:ListItem>
                                            </asp:DropDownList>
            
                </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label123" Text="Created On" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label124" Text=":" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td width="120px">
            
                                            <asp:TextBox ID="txtCreatedOn" class="txtBoxLargeMedium" runat="server" 
                                                MaxLength="300" TabIndex="13" ReadOnly="True" ></asp:TextBox>
            
                </td>
            <td width="120px">
            
               </td>
            <td>
            
               </td>
            <td>
            
               </td>
            <td>
            
               </td>
            <td>
            
                                       </td>
            <td>
            
                                       </td>
            <td>
            
                                           </td>
        </tr>
        <tr>
            <td width="225px">
            
                                        <asp:Label ID="Label121" Text="Comments" runat="server" 
                    Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td>
            
                                        <asp:Label ID="Label122" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
            
                </td>
            <td colspan="8" width="120px">
            
                                            <asp:TextBox ID="txtComments" class="txtBoxLargeMedium" runat="server" 
                                                Height="51px" TextMode="MultiLine" Width="677px"></asp:TextBox>                                       
            
                </td>
           
        </tr>
        <tr>
            <td align="center" colspan="10">
            
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="dpbutton" Text="Submit entries" OnClick="btnSubmit_Click" />
                                        </td>
           
        </tr>
    </table>

    </div>
        <br />
    
    </div>
    </form>
</body>
</html>

<%@ page language="C#" autoeventwireup="true" inherits="customer, App_Web_w6b3pav3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <link href="scripts/messages.css" rel="stylesheet" type="text/css" />
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/messages.js"></script>

    <script type="text/javascript">
      function validateForm() {
          var CustName;
          var CustCode;
          var Officename; 
          var Financename;
          var Currency;
          if(document.form1.txtCustName!=null)CustName = document.form1.txtCustName.value;
          if(document.form1.txtCustCode!=null)CustCode = document.form1.txtCustCode.value;
          if(document.form1.txtOfficeName!=null)Officename = document.form1.txtOfficeName.value;          
          if(document.form1.txtFinanceName!=null)Financename = document.form1.txtFinanceName.value;
          if(document.form1.drpCurrency!=null)Currency = document.form1.drpCurrency.value;
          if(CustName == "") {
            inlineMsg('txtCustName','You must enter Customer Name.',2);
            return false;
          }    
          if(CustCode == "") {
            inlineMsg('txtCustCode','You must enter Cutomer Code.',2);
            return false;
          }
          if(Currency == "0"){
            inlineMsg('drpCurrency','You must select a currency.',2);
            return false;
          }
          if(Officename == "") {
            inlineMsg('txtOfficeName','You must enter Office Name.',2);
            return false;
          }
          if(Financename == "") {
            inlineMsg('txtFinanceName','You must enter Finance Name.',2);
            return false;
          }
          
          /*
          var email = form.email.value;
          var gender = form.gender.value;
          var message = form.message.value;
          var nameRegex = /^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*$/;
          var emailRegex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
          var messageRegex = new RegExp(/<\/?\w+((\s+\w+(\s*=\s*(?:".*?"|'.*?'|[^'">\s]+))?)+\s*|\s*)\/?>/gim);
          
          if(name == "") {
            inlineMsg('name','You must enter your name.',2);
            return false;
          }
          if(!name.match(nameRegex)) {
            inlineMsg('name','You have entered an invalid name.',2);
            return false;
          }
          if(email == "") {
            inlineMsg('email','<strong>Error</strong><br />You must enter your email.',2);
            return false;
          }
          if(!email.match(emailRegex)) {
            inlineMsg('email','<strong>Error</strong><br />You have entered an invalid email.',2);
            return false;
          }
          if(gender == "") {
            inlineMsg('gender','<strong>Error</strong><br />You must select your gender.',2);
            return false;
          }
          if(message == "") {
            inlineMsg('message','You must enter a message.');
            return false;
          }
          if(message.match(messageRegex)) {
            inlineMsg('message','You have entered an invalid message.');
            return false;
          }
          */
          return true;
        }
    function confirmDelete(){
    if (!confirm('Confirm Delete?')) return false;
    }
    
    function addOffice_onclick() {
        if(document.getElementById('drpOffice')!=null)document.getElementById('drpOffice').value="0";
        if(document.getElementById('txtOfficeName')!=null)document.getElementById('txtOfficeName').value="";
        if(document.getElementById('txtOffAdd1')!=null)document.getElementById('txtOffAdd1').value="";
        if(document.getElementById('txtOffAdd2')!=null)document.getElementById('txtOffAdd2').value="";
        if(document.getElementById('txtOffAdd3')!=null)document.getElementById('txtOffAdd3').value="";
        if(document.getElementById('txtOffAdd4')!=null)document.getElementById('txtOffAdd4').value="";
        if(document.getElementById('txtOffAdd5')!=null)document.getElementById('txtOffAdd5').value="";
        if(document.getElementById('txtOffPCode')!=null)document.getElementById('txtOffPCode').value="";
        if(document.getElementById('txtOffCity')!=null)document.getElementById('txtOffCity').value="";
        if(document.getElementById('txtOffState')!=null)document.getElementById('txtOffState').value="";
        if(document.getElementById('drpCountryo')!=null)document.getElementById('drpCountryo').value="0";
        if(document.getElementById('txtOffPhone1')!=null)document.getElementById('txtOffPhone1').value="";
        if(document.getElementById('txtOffPhone2')!=null)document.getElementById('txtOffPhone2').value="";
        if(document.getElementById('txtOffPhone3')!=null)document.getElementById('txtOffPhone3').value="";
        if(document.getElementById('txtOffFax1')!=null)document.getElementById('txtOffFax1').value="";
        if(document.getElementById('txtOffFax2')!=null)document.getElementById('txtOffFax2').value="";
        if(document.getElementById('txtOffEmail')!=null)document.getElementById('txtOffEmail').value="";
        if(document.getElementById('txtOffURL')!=null)document.getElementById('txtOffURL').value="";
        
        if(document.getElementById('txtOfficeName')!=null)document.getElementById('txtOfficeName').focus();
    }
    function addFinance_onclick() {
        if(document.getElementById('drpFinance')!=null)document.getElementById('drpFinance').value="0";
        if(document.getElementById('txtFinanceName')!=null)document.getElementById('txtFinanceName').value="";
        if(document.getElementById('txtFinAdd1')!=null)document.getElementById('txtFinAdd1').value="";
        if(document.getElementById('txtFinAdd2')!=null)document.getElementById('txtFinAdd2').value="";
        if(document.getElementById('txtFinAdd3')!=null)document.getElementById('txtFinAdd3').value="";
        if(document.getElementById('txtFinAdd4')!=null)document.getElementById('txtFinAdd4').value="";
        if(document.getElementById('txtFinAdd5')!=null)document.getElementById('txtFinAdd5').value="";
        if(document.getElementById('txtFinPCode')!=null)document.getElementById('txtFinPCode').value="";
        if(document.getElementById('txtFinCity')!=null)document.getElementById('txtFinCity').value="";
        if(document.getElementById('txtFinState')!=null)document.getElementById('txtFinState').value="";
        if(document.getElementById('drpCountryf')!=null)document.getElementById('drpCountryf').value="0";
        if(document.getElementById('txtFinPhone1')!=null)document.getElementById('txtFinPhone1').value="";
        if(document.getElementById('txtFinPhone2')!=null)document.getElementById('txtFinPhone2').value="";
        if(document.getElementById('txtFinPhone3')!=null)document.getElementById('txtFinPhone3').value="";
        if(document.getElementById('txtFinFax1')!=null)document.getElementById('txtFinFax1').value="";
        if(document.getElementById('txtFinFax2')!=null)document.getElementById('txtFinFax2').value="";
        if(document.getElementById('txtFinEmail')!=null)document.getElementById('txtFinEmail').value="";
        if(document.getElementById('txtFinVatNo')!=null)document.getElementById('txtFinVatNo').value="";
        if(document.getElementById('txtFinCommodityNo')!=null)document.getElementById('txtFinCommodityNo').value="";
        if(document.getElementById('txtFinLogin')!=null)document.getElementById('txtFinLogin').value="";
        if(document.getElementById('txtFinPassword')!=null)document.getElementById('txtFinPassword').value="";
        
        if(document.getElementById('txtFinanceName')!=null)document.getElementById('txtFinanceName').focus();
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div class="dptitle">
            Customer Management</div>
        <div>
            <table align="left" width="100%">
                <tr>
                    <td align="left">
                        <table id="tblCustDetails" width="100%">
                            <tr>
                                <td class="dpLtGreenHeader">
                                    Customer</td>
                                <td class="dpLtGreenHeader" colspan="4">
                                    <asp:DropDownList ID="drpCustomer" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpCustomer_SelectedIndexChanged"
                                        Width="291px">
                                    </asp:DropDownList>
                                    &nbsp;&nbsp;<span style="font-size: 7pt; color: #ff0000">* Required</span></td>
                                <td class="dpJobGreenHeader" style="text-align:right">
                                    <asp:ImageButton ID="imgbtnNewCustomer" runat="server" ImageUrl="~/images/tools/j_new.png"
                                        OnClick="imgbtnNewCustomer_Click" ToolTip="New Customer" CssClass="CursorAdd" />
                                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="~/images/tools/j_save.png"
                                    OnClientClick="return validateForm();" OnClick="btnSubmit_Click" ToolTip="Save Customer" CssClass="CursorAdd" />
                                    <asp:ImageButton ID="imgbtnDeleteCustomer" OnClientClick="return confirmDelete()"
                                        runat="server" ImageUrl="~/images/tools/minus.png" OnClick="imgbtnDeleteCustomer_Click" Visible="false" /></td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Customer Name</strong><span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtCustName" runat="server" Width="286px" CssClass="TxtBox" MaxLength="200"
                                        BackColor="#FFFFC0"></asp:TextBox>
                                </td>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <strong>Created On</strong></td>
                                <td>
                                    <asp:Label ID="lblCreatedon" runat="server" ForeColor="Blue" Width="220px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Customer Code</strong><span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td>
                                    <asp:TextBox ID="txtCustCode" runat="server" CssClass="TxtBox" Width="143px" MaxLength="20"
                                        BackColor="#FFFFC0"></asp:TextBox></td>
                                <td>
                                </td>
                                <td>
                                    <strong>PDF Enabled&nbsp;&nbsp;&nbsp;&nbsp;</strong></td>
                                <td>
                                    <asp:DropDownList ID="drpPdfEnabled" runat="server">
                                        <asp:ListItem Value="X">N/A</asp:ListItem>
                                        <asp:ListItem Value="N">N</asp:ListItem>
                                        <asp:ListItem Value="Y">Y</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Customer Group</strong></td>
                                <td>
                                    <asp:DropDownList ID="drpCustGroup" runat="server" Width="291px">
                                    </asp:DropDownList></td>
                                <td>
                                </td>
                                <td>
                                    <strong>Currency</strong><span style="font-size: 9pt; color: #ff0000">*</span></td>
                                <td>
                                    <asp:DropDownList ID="drpCurrency" runat="server">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Email</strong></td>
                                <td>
                                    <asp:TextBox ID="txtCustEmail" runat="server" CssClass="TxtBox" Width="286px" MaxLength="250"></asp:TextBox></td>
                                <td>
                                <td><strong>Do you want Bank Details in Invoice ? </strong></td>
                                 <td>
                                    <asp:DropDownList ID="drpbankdetails"  runat="server">
                                        <asp:ListItem Value="0">N</asp:ListItem>
                                        <asp:ListItem Value="1">Y</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" align="center">
                                    <span id="Messagebox" runat="server" style="font-size: 8pt; background-color: Yellow;
                                        color: Black;"></span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <ol id="toc">
                            <li id="miOffice" runat="server">
                                <asp:LinkButton ID="btnOfficeTab" runat="server" OnClick="btnOfficeTab_Click">Office Site</asp:LinkButton></li>
                            <li id="miFinance" runat="server">
                                <asp:LinkButton ID="btnFinancetab" runat="server" OnClick="btnFinancetab_Click">Finance Site</asp:LinkButton></li>
                            <li id="miBudjet" runat="server">
                                <asp:LinkButton ID="btnBudjettab" OnClientClick="javascript:alert('Under Construction.');return false;" runat="server" OnClick="btnBudjettab_Click">Budjet</asp:LinkButton></li>
                        </ol>
                        <div>
                            <div class="content" id="tabOffice" runat="server">
                                <table cellpadding="2" cellspacing="2" id="tblOffice" width="100%">
                                    <tr>
                                        <td class="dpLtGreenHeader">
                                            Office Name &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </td>
                                        <td class="dpLtGreenHeader" colspan="4">
                                            <asp:DropDownList ID="drpOffice" runat="server" Width="291px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpOffice_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;&nbsp;<img id="addOffice" src="images/tools/add.png" language="javascript"
                                                onclick="return addOffice_onclick()" class="CursorAdd" title="New Office Site" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Office Name</strong><span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtOfficeName" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"
                                                BackColor="#FFFFC0"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <strong>Phone 1</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffPhone1" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Address</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffAdd1" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Phone 2</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffPhone2" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOffAdd2" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Phone 3</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffPhone3" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOffAdd3" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Fax 1</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffFax1" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOffAdd4" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Fax 2</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffFax2" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtOffAdd5" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Email</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffEmail" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>City</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffCity" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Website&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffURL" runat="server" CssClass="TxtBox" Width="220px" MaxLength="300"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>State</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffState" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>PO Code</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtOffPCode" runat="server" Width="286px" CssClass="TxtBox" MaxLength="50"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Country</strong></td>
                                        <td>
                                            <asp:DropDownList ID="drpCountryo" runat="server" Width="291px">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="content" id="tabFinance" runat="server">
                                <table cellpadding="2" cellspacing="2" id="tblFinance" width="100%">
                                    <tr>
                                        <td class="dpLtGreenHeader">
                                            Financial Name</td>
                                        <td class="dpLtGreenHeader" colspan="4">
                                            <asp:DropDownList ID="drpFinance" runat="server" Width="291px" AutoPostBack="True"
                                                OnSelectedIndexChanged="drpFinance_SelectedIndexChanged">
                                            </asp:DropDownList>&nbsp;&nbsp;<img id="addFinance" src="images/tools/add.png" language="javascript"
                                                onclick="return addFinance_onclick()" class="CursorAdd" title="New Finance Site" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Financial Name</strong><span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtFinanceName" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"
                                                BackColor="#FFFFC0"></asp:TextBox></td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            <strong>Phone 1</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinPhone1" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Address</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinAdd1" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Phone 2</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinPhone2" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFinAdd2" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Phone 3</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinPhone3" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFinAdd3" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Fax 1</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinFax1" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFinAdd4" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Fax 2</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinFax2" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFinAdd5" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Email</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinEmail" runat="server" CssClass="TxtBox" Width="220px" MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>City</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinCity" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Commodity No.</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinCommodityNo" runat="server" CssClass="TxtBox" Width="220px"
                                                MaxLength="50"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>State</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinState" runat="server" Width="286px" CssClass="TxtBox" MaxLength="100"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>VAT No.</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinVatNo" runat="server" CssClass="TxtBox" Width="220px" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>PO Code</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinPCode" runat="server" Width="286px" CssClass="TxtBox" MaxLength="50"></asp:TextBox></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Login</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinLogin" runat="server" CssClass="TxtBox" Width="220px" MaxLength="20"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Country</strong></td>
                                        <td>
                                            <asp:DropDownList ID="drpCountryf" runat="server" Width="291px">
                                            </asp:DropDownList></td>
                                        <td>
                                        </td>
                                        <td>
                                            <strong>Password</strong></td>
                                        <td>
                                            <asp:TextBox ID="txtFinPassword" runat="server" CssClass="TxtBox" Width="220px" MaxLength="15"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </div>
                            <div class="content" id="tabBudget" runat="server">
                                <table cellpadding="2" cellspacing="2" id="tblBudjet" width="100%">
                                    <tr valign="top">
                                        <td class="TabArea" style="width: 600px">
                                            <table id="Table3" style="text-align: left">
                                                <tr>
                                                    <td class="dpLtGreenHeader">
                                                        Budget Details</td>
                                                    <td class="dpLtGreenHeader" colspan="4">
                                                        <span style="font-size: 9pt; color: #ff0000">&nbsp;</span></td>
                                                </tr>
                                                <tr style="font-size: 8pt">
                                                    <td>
                                                        <strong>Budjet Name<span style="color: #000000">*</span></strong></td>
                                                    <td style="font-weight: bold; color: #000000">
                                                        <asp:TextBox ID="TextBox39" runat="server" CssClass="TxtBox" Width="286px"></asp:TextBox></td>
                                                    <td style="font-weight: bold; color: #000000">
                                                        &nbsp;</td>
                                                    <td style="font-weight: bold">
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
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
                                                <tr style="font-size: 8pt">
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" ForeColor="Blue" Width="220px"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">                        
                        <asp:HiddenField ID="hfValidated" runat="server" />
                    </td>
                </tr>
                
            </table>
        </div>
        
    </form>
</body>

<script language="javascript">
function messagebox(msg)
{
    if(msg!="")alert(msg);
}

</script>

</html>

<%@ page language="C#" autoeventwireup="true" inherits="Customer, App_Web_25d24vps" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="default.css" type="text/css" rel="stylesheet" />
     <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.5.0/jquery.min.js" type="text/javascript"></script>
     <script type="text/javascript">
         function onload() {
//             alert('gfhfgh');
           //document.getElementById('childCusDetails').style.display = 'none';
         }
         function testclick() {
//             alert('gfhfgh');
//             if (document.getElementById('childCusDetails').style.display == 'none') {
//                 alert("sdfsd");
//                 document.getElementById('childCusDetails').style.display = 'block';
//             }
//             else {
//                 document.getElementById('childCusDetails').style.display = 'none';
//             }

         }
         $(document).ready(function () {
             $("#Label3").click(function () {
                 $("#childCusDetails").slideToggle();
                 if ($("#Label3").text() == " - ") {
                     $("#Label3").html(" + ")
                 }
                 else {
                     $("#Label3").text(" - ")
                 }
             });

             $("#Label25").click(function () {
                 $("#childOfficeDeails").slideToggle();
                 if ($("#Label25").text() == " - ") {
                     $("#Label25").html(" + ")
                 }
                 else {
                     $("#Label25").text(" - ")
                 }
             });

             $("#Label32").click(function () {
                 $("#childFinancialName").slideToggle();
                 if ($("#Label32").text() == " - ") {
                     $("#Label32").html(" + ")
                 }
                 else {
                     $("#Label32").text(" - ")
                 }
             });

         });
     </script>
  
    <style type="text/css">
        .style1
        {
            width: 100px;
            height: 21px;
        }
        .style2
        {
            height: 21px;
        }
        .style3
        {
            width: 125px;
            height: 21px;
        }
    </style>
  
</head>
<body onload="onload()" class="LightBackGound" style="background-repeat:no-repeat;" >
    <form id="form1" runat="server">
    <%--<div class="dptitle" id="divTitle" align="left" runat="server">Customer Information</div>--%>
    <div>
        
        <table width="100%" cellpadding="0" cellspacing="0" height="100%">
            <tr>
                <td>
                     <div id="hdrCustDetails" class="darkbackground" style="width:100%;height:25px">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" valign="middle" width="50%">
                                     <asp:Label ID="lblCustomerDetails1" Text="CUSTOMER DETAILS" style="text-align:left"
                                        runat="server" Font-Names="Segoe UI" Font-Bold="True" Font-Size="12px" ></asp:Label> 
                                </td>
                                <td align="left" valign="middle" width="25%">
                                      
                                </td>
                                <td align="right" valign="middle" style="padding-right:10px">
                                     <asp:Label ID="Label3" Text=" - " style="text-align:center;cursor:pointer;" onclientclick="testclick()"
                                      runat="server" Font-Names="Segoe UI" Font-Bold="true" Font-Size="18px" ></asp:Label> 
                                </td>
                            </tr>
                        </table>
                     </div>
                        <div id="childCusDetails" class="LightBackGound" style="width:100%;">
                            <table align="left" class="LightBackGound" style="width:100%;">
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label1" Text="Customer Name" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label58" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="drpCustomer" class="drpBoxLonger" runat="server" 
                                                        AutoPostBack="True" onselectedindexchanged="drpCustomer_SelectedIndexChanged">
                                                    </asp:DropDownList>    
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnCustomerClear" runat="server" Height="20px" 
                                                        ImageUrl="~/images/Files-New-File-icon.png" 
                                                        onclick="btnCustomerClear_Click" />        
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label8" Text="Customer" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                        <asp:Label ID="Label115" Text=" *" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ForeColor="Red" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label71" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCustomer" class="txtBoxLonger" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label9" Text="Email" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label59" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtEmail" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label22" Text="Folder Name" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label70" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFolderName" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="30"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 125px;" >
                                         </td>
                                    <td align="left" >
                                        </td>
                                    <td align="left">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label10" Text="Created On" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label113" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCreatedOn" class="txtBoxLargeMedium" ReadOnly="true" 
                                            runat="server" MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label11" Text="Currency" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label114" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpCurrency" class="drpBoxLargeMedium" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        </td>
                                     <td align="left" style="width: 125px;" >
                                        <asp:Label ID="Label23" Text="PDF Enabled" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label72" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpPdfEnabled" class="drpBoxSmall" runat="server">
                                            <asp:ListItem Value="0">-Select-</asp:ListItem>
                                            <asp:ListItem Value="X">N/A</asp:ListItem>
                                            <asp:ListItem Value="Y">Y</asp:ListItem>
                                            <asp:ListItem Value="N">N</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label2" Text="Book Budget" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label60" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBookPrev" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label12" Text="Book Budget" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label69" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBookCurrent" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 125px;" >
                                        <asp:Label ID="Label21" Text="Book Budget" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label73" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBookNext" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;"  >
                                        <asp:Label ID="Label4" Text="Project Budget" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label61" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtProjectPrev" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                    </td>
                                     <td align="left" style="width: 100px;"  >
                                        <asp:Label ID="Label13" Text="Project Budjet" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label68" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtProjectCurrent" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                    </td>
                                     <td align="left" style="width: 125px;"  >
                                        <asp:Label ID="Label20" Text="Project Budjet" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label74" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtProjectNext" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label5" Text="Book Pages" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label62" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPagesPrev" class="txtBoxLargeMedium" runat="server" 
                                            ></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label14" Text="Book Pages" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label67" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPagesCurrent" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 125px;" >
                                        <asp:Label ID="Label19" Text="Book Pages" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label75" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPagesNext" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label6" Text="No. Books" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label63" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofBooksPrev" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label16" Text="No. Books" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label66" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofBooksCurrent" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 125px;" >
                                        <asp:Label ID="Label18" Text="No. Books" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label76" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofBooksNext" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label7" Text="No. Projects" runat="server" Font-Names="Segoe UI" Font-Bold="false" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label64" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofProjectPrev" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label17" Text="No. Projects" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label65" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofProjectCurrent" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 125px;" >
                                        <asp:Label ID="Label15" Text="No. Projects" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label77" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNoofProjectNext" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>                    
                </td>
            </tr>
            <tr>
                <td>
                     <div id="hdrOfficeDeails" class="darkbackground" style="width:100%;height:25px">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" valign="middle" width="50%">
                                     <asp:Label ID="Label24" Text="OFFICE NAME" style="text-align:left"
                                        runat="server" Font-Names="Segoe UI" Font-Bold="True" Font-Size="12px" ></asp:Label> 
                                </td>
                                <td align="left" valign="middle" width="25%">
                                    &nbsp;</td>
                                <td align="right" valign="middle" style="padding-right:10px">
                                     <asp:Label ID="Label25" Text=" - " style="text-align:center;cursor:pointer;" onclientclick="testclick()"
                                      runat="server" Font-Names="Segoe UI" Font-Bold="true" Font-Size="18px" ></asp:Label> 
                                </td>
                            </tr>
                        </table>
                     </div>
                        <div id="childOfficeDeails" class="LightBackGound" style="width:100%;">
                            <table align="left" class="LightBackGound" style="width:100%;">
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label26" Text="Office Name" runat="server" 
                                            Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label80" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <table cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="drpOffice" class="drpBoxLonger" runat="server" 
                                                        onselectedindexchanged="drpOffice_SelectedIndexChanged" 
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>        
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnOfficialClear" runat="server" Height="20px" 
                                                    ImageUrl="~/images/Files-New-File-icon.png" onclick="btnOfficialClear_Click" />        
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label27" Text="Office Name" runat="server" 
                                            Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        <asp:Label ID="Label117" Text=" *" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ForeColor="Red" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label87" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" colspan="1">
                                        <asp:TextBox ID="txtOfficeName" class="txtBoxLonger" runat="server" 
                                            MaxLength="100"></asp:TextBox>
                                    </td>

                                    <td align="left">
                                        </td>

                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label30" Text="Address" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label81" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAddress" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="255"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label28" Text="Location" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label86" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLocation" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                    <td align="left" style="width: 125px;">
                                        <asp:Label ID="Label34" Text="Country" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label88" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpCountry" class="drpBoxLargeMedium" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label33" Text="PO Code" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label82" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPinCode" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label31" Text="City" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label85" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCity" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                    <td align="left" style="width: 125px;">
                                        <asp:Label ID="Label49" Text="State" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label89" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtState" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label36" Text="Phone" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label83" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtPhone" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label48" Text="Fax" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label84" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFax" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                    <td align="left" style="width: 125px;">
                                        <asp:Label ID="Label40" Text="Website" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:Label ID="Label90" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtWebSite" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                        </td>
                                </tr>
                                </table>
                        </div>               
                </td>
            </tr>
            <tr>
                <td>
                     <div id="hdrFinancialName" class="darkbackground" style="width:100%;height:25px">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="left" valign="middle" width="50%">
                                     <asp:Label ID="Label29" Text="FINANCIAL NAME" style="text-align:left"
                                        runat="server" Font-Names="Segoe UI" Font-Bold="True" Font-Size="12px" ></asp:Label> 
                                </td>
                                <td width="25%">
                                    &nbsp;</td>
                                <td align="right" valign="middle" style="padding-right:10px">
                                     <asp:Label ID="Label32" Text=" - " style="text-align:center;cursor:pointer;" onclientclick="testclick()"
                                      runat="server" Font-Names="Segoe UI" Font-Bold="true" Font-Size="18px" ></asp:Label> 
                                </td>
                            </tr>
                        </table>
                     </div>
                        <div id="childFinancialName" class="LightBackGound" style="width:100%;">
                            <table align="left" class="LightBackGound" style="width:100%;">
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label35" Text="Financial Name" runat="server" 
                                            Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label103" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="drpFinancial" class="drpBoxLonger" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="drpFinancial_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnFinancialClear" runat="server" Height="20px" 
                                                ImageUrl="~/images/Files-New-File-icon.png" 
                                                    onclick="btnFinancialClear_Click" />    
                                            </td>
                                        </tr>
                                    </table>
                                        
                                        
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label37" Text="Financial Name" runat="server" 
                                            Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        <asp:Label ID="Label116" Text=" *" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ForeColor="Red" ></asp:Label> 
                                         </td>
                                    <td align="left" >
                                        <asp:Label ID="Label93" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFinancialName" class="txtBoxLonger" runat="server" 
                                            MaxLength="100"></asp:TextBox>
                                        </td>

                                    <td align="left" >
                                        </td>

                                </tr>
                                <tr>
                                    <td align="left" class="style1" >
                                        <asp:Label ID="Label57" Text="Address" runat="server" 
                                            Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" class="style2" >
                                        <asp:Label ID="Label104" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" class="style2">
                                        <asp:TextBox ID="txtFinAddress" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="100"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style2">
                                    </td>
                                     <td align="left" class="style1" >
                                        <asp:Label ID="Label38" Text="Phone" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" class="style2" >
                                        <asp:Label ID="Label94" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" class="style2">
                                        <asp:TextBox ID="txtFinPhone" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="30"></asp:TextBox>
                                    </td>
                                    <td align="left" class="style2" >
                                        </td>
                                    <td align="left" class="style3">
                                          <asp:Label ID="Label53" Text="Login" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label>  
                                        </td>
                                    <td align="left" class="style2">
                                        <asp:Label ID="Label110" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" class="style2" >
                                        <asp:TextBox ID="txtLogin" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="10"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;"  >
                                        </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label105" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtFinAddress1" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                    </td>
                                     <td align="left" style="width: 100px;"  >
                                        <asp:Label ID="Label55" Text="Fax" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" 
                                             ToolTip="Email" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label95" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtFinfax" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="30"></asp:TextBox>
                                    </td>
                                    <td align="left"  >
                                        </td>
                                    <td align="left" style="width: 125px;" >
                                          <asp:Label ID="Label54" Text="Password" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:Label ID="Label111" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left"  >
                                        <asp:TextBox ID="txtPassword" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="10"></asp:TextBox>
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;" >
                                        </td>
                                    <td align="left" >
                                        <asp:Label ID="Label106" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFinAddress2" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left">
                                    </td>
                                     <td align="left" style="width: 100px;" >
                                        <asp:Label ID="Label56" Text="Email" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:Label ID="Label96" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtFinEmail" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="200"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                        </td>
                                    <td align="left" style="width: 125px;">
                                        <asp:Label ID="Label47" Text="Team/Group Assigned" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left">
                                        <asp:Label ID="Label112" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:DropDownList ID="drpGroup" class="drpBoxLargeMedium" runat="server">
                                        </asp:DropDownList>
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;"  >
                                        </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label107" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtFinAddress3" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                    </td>
                                     <td align="left" style="width: 100px;"  >
                                        <asp:Label ID="Label43" Text="Commodity No" runat="server" 
                                             Font-Names="Segoe UI" Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label97" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtCommNo" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left"  >
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;"  >
                                        </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label108" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtFinAddress4" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                    </td>
                                     <td align="left" style="width: 100px;"  >
                                        <asp:Label ID="Label52" Text="Vat No" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label98" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtVatNo" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="20"></asp:TextBox>
                                    </td>
                                    <td align="left"  >
                                        </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100px;"  >
                                        </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label109" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtFinAddress5" class="txtBoxLargeMedium" runat="server" 
                                            MaxLength="50"></asp:TextBox>
                                    </td>
                                    <td align="left" >
                                        </td>
                                     <td align="left" style="width: 100px;"  >
                                          <asp:Label ID="Label39" Text="Ledger Id" runat="server" Font-Names="Segoe UI" 
                                             Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                    </td>
                                    <td align="left"  >
                                        <asp:Label ID="Label99" Text=":" runat="server" Font-Names="Segoe UI" 
                                            Font-Bold="False" Font-Size="11px" ></asp:Label> 
                                        </td>
                                    <td align="left" >
                                        <asp:TextBox ID="txtLedgerId" class="txtBoxLargeMedium" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="left"  >
                                        </td>
                                </tr>
                                </table>
                        </div>               
                </td>
            </tr>
        </table>
        <table align="center" style="width:100%" cellpadding="0" cellspacing="0">
            <tr>
                <td align="center">
                    <asp:Label ID="lblError" ForeColor="Red" Font-Bold="True" runat="server" 
                        Font-Names="Segoe UI" Font-Size="11px"></asp:Label>    
                </td>
            </tr>
            <tr>
                <td align="center">
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" CssClass="dpbutton" runat="server" Text="Save"  
                                            ToolTip="Save" Width="70px" onclick="btnSave_Click"/>
                                </td>
                                <td>
                                    <asp:Button ID="btnClear" CssClass="dpbutton" runat="server" Text="Clear"  
                                            ToolTip="Clear" Width="70px" onclick="btnClear_Click"/>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnMode" Value="0" runat="server" />
        <asp:HiddenField ID="hdnOfficeMode" Value="0" runat="server" />
        <asp:HiddenField ID="hdnFinancialMode" Value="0" runat="server" />


        
         
    </div>
    </form>
</body>
</html>

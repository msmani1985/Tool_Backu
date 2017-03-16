<%@ page language="C#" autoeventwireup="true" inherits="EditJournalDetails, App_Web_olx2vwmy" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Journal Page</title>
    <link href="default.css" rel=Stylesheet type="text/css" />
    <script type="text/javascript" src="Javascript/Script.min.js"></script>
    <script language="javascript" type="text/javascript">
    $(document).ready(function()
    {
        $("#img_invup").click(function()
        {
            $("#img_invdown").show();
            $("#img_invup").hide();
            $("#div_invoicedetails").slideToggle("slow");
        }
        );
        $("#img_invdown").click(function()
        {
            $("#img_invdown").hide();
            $("#img_invup").show();
            $("#div_invoicedetails").slideToggle("slow");
        }
        );
        
        $("#img_rmvjournal1").click(function()
        {
            $("#img_addjournal1").show();
            $("#img_rmvjournal1").hide();
            $("#div_journal1").slideToggle("slow");
        }
        );
        $("#img_addjournal1").click(function()
        {
            $("#img_addjournal1").hide();
            $("#img_rmvjournal1").show();
            $("#div_journal1").slideToggle("slow");
        }
        );
         $("#img_rmvjournal2").click(function()
        {
            $("#img_addjournal2").show();
            $("#img_rmvjournal2").hide();
            $("#div_journal2").slideToggle("slow");
        }
        );
        $("#img_addjournal2").click(function()
        {
            $("#img_addjournal2").hide();
            $("#img_rmvjournal2").show();
            $("#div_journal2").slideToggle("slow");
        }
        );
        //For onchange
        $("#iscopyedit").change(function()
        {
            if($("#iscopyedit :selected").text()=="N")
                $("tr#CE_row2").hide();
            else
                $("tr#CE_row2").show();
        }
        );
        //For FormLoad
        if($("#iscopyedit :selected").text()=="N")
          $("tr#CE_row2").hide();
        else
          $("tr#CE_row2").show();
        
        $("#issam").change(function()
        {
            if($("#issam :selected").text()=="N")
                $("tr#SAM_row2").hide();
            else
                $("tr#SAM_row2").show();
         }
        );
       
        if($("#issam :selected").text()=="N")
            $("tr#SAM_row2").hide();
        else
            $("tr#SAM_row2").show();
        

        $("#service_type_id").change(function()
        {
            if($("#service_type_id :selected").text()=="Article Based")
            {
                $("#SAM_cost_type_id").val('4');
                $("#CE_cost_type_id").val('4');
                $("#cost_type_id").val('4');
            }
            else if($("#service_type_id :selected").text()=="Issue Based")
            {
                $("#SAM_cost_type_id").val('3');
                $("#CE_cost_type_id").val('3');
                $("#cost_type_id").val('3');
            }
            else
            {
                $("#SAM_cost_type_id").val('0');
                $("#CE_cost_type_id").val('0');
                $("#cost_type_id").val('0');
            }
            $("#CE_service_type_id").val($("#service_type_id :selected").val());
            $("#SAM_service_type_id").val($("#service_type_id :selected").val());
        }
        );
        
        
        if($("#service_type_id :selected").text()=="Article Based" && $("#cost_type_id :selected").val()=="0")
        {
            $("#SAM_cost_type_id").val('4');
            $("#CE_cost_type_id").val('4');
            $("#cost_type_id").val('4');
        }
        else if($("#service_type_id :selected").text()=="Issue Based" && $("#cost_type_id :selected").val()=="0")
        {
            $("#SAM_cost_type_id").val('3');
            $("#CE_cost_type_id").val('3');
            $("#cost_type_id").val('3');
        }
        $("#Btn_journalupdate").click(function()
        {
            if($("#journal_id :selected").val()=="0")
            {
                alert("Invalid Journal name");
                $("#journal_id").focus();
                return false;
            }
            if($("#journal_code :selected").val()=="0")
            {
                alert("Invalid Journal code");
                $("#journal_code").focus();
                return false;
            }
        }
        );
        
        function validation(obj1,obj2)
        {
            if($(obj1).val()=="1" && (isNaN($(obj2).val()) || $(obj2).val()=="" || $(obj2).val()=="0" ))
            {
                alert("Invalid Numeric format in pricecode field (It's Mandatory)");
                $(obj2).css('background-color','yellow');
                $(obj2).focus();
                return false;
            }
            return true;
        }
        function OrderIndex_Validation(obj)
        {
            if(isNaN($(obj).val()) || $(obj).val()=="")
            {
                alert("Invalid Numeric format in Order Index field (Default value is 0)");
                $(obj).css('background-color','yellow');
                $(obj).focus();
                return false;
            }
            return true;
        }
        $("#btnupdate").click(function()
        {
           $(":text").css('background-color','white');
           $("#service_type_id").css('background-color','white');
           $("#cost_type_id").css('background-color','white');
           $("#CE_cost_type_id").css('background-color','white');
           $("#SAM_cost_type_id").css('background-color','white');
             
            
            if($("#service_type_id :selected").val()=="0")
            {
                alert("Invalid service Type value in TypeSet invoice details");
                //$("tr#TS_row2").css('color','red');
                $("service_type_id").css('background-color','yellow');
                $("#service_type_id").focus();
                return false;
            }
            if($("#cost_type_id :selected").val()=="0")
            {
                alert("Invalid value in Typeset cost type field (It's Mandatory)");
                $("#cost_type_id").css('background-color','yellow');
                $("#cost_type_id").focus();
                return false;
                
            }
            if(isNaN($("#pricecode").val()) || $("#pricecode").val()=="" || $("#pricecode").val()=="0" )
            {
                alert("Invalid Numeric format in Typeset Pricecode field (It's Mandatory)");
                //$("tr#TS_row2").css('color','red');
                $("#pricecode").css('background-color','yellow');
                $("#pricecode").focus();
                return false;
            }
            
            if(!OrderIndex_Validation("#order_index"))
                return false;
                
            if($("#iscopyedit :selected").text()=="Y" && ($("#CE_cost_type_id :selected").val()=="0"))
            {
                alert("Invalid CostType and pricecode value in Copyediting invoice details");
                //$("tr#CE_row2").css('color', 'red');
                $("#CE_cost_type_id").css('background-color','yellow');
                $("#CE_cost_type_id").focus();
                return false;
            }
            
            if(!validation("#iscopyedit","#CE_pricecode"))
               return false;
            if(!OrderIndex_Validation("#CE_order_index"))
                return false;

               
            if($("#issam :selected").text()=="Y" && ($("#SAM_cost_type_id :selected").val()=="0") )
            {
                alert("Invalid CostType value in SAM invoice details");
                //$("tr#SAM_row2").css('color','red');
                $("#SAM_cost_type_id").css('background-color','yellow');
                $("#SAM_cost_type_id").focus();
                return false;
            }
            if(!validation("#issam","#SAM_pricecode"))
               return false; 


            if(!OrderIndex_Validation("#SAM_order_index"))
                return false;

        }
        );
      $("#CE_service_type_id").change(function()
      {
        $("#service_type_id").val($("#CE_service_type_id :selected").val());
        $("#SAM_service_type_id").val($("#CE_service_type_id :selected").val());
      }
      );
      
      $("#SAM_service_type_id").change(function()
      {
        $("#service_type_id").val($("#SAM_service_type_id :selected").val());
        $("#CE_service_type_id").val($("#SAM_service_type_id :selected").val());
      }
      );
      
      
       
    }
    );
    </script>
    <style>
        .combowidth
        {
        width:150px;
        }
        .headerbackcolor
        {
            background-color:#EAFEE2; /*WhiteSmoke;*/
            border-bottom:solid 2px Gray;
            color:Green;
            font-size:10pt;font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divtitle" class="dptitle">Journal Details</div>
    <div id="divjournal" runat="server">
       <table width="100%" class="bordertable" >
            <tr>
                <td class="headerbackcolor">Journal Information <img id="img_addjournal1" src="images/Symbol_Add.png" style="display:none;" /><img id="img_rmvjournal1" src="images/ih-minus-16x16.gif" />
                </td>
            </tr>
            <tr>
                <td>
                <div id="div_journal1">
                    <table width="100%">
                       <tr><td>Customer Name</td>
                           <td colspan="5"><asp:DropDownList ID="customer_id" runat="server" DataTextField="cust_name" DataValueField="customer_id" OnSelectedIndexChanged="customer_id_SelectedIndexChanged" AutoPostBack="True" Width="510px"></asp:DropDownList></td> 
                       </tr> 
                       <tr><td>Journal Name</td>
                           <td colspan="5"><asp:DropDownList ID="journal_id" runat="server" DataTextField="journal_name" DataValueField="journal_id" AutoPostBack="True" OnSelectedIndexChanged="journal_id_SelectedIndexChanged" Width="510px"></asp:DropDownList></td>
                       </tr>
                       <tr><td>Journal Title</td><td colspan="5"><asp:TextBox ID="journal_name" Text="" runat="Server" Width="510px"></asp:TextBox></td></tr>
                       <tr>
                           <td>Code</td>
                           <td><asp:DropDownList ID="jour_code" CssClass="combowidth" runat="server" DataTextField="journal_code" DataValueField="journal_id" AutoPostBack="True" OnSelectedIndexChanged="jour_code_SelectedIndexChanged"></asp:DropDownList></td>
                           <td>Subject Category</td>
                           <td colspan="3"><asp:DropDownList CssClass="combowidth" ID="subcategory_id" runat="server" DataValueField="subcategory_id" DataTextField="subcategory_name"></asp:DropDownList></td>  
                       </tr>
                       <tr>
                           <td>Sales Job Group</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="sales_job_group_id" DataTextField="sales_group_name" DataValueField="sales_job_group_id" runat="server"></asp:DropDownList></td>
                           <%--<td>Service Type</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="service_type_id" runat="server" DataTextField="service_type_name" DataValueField="service_type_id"></asp:DropDownList></td>--%>
                           <td>Cover Material</td>
                           <td colspan="3"><asp:DropDownList CssClass="combowidth" ID="covermaterial_id" runat="server" DataTextField="covermaterial_name" DataValueField="covermaterial_id"></asp:DropDownList></td>  
                           
                       </tr>
                        <tr>
                        <td >Style</td>
                           <td ><asp:DropDownList CssClass="combowidth" ID="typestyle_id" runat="server" DataTextField="typestyle_name" DataValueField="typestyle_id"></asp:DropDownList></td> 
                        <td>Production Editor</td>
                        <td colspan="3"><asp:DropDownList Width="325px" ID="prodeditor_id" runat="server" DataTextField="display_name" DataValueField="contact_id"></asp:DropDownList></td>
                    </tr>
                    <tr>
                       <td>Trim Size</td>
                       <td><asp:DropDownList CssClass="combowidth" ID="pagetrim_id" runat="server" DataTextField="sizecode" DataValueField="pagetrim_id"></asp:DropDownList></td> 
                       <td>Production Manager</td>
                       <td colspan="3"><asp:DropDownList Width="325px" ID="prodmgr_id" runat="server" DataTextField="display_name" DataValueField="contact_id"></asp:DropDownList></td> 
                    </tr>
                    </table>
                    </div>
                </td>
            </tr>         
            <tr>
                <td class="headerbackcolor">Additional Information <img id="img_addjournal2" src="images/Symbol_Add.png" style="display:none;" /><img id="img_rmvjournal2" src="images/ih-minus-16x16.gif" />
                </td>
            </tr>
           <tr>
                <td>
                    <div id="div_journal2">
                        <table width="100%">
                            <tr >
                           <td >Article Urgent TAT</td>
                               <td><asp:DropDownList CssClass="combowidth" ID="article_urgent_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                                   <asp:ListItem>6</asp:ListItem>
                                   <asp:ListItem>7</asp:ListItem>
                                   <asp:ListItem>8</asp:ListItem>
                                   <asp:ListItem>9</asp:ListItem>
                                   <asp:ListItem>10</asp:ListItem>
                                   <asp:ListItem>11</asp:ListItem>
                                   <asp:ListItem>12</asp:ListItem>
                                   <asp:ListItem>13</asp:ListItem>
                                   <asp:ListItem>14</asp:ListItem>
                                   <asp:ListItem>15</asp:ListItem>
                               </asp:DropDownList></td>
                               <td >Issue Urgent TAT</td>
                               <td ><asp:DropDownList CssClass="combowidth" ID="issue_urgent_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                               </asp:DropDownList></td>
                               <td>Issue TAT</td>
                               <td><asp:DropDownList CssClass="combowidth" ID="issue_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                                   <asp:ListItem>6</asp:ListItem>
                                   <asp:ListItem>7</asp:ListItem>
                                   <asp:ListItem>8</asp:ListItem>
                                   <asp:ListItem>9</asp:ListItem>
                                   <asp:ListItem>10</asp:ListItem>
                                   <asp:ListItem>11</asp:ListItem>
                                   <asp:ListItem>12</asp:ListItem>
                                   <asp:ListItem>13</asp:ListItem>
                                   <asp:ListItem>14</asp:ListItem>
                                   <asp:ListItem>15</asp:ListItem>
                               </asp:DropDownList></td> 
                               
                           </tr>
                           <tr>
                               <td>Issue Revise TAT</td>
                               <td ><asp:DropDownList CssClass="combowidth" ID="issue_rev_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                               </asp:DropDownList></td>
                               <td>Article TAT</td>
                               <td><asp:DropDownList CssClass="combowidth" ID="article_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                                   <asp:ListItem>6</asp:ListItem>
                                   <asp:ListItem>7</asp:ListItem>
                                   <asp:ListItem>8</asp:ListItem>
                                   <asp:ListItem>9</asp:ListItem>
                                   <asp:ListItem>10</asp:ListItem>
                                   <asp:ListItem>11</asp:ListItem>
                                   <asp:ListItem>12</asp:ListItem>
                                   <asp:ListItem>13</asp:ListItem>
                                   <asp:ListItem>14</asp:ListItem>
                                   <asp:ListItem>15</asp:ListItem>
                               </asp:DropDownList></td>
                               <td >Article Revise TAT</td>
                               <td ><asp:DropDownList CssClass="combowidth" ID="article_rev_tat" runat="server">
                                   <asp:ListItem Value="0">-- Select a value --</asp:ListItem>
                                   <asp:ListItem>1</asp:ListItem>
                                   <asp:ListItem>2</asp:ListItem>
                                   <asp:ListItem>3</asp:ListItem>
                                   <asp:ListItem>4</asp:ListItem>
                                   <asp:ListItem>5</asp:ListItem>
                               </asp:DropDownList></td> 
                           </tr>
                           <tr>
                               <td>Paper GSM</td> 
                               <td><asp:DropDownList CssClass="combowidth" ID="papergsm_id" runat="server" DataTextField="gsmweight" DataValueField="papergsm_id"></asp:DropDownList></td>
                               <td>Paper Type</td>
                               <td><asp:DropDownList CssClass="combowidth" ID="papertype_id" runat="server" DataTextField="papertype_name" DataValueField="papertype_id"></asp:DropDownList></td>
                               <td>Page Starts</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="pagestyle_id" runat="server" DataTextField="pagestyle_name" DataValueField="pagestyle_id"></asp:DropDownList></td>
                               
                           </tr>
                           <tr><td>Frequency</td><td><asp:TextBox ID="frequency" runat="server"></asp:TextBox></td>
                                <td>Publication Dates</td><td><asp:TextBox ID="Publication_Dates" runat="server"></asp:TextBox></td>
                                <td>Target No.of Pages</td><td><asp:TextBox ID="Target_Numberof_Pages" runat="server"></asp:TextBox></td>
                           </tr>
                       
                        
                            <tr>
                               <td>Annual Pages</td><td><asp:TextBox ID="Annual_Pages" runat="server"></asp:TextBox></td>
                               <td>ISSN(print)</td>
                               <td><asp:TextBox ID="issn_print" runat="server" Text=""></asp:TextBox></td> 
                               <td>ISSN(online)</td>
                               <td><asp:TextBox ID="issn_online" runat="server" Text=""></asp:TextBox></td> 
                           </tr>
                           <tr>
                                <td>Indexing Sites</td><td ><asp:TextBox ID="Indexing_Sites" runat="server"></asp:TextBox></td>
                                <td>Web Path</td><td ><asp:TextBox ID="web_path" runat="server" ></asp:TextBox></td>
                                <%--<td>Page Starts</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="pagestyle_id" runat="server" DataTextField="pagestyle_name" DataValueField="pagestyle_id"></asp:DropDownList></td>--%>
                                <td >DOI</td>
                               <td ><asp:TextBox ID="doi" runat="server" Text=""></asp:TextBox></td>
                           </tr>
                        </table>
                    </div>
                </td>
           </tr>
        </table>
        <table class="bordertable" width="100%">
            <tr>
                <td>IscopyEdit</td>
                <td><asp:DropDownList ID="iscopyedit" runat="server"><asp:ListItem Text="Y" Value="1"></asp:ListItem>
                   <asp:ListItem Text="N" Value="0"></asp:ListItem>
               </asp:DropDownList></td>
               <td>Issam</td>
               <td><asp:DropDownList ID="issam" runat="server"><asp:ListItem Text="Y" Value="1"></asp:ListItem>
                   <asp:ListItem Text="N" Value="0"></asp:ListItem> 
               </asp:DropDownList></td>
               <td>Cover Pre-printed</td>
                <td><asp:DropDownList ID="cover_preprint" runat="server"><asp:ListItem Text="Y" Value="1"></asp:ListItem>
                    <asp:ListItem Text="N" Value="0"></asp:ListItem></asp:DropDownList>
               </td>
               <td>Litho</td>
               <td><asp:DropDownList ID="litho" runat="server">
                    <asp:ListItem Text="Y" Value="1"></asp:ListItem><asp:ListItem Text="N" Value="0"></asp:ListItem>
                   </asp:DropDownList></td> <td>Digital</td>
               <td><asp:DropDownList ID="digital" runat="server">
                    <asp:ListItem Text="Y" Value="1"></asp:ListItem><asp:ListItem Text="N" Value="0"></asp:ListItem>
               </asp:DropDownList></td><td>Sensitive journal?</td>
               <td><asp:DropDownList ID="issensitive" runat="server"><asp:ListItem Text="Y" Value="1"></asp:ListItem>
                   <asp:ListItem Text="N" Value="0"></asp:ListItem>                     
                   </asp:DropDownList>
               </td>
                  
            </tr>
            <tr><td align="center" colspan="12"><asp:Button CssClass="dpbutton" ID="Btn_journalupdate" Text="Update" runat="server" OnClick="Btn_journalupdate_Click" /></td></tr> 
        </table>
        <table width="100%" class="bordertable" >
            <tr>
                <td class="headerbackcolor">Invoice Details <img id="img_invdown" src="images/Symbol_Add.png" style="display:none;" /><img id="img_invup" src="images/ih-minus-16x16.gif" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div_invoicedetails">
                    <table width="100%" >
                    <tr id="TS_row2"><td><table width="100%">
                        <tr><td colspan="6"><b>TypeSetting</b></td></tr>
                    <tr ><td>Service Type</td>
                    <td><asp:DropDownList CssClass="combowidth" ID="service_type_id" Width="100px" runat="server" ><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Article Based" Value="5"></asp:ListItem><asp:ListItem Text="Issue Based" Value="6"></asp:ListItem></asp:DropDownList></td>
                    <td>Invoice Type Item</td><td><asp:DropDownList Width="125px" ID="invoicetype_item_id" runat="server"><asp:ListItem Text="Typesetting" Value="1"></asp:ListItem></asp:DropDownList></td>
                    <td>Cost Type</td><td><asp:DropDownList ID="cost_type_id" Width="110px" runat="server"><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Page Cost" Value="3"></asp:ListItem><asp:ListItem Text="Item Cost" Value="4"></asp:ListItem></asp:DropDownList></td>
                    <td>Price Code</td><td ><asp:TextBox ID="pricecode" runat="server" Width="100px"></asp:TextBox></td></tr>
                    <tr><td>Order Index</td><td><asp:TextBox ID="order_index" runat="server" Width="100px">0</asp:TextBox></td><td>Description</td><td colspan="5"><asp:TextBox ID="description" runat="server" Width="460px"></asp:TextBox></td></tr>
                    </table></td></tr><asp:HiddenField ID="typeset_rowid" runat="server" />
                    <tr id="CE_row2"><td><table width="100%"><tr><td colspan="6"><b>CopyEditing</b></td></tr>
                    <tr >
                    <td>Service Type</td>
                           <td><asp:DropDownList CssClass="combowidth" ID="CE_service_type_id" Enabled="false" Width="100px" runat="server"><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Article Based" Value="5"></asp:ListItem><asp:ListItem Text="Issue Based" Value="6"></asp:ListItem></asp:DropDownList></td>
                    <td>Invoice Type Item</td><td><asp:DropDownList Width="125px" ID="CE_invoicetype_item_id" runat="server"><asp:ListItem Text="Copyediting Charges" Value="6"></asp:ListItem></asp:DropDownList></td>
                    <td>Cost Type</td><td><asp:DropDownList ID="CE_cost_type_id" Width="110px" runat="server"><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Page Cost" Value="3"></asp:ListItem><asp:ListItem Text="Item Cost" Value="4"></asp:ListItem></asp:DropDownList></td>
                    <td>Price Code</td><td ><asp:TextBox ID="CE_pricecode" runat="server" Width="100px"></asp:TextBox></td></tr>
                    <tr><td>Order Index</td><td><asp:TextBox ID="CE_order_index" runat="server" Width="100px">0</asp:TextBox></td><td>Description</td><td colspan="5"><asp:TextBox ID="CE_Description" runat="server" Width="460px"></asp:TextBox></td></tr></table>
                    </td></tr><asp:HiddenField ID="copyedit_rowid" runat="server" />
                    
                    <tr id="SAM_row2"><td><table width="100%">
                        <tr id="SAM_row1"><td colspan="6"><b>SAM Charges</b></td></tr>
                    <tr ><td>Service Type</td>
                    <td><asp:DropDownList CssClass="combowidth" ID="SAM_service_type_id" Enabled="false" Width="100px" runat="server" ><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Article Based" Value="5"></asp:ListItem><asp:ListItem Text="Issue Based" Value="6"></asp:ListItem></asp:DropDownList></td><td>Invoice Type Item</td><td><asp:DropDownList Width="125px" ID="SAM_invoicetype_item_id" runat="server"><asp:ListItem Text="SAM Charges" Value="15"></asp:ListItem></asp:DropDownList></td>
                    <td>Cost Type</td><td><asp:DropDownList ID="SAM_cost_type_id" Width="110px" runat="server"><asp:ListItem Text="-- Select a value --" Value="0"></asp:ListItem><asp:ListItem Text="Page Cost" Value="3"></asp:ListItem><asp:ListItem Text="Item Cost" Value="4"></asp:ListItem></asp:DropDownList></td>
                    <td>Price Code</td><td ><asp:TextBox ID="SAM_pricecode" runat="server" Width="100px"></asp:TextBox></td></tr>
                    <tr><td>Order Index</td><td><asp:TextBox ID="SAM_order_index" runat="server" Width="100px" >0</asp:TextBox></td><td>Description</td><td colspan="5"><asp:TextBox ID="SAM_Description" runat="server" Width="460px"></asp:TextBox></td></tr>
                    </table></td></tr><asp:HiddenField ID="sam_rowid" runat="server" />
                    
                    
                    <tr><td align="center"><asp:Button ID="btnupdate" Text="Update" CssClass="dpbutton" runat="server" OnClick="btnupdate_Click" /></td></tr>
                    </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

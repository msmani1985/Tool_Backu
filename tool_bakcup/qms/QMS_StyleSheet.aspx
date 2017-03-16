<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QMS_StyleSheet.aspx.cs" Inherits="QMS_StyleSheet" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
     <link href="default.css" rel="stylesheet" type="text/css" />   
        <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />
            <script src="scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
<script src="scripts/ScrollableGridPlugin.js" type="text/javascript"></script>
<script type = "text/javascript">
$(document).ready(function () {
    $('#<%=grdStyleSheet.ClientID %>').Scrollable({
        ScrollHeight: 550
    });
});
</script>
     <script language="javascript">    
    var cnt=0;
    var tt;
    var timer_is_on=0;
    var elemn;
    var h = 0;
    var max =20;
    function timedCount(){
        //alert(document.getElementById('divfooter'));
        elemn = document.getElementById('divfooter');
	    h = elemn.style.height.replace('px','');	
	    if(timer_is_on && cnt<=max && h<=max){
	    //ctrl.value=cnt;	
	    if(h=='')h=0;
	    //alert(h);
	    elemn.style.height = parseInt(h)+5+'px';	    
	    //alert(cnt);
	    cnt=cnt+5;
	    tt=setTimeout("timedCount()",0);
	    }else {
	    timer_is_on=0;
	    cnt=0;
	    }
    }
    function doTimer()
    {    
    if (!timer_is_on)
      {
      timer_is_on=1;
      timedCount();
      }else timer_is_on=0;  
    }
    
    
    
    var gvelem;
        var gvcolor;
        function setColor(element, val) {
            //alert(gvelem);
            if (gvelem != null) {//alert(gvelem.style.backgroundColor);
                gvelem.style.backgroundColor = gvcolor;
            }
            gvelem = element;
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            document.form1.hfA_Journal.value = val;
            if (document.getElementById("lblJournalSummary"))
                document.getElementById("lblJournalSummary").innerText = "Journal : " + val ;
            else if (document.all.lblJournalSummary)
                document.all.lblJournalSummary.innerText = "Journal : " + val ;
            else if (document.form1.lblArticleSummary)
                document.form1.lblJournalSummary.innerText = "Journal : " + val ;
            doTimer();
        }
    
       function setMouseOverColor(element) {
            gvcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#C2C2C2';
            element.style.cursor = 'hand';
            element.style.textDecoration = 'underline';
        }
        function setMouseOutColor(element) {
            element.style.backgroundColor = gvcolor;
            element.style.textDecoration = 'none';
        }
    </script>

        
        <style>
           
           .GVFixedHeader
        {
            font-weight: bold;
            background-color: Green;
            position: relative;
            top: expression(this.parentNode.parentNode.parentNode.scrollTop-1);
        }
    </style>
      <style type="text/css">
        .footer{	    
	    background-color: green;
	    height: 0.1px;
	    text-align: center;
	    font-size:11px;
	    font-weight:bold;
	    color:#FFFFFF;
	    font-family:Verdana;	
	    padding-top: 0px;	    
	    width: 120px;
	    position:fixed;	    
	    right: 3px;
	    bottom: 0px;
	    cursor:pointer;
	    line-height:20px;
        }
    </style>
     
</head>
<body>
    <form id="form1" runat="server">
        <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        
       <div>
            <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
       <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
       <tr>
         <td>
          <ol id="toc">
            <li id="miGeneral" runat="server">&nbsp;</li><li id="miJournal" runat="server">&nbsp;</li></ol>
                                    <asp:HiddenField ID="hfgvJournal" runat="server" />      
                                </td>
       </tr>
         <tr class="dpJobGreenHeader">
                                    <td colspan="4" style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblJournalSummary" runat="server" Text="Journal Summary"></asp:Label>
                                        <asp:ImageButton ID="imgAdd" src="images/tools/add.png" runat="server" OnClick="imgAdd_Click" />
                                        
                                    </td>
                                  
                                </tr>
       </table>
         <div class="content" id="tabGeneral" runat="server">
        <asp:GridView ID="grdStyleSheet" runat="server"  AutoGenerateColumns="false"
           EmptyDataText="No data available." Font-Size="8pt"  
          PageSize="7" OnRowDataBound="grdStyleSheet_RowDataBound" OnRowCommand="grdStyleSheet_RowCommand" DataKeyNames="Journal Acronym" OnSorting="grdStyleSheet_Sorting"  AllowSorting="True" OnRowEditing="grdStyleSheet_RowEditing" OnRowCancelingEdit="grdStyleSheet_RowCancelingEdit" >
            <HeaderStyle CssClass="GVFixedHeader"/>
            <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
            <asp:TemplateField>
                <HeaderTemplate>Serial No.</HeaderTemplate>
                <ItemTemplate>
                <asp:Label ID="lblSRNO" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                <asp:HiddenField ID="hfgvJournal" runat="server" Value='<%# Eval("Journal Acronym") %>' />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="5px" />
            </asp:TemplateField>

                <asp:BoundField DataField="Journal Acronym"  SortExpression="Journal Acronym" HeaderText="Journal Acronym" >
                    <ItemStyle HorizontalAlign="Center" Width="11px" />
                </asp:BoundField>
                <asp:BoundField DataField="Journal Title" HeaderText="Journal Title">
                    <ItemStyle HorizontalAlign="Left" Width="250px" />
                </asp:BoundField>
                <asp:BoundField DataField="Production Editor" HeaderText="Production Editor">
                    <ItemStyle HorizontalAlign="Left" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="Trim Size" HeaderText="Trim Size" >
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:BoundField DataField="Is CopyEdit" SortExpression="Is CopyEdit" HeaderText="Is CopyEdit">
                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="Is Sensitive" SortExpression="Is Sensitive" HeaderText="Is Sensitive">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="Is SAM"  SortExpression="Is SAM" HeaderText="Is SAM">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:BoundField>
                <asp:BoundField DataField="FPM Journal" SortExpression="FPM Journal" HeaderText="FPM Journal">
                    <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:BoundField>
                
                 <asp:TemplateField HeaderText="SAM Profile" >
                 <ItemTemplate>
                 <asp:ImageButton ID="imgSAMProfile" runat="server" CommandName="SAM Profile" ImageUrl="~/images/QMS/word.png" />
                  
                 </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:TemplateField>
               <asp:TemplateField HeaderText="Style Sheet">
                 <ItemTemplate>
                   <asp:ImageButton ID="imgStyleSheet" runat="server" ImageUrl="~/images/QMS/word.png" CommandName="Style Sheet" /> 
                 </ItemTemplate>
                   <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:TemplateField>
                
                   <asp:TemplateField HeaderText="Markup Sample" >
                 <ItemTemplate>
                   <asp:ImageButton ID="imgMarkupSample" runat="server"  ImageUrl="~/images/QMS/pdf.png" CommandName="Markup Sample"/>
                 </ItemTemplate>
                       <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:TemplateField>
                
                 <asp:TemplateField HeaderText="AQ Cover Sheet Number"> 
                 <ItemTemplate>
                   <asp:LinkButton ID="lnkAQ_Cover_Sheet_No" runat="server"   CommandName="AQ_Cover_Sheet_No" Text='<%# Eval("AQ_Cover_Sheet_No") %>'></asp:LinkButton>
                 </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" Width="20px" />
                </asp:TemplateField>
                <asp:BoundField DataField="DOI" HeaderText="References style with DOI information">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                 <asp:BoundField DataField="Revised stylesheet received date" HeaderText="Revised stylesheet received date" Visible="False">
                     <ItemStyle Width="100px" />
                 </asp:BoundField>
                <asp:BoundField DataField="Template update date" HeaderText="Template update date" Visible="False">
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:TemplateField >
                <ItemTemplate><%#DataBinder.Eval(Container.DataItem,"Edit") %></ItemTemplate>
                    <ItemStyle Font-Bold="True" />
            </asp:TemplateField>
 
              
          
          </Columns>
        </asp:GridView>
        &nbsp;&nbsp;
        </div>
        
          <script language="javascript">
        var xmlHTTP;
        function InsertJournal()
        {
            try
            {
                if(document.getElementById("Txtjourcode").value=="" || document.getElementById("Txtjourname").value=="")
                {
                    alert("Please give Journal Title and Journal Code");
                    return false;
                }
                // Firefox, Opera 8.0+, Safari
                xmlHTTP=new xmlHTTPRequest();
            }
            catch(e)
            {
                // Internet Explorer
                 try
                 {
                    xmlHTTP=new ActiveXObject("Msxml2.XMLHTTP");
                 }
                 catch(e)
                 {
                    try
                    {
                        xmlHTTP=new ActiveXObject("Microsoft.XMLHTTP");
                    }
                    catch(e)
                    {
                        alert("Your browser does not support AJAX!");
                        return false;
                    }
                 }
            }
            
            var idval;
            xmlHTTP.onreadystatechange=function()
            {
                if(xmlHTTP.readyState==4)
                {
                    if(xmlHTTP.responseText=="This Journal is Already Exists")
                        alert(xmlHTTP.responseText);
                    else    
                        document.location="EditJournalDetails.aspx?" + xmlHTTP.responseText;
                }
            }
            var val="custid=" + document.getElementById("ddlcustname").value + "&jourcode=" + document.getElementById("Txtjourcode").value + "&jourtitle=" + document.getElementById("Txtjourname").value;
             xmlHTTP.open("Get","InsertJournal.aspx?" + val, true);
             xmlHTTP.send(null);
             
        }
        
    
    </script>
        
        <div class="content" id="tabJournaldetails" runat="server">
            &nbsp;</div>
        
     </div>
    </form>
    
</body>
</html>

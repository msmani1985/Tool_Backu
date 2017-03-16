<%@ page language="C#" autoeventwireup="true" inherits="Projects, App_Web_opij0lkt" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Projects</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="scripts/tabs1.js"></script>--%>
    <link href="scripts/tabs.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="scripts/common.js"></script>

    <script type="text/javascript">
    var gvelem;
    var gvcolor;
    function setColor(element,val,val1){      
      //alert(gvelem);
      if(gvelem!=null){//alert(gvelem.style.backgroundColor);
        gvelem.style.backgroundColor = gvcolor;
      }
      gvelem = element;
      gvcolor = element.style.backgroundColor;
      element.style.backgroundColor = '#C2C2C2';
      document.form1.hfP_ID.value=val;
      document.form1.hfP_Name.value=val1
      if(document.getElementById("lblProjectSummary"))
        document.getElementById("lblProjectSummary").innerText="Project : "+val1;
      else if(document.all.lblProjectSummary)
        document.all.lblProjectSummary.innerText="Project : "+val1;
      else if(document.form1.lblProjectSummary)
        document.form1.lblProjectSummary.innerText="Project : "+val1;
    }
    function setMouseOverColor(element)
    {
        gvcolor = element.style.backgroundColor;
        element.style.backgroundColor='#C2C2C2';
        element.style.cursor='hand';
        element.style.textDecoration='underline';
    }
    function setMouseOutColor(element)
    {
        element.style.backgroundColor=gvcolor;
        element.style.textDecoration='none';
    }

    function imgBD_editor_onclick() {
 
        if(document.form1.drpProjectcustomer!=null && document.form1.drpProjectcustomer.value !="0")
            window.open("contacts.aspx?form=Projects&type=0&trgname=txtProjectEditor&trgid=hfprojectEditorId&cid="+document.form1.drpProjectcustomer.value,"Contacts","width=800,height=600,status=yes, scrollbars=yes");
        else alert("Select a customer"); 
    }
//    function vallidChapter(){
//        var msg = "";
//        if(document.form1.drpChapterName!=null && document.form1.drpChapterName.value =="0")
//            msg+="* Select a Chapter Name.\r\n";
//        if(document.form1.txtChapCount.value=="" || document.form1.txtChapCount.value=="0")
//            msg+="* No. of Items should be greater than zero.\r\n";
//        if(msg!=""){
//            alert(msg);
//        return false;
//        }
//        return true;
//    }
    function showhideDiv(me,ctrl){//alert(document.getElementById("divCommHistory"));
    var o;
        if(document.getElementById(ctrl))o=document.getElementById(ctrl);
        else if(document.all.ctrl)o=document.all.ctrl;
        else if(document.form1.ctrl)o=document.form1.ctrl;
         //alert(me.innerText);   
        if(o.style.display=='' || o.style.display=='block'){
            o.style.display = 'none';
            if(document.all)me.innerText='(Show)'; else me.textContent='(Show)';
        }
        else {
            o.style.display = 'block'
            if(document.all)me.innerText='(Hide)'; else me.textContent='(Hide)';
        }
    }
     function openModal(){ 
        document.getElementById ('txtModulePrefix').value='Module';
        document.getElementById ('txtModuleCount').value='';
        document.getElementById ('txtModuleSdateNew').value=document.getElementById('txtModuleSdate').value;
        document.getElementById ('txtModuleDdateNew').value=document.getElementById('txtModuleDdate').value;
        document.getElementById ('txtModuleHDdateNew').value=document.getElementById('txtModuleHDdate').value;
        document.getElementById ('divPopModule').style.visibility='visible';
        document.getElementById ('divPopModule').style.display='';       
        document.getElementById ('divPopModule').style.top= '150px';
        document.getElementById ('divPopModule').style.left='248px';     
        if (typeof document.body.style.maxHeight == "undefined")
        {  
            var layer = document.getElementById ('divPopModule');
            layer.style.display = 'block';
            var iframe = document.getElementById('iframetop');
            iframe.style.display = 'block';
            iframe.style.visibility = 'visible';
            iframe.style.top= layer.offsetTop-10;
            iframe.style.left= layer.offsetLeft-10;
            iframe.style.width=  layer.offsetWidth+10;
            iframe.style.height= layer.offsetHeight+10; 
        }
        else
        {     
            document.getElementById ('divMasked').style.display='';
            document.getElementById ('divMasked').style.visibility='visible';
            document.getElementById ('divMasked').style.top='0px';
            document.getElementById ('divMasked').style.left='0px';
            document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
            document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';        
        }
        document.getElementById ('txtModulePrefix').select();
    }
    function closeModal(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopModule').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }
     function validModule(){
     var msg = "";
        if(document.getElementById('txtModuleCount').value=='' || document.getElementById('txtModuleCount').value =="0")
            msg+="* No. of Modules should be greater than zero.\r\n";
       /*
        if(document.getElementById('txtModuleSdateNew').value=='')
            msg+="* Select a Start Date.\r\n";
        if(document.getElementById('txtModuleDdateNew').value=='')
            msg+="* Select a Due Date.\r\n";
        if(document.getElementById('txtModuleHDdateNew').value=='')
            msg+="* Select a HalfDue Date.\r\n"; */
        if(msg!="") alert(msg);
        else{
            document.getElementById ('divMasked').style.display='none';
            document.getElementById ('divPopModule').style.display='none';
            document.getElementById ('lnkModuleAdd').click();
        }
    }
    function validInvoiceTypeItem(){
        if(document.form1.drpCostInvoiceType!=null && document.form1.drpCostInvoiceType.value !="0" && document.form1.drpCostInvoiceType.value =="4"){
//            alert(document.getElementById ('divPopBCostInvTypeItem'));
//            alert(document.getElementById ('divMasked'));
            document.getElementById ('divPopPCostInvTypeItem').style.visibility='visible';
            document.getElementById ('divPopPCostInvTypeItem').style.display='';       
            document.getElementById ('divPopPCostInvTypeItem').style.top= '150px';
            document.getElementById ('divPopPCostInvTypeItem').style.left='248px';        
            if (typeof document.body.style.maxHeight == "undefined")
            {
                var layer = document.getElementById ('divPopPCostInvTypeItem');
                layer.style.display = 'block';
                var iframe = document.getElementById('iframetop');
                iframe.style.display = 'block';
                iframe.style.visibility = 'visible';
                iframe.style.top= layer.offsetTop-10;
                iframe.style.left= layer.offsetLeft-10;
                iframe.style.width=  layer.offsetWidth+10;
                iframe.style.height= layer.offsetHeight+10;                
            }else
            {
                document.getElementById ('divMasked').style.display='';
                document.getElementById ('divMasked').style.visibility='visible';
                document.getElementById ('divMasked').style.top='0px';
                document.getElementById ('divMasked').style.left='0px';
                document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px'; 
            }  
            
            document.getElementById ('txtPCpopInvTypeItem').select();         
        }
        else {alert("Select Invoice Type to Additional Cost");}
    }
    function validSaveItem(){
        if(document.form1.txtPCpopInvTypeItem.value=='')alert('Enter Invoice Type Item');
        else document.getElementById ('lnkCostAddInvTypeItem').click();
    }
    function closeModalPCost(){
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopPCostInvTypeItem').style.display='none';
        document.getElementById ('iframetop').style.display='none';
    }
    function printProject(){if(document.form1.hfP_ID.value==""){alert('Select a Project');return false;} var w=window.open('jobbag.aspx?jobid=' + document.form1.hfP_ID.value + '&jobtypeid=4&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;}
    
    function printModule(){if(document.form1.drpModuleNo.value=="0"){alert('Select a Module');return false;} var w=window.open('jobbag.aspx?jobid=' + document.form1.drpModuleNo.value + '&jobtypeid=8&print=1','Preview','width=1000,height=600,left=5,top=25,toolbars=no,scrollbars=yes,status=yes,resizable=yes');w.focus();return false;}

    function closeModalArtHold(){
        document.form1.chkProjectOnHold.checked = false;
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopIsPOnHold').style.display='none';
        document.getElementById ('iframetop').style.display='none';
        }
        function validSaveItem_hold(){
            if(document.form1.drpProjectOnHoldType.value=='0')alert('Select an hold type');
            else if(document.form1.txtProjectOnHoldReason.value=='')alert('Enter hold reason');
            else __doPostBack('lnkProjectHold','');
        }
     function isPOnhold(){
            if(document.form1.hfP_ID.value==''){
                alert('You should first create the Book.');
                document.form1.chkProjectOnHold.checked = false;
                return;
            }
            if(!document.form1.chkProjectOnHold.checked){
                if(confirm('This job is currently On Hold, Do you want to release?')){
                    document.form1.chkProjectOnHold.checked = false;
                    __doPostBack('lnkProjectHold','');
                    }
                else document.form1.chkProjectOnHold.checked = true;
            }
            else
            {
                document.getElementById ('divPopIsPOnHold').style.visibility='visible';
                document.getElementById ('divPopIsPOnHold').style.display='';       
                document.getElementById ('divPopIsPOnHold').style.top= '150px';
                document.getElementById ('divPopIsPOnHold').style.left='248px';
                if (typeof document.body.style.maxHeight == "undefined")
                {  
                    var layer = document.getElementById ('divPopIsPOnHold');
                    layer.style.display = 'block';
                    var iframe = document.getElementById('iframetop');
                    iframe.style.display = 'block';
                    iframe.style.visibility = 'visible';
                    iframe.style.top= layer.offsetTop-10;
                    iframe.style.left= layer.offsetLeft-10;
                    iframe.style.width=  layer.offsetWidth+10;
                    iframe.style.height= layer.offsetHeight+10; 
                }else
                {   
                    document.getElementById ('divMasked').style.display='';
                    document.getElementById ('divMasked').style.visibility='visible';
                    document.getElementById ('divMasked').style.top='0px';
                    document.getElementById ('divMasked').style.left='0px';
                    document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                    document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';                     
                }
                document.form1.drpProjectOnHoldType.value='0';
                document.form1.txtProjectOnHoldReason.value='';
            }
        }
        function closeModalArtMHold(){
        document.form1.chkModuleOnHold.checked = false;
        document.getElementById ('divMasked').style.display='none';
        document.getElementById ('divPopIsMOnHold').style.display='none';
        document.getElementById ('iframetop').style.display='none';
        }
        function validSaveItem_Mhold(){
            if(document.form1.drpModuleOnHoldType.value=='0')alert('Select an hold type');
            else if(document.form1.txtModuleOnHoldReason.value=='')alert('Enter hold reason');
            else __doPostBack('lnkModuleHold','');
        }
     function isMOnhold(){
            if(document.form1.hfP_jobidforhold.value==''){
                alert('You should first create the Module.');
                document.form1.chkModuleOnHold.checked = false;
                return;
            }
            if(!document.form1.chkModuleOnHold.checked){
                if(confirm('This job is currently On Hold, Do you want to release?')){
                    document.form1.chkModuleOnHold.checked = false;
                    __doPostBack('lnkModuleHold','');
                    }
                else document.form1.chkModuleOnHold.checked = true;
            }
            else
            {
                document.getElementById ('divPopIsMOnHold').style.visibility='visible';
                document.getElementById ('divPopIsMOnHold').style.display='';       
                document.getElementById ('divPopIsMOnHold').style.top= '150px';
                document.getElementById ('divPopIsMOnHold').style.left='248px';
                if (typeof document.body.style.maxHeight == "undefined")
                {  
                    var layer = document.getElementById ('divPopIsMOnHold');
                    layer.style.display = 'block';
                    var iframe = document.getElementById('iframetop');
                    iframe.style.display = 'block';
                    iframe.style.visibility = 'visible';
                    iframe.style.top= layer.offsetTop-10;
                    iframe.style.left= layer.offsetLeft-10;
                    iframe.style.width=  layer.offsetWidth+10;
                    iframe.style.height= layer.offsetHeight+10; 
                }else
                {   
                    document.getElementById ('divMasked').style.display='';
                    document.getElementById ('divMasked').style.visibility='visible';
                    document.getElementById ('divMasked').style.top='0px';
                    document.getElementById ('divMasked').style.left='0px';
                    document.getElementById ('divMasked').style.width=  document.documentElement.clientWidth + 'px';
                    document.getElementById ('divMasked').style.height= document.documentElement.clientHeight+ 'px';                     
                }
                document.form1.drpModuleOnHoldType.value='0';
                document.form1.txtModuleOnHoldReason.value='';
            }
        }
    </script>

    <style>
         iframe.divMasked
        {
        position:absolute;
	    padding:5px;
	    visibility:hidden;
	    border:1px solid gray;
	    font:normal 12px Verdana;
	    line-height:18px;
	    z-index:10000;
	    background-color:#ededed;
	    overflow-x:auto;
	    overflow-y:auto;
	    top:-1000px;
	    left:-1000px;
	    filter:progid:DXImageTransform.Microsoft.Shadow(color=#96A8BA,direction=135,Strength=5);
        }
        
        div.divMasked 
        {
        visibility: hidden;
        position:absolute;
        left:0px;
        top:0px;
        font-family:verdana;
        font-weight:bold;
        padding:40px;
        z-index:100;        
        background-image:url(images/tools/Mask.png);
        /* ieWin only stuff */
        _background-image:none;
        _filter:progid:DXImageTransform.Microsoft.AlphaImageLoader(enabled=true, sizingMethod=scale src='Mask.png');
        opacity:0.4;
        filter:alpha(opacity=70)
        }
        div.ModalPopup {
        font-family: Verdana, Arial, Helvetica, sans-serif;
        font-size: 11px;
        font-style: normal;
        background-color: #fff;
        position:absolute;
        /* set z-index higher than possible */
        z-index:10000;
        visibility: hidden;
         color: Black;
        border-style: solid;
        border-color: #999999;
        border-width: 1px;
        width: 300px;
        height :auto;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divMasked" class="divMasked" style="left: 0px; top: 0px">
        </div>
        <iframe width="0" scrolling="no" height="0" 
            frameborder="0" class="divMasked" id="iframetop">
        </iframe>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div>
                            <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Project</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Customer</strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:DropDownList ID="drpCustomerSearch" runat="server" Width="325px" TabIndex = "1">
                                        </asp:DropDownList>
                                        <asp:CheckBox ID="chkViewCompleted" runat="server" Font-Bold="True" Text="Show Completed Jobs" TabIndex = "2" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Project</strong>
                                    </td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" CssClass="TxtBoxSearch" TabIndex = "3"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" TabIndex = "4" CssClass="dpbutton" OnClick="btnSearch_Click" />
                                        <asp:HiddenField ID="hfP_ID" runat="server" />
                                        <asp:HiddenField ID="hfP_Name" runat="server" />
                                        <asp:HiddenField ID="hfP_typeid" runat="server" />
                                        <asp:HiddenField ID="hfP_jobidforhold" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <strong>&nbsp;</strong>
                        </div>
                    </td>
                </tr>
                <tr>
                    
                    <td>
                        <ol id="toc">
                            <li id="miGeneral" runat="server">
                                <asp:LinkButton ID="lnkGeneral" runat="server" TabIndex = "4" OnClick="lnkGeneral_Click">General</asp:LinkButton></li>
                            <li id="miProjectDetails" runat="server">
                                <asp:LinkButton ID="lnkProjectdetails" TabIndex = "4" runat="server" OnClick="lnkProjectdetails_Click" >Project Details</asp:LinkButton></li>
                            <li id="miProjectAddCost" runat="server">
                                <asp:LinkButton ID="lnkProjectAddCost" TabIndex = "4" runat="server" OnClick="lnkProjectAddCost_Click" >Project Cost</asp:LinkButton></li>
                            <li id="miProjecModuleDetails" runat="server">
                                <asp:LinkButton ID="lnkPModuledetails" TabIndex = "4" runat="server" OnClick="lnkPModuledetails_Click">Module Details</asp:LinkButton></li>
                            <li id="miProjectEvents" runat="server">
                                <asp:LinkButton ID="lnkProjectEvents" TabIndex = "4" runat="server" OnClick="lnkProjectEvents_Click">Events</asp:LinkButton></li>
                            <li id="miComments" runat="server">
                                <asp:LinkButton ID="lnkComments" TabIndex = "4" runat="server" OnClick="lnkComments_Click">Comments</asp:LinkButton></li>
                        </ol>
                        
                        <div class="content" id="tabGeneral" runat="server">
                            <table id="Table4" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr class="dpJobGreenHeader">
                                    <td  style="height: 32px">
                                        <img id="Img8" align="absmiddle" src="images/tools/information.png" runat="server" />
                                        <asp:Label ID="lblProjectSummary" runat="server" Text="Search Summary"></asp:Label></td>
                                        <td align="right" style="padding-right:10px">
                                        <asp:ImageButton ImageUrl="images/excel.jpg" runat="server" ID="exportExcel" OnClick="exportExcel_Click" ToolTip="Export Excel" />
                                        </td>
                                        
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:GridView ID="GvProject" runat="server" AutoGenerateColumns="False"  Font-Size="8pt"  
                                        OnRowDataBound="GridView1_RowDataBound" CssClass="lightbackground" width="100%"
                                         AllowSorting="True" OnSorting="GvProject_Sorting" >
                                            <HeaderStyle CssClass="GVFixedHeader" />
                                            <AlternatingRowStyle BackColor="#f2f2f2" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="<%# id++ %>"></asp:Label>
                                                        <br />
                                                        <asp:HiddenField ID="hfgvProjectID" runat="server" Value='<%# Eval("parent_job_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField SortExpression="parent_job_id" HeaderText="Job ID" >
                                                        <ItemTemplate>
                                                               <asp:Label ID="lblJOB" runat="server" Text='<%# Eval("parent_job_id") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                
                                                <asp:TemplateField SortExpression="cust_name" HeaderText="Customer"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCusName" runat="server" Text='<%# Eval("cust_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="name" HeaderText="Project"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPnumber" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="title" HeaderText="Project Title" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPtitle" runat="server" Text='<%# Eval("title") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  SortExpression="display_name" HeaderText="Editor" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBEditor" runat="server" Text='<%# Eval("display_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="job_stage_name" HeaderText="Stage" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPStage" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="received_date"  HeaderText="Rec. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPRecDate" runat="server" Text='<%# Eval("received_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="half_due_date" HeaderText="Half Due Date"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPHlfDueDate" runat="server" Text='<%# Eval("half_due_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="due_date" HeaderText="Due Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  SortExpression="despatch_date"  HeaderText="Disp. Date" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPDispDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField SortExpression="invoice_no" HeaderText="Invoice No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPInvNo" runat="server" Text='<%# Eval("invoice_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabProjectDetails" runat="server">
                            <div id="CUSTOMER_TABLE" class="boxTable">
                            </div>
                            <div id="PARENT_JOB" class="boxTable" style="">
                                <table id="XMLTAGS" border="0" width="100%" cellpadding="2" cellspacing="0">
                                    <tr bgcolor="#f0fff0">
                                        <td colspan="4" class="dpJobGreenHeader" style="height: 20px">
                                            <img id="imgProjectHeader" align="absmiddle" src="images/tools/new.png" runat="server" />&nbsp;<asp:Label
                                                ID="lblProjectHeader" runat="server" Text="Label">New Project</asp:Label></td>
                                        <td class="dpJobGreenHeader" style="height: 20px">
                                            <asp:ImageButton ID="cmd_New_Project" ImageUrl="~/images/tools/j_new.png"  runat="server"
                                                ToolTip="New Project" TabIndex = "24" OnClick="cmd_New_Project_Click"  />
                                            <asp:ImageButton ID="cmd_Save_Project" ImageUrl="~/images/tools/j_save.png" runat="server"
                                                ToolTip="Save Project" TabIndex = "25" OnClick="cmd_Save_Project_Click"  />
                                            <asp:ImageButton ID="cmd_Print_Project" ImageUrl="~/images/tools/j_print.png" runat="server"
                                                ToolTip="Print Preview" TabIndex = "26"  OnClientClick="javascript:return printProject()" OnClick="cmd_Print_Project_Click" />
                                    </tr>
                                    <tr>
                                        <td>
                                            Customer:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectcustomer" runat="server" Width="306px" OnSelectedIndexChanged="drpProjectcustomer_SelectedIndexChanged" AutoPostBack="True" TabIndex = "5">
                                            </asp:DropDownList></td>
                                        <td>
                                            Project&nbsp;Stage:
                                        </td>
                                        <td>
                                          <asp:Label ID="lblProjectStage" runat="server" CssClass="TxtBox" Width="100px" MaxLength="15" Text = "Inhouse">
                                          </asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Cat #:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="TxtBox" BackColor="#FFFFC0"
                                                Width="300px" MaxLength="150" TabIndex = "6"></asp:TextBox></td>
                                        <td>
                                            Start Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectSdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "20"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtProjectSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_stdate" runat="server" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 24px">
                                            Project Title:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td style="height: 24px">
                                            <asp:TextBox ID="txtProjectTitle" runat="server" CssClass="TxtBox" Width="300px"
                                                BackColor="#FFFFC0" MaxLength="300" TabIndex = "7"></asp:TextBox></td>
                                        
                                        <td>
                                            Half Due Date:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectHDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "21"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtProjectHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_hdudate" runat="server" /></td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 13px">
                                            Financial Site</td>
                                        <td style="height: 13px">
                                                 <asp:DropDownList ID="drpProjectcustfinsite" runat="server" Width="306px" TabIndex = "8">
                                        </asp:DropDownList></td>
                                        
                                        <td style="height: 13px">
                                            Due Date:</td>
                                        <td style="height: 13px">
                                            <asp:TextBox ID="txtProjectDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "22"></asp:TextBox>
                                            <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtProjectDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                src="images/Calendar.jpg" style="cursor: pointer;" id="imgBD_dudate"  runat="server" /></td>
                                        <td style="height: 13px">
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Size:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectSize" runat="server" CssClass="TxtBox" Width="200px" MaxLength="50" TabIndex = "9"></asp:TextBox>
                                        </td>
                                          <td>
                                            Despatch:</td>
                                        <td>
                                            <asp:CheckBox ID="chkProjectDespatch" runat="server" TabIndex = "23"/>
                                        </td>
                                        <td>
                                        </td>
                                        
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Print ISBN:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectPISBN" runat="server" CssClass="TxtBox" Width="200px"
                                                MaxLength="16" TabIndex = "10"></asp:TextBox></td>
                                  <td>
                                            Sales Group: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectSalesGroup" runat="server" TabIndex="23" /></td>
                                        <td>
                                        </td>                                          
                                    </tr>
                                    <tr>
                                        <td>
                                            Online ISBN:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectOISBN" runat="server" CssClass="TxtBox" Width="200px"
                                                MaxLength="16" TabIndex = "11"></asp:TextBox></td>
                                              <td>Invoice Job Location</td>
                                              
                                               <td><asp:DropDownList ID="drpprojectlocation" runat="server" TabIndex="12">
                                        <asp:ListItem Value="2">Chennai</asp:ListItem>
                                         <asp:ListItem Value="3">Coimbatore</asp:ListItem>
                                        </asp:DropDownList></td>  
                                               
                                        
                                    </tr>
                                    <tr>
                                        <td>
                                            Format/Style:<span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectTypeset" runat="server" TabIndex = "12">
                                            </asp:DropDownList>
                                        </td>
                                         <td><asp:CheckBox ID="chkProjectOnHold" onclick="javascript:isPOnhold();" runat="server"
                                                Font-Bold="False" Text="On Hold" TabIndex="13" />
                                        <asp:LinkButton ID="lnkProjectHold" runat="server" OnClick="lnkProjectHold_Click"></asp:LinkButton></td>
                                        
                                                <td align="left" colspan="3" rowspan="9" valign="top">
                                                    <div style="vertical-align: top">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td colspan="3" class="subheading">
                                                                    <strong>Completed Stage(s)</strong></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="subheading" colspan="3" valign="top">
                                                                    <asp:DataGrid ID="dgrdProjectStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
                                                                        <AlternatingItemStyle CssClass="dullbackground" />
                                                                        <HeaderStyle CssClass="darkbackground" />
                                                                        <Columns>
                                                                            <asp:TemplateColumn HeaderText="Stage">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPD_StageName" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Start Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPD_StartDate" runat="server" Text='<%# Eval("received_date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Due Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPD_DueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Half Due Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPD_HalfdueDate" runat="server" Text='<%# Eval("half_due_date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn HeaderText="Desp. Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPD_DespDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid></td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Service Type:<span style="font-size: 9pt; color: #ff0000">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="drpProjectServicetype" runat="server" TabIndex = "13">
                                            </asp:DropDownList></td>
                                        
                                    </tr>
                                    <tr>
                                     <td>
                                            PO Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPONUMBER" runat="server" CssClass="TxtBox" Width="200px" MaxLength="100" TabIndex = "14"></asp:TextBox>
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                     <td>
                                            Print Pages:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtprintpages" runat="server" CssClass="TxtBox" Width="200px" MaxLength="100" TabIndex = "14"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                     <td>
                                            Project Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProjectNumber" runat="server" CssClass="TxtBox" Width="200px" MaxLength="16" TabIndex = "15"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Author:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectAuthor" runat="server" CssClass="TxtBox" MaxLength="100" TabIndex="15"
                                                Width="200px"></asp:TextBox></td>
                                    </tr>
                                    
                                    
                                    <tr>
                                        <td>
                                            Project Editor:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectEditor" runat="server" CssClass="TxtBox" Width="280px" TabIndex = "16"></asp:TextBox>
                                            <img id="imgBD_editor" align="absMiddle" src="images/tools/user_go.png" language="javascript" TabIndex = "17"
                                                onclick="return imgBD_editor_onclick()" style="cursor: pointer" title="Select Editor" />
                                            <asp:HiddenField ID="hfprojectEditorId" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Comments:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectComments" runat="server" CssClass="TxtBox" TextMode="MultiLine"
                                                Height="50px" Width="300px" TabIndex = "18"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Invoice Description:</td>
                                        <td>
                                            <asp:TextBox ID="txtProjectInvoiceDesc" runat="server" Height="50px" TextMode="MultiLine"
                                                Width="300px" CssClass="TxtBox" TabIndex = "19"></asp:TextBox></td>
                                    </tr>
                                    <tr><td colspan="2" style="height: 17px">
                                    <div id="divPopIsPOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Project On Hold
                                                        </td>
                                                        <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                            <a href="#" title="Close" onclick="javascript:closeModalArtHold();" style="color: White;">
                                                                [x]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpProjectOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtProjectOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem_hold();"><strong>Submit</strong></a>
                                                            &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtHold();"><strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                    </td></tr>
                                </table>
                            </div>
                        </div>
                        <div class="content" id="tabProjectAddCost" runat="server">
                            <table id="Table1" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 657px">
                                        <img id="ImgProjectCost" align="absmiddle" src="images/tools/currency_eur.png" runat="server" />
                                        <asp:Label ID="lblCostHeader" runat="server" Text="Project Cost"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="cmd_Cost_new" ImageUrl="~/images/tools/j_new.png" runat="server"
                                            ToolTip="New" OnClick="cmd_Cost_new_Click" />
                                        <asp:ImageButton ID="cmd_Save_Cost" ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save" OnClick="cmd_Save_Cost_Click" />
                                        <asp:ImageButton ID="cmd_Cost_orderindex" ImageUrl="~/images/tools/j_index.png" runat="server"
                                            ToolTip="Order Index" OnClick="cmd_Cost_orderindex_Click" /></td>
                                </tr>
                                <tr id="trPCCtrls" runat="server">
                                    <td colspan="5">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td style="width: 137px; height: 21px;">
                                                    Invoice Type: <span style="font-size: 9pt; color: #ff0000">*</span>
                                                </td>
                                                <td style="width: 141px; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceType" runat="server" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="drpCostInvoiceType_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="height: 21px; width: 146px;">
                                                    &nbsp; Invoice Type Item: <span style="font-size: 9pt; color: #ff0000">*</span>&nbsp;</td>
                                                <td style="font-size: 8pt; height: 21px;">
                                                    <asp:DropDownList ID="drpCostInvoiceTypeItem" runat="server" Width="200px">
                                                    </asp:DropDownList>
                                                    <img id="imgbtnPCAddInvTypeItem" align="absMiddle" src="images/tools/add.png" style="cursor: pointer"
                                                        title="New Invoice Type Item" onclick="javascript:return validInvoiceTypeItem();"
                                                        runat="server" />
                                                        </td>
                                            </tr>
                                            <tr style="font-size: 8pt">
                                                <td style="width: 137px; height: 19px;">
                                                    Type of Cost: <span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                <td style="width: 141px; font-size: 8pt; height: 19px;">
                                                    <asp:DropDownList ID="drpCostType" runat="server" Width="200px">
                                                    </asp:DropDownList></td>
                                                <td style="font-size: 8pt; width: 146px; height: 19px;">
                                                    <span style="color: #000000">&nbsp; Quantity:</span></td>
                                                <td style="font-size: 8pt; height: 19px;">
                                                    <asp:TextBox ID="txtCostQuantity" runat="server" Width="50px" CssClass="TxtBox" onkeypress="javascript: return OnlyAllowNumbers(this,event);"></asp:TextBox>&nbsp;
                                                    Price Code: <span style="font-size: 9pt; color: #ff0000">*
                                                        <asp:TextBox ID="txtCostPriceCode" runat="server" Width="50px" CssClass="TxtBox"
                                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" BackColor="#FFFFC0"></asp:TextBox></span></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Item Description</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtCostItemdesc" runat="server" CssClass="TxtBox" MaxLength="100"
                                                        ToolTip="Eg: 40204.PY9781420073669.PBHRD.XXXX" Width="542px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:HiddenField ID="hfCostInvTypeItemID" runat="server" />
                                        <asp:LinkButton ID="lnkCostAddInvTypeItem" runat="server" OnClick="lnkCostAddInvTypeItem_Click"></asp:LinkButton></td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:GridView ID="gvProjectCost" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                            Width="100%" OnRowCommand="gvProjectCost_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Invoice Type Item">
                                                    <ItemTemplate>
                                                        <%# Eval("InvoiceType_item_Name")%>
                                                        <asp:HiddenField ID="hfPC_invoicetypeitem" runat="server" Value='<%# Eval("job_invoice_type_item_id") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type of Cost">
                                                    <ItemTemplate>
                                                        <%# Eval("cost_type_name")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <ItemTemplate>
                                                        <%# Eval("quantity_addnl_type")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price Code">
                                                    <ItemTemplate>
                                                        <%# Eval("price_code")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Description">
                                                    <ItemTemplate>
                                                        <%# Eval("description")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Index">
                                                    <ItemTemplate>
                                                        <%# Eval("order_index")%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Re-Index">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="drpPC_orderindex" runat="server" SelectedValue='<%# Eval("order_index") %>'>
                                                            <asp:ListItem Text="" Value="0"></asp:ListItem>
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
                                                            <asp:ListItem>16</asp:ListItem>
                                                            <asp:ListItem>17</asp:ListItem>
                                                            <asp:ListItem>18</asp:ListItem>
                                                            <asp:ListItem>19</asp:ListItem>
                                                            <asp:ListItem>20</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnPC_Edit" runat="server" ImageUrl="~/images/tools/edit.png"
                                                            ToolTip="Edit" CommandName="PCEdit" />
                                                        <asp:ImageButton ID="imgbtnPC_Delete" runat="server" ImageUrl="~/images/tools/delete.png"
                                                            ToolTip="Delete" CommandName="PCDelete" OnClientClick="javascript: return confirm('Confirm Delete?');" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                    No records found.</div>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="darkbackground" />
                                            <AlternatingRowStyle CssClass="dullbackground" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="height: 17px">
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" style="height: 17px">
                                        <div id="divPopPCostInvTypeItem" class="ModalPopup">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                        width: 163px;">
                                                        &nbsp;New Invoice Type Item
                                                    </td>
                                                    <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                        <a href="#" title="Close" onclick="javascript:closeModalPCost();" style="color: White;">
                                                            [x]</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 163px">
                                                        &nbsp;Invoice Type:</td>
                                                    <td>
                                                        <strong>Additional Cost</strong></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 163px">
                                                        &nbsp;Invoice Type Item:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                    <td>
                                                        <asp:TextBox ID="txtPCpopInvTypeItem" runat="server" CssClass="TxtBox" Width="120px"
                                                            MaxLength="50"></asp:TextBox></td>
                                                </tr>
                                                <tr bgcolor="Honeydew">
                                                    <td colspan="2" align="center" style="height: 18px">
                                                        <a class="link1" href="#" onclick="javascript:validSaveItem();"><strong>Submit</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeModalPCost();"><strong>Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabModuledetails" runat="server">
                            <table id="Table3" border="0" width="100%" cellpadding="2" cellspacing="0">
                                <tr bgcolor="#f0fff0">
                                    <td colspan="4" class="dpJobGreenHeader">
                                        <img id="imgModuleHeader" align="absmiddle" src="images/tools/new.png" runat="server" />
                                        <asp:Label ID="lblModHeader" runat="server" Text="Module Details"></asp:Label></td>
                                    <td class="dpJobGreenHeader" style="width: 104px">
                                        <span style="font-size: 9pt; color: #ff0000"></span>
                                        <asp:ImageButton ID="cmd_Save_Module" TabIndex = "39"  ImageUrl="~/images/tools/j_save.png" runat="server"
                                            ToolTip="Save Module" OnClick="cmd_Save_Module_Click"  />
                                        <asp:ImageButton ID="ImageButton4" ImageUrl="~/images/tools/j_print.png" runat="server" TabIndex = "40"
                                            ToolTip="Print" OnClientClick="javascript:return printModule()" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <div id="divPopModule" class="ModalPopup">
                                            <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td align="left" style="width: 143px; background-color: green; color: White; font-weight: bold"
                                                        valign="top">
                                                        &nbsp;Add Module</td>
                                                    <td style="width: 60%; background-color: green; color: White; font-weight: bold;"
                                                        align="right" valign="top">
                                                        <a href="#" title="Close" onclick="javascript:closeModal();" style="color: White;">[x]</a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 143px">
                                                        &nbsp;Module Prefix:</td>
                                                    <td style="width: 159px">
                                                        <asp:TextBox ID="txtModulePrefix" runat="server" CssClass="TxtBox" MaxLength="20" Width="120px"
                                                            ToolTip="Use Module Prefix to create more than one Module, Toc, Appendix, Prelim, Reference, Notes, Annex, Overview etc.,"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 143px">
                                                        &nbsp;No. of Modules:<span style="font-size: 9pt; color: #ff0000">*</span>
                                                    </td>
                                                    <td style="width: 159px">
                                                        <asp:TextBox ID="txtModuleCount" runat="server" CssClass="TxtBox" MaxLength="2" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                                            Width="120px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 143px">
                                                        &nbsp;Stage:</td>
                                                    <td style="width: 159px">
                                                        <asp:Label ID="lblModStage" runat="server" CssClass="TxtBox" Width="100px" MaxLength="15">
                                                        <strong>Inhouse</strong></asp:Label>
                                                       
                                          </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 143px">
                                                        &nbsp;Start Date:<span style="font-size: 9pt; color: #ff0000"></span></td>
                                                    <td style="width: 159px">
                                                        <asp:TextBox ID="txtModuleSdateNew" runat="server" CssClass="TxtBox" Width="80px" MaxLength="10"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleSdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img3" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 143px">
                                                        &nbsp;HalfDue Date:<span style="font-size: 9pt; color: #ff0000"></span></td>
                                                    <td style="width: 159px">
                                                        <asp:TextBox ID="txtModuleHDdateNew" runat="server" CssClass="TxtBox" Width="80px"
                                                            MaxLength="10"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleHDdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img11" runat="server" /></td>
                                                </tr>
                                                <tr>
                                                
                                                    <td style="width: 143px">
                                                        &nbsp;Due Date:<span style="font-size: 9pt; color: #ff0000"></span></td>
                                                    <td style="width: 159px">
                                                        <asp:TextBox ID="txtModuleDdateNew" runat="server" CssClass="TxtBox" Width="80px" MaxLength="10"></asp:TextBox>
                                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleDdateNew','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                                            src="images/Calendar.jpg" style="cursor: pointer;" id="Img10" runat="server" /></td>

                                                </tr>
                                                <tr bgcolor="Honeydew">
                                                    <td colspan="2" align="center">
                                                        <a class="link1" href="#" onclick="javascript:validModule();"><strong>Submit</strong></a>
                                                        &nbsp; <a class="link1" href="#" onclick="javascript:closeModal();"><strong>Cancel</strong></a>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px; height: 23px;">
                                        Module Name:</td>
                                    <td style="width: 314px; height: 23px;"> Body Matter
                                        <asp:LinkButton ID="lnkModuleAdd" runat="server" OnClick="lnkModuleAdd_Click" ></asp:LinkButton></td>
                                    <td style="height: 23px">
                                    </td>
                                    <td style="height: 23px">
                                    </td>
                                    <td style="width: 104px; height: 23px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px; height: 23px;">
                                        Module No.:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                    <td style="width: 314px; height: 23px;">
                                        <asp:DropDownList ID="drpModuleNo" runat="server" AutoPostBack="True" TabIndex = "27"
                                            Width="204px" OnSelectedIndexChanged="drpModuleNo_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 23px">
                                        Module Stage:</td>
                                    <td style="height: 23px">
                                        <asp:Label ID="lblModuleStage" runat="server" CssClass="TxtBox" Width="100px" MaxLength="15" Text = "Inhouse">
                                          </asp:Label></td>
                                    <td style="width: 104px; height: 23px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px; height: 24px;">
                                    </td>
                                    <td style="width: 314px; height: 24px;">
                                        <asp:TextBox ID="txtModuleNo" runat="server" BackColor="#FFFFC0" CssClass="TxtBox" TabIndex = "27"
                                            Width="200px" ToolTip="Enter Module No." MaxLength="30"></asp:TextBox>
                                        <img align="absMiddle" border="0" id="imgAddModule" runat="server" src="images/tools/add.png" style="cursor: pointer" TabIndex = "27"
                                            onclick="javascript:openModal();" title="Add multiple Modules" /></td>
                                    <td style="height: 24px">
                                        Start Date:</td>
                                    <td style="height: 24px">
                                        <asp:TextBox ID="txtModuleSdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "35"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleSdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgModule_stdate" runat="server" /></td>
                                    <td style="width: 104px; height: 24px;">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px; height: 24px;">
                                        Module Title:</td>
                                    <td style="width: 314px; height: 24px;">
                                        <asp:TextBox ID="txtModuleTitle" runat="server" CssClass="TxtBox" Width="300px" TabIndex = "28"
                                            ToolTip="Enter Module Title" MaxLength="300"></asp:TextBox></td>
                                    
                                    <td>
                                        HalfDue Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtModuleHDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "36"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleHDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;"  id="imgModule_hdudate" runat="server" /></td>
                                    <td style="width: 104px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        MS Pages:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtModuleMSpages" runat="server"  TabIndex = "29" CssClass="TxtBox" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15"></asp:TextBox></td>
                                    <td style="height: 24px">
                                        Due Date:</td>
                                    <td style="height: 24px">
                                        <asp:TextBox ID="txtModuleDdate" runat="server" CssClass="TxtBox" Width="80px" TabIndex = "37"></asp:TextBox>
                                        <img align="absmiddle" alt="Calendar" border="0" height="20"  onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtModuleDdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=no,status=no,resizable=no');calendar_window.focus()"
                                            src="images/Calendar.jpg" style="cursor: pointer;" id="imgModule_dudate" runat="server" /></td>
                                    <td style="width: 104px; height: 24px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Typeset Pages:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtModuleTypesetPages" runat="server" CssClass="TxtBox" Width="200px"  TabIndex = "30"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" MaxLength="15"></asp:TextBox></td>
                                    <td>
                                        Despatch:</td>
                                    <td>
                                        <asp:CheckBox ID="chkModuleDespatch" runat="server" TabIndex = "38" /></td>
                                    <td style="width: 104px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Figures:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtModuleFigures" runat="server" CssClass="TxtBox" Width="200px" TabIndex = "31" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15"></asp:TextBox></td>
                                    <td>
                                     <asp:CheckBox ID="chkModuleOnHold" onclick="javascript:isMOnhold();" runat="server"
                                                Font-Bold="False" Text="On Hold" TabIndex="13" />
                                                <asp:LinkButton ID="lnkModuleHold" runat="server" OnClick="lnkModuleHold_Click"></asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                    <td style="width: 104px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px; height: 28px;">
                                        Tables:</td>
                                    <td style="width: 314px; height: 28px;">
                                        <asp:TextBox ID="txtModuleTables" runat="server" CssClass="TxtBox"  TabIndex = "32" Width="200px" onkeypress="javascript: return OnlyAllowNumbers(this,event);"
                                            MaxLength="15"></asp:TextBox></td>
                                    <td colspan="3" rowspan="3" valign="top" align="left">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td colspan="3" class="subheading" valign="top">
                                                    <strong>Completed Stage(s)</strong></td>
                                            </tr>
                                            <tr>
                                                <td class="subheading" colspan="3" valign="top">
                                                    <asp:DataGrid ID="dgrdModuleStages" runat="server" AutoGenerateColumns="False" CssClass="lightbackground">
                                                        <AlternatingItemStyle CssClass="dullbackground" />
                                                        <HeaderStyle CssClass="darkbackground" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Stage">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMod_StageName" runat="server" Text='<%# Eval("job_stage_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Start Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMod_StartDate" runat="server" Text='<%# Eval("received_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Due Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMod_DueDate" runat="server" Text='<%# Eval("due_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Half Due Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMod_HalfdueDate" runat="server" Text='<%# Eval("half_due_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Desp. Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMod_DespDate" runat="server" Text='<%# Eval("despatch_date") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Equations:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtModuleEquations" runat="server" CssClass="TxtBox" Width="200px" TabIndex = "33"
                                            onkeypress="javascript: return OnlyAllowNumbers(this,event);" MaxLength="15"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td style="width: 105px">
                                        Comments:</td>
                                    <td style="width: 314px">
                                        <asp:TextBox ID="txtModuleComments" runat="server" CssClass="TxtBox" Height="60px"
                                            TabIndex = "34" TextMode="MultiLine" Width="300px"></asp:TextBox></td>
                                </tr>
                                <tr><td>
                                     <div id="divPopIsMOnHold" class="ModalPopup">
                                                <table cellpadding="2" cellspacing="0" border="0" width="100%">
                                                    <tr>
                                                        <td align="left" style="background-color: green; color: White; font-weight: bold;
                                                            width: 163px;">
                                                            &nbsp;Module On Hold
                                                        </td>
                                                        <td align="right" style="background-color: green; color: White; font-weight: bold">
                                                            <a href="#" title="Close" onclick="javascript:closeModalArtMHold();" style="color: White;">
                                                                [x]</a>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;OnHold Type:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:DropDownList ID="drpModuleOnHoldType" runat="server">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 163px">
                                                            &nbsp;Reason for Hold:<span style="font-size: 9pt; color: #ff0000">*</span></td>
                                                        <td>
                                                            <asp:TextBox ID="txtModuleOnHoldReason" runat="server" CssClass="TxtBox" Width="180px"
                                                                MaxLength="300"></asp:TextBox></td>
                                                    </tr>
                                                    <tr bgcolor="Honeydew">
                                                        <td colspan="2" align="center">
                                                            <a class="link1" href="#" onclick="javascript:validSaveItem_Mhold();"><strong>Submit</strong></a>
                                                            &nbsp; <a class="link1" href="#" onclick="javascript:closeModalArtMHold();"><strong>Cancel</strong></a>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                </td></tr>
                            </table>
                        </div>
                        <div class="content" id="tabProjectEvents" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px">
                                        <img id="Img7" align="absmiddle" src="images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Logged Events"></asp:Label></td>
                                    <td class="dpJobGreenHeader">
                                        <asp:ImageButton ID="imgbtnEventExport" ImageUrl="~/images/tools/j_excel.png" runat="server"
                                            ToolTip="Export Excel" OnClick="imgbtnEventExport_Click" />
                                            </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <strong>Project: <a id="aEvents" href="#" onclick="javascript:showhideDiv(this,'divEvents');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6">
                                        <div id="divEvents" style="display: block;">
                                            <asp:GridView ID="gvEvents" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Stage">
                                                        <ItemTemplate>
                                                            <%# Eval("job_stage_name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                            <%# Eval("task_name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time">
                                                        <ItemTemplate>
                                                            <%# Eval("start_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time">
                                                        <ItemTemplate>
                                                            <%# Eval("end_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration">
                                                        <ItemTemplate>
                                                            <%# Eval("total_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="font-size: 8pt; font-family: Verdana">
                                    <td colspan="6" style="width: 100px">
                                        <strong>Module: <a id="aEvents1" href="#" onclick="javascript:showhideDiv(this,'divEvents1');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        <div id="divEvents1" style="display: block;">
                                            <asp:GridView ID="gvEvents1" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Job">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Stage">
                                                        <ItemTemplate>
                                                            <%# Eval("job_stage_name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task">
                                                        <ItemTemplate>
                                                            <%# Eval("task_name")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Start Time">
                                                        <ItemTemplate>
                                                            <%# Eval("start_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="End Time">
                                                        <ItemTemplate>
                                                            <%# Eval("end_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration">
                                                        <ItemTemplate>
                                                            <%# Eval("total_time")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="content" id="tabComments" runat="server">
                            <table width="100%" cellpadding="2" cellspacing="0">
                                <tr>
                                    <td class="dpJobGreenHeader" style="height: 32px">
                                        <img id="Img6" align="absmiddle" src="images/tools/comment.png" runat="server" />
                                        <asp:Label ID="lblCommentsHeader" runat="server" Text="Comments History"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Project: <a id="alinkcomments" href="#" onclick="javascript:showhideDiv(this,'divCommHistory');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divCommHistory" style="display: block;">
                                            <asp:GridView ID="gvCommHistory" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created by">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <strong>Module: <a id="alinkcomments1" href="#" onclick="javascript:showhideDiv(this,'divCommHistory1');"
                                            class="link1">(Hide)</a></strong></td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="divCommHistory1" style="display: block;">
                                            <asp:GridView ID="gvCommHistory1" runat="server" AutoGenerateColumns="False" CssClass="lightbackground"
                                                Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <%# Eval("name") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Comments">
                                                        <ItemTemplate>
                                                            <%# Eval("comments")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Created by">
                                                        <ItemTemplate>
                                                            <%# Eval("fname")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <div align="center" style="width: 100%; height: 20px; font-weight: bold">
                                                        No records found.</div>
                                                </EmptyDataTemplate>
                                                <HeaderStyle CssClass="darkbackground" />
                                                <AlternatingRowStyle CssClass="dullbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>                        
                        
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

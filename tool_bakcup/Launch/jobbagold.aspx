<%@ page language="C#" autoeventwireup="true" inherits="jobbag, App_Web_opij0lkt" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="default.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/javascript">
        function validateFn(source,args)
        {
            var elem=document.getElementById('Txtjobno').value;
            if(isNaN(elem))
                args.IsValid=false;
            else
                args.IsValid=true;
        }
        function Navigationfn(str,objid1,objid2,divid)
        {
            switch(str)
            {
                case 'show':
                    document.getElementById(divid).style.display='';
                    document.getElementById(objid1).style.display='none';
                    document.getElementById(objid2).style.display='';
                    break;
                case 'notshow':
                    document.getElementById(divid).style.display='none';
                    document.getElementById(objid1).style.display='none';
                    document.getElementById(objid2).style.display='';
                    break;
            }
                
            
        }
        function printdocument()
        {
            window.parent.frames["right"].focus();
            window.print();
        }
    </script>
    <style type="text/css">
    @media print
    {
	    .NotPrintDiv { display: none; }
    }
    table{
    vertical-align:top;
    }
   div{
    vertical-align:top;
    }

</style>


</head>
<body>
    <form id="form1" runat="server">
    <div id="divheader" runat="Server" class="dptitle NotPrintDiv">Job Bag</div>
    <div id="getjobnodiv" runat="server" class="NotPrintDiv" >
        <table width="550px" align="center" cellpadding="2" cellspacing="2" class="bordertable">
            <tr>
                <td>
                    Job Type:
                </td>
                <td>
                    <asp:DropDownList ID="DDLjobtype" runat="server">
                        <asp:ListItem Text="Issue" Value="6" Selected="true"></asp:ListItem>
                        <asp:ListItem Text="Article" Value="5"></asp:ListItem>
                        <asp:ListItem Text="Book" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Chapter" Value="7"></asp:ListItem>
                        <asp:ListItem Text="Project" Value="4"></asp:ListItem>
                        <asp:ListItem Text="Module" Value="8"></asp:ListItem>
                    </asp:DropDownList>  
                </td>
                <td>Job No.:</td>
                <td ><asp:TextBox ID="Txtjobno" Text="" runat="server"></asp:TextBox><asp:CustomValidator ID="vjobno" runat="server" ControlToValidate="Txtjobno" ErrorMessage="Enter only Number" ClientValidationFunction="validateFn"></asp:CustomValidator>
                </td>
                <td><asp:Button ID="Btnlognow" Text="Log Now" runat="server" CssClass="dpbutton" OnClick="Btnlognow_Click" /></td>
            </tr>
            
        </table>
    </div>
    <br />
    <div runat="server" id="errMessage" class="errorMsg"></div>
    <div id="jobdetaildiv" runat="server"  style="border:1px solid green; width:610px;">
        <table  width="600px"  cellpadding="2"  cellspacing="2">
            <tr><td><table width="600px"  cellpadding="2"  cellspacing="2" style="border-bottom:1px solid green;">
                <tr ><td align="center" width="550px" class="HeadText">Job Bag</td>
            <td align="right"><a href="#" onclick="parent.right.print();"><img border="0" class="NotPrintDiv" title="Print" src="images/print.jpg" /> </a></td></tr>
            </table></td></tr>
        </table>
        <table>
        <tr><td>
            <div runat="server" id="detaildiv">
                <table width="600px" cellpadding="2"  cellspacing="2" >
                    <tr><td class="captiontext" >Job Number : <asp:Label ID="Lbljobnumber" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                        <td class="captiontext"  align="left" >Job Type : <asp:Label ID="Lbljobtype" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                        <td rowspan="11" valign="top" >
                            <div id="samcopydiv" runat="server">SAM<br />Copy Edit</div>
                        </td>
                    </tr>
                    <tr>
                        <td class="captiontext"><asp:Label ID="NAME1" Text="" CssClass="labeltxt" runat="server" style="font-size:16pt;font-weight:bold"></asp:Label></td>
                        <td class="captiontext">Service Type : <asp:Label CssClass="labeltxt" ID="service_type_name" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="captiontext">Title : <asp:Label ID="TITLE1" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                        <td class="captiontext">TypeSetting Platform : <asp:Label ID="typesetplatform_name" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="captiontext" >Start Page : <asp:Label ID="start_page" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>    
                        <td class="captiontext" >Is_Sam : <asp:Label ID="is_sam" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="captiontext" >End Page : <asp:Label ID="end_page" Text="" CssClass="labeltxt" runat="Server"></asp:Label></td>
                        <td class="captiontext">Is_CopyEdit : <asp:Label ID="is_copyedit" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="captiontext" >ISBN Print : <asp:Label ID="isbn_print" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                        <td class="captiontext">Is_Sensitive : <asp:Label ID="issensitive" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext" >ISBN Online : <asp:Label ID="isbn_online" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                        <td class="captiontext" >Size : <asp:Label ID="size" CssClass="labeltxt" Text="" runat="server"></asp:Label> </td>
                    </tr>
                    <tr><td class="captiontext">Invoice No. : <asp:Label ID="invoice_no" Text="" CssClass="labeltxt" runat="Server"></asp:Label></td>
                    </tr>
                  <%-- <tr> 
                        <td colspan="2" class="HeaderText"  >Contact Details</td>
                   </tr>
                   <tr>
                        <td class="captiontext" >Journal Title : <a id="journallink" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_name" Text="" CssClass="labeltxt" runat="server"></asp:Label></a></td>
                        <td class="captiontext">Production Editor : <asp:Label ID="peditor" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                        <td class="captiontext">Journal Name : <a id="journallink2" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_code" Text="" CssClass="labeltxt" runat="server"></asp:Label></a></td>
                        <td class=" captiontext">Production Manager : <asp:Label ID="pmanager" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                    </tr>--%>
                    
                </table>
            </div>
            <div id="ChildJob" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2" >
                    <tr><td class="captiontext" >Job Number : <asp:Label ID="Lbljob_number1" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                        <td class="captiontext"  align="left" >Job Type : <asp:Label ID="Lbljob_type1" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                        <td rowspan="12"><div style="border:1; border-color:Black;" id="childcopyedit" runat="server">copy Edit</div></td>
                    </tr>
                    <tr><td class="captiontext" ><asp:Label ID="name" CssClass="labeltxt" runat="server" style="font-size:16pt;font-weight:bold" Text=""></asp:Label></td>
                        <td class="captiontext">No. of Pages : <asp:Label ID="print_pages" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext">Title : <asp:Label ID="title" runat="server" CssClass="labeltxt" Text=""></asp:Label></td>
                        <td class="captiontext">Manscript Pages : <asp:Label ID="ms_pages" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext" >Parent Job Name : <a href='' id="linkparentjob" runat="server"><asp:Label ID="Parent_Job_Name" runat="server" CssClass="labeltxt" Text=""></asp:Label></a></td>
                        <td class="captiontext" >No. of Tables : <asp:Label ID="no_tables" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext" >Author : <asp:Label ID="author" runat="server" CssClass="labeltxt" Text=""></asp:Label></td>
                        <td class="captiontext">No. of Authors : <asp:Label ID="no_authors" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext" >Author Email : <asp:Label ID="author_email" runat="server" CssClass="labeltxt" Text=""></asp:Label></td>
                        <td class="captiontext">No. of Equations : <asp:Label ID="no_equations" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext">Is Extra CopyEdit : <asp:Label ID="is_extra_copyedit" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                        <td class="captiontext">No. of Figures : <asp:Label ID="no_figures" CssClass="labeltxt" Text="" runat="server"></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext">DOI Number : <asp:Label ID="doi" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>    
                        <td class="captiontext">Document Item Type : <asp:Label ID="document_item_code" CssClass="labeltxt" runat="server" Text=""></asp:Label></td>    
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <%--<tr> 
                        <td colspan="2" align="left" class="HeaderText"  >Contact Details</td>
                   </tr>
                    <tr><td class="captiontext">Journal Name : <a id="journal_link1" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_name1" runat="server" CssClass="labeltxt" Text=""></asp:Label></a> </td>
                        <td class="captiontext">Production Editor : <asp:Label ID="childpeditor" runat="server" CssClass="labeltxt" Text=""></asp:Label></td>
                    </tr>
                    <tr><td class="captiontext">Journal Code : <a id="journal_link2" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_code1" runat="server" CssClass="labeltxt" Text=""></asp:Label></a></td>
                        <td class="captiontext">Production Manager : <asp:Label ID="childpmanager" runat="server" CssClass="labeltxt" Text=""></asp:Label></td>
                    </tr>--%>
                </table>
            </div>
            <div id="CustomerDetails" >
                <table width="600px" cellpadding="2" cellspacing="2"  >
                <!-- Paren Job-->
                     <tr> 
                        <td colspan="2" class="HeaderText"  >Contact Details</td>
                   </tr>
                   <tr>
                        <td class="captiontext" >Journal Title : <a id="journallink" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_name" Text="" CssClass="labeltxt" runat="server"></asp:Label></a></td>
                        <td class="captiontext"><asp:Label ID="contact1" runat="server"></asp:Label><asp:Label ID="peditor1" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                   </tr>
                   <tr>
                        <td class="captiontext">Journal Name : <a id="journallink2" href="" target="_blank" onclick="window.open(this.href,'JournalInformation','width=400,height=500,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server"><asp:Label ID="journal_code" Text="" CssClass="labeltxt" runat="server"></asp:Label></a></td>
                        <td class=" captiontext"><asp:Label ID="contact2" runat="Server"></asp:Label><asp:Label ID="peditor2" Text="" CssClass="labeltxt" runat="server"></asp:Label></td>
                    </tr>
                    
                    
                    <tr>                
                        <td class="captiontext" >Customer Name : <a href='' target="_blank" onclick="window.open(this.href,'CustomerInformation','width=450,height=600,scrollbars=yes,resizable=0,top=20,left=400');return false;" runat="server" id="customerlink"><asp:Label ID="cust_name" CssClass="labeltxt" Text="cust" runat="server"></asp:Label></a></td>
                   </tr>
                </table>
            </div>
            
            
            
            <div id="commentsdiv" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2" >
                    <tr><td>
                        <div id="commentsheader" align="left">
                            <%--<table class="HeaderText"><tr><td>Comments Details</td>
                                       <td><img id="commentshow" alt="Show Comments"  onclick="Navigationfn('show','commentshow','commenthide','commentdetails')" src="images/Symbol_Add.png" style="cursor:pointer" />
                                           <img  id="commenthide" alt="Hide Comments" onclick="Navigationfn('notshow','commenthide','commentshow','commentdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />
                                   </td></tr> 
                            </table>--%>
                            
                             <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green'>
                             <tr>
                                 <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );" width="100" align="left" height="25">
                                    <b><font color="#000000" face="Verdana" size="2">&nbsp;Comments</font></b>
                                 </td>
                                 <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
                                    <img id="commentshow" alt="Show Comments"  onclick="Navigationfn('show','commentshow','commenthide','commentdetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
                                    <img  id="commenthide" alt="Hide Comments" onclick="Navigationfn('notshow','commenthide','commentshow','commentdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />
                                 </td>
                                 <td width="*">&nbsp;</td>
                             </tr> 
                             </table>
                            
                        </div>
                    </td></tr>
                    <tr>    
                        <td>
                            <div id="commentdetails" >
                                <asp:GridView HeaderStyle-CssClass="darkbackground" ID="commentgv" runat="server" Width="550px" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="Date" DataFormatString ="{0:MM/dd/yyyy}" HtmlEncode="false" DataField="created_on" />
                                        <asp:BoundField HeaderText="Comments"  DataField="comments" />
                                        <asp:BoundField HeaderText="comment Type" DataField="comment_type_name" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div id="jobhistorydiv" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2"   >
                    <tr>
                        <td>
                            <div id="jobhisheader" align="left" >
                               <%-- <table class="HeaderText"><tr><td>Job History</td>
                                <td>
                                    <img id="showdetails" alt="Show Job History Details"   onclick="Navigationfn('show','showdetails','hidedetails','jobhisdetails')" src="images/Symbol_Add.png" style="cursor:pointer;" />
                                    <img  id="hidedetails" alt="Hide Job History" onclick="Navigationfn('notshow','hidedetails','showdetails','jobhisdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />
                                </td></tr>
                                
                                </table>--%>
                                
                                <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green'>
                                    <tr>
                                    <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );" width="100" align="left" height="25">
                                        <b><font color="#000000" face="Verdana" size="2">&nbsp;Job History</font></b>
                                    </td>
					                <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
					                    <img id="showdetails" alt="Show Job History Details"   onclick="Navigationfn('show','showdetails','hidedetails','jobhisdetails')" src="images/Symbol_Add.png" style="cursor:pointer;cursor:pointer;display:none;" />
                                        <img  id="hidedetails" alt="Hide Job History" onclick="Navigationfn('notshow','hidedetails','showdetails','jobhisdetails')" src="images/ih-minus-16x16.gif" />              
					                </td>
					                <td width="*">&nbsp;</td></tr>
				                </table>
                                
                            </div>
                        </td>
                    </tr>
                    <tr><td>
                            <div id="jobhisdetails" >
                                <asp:GridView width="550px"  ID="jobhistroygv" runat="Server" AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground" >
                                    <Columns>
                                        <asp:BoundField HeaderText="Job Stage" DataField="job_stage_name" />
                                        <asp:BoundField HeaderText="Received" DataField="received_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                        <asp:BoundField HeaderText="Due" DataField="due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false"  />
                                        <asp:BoundField HeaderText="Half Due" DataField="half_due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                        <asp:BoundField HeaderText="Despatch" DataField="despatch_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                        <asp:BoundField HeaderText="CATs Due" DataField="cats_due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
                
            </div>
            
            <div id="onholddiv" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2" >
                    <tr><td>
                        <div id="onholdHeader" align="left">
                            <%--<table class="HeaderText">
                            <tr><td>OnHold Details</td>
                                <td><img id="onholdshow" alt="Show Comments"  onclick="Navigationfn('show','onholdshow','onholdhide','onholddetails')" src="images/Symbol_Add.png" style="cursor:pointer" />
                                    <img  id="onholdhide" alt="Hide Comments" onclick="Navigationfn('notshow','onholdhide','onholdshow','onholddetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />
                                </td>
                            </tr>
                                <tr></tr>
                            </table>--%>
                            <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green'>
                            <tr>
                                <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );" width="100" align="left" height="25">
                                    <b><font color="#000000" face="Verdana" size="2">&nbsp;OnHold</font></b>
                                </td>
                                <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
                                    <img id="onholdshow" alt="Show Comments"  onclick="Navigationfn('show','onholdshow','onholdhide','onholddetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
                                    <img  id="onholdhide" alt="Hide Comments" onclick="Navigationfn('notshow','onholdhide','onholdshow','onholddetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />
                                </td>  
                                <td width="*">&nbsp;</td>          
							</tr> 
                            </table>
                            
                        </div>
                    </td></tr>
                    <tr>
                        <td><div id="onholddetails" >
                                <asp:GridView ID="onholdgv" Width="550px" runat="server" HeaderStyle-CssClass="darkbackground" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="Name" DataField="job_stage_name" />
                                        <asp:BoundField HeaderText="Details" DataField="details" />
                                        <asp:BoundField HeaderText="Onhold Type Name" DataField="onhold_type_name" />
                                        <asp:BoundField HeaderText="Start Date" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" DataField="start_date" />
                                        <asp:BoundField HeaderText="End Date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" DataField="end_date" />
                                        <asp:BoundField HeaderText="Creaed By" DataField="empname" />
                                    </Columns>
                                </asp:GridView>
                            </div></td>
                    </tr>
                </table>
                <br />
            </div>
    
                <div id="GraphsDetailsdiv" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2" >
                    <tr>
                        <td>
                            <div id="graphheader" align="left">
                                <%--<table class="HeaderText"><tr><td>Graphs Details</td>
                                           <td><img id="graphshow" alt="Show Graphs"  onclick="Navigationfn('show','graphshow','graphhide','graphdetails')" src="images/Symbol_Add.png" style="cursor:pointer" />
                                               <img  id="graphhide" alt="Hide Graphs" onclick="Navigationfn('notshow','graphhide','graphshow','graphdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />
                                           </td></tr> 
                                </table>--%>
                                
                                 <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green'>
                                 <tr>
                                    <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );" width="100" align="left" height="25">
                                        <b><font color="#000000" face="Verdana" size="2">&nbsp;Grapics</font></b>
                                    </td>
                                    <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
                                        <img id="graphshow" alt="Show Graphs"  onclick="Navigationfn('show','graphshow','graphhide','graphdetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
                                        <img  id="graphhide" alt="Hide Graphs" onclick="Navigationfn('notshow','graphhide','graphshow','graphdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />
                                    </td>
                                    <td width="*">&nbsp;</td>          
								</tr> 
                                </table>
                                
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="graphdetails" >
                                <asp:GridView ID="graphsgv" Width="550px" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground">
                                    <Columns>
                                        <asp:BoundField HeaderText="Graph Name" DataField="graphic_name" />
                                        <asp:BoundField HeaderText="Graph Type Name" DataField="graphic_type_name" />
                                        <asp:BoundField HeaderText="Description" DataField="graphic_desc" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
            
            <div id="ChildJobs" runat="server">
                <table width="600px" cellpadding="2" cellspacing="2" >
                    <tr>
                        <td>
                            <div id="childjobheader" align="left">
                               <%-- <table class="HeaderText">
                                    <tr><td>Child Jobs</td>
                                    <td><img id="childshow" alt="Show Child Jobs"  onclick="Navigationfn('show','childshow','childhide','childjobdetails')" src="images/Symbol_Add.png" style="cursor:pointer" />
                                            <img  id="childhide" alt="Hide Child Jobs" onclick="Navigationfn('notshow','childhide','childshow','childjobdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />            
                                    </td></tr>
                                </table>--%>
                                
                                <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green;'>
			                        <tr>
			                            <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );vertical-align:bottom" width="100" align="left" height="25"><b><font color="#000000" face="Verdana" size="2">&nbsp;Child Jobs</font></b>
			                            </td>
							            <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
							                <img id="childshow" alt="Show Child Jobs"  onclick="Navigationfn('show','childshow','childhide','childjobdetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
										    <img  id="childhide" alt="Hide Child Jobs" onclick="Navigationfn('notshow','childhide','childshow','childjobdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />              
							            </td>
							            <td width="*">&nbsp;</td>
							        </tr>
						        </table>
                                
                            </div>
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="childjobdetails" >
                                <asp:GridView width="550px" ID="childjobgv" runat="server" AutoGenerateColumns="false" HeaderStyle-CssClass="darkbackground">
                                    <Columns>   
                                        <asp:TemplateField HeaderText="Job Name">
                                            <ItemTemplate><a href='<%# "jobbag.aspx?jobid=" + Eval("job_id") + "&jobtypeid=" + Eval("job_type_id") %>'><%# DataBinder.Eval(Container.DataItem,"name") %></a> </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Job Title" DataField="title" />
                                        <asp:BoundField HeaderText="Job Stage" DataField="job_stage_name" />
                                        <asp:BoundField HeaderText="Status" DataField="despatchstatus" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
        </td></tr>
    </table>
    </div>
    

    <br />
	<div style="width:100%;height:150px;border:1px solid green;text-align:center;width:610px">
		<table align='center'>
			<tr><td>&nbsp;</td><td>Initials</td><td>1st Date</td><td>2nd Date</td></tr>
			<tr><td align='left'>Origination</td>
				<td style="border:1px solid green;width:150px;height:30px;">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
			</tr>
			<tr><td align='left'>Pagination</td>
				<td style="border:1px solid green;width:150px;height:30px;">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
			</tr>
			<tr><td align='left'>Quality Control</td>
				<td style="border:1px solid green;width:150px;height:30px;">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
				<td style="border:1px solid green;width:150px">&nbsp;</td>
			</tr>

		</table>

	</div>            
            
    </form>
</body>
</html>

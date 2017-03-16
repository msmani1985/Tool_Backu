<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print_jobbag.aspx.cs" Inherits="Print_jobbag" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Job Bag Page</title>
    <link href="default.css" rel="stylesheet" type="text/css" />
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
    //            if(window.parent.frames==null)
    //                window.focus();
    //            else
    //                window.parent.frames["right"].focus();
    //alert(window.parent.frames["right"]);
    if(window.parent.frames["right"])
    {
        window.parent.frames["right"].print();
    }
    else 
    {
        window.print(); 
    }
    /*
    if(window.parent.frames==null)
        window.print();
    else
        window.parent.frames["right"].print();
        */
    //             document.form1.print();
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

.tdheader
{
text-align:center; 
font-weight:bold;
height=20px;
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
                <td><asp:Button ID="Btnlognow" Text="Submit" runat="server" CssClass="dpbutton" OnClick="Btnlognow_Click" /></td>
            </tr>
            
        </table>
    </div>
    <br />
    <div runat="server" id="errMessage" class="errorMsg"></div>
    <div id="jobdetaildiv" runat="server"   >
    <table width="600px" style="border:solid 1px green;" align="center"><tr><td>
                    <table width="600px"  cellpadding="2"  cellspacing="2" style="border-bottom:1px solid green;">
                    <tr ><td align="center" width="500px" class="HeadText">Job Bag</td>
                    <td align="right" valign="middle"><a href="#" onclick="javascript:printdocument();"><img border="0" class="NotPrintDiv" title="Print" src="images/print.jpg" /> </a>
                    <asp:ImageButton ID="ibtn_email" runat="server" OnClick="ibtn_email_click" ImageUrl="~/images/email.jpg"  />
                    </td></tr>
                    </table>                      
               </td>
           </tr>
           <tr><td>
                   <table  >
                        <tr><td>
                            <div runat="server" id="detaildiv">
                            test
                            </div>
          
                            <div id="DivLabel" style="display:none;" ><%--For Editor and Manager Label --%>
                                <asp:Label ID="contact1" runat="server"></asp:Label>
                                <asp:Label ID="peditor1" Text="" CssClass="labeltxt" runat="server"></asp:Label>
                                <asp:Label ID="contact2" runat="Server"></asp:Label>
                                <asp:Label ID="peditor2" Text="" CssClass="labeltxt" runat="server"></asp:Label>
                            </div>
            
                   
                            <div id="commentsdiv" runat="server" visible=false>
                                <table width="600px" cellpadding="2" cellspacing="2" >
                                    <tr><td>
                                        <div id="commentsheader" align="left">
                                             <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green'>
                                             <tr>
                                                 <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );" width="100" align="left" height="25">
                                                    <b><font color="#000000" face="Verdana" size="2">&nbsp;Comments</font></b>
                                                 </td>
                                                 <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
                                                    <img id="commentshow" alt="Show Comments"  onclick="Navigationfn('show','commentshow','commenthide','commentdetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
                                                    <img id="commenthide" alt="Hide Comments" onclick="Navigationfn('notshow','commenthide','commentshow','commentdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />
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
                                                    <asp:BoundField HeaderText="Job Stage" DataField="STYPENAME" />
                                                    <asp:BoundField HeaderText="Received" DataField="BFIRSTSTARTDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                                    <asp:BoundField HeaderText="Due" DataField="BFIRSTDUEDATE" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false"  />
                                                    <asp:BoundField HeaderText="Half Due"  HtmlEncode="false" />
                                                    <asp:BoundField HeaderText="Despatch" HtmlEncode="false" />
                                                    <%--<asp:BoundField HeaderText="CATs Due" DataField="cats_due_date" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="false" />
                                                    <asp:BoundField HeaderText="Correction" DataField="correction_type" />--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            
                        </div>
            
                        <div id="onholddiv" runat="server" visible=false>
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
    
                        <div id="GraphsDetailsdiv" runat="server" visible=false>
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
                                            
                                            <%--<table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green;'>
			                                    <tr>
			                                        <td  style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );vertical-align:bottom" width="100" align="left" height="25"><b><font color="#000000" face="Verdana" size="2">&nbsp;Articles</font></b>
			                                        </td>
							                        <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
							                            <img id="childshow" alt="Show Child Jobs"  onclick="Navigationfn('show','childshow','childhide','childjobdetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
										                <img  id="childhide" alt="Hide Child Jobs" onclick="Navigationfn('notshow','childhide','childshow','childjobdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />              
							                        </td>
							                        <td width="*">&nbsp;</td>
							                    </tr>
						                    </table>--%>
                                            
                                        </div>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="childjobdetails" >
                                            <asp:GridView width="550px" ID="childjobgv" runat="server" AutoGenerateColumns="False" HeaderStyle-CssClass="darkbackground"
                                             OnRowDataBound="ondatabound">
                                                <Columns>   
                                                    <asp:TemplateField HeaderText="Job Name">
                                                        <ItemTemplate><a id="a_jobname" runat="server" href='<%# "Print_jobbag.aspx?jobid=" + Eval("job_id") + "&jobtypeid=" + Eval("job_type_id") %>'><%# DataBinder.Eval(Container.DataItem,"name") %></a> </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Job Title" DataField="title" />
                                                    <asp:BoundField HeaderText="Job Stage" DataField="job_stage_name" />
                                                    <asp:BoundField HeaderText="Status" DataField="despatchstatus" />
                                                </Columns>
                                                <HeaderStyle CssClass="darkbackground" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                
                            </table>
                        </div>
                        <div id="divEventDetails" visible=false runat="server" >
                            <table width="600px" cellpadding="2" cellspacing="2" >
                                <tr>
                                    <td>
                                        <div id="EventDetailHeader" align="left">
                                           <%-- <table class="HeaderText">
                                                <tr><td>Child Jobs</td>
                                                <td><img id="childshow" alt="Show Child Jobs"  onclick="Navigationfn('show','childshow','childhide','childjobdetails')" src="images/Symbol_Add.png" style="cursor:pointer" />
                                                        <img  id="childhide" alt="Hide Child Jobs" onclick="Navigationfn('notshow','childhide','childshow','childjobdetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;display:none;" />            
                                                </td></tr>
                                            </table>--%>
                                            
                                            <table class="HeaderText" width="100%" cellspacing=0 style='border-bottom:2px solid green;'>
			                                    <tr>
			                                        <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN );vertical-align:bottom" width="100" align="left" height="25"><b><font color="#000000" face="Verdana" size="2">&nbsp;Events</font></b>
			                                        </td>
							                        <td style="background:green; FILTER: progid:DXImageTransform.Microsoft.gradient (GradientType=0, startColorstr=WHITE, endColorstr=GREEN);" width="25" align="left" height="25">
							                            <img id="eventshow" alt="Show Child Jobs"  onclick="Navigationfn('show','eventshow','eventhide','EventDetails')" src="images/Symbol_Add.png" style="cursor:pointer;display:none;" />
										                <img  id="eventhide" alt="Hide Child Jobs" onclick="Navigationfn('notshow','eventhide','eventshow','EventDetails')" src="images/ih-minus-16x16.gif" style="cursor:pointer;" />              
							                        </td>
							                        <td width="*">&nbsp;</td>
							                    </tr>
						                    </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="EventDetails">
                                            <asp:GridView Width="550px" ID="Eventdetailsgv" runat="server" AutoGenerateColumns="true" HeaderStyle-CssClass="darkbackground">
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
        </td></tr>
    </table>
               </td>
           </tr>
    </table>
       <br />
       <table style="border:solid 1px green;" width="615px" cellpadding=0 cellspacing=0 align="center">
       <tr><td height="75px" valign="top" align="left" style="vertical-align:text-top;border-bottom:solid 1px green;">Instruction for Graphics</td></tr>
       <tr><td height="75px" valign="top" align="left" style="vertical-align:text-top;border-bottom:solid 1px green;">Instruction for pagination</td></tr>
       <tr><td height="75px" valign="top" align="left" style="vertical-align:text-top;border-bottom:solid 1px green;">Instruction for QC</td></tr>
       <tr><td>&nbsp;</td></tr>
       <tr><td>
        <table border=1px bordercolor=green cellpadding=0 cellspacing=0 width="615px" ><tr><td class="tdheader">Name</td><td class="tdheader">Process</td><td class="tdheader">Date</td><td class="tdheader">Start Time</td><td class="tdheader">End Time</td><td class="tdheader">Remark</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td height="30px">&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>
        
        </table>
        
       </td></tr>
       </table>
       
    </div>
    </form>
</body>
</html>

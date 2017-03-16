<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" EnableEventValidation="false"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
 <link href="default.css" rel="stylesheet" type="text/css" />   
    
  
    <script language="javascript" type="text/javascript" src="jquery-1.3.2.js">
   $(document).ready(function() {
      $("#divBody").css("display", "none");
    });
</script>
<style type="text/css">
.selector {
    -moz-border-radius: 10px;
    -webkit-border-radius: 10px;
    border-radius: 10px;
    border: 1px solid;
}

</style>

 <script type="text/javascript">
        var gvelem;
        var gvcolor;
     
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
       </script>

   <script src="tabs.js" type="text/javascript"></script>
    <link href="tab.css" rel="stylesheet" type="text/css" />

</head>
<body>
<form id="form1" runat="server">
  <div class="dptitle">
               Feedback</div>
<div>
 
 
   <br />
  
    <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3" style="width: 722px">
                                       <strong></strong></td>
                                       <td style="width: 63px">
                                        <asp:LinkButton ID="lnkHome" runat="server" ForeColor="Navy" OnClick="lnkHome_Click">Home</asp:LinkButton> </td>
                                    <td>
                                        <asp:LinkButton ID="lnkUpload" runat="server" ForeColor="Navy" OnClick="lnkUpload_Click">Upload</asp:LinkButton> </td>
                                    <td>
                                        <asp:LinkButton ID="lnkReport" runat="server" ForeColor="Navy" OnClick="lnkReport_Click">Report</asp:LinkButton> </td>
                                </tr>
                                
                               
                                
                                </table>
                                 <div class="content" id="divUpload" runat="server" visible="false">
        <table>
        <tr>
            <td>
            <span style="color: Red">*</span>Attach Excel file
            </td>
            <td style="width: 211px">
            <asp:FileUpload ID="fileBrowse" runat="server" />
            </td>
            </tr>
            <tr>
            <td style="height: 26px"></td>
            <td style="height: 26px; width: 211px;">
            <asp:Button ID="btnSave" runat="server" Text="Export" OnClick="btnSave_Click" />&nbsp;
            </td>
        </tr>
        </table>
    </div>
    
    <div class="content" id="divReports" runat="server" visible="false">
       <table class="bordertable" width="700">
           <tr>
               <td align="right">
                   Start Date:</td>
               <td>
                   <asp:TextBox ID="Txtsdate" runat="server" Text=""></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /><oimg alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtsdate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /></td>
               <td align="right">
                   End Date:</td>
               <td>
                   <asp:TextBox ID="Txtedate" runat="server"></asp:TextBox>
                   <img alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=Txtedate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" />
               </td>
               <td rowspan="1" valign="middle">
                   <asp:Button ID="BtnSubmit" runat="server" CssClass="dpbutton"
                       Text="View" OnClick="BtnSubmit_Click" /></td>
               <td rowspan="1" valign="middle">
            <asp:ImageButton ID="imgbtnEventExport" runat="server" ImageUrl="~/Excel.png" OnClick="imgbtnEventExport_Click"
            ToolTip="Export Excel" /></td>
           </tr>
       </table>
       <br />
       <div style="overflow:scroll;width:1000px; height:900px">
       <asp:GridView ID="grvrpt" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt">
           <HeaderStyle CssClass="GVFixedHeader" />
           <AlternatingRowStyle BackColor="#F2F2F2" />
            <Columns>
             <asp:boundfield datafield="Upload_Id"  headertext="Id" Visible="false"/>
                     <asp:boundfield datafield="Issue_ID"  headertext="Issue ID"/>
                      <asp:boundfield datafield="Prod_Staff"   headertext="Production Staff"/>
                      <asp:boundfield datafield="Prod_Manager"  headertext="Production Manager"/>
                      <asp:boundfield datafield="Journal"  headertext="Journal"/>
                      <asp:boundfield datafield="Manuscript_Issues"  headertext="Manuscript / Issue"/>
                      <asp:boundfield datafield="Supplier"  headertext="Supplier"/>
                      <asp:boundfield datafield="Freelance_CE"  headertext="(If Freelance CE, please give name)N/A"/>
                      <asp:boundfield datafield="Date_Feedback_Log"   headertext="Date feedback logged"/>
                      <asp:boundfield datafield="Type_Problem"  headertext="Type of problem"/>
                      <asp:boundfield datafield="SOP"  headertext="Severity of problem"/>
                      <asp:boundfield datafield="WorkFlow"  headertext="Workflow"/>
                      <asp:boundfield datafield="Description"  headertext="Description (also identify TS copy-editor, typesetter, collator, other contacts)"/>
                      <asp:boundfield datafield="Shortdetails"  headertext="If repeat incident, give short details and previous tracker ID"/>
                      <asp:boundfield datafield="Problem_type"  headertext="Is the problem type accurate from supplier point of view?"/>
                      <asp:boundfield datafield="Assign_category"  headertext="If not, please assign a category from the list provided If single category, select here. If multiple categories, type categories from this list in the next cell.If 'Other', specify further details in the next cell."/>
                      <asp:boundfield datafield="Multiple_category"   headertext="Specify here for multiple categories or further details e.g. CE - text syle; CE - ref style"/>
                      <asp:boundfield datafield="Problem_occurred"  headertext="Has this problem occurred before in any journal?"/>
                      <asp:boundfield datafield="Root_causes"  headertext="Give root causes.If this is a repeat problem: give root causes of why the previous root cause analysis and corrective action did not prevent the problem from reoccurring (give full details of fresh root cause analysis)."/>
                      <asp:boundfield datafield="Nature_problem_Single"  headertext="Nature of problem If single category, select here. If multiple categories, type categories from this list in the next cell.If 'Other', specify further details in the next cell."/>
                      <asp:boundfield datafield="Nature_problem_Multiple"  headertext="Nature of problem - specify here for multiple categories or further details e.g. Technical; Training"/>
                      <asp:boundfield datafield="Supplier_actions"  headertext="Supplier actions to be taken (give full details, including action to be taken to ensure the same problem will not recur)"/>
                      
                            <asp:boundfield datafield="Completion_date"  headertext="Completion date of corrective action (i.e. when the solution will be fully implemented and live in the workflow, not when work to implement this will be started)"/>
                      <asp:boundfield datafield="Name_role"   headertext="Name and role title of 1 person responsible for monitoring corrective action"/>
                      <asp:boundfield datafield="Is_issue"  headertext="Is issue now  resolved from supplier point of view?"/>
                      <asp:boundfield datafield="interim"  headertext="If not, what is the interim solution?"/>
                      <asp:boundfield datafield="Projected_final"  headertext="Projected final resolution date if applicable (NB - named person responsible should follow up to confirm resolution with T&F Head of Quality Management)"/>
                      <asp:boundfield datafield="Date_RCA"  headertext="Date RCA returned"/>
                      <asp:boundfield datafield="TandF1"  headertext="T&F:Was the supplier's RCA satisfactory?"/>
                      
                
                      <asp:boundfield datafield="TandF2"  headertext="T&F: Do you think their action will stop this happening on this journal?"/>
                      <asp:boundfield datafield="TandF3"  headertext="T&F: Do you think it will stop it happening on other journals with this supplier?"/>
                      <asp:boundfield datafield="TandF4"  headertext="T&F: Sign off or escalate]"/>
                      <asp:boundfield datafield="TandF5"  headertext="T&F: Comments if escalated"/>
                      <asp:boundfield datafield="TandF_Comments1"  headertext="(T&F QM/SM comment if escalated) T&F or supplier actions to resolve problem"/>
                      <asp:boundfield datafield="TandF_Comments2"  headertext="(T&F QM/SM comment if escalated) Any further notes "/>
                      <asp:boundfield datafield="TandF_Comments3"  headertext="(T&F QM/SM comment if escalated) Process or task owner"/>
                      
                      
                    
            </Columns>
       </asp:GridView>
       </div>
       <br />
       
    </div>
                                <br />
                                <br />
 <div id="divHome" runat="server">

 <table class="content" width="100%">
                                <tr class="dpJobGreenHeader">
                                    <td colspan="3">
                                        <img align="absmiddle" src="images/tools/search.png" />&nbsp;<strong>Search Journal</strong></td>
                                </tr>
                                <tr>
                                    <td style="width: 85px">
                                        <strong>Journal</strong></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtSearch" runat="server" Width="318px" style="text-transform:uppercase" CssClass="txtArticleSearch" TabIndex="1"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="dpbutton" OnClick="btnSearch_Click" TabIndex="4" />
                                      
                                    </td>
                                </tr>
                                </table>
 <asp:GridView ID="gvCustomerList" runat="server" OnPageIndexChanging="gvCustomerList_PageIndexChanging"   autogeneratecolumns="false" 
        emptydatatext="No data available." allowpaging="True" Font-Size="9pt"
        pagesize="7" OnRowDataBound="gvCustomerList_RowDataBound" OnRowCommand="gvCustomerList_RowCommand"  DataKeyNames="Upload_Id" OnSelectedIndexChanged="gvCustomerList_SelectedIndexChanged" Width="1361px">
                   <HeaderStyle CssClass="GVFixedHeader" />
                    <AlternatingRowStyle BackColor="#F2F2F2" /> 
                    
                      
                     <Columns>
                       <asp:TemplateField HeaderText="View">
                     <ItemTemplate>
                     <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="View" CommandArgument ="<%((GridviewRow) Container).RowIndex %>"></asp:LinkButton>
                      </ItemTemplate>
                      </asp:TemplateField>
                      <asp:boundfield datafield="Upload_Id"  headertext="ID"/>
                     <asp:boundfield datafield="Issue ID"  headertext="Issue ID"/>
                      <asp:boundfield datafield="Production Staff"   headertext="Production Staff"/>
                      <asp:boundfield datafield="Production Manager"  headertext="Production Manager"/>
                      <asp:boundfield datafield="Journal"  headertext="Journal"/>
                      <asp:boundfield datafield="Manuscript / Issue"  headertext="Manuscript / Issue"/>
                      <asp:boundfield datafield="Supplier"  headertext="Supplier" Visible="False" />
                      <asp:boundfield datafield="Freelance_CE"  headertext="(If Freelance CE, please give name) N/A" Visible="False"/>
                      <asp:boundfield datafield="Date feedback logged"  headertext="Date feedback logged"/>
                      <asp:boundfield datafield="Type of problem"  headertext="Type of problem"/>
                      <asp:boundfield datafield="Severity of problem"  headertext="Severity of problem"/>
                      <asp:boundfield datafield="Workflow"  headertext="Workflow"/>
                      <asp:boundfield datafield="Description (also identify TS copy-editor, typesetter, collator, other contacts)"  headertext="Description (also identify TS copy-editor, typesetter, collator, other contacts)" Visible="False"/>
                      <asp:boundfield datafield="Root_causes"  headertext="Root_causes" Visible="False"/>
                   
                     
                     
                     </Columns>
                </asp:GridView>

<br />
<br />
<br />
 


<div id="divPanel" runat="server">

<ol id="toc">
                        
                            <li id="miTypeSet" runat="server">
                                <asp:LinkButton ID="lnkFeedback" runat="server"   TabIndex="2" OnClick="lnkFeedback_Click">Feedback</asp:LinkButton></li>                            
                            <li id="miClientFeedback" runat="server">
                                <asp:LinkButton ID="lnkResponse" runat="server"   TabIndex="3" OnClick="lnkResponse_Click">Response</asp:LinkButton></li>
                            <li id="miFeedbackUpload" runat="server" visible="false">
                                <asp:LinkButton ID="lnkFeedbackUpload" runat="server"   TabIndex="4" OnClick="lnkFeedbackUpload_Click" >Upload</asp:LinkButton></li>
                            <li id="miReports" runat="server" visible="false">
                            <asp:LinkButton ID="lnkReports" runat="server" TabIndex="5" OnClick="lnkReports_Click" >Reports</asp:LinkButton></li>
                            
                            </ol>
                        <div  class="content" id="divInternal" runat="server" visible="false">
<table>
<tr>
    <td style="text-align: center; width: 932px;">
        <table align="center" border="0" cellpadding="5" cellspacing="2" >
         <tr>
                                    <td colspan="4" class="dpJobGreenHeader" style="width: 839px; height: 9px;">
                                        <asp:Image id="Img7"  src="~/images/tools/events.png" runat="server" />
                                        <asp:Label ID="lblEventsHeader" runat="server" Text="Internal"></asp:Label></td>
                                    <td class="dpJobGreenHeader" style="height: 9px">
                                      
                                            </td>
                                </tr>
            <tr>
                <td class="tdstyle" align="left">
                    Issue ID</td>
                <td class="tdstyle" align="left" >
                    <asp:TextBox ID="txtIssueId" runat="server"  ForeColor="Gray" CssClass="txtBox"  ></asp:TextBox>
                </td>
                <td class="style3">&nbsp;</td>
                <td class="tdstyle" align="left" >
                    (If Freelance CE, please give name) N/A
                </td>
                <td class="tdstyle" align="left">
                    <asp:TextBox ID="txtReference" runat="server"  ForeColor="Gray" CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" >
                   Production Staff
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtProdId" runat="server"  ForeColor="Gray"  CssClass="txtBox"></asp:TextBox>
                </td>
                <td class="style3">&nbsp;</td>
                <td class="tdstyle"  align="left" >
                   Date feedback logged
                </td>
                <td class="tdstyle"  align="left" >
                  <asp:TextBox ID="txtFeedBack" runat="server"  ForeColor="Gray"   CssClass="txtBox" ></asp:TextBox>
                     
                  </td>
            </tr>
            <tr>
                <td class="tdstyle"  align="left" >
                   Production Manager
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtProdMgr" runat="server" ForeColor="Gray" CssClass="txtBox"></asp:TextBox>
                </td>
                <td class="style3" >
                    &nbsp;</td>
                <td class="tdstyle"  align="left" >
                    Type of problem
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtProblem" runat="server"    ForeColor="Gray"  CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdstyle"  align="left" >
                    Journal
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtJournal" runat="server" ForeColor="Gray"   CssClass="txtBox"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="tdstyle"  align="left" >
                    Serverity of Problem
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtServerity" runat="server" ForeColor="Gray"   CssClass="txtBox"> </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdstyle"  align="left" >
                    Manuscript/Issue
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtManuscript" runat="server"  ForeColor="Gray"   CssClass="txtBox"></asp:TextBox>
                </td>
                <td class="style3">
                    &nbsp;</td>
                <td class="tdstyle"  align="left" >
                    WorkFlow
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtWorkFlow" runat="server"   ForeColor="Gray"  CssClass="txtBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdstyle"  align="left" >
                   Supplier
                </td>
                <td class="tdstyle"  align="left" >
                    <asp:TextBox ID="txtSupplier" runat="server"  ForeColor="Gray"  CssClass="txtBox"></asp:TextBox>
                </td>
                <td class="style3" >
                    &nbsp;</td>
                <td class="tdstyle"  align="left" >
                </td>
                <td class="tdstyle" align="left">
                    &nbsp;</td>
            </tr>
        </table>
       </td>
  </tr>
       
    </table>
</div>

                        <div class="content" id="divTypeset" runat="server">
                        <br />
                        <table style="width: 1343px">
                        <tr>
                <td class="tdstyle" align="left" style="height: 55px; width: 92px; font-family: Verdana;">
                    <strong><span style="font-size: 9pt">
                 Description </span></strong>
                </td>
                <td class="tdstyle" align="left" style=" height: 55px; width: 962px;">
                  <%--  <asp:TextBox ID="txtDesc" runat="server"  ForeColor="Gray"  CssClass="txtBox" 
                        TextMode="MultiLine" Width="686px" Height="154px"></asp:TextBox>--%>
                   <div class="selector"> &nbsp;<asp:Label ID="lblDesc" runat="server" Font-Names="Verdana" Font-Size="9pt"></asp:Label></div>
                   <br />
                </td>
            </tr>
                      <tr>
                      <td class="tdstyle" align="left" style="height: 54px; width: 92px;">
                          <strong><span style="font-size: 9pt">
                           Root cause</span></strong></td>
                <td class="tdstyle" align="left" style=" height: 54px; width: 962px;">
                <%--    <asp:TextBox ID="txtRoot_causes" runat="server"  ForeColor="Gray"  CssClass="txtBox" 
                        TextMode="MultiLine" Width="686px" Height="140px"></asp:TextBox>--%>
                   <div class="selector">       &nbsp;<asp:Label ID="lblRoot_causes" runat="server" CssClass="txtBox" Font-Names="Verdana" Font-Size="9pt"></asp:Label></div>
                         
<br />
                </td>
            </tr>  
            
              <tr>
                      <td class="tdstyle" align="left" style="height: 55px; width: 92px;">
                          <strong><span style="font-size: 9pt">Corrective &nbsp;action</span></strong></td>
                <td class="tdstyle" align="left" style="width: 962px; height: 55px">
                    <%--<asp:TextBox ID="txtSupplier_actions" runat="server"  ForeColor="Gray"  CssClass="txtBox" 
                        TextMode="MultiLine" Width="686px" Height="140px"></asp:TextBox>--%>
                <div class="selector">     &nbsp;<asp:Label ID="lblSupplier_actions" runat="server" CssClass="txtBox" Font-Names="Verdana" Font-Size="9pt"></asp:Label>
                </div>
                        

                </td>
            </tr>
                        </table>
                        
                        
                        

</div>
<div class="content" id="divClientFeedback" runat="server">


<table id="tblResponce" runat="server" border="0" cellpadding="5" cellspacing="2" class="boxTable">
    <tr>
        <td align="left" class="tdstyle" colspan="5">
                                
                                  <asp:GridView ID="grvSort" runat="server"  autogeneratecolumns="false" emptydatatext="No data available."  Font-Size="9pt">
                   <HeaderStyle CssClass="GVFixedHeader" />
                    <AlternatingRowStyle BackColor="#F2F2F2" /> 
            
            <Columns>
             <asp:boundfield datafield="Upload_Id"  headertext="ID"/>
                     <asp:boundfield datafield="Issue_ID"  headertext="Issue ID"/>
                      <asp:boundfield datafield="Prod_Staff"   headertext="Production Staff"/>
                      <asp:boundfield datafield="Prod_Manager"  headertext="Production Manager"/>
                      <asp:boundfield datafield="Journal"  headertext="Journal"/>
                      <asp:boundfield datafield="Manuscript_Issues"  headertext="Manuscript / Issue"/>
                      <asp:boundfield datafield="Date_Feedback_Log"  headertext="Date feedback logged"/>
                      <asp:boundfield datafield="SOP"  headertext="Severity of problem"/>
                      
                    
            </Columns>
            
            
            </asp:GridView>
          
          
          
          
        </td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" colspan="5">
        </td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" style="width: 440px">
            <span style="font-size: 9pt"><strong>Description (also identify TS copy-editor, typesetter,
                collator, other contacts)</strong></span></td>
        <td align="left" class="tdstyle" colspan="4">
            <asp:TextBox ID="txtDescription1" runat="server" Height="40px" TextMode="MultiLine" Width="875px"></asp:TextBox></td>
    </tr>






            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                    Is the problem type accurate from supplier point of view ? </strong>
                </td>
                  <td align="left" class="tdstyle" colspan="4" style="height: 32px">
            <asp:DropDownList ID="ddlSupplier_point" runat="server">
                    <asp:ListItem>-- Select --</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                    If not, please assign a category from the list provided
                    If single category, select here.If multiple categories, type categories from this list in the next cell.If 'Other', specify further details in the next cell. </strong>
                </td>
                <td class="tdstyle" align="left" colspan="4">
                    &nbsp;<asp:DropDownList ID="ddlAssign_category" runat="server" Width="150px" AutoPostBack="false">
                        <asp:ListItem >-- Select -- </asp:ListItem>
                        <asp:ListItem >	Positive feedback	</asp:ListItem>
                        <asp:ListItem >	Wrong deliverables	</asp:ListItem>
                        <asp:ListItem>	Typesetter Manuscript Entry	</asp:ListItem>
                        <asp:ListItem>	Pre-editing - style	</asp:ListItem>
                        <asp:ListItem>	Pre-editing - formatting	</asp:ListItem>
                        <asp:ListItem>	Pre-editing - application of rules	</asp:ListItem>
                        <asp:ListItem>	Pre-editing - CrossRef	</asp:ListItem>
                        <asp:ListItem>	Pre-editing - other	</asp:ListItem>
                        <asp:ListItem >	CE - text style	</asp:ListItem>
                        <asp:ListItem>	CE - ref style	</asp:ListItem>
                        <asp:ListItem >	CE - language / spelling / grammar	</asp:ListItem>
                        <asp:ListItem>	CE - introduction of errors	</asp:ListItem>
                        <asp:ListItem>	CE - unnecessary editing	</asp:ListItem>
                        <asp:ListItem>	CE - not following special instructions	</asp:ListItem>
                        <asp:ListItem >	CE - timeliness	</asp:ListItem>
                        <asp:ListItem >	CE - other	</asp:ListItem>
                        <asp:ListItem >	TS - formatting	</asp:ListItem>
                        <asp:ListItem >	TS - style / laylout / template	</asp:ListItem>
                        <asp:ListItem >	TS - pagination / composition	</asp:ListItem>
                        <asp:ListItem >	TS - not following special instructions	</asp:ListItem>
                        <asp:ListItem >	TS - covers	</asp:ListItem>
                        <asp:ListItem >	TS - other	</asp:ListItem>
                        <asp:ListItem >	AQs - language / typos / grammar	</asp:ListItem>
                        <asp:ListItem >	AQs - unnecessary queries	</asp:ListItem>
                        <asp:ListItem >	AQs - formatting	</asp:ListItem>
                        <asp:ListItem >	AQs - unclear or insufficient info	</asp:ListItem>
                        <asp:ListItem >	AQs - other	</asp:ListItem>
                        <asp:ListItem >	SAM collation - style	</asp:ListItem>
                        <asp:ListItem >	SAM collation - inaccuracy	</asp:ListItem>
                        <asp:ListItem >	SAM collation - introduction of error	</asp:ListItem>
                        <asp:ListItem >	SAM collation - other	</asp:ListItem>
                        <asp:ListItem >	Communication - with author	</asp:ListItem>
                        <asp:ListItem >	Communication - with PE	</asp:ListItem>
                        <asp:ListItem >	Communication - other	</asp:ListItem>
                        <asp:ListItem >	FPM - project management	</asp:ListItem>
                        <asp:ListItem >	FPM - other	</asp:ListItem>
                        <asp:ListItem >	Printing - covers	</asp:ListItem>
                        <asp:ListItem >	Printing - colour	</asp:ListItem>
                        <asp:ListItem >	Printing - coverboard / paper stock	</asp:ListItem>
                        <asp:ListItem >	Printing - other	</asp:ListItem>
                        <asp:ListItem >	Validation - missing / incorrect special issue title	</asp:ListItem>
                        <asp:ListItem >	Validation - missing / incomplete files	</asp:ListItem>
                        <asp:ListItem >	Validation - incorrect file naming	</asp:ListItem>
                        <asp:ListItem >	Validation - missing / incorrect related article data	</asp:ListItem>
                        <asp:ListItem >	Validation - missing / incorrect supplementary files data	</asp:ListItem>
                        <asp:ListItem >	Validation - XML validation error	</asp:ListItem>
                        <asp:ListItem >	Validation - other	</asp:ListItem>
                        <asp:ListItem >	Dispatch - wrong address	</asp:ListItem>
                        <asp:ListItem >	Dispatch - wrong item	</asp:ListItem>
                        <asp:ListItem >	Dispatch - delays	</asp:ListItem>
                        <asp:ListItem >	Dispatch - other	</asp:ListItem>
                        <asp:ListItem >	Warehouse 	</asp:ListItem>
                        <asp:ListItem >	Invoices - incorrect coder / authoriser	</asp:ListItem>
                        <asp:ListItem >	Invoices - incorrect journal / amount	</asp:ListItem>
                        <asp:ListItem >	Invoices - non-processing of invoices	</asp:ListItem>
                        <asp:ListItem >	Invoices - other	</asp:ListItem>
                        <asp:ListItem >	Customer services - response time	</asp:ListItem>
                        <asp:ListItem >	Customer services - account settings	</asp:ListItem>
                        <asp:ListItem>	Customer services - other	</asp:ListItem>
                        <asp:ListItem >	Other	</asp:ListItem>

                        
                    </asp:DropDownList>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                    Specify here for multiple categories or further details e.g. CE - text syle; CE - ref style </strong>
                </td>
                <td class="tdstyle" align="left" colspan="4">
                    <asp:TextBox ID="txtmultiplecategories" runat="server" CssClass="txtBox"></asp:TextBox>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
            Has this problem occurred before in any journal? </strong>
                </td>
              <td align="left" class="tdstyle" colspan="4" style="height: 32px">
            <asp:DropDownList ID="ddlproblem_occurred" runat="server">
                    <asp:ListItem>-- Select --</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                    Give root causes.If this is a repeat problem: </strong>
                </td>
                <td class="tdstyle" align="left" colspan="4">
                    <asp:TextBox ID="txtroot_causes1" ToolTip ="Give root causes.If this is a repeat problem: give root causes of why the previous root cause analysis and corrective action did not prevent the problem from reoccurring (give full details of fresh root cause analysis)." runat="server" CssClass="txtBox" Height="40px" TextMode="MultiLine" Width="875px"></asp:TextBox>
                    &nbsp;&nbsp;</td>
            </tr>
    <tr>
        <td align="left" class="tdstyle" style="height: 40px; width: 440px;">
            <br />
            <strong>Nature of problem If single category, select here.If multiple categories, type
                categories from this list in the next cell.If 'Other', specify further details in
                the next cell. </strong>
        </td>
        <td align="left" class="tdstyle" style="height: 40px" colspan="4"><asp:DropDownList ID="ddlNature_problem_Single" runat="server" Width="150px" AutoPostBack="false">
            <asp:ListItem>    -- Select --  </asp:ListItem>
            <asp:ListItem>Technical</asp:ListItem>
            <asp:ListItem>Insufficient / incorrect T&F info</asp:ListItem>
            <asp:ListItem>Procedural</asp:ListItem>
            <asp:ListItem>Training</asp:ListItem>
            <asp:ListItem>Other</asp:ListItem>
        </asp:DropDownList></td>
    </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                  Nature of problem - specify here for multiple categories or further details e.g. Technical; Training
                    <br />
                    </strong>
                </td>
                <td class="tdstyle" align="left" colspan="4">
                    <asp:TextBox ID="txtSingleCategory" runat="server" CssClass="txtBox" ></asp:TextBox>
                    &nbsp;&nbsp;</td>
            </tr>
            <tr>
                <td class="tdstyle" align="left" style="width: 440px">
                    <strong>
                    Supplier actions to be taken (give full details, including action to be taken to ensure the same problem will not recur) </strong>
       </td>
                <td class="tdstyle" align="left" colspan="4">
                    &nbsp;<asp:TextBox ID="txtSupplier_actions1" runat="server" CssClass="txtBox" Height="40px" TextMode="MultiLine" Width="875px"></asp:TextBox>
                    &nbsp;</td>
            </tr>
    <tr>
        <td align="left" class="tdstyle" style="width: 440px">
            <strong>
                    Completion date of corrective action (i.e. when the solution will be fully implemented and live in the workflow, not when work to implement this will be started) </strong>
        </td>
        <td align="left" class="tdstyle" colspan="4">
            <asp:TextBox ID="txtCompletionDate" runat="server" CssClass="txtBox"></asp:TextBox>
            <img id="imgcal" runat="server" alt="Calendar" border="0" height="20" onclick="javascript:calendar_window=window.open('calendar.aspx?formname=txtCompletionDate','calendar_window','width=180,height=170,left=450,top=360,toolbars=no,scrollbars=yes,status=no,resizable=no');calendar_window.focus()"
                       src="images/Calendar.jpg" style="cursor: pointer; border-top-style: none; border-right-style: none;
                       border-left-style: none; border-bottom-style: none" /></td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" style="width: 440px">
            <strong>
                    Name and role title of 1 person responsible for monitoring corrective action
            </strong>
        </td>
        <td align="left" class="tdstyle" colspan="4">
             
            
            
            <asp:DropDownList ID="ddlName_role_title" runat="server" Width="150px" AutoPostBack="false">
            <asp:ListItem>    -- Select --  </asp:ListItem>
            <asp:ListItem Text="Soundar, Project Manager">Soundar, Project Manager</asp:ListItem>
            <asp:ListItem Text="Shanthi, Copyediting Manager">Shanthi, Copyediting Manager</asp:ListItem>
          
        </asp:DropDownList>
            
            </td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" style="height: 32px; width: 440px;">
            <strong>
                 Is issue now  resolved from supplier point of view? </strong>
        </td>
        <td align="left" class="tdstyle" colspan="4" style="height: 32px">
            <asp:DropDownList ID="ddlIs_issue" runat="server" AutoPostBack="false">
                    <asp:ListItem>-- Select --</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                    </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" style="height: 32px; width: 440px;">
            <strong>
                  If not, what is the interim solution? </strong>
        </td>
        <td align="left" class="tdstyle" colspan="4" style="height: 32px">
            <asp:TextBox ID="txtinterim" runat="server" CssClass="txtBox"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left" class="tdstyle" style="height: 32px; width: 440px;">
            <strong>
                  Projected final resolution date if applicable
                        (NB - named person responsible should follow up to confirm resolution with T&F Head of Quality Management </strong>
        </td>
        <td align="left" class="tdstyle" colspan="4" style="height: 32px">
                    <asp:TextBox ID="txtFinalResolution" runat="server" CssClass="txtBox"></asp:TextBox></td>
    </tr>
            </table>
           <div align="center">  <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click"  />
               <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" /></div>

<br />
<br />
</div>



  

   

</div>
    </div>                           
<div id="divSubpanel" runat="server">

<asp:GridView ID="grdSublist" runat="server"  autogeneratecolumns="false" 
        emptydatatext="No data available." allowpaging="True" Font-Size="9pt"
        pagesize="20"  DataKeyNames="Upload_Id"  Width="1361px" OnPageIndexChanging="grdSublist_PageIndexChanging" OnRowCommand="grdSublist_RowCommand" OnRowDataBound="grdSublist_RowDataBound">
                   <HeaderStyle CssClass="GVFixedHeader" />
                    <AlternatingRowStyle BackColor="#F2F2F2" /> 
                    
                      
                     <Columns>
                       <asp:TemplateField HeaderText="View">
                     <ItemTemplate>
                     <asp:LinkButton ID="lnkView" runat="server" Text="View" CommandName="View" CommandArgument ="<%((GridviewRow) Container).RowIndex %>"></asp:LinkButton>
                      </ItemTemplate>
                      </asp:TemplateField>
                      <asp:boundfield datafield="Upload_Id"  headertext="ID"/>
                     <asp:boundfield datafield="Issue ID"  headertext="Issue ID"/>
                      <asp:boundfield datafield="Production Staff"   headertext="Production Staff"/>
                      <asp:boundfield datafield="Production Manager"  headertext="Production Manager"/>
                      <asp:boundfield datafield="Journal"  headertext="Journal"/>
                      <asp:boundfield datafield="Manuscript / Issue"  headertext="Manuscript / Issue"/>
                      <asp:boundfield datafield="Supplier"  headertext="Supplier" Visible="False" />
                      <asp:boundfield datafield="Freelance_CE"  headertext="(If Freelance CE, please give name) N/A" Visible="False"/>
                      <asp:boundfield datafield="Date feedback logged"  headertext="Date feedback logged"/>
                      <asp:boundfield datafield="Type of problem"  headertext="Type of problem"/>
                      <asp:boundfield datafield="Severity of problem"  headertext="Severity of problem"/>
                      <asp:boundfield datafield="Workflow"  headertext="Workflow"/>
                      <asp:boundfield datafield="Description (also identify TS copy-editor, typesetter, collator, other contacts)"  headertext="Description (also identify TS copy-editor, typesetter, collator, other contacts)" Visible="False"/>
                      <asp:boundfield datafield="Root_causes"  headertext="Root_causes" Visible="False"/>
                   
                     
                     
                     </Columns>
                </asp:GridView>
</div>
</div>
</form>
</body>
</html>
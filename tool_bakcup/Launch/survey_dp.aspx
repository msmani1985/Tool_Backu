<%@ page language="C#" autoeventwireup="true" inherits="survey_dp, App_Web_opij0lkt" enableeventvalidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<head runat="server">
<link href="default.css" type="text/css" rel="stylesheet" />
    <title>Survey Page</title>
</head>
<body bgcolor=>
    <form id="form1" runat="server">
    <div class="dptitle">
        Employee Satisfaction Survey  
    </div>
    <div align="center" >
        <asp:Wizard ID="wd_survey_dp" runat="server" Width="700px" Height="350px" ActiveStepIndex="0" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" OnNextButtonClick="wd_survey_dp_NextButtonClick" OnPreviousButtonClick="wd_survey_dp_PreviousButtonClick"
         EnableViewState=true OnSideBarButtonClick="wd_survey_dp_SideBarButtonClick" OnFinishButtonClick="wd_survey_dp_FinishButtonClick">
            <WizardSteps>
                <asp:WizardStep ID="WizardStep1" runat="server" Title="Step I">
                <div id="div_step1" runat="server" class="divstyle"></div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="Step II">
                <div id="div_step2" runat="server" class="divstyle"></div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep3" runat="server" Title="Step III">
                <div id="div_step3" runat="server" class="divstyle"></div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep4" runat="server" Title="Step IV">
                <div id="div_step4" runat="server" class="divstyle"></div>
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep5" runat="server" Title="Step V">
                <div id="div_step5" runat="server" class="divstyle"></div>
                </asp:WizardStep>
            </WizardSteps>
            <SideBarButtonStyle ForeColor="Honeydew" Font-Names="Microsoft Sans Serif" Font-Size="Large" />
            <SideBarStyle BackColor="SeaGreen" ForeColor="LightGreen" Width="150px" Wrap="True" />
            <HeaderTemplate><asp:Label ID="lbl_wizard_header" Text="Your Position in the Company" runat="server"></asp:Label></HeaderTemplate>
            <HeaderStyle BackColor="Honeydew" Font-Bold="True" Font-Size="Large" ForeColor="ForestGreen" Height="40px" />
            <StartNextButtonStyle Font-Bold="True" Font-Size="Small" />
            <NavigationButtonStyle BackColor="Honeydew" BorderColor="Green" BorderWidth="2px" />
            <NavigationStyle Height="20px" Font-Bold="True" Font-Size="Small" />
            <StepStyle Font-Bold="True" Font-Size="Medium" />
            <StepPreviousButtonStyle Font-Bold="True" Font-Size="Small" />
            <FinishCompleteButtonStyle Font-Bold="True" Font-Size="Small" />
            <FinishPreviousButtonStyle Font-Bold="True" Font-Size="Small" />
            <StepNextButtonStyle Font-Bold="True" Font-Size="Small" />
        </asp:Wizard>
        
    </div>
    </form>
</body>
</html>

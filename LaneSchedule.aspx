<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="LaneSchedule.aspx.vb" Inherits="Account_LaneSchedule" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
       
           
    <asp:Panel ID="pnlCageRental" runat="server" Visible="false">
   <br />
    <asp:GridView id="gvScheduleErrors" 
	DataSourceID="SqlScheduleErrors" 
	AllowSorting="True" 
	AllowPaging="True" 
	Runat="Server"
	CellPadding="2"
  BorderStyle="solid"
  BorderColor="white"
  BorderWidth="5"
  BackColor="white"
  ForeColor="red"
  HeaderStyle-BackColor="RED"
  HeaderStyle-ForeColor="white"
   	AutoGenerateColumns="False" PageSize="25" width="80%" Font-Bold="True">
   
    
	
    <Columns>
        <asp:BoundField HeaderText="Error" DataField="Error"></asp:BoundField>
              
    </Columns>
        <HeaderStyle HorizontalAlign="Left" />
   </asp:GridView>
        <table>
            <tr>
                <td><asp:Label ID="Label5" text="Facility:" runat="server" Font-Bold="True" 
                Font-Size="Medium"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td>  <asp:DropDownList ID="ddlFacility" runat="server" AutoPostBack="True" Font-Size="Small">
            <asp:ListItem Text="---Select Facility---" Value="0"></asp:ListItem>
            <asp:ListItem Text="Sting Field" Value="1"></asp:ListItem>
            <asp:ListItem Text="Springdale PIT" Value="2"></asp:ListItem>
            <asp:ListItem Text="Viper Field" Value="3"></asp:ListItem>
            <asp:ListItem Text="Tom Sawyer" Value="4"></asp:ListItem>
        </asp:DropDownList>
                </td>
            </tr>
        </table>
         <asp:Label ID="lblSelectFacility" runat="server" Text="" ForeColor="Red"></asp:Label>
          
       
        <br />
        <br />
         <asp:Panel ID="pnlSchedule" runat="server">
        <asp:Label ID="lblCurrentDate" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <div style="float: right">
            <asp:LinkButton ID="btnEditProfile" runat="server">Edit My Profile</asp:LinkButton>
        </div>
        <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" BusinessBeginsHour="6" BusinessEndsHour="23" CellDuration="60" CellWidth="40" DataEndField="end" DataResourceField="resource" DataStartField="start" DataTextField="name" DataValueField="id" Days="1" EventFontSize="11px" EventHeight="20" HeaderFontSize="8pt" HeaderHeight="20" HoverColor="Red" OnEventClick="DayPilotScheduler1_EventClick" NonBusinessBackColor="#FFF4BC">
            <Resources>
                <DayPilot:Resource Name="Lane 1" Value="1" />
                <DayPilot:Resource Name="Lane 2" Value="2" />
                <DayPilot:Resource Name="Lane 3" Value="3" />
                <DayPilot:Resource Name="Instructor" Value="5" />
            </Resources>
        </DayPilot:DayPilotScheduler>

        <DayPilot:DayPilotScheduler ID="DayPilotScheduler2" runat="server" BusinessBeginsHour="6" BusinessEndsHour="23" CellDuration="60" CellWidth="40" DataEndField="end" DataResourceField="resource" DataStartField="start" DataTextField="name" DataValueField="id" Days="1" EventFontSize="11px" EventHeight="20" HeaderFontSize="8pt" HeaderHeight="20" HoverColor="Red" OnEventClick="DayPilotScheduler1_EventClick">
            <Resources>
                <DayPilot:Resource Name="Viper Field" Value="1" />
            
            </Resources>
        </DayPilot:DayPilotScheduler>
        <DayPilot:DayPilotScheduler ID="DayPilotSchedulerSting" runat="server" BusinessBeginsHour="6" BusinessEndsHour="23" CellDuration="60" CellWidth="40" DataEndField="end" DataResourceField="resource" DataStartField="start" DataTextField="name" DataValueField="id" Days="1" EventFontSize="11px" EventHeight="20" HeaderFontSize="8pt" HeaderHeight="20" HoverColor="Red" OnEventClick="DayPilotScheduler1_EventClick" NonBusinessBackColor="#FFF4BC">
            <Resources>
                <DayPilot:Resource Name="Batting Cage" Value="1" />
                <DayPilot:Resource Name="Field" Value="2" />
            </Resources>
        </DayPilot:DayPilotScheduler>
          <DayPilot:DayPilotScheduler ID="DayPilotSchedulerTomSawyer" runat="server" BusinessBeginsHour="6" BusinessEndsHour="23" CellDuration="60" CellWidth="40" DataEndField="end" DataResourceField="resource" DataStartField="start" DataTextField="name" DataValueField="id" Days="1" EventFontSize="11px" EventHeight="20" HeaderFontSize="8pt" HeaderHeight="20" HoverColor="Red" OnEventClick="DayPilotScheduler1_EventClick" NonBusinessBackColor="#FFF4BC">
            <Resources>
                <DayPilot:Resource Name="Field 1" Value="1" />
                <DayPilot:Resource Name="Field 2" Value="2" />
                <DayPilot:Resource Name="Field 3" Value="3" />
            </Resources>
        </DayPilot:DayPilotScheduler>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="Medium" text="Make Reservation"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red"></asp:Label>
    
       
        <table cellspacing="10" style="border-style: groove">
    <tr>
    <td style="font-weight: bold;">Step 1:  Select Date</td>
    <td></td>
    <td style="font-weight: bold;" colspan="2">Step 2: Select Time, Duration, and 
        Lane(s)</td>
        <td></td>
         <td style="font-weight: bold;" colspan="2">Step 3: Click Button</td>
    </tr>
    <tr>
    <td rowspan="5" valign="top" >
     <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar" OnSelectionChanged="Calendar1_SelectionChanged" DayNameFormat="FirstTwoLetters">
                <TodayDayStyle BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"></TodayDayStyle>
                <SelectedDayStyle BackColor="#FBE694" ForeColor="Black" CssClass="selected"></SelectedDayStyle>
                <TitleStyle BackColor="White"></TitleStyle>
                <OtherMonthDayStyle ForeColor="#ACA899"></OtherMonthDayStyle>
            </asp:Calendar>
        <br />
        <br />
        <asp:Label ID="lblRepeat" runat="server" Text="Repeat Weekly:  " Font-Bold="True" Visible="false"></asp:Label>
        <asp:DropDownList ID="ddlRepeat" visible="false" runat="server">
        <asp:ListItem Value="0" Text="No Repeat"></asp:ListItem>
        <asp:ListItem Value="1" Text="1 Week"></asp:ListItem>
        <asp:ListItem Value="2" Text="2 Weeks"></asp:ListItem>
        <asp:ListItem Value="3" Text="3 Weeks"></asp:ListItem>
        <asp:ListItem Value="4" Text="4 Weeks"></asp:ListItem>
        <asp:ListItem Value="5" Text="5 Weeks"></asp:ListItem>
        <asp:ListItem Value="6" Text="6 Weeks"></asp:ListItem>
        <asp:ListItem Value="7" Text="7 Weeks"></asp:ListItem>
        <asp:ListItem Value="8" Text="8 Weeks"></asp:ListItem>
        <asp:ListItem Value="9" Text="9 Weeks"></asp:ListItem>
        <asp:ListItem Value="10" Text="10 Weeks"></asp:ListItem>
        </asp:DropDownList>
    </td>
    <td rowspan="5" width="75"></td>
    <td style="font-weight: bold">Date:</td>
    <td>
        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label>
       
    </td>
     <td rowspan="5" width="75"></td>
     <td rowspan="5">
         <asp:RadioButtonList ID="radDisplay" runat="server">
         <asp:ListItem Text="Team Practice" Value="Team"></asp:ListItem>
         <asp:ListItem Text="Individual Practice" Value="Individual"></asp:ListItem>
         <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
         </asp:RadioButtonList>
         <asp:Label ID="lblOther" runat="server" Text="Other:"></asp:Label>&nbsp;
         <asp:TextBox ID="txtOther" runat="server" Width="88px"></asp:TextBox>
          <br />
         <br />
          <asp:Button ID="btnSubmit" runat="server" Text="Submit Reservation" /></td>
    </tr>
    <tr>
     <td style="font-weight: bold">Start Time:</td>
    <td>
        <asp:DropDownList ID="ddlStartTime" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
     <td style="font-weight: bold">Duration:</td>
    <td>
        <asp:DropDownList ID="ddlDuration" runat="server">
         <asp:ListItem Text="30 min" Value="30"></asp:ListItem>
         <asp:ListItem Text="1 hour" Value="60"></asp:ListItem>
         <asp:ListItem Text="1.5 hours" Value="90"></asp:ListItem>
         </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td style="font-weight: bold" valign="top">Lane(s):</td>
    <td>
        <asp:CheckBoxList ID="cblLanes" runat="server" Width="154px">
        <asp:ListItem Text="Lane 1" Value=1></asp:ListItem>
        <asp:ListItem Text="Lane 2" Value=2></asp:ListItem>
        <asp:ListItem Text="Lane 3" Value=3></asp:ListItem>
        <asp:ListItem Text="Instructor Lane" Value=5 Enabled="false"></asp:ListItem>
        </asp:CheckBoxList>
        </td>
       
       
    </tr>
    
    </table>
            </asp:Panel>
    <br />
        <br />
     <asp:Label ID="Label1" text="My Reservations" runat="server" Font-Bold="True" 
                Font-Size="Medium"></asp:Label>
         <br />
     <asp:Label ID="Label4" text="Remaining Cage Hours:  " runat="server" Font-Bold="True" 
                Font-Size="X-Small"></asp:Label>
        <asp:Label ID="lblCageHours" runat="server" Font-Bold="True" 
                Font-Size="X-Small"></asp:Label>
       <br />
     <asp:Label ID="Label6" text="Remaining Field Hours:  " runat="server" Font-Bold="True" 
                Font-Size="X-Small"></asp:Label>
        <asp:Label ID="lblFieldHours" runat="server" Font-Bold="True" 
                Font-Size="X-Small"></asp:Label>
    <asp:GridView id="gvReservations" 
	DataSourceID="SqlReservations" 
	AllowSorting="True" 
	AllowPaging="True" 
	Runat="Server"
	CellPadding="2"
  BorderStyle="solid"
  BorderColor="white"
  BorderWidth="5"
  BackColor="white"
  ForeColor="black"
  HeaderStyle-BackColor="Black"
  HeaderStyle-ForeColor="white"
  AlternatingRowStyle-BackColor="#EEEEEE"
  AlternatingRowStyle-ForeColor="black"
  AutoGenerateDeleteButton ="true"
	AutoGenerateColumns="False" PageSize="25" width="80%"
    EmptyDataText="You Currently Have No Reservations"
    DataKeyNames="ScheduleHeaderID" EnableViewState="false">
	
    <Columns>
    <asp:BoundField HeaderText="Start" DataField="StartDateTime"></asp:BoundField>
    <asp:BoundField HeaderText="Duration" DataField="Duration"></asp:BoundField>
    <asp:BoundField HeaderText="Facility" DataField="FacilityName"></asp:BoundField>
    <asp:BoundField HeaderText="Lanes" DataField="Lanes"></asp:BoundField>
    <asp:BoundField HeaderText="End" DataField="EndDateTime"></asp:BoundField>
    </Columns>
        <HeaderStyle HorizontalAlign="Left" />
   </asp:GridView>
      </asp:Panel>
      <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                   <asp:Label ID="Label3" text="Switch User: " runat="server" Font-Bold="True" 
                Font-Size="Medium"></asp:Label>
            <asp:DropDownList
                ID="ddlUser" runat="server" DataTextField="users" DataValueField="UserID">
            </asp:DropDownList>
        </asp:Panel>
    <asp:Panel ID="pnlLogin" runat="server">
    <br />
    <br />
    <table width="75%">
    <tr>
    <td align="center" style="font-size: medium">
    <b>You must first login to access PIT scheduling</b>
        <br />
    <br />
    </td>
    </tr>
   <tr>
   <td>
   <table width="100%">
    <tr>
      <td align="center">
          <asp:Label ID="lblLogin" runat="server" Text="eMail:" Font-Size="Small" 
              ForeColor="Black" Font-Bold="True"></asp:Label>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      <input id="txtUserName" type="text" runat="server" style="width: 213px" /></td>
     
   </tr>
   <tr>
      <td align="center">
          <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="Small" 
              ForeColor="Black" Text="Password:"></asp:Label>
      <input id="txtUserPass" type="password" runat="server" style="width: 212px" /></td>
      
   </tr>
   
</table>
   </td>
   </tr>
   <tr>
   <td align="center">
    <asp:Button ID="btnLogin" runat="server" Text="Login" />
   </td>
    </tr>
   <tr>
   <td align="center">
    <asp:Label id="lblMsg" ForeColor="red"  Font-Size="10" runat="server" />
   </td>
   </tr>
    <tr>
   <td align="center">
       <asp:LinkButton ID="lnkForgot" runat="server">Retrieve Password</asp:LinkButton>
   </td>
   </tr>
  </table>
       
       


    </asp:Panel>
    <asp:SqlDataSource ID="SqlScheduleErrors" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlReservations" runat="server" ></asp:SqlDataSource>
</asp:Content>


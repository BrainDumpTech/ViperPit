<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>
<%@ Register Assembly="DayPilot" Namespace="DayPilot.Web.Ui" TagPrefix="DayPilot" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Calendar ID="Calendar1" runat="server" CssClass="calendar" OnSelectionChanged="Calendar1_SelectionChanged" DayNameFormat="FirstTwoLetters">
                <TodayDayStyle BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"></TodayDayStyle>
                <SelectedDayStyle BackColor="#FBE694" ForeColor="Black" CssClass="selected"></SelectedDayStyle>
                <TitleStyle BackColor="White"></TitleStyle>
                <OtherMonthDayStyle ForeColor="#ACA899"></OtherMonthDayStyle>
            </asp:Calendar>
            <br />
               <asp:Label ID="lblCurrentDate" runat="server" Text=""></asp:Label>
    <DayPilot:DayPilotScheduler ID="DayPilotScheduler1" runat="server" 
        HeaderFontSize="8pt" 
        HeaderHeight="20" 
        DataStartField="start" 
        DataEndField="end" 
        DataTextField="name" 
        DataValueField="id" 
        DataResourceField="resource" 
        EventHeight="20"
        EventFontSize="11px" 
        CellDuration="60" 
        CellWidth="40"
        BusinessBeginsHour="9"
        BusinessEndsHour="22"
        OnEventClick="DayPilotScheduler1_EventClick"
        HoverColor="Red"
        Days="1" >
        <Resources>
            <DayPilot:Resource Name="Lane 1" Value="1" />
            <DayPilot:Resource Name="Lane 2" Value="2" />
            <DayPilot:Resource Name="Lane 3" Value="3" />
            <DayPilot:Resource Name="Lane 4" Value="4" />
            <DayPilot:Resource Name="Lane 5" Value="5" />
            
        </Resources>
    </DayPilot:DayPilotScheduler>
    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>

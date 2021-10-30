<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="UserDetail.aspx.vb" Inherits="UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
    <table>
<tr>
<td colspan="5">
    <br />
    <br />
    </td>
</tr>
<tr>
<td colspan="5" 
        style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;"  
        width="100%">User Detail</td>
        </tr>
<tr>
<td>First Name:</td>
<td>
    <asp:TextBox ID="txtFirstName" runat="server" Width="158px"></asp:TextBox></td>
<td style="width: 108px"></td>
<td>Team:</td>
<td>
    <asp:DropDownList ID="ddlTeam" runat="server" DataTextField="Team" 
        DataValueField="TeamID">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>Last Name:</td>
<td>
    <asp:TextBox ID="txtLastName" runat="server" Width="160px"></asp:TextBox></td>
    <td style="width: 108px"></td>
<td>Old Henry Role:</td>
<td>
    <asp:DropDownList ID="ddlRole" runat="server" DataTextField="RoleName" 
        DataValueField="RoleID">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>eMail:</td>
<td>
    <asp:TextBox ID="txtEmail" runat="server" Width="252px"></asp:TextBox></td>
     <td style="width: 108px"></td>
<td>Springdale Role:</td>
<td>
    <asp:DropDownList ID="ddlSpringdaleRole" runat="server" DataTextField="RoleName" 
        DataValueField="RoleID">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>Phone:</td>
<td>
    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox></td>
     <td style="width: 108px"></td>
<td>Viper Field Role:</td>
<td>
    <asp:DropDownList ID="ddlViperFieldRole" runat="server" DataTextField="RoleName" 
        DataValueField="RoleID">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td>Password:</td>
<td>
    <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox></td>
     <td style="width: 108px"></td>
<td>Cage Hours Plan:</td>
<td>
    <asp:DropDownList ID="ddlUnlimitedFlag" runat="server">
        <asp:ListItem Text="Unlimited" Value="Y"></asp:ListItem>
        <asp:ListItem Text="Limited" Value="N"></asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
<tr>
    <td></td>
    <td></td>
     <td style="width: 108px"></td>
<td>Remaining Cage Hours:</td>
<td>
   <asp:TextBox ID="txtCageHours" runat="server" Width="73px"></asp:TextBox>
</td>
</tr>
<tr>
    <td></td>
    <td></td>
     <td style="width: 108px"></td>
<td>Field Hours Plan:</td>
<td>
    <asp:DropDownList ID="ddlFieldUnlimitedFlag" runat="server">
        <asp:ListItem Text="Unlimited" Value="Y"></asp:ListItem>
        <asp:ListItem Text="Limited" Value="N"></asp:ListItem>
    </asp:DropDownList>
</td>
</tr>
        <tr>
    <td></td>
    <td></td>
     <td style="width: 108px"></td>
<td>Remaining Field Hours:</td>
<td>
   <asp:TextBox ID="txtFieldHours" runat="server" Width="73px"></asp:TextBox>
</td>
</tr>
<tr><td colspan="5" align="center">
    <br />
    <br />
    <asp:LinkButton ID="btnSave" runat="server">Save</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:LinkButton ID="btnCancel" runat="server">Cancel</asp:LinkButton>
    </td>
    
</tr>
<tr>
<td colspan="5" align="center">
    <br />
    <br />
    <br />
    <br />
    <asp:LinkButton ID="btnDelete" runat="server">Delete User</asp:LinkButton>
    </td>
</tr>
</table>
</asp:Content>


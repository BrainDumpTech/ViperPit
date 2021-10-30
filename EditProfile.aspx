<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="EditProfile.aspx.vb" Inherits="EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
    <table width="100%">

    <tr><td colspan="2" style="background-color: #BE3018; color: #FFFFFF;">Edit Profile</td></tr>
     <tr>
    <td style="height: 25px"><b>First Name:</b></td>
    <td style="height: 25px">
        <asp:TextBox ID="txtFirstName" runat="server" Visible="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td style="height: 25px"><b>Last Name:</b></td>
    <td style="height: 25px">
        <asp:TextBox ID="txtLastName" runat="server" Visible="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td style="height: 25px"><b>eMail:</b></td>
    <td style="height: 25px">
        <asp:TextBox ID="txtEmail" runat="server" Visible="true" Width="285px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td style="height: 25px"><b>Password:</b></td>
    <td style="height: 25px">
        <asp:TextBox ID="txtPassword" runat="server" Visible="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td style="height: 25px"><b>Phone:</b></td>
    <td style="height: 25px">
        <asp:TextBox ID="txtPhone" runat="server" Visible="true"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td style="height: 25px"><b>Team:</b></td>
    <td style="height: 25px">
        <asp:Label ID="lblTeam" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    
    <tr><td colspan="2"><br/><br/></td></tr>
    <tr>
    <td align="left" colspan="2">
        <asp:Image ID="imgSave" runat="server" ImageUrl="~/Images/icon_save.png" />
        <asp:LinkButton ID="btnSave" runat="server" >Save</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Image ID="imgCancel"  runat="server" ImageUrl="~/Images/icon_cancel.png" />
        <asp:LinkButton ID="btnCancel" runat="server" >Cancel</asp:LinkButton>
    </td>
    </tr>
</table>
</asp:Content>


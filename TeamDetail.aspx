<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="TeamDetail.aspx.vb" Inherits="UserDetail" %>

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
        style="background-color: #BE3018; color: #FFFFFF;" 
        width="100%" style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;" >Team Detail</td>
        </tr>
<tr>
<td>Team Name:</td>
<td>
    <asp:TextBox ID="txtTeamName" runat="server" Width="158px"></asp:TextBox></td>

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
    <asp:LinkButton ID="btnDelete" runat="server">Delete Team</asp:LinkButton>
    </td>
</tr>
</table>
</asp:Content>


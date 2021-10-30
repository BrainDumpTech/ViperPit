<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
    <table width="75%">
    <tr>
    <td align="center" style="font-size: medium">
        <b>
        <br />
        Password Retrieval</b><br />
    <br />
    </td>
    </tr>
   <tr>
   <td>
   <table width="100%">
    <tr>
      <td align="center">
          <asp:Label ID="lblLogin" runat="server" Text="eMail:" Font-Size="Small" ForeColor="Black" Font-Bold="True"></asp:Label>
          &nbsp;&nbsp;
          <asp:TextBox ID="txtEmail" runat="server" Width="245px"></asp:TextBox>
          <br />
          <br />
      </td>
     
   </tr>
   <tr>
   <td align="center">
    <asp:Button ID="btnLogin" runat="server" Text="Email Password" />
   </td>
    </tr>
   <tr>
   <td align="center">
    <asp:Label id="lblMsg" ForeColor="red"  Font-Size="10" runat="server" />
   </td>
   </tr>
   
</table>
</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="TeamList.aspx.vb" Inherits="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">

<table  width="100%">
    <tr><td style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;"  width="100%">Teams</td></tr>
     <tr><td>
    <asp:GridView id="gvUsers" 
	DataSourceID="sqlUsers" 
	AllowSorting="True" 
	AllowPaging="True" 
	Runat="Server"
	CellPadding="2"
  BorderStyle="solid"
  BorderColor="white"
  BorderWidth="5"
  BackColor="white"
  ForeColor="black"
  HeaderStyle-ForeColor="white"
  HeaderStyle-Font-Names="Verdana"
  HeaderStyle-Font-Size="9pt"
  RowStyle-Font-Names="Verdana"
  RowStyle-Font-Size="9pt"
  AlternatingRowStyle-BackColor="#EEEEEE"
  AlternatingRowStyle-ForeColor="black"
	AutoGenerateColumns="False" PageSize="15"  width="100%"
      EmptyDataText="No users meet search criteria">
	
<AlternatingRowStyle BackColor="#EEEEEE" ForeColor="Black"></AlternatingRowStyle>
	
    <Columns>
        <asp:HyperLinkField  DataNavigateUrlFields="TeamID" DataNavigateUrlFormatString="TeamDetail.aspx?ID={0}"  HeaderText="Team" DataTextField="Team"/>
     
    </Columns>
        <HeaderStyle HorizontalAlign="Left" BackColor="Black" Font-Names="Verdana" 
                Font-Size="9pt" ForeColor="White" />

<RowStyle Font-Names="Verdana" Font-Size="9pt"></RowStyle>
   </asp:GridView>
</td></tr>

    <tr><td>
    
    
    <asp:Image ID="imgAddUser" runat="server" ImageUrl="~/Images/addIcon.gif" />
    &nbsp;<asp:LinkButton ID="btnAddUser" runat="server">Add New Team</asp:LinkButton>
     </td>
      </tr>
      
</table>

	 
        <asp:SqlDataSource ID="sqlUsers" runat="server" />
</asp:Content>


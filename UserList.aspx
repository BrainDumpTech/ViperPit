<%@ Page Title="" Language="VB" MasterPageFile="~/ViperMaster.master" AutoEventWireup="false" CodeFile="UserList.aspx.vb" Inherits="UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SampleContent" Runat="Server">
    <table>		
<tr>
<td valign="top" width="300">


<table width="100%">
<tr><td colspan="2" 
        style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;" 
        width="100%">Filters</td></tr>

<tr>
<td valign="top" style="font-weight: bold; font-size: small;">Team:</td>
<td>
    <asp:DropDownList ID="ddlTeams" runat="server" DataTextField="Team" 
        DataValueField="TeamID" AutoPostBack="True">
    </asp:DropDownList>
</td>
</tr>
<tr>
<td><br /><br /></td>
</tr>
<tr><td colspan="2" 
        style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;" >Search</td></tr>
<tr>
<td style="font-weight: bold; font-size: small;">Search Text:&nbsp;&nbsp;
</td><td>
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
</td>
    
</tr>
<tr>
<td colspan="2">
        <asp:Button ID="btnSearch" runat="server" Text="Search" /></td>
</tr>
<tr><td><br /><br /></td></tr>

<tr><td colspan="2" id="tdAdminTasks" runat="server" 
        style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;" >Administrative Tasks</td></tr>       
<tr><td colspan="2">
    
    
    <asp:Image ID="imgAddUser" runat="server" ImageUrl="~/Images/addIcon.gif" />
    &nbsp;<asp:LinkButton ID="btnAddUser" runat="server">Add New User</asp:LinkButton>
     </td>
      </tr>
      <td><br/>
    <br />
    </td>
</tr>
<tr><td colspan="2">
    &nbsp;</td></tr>
        
   
    </table>


</td>
<td valign="top" width="600">

<table  width="100%">
    <tr><td style="background-color: #BE3018; color: #FFFFFF; font-size: 9pt; font-weight: bold;" >Users</td></tr>
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
        <asp:HyperLinkField  DataNavigateUrlFields="UserId" DataNavigateUrlFormatString="UserDetail.aspx?ID={0}"  HeaderText="Last Name" DataTextField="Lastname"/>
        <asp:BoundField HeaderText="First Name" DataField="FirstName" ></asp:BoundField>
        <asp:BoundField HeaderText="email" DataField="Email" ></asp:BoundField>
       <asp:BoundField HeaderText="Unlimited Cage Hours" DataField="UnlimitedHoursFlag" ></asp:BoundField>
        <asp:BoundField HeaderText="Cage Hours" DataField="CageHours" ></asp:BoundField>
        <asp:BoundField HeaderText="Unlimited Field Hours" DataField="UnlimitedFieldHours" ></asp:BoundField>
        <asp:BoundField HeaderText="Field Hours" DataField="FieldHours" ></asp:BoundField>
    </Columns>
        <HeaderStyle HorizontalAlign="Left" BackColor="Black" Font-Names="Verdana" 
                Font-Size="9pt" ForeColor="White" />

<RowStyle Font-Names="Verdana" Font-Size="9pt"></RowStyle>
   </asp:GridView>
</td></tr></table>
</td>



</tr>
</table> 

	 
        <asp:SqlDataSource ID="sqlUsers" runat="server" />
</asp:Content>


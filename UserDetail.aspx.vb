Imports System.Data.SqlClient
Imports System.Data

Partial Class UserDetail
    Inherits System.Web.UI.Page

    Dim m_strDBConnString As String
    Dim m_SqlConn As SqlConnection
    Dim m_strID As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim strSQL As String

        m_strID = Request.QueryString("ID")

        m_strDBConnString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        m_SqlConn = New SqlConnection(m_strDBConnString)
        m_SqlConn.Open()



        If m_strID = "" Then
            PopulateDropdowns()
        Else
            If Not Page.IsPostBack Then
                PopulateDropdowns()
                LoadUserDetail()
            End If
        End If

    End Sub

    Protected Sub LoadUserDetail()
        Dim objCommand As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet


        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "select * from users where userid = " & m_strID

        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(objCommand)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        txtEmail.Text = objRow.Item("email").ToString
        txtFirstName.Text = objRow.Item("firstname").ToString
        txtLastName.Text = objRow.Item("lastname").ToString
        txtPhone.Text = objRow.Item("phone").ToString
        txtPassword.Text = objRow.Item("password").ToString
        txtCageHours.Text = objRow.Item("CageHours").ToString
        txtFieldHours.Text = objRow.Item("FieldHours").ToString

        ddlRole.SelectedValue = objRow.Item("oldhenryroleid").ToString
        ddlSpringdaleRole.SelectedValue = objRow.Item("springdaleroleid").ToString
        ddlViperFieldRole.SelectedValue = objRow.Item("viperfieldroleid").ToString
        ddlTeam.SelectedValue = objRow.Item("teamid").ToString
        ddlUnlimitedFlag.SelectedValue = objRow.Item("UnlimitedHoursFlag").ToString
        ddlFieldUnlimitedFlag.SelectedValue = objRow.Item("UnlimitedFieldHours").ToString

    End Sub

    Private Sub PopulateDropdowns()
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objRow2 As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim objCommand As New SqlCommand

        strSQL = "select * from roles; select * from teams"

        objCommand = New SqlCommand(strSQL, m_SqlConn)
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        objRow2 = objDS.Tables(1).Rows(0)

        ddlRole.DataSource = objDS.Tables(0)
        ddlRole.DataBind()

        ddlSpringdaleRole.DataSource = objDS.Tables(0)
        ddlSpringdaleRole.DataBind()

        ddlViperFieldRole.DataSource = objDS.Tables(0)
        ddlViperFieldRole.DataBind()

        ddlTeam.DataSource = objDS.Tables(1)
        ddlTeam.DataBind()
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("UserList.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCommand As New SqlCommand
        Dim strSQL As String

        objCommand.CommandType = CommandType.Text

        If m_strID = "" Then
            strSQL = "Insert into Users (Lastname, firstname, email, phone, password,teamid, CageHours," & _
                     "UnlimitedHoursFlag, FieldHours, UnlimitedFieldHours, SpringdaleRoleID, ViperFieldRoleID, " & _
                      "OldHenryRoleID) values " & _
                "('" & txtLastName.Text & "','" & txtFirstName.Text & "','" & txtEmail.Text & "','" & _
                txtPhone.Text & "','" & txtPassword.Text & "'," & ddlTeam.SelectedValue & "," & txtCageHours.Text & _
                ",'" & ddlUnlimitedFlag.SelectedValue & "'," & txtFieldHours.Text & ",'" & ddlFieldUnlimitedFlag.SelectedValue & _
                "'," & ddlSpringdaleRole.SelectedValue & "," & ddlViperFieldRole.SelectedValue & "," & ddlRole.SelectedValue & ")"
        Else
            strSQL = "update users set firstname='" & txtFirstName.Text & "', " & _
                "lastname = '" & txtLastName.Text & "', " & _
                "email= '" & txtEmail.Text & "', " & _
                "phone= '" & txtPhone.Text & "', " & _
                "password= '" & txtPassword.Text & "', " & _
                "teamid = " & ddlTeam.SelectedValue & ", " & _
                "CageHours = " & txtCageHours.Text & ", " & _
                "unlimitedhoursflag = '" & ddlUnlimitedFlag.SelectedValue & "', " & _
                "FieldHours = " & txtFieldHours.Text & ", " & _
                "unlimitedfieldhours = '" & ddlFieldUnlimitedFlag.SelectedValue & "', " & _
                "springdaleroleid = " & ddlSpringdaleRole.SelectedValue & ", " & _
                "viperfieldroleid = " & ddlViperFieldRole.SelectedValue & ", " & _
                "oldhenryroleid = " & ddlRole.SelectedValue & " where userid = " & m_strID


        End If

       objCommand.CommandText = strSQL

        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

        Response.Redirect("UserList.aspx")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objCommand As New SqlCommand
        Dim strSQL As String

        objCommand.CommandType = CommandType.Text

        strSQL = "delete from users where userid = " & m_strID

        objCommand.CommandText = strSQL

        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

        Response.Redirect("UserList.aspx")

    End Sub
End Class

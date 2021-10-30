Imports System.Data.SqlClient
Imports System.Data

Partial Class EditProfile
    Inherits System.Web.UI.Page

    Dim m_strViperConnString As String
    Dim m_SqlConn As SqlConnection
    Dim m_strID As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strSQL As String

        m_strID = Request.QueryString("ID")

        m_strViperConnString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        m_SqlConn = New SqlConnection(m_strViperConnString)
        m_SqlConn.Open()

        If Not Page.IsPostBack Then
            LoadUserDetail()
        End If

    End Sub

    Protected Sub LoadUserDetail()
        Dim objCommand As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet


        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "select * from Users, Teams " & _
                                 "where users.TeamID = teams.TeamID " & _
                                 "and UserID = " & m_strID

        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(objCommand)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        txtEmail.Text = objRow.Item("email").ToString
        txtFirstName.Text = objRow.Item("firstname").ToString
        txtLastName.Text = objRow.Item("lastname").ToString
        txtPhone.Text = objRow.Item("phone").ToString
        txtPassword.Text = objRow.Item("password").ToString
        lblTeam.Text = objRow.Item("team").ToString

    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("LaneSchedule.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCommand As New SqlCommand
        Dim strSQL As String


        strSQL = "update users " & _
                 "set FirstName='" & txtFirstName.Text & "'," & _
                 "LastName='" & txtLastName.Text & "'," & _
                 "Email='" & txtEmail.Text & "'," & _
                 "Phone='" & txtPhone.Text & "'," & _
                 "Password='" & txtPassword.Text & "' " & _
                 "where UserID = " & m_strID

        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = strSQL

        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

        Response.Redirect("LaneSchedule.aspx")
    End Sub
End Class

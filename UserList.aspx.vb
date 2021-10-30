Imports System.Data.SqlClient
Imports System.Data


Partial Class UserList
    Inherits System.Web.UI.Page


    Dim m_strDBConnString As String
    Dim m_SqlConn As SqlConnection



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim strSQL As String

        m_strDBConnString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        sqlUsers.ConnectionString = m_strDBConnString

        m_SqlConn = New SqlConnection(m_strDBConnString)
        m_SqlConn.Open()


        If Not Page.IsPostBack Then
            PopulateDropDowns()
            strSQL = "select * from users " & _
                     "order by LastName, firstname"

            sqlUsers.SelectCommand = strSQL
            ViewState.Add("sql", strSQL)
        Else
            strSQL = ViewState("sql")
            sqlUsers.SelectCommand = strSQL
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strSQL As String

        strSQL = "select * from Users " & _
                "where (LastName like '%" & txtSearch.Text & "%' " & _
                "or FirstName like '%" & txtSearch.Text & "%') "

        ViewState.Add("sql", strSQL)
        sqlUsers.SelectCommand = strSQL

        ddlTeams.SelectedValue = 0
    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Response.Redirect("UserDetail.aspx")
    End Sub

    Private Sub PopulateDropDowns()


        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim objCommand As New SqlCommand

        strSQL = "select * from teams union select 0,'--All Teams--' order by team"

        objCommand = New SqlCommand(strSQL, m_SqlConn)
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        Try
            objRow = objDS.Tables(0).Rows(0)
            ddlTeams.DataSource = objDS.Tables(0)
            ddlTeams.DataBind()

        Catch
        End Try

    End Sub

    Protected Sub ddlTeams_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTeams.SelectedIndexChanged
        Dim strSQL As String

        If ddlTeams.SelectedValue = 0 Then
            strSQL = "select * from users " & _
                     "order by LastName, firstname"

        Else
            strSQL = "select * from users " & _
                 "where users.teamid = " & ddlTeams.SelectedValue & _
                 "order by LastName, firstname"
        End If



        sqlUsers.SelectCommand = strSQL
        ViewState.Add("sql", strSQL)
    End Sub
End Class

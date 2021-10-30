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

        Else
            If Not Page.IsPostBack Then

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
        objCommand.CommandText = "select * from teams where teamid = " & m_strID

        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(objCommand)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        txtTeamName.Text = objRow.Item("team").ToString
        

    End Sub

    Protected Function GetNextTeamID() As String
        Dim objCommand As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet


        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "select max(teamid) + 1 as NextID from teams"

        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(objCommand)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        GetNextTeamID = objRow.Item("NextID").ToString


    End Function

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Response.Redirect("TeamList.aspx")
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim objCommand As New SqlCommand
        Dim strSQL As String

        objCommand.CommandType = CommandType.Text

        If m_strID = "" Then


            Dim strNextID As String
            strNextID = GetNextTeamID()

            strSQL = "Insert into Teams (TeamID,Team) values " & _
                "(" & strNextID & ",'" & txtTeamName.Text & "')"


        Else
            strSQL = "update teams set team ='" & txtTeamName.Text & "' " & _
                "where teamid = " & m_strID


        End If

       objCommand.CommandText = strSQL

        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

        Response.Redirect("TeamList.aspx")
    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objCommand As New SqlCommand
        Dim strSQL As String

        objCommand.CommandType = CommandType.Text

        strSQL = "delete from teams where teamid = " & m_strID

        objCommand.CommandText = strSQL

        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

        Response.Redirect("TeamList.aspx")

    End Sub
End Class

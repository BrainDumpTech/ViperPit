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

            strSQL = "select * from Teams " & _
                    "order by Team"

            sqlUsers.SelectCommand = strSQL
            ViewState.Add("sql", strSQL)
        Else
            strSQL = ViewState("sql")
            sqlUsers.SelectCommand = strSQL
        End If
    End Sub

    

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click
        Response.Redirect("TeamDetail.aspx")
    End Sub

    

    
End Class

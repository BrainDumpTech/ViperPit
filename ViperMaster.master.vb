
Partial Class ViperMaster
    Inherits System.Web.UI.MasterPage

    Dim m_strRole As String = ""
    Dim m_strUserID As String = ""


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            m_strRole = Request.Cookies("ViperCookie")("AdminFlag")

        Catch ex As Exception

        End Try

        If m_strRole = "Y" Then

            navigation.Controls.Add(New LiteralControl("<li><a href=""#"">Admin</a> " & _
                                                           "<ul class=""nav-sub"">" & _
                                                           "<li><a href=""userlist.aspx"">Users</a></li>" & _
                                                           "<li><a href=""teamlist.aspx"">Teams</a></li>" & _
                                                           "</ul></li>"))

        End If


        Try
            m_strUserID = Request.Cookies("ViperCookie")("UserId")
        Catch

        End Try

        If m_strUserID <> "" Then
            navigation.Controls.Add(New LiteralControl("<li><a href=""Logout.aspx"">Logout</a></li>"))
        End If

    End Sub
End Class


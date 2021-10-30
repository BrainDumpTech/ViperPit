
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cookieCols As New HttpCookieCollection
        cookieCols = Request.Cookies
        Request.Cookies.Remove("ViperCookie")

        Dim newCookie As HttpCookie = New HttpCookie("ViperCookie")
        newCookie.Values.Add("UserId", "")
        newCookie.Values.Add("FirstName", "")
        newCookie.Values.Add("LastName", "")
        newCookie.Values.Add("Role", "")
        newCookie.Values.Add("TeamID", "")
        newCookie.Values.Add("Team", "")
        newCookie.Values.Add("Email", "")
        newCookie.Values.Add("ShowDate", "")
        Response.Cookies.Add(newCookie)

        Response.Redirect("default.aspx")
    End Sub
End Class

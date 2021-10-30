Imports System.Data.SqlClient
Imports System.Web.Security

Partial Class test
    Inherits System.Web.UI.Page

    Protected Sub btnTest1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTest1.Click
        Try
            Dim OleDbConnection As SqlConnection
            OleDbConnection = New SqlConnection(TextBox1.Text)
            OleDbConnection.Open()
        Catch ex As Exception
            Label1.Text = Err.Description
        End Try
        Label1.Text = "Connection Sucessful"
    End Sub

    Protected Sub ButtbtnTest2on1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtbtnTest2on1.Click
        Try
            Dim OleDbConnection As SqlConnection
            OleDbConnection = New SqlConnection(TextBox2.Text)
            OleDbConnection.Open()
        Catch ex As Exception
            Label2.Text = Err.Description
        End Try
        Label2.Text = "Connection Sucessful"
    End Sub

   
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim msg As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient

        Dim strPlayerName As String
        Dim strEmail As String
        strPlayerName = Request.Cookies("ViperCookie")("FirstName")
        strEmail = Request.Cookies("ViperCookie")("Email")
        Dim addrFrom As New System.Net.Mail.MailAddress("support@viper-pit.org")
        Dim addrTo As New System.Net.Mail.MailAddress(strEmail.ToString)
        Dim strSubject As String = ""
        msg.From = addrFrom
        msg.To.Add(addrTo)
        strSubject = "Viper PIT Reservation Confirmed"
        msg.Subject = strSubject
        msg.IsBodyHtml = True
        msg.Body = "Reservation Confirmed "
        smtp.Host = "relay-hosting.secureserver.net"
        smtp.Port = 25
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.Credentials = New System.Net.NetworkCredential("info@vipers-pit.org", "Vipers$4206")

        Try
            smtp.Send(msg)
        Catch ex As Exception
            Response.Write(Err.Description)
        End Try
    End Sub
End Class

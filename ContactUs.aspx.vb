Imports System.Web.Mail

Partial Class ContactUs
    Inherits System.Web.UI.Page

    Dim m_strName As String
    Dim m_strEmail As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
    End Sub

    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click
        Dim msg As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim strEmail As String
        strEmail = txtEmail.Text

       
        Dim addrFrom As New System.Net.Mail.MailAddress("info@vipers-pit.org")
        Dim addrTo As New System.Net.Mail.MailAddress(strEmail.ToString)
        Dim strSubject As String = ""
        msg.From = addrFrom
        msg.To.Add(addrTo)
        strSubject = txtSubject.Text
        msg.Subject = strSubject
        msg.IsBodyHtml = True
        smtp.Host = "relay-hosting.secureserver.net"
        msg.Body = txtMessage.Text
        'smtp.Host = "smtp.gmail.com"
        'smtp.Port = 25
        smtp.Port = 25
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.Credentials = New System.Net.NetworkCredential("info@vipers-pit.org", "Vipers$4206")
        'smtp.Credentials = New System.Net.NetworkCredential("eddie.hobson@insightbb.com", "4206kheh")
        smtp.Send(msg)
        lblResult.Text = "Message Sent"
    End Sub

    Protected Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        txtSubject.Text = ""
        txtMessage.Text = ""
    End Sub
End Class

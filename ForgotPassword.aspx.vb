Imports System.Data.SqlClient
Imports System.Data

Partial Class ForgotPassword
    Inherits System.Web.UI.Page

    Dim m_strViperConnString As String
    Dim m_SqlConn As New SqlConnection

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Dim msg As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim strEmail As String
        Dim strPassword As String
        Dim objCommand As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet

        strEmail = txtEmail.Text

        objCommand.CommandType = CommandType.Text
        objCommand.CommandText = "select * from users where email = '" & strEmail & "'"
       

        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(objCommand)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        strPassword = objRow.Item("password").ToString

        Dim addrFrom As New System.Net.Mail.MailAddress("info@vipers-pit.org")
        Dim addrTo As New System.Net.Mail.MailAddress(strEmail.ToString)
        Dim strSubject As String = ""
        msg.From = addrFrom
        msg.To.Add(addrTo)
        strSubject = "Viper P.I.T. Password Reminder"
        msg.Subject = strSubject
        msg.IsBodyHtml = True
        smtp.Host = "relay-hosting.secureserver.net"
        msg.Body = "Your password is: " & strPassword
        'smtp.Host = "smtp.gmail.com"
        'smtp.Port = 25
        smtp.Port = 25
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.Credentials = New System.Net.NetworkCredential("info@vipers-pit.org", "Vipers$4206")
        'smtp.Credentials = New System.Net.NetworkCredential("eddie.hobson@insightbb.com", "4206kheh")
        smtp.Send(msg)
        lblMsg.Text = "Password has been sent to email...."
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        m_strViperConnString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        m_SqlConn.ConnectionString = m_strViperConnString
        m_SqlConn.Open()

    End Sub
End Class

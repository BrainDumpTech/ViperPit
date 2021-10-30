Imports System.Data.SqlClient
Imports System.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Events
Imports System



Partial Class Account_LaneSchedule
    Inherits System.Web.UI.Page

    Dim m_strUserID As String = ""
    Dim m_strEmail As String
    Dim m_strRole As String
    Dim m_strFirstName As String
    Dim m_strLastName As String
    Dim m_strTeamID As String
    Dim m_strTeamName As String
    Dim m_strViperConnString As String
    Dim m_SqlConn As New SqlConnection
    Dim strShowDate As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            m_strUserID = Request.Cookies("ViperCookie")("UserId")
        Catch
            m_strUserID = ""
        End Try



        If m_strUserID = "" Then
            pnlCageRental.Visible = False
            pnlLogin.Visible = True
        Else
            pnlLogin.Visible = False
            pnlCageRental.Visible = True

            If ddlFacility.SelectedValue = 0 Then
                pnlSchedule.Visible = False
                lblSelectFacility.Text = "Please select a facility to proceed to scheduling"
            Else
                pnlSchedule.Visible = True
                lblSelectFacility.Text = ""
            End If

            m_strTeamID = Request.Cookies("ViperCookie")("TeamID")
                m_strTeamName = Request.Cookies("ViperCookie")("Team")
                m_strEmail = Request.Cookies("ViperCookie")("Email")
                m_strLastName = Request.Cookies("ViperCookie")("LastName")

                m_strViperConnString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
                m_SqlConn.ConnectionString = m_strViperConnString
                m_SqlConn.Open()

                LoadCageHours()
                LoadFieldHours()
                m_strRole = GetRole()

                If m_strRole = "None" Then
                    lblRepeat.Visible = False
                    ddlRepeat.Visible = False
                    'cblLanes.Items(0).Enabled = False
                    'cblLanes.Items(1).Enabled = False
                    'cblLanes.Items(2).Enabled = False
                    'cblLanes.Items(3).Enabled = False
                    btnSubmit.Enabled = False

                End If

                If m_strRole = "Administrator" Then
                    lblRepeat.Visible = True
                    ddlRepeat.Visible = True
                    'cblLanes.Items(0).Enabled = True
                    'cblLanes.Items(1).Enabled = True
                    'cblLanes.Items(2).Enabled = True
                    'cblLanes.Items(3).Enabled = True
                    btnSubmit.Enabled = True
                End If

                If m_strRole = "Instructor" Then
                    lblRepeat.Visible = True
                    ddlRepeat.Visible = True
                    'cblLanes.Items(0).Enabled = False
                    'cblLanes.Items(1).Enabled = False
                    'cblLanes.Items(2).Enabled = False
                    'cblLanes.Items(3).Enabled = True
                    btnSubmit.Enabled = True
                End If

                If m_strRole = "Coach" Then
                    lblRepeat.Visible = False
                    ddlRepeat.Visible = False
                    'cblLanes.Items(0).Enabled = True
                    'cblLanes.Items(1).Enabled = True
                    'cblLanes.Items(2).Enabled = True
                    'cblLanes.Items(3).Enabled = True
                    btnSubmit.Enabled = True
                End If

                If m_strRole = "Player" Then
                    lblRepeat.Visible = False
                    ddlRepeat.Visible = False
                    'cblLanes.Items(0).Enabled = True
                    'cblLanes.Items(1).Enabled = True
                    'cblLanes.Items(2).Enabled = True
                    'cblLanes.Items(3).Enabled = True
                    btnSubmit.Enabled = True
                End If

            '''''SetRadioDisplay()

            SqlScheduleErrors.ConnectionString = m_strViperConnString
                SqlScheduleErrors.SelectCommand = "select * from ScheduleErrors where UserID = " & m_strUserID

                SqlReservations.ConnectionString = m_strViperConnString
                SqlReservations.SelectCommand = "Select * from ScheduleHeader, Facility where ScheduleHeader.FacilitySysID = Facility.Facilitysysid and UserID = " & m_strUserID & " and enddatetime > getdate() order by StartDateTime"



                strShowDate = ViewState("ShowDate")
                If strShowDate <> "" Then
                DayPilotScheduler1.StartDate = strShowDate
                ''Just added
                DayPilotScheduler2.StartDate = strShowDate
                DayPilotSchedulerSting.StartDate = strShowDate
                DayPilotSchedulerTomSawyer.StartDate = strShowDate
            End If



                lblCurrentDate.Text = "Schedule for " & DayPilotScheduler1.StartDate
                lblDate.Text = DayPilotScheduler1.StartDate


                SetClosedTimes()
                LoadStartTimeDropDown()

            If ddlFacility.SelectedValue = 3 Then
                'cblLanes.Items(0).Selected = True
                'cblLanes.Items(0).Enabled = False
                'cblLanes.Items(1).Selected = False
                'cblLanes.Items(1).Enabled = False
                'cblLanes.Items(2).Selected = False
                'cblLanes.Items(2).Enabled = False
                'cblLanes.Items(3).Selected = False
                'cblLanes.Items(3).Enabled = False
                DayPilotScheduler1.Visible = False
                DayPilotScheduler2.Visible = True
                DayPilotSchedulerSting.Visible = False
                DayPilotSchedulerTomSawyer.Visible = False



                DayPilotScheduler2.TimeRangeSelectedHandling = TimeRangeSelectedHandling.PostBack
                DayPilotScheduler2.EventClickHandling = EventClickHandlingEnum.PostBack

            ElseIf ddlFacility.SelectedValue = 1 Then
                DayPilotScheduler1.Visible = False
                DayPilotScheduler2.Visible = False
                DayPilotSchedulerSting.Visible = True
                DayPilotSchedulerTomSawyer.Visible = False
                DayPilotSchedulerSting.TimeRangeSelectedHandling = TimeRangeSelectedHandling.PostBack
                DayPilotSchedulerSting.EventClickHandling = EventClickHandlingEnum.PostBack
            ElseIf ddlFacility.SelectedValue = 2 Then
                DayPilotScheduler1.Visible = True
                DayPilotScheduler2.Visible = False
                DayPilotSchedulerSting.Visible = False
                DayPilotSchedulerTomSawyer.Visible = False
                DayPilotScheduler1.TimeRangeSelectedHandling = TimeRangeSelectedHandling.PostBack
                DayPilotScheduler1.EventClickHandling = EventClickHandlingEnum.PostBack

            ElseIf ddlFacility.SelectedValue = 4 Then
                DayPilotScheduler1.Visible = False
                DayPilotScheduler2.Visible = False
                DayPilotSchedulerSting.Visible = False
                DayPilotSchedulerTomSawyer.Visible = True
                DayPilotSchedulerTomSawyer.TimeRangeSelectedHandling = TimeRangeSelectedHandling.PostBack
                DayPilotSchedulerTomSawyer.EventClickHandling = EventClickHandlingEnum.PostBack
            End If

            If Not Page.IsPostBack Then
                    LoadStartTimeDropDown()
                    PopulateDropdowns()

                    If ddlFacility.SelectedValue > 0 Then
                        DayPilotScheduler1.DataSource = MakeDataTableAndDisplay()
                    DayPilotScheduler2.DataSource = MakeDataTableAndDisplay()
                    DayPilotSchedulerSting.DataSource = MakeDataTableAndDisplay()
                    DayPilotSchedulerTomSawyer.DataSource = MakeDataTableAndDisplay()
                    DataBind()
                    End If
                End If

            End If





    End Sub

    Function GetTable() As DataTable
        ' Create new DataTable instance.
        Dim table As New DataTable


        ' Create four typed columns in the DataTable.

        table.Columns.Add("start", GetType(DateTime))
        table.Columns.Add("end", GetType(DateTime))
        table.Columns.Add("name", GetType(String))
        table.Columns.Add("id", GetType(String))
        table.Columns.Add("resource", GetType(String))
        ' Add five rows with those columns filled in the DataTable.
        'table.Rows.Add(Convert.ToDateTime("8:00"), Convert.ToDateTime("9:00").AddDays(1), "David", 1, "1")
        'table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now)
        'table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now)
        'table.Rows.Add(21, "Combivent", "Janet", DateTime.Now)
        'table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now)



        Dim con As New SqlConnection
        con.ConnectionString = m_strViperConnString

        Dim cmd As New SqlCommand()

        cmd.CommandText = "select ScheduleID, LaneID, UserID, StartDateTime, EndDateTime from Schedule where startdatetime > dateadd(day, -7, getdate())"
        cmd.Connection = m_SqlConn

        Dim Row1 As DataRow
        Try
            con.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader '(CommandBehavior.SingleRow)
            While reader.Read()
                Row1 = table.NewRow()
                'declaring a new row
                Row1.Item("start") = reader.GetDateTime(0)
                Row1.Item("end") = reader.GetDateTime(1)
                Row1.Item("name") = reader.GetString(2)
                Row1.Item("id") = reader.GetString(3)
                Row1.Item("resource") = reader.GetString(4)
                table.Rows.Add(Row1)

            End While
            reader.Close()
        Finally
            con.Close()
        End Try

        GetTable = table
    End Function

    Private Sub SetRadioDisplay()
        Select Case m_strRole
            Case "Administrator"
                radDisplay.Enabled = True
                radDisplay.SelectedValue = "Team"
                radDisplay.Items(2).Enabled = True
                txtOther.Visible = True
                lblOther.Visible = True

            Case "Coach"
                radDisplay.Enabled = True
                radDisplay.SelectedValue = "Team"
                radDisplay.Items(2).Enabled = False
                txtOther.Visible = False
                lblOther.Visible = False

            Case "Player"
                radDisplay.Enabled = False
                radDisplay.SelectedValue = "Individual"
                radDisplay.Items(2).Enabled = False
                txtOther.Visible = False
                lblOther.Visible = False

            Case "Instructor"
                radDisplay.Enabled = False
                radDisplay.SelectedValue = "Individual"
                radDisplay.Items(2).Enabled = False
                txtOther.Visible = False
                lblOther.Visible = False

            Case "None"
                radDisplay.Enabled = False
                txtOther.Visible = False
                lblOther.Visible = False

        End Select


    End Sub


    Private Function MakeDataTableAndDisplay() As DataTable
        ' Create new DataTable and DataSource objects. 
        Dim table As DataTable = New DataTable()
        Dim sConnectionString As String
        Dim OleDbConnection As SqlConnection
        Dim objCommand As New SqlCommand

        ' Declare DataColumn and DataRow variables. 
        Dim column As DataColumn
        Dim row As DataRow
        Dim view As DataView

        ' Create new DataColumn, set DataType, ColumnName and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.DateTime")
        column.ColumnName = "start"
        table.Columns.Add(column)

        column = New DataColumn()
        column.DataType = System.Type.GetType("System.DateTime")
        column.ColumnName = "end"
        table.Columns.Add(column)

        ' Create second column.
        column = New DataColumn()
        column.DataType = Type.GetType("System.String")
        column.ColumnName = "name"
        table.Columns.Add(column)


        column = New DataColumn()
        column.DataType = Type.GetType("System.String")
        column.ColumnName = "id"
        table.Columns.Add(column)

        column = New DataColumn()
        column.DataType = Type.GetType("System.String")
        column.ColumnName = "resource"
        table.Columns.Add(column)


        'row = table.NewRow()
        'row("start") = Convert.ToDateTime("10/30/2012 15:00")
        'row("end") = Convert.ToDateTime("10/30/2012 17:00")
        'row("id") = 1
        'row("resource") = "3"
        'row("name") = "13U"
        'table.Rows.Add(row)

        sConnectionString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        OleDbConnection = New SqlConnection(sConnectionString)

        objCommand.CommandText = "select ScheduleID, LaneID, StartDateTime, EndDateTime, DisplayText " & _
                                 "from Schedule where startdatetime > dateadd(day, -7, getdate()) " & _
                                 "and facilitysysid = " & ddlFacility.SelectedValue
        objCommand.Connection = OleDbConnection

        Dim Row1 As DataRow
        Try
            OleDbConnection.Open()
            Dim reader As SqlDataReader = objCommand.ExecuteReader '(CommandBehavior.SingleRow)
            While reader.Read()
                Row1 = table.NewRow()
                'declaring a new row
                Row1.Item("start") = reader.Item(2)
                Row1.Item("end") = reader.Item(3)
                Row1.Item("id") = reader.Item(0)
                Row1.Item("resource") = reader.Item(1)
                Row1.Item("name") = reader.Item(4)
               
                table.Rows.Add(Row1)

            End While
            reader.Close()
        Finally
            OleDbConnection.Close()
        End Try
        MakeDataTableAndDisplay = table
    End Function

    Protected Sub DayPilotScheduler1_EventClick(ByVal sender As Object, ByVal e As DayPilot.Web.Ui.Events.EventClickEventArgs) Handles DayPilotScheduler1.EventClick
        'Label1.Text = "Event with ID " + e.Value + " clicked."
    End Sub

    Protected Sub DayPilotScheduler1_TimeRangeSelected(ByVal sender As Object, ByVal e As DayPilot.Web.Ui.Events.TimeRangeSelectedEventArgs) Handles DayPilotScheduler1.TimeRangeSelected
        Dim blValidTime As Boolean = False

        ' Label1.Text = "Time cell starting at " + Convert.ToDateTime(e.Start).TimeOfDay.ToString + " clicked."
        lblDate.Text = Convert.ToDateTime(e.Start).Date

        Try
            ddlStartTime.SelectedValue = Convert.ToDateTime(e.Start).TimeOfDay.ToString
        Catch
            ddlStartTime.SelectedValue = "Select"
        End Try
        ddlDuration.SelectedValue = 60

        SetCageOptionsBasedOnDate()

        'Select Case e.Resource
        '    Case 1
        '        cblLanes.Items(0).Selected = True
        '        cblLanes.Items(1).Selected = False
        '        cblLanes.Items(2).Selected = False
        '        cblLanes.Items(3).Selected = False
        '        If m_strRole = "Instructor" Then
        '            cblLanes.Items(4).Selected = False
        '        End If

        '    Case 2
        '        cblLanes.Items(0).Selected = False
        '        cblLanes.Items(1).Selected = True
        '        cblLanes.Items(2).Selected = False
        '        cblLanes.Items(3).Selected = False
        '        If m_strRole = "Instructor" Then
        '            cblLanes.Items(4).Selected = False
        '        End If

        '    Case 3
        '        cblLanes.Items(0).Selected = False
        '        cblLanes.Items(1).Selected = False
        '        cblLanes.Items(2).Selected = True
        '        cblLanes.Items(3).Selected = False
        '        If m_strRole = "Instructor" Then
        '            cblLanes.Items(4).Selected = False
        '        End If

        '    Case 4
        '        cblLanes.Items(0).Selected = False
        '        cblLanes.Items(1).Selected = False
        '        cblLanes.Items(2).Selected = False
        '        cblLanes.Items(3).Selected = True
        '        If m_strRole = "Instructor" Then
        '            cblLanes.Items(4).Selected = False
        '        End If

        '    Case 5
        '        cblLanes.Items(0).Selected = False
        '        cblLanes.Items(1).Selected = False
        '        cblLanes.Items(2).Selected = False
        '        cblLanes.Items(3).Selected = False
        '        If m_strRole = "Instructor" Then
        '            cblLanes.Items(4).Selected = True
        '        End If

        'End Select

    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        setWeek()
        SetCageOptionsBasedOnDate()

        SqlReservations.ConnectionString = m_strViperConnString
        SqlReservations.SelectCommand = "Select * from ScheduleHeader, Facility where ScheduleHeader.FacilitySysID = Facility.Facilitysysid and UserID = " & m_strUserID & " and enddatetime > getdate() order by StartDateTime"

    End Sub
    Sub setWeek()

        DayPilotScheduler1.StartDate = Calendar1.SelectedDate
        DayPilotScheduler2.StartDate = Calendar1.SelectedDate
        DayPilotSchedulerSting.StartDate = Calendar1.SelectedDate
        DayPilotSchedulerTomSawyer.StartDate = Calendar1.SelectedDate
        lblCurrentDate.Text = "Schedule for " & DayPilotScheduler1.StartDate
        lblDate.Text = DayPilotScheduler1.StartDate


    End Sub

    Private Sub SetClosedTimes()
        DayPilotScheduler1.BusinessBeginsHour = "6"
        DayPilotScheduler1.BusinessEndsHour = "23"

        DayPilotScheduler2.BusinessBeginsHour = "6"
        DayPilotScheduler2.BusinessEndsHour = "23"

        DayPilotSchedulerSting.BusinessBeginsHour = "6"
        DayPilotSchedulerSting.BusinessEndsHour = "23"

        DayPilotSchedulerTomSawyer.BusinessBeginsHour = "6"
        DayPilotSchedulerTomSawyer.BusinessEndsHour = "23"
    End Sub

    Private Sub LoadStartTimeDropDown()

        Select Case m_strRole
            Case "Administrator"
                ddlStartTime.Items.Add(New ListItem("Select", "Select"))
                ddlStartTime.Items.Add(New ListItem("6:00 AM", "06:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 AM", "06:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 AM", "07:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 AM", "07:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 AM", "08:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 AM", "08:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 AM", "09:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 AM", "09:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 AM", "10:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 AM", "10:30:00"))
                ddlStartTime.Items.Add(New ListItem("11:00 AM", "11:00:00"))
                ddlStartTime.Items.Add(New ListItem("11:30 AM", "11:30:00"))
                ddlStartTime.Items.Add(New ListItem("12:00 PM", "12:00:00"))
                ddlStartTime.Items.Add(New ListItem("12:30 PM", "12:30:00"))
                ddlStartTime.Items.Add(New ListItem("1:00 PM", "13:00:00"))
                ddlStartTime.Items.Add(New ListItem("1:30 PM", "13:30:00"))
                ddlStartTime.Items.Add(New ListItem("2:00 PM", "14:00:00"))
                ddlStartTime.Items.Add(New ListItem("2:30 PM", "14:30:00"))
                ddlStartTime.Items.Add(New ListItem("3:00 PM", "15:00:00"))
                ddlStartTime.Items.Add(New ListItem("3:30 PM", "15:30:00"))
                ddlStartTime.Items.Add(New ListItem("4:00 PM", "16:00:00"))
                ddlStartTime.Items.Add(New ListItem("4:30 PM", "16:30:00"))
                ddlStartTime.Items.Add(New ListItem("5:00 PM", "17:00:00"))
                ddlStartTime.Items.Add(New ListItem("5:30 PM", "17:30:00"))
                ddlStartTime.Items.Add(New ListItem("6:00 PM", "18:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 PM", "18:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 PM", "19:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 PM", "19:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 PM", "20:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 PM", "20:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 PM", "21:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 PM", "21:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 PM", "22:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 PM", "22:30:00"))

            Case "Coach"
                ddlStartTime.Items.Add(New ListItem("Select", "Select"))
                ddlStartTime.Items.Add(New ListItem("6:00 AM", "06:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 AM", "06:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 AM", "07:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 AM", "07:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 AM", "08:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 AM", "08:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 AM", "09:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 AM", "09:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 AM", "10:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 AM", "10:30:00"))
                ddlStartTime.Items.Add(New ListItem("11:00 AM", "11:00:00"))
                ddlStartTime.Items.Add(New ListItem("11:30 AM", "11:30:00"))
                ddlStartTime.Items.Add(New ListItem("12:00 PM", "12:00:00"))
                ddlStartTime.Items.Add(New ListItem("12:30 PM", "12:30:00"))
                ddlStartTime.Items.Add(New ListItem("1:00 PM", "13:00:00"))
                ddlStartTime.Items.Add(New ListItem("1:30 PM", "13:30:00"))
                ddlStartTime.Items.Add(New ListItem("2:00 PM", "14:00:00"))
                ddlStartTime.Items.Add(New ListItem("2:30 PM", "14:30:00"))
                ddlStartTime.Items.Add(New ListItem("3:00 PM", "15:00:00"))
                ddlStartTime.Items.Add(New ListItem("3:30 PM", "15:30:00"))
                ddlStartTime.Items.Add(New ListItem("4:00 PM", "16:00:00"))
                ddlStartTime.Items.Add(New ListItem("4:30 PM", "16:30:00"))
                ddlStartTime.Items.Add(New ListItem("5:00 PM", "17:00:00"))
                ddlStartTime.Items.Add(New ListItem("5:30 PM", "17:30:00"))
                ddlStartTime.Items.Add(New ListItem("6:00 PM", "18:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 PM", "18:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 PM", "19:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 PM", "19:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 PM", "20:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 PM", "20:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 PM", "21:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 PM", "21:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 PM", "22:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 PM", "22:30:00"))

            Case "Player"
                ddlStartTime.Items.Add(New ListItem("Select", "Select"))
                ddlStartTime.Items.Add(New ListItem("6:00 AM", "06:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 AM", "06:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 AM", "07:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 AM", "07:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 AM", "08:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 AM", "08:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 AM", "09:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 AM", "09:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 AM", "10:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 AM", "10:30:00"))
                ddlStartTime.Items.Add(New ListItem("11:00 AM", "11:00:00"))
                ddlStartTime.Items.Add(New ListItem("11:30 AM", "11:30:00"))
                ddlStartTime.Items.Add(New ListItem("12:00 PM", "12:00:00"))
                ddlStartTime.Items.Add(New ListItem("12:30 PM", "12:30:00"))
                ddlStartTime.Items.Add(New ListItem("1:00 PM", "13:00:00"))
                ddlStartTime.Items.Add(New ListItem("1:30 PM", "13:30:00"))
                ddlStartTime.Items.Add(New ListItem("2:00 PM", "14:00:00"))
                ddlStartTime.Items.Add(New ListItem("2:30 PM", "14:30:00"))
                ddlStartTime.Items.Add(New ListItem("3:00 PM", "15:00:00"))
                ddlStartTime.Items.Add(New ListItem("3:30 PM", "15:30:00"))
                ddlStartTime.Items.Add(New ListItem("4:00 PM", "16:00:00"))
                ddlStartTime.Items.Add(New ListItem("4:30 PM", "16:30:00"))
                ddlStartTime.Items.Add(New ListItem("5:00 PM", "17:00:00"))
                ddlStartTime.Items.Add(New ListItem("5:30 PM", "17:30:00"))
                ddlStartTime.Items.Add(New ListItem("6:00 PM", "18:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 PM", "18:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 PM", "19:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 PM", "19:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 PM", "20:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 PM", "20:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 PM", "21:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 PM", "21:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 PM", "22:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 PM", "22:30:00"))

            Case "Instructor"
                ddlStartTime.Items.Add(New ListItem("Select", "Select"))
                ddlStartTime.Items.Add(New ListItem("6:00 AM", "06:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 AM", "06:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 AM", "07:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 AM", "07:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 AM", "08:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 AM", "08:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 AM", "09:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 AM", "09:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 AM", "10:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 AM", "10:30:00"))
                ddlStartTime.Items.Add(New ListItem("11:00 AM", "11:00:00"))
                ddlStartTime.Items.Add(New ListItem("11:30 AM", "11:30:00"))
                ddlStartTime.Items.Add(New ListItem("12:00 PM", "12:00:00"))
                ddlStartTime.Items.Add(New ListItem("12:30 PM", "12:30:00"))
                ddlStartTime.Items.Add(New ListItem("1:00 PM", "13:00:00"))
                ddlStartTime.Items.Add(New ListItem("1:30 PM", "13:30:00"))
                ddlStartTime.Items.Add(New ListItem("2:00 PM", "14:00:00"))
                ddlStartTime.Items.Add(New ListItem("2:30 PM", "14:30:00"))
                ddlStartTime.Items.Add(New ListItem("3:00 PM", "15:00:00"))
                ddlStartTime.Items.Add(New ListItem("3:30 PM", "15:30:00"))
                ddlStartTime.Items.Add(New ListItem("4:00 PM", "16:00:00"))
                ddlStartTime.Items.Add(New ListItem("4:30 PM", "16:30:00"))
                ddlStartTime.Items.Add(New ListItem("5:00 PM", "17:00:00"))
                ddlStartTime.Items.Add(New ListItem("5:30 PM", "17:30:00"))
                ddlStartTime.Items.Add(New ListItem("6:00 PM", "18:00:00"))
                ddlStartTime.Items.Add(New ListItem("6:30 PM", "18:30:00"))
                ddlStartTime.Items.Add(New ListItem("7:00 PM", "19:00:00"))
                ddlStartTime.Items.Add(New ListItem("7:30 PM", "19:30:00"))
                ddlStartTime.Items.Add(New ListItem("8:00 PM", "20:00:00"))
                ddlStartTime.Items.Add(New ListItem("8:30 PM", "20:30:00"))
                ddlStartTime.Items.Add(New ListItem("9:00 PM", "21:00:00"))
                ddlStartTime.Items.Add(New ListItem("9:30 PM", "21:30:00"))
                ddlStartTime.Items.Add(New ListItem("10:00 PM", "22:00:00"))
                ddlStartTime.Items.Add(New ListItem("10:30 PM", "22:30:00"))
        End Select



    End Sub

    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click


        Dim strFirstName As String = "eddie.hobson@insightbb.com"
        Dim strLastName As String = "4206kheh"
        Dim strPassword As String
        Dim strTeamID As String
        Dim strTeamName As String
        Dim strEmail As String
        Dim strUserID As String
        Dim strAdminFlag As String
        Dim sConnectionString As String
        Dim OleDbConnection As SqlConnection
        Dim objCommand As New SqlCommand
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim newCookie As HttpCookie = New HttpCookie("ViperCookie")

        strEmail = txtUserName.Value

        'strEmail = "eddie.hobson@insightbb.com"
        'txtUserPass.Value = "4206kheh"

        sConnectionString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
            OleDbConnection = New SqlConnection(sConnectionString)
            OleDbConnection.Open()

        strSQL = "Select * from Users,Teams where email ='" & strEmail & "' and Teams.TeamID=Users.TeamID"

        objCommand = New SqlCommand(strSQL, OleDbConnection)
        objDA = New SqlDataAdapter(strSQL, OleDbConnection)
            objDA.Fill(objDS)
            If objDS.Tables(0).Rows.Count > 0 Then
                objRow = objDS.Tables(0).Rows(0)


                strPassword = objRow.Item("password").ToString
            strUserID = objRow.Item("userid").ToString
            strFirstName = objRow.Item("FirstName").ToString
            strLastName = objRow.Item("LastName").ToString
            strAdminFlag = objRow.Item("SiteAdminFlag").ToString
            strTeamID = objRow.Item("teamid").ToString
            strTeamName = objRow.Item("team").ToString

                If strPassword = txtUserPass.Value Then
                newCookie.Values.Add("UserId", strUserID)
                newCookie.Values.Add("FirstName", strFirstName)
                newCookie.Values.Add("LastName", strLastName)
                newCookie.Values.Add("AdminFlag", strAdminFlag)
                newCookie.Values.Add("TeamID", strTeamID)
                newCookie.Values.Add("Team", strTeamName)
                newCookie.Values.Add("Email", strEmail)
                newCookie.Values.Add("ShowDate", Today)
                Response.Cookies.Add(newCookie)

                m_strUserID = strUserID
                DeleteScheduleErrors()

                Response.Redirect("LaneSchedule.aspx?UserID=" & strUserID)
                Else
                    lblMsg.Text = "Invalid Login"
                    txtUserName.Value = ""
                    txtUserPass.Value = ""
                End If

            Else
                lblMsg.Text = "Invalid Login"
                txtUserName.Value = ""
                txtUserPass.Value = ""
            End If


            OleDbConnection.Close()

      

    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click

        Dim strHeaderID As String
        Dim strLanes As String = ""
        Dim intRepeatInterval As Integer
        Dim strStartDateTime As DateTime
        Dim strDateCheck As DateTime
        Dim strEndDateTime As DateTime
        Dim dblDuration As Double
        Dim intIndex As Integer
        Dim strDisplay As String = ""
        Dim blnScheduleErrorFlag As Boolean = False
        Dim strScheduleError As String = ""
        Dim intLaneCount As Integer = 0
        Dim decDurationInHours As Decimal
        Dim decHoursNeeded As Decimal
        Dim decHoursNeededPerReservation As Decimal

        lblError.Text = ""

        intRepeatInterval = ddlRepeat.SelectedValue
        dblDuration = ddlDuration.SelectedValue

        If ddlStartTime.SelectedValue = "Select" Then
            blnScheduleErrorFlag = True
            strScheduleError = "Please choose start time"
        End If

        If Not blnScheduleErrorFlag = True Then
            If ddlFacility.SelectedValue = 0 Then
                blnScheduleErrorFlag = True
                strScheduleError = "Please choose facility"
            End If
        End If

        If Not blnScheduleErrorFlag = True Then
            strDateCheck = lblDate.Text & " " & ddlStartTime.SelectedValue.ToString

            Select Case m_strRole
                Case "Administrator"
                    If radDisplay.SelectedValue = "Team" Then
                        strDisplay = m_strTeamName
                    ElseIf radDisplay.SelectedValue = "Individual" Then
                        strDisplay = m_strLastName
                    ElseIf radDisplay.SelectedValue = "Other" Then
                        strDisplay = txtOther.Text
                    End If

                Case "Coach"
                    If radDisplay.SelectedValue = "Team" Then
                        strDisplay = m_strTeamName
                    Else
                        strDisplay = m_strLastName
                    End If
                    If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 336 Then
                        blnScheduleErrorFlag = True
                        strScheduleError = "Coaches may not schedule more than 14 days in advance"
                    End If

                Case "Player"
                    strDisplay = m_strLastName
                    If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 48 Then
                        blnScheduleErrorFlag = True
                        strScheduleError = "Players may not schedule more than 48 hours in advance"
                    End If

                Case "Instructor"
                    strDisplay = m_strLastName

            End Select

            'If cblLanes.Items(0).Selected = True Then
            '    strLanes = strLanes & "1 "
            'End If

            'If cblLanes.Items(1).Selected = True Then
            '    strLanes = strLanes & "2 "
            'End If

            'If cblLanes.Items(2).Selected = True Then
            '    strLanes = strLanes & "3 "
            'End If

            'If cblLanes.Items(3).Selected = True Then
            '    strLanes = strLanes & "4 "
            'End If

            'If cblLanes.Items(4).Selected = True Then
            '    strLanes = strLanes & "5 "
            'End If
        End If

        'Verify Cage Hours
        If blnScheduleErrorFlag = False Then
            If radDisplay.SelectedValue = "Team" Then
                If ddlFacility.SelectedValue = 2 Then
                    If cblLanes.Items(0).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If
                    If cblLanes.Items(1).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If
                    If cblLanes.Items(2).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If
                    If cblLanes.Items(3).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If

                    decDurationInHours = ddlDuration.SelectedValue / 60

                    decHoursNeeded = intLaneCount * decDurationInHours

                    If intRepeatInterval > 0 Then
                        decHoursNeededPerReservation = decHoursNeeded
                        decHoursNeeded = decHoursNeeded * intRepeatInterval
                    End If

                    If VerifyCageHours(m_strUserID, decHoursNeeded) = False Then
                        blnScheduleErrorFlag = True
                        strScheduleError = "Not enough availble cage hours to complete reservation.  Hours needed: " & decHoursNeeded
                    End If
                ElseIf ddlFacility.SelectedValue = 1 Then
                    If cblLanes.Items(0).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If
                    If cblLanes.Items(1).Selected = True Then
                        intLaneCount = intLaneCount + 1
                    End If

                    decDurationInHours = ddlDuration.SelectedValue / 60

                    decHoursNeeded = intLaneCount * decDurationInHours

                    If intRepeatInterval > 0 Then
                        decHoursNeededPerReservation = decHoursNeeded
                        decHoursNeeded = decHoursNeeded * intRepeatInterval
                    End If

                    If VerifyCageHours(m_strUserID, decHoursNeeded) = False Then
                        blnScheduleErrorFlag = True
                        strScheduleError = "Not enough availble cage hours to complete reservation.  Hours needed: " & decHoursNeeded
                    End If

                ElseIf ddlFacility.SelectedValue = 3 Then
                    decDurationInHours = ddlDuration.SelectedValue / 60

                    If intRepeatInterval > 0 Then
                        decHoursNeededPerReservation = decHoursNeeded
                        decHoursNeeded = decHoursNeeded * intRepeatInterval
                    Else
                        decHoursNeeded = decDurationInHours
                    End If

                    If VerifyFieldHours(m_strUserID, decHoursNeeded) = False Then
                        blnScheduleErrorFlag = True
                        strScheduleError = "Not enough availble field hours to complete reservation.  Hours needed: " & decHoursNeeded
                    End If

                End If
            Else
                decHoursNeededPerReservation = 0
                decHoursNeeded = 0
            End If
        End If

            If blnScheduleErrorFlag = False Then

            For intIndex = 0 To intRepeatInterval
                strStartDateTime = lblDate.Text & " " & ddlStartTime.SelectedValue.ToString
                strEndDateTime = DateAdd(DateInterval.Minute, dblDuration, strStartDateTime)

                strStartDateTime = DateAdd(DateInterval.Day, intIndex * 7, strStartDateTime)
                strEndDateTime = DateAdd(DateInterval.Day, intIndex * 7, strEndDateTime)

                If ddlFacility.SelectedValue = 3 Then

                    If intRepeatInterval > 0 Then
                        strHeaderID = InsertScheduleHeader("Field", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractFieldHours(m_strUserID, decHoursNeededPerReservation)
                        End If
                    Else
                        strHeaderID = InsertScheduleHeader("Field", strStartDateTime, strEndDateTime, decHoursNeeded)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractFieldHours(m_strUserID, decHoursNeeded)
                        End If
                    End If

                    If cblLanes.Items(0).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 1, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "Field"
                        End If
                    End If
                ElseIf ddlFacility.SelectedValue = 2 Then

                    If intRepeatInterval > 0 Then
                        strHeaderID = InsertScheduleHeader("No Lanes", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractCageHours(m_strUserID, decHoursNeededPerReservation)
                        End If
                    Else
                        strHeaderID = InsertScheduleHeader("No Lanes", strStartDateTime, strEndDateTime, decHoursNeeded)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractCageHours(m_strUserID, decHoursNeeded)
                        End If
                    End If
                    If cblLanes.Items(0).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 1, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "1 "
                        End If
                    End If

                    If cblLanes.Items(1).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 2, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "2 "
                        End If
                    End If

                    If cblLanes.Items(2).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 3, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "3 "
                        End If
                    End If

                    If cblLanes.Items(3).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 5, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "Instructor "
                        End If
                    End If

                ElseIf ddlFacility.SelectedValue = 1 Then
                    If intRepeatInterval > 0 Then
                        strHeaderID = InsertScheduleHeader("No Lanes", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractCageHours(m_strUserID, decHoursNeededPerReservation)
                        End If
                    Else
                        strHeaderID = InsertScheduleHeader("No Lanes", strStartDateTime, strEndDateTime, decHoursNeeded)
                        If radDisplay.SelectedValue = "Team" Then
                            SubtractCageHours(m_strUserID, decHoursNeeded)
                        End If
                    End If
                    If cblLanes.Items(0).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 1, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            If cblLanes.Items(1).Selected = True Then
                                strLanes = strLanes & "Cage, "
                            Else
                                strLanes = strLanes & "Cage "
                            End If
                        End If
                    End If

                    If cblLanes.Items(1).Selected = True Then
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 2, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = strLanes & "Field "
                        End If
                    End If

                ElseIf ddlFacility.SelectedValue = 4 Then

                    If cblLanes.Items(0).Selected = True Then
                        If intRepeatInterval > 0 Then
                            strHeaderID = InsertScheduleHeader("Field 1", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeededPerReservation)
                            End If
                        Else
                            strHeaderID = InsertScheduleHeader("Field 1", strStartDateTime, strEndDateTime, decHoursNeeded)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeeded)
                            End If
                        End If
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 1, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = ""
                        End If
                    End If



                    If cblLanes.Items(1).Selected = True Then
                        If intRepeatInterval > 0 Then
                            strHeaderID = InsertScheduleHeader("Field 2", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeededPerReservation)
                            End If
                        Else
                            strHeaderID = InsertScheduleHeader("Field 2", strStartDateTime, strEndDateTime, decHoursNeeded)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeeded)
                            End If
                        End If
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 2, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = ""
                        End If
                    End If



                    If cblLanes.Items(2).Selected = True Then
                        If intRepeatInterval > 0 Then
                            strHeaderID = InsertScheduleHeader("Field 3", strStartDateTime, strEndDateTime, decHoursNeededPerReservation)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeededPerReservation)
                            End If
                        Else
                            strHeaderID = InsertScheduleHeader("Field 3", strStartDateTime, strEndDateTime, decHoursNeeded)
                            If radDisplay.SelectedValue = "Team" Then
                                SubtractFieldHours(m_strUserID, decHoursNeeded)
                            End If
                        End If
                        If InsertReservation(ddlFacility.SelectedValue.ToString, 3, strHeaderID, strStartDateTime, strEndDateTime, strDisplay) = True Then
                            strLanes = ""
                        End If
                    End If



                End If


                If ddlFacility.SelectedValue <> 4 Then
                    If strLanes <> "" Then

                        UpdateScheduleHeader(strLanes, strHeaderID)
                        SendEmail(strStartDateTime, dblDuration, strLanes)
                    Else
                        DeleteScheduleHeader(strHeaderID)
                    End If
                End If

                strLanes = ""
            Next

            'Response.Cookies.Set("ViperCookie")("ShowDate") = strStartDateTime
            'Response.Redirect("LaneSchedule.aspx")
        Else
            lblError.Text = strScheduleError

        End If

        DayPilotScheduler1.StartDate = strStartDateTime
        DayPilotScheduler1.DataSource = MakeDataTableAndDisplay()

        DayPilotScheduler2.StartDate = strStartDateTime
        DayPilotScheduler2.DataSource = MakeDataTableAndDisplay()

        DayPilotSchedulerSting.StartDate = strStartDateTime
        DayPilotSchedulerSting.DataSource = MakeDataTableAndDisplay()

        DayPilotSchedulerTomSawyer.StartDate = strStartDateTime
        DayPilotSchedulerTomSawyer.DataSource = MakeDataTableAndDisplay()


        DataBind()

        LoadCageHours()
        LoadFieldHours()

    End Sub
    Private Sub SubtractCageHours(pStrUserID As String, pDecHours As Decimal)
        Dim strSQL As String
        Dim strID As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim decInitialCageHours As Decimal
        Dim decNewCageHoursTotal As Decimal
        Dim strUpdateSQL As String
        Dim strUnlimitedHoursFlag As String


        objCommand.CommandType = CommandType.Text


        strSQL = "select CageHours, UnlimitedHoursFlag from Users where Userid = " & pStrUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        decInitialCageHours = objRow.Item("CageHours").ToString
        strUnlimitedHoursFlag = objRow.Item("UnlimitedHoursFlag").ToString

        If strUnlimitedHoursFlag = "N" Then

            decNewCageHoursTotal = decInitialCageHours - pDecHours

            strUpdateSQL = "Update users set CageHours = " & decNewCageHoursTotal & " where userid = " & pStrUserID
            objCommand.CommandText = strUpdateSQL
            objCommand.ExecuteNonQuery()
        End If

    End Sub


    Private Sub SubtractFieldHours(pStrUserID As String, pDecHours As Decimal)
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim decInitialFieldHours As Decimal
        Dim decNewFieldHoursTotal As Decimal
        Dim strUpdateSQL As String
        Dim strUnlimitedHoursFlag As String


        objCommand.CommandType = CommandType.Text


        strSQL = "select FieldHours, UnlimitedFieldHours from Users where Userid = " & pStrUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        decInitialFieldHours = objRow.Item("FieldHours").ToString
        strUnlimitedHoursFlag = objRow.Item("UnlimitedFieldHours").ToString

        If strUnlimitedHoursFlag = "N" Then

            decNewFieldHoursTotal = decInitialFieldHours - pDecHours

            strUpdateSQL = "Update users set FieldHours = " & decNewFieldHoursTotal & " where userid = " & pStrUserID
            objCommand.CommandText = strUpdateSQL
            objCommand.ExecuteNonQuery()
        End If

    End Sub

    Private Function VerifyCageHours(pStrUserID As String, pDecHours As Decimal) As Boolean
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim strUnlimitedHoursFlag As String
        Dim decInitialCageHours As Decimal


        objCommand.CommandType = CommandType.Text


        strSQL = "select CageHours, UnlimitedHoursFlag from Users where Userid = " & pStrUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        strUnlimitedHoursFlag = objRow.Item("UnlimitedHoursFlag").ToString
        decInitialCageHours = objRow.Item("CageHours").ToString

        If strUnlimitedHoursFlag = "Y" Then
            VerifyCageHours = True
        Else
            If decInitialCageHours > pDecHours Then
                VerifyCageHours = True
            Else
                VerifyCageHours = False
            End If
        End If

    End Function

    Private Function VerifyFieldHours(pStrUserID As String, pDecHours As Decimal) As Boolean
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim strUnlimitedFieldHoursFlag As String
        Dim decInitialFieldHours As Decimal


        objCommand.CommandType = CommandType.Text


        strSQL = "select FieldHours, UnlimitedFieldHours from Users where Userid = " & pStrUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        strUnlimitedFieldHoursFlag = objRow.Item("UnlimitedFieldHours").ToString
        decInitialFieldHours = objRow.Item("FieldHours").ToString

        If strUnlimitedFieldHoursFlag = "Y" Then
            VerifyFieldHours = True
        Else
            If decInitialFieldHours > pDecHours Then
                VerifyFieldHours = True
            Else
                VerifyFieldHours = False
            End If
        End If

    End Function

    Private Sub LoadCageHours()
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim strUnlimitedHoursFlag As String
        Dim decInitialCageHours As Decimal


        objCommand.CommandType = CommandType.Text


        strSQL = "select CageHours, UnlimitedHoursFlag from Users where Userid = " & m_strUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        strUnlimitedHoursFlag = objRow.Item("UnlimitedHoursFlag").ToString
        decInitialCageHours = objRow.Item("CageHours").ToString

        If strUnlimitedHoursFlag = "Y" Then
            lblCageHours.Text = "Unlimited"
        Else
            lblCageHours.Text = decInitialCageHours.ToString
        End If

    End Sub

    Private Sub LoadFieldHours()
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim strUnlimitedHoursFlag As String
        Dim decInitialCageHours As Decimal


        objCommand.CommandType = CommandType.Text


        strSQL = "select FieldHours, UnlimitedFieldHours from Users where Userid = " & m_strUserID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        strUnlimitedHoursFlag = objRow.Item("UnlimitedFieldHours").ToString
        decInitialCageHours = objRow.Item("FieldHours").ToString

        If strUnlimitedHoursFlag = "Y" Then
            lblFieldHours.Text = "Unlimited"
        Else
            lblFieldHours.Text = decInitialCageHours.ToString
        End If

    End Sub


    Private Function InsertReservation(ByVal pStrFacilitySysID As String, ByVal pStrLane As String, ByVal pStrHeaderID As String, ByVal pstrStartDateTime As String, ByVal pstrEndDateTime As String, ByVal pStrDisplay As String) As Boolean

        Dim strSQL As String
        Dim strSQLCheck As String
        'Dim strStartDateTime As DateTime
        'Dim strEndDateTime As DateTime
        Dim dblDuration As Double
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet

        dblDuration = ddlDuration.SelectedValue

        objCommand.CommandType = CommandType.Text

        'strStartDateTime = lblDate.Text & " " & ddlStartTime.SelectedValue.ToString
        'strEndDateTime = DateAdd(DateInterval.Minute, dblDuration, strStartDateTime)

        strSQLCheck = "select * from schedule " & _
      "where ((dateadd(mi,1,'" & pstrStartDateTime & "') between startdatetime and enddatetime) " & _
      "or (dateadd(mi,-1,'" & pstrEndDateTime & "') between startdatetime and enddatetime)) " & _
      "and laneid = " & pStrLane & " and facilitysysid = " & pStrFacilitySysID

        objDA = New SqlDataAdapter(strSQLCheck, m_SqlConn)
        objDA.Fill(objDS)

        If objDS.Tables(0).Rows.Count > 0 Then
            Dim strError As String
            strError = "Unable to reserve lane.  Schedule conflict on Lane " & pStrLane & " between " & pstrStartDateTime & " and " & pstrEndDateTime

            strSQL = "Insert into ScheduleErrors (UserID, Error, TimeStamp) values (" & _
                      m_strUserID & ",'" & strError & "',Getdate())"

            InsertReservation = False

        Else
            strSQL = "Insert into Schedule (FacilitySysID, LaneID, UserID, StartDateTime, EndDateTime, HeaderID, DisplayText) Values (" & ddlFacility.SelectedValue & "," & pStrLane & "," & _
          m_strUserID & ",'" & pstrStartDateTime & "','" & pstrEndDateTime & "'," & pStrHeaderID & ",'" & pStrDisplay & "')"

            InsertReservation = True
        End If

        Try
            objCommand.CommandText = strSQL
            objCommand.Connection = m_SqlConn
            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            InsertReservation = False
        End Try




    End Function

    Private Sub UpdateScheduleHeader(ByVal pStrLane As String, ByVal pStrHeaderID As String)

        Dim strSQL As String
        Dim strID As String
        Dim dblDuration As Double
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet


        objCommand.CommandType = CommandType.Text


        strSQL = "Update ScheduleHeader set Lanes = '" & pStrLane & "' where ScheduleHeaderId = " & pStrHeaderID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

    End Sub

    Private Sub DeleteScheduleHeader(ByVal pStrHeaderID As String)

        Dim strSQL As String
        Dim strID As String
        Dim dblDuration As Double
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet


        objCommand.CommandType = CommandType.Text


        strSQL = "DELETE FROM ScheduleHeader WHERE ScheduleHeaderID  = " & pStrHeaderID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objCommand.ExecuteNonQuery()

    End Sub

    Private Function InsertScheduleHeader(ByVal pStrLane As String, ByVal pstrStartDateTime As String, ByVal pstrEndDateTime As String, ByVal pDecTotalDuration As Double) As String

        Dim strSQL As String
        Dim strID As String
        Dim dblDuration As Double
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet

        strID = 0

        dblDuration = ddlDuration.SelectedValue

        objCommand.CommandType = CommandType.Text


        strSQL = "Insert into ScheduleHeader (FacilitySysID, Lanes, StartDateTime, EndDateTime, UserID, Duration, TotalDuration) Values (" & ddlFacility.SelectedValue & ",'" & pStrLane & _
           "','" & pstrStartDateTime & "','" & pstrEndDateTime & "'," & m_strUserID & ",'" & ddlDuration.SelectedItem.Text & "'," & pDecTotalDuration & "); select @@identity as id;"

        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)


        If objDS.Tables(0).Rows.Count > 0 Then
            objRow = objDS.Tables(0).Rows(0)
            strID = objRow.Item("id").ToString
        End If


        InsertScheduleHeader = strID


    End Function


    Private Sub CheckAvailability(ByVal pStrLaneID As String)

        Dim strSQL As String
        Dim strStartDateTime As DateTime
        Dim strEndDateTime As DateTime
        Dim dblDuration As Double

        strStartDateTime = lblDate.Text & " " & ddlStartTime.SelectedValue.ToString

        strSQL = "select * from schedule " & _
        "where ((dateadd(mi,1,'12-22-2012 18:30') between startdatetime and enddatetime) " & _
        "or (dateadd(mi,-1,'12-22-2012 19:00') between startdatetime and enddatetime)) " & _
        "and laneid = " & pStrLaneID


    End Sub

    Private Sub DeleteScheduleErrors()

        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim sConnectionString As String
        Dim OleDbConnection As SqlConnection

        sConnectionString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        OleDbConnection = New SqlConnection(sConnectionString)
        OleDbConnection.Open()
        objCommand.CommandType = CommandType.Text


        strSQL = "delete from ScheduleErrors where UserID =  " & m_strUserID


            objCommand.CommandText = strSQL
        objCommand.Connection = OleDbConnection
            objCommand.ExecuteNonQuery()

    End Sub

    Private Sub SendEmail(ByVal pStrStartDateTime As String, ByVal pStrDuration As String, ByVal pStrLanes As String)
        Dim msg As New System.Net.Mail.MailMessage
        Dim smtp As New System.Net.Mail.SmtpClient
        Dim strEmail As String
        strEmail = m_strEmail
        'strEmail = "eddie.hobson@me.com"


        Dim addrFrom As New System.Net.Mail.MailAddress("info@vipers-pit.org")
        Dim addrTo As New System.Net.Mail.MailAddress(strEmail.ToString)
        Dim strSubject As String = ""
        msg.From = addrFrom
        msg.To.Add(addrTo)
        strSubject = "Viper PIT Reservation Confirmed"
        msg.Subject = strSubject
        msg.IsBodyHtml = True
        smtp.Host = "relay-hosting.secureserver.net"
        msg.Body = "Reservation Confirmed " & "<br><br>" & _
            "Date/Time: " & pStrStartDateTime & "<br>" & _
            "Duration: " & pStrDuration & "<br>" & _
            "Lane(s): " & pStrLanes

        'smtp.Host = "smtp.gmail.com"
        'smtp.Port = 25
        smtp.Port = 25
        smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
        smtp.Credentials = New System.Net.NetworkCredential("info@vipers-pit.org", "Vipers$4206")
        'smtp.Credentials = New System.Net.NetworkCredential("eddie.hobson@insightbb.com", "4206kheh")
        ' smtp.Send(msg)


    End Sub
    Private Sub PopulateDropdowns()
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim objCommand As New SqlCommand

        strSQL = "select userid, firstname + ' ' + lastname as users from users"

        objCommand = New SqlCommand(strSQL, m_SqlConn)
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)

        ddlUser.DataSource = objDS.Tables(0)
        ddlUser.DataBind()
    End Sub


    Private Function GetRole() As String
        Dim strSQL As String
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim objCommand As New SqlCommand

        If ddlFacility.SelectedValue = 0 Then
            GetRole = "None"
        Else
            Select Case ddlFacility.SelectedValue

                Case 1
                    strSQL = "select * from users, roles where userid = " & m_strUserID & " and users.oldhenryroleid = roles.roleid"
                Case 2
                    strSQL = "select * from users, roles where userid = " & m_strUserID & " and users.springdaleroleid = roles.roleid"
                Case 3
                    strSQL = "select * from users, roles where userid = " & m_strUserID & " and users.viperfieldroleid = roles.roleid"
                Case 4
                    strSQL = "select * from users, roles where userid = " & m_strUserID & " and users.viperfieldroleid = roles.roleid"
            End Select

            objCommand = New SqlCommand(strSQL, m_SqlConn)
            objDA = New SqlDataAdapter(strSQL, m_SqlConn)
            objDA.Fill(objDS)
            objRow = objDS.Tables(0).Rows(0)

            GetRole = objRow.Item("RoleName").ToString

        End If

    End Function

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        Try
            m_SqlConn.Close()
        Catch ex As Exception

        End Try
    End Sub

  
    Protected Sub ddlUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged

    End Sub

    Protected Sub gvReservations_RowDeleted(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeletedEventArgs) Handles gvReservations.RowDeleted
        Response.Redirect("LaneSchedule.aspx")
    End Sub

    Protected Sub btnEditProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEditProfile.Click
        Response.Redirect("editprofile.aspx?ID=" & m_strUserID)
    End Sub

    Protected Sub lnkForgot_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkForgot.Click
        Response.Redirect("ForgotPassword.aspx")
    End Sub
    Protected Sub ddlFacility_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFacility.SelectedIndexChanged


        DayPilotScheduler1.DataSource = MakeDataTableAndDisplay()
        DayPilotScheduler2.DataSource = MakeDataTableAndDisplay()
        DayPilotSchedulerSting.DataSource = MakeDataTableAndDisplay()
        DayPilotSchedulerTomSawyer.DataSource = MakeDataTableAndDisplay()
        DataBind()
        SetCageOptionsBasedOnDate()

    End Sub

    Private Sub DayPilotScheduler2_TimeRangeSelected(sender As Object, e As TimeRangeSelectedEventArgs) Handles DayPilotScheduler2.TimeRangeSelected
        lblDate.Text = Convert.ToDateTime(e.Start).Date

        Try
            ddlStartTime.SelectedValue = Convert.ToDateTime(e.Start).TimeOfDay.ToString
        Catch
            ddlStartTime.SelectedValue = "Select"
        End Try
        ddlDuration.SelectedValue = 60
    End Sub

    Private Sub DayPilotSchedulerSting_TimeRangeSelected(sender As Object, e As TimeRangeSelectedEventArgs) Handles DayPilotSchedulerSting.TimeRangeSelected
        lblDate.Text = Convert.ToDateTime(e.Start).Date

        Try
            ddlStartTime.SelectedValue = Convert.ToDateTime(e.Start).TimeOfDay.ToString
        Catch
            ddlStartTime.SelectedValue = "Select"
        End Try
        ddlDuration.SelectedValue = 60
    End Sub

    Private Sub DayPilotScheduler2_EventClick(sender As Object, e As EventClickEventArgs) Handles DayPilotScheduler2.EventClick
        'Label1.Text = "Event with ID " + e.Value + " clicked."
    End Sub
    Private Sub SetCageOptionsBasedOnDate()

        Dim strDateCheck As DateTime

        If ddlFacility.SelectedValue = 2 Then
            cblLanes.Items(0).Text = "Lane 1"
            cblLanes.Items(1).Text = "Lane 2"
            cblLanes.Items(2).Text = "Lane 3"
            cblLanes.Items(3).Text = "Instructor Lane"
        ElseIf ddlFacility.SelectedValue = 1 Then
            cblLanes.Items(0).Text = "Batting Cage"
            cblLanes.Items(1).Text = "Field"
            cblLanes.Items(2).Text = "N/A"
            cblLanes.Items(3).Text = "N/A"
            cblLanes.Items(2).Enabled = False
            cblLanes.Items(2).Selected = False
            cblLanes.Items(3).Enabled = False
            cblLanes.Items(3).Selected = False

        ElseIf ddlFacility.SelectedValue = 3 Then
            cblLanes.Items(0).Text = "Field 1"
            cblLanes.Items(1).Text = "N/A"
            cblLanes.Items(2).Text = "N/A"
            cblLanes.Items(3).Text = "N/A"
            cblLanes.Items(1).Enabled = False
            cblLanes.Items(1).Selected = False
            cblLanes.Items(2).Enabled = False
            cblLanes.Items(2).Selected = False
            cblLanes.Items(3).Enabled = False
            cblLanes.Items(3).Selected = False

        ElseIf ddlFacility.SelectedValue = 4 Then
            cblLanes.Items(0).Text = "Field 1"
            cblLanes.Items(1).Text = "Field 2"
            cblLanes.Items(2).Text = "Field 3"
            cblLanes.Items(3).Text = "N/A"
            cblLanes.Items(1).Enabled = True
            cblLanes.Items(1).Selected = False
            cblLanes.Items(2).Enabled = True
            cblLanes.Items(2).Selected = False
            cblLanes.Items(3).Enabled = False
            cblLanes.Items(3).Selected = False

        End If



        If ddlStartTime.SelectedValue.ToString <> "Select" Then

            strDateCheck = lblDate.Text & " " & ddlStartTime.SelectedValue.ToString

            If ddlFacility.SelectedValue = 1 Or ddlFacility.SelectedValue = 2 Or ddlFacility.SelectedValue = 4 Then

                Select Case m_strRole
                    Case "Administrator"

                    Case "Coach"

                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 336 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                        Else
                            cblLanes.Items(0).Enabled = True
                            cblLanes.Items(1).Enabled = True
                            cblLanes.Items(2).Enabled = True
                        End If

                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 168 Then
                            cblLanes.Items(3).Enabled = False
                        Else
                            cblLanes.Items(3).Enabled = True
                        End If

                    Case "Player"

                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 48 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        Else
                            cblLanes.Items(0).Enabled = True
                            cblLanes.Items(1).Enabled = True
                            cblLanes.Items(2).Enabled = True
                            cblLanes.Items(3).Enabled = True
                        End If

                    Case "Instructor"
                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 48 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Enabled = True
                        Else
                            cblLanes.Items(0).Enabled = True
                            cblLanes.Items(1).Enabled = True
                            cblLanes.Items(2).Enabled = True
                            cblLanes.Items(3).Enabled = True
                        End If

                End Select
            ElseIf ddlFacility.SelectedValue = 3 Then

                Select Case m_strRole
                    Case "Administrator"
                        cblLanes.Items(0).Enabled = True
                        cblLanes.Items(1).Enabled = False
                        cblLanes.Items(2).Enabled = False
                        cblLanes.Items(3).Enabled = False
                        cblLanes.Items(1).Selected = False
                        cblLanes.Items(2).Selected = False
                        cblLanes.Items(3).Selected = False
                    Case "Coach"

                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 336 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        Else
                            cblLanes.Items(0).Enabled = True
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        End If


                    Case "Player"

                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 48 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        Else
                            cblLanes.Items(0).Enabled = True
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        End If

                    Case "Instructor"
                        If DateDiff(DateInterval.Hour, Now(), strDateCheck) > 48 Then
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        Else
                            cblLanes.Items(0).Enabled = False
                            cblLanes.Items(1).Enabled = False
                            cblLanes.Items(2).Enabled = False
                            cblLanes.Items(3).Enabled = False
                            cblLanes.Items(0).Selected = False
                            cblLanes.Items(1).Selected = False
                            cblLanes.Items(2).Selected = False
                            cblLanes.Items(3).Selected = False
                        End If

                End Select
            End If
        End If
    End Sub
    Protected Sub ddlStartTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlStartTime.SelectedIndexChanged
        SetCageOptionsBasedOnDate()
    End Sub

    Private Sub gvReservations_RowDeleting(sender As Object, e As GridViewDeleteEventArgs) Handles gvReservations.RowDeleting
        Dim strSQL As String
        Dim objCommand As New SqlCommand
        Dim sConnectionString As String
        Dim OleDbConnection As SqlConnection
        Dim intScheduleHeaderSysID As Int64

        intScheduleHeaderSysID = Convert.ToInt64(gvReservations.DataKeys(e.RowIndex).Values(0))

        AddBackHours(intScheduleHeaderSysID)

        sConnectionString = System.Configuration.ConfigurationManager.AppSettings("ViperConnString").ToString
        OleDbConnection = New SqlConnection(sConnectionString)
        OleDbConnection.Open()
        objCommand.CommandType = CommandType.Text


        strSQL = "DELETE FROM ScheduleHeader WHERE ScheduleHeaderID =" & intScheduleHeaderSysID & "; delete from schedule where headerid = " & intScheduleHeaderSysID


        objCommand.CommandText = strSQL
        objCommand.Connection = OleDbConnection
        objCommand.ExecuteNonQuery()

        LoadCageHours()
        LoadFieldHours()
    End Sub


    Private Sub AddBackHours(pStrHeaderSysID As String)
        Dim strSQL As String
        Dim strSQL2 As String
        Dim objCommand As New SqlCommand
        Dim objCommandCheck As New SqlCommand
        Dim objDA As SqlDataAdapter
        Dim objRow As Data.DataRow
        Dim objDS As New Data.DataSet
        Dim objDA2 As SqlDataAdapter
        Dim objRow2 As Data.DataRow
        Dim objDS2 As New Data.DataSet
        Dim decInitialFieldHours As Decimal
        Dim decDurationTotal As Decimal
        Dim strFacilitySysID As String
        Dim strCageUnlimtedFlag As String
        Dim strFieldUnlimitedFlag As String
        Dim decInitialCageHours As Decimal
        Dim strSQLUpdate As String = ""
        Dim decNewHoursTotal As Decimal


        objCommand.CommandType = CommandType.Text


        strSQL = "select * from ScheduleHeader where ScheduleHeaderID = " & pStrHeaderSysID
        objCommand.CommandText = strSQL
        objCommand.Connection = m_SqlConn
        objDA = New SqlDataAdapter(strSQL, m_SqlConn)
        objDA.Fill(objDS)
        objRow = objDS.Tables(0).Rows(0)
        decDurationTotal = objRow.Item("TotalDuration").ToString
        strFacilitySysID = objRow.Item("FacilitySysID").ToString

        strSQL2 = "select * from Users where UserID = " & m_strUserID
        objDA2 = New SqlDataAdapter(strSQL2, m_SqlConn)
        objDA2.Fill(objDS2)
        objRow2 = objDS2.Tables(0).Rows(0)
        strCageUnlimtedFlag = objRow2.Item("UnlimitedHoursFlag").ToString
        strFieldUnlimitedFlag = objRow2.Item("UnlimitedFieldHours").ToString
        decInitialCageHours = objRow2.Item("CageHours").ToString
        decInitialFieldHours = objRow2.Item("FieldHours").ToString


        If strFacilitySysID = 1 Or strFacilitySysID = 2 Then
            If strCageUnlimtedFlag = "N" Then
                decNewHoursTotal = decInitialCageHours + decDurationTotal
                strSQLUpdate = "Update Users set CageHours = " & decNewHoursTotal & " where UserID = " & m_strUserID
            End If


        ElseIf strFacilitySysID = 3 Then
            If strFieldUnlimitedFlag = "N" Then
                decNewHoursTotal = decInitialFieldHours + decDurationTotal
                strSQLUpdate = "Update Users set FieldHours = " & decNewHoursTotal & " where UserID = " & m_strUserID
            End If
        End If

        If strSQLUpdate <> "" Then
            objCommand.CommandText = strSQLUpdate
            objCommand.ExecuteNonQuery()
        End If

    End Sub


    Private Sub gvReservations_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles gvReservations.RowDataBound
        If e.Row.RowIndex <> -1 Then

            If DateDiff(DateInterval.Hour, Now(), Convert.ToDateTime(e.Row.Cells(1).Text.ToString)) < 48 Then
                e.Row.Cells(0).Controls.Clear()
            End If
        End If
    End Sub

    Private Sub gvReservations_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles gvReservations.RowCreated

    End Sub

    Private Sub DayPilotSchedulerTomSawyer_TimeRangeSelected(sender As Object, e As TimeRangeSelectedEventArgs) Handles DayPilotSchedulerTomSawyer.TimeRangeSelected
        lblDate.Text = Convert.ToDateTime(e.Start).Date

        Try
            ddlStartTime.SelectedValue = Convert.ToDateTime(e.Start).TimeOfDay.ToString
        Catch
            ddlStartTime.SelectedValue = "Select"
        End Try
        ddlDuration.SelectedValue = 60
    End Sub
End Class

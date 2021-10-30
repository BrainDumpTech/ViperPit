Imports System.Data.SqlClient
Imports System.Data
Imports DayPilot.Web.Ui.Enums
Imports DayPilot.Web.Ui.Events
Partial Class Default2

    
    Inherits System.Web.UI.Page
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DayPilotScheduler1.TimeRangeSelectedHandling = TimeRangeSelectedHandling.PostBack
        DayPilotScheduler1.EventClickHandling = EventClickHandlingEnum.PostBack

        'DayPilotScheduler1.StartDate = Today
        DayPilotScheduler1.DataSource = MakeDataTableAndDisplay()
        DataBind()

        lblCurrentDate.Text = DayPilotScheduler1.StartDate
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
        table.Rows.Add(Convert.ToDateTime("8:00"), Convert.ToDateTime("9:00").AddDays(1), "David", 1, "1")
        'table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now)
        'table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now)
        'table.Rows.Add(21, "Combivent", "Janet", DateTime.Now)
        'table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now)
        GetTable = table
    End Function

    Private Function MakeDataTableAndDisplay() As DataTable
        ' Create new DataTable and DataSource objects. 
        Dim table As DataTable = New DataTable()

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


        row = table.NewRow()
        row("start") = Convert.ToDateTime("10/5/2012 8:00")
        row("end") = Convert.ToDateTime("10/5/2012 10:00")
        row("id") = 1
        row("resource") = "3"
        row("name") = "13U"
        table.Rows.Add(row)
        MakeDataTableAndDisplay = table
    End Function

    Protected Sub DayPilotScheduler1_EventClick(ByVal sender As Object, ByVal e As DayPilot.Web.Ui.Events.EventClickEventArgs) Handles DayPilotScheduler1.EventClick
        Label1.Text = "Event with ID " + e.Value + " clicked."
    End Sub

    Protected Sub DayPilotScheduler1_TimeRangeSelected(ByVal sender As Object, ByVal e As DayPilot.Web.Ui.Events.TimeRangeSelectedEventArgs) Handles DayPilotScheduler1.TimeRangeSelected
        Label1.Text = "Time cell starting at " + e.Start + " clicked."
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        setWeek()
    End Sub
    Sub setWeek()

        DayPilotScheduler1.StartDate = Calendar1.SelectedDate
        lblCurrentDate.Text = DayPilotScheduler1.StartDate
       
    End Sub
End Class

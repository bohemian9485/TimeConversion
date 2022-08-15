Imports System.Data.SQLite

Public Class FrmMain
#Region "Module-Wide"
    Private Const DATA_TYPE_INT As String = "System.Int32"
    Private Const DATA_TYPE_TEXT As String = "System.String"
    Private Const DATA_TYPE_DECIMAL As String = "System.Decimal"

    Private Const CONVERSION_TYPE_HOUR As Long = 1
    Private Const CONVERSION_TYPE_MINUTE As Long = 2
    Private Const MAXIMUM_MINUTES As Integer = 480

    Private Const APP_DB As String = "ConversionTable.db"
    Private dbPath As String = Application.StartupPath & "\" & APP_DB
    Private connString As String = "Data Source=" & dbPath & ";Version=3;"

    Private scnConversion As New SQLiteConnection(connString)

    Private Const SQL_TIME_TYPE As String = "SELECT TimeTypeId, TimeType FROM TimeTypes"
    Private Const TBL_TYPES As String = "TimeTypes"
    Private Const COL_TYPES_ID As String = "TimeTypeId"
    Private Const COL_TYPES_TYPE As String = "TimeType"
    Private ReadOnly scdTimeType As New SQLiteCommand(SQL_TIME_TYPE, scnConversion) With {.CommandType = CommandType.Text}
    Private dtbTypes As New DataTable With {.TableName = TBL_TYPES}

    Private Const SQL_CONVERSION_TABLE As String = "SELECT ConversionId, ConversionTime, TimeTypeId, ConversionValue FROM ConversionTable"
    Private Const TBL_CONVERSION As String = "ConversionTable"
    Private Const COL_CONVERSION_ID As String = "ConversionId"
    Private Const COL_CONVERSION_TIME As String = "ConversionTime"
    Private Const COL_CONVERSION_VALUE As String = "ConversionValue"
    Private ReadOnly scdConversion As New SQLiteCommand(SQL_CONVERSION_TABLE, scnConversion) With {.CommandType = CommandType.Text}
    Private dtbConversion As New DataTable With {.TableName = TBL_CONVERSION}

    Private userChangeType As Boolean
    Private userChangeTime As Boolean
    Private userClickRadioButtonType As Boolean
    Private userClickRadioButtonManual As Boolean
    Private userChangeManualValue As Boolean

    Private combHourData As New AutoCompleteStringCollection()
    Private combMinuteData As New AutoCompleteStringCollection()
#End Region

    Private Sub ShowError(ByVal ModuleName As String, ByVal ErrorMessage As String)
        ' Shows error messages in a uniform way
        Dim caption As String = ModuleName & " Error"
        Dim unUsed = MessageBox.Show(ErrorMessage, caption, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub BuildTables()
        Try
            With dtbTypes
                Dim column As New DataColumn(COL_TYPES_ID) With {.DataType = Type.GetType(DATA_TYPE_INT)}
                .Columns.Add(column)
                column = New DataColumn(COL_TYPES_TYPE) With {.DataType = Type.GetType(DATA_TYPE_TEXT)}
                .Columns.Add(column)
            End With
            With dtbConversion
                Dim column = New DataColumn(COL_CONVERSION_ID) With {.DataType = Type.GetType(DATA_TYPE_INT)}
                .Columns.Add(column)
                column = New DataColumn(COL_CONVERSION_TIME) With {.DataType = Type.GetType(DATA_TYPE_TEXT)}
                .Columns.Add(column)
                column = New DataColumn(COL_TYPES_ID) With {.DataType = Type.GetType(DATA_TYPE_INT)}
                .Columns.Add(column)
                column = New DataColumn(COL_CONVERSION_VALUE) With {.DataType = Type.GetType(DATA_TYPE_DECIMAL)}
                .Columns.Add(column)
            End With
        Catch ex As Exception
            ShowError("BuildTables()", ex.Message)
        End Try
    End Sub

    Private Sub GetTimeTypeData()
        Try
            Dim row As DataRow
            scnConversion.Open()
            Dim reader As SQLiteDataReader = scdTimeType.ExecuteReader()
            If reader.HasRows Then
                While reader.Read()
                    With dtbTypes
                        row = .NewRow
                        row.Item(0) = reader.Item(0)
                        row.Item(1) = reader.Item(1)
                        .Rows.Add(row)
                    End With
                End While
            End If
            reader.Close()
        Catch ex As Exception
            ShowError("GetTimeTypeData()", ex.Message)
        Finally
            If scnConversion.State And ConnectionState.Open Then scnConversion.Close()
        End Try
    End Sub

    Private Sub GetConversionData()
        Try
            Dim row As DataRow
            scnConversion.Open()
            Dim reader As SQLiteDataReader = scdConversion.ExecuteReader()
            If reader.HasRows Then
                While reader.Read
                    With dtbConversion
                        row = .NewRow
                        row.Item(0) = reader.Item(0)
                        row.Item(1) = reader.Item(1)
                        row.Item(2) = reader.Item(2)
                        row.Item(3) = reader.Item(3)
                        .Rows.Add(row)
                    End With
                End While
            End If
            reader.Close()
        Catch ex As Exception
            ShowError("GetConversionData()", ex.Message)
        Finally
            If scnConversion.State And ConnectionState.Open Then scnConversion.Close()
        End Try
    End Sub

    Private Sub BindDataSource()
        With CbxTypes
            .DisplayMember = COL_TYPES_TYPE
            .ValueMember = COL_TYPES_ID
            .DataSource = dtbTypes
        End With
        With CbxTime
            .DisplayMember = COL_CONVERSION_TIME
            .ValueMember = COL_CONVERSION_ID
            .DataSource = dtbConversion
        End With
    End Sub

    Private Sub FilterTimeType()
        Try
            Dim filter As String = COL_TYPES_ID & " = " & CbxTypes.SelectedValue.ToString
            ' Use dataview to filter
            dtbConversion.DefaultView.RowFilter = filter
        Catch ex As Exception
            ShowError("FilterTimeType()", ex.Message)
        End Try
    End Sub

    Private Sub GetConversionValue()
        TxtConverted.Text = vbNullString
        Try
            Dim filter As String = COL_CONVERSION_ID & " = " & CbxTime.SelectedValue.ToString
            Dim dv = New DataView(dtbConversion) With {.RowFilter = filter}
            If dv.ToTable.Rows.Count > 0 Then
                TxtConverted.Text = Format(dv.ToTable.Rows(0).Item(3), "0.000")
            End If
            dv.Dispose()
        Catch ex As Exception
            ShowError("GetConversionValue()", ex.Message)
        End Try
    End Sub

    Private Sub BuildAutoCompleteData()
        Try
            Dim filter As String = COL_TYPES_ID & " = " & CONVERSION_TYPE_HOUR.ToString
            Dim dv As New DataView(dtbConversion) With {.RowFilter = filter}
            If dv.ToTable.Rows.Count > 0 Then
                For Each row As DataRow In dv.ToTable.Rows
                    combHourData.Add(row.Item(1).ToString)
                Next
            End If
            filter = COL_TYPES_ID & " = " & CONVERSION_TYPE_MINUTE.ToString
            dv = New DataView(dtbConversion) With {.RowFilter = filter}
            If dv.ToTable.Rows.Count > 0 Then
                For Each row As DataRow In dv.ToTable.Rows
                    combMinuteData.Add(row.Item(1).ToString)
                Next
            End If
            dv.Dispose()
        Catch ex As Exception
            ShowError("BuildAutoCompleteData()", ex.Message)
        End Try
    End Sub

    Private Sub WhatToDisable()
        If RbnTypes.Checked Then
            CbxTypes.Enabled = True
            CbxTime.Enabled = True
            TxtManual.Text = vbNullString
            TxtManual.Enabled = False
            GetConversionValue()
        ElseIf RbnManual.Checked Then
            CbxTypes.Enabled = False
            CbxTime.Enabled = False
            TxtManual.Enabled = True
            TxtManual.Focus()
            TxtConverted.Text = vbNullString
        End If
    End Sub

    Private Sub ChangeAutoCompleteData()
        ' Changes CbxTime's AutoCompleteCustomSource property based
        ' on user selection in CbxTypes
        Select Case CbxTypes.SelectedValue
            Case CONVERSION_TYPE_HOUR
                CbxTime.AutoCompleteCustomSource = combHourData
            Case CONVERSION_TYPE_MINUTE
                CbxTime.AutoCompleteCustomSource = combMinuteData
        End Select
    End Sub

    Private Sub GetConversionValueByManual()
        Try
            TxtConverted.Text = vbNullString
            Dim filter As String = COL_CONVERSION_TIME & " = '" & TxtManual.Text & "'"
            Dim dv As New DataView(dtbConversion) With {.RowFilter = filter}
            If dv.ToTable.Rows.Count > 0 Then
                TxtConverted.Text = Format(dv.ToTable.Rows(0).Item(3), "0.000")
            End If
            dv.Dispose()
        Catch ex As Exception
            ShowError("GetConversionValueByManual()", ex.Message)
        End Try
    End Sub

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        BuildTables()
        GetTimeTypeData()
        GetConversionData()
        BuildAutoCompleteData()
        BindDataSource()
        FilterTimeType()
        GetConversionValue()
        ChangeAutoCompleteData()
        ChangeAutoCompleteData()
        WhatToDisable()
    End Sub

    Private Sub CbxTypes_GotFocus(sender As Object, e As EventArgs) Handles CbxTypes.GotFocus
        userChangeType = True
    End Sub

    Private Sub CbxTypes_LostFocus(sender As Object, e As EventArgs) Handles CbxTypes.LostFocus
        userChangeType = False
    End Sub

    Private Sub CbxTypes_SelectedValueChanged(sender As Object, e As EventArgs) Handles CbxTypes.SelectedValueChanged
        If userChangeType Then
            FilterTimeType()
            GetConversionValue()
            ChangeAutoCompleteData()
        End If
    End Sub

    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        userChangeTime = False
        userChangeType = False
        userClickRadioButtonManual = False
        userClickRadioButtonType = False
    End Sub

    Private Sub CbxTime_GotFocus(sender As Object, e As EventArgs) Handles CbxTime.GotFocus
        userChangeTime = True
    End Sub

    Private Sub CbxTime_LostFocus(sender As Object, e As EventArgs) Handles CbxTime.LostFocus
        userChangeTime = False
    End Sub

    Private Sub CbxTime_SelectedValueChanged(sender As Object, e As EventArgs) Handles CbxTime.SelectedValueChanged
        If userChangeTime Then
            GetConversionValue()
        End If
    End Sub

    Private Sub RbnTypes_CheckedChanged(sender As Object, e As EventArgs) Handles RbnTypes.CheckedChanged
        If userClickRadioButtonType Then
            WhatToDisable()
        End If
    End Sub

    Private Sub RbnManual_CheckedChanged(sender As Object, e As EventArgs) Handles RbnManual.CheckedChanged
        If userClickRadioButtonManual Then
            WhatToDisable()
        End If
    End Sub

    Private Sub RbnTypes_GotFocus(sender As Object, e As EventArgs) Handles RbnTypes.GotFocus
        userClickRadioButtonType = True
    End Sub

    Private Sub RbnTypes_LostFocus(sender As Object, e As EventArgs) Handles RbnTypes.LostFocus
        userClickRadioButtonType = False
    End Sub

    Private Sub RbnManual_LostFocus(sender As Object, e As EventArgs) Handles RbnManual.LostFocus
        userClickRadioButtonManual = False
    End Sub

    Private Sub RbnManual_GotFocus(sender As Object, e As EventArgs) Handles RbnManual.GotFocus
        userClickRadioButtonManual = True
    End Sub

    Private Sub TxtManual_GotFocus(sender As Object, e As EventArgs) Handles TxtManual.GotFocus
        userChangeManualValue = True
    End Sub

    Private Sub TxtManual_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtManual.KeyPress
        ' Limit user input to numeric characters
        '97 - 122 = Ascii codes for simple letters
        '65 - 90  = Ascii codes for capital letters
        '48 - 57  = Ascii codes for numbers
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub TxtManual_LostFocus(sender As Object, e As EventArgs) Handles TxtManual.LostFocus
        userChangeManualValue = False
    End Sub

    Private Sub TxtManual_TextChanged(sender As Object, e As EventArgs) Handles TxtManual.TextChanged
        If userChangeManualValue Then
            If TxtManual.Text <> vbNullString Then
                If CInt(TxtManual.Text) <= MAXIMUM_MINUTES Then
                    GetConversionValueByManual()
                Else
                    TxtManual.Text = vbNullString
                    TxtManual.Focus()
                    TxtConverted.Text = vbNullString
                    Dim unUsed = MessageBox.Show("Maximum minutes is 480.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            Else
                TxtConverted.Text = vbNullString
            End If
        End If
    End Sub

    Private Sub ChkOnTop_CheckedChanged(sender As Object, e As EventArgs) Handles ChkOnTop.CheckedChanged
        TopMost = ChkOnTop.Checked
    End Sub
End Class

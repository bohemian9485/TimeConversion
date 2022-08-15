<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmMain))
        Me.CbxTypes = New System.Windows.Forms.ComboBox()
        Me.CbxTime = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TxtConverted = New System.Windows.Forms.TextBox()
        Me.RbnTypes = New System.Windows.Forms.RadioButton()
        Me.RbnManual = New System.Windows.Forms.RadioButton()
        Me.TxtManual = New System.Windows.Forms.TextBox()
        Me.ChkOnTop = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SuspendLayout()
        '
        'CbxTypes
        '
        Me.CbxTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CbxTypes.FormattingEnabled = True
        Me.CbxTypes.Location = New System.Drawing.Point(157, 12)
        Me.CbxTypes.Name = "CbxTypes"
        Me.CbxTypes.Size = New System.Drawing.Size(79, 24)
        Me.CbxTypes.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CbxTypes, "Select between hour and minute")
        '
        'CbxTime
        '
        Me.CbxTime.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CbxTime.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.CbxTime.DropDownHeight = 131
        Me.CbxTime.FormattingEnabled = True
        Me.CbxTime.IntegralHeight = False
        Me.CbxTime.Location = New System.Drawing.Point(242, 12)
        Me.CbxTime.Name = "CbxTime"
        Me.CbxTime.Size = New System.Drawing.Size(39, 24)
        Me.CbxTime.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.CbxTime, "Number of hours or minutes")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(115, 16)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Fractions of a Day"
        '
        'TxtConverted
        '
        Me.TxtConverted.Location = New System.Drawing.Point(157, 72)
        Me.TxtConverted.Name = "TxtConverted"
        Me.TxtConverted.ReadOnly = True
        Me.TxtConverted.Size = New System.Drawing.Size(124, 22)
        Me.TxtConverted.TabIndex = 6
        Me.TxtConverted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.TxtConverted, "Fractions of a day")
        '
        'RbnTypes
        '
        Me.RbnTypes.AutoSize = True
        Me.RbnTypes.Checked = True
        Me.RbnTypes.Location = New System.Drawing.Point(12, 14)
        Me.RbnTypes.Name = "RbnTypes"
        Me.RbnTypes.Size = New System.Drawing.Size(128, 20)
        Me.RbnTypes.TabIndex = 0
        Me.RbnTypes.TabStop = True
        Me.RbnTypes.Text = "Conversion Type"
        Me.ToolTip1.SetToolTip(Me.RbnTypes, "Select from conversion table")
        Me.RbnTypes.UseVisualStyleBackColor = True
        '
        'RbnManual
        '
        Me.RbnManual.AutoSize = True
        Me.RbnManual.Location = New System.Drawing.Point(12, 43)
        Me.RbnManual.Name = "RbnManual"
        Me.RbnManual.Size = New System.Drawing.Size(139, 20)
        Me.RbnManual.TabIndex = 3
        Me.RbnManual.TabStop = True
        Me.RbnManual.Text = "Manual (in minutes)"
        Me.ToolTip1.SetToolTip(Me.RbnManual, "Manual input")
        Me.RbnManual.UseVisualStyleBackColor = True
        '
        'TxtManual
        '
        Me.TxtManual.Location = New System.Drawing.Point(157, 42)
        Me.TxtManual.Name = "TxtManual"
        Me.TxtManual.Size = New System.Drawing.Size(124, 22)
        Me.TxtManual.TabIndex = 4
        Me.TxtManual.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.TxtManual, "Number of minutes")
        '
        'ChkOnTop
        '
        Me.ChkOnTop.AutoSize = True
        Me.ChkOnTop.Location = New System.Drawing.Point(12, 100)
        Me.ChkOnTop.Name = "ChkOnTop"
        Me.ChkOnTop.Size = New System.Drawing.Size(166, 20)
        Me.ChkOnTop.TabIndex = 7
        Me.ChkOnTop.Text = "On top of other windows"
        Me.ToolTip1.SetToolTip(Me.ChkOnTop, "Place this window on top of other windows")
        Me.ChkOnTop.UseVisualStyleBackColor = True
        '
        'ToolTip1
        '
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.ToolTip1.ToolTipTitle = "Hour/Minute Conversion"
        '
        'FrmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(293, 132)
        Me.Controls.Add(Me.ChkOnTop)
        Me.Controls.Add(Me.TxtManual)
        Me.Controls.Add(Me.RbnManual)
        Me.Controls.Add(Me.RbnTypes)
        Me.Controls.Add(Me.TxtConverted)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.CbxTime)
        Me.Controls.Add(Me.CbxTypes)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Hour/Minute Conversion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CbxTypes As ComboBox
    Friend WithEvents CbxTime As ComboBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtConverted As TextBox
    Friend WithEvents RbnTypes As RadioButton
    Friend WithEvents RbnManual As RadioButton
    Friend WithEvents TxtManual As TextBox
    Friend WithEvents ChkOnTop As CheckBox
    Friend WithEvents ToolTip1 As ToolTip
End Class

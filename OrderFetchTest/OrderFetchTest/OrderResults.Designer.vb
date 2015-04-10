<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OrderResults
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
        Me.FetchOrders = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.email = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Increment = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ApiUsername = New System.Windows.Forms.TextBox()
        Me.ApiKey = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'FetchOrders
        '
        Me.FetchOrders.Location = New System.Drawing.Point(15, 129)
        Me.FetchOrders.Name = "FetchOrders"
        Me.FetchOrders.Size = New System.Drawing.Size(306, 32)
        Me.FetchOrders.TabIndex = 0
        Me.FetchOrders.Text = "Fetch Orders"
        Me.FetchOrders.UseVisualStyleBackColor = True
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(15, 219)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TextBox1.Size = New System.Drawing.Size(306, 200)
        Me.TextBox1.TabIndex = 1
        '
        'email
        '
        Me.email.Location = New System.Drawing.Point(15, 180)
        Me.email.Name = "email"
        Me.email.Size = New System.Drawing.Size(306, 20)
        Me.email.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 164)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(244, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Customer Email (example of fetching a single field):"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 203)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "XML Response:"
        '
        'Increment
        '
        Me.Increment.Location = New System.Drawing.Point(12, 103)
        Me.Increment.Name = "Increment"
        Me.Increment.Size = New System.Drawing.Size(309, 20)
        Me.Increment.TabIndex = 2
        Me.Increment.Text = "100000030"
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(703, -157)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(271, 20)
        Me.TextBox3.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 87)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(227, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Order Increment ID (This order will be fetched):"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Magento API Username"
        '
        'ApiUsername
        '
        Me.ApiUsername.Location = New System.Drawing.Point(15, 25)
        Me.ApiUsername.Name = "ApiUsername"
        Me.ApiUsername.Size = New System.Drawing.Size(309, 20)
        Me.ApiUsername.TabIndex = 2
        '
        'ApiKey
        '
        Me.ApiKey.Location = New System.Drawing.Point(15, 64)
        Me.ApiKey.Name = "ApiKey"
        Me.ApiKey.Size = New System.Drawing.Size(309, 20)
        Me.ApiKey.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Magento API Key"
        '
        'OrderResults
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 432)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.ApiKey)
        Me.Controls.Add(Me.ApiUsername)
        Me.Controls.Add(Me.Increment)
        Me.Controls.Add(Me.email)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.FetchOrders)
        Me.MaximumSize = New System.Drawing.Size(349, 470)
        Me.MinimumSize = New System.Drawing.Size(349, 470)
        Me.Name = "OrderResults"
        Me.Text = "Magento API Test"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents FetchOrders As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents email As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Increment As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ApiUsername As System.Windows.Forms.TextBox
    Friend WithEvents ApiKey As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label

End Class

Public Class OrderResults

    Dim mage As New MagentoService.Mage_Api_Model_Server_V2_HandlerPortTypeClient()
    Dim sessionId As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FetchOrders.Click

        REM * Do not proceed if API details are not set

        If ApiDetailsSet() = False Then
            email.Clear()
            TextBox1.Clear()
            TextBox1.AppendText("Please set API details and try again")
            Return
        End If

        REM Log in and fetch session ID - the Session ID is needed for all
        REM calls to the Magento API

        Dim sessionId As String
        sessionId = GetSessionId()

        REM Request the specified sales order item by its increment id.  
        REM You can do basically anything with MagentoService (explore the 
        REM MagentoService class to see what is available)

        Dim salesOrderEntity = New MagentoService.salesOrderEntity()
        Dim salesOrderInfoResponse = New MagentoService.salesOrderInfoResponse()
        Dim salesOrderRequest = New MagentoService.salesOrderInfoRequest(sessionId, Increment.Text)
        salesOrderInfoResponse = mage.salesOrderInfo(salesOrderRequest)
        salesOrderEntity = salesOrderInfoResponse.result

        REM Serialize the object into an XML string and print this in the main
        REM Text area (TextBox1)

        TextBox1.Clear()
        Dim xmlSerializer As New Xml.Serialization.XmlSerializer(salesOrderEntity.GetType())
        Dim stringWriter As New IO.StringWriter()
        xmlSerializer.Serialize(stringWriter, salesOrderEntity)
        TextBox1.AppendText(stringWriter.ToString())

        REM Also pull a specific field from the response to further demonstrate
        REM the API

        email.Clear()
        email.AppendText(salesOrderEntity.customer_email)

    End Sub

    Private Function GetSessionId()
        If sessionId <> "" Then
            Return sessionId
        End If

        Dim loginResponse As New MagentoService.loginResponse()
        Dim loginRequest As New MagentoService.loginRequest(ApiUsername.Text, ApiKey.Text)
        loginResponse = mage.login(loginRequest)
        sessionId = loginResponse.loginReturn.ToString
        Return sessionId

    End Function

    REM * Has the user entered their API username and key?

    Private Function ApiDetailsSet()
        If ApiUsername.Text = "" Or ApiKey.Text = "" Then
            Return False
        End If
        Return True
    End Function

End Class
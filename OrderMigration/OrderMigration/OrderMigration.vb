Option Strict On

Imports System.Data.SqlClient
Imports System.IO
Imports System.Net.Mail

Module OrderMigration
    Dim mageService As New MagentoReference.Mage_Api_Model_Server_V2_HandlerPortTypeClient
    Dim sessionId As String
    Dim solutionsConnectionString As String = "Data Source=sqlserver;Initial Catalog=SERV-U;User ID=sa;Password=!get2it"
    Dim solutionsConnection As New SqlClient.SqlConnection
    Dim solutionsSQL As String = "SELECT a.FIRST, a.LAST, a.EMAIL_ADDRESS, b.[date], a.address1, a.POSTAL_CODE, b.AUX_ORDER_NO, c.PRODUCT_#, c.SIZE, c.COLOR, c.qty, c.UNIT_COST FROM [CUST] as a " & _
                                "INNER JOIN [ORDERS] as b ON a.ID = b.CUST_ID " & _
                                "INNER JOIN [LITEM] as c ON b.[NO] = c.[ORDER_NO] " & _
                                "WHERE b.[DATE] > '2012-01-01 00:00:00.000' AND b.[DATE] < '2012-03-03 00:00:00.000' AND " & _
                                "b.AUX_ORDER_NO <> '' and c.product_# NOT LIKE 'CAT-%' "

    Sub Main()

        Dim StartTime As Double, EndTime As Double, i As Integer
        StartTime = Timer
        i = 0

        sessionId = GetSessionId()

        Dim fltr = New MagentoReference.filters()
        Dim customers = mageService.customerCustomerList(sessionId, fltr)


        For Each customer In customers
            i += 1
            getOrderHistory(customer)
        Next

        EndTime = Timer
        Console.WriteLine("Number: " & i)
        Console.WriteLine("Execution time in seconds: ", EndTime - StartTime)

        Threading.Thread.Sleep(100000)

    End Sub

    Private Function getOrderHistory(ByVal customer As MagentoReference.customerCustomerEntity) As Boolean
        Dim returnValue As Boolean = False
        Dim StartTime As Double, EndTime As Double
        Dim auxOrder As Integer = 0
        Dim quoteId, orders, items, orderArrayTrack As Integer
        Dim customerCart As Boolean = False
        Dim orderNumbers() As Integer
        Dim cartNumbers() As Integer

        StartTime = Timer

        sessionId = GetSessionId()

        Try
            solutionsConnection.ConnectionString = solutionsConnectionString
            solutionsConnection.Open()

            Dim customerSQL As SqlCommand = New SqlCommand(solutionsSQL & "AND a.First = " & "'" & customer.firstname & "'" & " AND a.LAST = " & "'" & customer.lastname & "'" & " AND a.email_address = " & "'" & customer.email & "'", solutionsConnection)

            Try
                Dim customerReader As SqlDataReader = customerSQL.ExecuteReader()

                If customerReader.HasRows() Then
                    returnValue = True
                    orders = -1

                    While customerReader.Read()
                        If (auxOrder <> CInt(IfNull(customerReader, "aux_order_no", ""))) Then
                            Dim cartCustomer As New MagentoReference.shoppingCartCustomerEntity
                            orders += 1
                            auxOrder = CInt(IfNull(customerReader, "aux_order_no", ""))
                            ReDim orderNumbers(orders)
                            orderNumbers(orders) = auxOrder

                            quoteId = mageService.shoppingCartCreate(sessionId, CStr(2))
                            ReDim cartNumbers(orders)
                            cartNumbers(orders) = quoteId

                            cartCustomer.customer_id = customer.customer_id
                            cartCustomer.email = customer.email
                            cartCustomer.group_id = customer.group_id
                            cartCustomer.firstname = customer.firstname
                            cartCustomer.lastname = customer.lastname
                            customerCart = mageService.shoppingCartCustomerSet(sessionId, quoteId, cartCustomer, "dev_servu_en")
                        End If

                        If (customerCart) Then
                        Else
                            Console.WriteLine("Customer Cart set failed.")
                        End If

                        Console.WriteLine(IfNull(customerReader, "Date", ""))
                        Console.WriteLine(IfNull(customerReader, "aux_order_no", ""))
                        Console.WriteLine(IfNull(customerReader, "product_#", ""))
                        Console.WriteLine(IfNull(customerReader, "size", ""))
                        Console.WriteLine(IfNull(customerReader, "color", ""))
                        Console.WriteLine(IfNull(customerReader, "qty", ""))
                        'Console.WriteLine(orderResult.sku)
                    End While
                End If

                customerReader.Close()
                orderArrayTrack = 0

                For Each orderNumber In orderNumbers
                    items = -1
                    Dim innerSQL As SqlCommand = New SqlCommand(solutionsSQL & "AND b.AUX_ORDER_NO = " & "'" & orderNumber & "'", solutionsConnection)
                    Dim innerReader As SqlDataReader = innerSQL.ExecuteReader()
                    Dim productEntity() As MagentoReference.shoppingCartProductEntity

                    While innerReader.Read()
                        items += 1
                        ReDim productEntity(items)

                        productEntity(items).sku = IfNull(innerReader, "sku", "")
                        productEntity(items).qty = CDbl(IfNull(innerReader, "qty", ""))

                    End While

                    Dim shoppingCartAdd = mageService.shoppingCartProductAdd(sessionId, cartNumbers(orderArrayTrack), productEntity, CStr(2))

                    innerReader.Close()
                    orderArrayTrack += 1
                Next


            Catch e As Exception
                LogError("Solution Read Error: " & e.ToString())
            End Try

        Catch ex As Exception
            LogError("Solution Connection Error: " & ex.ToString())
        Finally
            solutionsConnection.Close()
        End Try

        EndTime = Timer
        Console.WriteLine("GET ORDER HISTORY: Execution time in seconds: ", EndTime - StartTime)

        Return returnValue

    End Function

    Public Function IfNull(Of T)(ByVal dr As SqlDataReader, ByVal fieldName As String, ByVal _default As T) As T

        If IsDBNull(dr(fieldName)) Then
            Return _default
        Else
            Return CType(dr(fieldName), T)
        End If

    End Function

    Private Function GetSessionId() As String

        If sessionId <> "" Then
            Return sessionId
        End If

        Try
            sessionId = mageService.login("dmillerapi", "5ERaL253201S")
        Catch e As Exception
            Console.WriteLine(e.ToString())
            'LogError("Session ID: " & e.ToString())
        End Try

        Return sessionId

    End Function

    Private Sub LogError(ByVal e As String)
        'Utility function to log errors
        Console.WriteLine(e.ToString())


    End Sub

    Sub sendException(ByVal subject As String, ByVal body As String)
        Dim mail As New MailMessage()
        Dim smtp As New SmtpClient("192.168.1.50")

        mail.From = New MailAddress("dustinmiller@servu-online.com")

        mail.Subject = subject
        mail.Body = body

        smtp.Send(mail)
    End Sub

End Module

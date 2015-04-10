Option Strict On

Imports System.Data.SqlClient

Module SolutionsMagentoSync
    Dim mageService As New MagentoService.Mage_Api_Model_Server_Wsi_HandlerPortTypeClient
    Dim sessionId As String

    Const CONNECTIONSTRING As String = "Server=WEBSERVER;Database=SERV-U;Trusted_Connection=Yes;User=SERVULOCAL\administrator;Password=!s3rvu3ukya"

    Sub Main()
        Dim myConn As New SqlConnection(CONNECTIONSTRING)
        Dim sqlCommand As String
        Dim productFilter As MagentoService.filters
        Dim productTypeFilter As MagentoService.complexFilter()
        Dim typeEntity As New MagentoService.associativeEntity
        Dim start_time, end_time As Date
        Dim catalogListRequest As New MagentoService.catalogProductListRequest()
        Dim catalogListResponse As New MagentoService.catalogProductListResponse
        start_time = Now()
        ' Do your stuff here '

        sessionId = GetSessionId()

        Try
            'productFilter = New MagentoService.filters()

            'productTypeFilter = New MagentoService.complexFilter(0) {}
            'typeEntity = New MagentoService.associativeEntity()

            'productTypeFilter(0) = New MagentoService.complexFilter()
            'productTypeFilter(0).key = "type"
            'typeEntity.key = "type"
            'typeEntity.value = "simple"
            'productTypeFilter(0).value = typeEntity
            'productFilter.complex_filter = productTypeFilter

            
            For counter As Integer = 0 To 200000
                catalogListRequest.sessionId = sessionId
                'catalogListRequest.filters = productFilter
                catalogListResponse = mageService.catalogProductList(catalogListRequest)
                For Each product As MagentoService.catalogProductEntity In catalogListResponse.result
                    Console.WriteLine(product.sku)
                    Console.WriteLine(counter)
                Next
            Next

        Catch e As Exception
            Console.WriteLine(e)
        End Try

        Try
            myConn.Open()
            sqlCommand = ""
            Dim myCmd As SqlCommand = New SqlCommand(sqlCommand, myConn)
            Dim productResultReader As SqlDataReader = myCmd.ExecuteReader()

            productResultReader.Close()
            myConn.Close()
        Catch e As Exception
            Console.WriteLine(e)
        End Try

        end_time = Now()

        Console.WriteLine(start_time & " " & end_time)

        Threading.Thread.Sleep(10000)
    End Sub

    Private Sub updateSku(ByVal skuNumber As String)
        Dim conn As New SqlConnection(CONNECTIONSTRING)
        Dim cmd As New SqlCommand()
        Dim transaction As SqlTransaction
        Dim sqlStatement As String

        sqlStatement = "select sku.product_#, sku.p1, skumktg.heading_subpn1, sku.size, " & _
            "skumktg.heading_subpn2, sku.color, skumktg.heading_subpn3, sku.subpn3, " & _
            "sku.status, sku.available, skumktg.ship_stnd_alone, skumktg.catalog_id, skumktg.cur_catalog_pg, " & _
            "skumktg.drop_ship_flag, skumktg.freight_class, skumktg.lbs, " & _
            "rtrim(rtrim(sku.product_#) + ' ' + rtrim(sku.size)) as subNumOne, " & _
            "rtrim(rtrim(sku.product_#) + ' ' + rtrim(sku.color)) as subNumTwo, " & _
            "rtrim(rtrim(sku.product_#) + ' ' + rtrim(sku.subpn3)) as subNumThree " & _
            "from sku LEFT OUTER JOIN skumktg on sku.product_# = skumktg.product_# " & _
            "where and skumktg.catalog_id = 'B64' and sku.status <> 'D',"

        Try
            conn.Open()
            transaction = conn.BeginTransaction()

            cmd.Connection = conn
            cmd.Transaction = transaction

            cmd.CommandText = sqlStatement
            cmd.ExecuteNonQuery()

            transaction.Commit()
            conn.Close()

        Catch e As SqlException
            transaction.Rollback()
        End Try

    End Sub

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
            Dim loginRequest As New MagentoService.loginRequest("dmillerapi", "5ERaL253201S")
            Dim loginResponse As New MagentoService.loginResponse
            loginResponse = mageService.login(loginRequest)
            sessionId = loginResponse.result
        Catch e As Exception
            Console.WriteLine(e)
        End Try

        Return sessionId

    End Function

End Module

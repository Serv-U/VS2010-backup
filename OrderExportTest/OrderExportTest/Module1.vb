Imports System.Text.RegularExpressions
Imports System.Threading
Module Module1

    Sub Main()
        'This does not properly split out the sku numbers for attributes, if the sku has just an
        '^ then it is fine, but anything with an = or a mixture of = and ^ will result in the 
        'entire sku being placed into the field for example WAYU-1275=Wood=Antiq  will show, but
        'the comments area will also have the proper splits but with the sku number.

        Dim lineItem, qtyOrdered, price, sku, comments, previousPart As String
        Dim importableAttributes As New ArrayList
        comments = ""
        lineItem = ""
        previousPart = "#FIRSTPART#"

        sku = "WAYU-1275#WOOD*^Wood=Antique White Beech"

        Dim splitSku() As String = Regex.Split(sku, "([=^#*])")

        For Each skuPart As String In splitSku
            If previousPart = "#FIRSTPART#" Then
                Console.WriteLine("First iteration: " + skuPart)
            ElseIf previousPart = Chr(61) Then
                Console.WriteLine("61 " + skuPart)
            ElseIf previousPart = Chr(94) Then
                Console.WriteLine("94 " + skuPart)
                importableAttributes.Add(skuPart)
            End If
            previousPart = skuPart
        Next


        '#TODO: There are two attribute fields that need to be ripped from the current item
        'The two lines of code that follow are simply place holders until this is established in Magento
        If importableAttributes.Count >= 1 Then
            lineItem &= CStr(importableAttributes(0))

            If importableAttributes.Count >= 2 Then
                lineItem &= CStr(importableAttributes(1))
            Else
                lineItem &= "10..............."
            End If
        Else
            lineItem &= "20                "
        End If
        Console.WriteLine(lineItem)
        Thread.Sleep(10000)
    End Sub

End Module

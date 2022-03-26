Imports System
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Module Program
    Sub Main(args As String())
        Dim pth As String
        ' Dim pth = "template.txt"
        If args Is Nothing Then
            Console.WriteLine("Please provide a path to the file and hit enter.")
            Console.Write(">")
            Console.ReadLine()
        Else
            pth = args(0)
        End If

        Dim indicatortemplate = IO.File.ReadAllText("indicatortemplate.txt")
        Dim config As JObject = JObject.Parse(IO.File.ReadAllText(pth))
        Dim qq = 1
        Dim spotmarkets = config.Item("states").Item("panes").Item("panes").Item("spot").Item("markets")
        Dim perpmarkets = config.Item("states").Item("panes").Item("panes").Item("spot").Item("markets")
        '  Dim futuresarkets = config.Item("states").Item("panes").Item("panes").Item("spot").Item("markets")
        For Each m In spotmarkets.Children
            Dim mkt As String = m.Value(Of String)()
            Dim friendlyname = mkt.Replace(":", "-") + "-spot"
            Dim ni = indicatortemplate.Replace("NM", friendlyname).Replace("MKT", mkt)
            IO.File.WriteAllText("cvd-" + friendlyname + ".txt", ni)

        Next
        For Each m In perpmarkets.Children
            Dim mkt As String = m.Value(Of String)()
            Dim friendlyname = mkt.Replace(":", "-") + "-perps"
            Dim ni = indicatortemplate.Replace("NM", friendlyname).Replace("MKT", mkt)
            IO.File.WriteAllText("cvd-" + friendlyname + ".txt", ni)
        Next

    End Sub
End Module

' ConsoleLiveMapping: Creates an object structure for a boiler, describes its mapping into OPC OPC Data Access server using 
' attributes, and then performs the live mapping. Boiler data is then read, written and/or subscribed to using plain .NET 
' object access.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.BaseLib.Runtime.InteropServices
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.LiveMapping
Imports OpcLabs.EasyOpc.DataAccess.LiveMapping.Extensions

' ReSharper disable CheckNamespace
Namespace Global.ConsoleLiveMapping
    ' ReSharper restore CheckNamespace

    Friend Class Program
        <MTAThread> ' needed for COM security initialization to succeed
        Shared Sub Main()
            ComManagement.Instance.AssureSecurityInitialization()

            Console.WriteLine()
            Console.WriteLine("Mapping our data structures to OPC...")
            Dim mapper = New DAClientMapper()
            Dim boiler1 = New Boiler()
            mapper.Map(boiler1, New DAMappingContext With {.ServerDescriptor = "OPCLabs.KitServer.2", .NodeDescriptor = New DANodeDescriptor With {.BrowsePath = "/Boilers/Boiler #1"}, .GroupParameters = 1000}) ' requested update rate (for subscriptions) -  local OPC server
            ' The NodeDescriptor below determines where in the OPC address space we want to map our data to.

            Console.WriteLine()
            Console.WriteLine("Reading all data of the boiler...")
            mapper.Read()
            Console.WriteLine("Drum level is: {0}", boiler1.Drum.LevelIndicator.Output)

            Console.WriteLine()
            Console.WriteLine("Writing new setpoint value...")
            boiler1.LevelController.SetPoint = 50.0
            Debug.Assert(boiler1.LevelController IsNot Nothing)
            mapper.WriteTarget(boiler1.LevelController, False) 'recurse:False

            Console.WriteLine()
            Console.WriteLine("Subscribing to boiler data changes...")
            mapper.Subscribe(True) 'active:True

            Thread.Sleep(30 * 1000)

            Console.WriteLine()
            Console.WriteLine("Unsubscribing from boiler data changes...")
            mapper.Subscribe(False) 'active:True

            Console.WriteLine()
            Console.WriteLine("Press Enter to continue...")
            Console.ReadLine()
        End Sub
    End Class
End Namespace

' UAConsoleLiveMapping: Creates an object structure for a boiler, describes its mapping into OPC Unified Architecture server 
' using attributes, and then performs the live mapping. Boiler data is then read, written and/or subscribed to using plain .NET 
' object access.
'
' Find all latest examples here: https://www.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://forum.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Threading
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.LiveMapping
Imports OpcLabs.EasyOpc.UA.LiveMapping.Extensions
Imports OpcLabs.EasyOpc.UA.Navigation
Imports OpcLabs.EasyOpc.UA.OperationModel

Namespace Global.UAConsoleLiveMapping

    Friend Class Program
        Shared Sub Main()
            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Console.WriteLine()
            Console.WriteLine("Mapping our data structures to OPC...")
            Dim mapper = New UAClientMapper()
            Dim boiler1 = New Boiler()
            ' The NodeDescriptor below determines where in the OPC address space we want to map our data to.
            ' '#' is a reserved character in a browse name, and must be escaped by '&' in the path below.
            mapper.Map(boiler1, New UAMappingContext With {
                      .EndpointDescriptor = endpointDescriptor,
                      .NodeDescriptor = New UANodeDescriptor With {
                      .BrowsePath = UABrowsePath.Parse("[ObjectsFolder]/Boilers/Boiler &#1", "http://opcfoundation.org/UA/Boiler/")},
                      .MonitoringParameters = 1000}) ' requested sampling interval (for subscriptions) -  local OPC server

            Console.WriteLine()
            Console.WriteLine("Starting the simulation of the boiler in the server, using an OPC method call...")
            ' Currently there is no live mapping for OPC methods, therefore we call the OPC method in a traditional way.
            Try
                EasyUAClient.SharedInstance.CallMethod(
                endpointDescriptor,
                UABrowsePath.Parse("[ObjectsFolder]/Boilers/Boiler &#1/Simulation", "http://opcfoundation.org/UA/Boiler/"),
                UABrowsePath.Parse("[nsu=http://opcfoundation.org/UA/Boiler/;i=1287].Start", "http://opcfoundation.org/UA/"))
            Catch e1 As UAException
                ' Production code would test the current state of the simulation first, and also handle the exception here.
            End Try

            Console.WriteLine()
            Console.WriteLine("Reading all data of the boiler...")
            mapper.Read()
            Console.WriteLine("Drum level is: {0}", boiler1.Drum.LevelIndicator.Output)

            Console.WriteLine()
            Console.WriteLine("Writing new setpoint value...")
            boiler1.LevelController.SetPoint = 50.0
            Debug.Assert(boiler1.LevelController IsNot Nothing)
            mapper.WriteTarget(boiler1.LevelController, False) 'recurse:

            Console.WriteLine()
            Console.WriteLine("Subscribing to boiler data changes...")
            mapper.Subscribe(True) 'active:

            Thread.Sleep(30 * 1000)

            Console.WriteLine()
            Console.WriteLine("Unsubscribing from boiler data changes...")
            mapper.Subscribe(False) 'active:

            Console.WriteLine()
            Console.WriteLine("Press Enter to continue...")
            Console.ReadLine()
        End Sub
    End Class
End Namespace
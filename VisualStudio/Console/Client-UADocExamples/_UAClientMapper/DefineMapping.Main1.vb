' $Header: $ 
' Copyright (c) CODE Consulting and Development, s.r.o., Plzen. All rights reserved.

' ReSharper disable CheckNamespace
' ReSharper disable LocalizableElement
#Region "Example"
' This example for OPC UA type-less mapping shows how to define a mapping and perform a read operation.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports OpcLabs.BaseLib.ComponentModel.Linking
Imports OpcLabs.EasyOpc.UA
Imports OpcLabs.EasyOpc.UA.LiveMapping

Namespace Global.UADocExamples._UAClientMapper
    Friend Class DefineMapping
        Class MyClass2
            Public Property Value As Object
        End Class

#Region "Example-DefineAndRead"
        Public Shared Sub Main1()

            ' Define which server we will work with.
            Dim endpointDescriptor As UAEndpointDescriptor =
                    "opc.tcp://opcua.demo-this.com:51210/UA/SampleServer"
            ' or "http://opcua.demo-this.com:51211/UA/SampleServer" (currently not supported)
            ' or "https://opcua.demo-this.com:51212/UA/SampleServer/"

            Dim mapper = New UAClientMapper()
            Dim target = New MyClass2()

            ' Define a type-less mapping.

            Dim memberInfo = target.GetType().GetMember("Value").SingleOrDefault()
            Debug.Assert(memberInfo IsNot Nothing)

            mapper.DefineMapping(
                New UAClientDataMappingSource(
                    endpointDescriptor,
                    "nsu=http://test.org/UA/Data/ ;i=10389",
                    UAAttributeId.Value,
                    UAIndexRangeList.Empty,
                    UAReadParameters.CacheMaximumAge),
                New UAClientDataMapping(GetType(Int32)),
                New ObjectMemberLinkingTarget(target.GetType(), target, memberInfo))

            ' Perform a read operation.
            mapper.Read()

            ' Display results
            Console.WriteLine(target.Value)
        End Sub
#End Region
    End Class
End Namespace

#End Region

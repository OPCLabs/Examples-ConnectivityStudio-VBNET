' WebService1: A simple Web service using ASMX technology. Provides "Hello World" method to read a value of an OPC item.
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.Web.Services
Imports OpcLabs.EasyOpc.DataAccess
Imports OpcLabs.EasyOpc.DataAccess.Extensions

''' <summary>
''' Summary description for Service1
''' </summary>
<WebService(Namespace:="http://tempuri.org/"), WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1), ComponentModel.ToolboxItem(False)> _
Public Class Service1
    Inherits WebService
    ' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    ' [System.Web.Script.Services.ScriptService]

    Shared Sub New()
        ' Enable auto-subscribing optimization (not necessary), which can improve performance with repeated Read requests.
        Client.TryEnableAutoSubscribingOptimization()
    End Sub

    ' Use a shared client instance to allow for better optimization.
    Shared ReadOnly Client As New EasyDAClient

    <WebMethod()> _
    Public Function HelloWorld() As String
        ' The following call may throw an OpcException, which would be reported to the client.
        ' Production-quality code may need to catch the OpcException And handle it differently.
        Dim value As Object = Client.ReadItemValue("", "OPCLabs.KitServer.2", "Demo.Ramp")
        Return If(value Is Nothing, "", value.ToString())
    End Function
End Class
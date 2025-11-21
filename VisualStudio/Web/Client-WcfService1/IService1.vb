
'
' Find all latest examples here: https://opclabs.doc-that.com/files/onlinedocs/OPCLabs-ConnectivityStudio/Latest/examples.html .
' OPC client and subscriber examples in VB.NET on GitHub: https://github.com/OPCLabs/Examples-ConnectivityStudio-VBNET .
' Missing some example? Ask us for it on our Online Forums, https://www.opclabs.com/forum/index ! You do not have to own
' a commercial license in order to use Online Forums, and we reply to every post.

Imports System.ServiceModel


' NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in Web.config.
<ServiceContract()> _
Public Interface IService1

    <OperationContract()> _
    Function GetData() As String
End Interface
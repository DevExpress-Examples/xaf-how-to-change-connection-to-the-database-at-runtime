Imports System.Collections.Generic

Namespace RuntimeDbChooser.Services

    Public Interface IConnectionStringHelper

        Function GetConnectionStringsMap() As IDictionary(Of String, String)

    End Interface
End Namespace

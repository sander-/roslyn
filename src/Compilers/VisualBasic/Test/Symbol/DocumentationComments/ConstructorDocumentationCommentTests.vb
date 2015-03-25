﻿' Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

Imports Microsoft.CodeAnalysis.Text
Imports Microsoft.CodeAnalysis.VisualBasic.Symbols
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Namespace Microsoft.CodeAnalysis.VisualBasic.UnitTests
    Public Class ConstructorDocumentationCommentTests

        Private _compilation As VisualBasicCompilation
        Private _acmeNamespace As NamespaceSymbol
        Private _widgetClass As NamedTypeSymbol

        Public Sub New()
            _compilation = CompilationUtils.CreateCompilationWithMscorlib(
                <compilation name="ConstructorDocumentationCommentTests">
                    <file name="a.vb">
                    Namespace Acme
                        Class Widget
                            Shared Sub New()
                            End Sub

                            Public Sub New()
                            End Sub

                            Public Sub New(s As String)
                            End Sub
                        End Class
                    End Namespace
                    </file>
                </compilation>)

            _acmeNamespace = DirectCast(_compilation.GlobalNamespace.GetMembers("Acme").Single(), NamespaceSymbol)
            _widgetClass = DirectCast(_acmeNamespace.GetTypeMembers("Widget").Single(), NamedTypeSymbol)
        End Sub

        <Fact>
        Public Sub TestSharedConstructor()
            Assert.Equal("M:Acme.Widget.#cctor",
                         _widgetClass.GetMembers()(0).GetDocumentationCommentId())
        End Sub

        <Fact>
        Public Sub TestInstanceConstructor1()
            Assert.Equal("M:Acme.Widget.#ctor",
                         _widgetClass.GetMembers()(1).GetDocumentationCommentId())
        End Sub

        <Fact>
        Public Sub TestInstanceConstructor2()
            Assert.Equal("M:Acme.Widget.#ctor(System.String)",
                         _widgetClass.GetMembers()(2).GetDocumentationCommentId())
        End Sub

    End Class
End Namespace

﻿' Licensed to the .NET Foundation under one or more agreements.
' The .NET Foundation licenses this file to you under the MIT license.
' See the LICENSE file in the project root for more information.

Imports System.Composition
Imports Microsoft.CodeAnalysis.CodeRefactorings
Imports Microsoft.CodeAnalysis.CodeRefactorings.AddAwait
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

Namespace Microsoft.CodeAnalysis.VisualBasic.CodeRefactorings.AddAwait
    <ExportCodeRefactoringProvider(LanguageNames.VisualBasic, Name:=PredefinedCodeRefactoringProviderNames.AddAwait), [Shared]>
    Friend Class VisualBasicAddAwaitCodeRefactoringProvider
        Inherits AbstractAddAwaitCodeRefactoringProvider(Of InvocationExpressionSyntax)

        <ImportingConstructor>
        Public Sub New()
        End Sub

        Protected Overrides Function GetTitle() As String
            Return VBFeaturesResources.Add_Await
        End Function

        Protected Overrides Function GetTitleWithConfigureAwait() As String
            Return VBFeaturesResources.Add_Await_and_ConfigureAwaitFalse
        End Function
    End Class
End Namespace

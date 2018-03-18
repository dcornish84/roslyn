﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System.Linq;
using Microsoft.CodeAnalysis.Host;
using Microsoft.CodeAnalysis.Host.Mef;

namespace Microsoft.CodeAnalysis.Remote
{
    /// <summary>
    /// Various Roslyn services provider.
    /// 
    /// TODO: change all these services to WorkspaceServices
    /// </summary>
    internal sealed class RoslynServices
    {
        // TODO: probably need to split this to private and public services
        public static readonly HostServices HostServices = MefHostServices.Create(
            MefHostServices.DefaultAssemblies
                // This adds the exported MEF services from Workspaces.Desktop
                .Add(typeof(Host.TemporaryStorageServiceFactory.TemporaryStorageService).Assembly)
                // This adds the exported MEF services from the RemoteWorkspaces assembly.
                .Add(typeof(RoslynServices).Assembly)
                .Add(typeof(CSharp.CodeLens.CSharpCodeLensDisplayInfoService).Assembly)
                .Add(typeof(VisualBasic.CodeLens.VisualBasicDisplayInfoService).Assembly));

        private readonly int _scopeId;

        public RoslynServices(int scopeId, AssetStorage storage, HostServices hostServices)
        {
            _scopeId = scopeId;

            var mefHostExportProvider = (IMefHostExportProvider)hostServices;
            var primaryWorkspace = mefHostExportProvider.GetExports<PrimaryWorkspace>().Single().Value;
            var workspace = (RemoteWorkspace)primaryWorkspace.Workspace ?? new RemoteWorkspace();

            AssetService = new AssetService(_scopeId, storage, workspace);
            SolutionService = new SolutionService(AssetService, workspace);
            CompilationService = new CompilationService(SolutionService);
        }

        public AssetService AssetService { get; }
        public SolutionService SolutionService { get; }
        public CompilationService CompilationService { get; }
    }
}

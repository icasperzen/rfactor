﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using Roslyn.Services.Editor;
using Roslyn.Services;
using Roslyn.Compilers.Common;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rfactor
{
    public class RenameServiceActionEdit : ICodeActionEdit
    {
        private IWorkspace workspace;
        private IRenameService renameService;
        private IDocument document;
        private ISymbol symbol;

        public RenameServiceActionEdit(IWorkspace workspace, IRenameService renameService,
            IDocument document, ISymbol symbol)
        {
            this.workspace = workspace;
            this.renameService = renameService;
            this.document = document;
            this.symbol = symbol;
        }

        public Task ApplyAsync(IWorkspace workspce,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            // This function will be called when the user actually clicks to apply the code action.
            // Perform the rename here.
            GuiProvider gui = new GuiProvider(symbol.Name.ToString());
            bool cancelRename = gui.GetGUI();

            if (cancelRename)
            {
                return Task.Factory.StartNew(() =>
                    {
                        CancelRequest();
                    });
            }
            else
            {
                return Task.Factory.StartNew(() =>
                    renameService.RenameSymbol(workspace, document.Project.Solution,
                        symbol, gui.Name)); //"WTF_" + symbol.Name.ToUpper()));
            }
        }

        public object GetPreview(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Return null since we don't have a way to preview the changes that the
            // rename service will perform.
            return null;
        }

        public void CancelRequest() {}
    }
}

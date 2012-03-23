﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Roslyn.Compilers;
using Roslyn.Compilers.Common;
using Roslyn.Compilers.CSharp;
using Roslyn.Services;
using Roslyn.Services.Editor;
using Rfactor.Lib;
using Rfactor.UnitTests.Stubs;

namespace Rfactor.UnitTests
{

    [TestFixture]
    class ContextFactoryTest
    {

        Context ctx;

        [SetUp]
        public void InitializeWithSampleApp()
        {
            IWorkspace workspace = Workspace.LoadSolution(@"../../TestFiles/SampleApplication.sln");
            ISolution solution = workspace.CurrentSolution;
            ctx = new Context(workspace, solution);
        }

        /*[Test]
        public void InitializeWithStubs()
        {
            IWorkspaceStub iworkstub = null;
            ISolutionStub isolstub = null;
            IDocumentStub idocstub = null;
            try
            {
                iworkstub = new IWorkspaceStub();
                isolstub = new ISolutionStub();
                idocstub = new IDocumentStub();
            }
            catch (NotImplementedException e)
            {
            }

            ctx = new Context(iworkstub, isolstub, idocstub);
        }*/

        [Test]
        public void VerifyInitialization()
        {
            Assert.IsInstanceOf<Context>(ctx);
            Assert.NotNull(ctx);
        }

        [Test]
        public void TestGetIWorkspace()
        {
            var ws = ctx.GetIWorkspace();
            Assert.IsInstanceOf<IWorkspace>(ws);
            Assert.NotNull(ws);
        }

        [Test]
        public void TestGetISolution()
        {
            var isol = ctx.GetISolution();
            Assert.IsInstanceOf<ISolution>(isol);
            Assert.NotNull(isol);
        }

        [Test]
        public void TestGetNullIDocument()
        {
            var idoc = ctx.getIDocument();
            Assert.Null(idoc);
        }

        [Test]
        public void TestAllFunctions()
        {
            var a = ctx.allFunctions();
            Assert.True(a.Any( (val) =>
                {
                    return val.Name == "Rename";
                }));
        }

        [Test]
        public void TestAllVariables()
        {
            var a = ctx.allVariables();
            Assert.True(a.Any( (val) =>
                {
                    return val.Name == "message";
                }));
        }

        [Test]
        public void TestVarNameCollisionP_ClassProgramScope()
        {
            var allT = ctx.allTypes();
            allT = allT.Where((val) =>
                {
                    return val.Name == "Program";
                });
            var program_scope = allT.Single();
            Assert.True(ctx.varNameCollisionP("message",program_scope));
            Assert.False(ctx.varNameCollisionP("thisVariableDoesNotExist",program_scope));
        }

        [Test]
        public void TestVarNameCollisionP_MethodRenameScope()
        {
            var allF = ctx.allFunctions();
            allF = allF.Where((val) =>
                {
                    return val.Name == "Rename";
                });
            var rename_scope = allF.Single();
            Assert.True(ctx.varNameCollisionP("Message",rename_scope));
            Assert.False(ctx.varNameCollisionP("thisVariableDoesNotExist",rename_scope));
        }

    }

}

﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace RoslynQueryGenerator
{
    [Generator]
    public class GeneratorQuery : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            //Debugger.Launch();

        }
        public void Execute(GeneratorExecutionContext context)
        {
            string newExec = "test";
            try
            {
                string namespaceName = context.Compilation?.AssemblyName;
                var g = new GeneratorQueryFromFile(context, namespaceName);
                newExec = "GenerateContext";
                var cnt = g.GenerateContext();
                context.AddSource($"context.gen.cs", SourceText.From(cnt, Encoding.UTF8));
                newExec = "GenerateExtensions";
                var ext = g.GenerateExtensions();
                context.AddSource($"extension.gen.cs", SourceText.From(ext, Encoding.UTF8));
                newExec = "GenerateController";
                var cont = g.GenerateController();
                context.AddSource($"controller.gen.cs", SourceText.From(cont, Encoding.UTF8));

                newExec = "GenerateFind";
                var find = g.GenerateFind();
                context.AddSource("find.gen.cs",SourceText.From(find, Encoding.UTF8));

            }
            catch (Scriban.Syntax.ScriptRuntimeException sc)
            {
                string s = sc.Message;
                var dd = new DiagnosticDescriptor(newExec, $"StartExecution", $"{sc.Message}", newExec, DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
                context.ReportDiagnostic(d);
            }
        
            catch (Exception ex)
            {
                string s = ex.Message;
                var dd = new DiagnosticDescriptor(newExec, $"StartExecution", $"{ex.Message}", newExec, DiagnosticSeverity.Error, true);
                var d = Diagnostic.Create(dd, Location.None, "andrei.txt");
                context.ReportDiagnostic(d);
            }
        }
    }
}
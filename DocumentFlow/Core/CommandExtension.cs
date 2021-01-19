//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.12.2020
// Time: 19:58
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using Inflector;
using DocumentFlow.Code;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Core
{
    public static class CommandExtension
    {
        private static readonly Dictionary<Command, CodeContent> codes = new Dictionary<Command, CodeContent>();

        public static IBrowserCode Browser(this Command command) => Task.Run(() => command.Compile()).Result.Browser;

        public static IEditorCode Editor(this Command command) => Task.Run(() => command.Compile()).Result.Editor;

        public static CodeContent Compile(this Command command)
        {
            lock(command.Locker)
            {
                if (string.IsNullOrEmpty(command.script))
                {
                    throw new EmptyCodeException($"Отсутствует код для создания окна просмотра: {command}.");
                }

                if (codes.ContainsKey(command) && codes[command].Compiled)
                {
                    return codes[command];
                }

                string dll = command.EntityKind == null ? command.code.Replace('-', '_').Pascalize() : command.EntityKind.code.Pascalize();

                CSharpCompilationOptions options = new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
#if DEBUG
                    .WithOptimizationLevel(OptimizationLevel.Debug);
#else
                    .WithOptimizationLevel(OptimizationLevel.Release);
#endif

                string binPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                MetadataReference[] references = new MetadataReference[]
                {
                    MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.ComponentModel.INotifyPropertyChanged).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Data.IDbConnection).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Windows.Forms.Form).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(Enumerable).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(System.Drawing.Color).GetTypeInfo().Assembly.Location),
                    MetadataReference.CreateFromFile(Path.Combine(binPath, "Dapper.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(binPath, "DocumentFlow.Core.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(binPath, "DocumentFlow.Data.Core.dll")),
                    MetadataReference.CreateFromFile(Path.Combine(binPath, "DocumentFlow.Code.dll"))
                };

                string assemblyInfo = @"
                    using System.Reflection;
                    [assembly: AssemblyVersion(""2.4.0.0"")]
                ";

                CSharpCompilation compilation = CSharpCompilation.Create(dll, references: references)
                    .WithOptions(options)
                    .AddSyntaxTrees(
                        CSharpSyntaxTree.ParseText(assemblyInfo),
                        CSharpSyntaxTree.ParseText(command.script)
                        );

                using (var ms = new MemoryStream())
                {
#if DEBUG
                    using (var pdb = new MemoryStream())
                    {
                        EmitResult emitResult = compilation.Emit(ms, pdb);
#else
                        EmitResult emitResult = compilation.Emit(ms);
#endif
                        if (emitResult.Success)
                        {
                            ms.Seek(0, SeekOrigin.Begin);
#if DEBUG
                            Assembly assembly = Assembly.Load(ms.ToArray(), pdb.ToArray());
#else
                            Assembly assembly = Assembly.Load(ms.ToArray());
#endif
                            try
                            {
                                Type[] types = assembly.GetTypes();
                                Type type = types.Where(x => x.GetInterface(nameof(IBrowserCode)) != null).SingleOrDefault();

                                if (type == null)
                                {
                                    throw new MissingImpException($"Не найдена реализация интерфейса {nameof(IBrowserCode)}");
                                }

                                IBrowserCode browser = Activator.CreateInstance(type) as IBrowserCode;
                                IEditorCode editor = null;
                                if (browser is IDataEditor dataEditor)
                                {
                                    editor = dataEditor.CreateEditor();
                                }

                                if (codes.ContainsKey(command))
                                {
                                    codes[command] = (browser, editor);
                                }
                                else
                                {
                                    codes.Add(command, (browser, editor));
                                    command.PropertyChanged += Command_PropertyChanged;
                                }
                            }
                            catch (InvalidOperationException e)
                            {
                                throw new InvalidOperationException($"Код отвечающий за создание окна просмотра имеет более одного класса реализующего {nameof(IBrowserCode)} или {nameof(IEditorCode)}.", e);
                            }

                            return codes[command];
                        }
                        else
                        {
                            // some errors
                            IEnumerable<Diagnostic> failures = emitResult.Diagnostics.Where(diagnostic =>
                                diagnostic.IsWarningAsError ||
                                diagnostic.Severity == DiagnosticSeverity.Error);

                            throw new CompilerException(failures);
                        }
#if DEBUG
                    }
#endif
                }
            }
        }

        private static void Command_PropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "script" && sender is Command command && codes.ContainsKey(command))
            {
                codes[command].Reset();
            }
        }
    }
}

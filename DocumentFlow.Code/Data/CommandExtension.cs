//-----------------------------------------------------------------------
// Copyright © 2010-2020 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 09.12.2020
// Time: 19:58
//-----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using Inflector;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Data
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

                // Хак для запуска c# 8.0
                // https://www.rsdn.org/forum/dotnet/7427760
                //
                var compiler = new CSharpCodeProvider(new Dictionary<string, string>
                {
                    ["CompilerDirectoryPath"] = @"c:\Program Files (x86)\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\Roslyn",
                });

                var parameters = new CompilerParameters
                {
                    GenerateInMemory = true,
                    GenerateExecutable = false,
#if DEBUG
                    OutputAssembly = command.EntityKind == null ? command.code.Replace('-', '_').Pascalize() : command.EntityKind.code.Pascalize(),
                    IncludeDebugInformation = true,
#endif
                    CompilerOptions = " -langversion:8.0 -optimize+ "
                };

#if DEBUG
                parameters.CompilerOptions += $" -embed ";
#endif

                parameters.ReferencedAssemblies.AddRange(new string[] {
                    "System.dll",
                    "System.Core.dll",
                    "System.Data.dll",
                    "System.Drawing.dll",
                    "System.Windows.Forms.dll",
                    "Dapper.dll",
                    "DocumentFlow.Code.dll",
                    "DocumentFlow.Core.dll",
                    "DocumentFlow.Data.Entities.dll" });

                var csprovider = new CSharpCodeProvider();

                string code;
                int ns_idx = command.script.IndexOf("namespace");
                if (ns_idx > 0)
                {
                    StringBuilder builder = new StringBuilder();

                    string using_text = command.script.Substring(0, ns_idx);


                    builder.AppendLine(using_text);

                    if (using_text.IndexOf("System.Reflection") == -1)
                    {
                        builder.AppendLine("using System.Reflection;");
                    }

                    builder.AppendLine("[assembly: DatabaseScript()]");
                    builder.AppendLine("[assembly: AssemblyVersion(\"1.0.0.0\")]");

                    builder.Append(command.script.Substring(ns_idx));
                    code = builder.ToString();
                }
                else
                {
                    code = command.script;
                }

                CompilerResults results = compiler.CompileAssemblyFromSource(parameters, code);
                if (results.Errors.HasErrors)
                {
                    var sb = new StringBuilder();

                    foreach (CompilerError error in results.Errors)
                    {
                        sb.AppendLine($"Error ({error.ErrorNumber}) at line {error.Line}: {error.ErrorText}");
                    }

                    throw new CompilerException(sb.ToString(), results.Errors);
                }

                Assembly assembly = results.CompiledAssembly;
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

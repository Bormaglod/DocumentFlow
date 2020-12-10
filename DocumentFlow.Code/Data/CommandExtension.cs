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
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Entities;

namespace DocumentFlow.Code.Data
{
    public static class CommandExtension
    {
        private static readonly Dictionary<Command, CodeContent> codes = new Dictionary<Command, CodeContent>();

        public static IBrowserCode Browser(this Command command) => Task.Run(() => command.Compile()).Result.browser;

        public static IEditorCode Editor(this Command command) => Task.Run(() => command.Compile()).Result.editor;

        public static CodeContent Compile(this Command command)
        {
            lock(command.Locker)
            {
                if (string.IsNullOrEmpty(command.script))
                {
                    throw new EmptyCodeException($"Отсутствует код для создания окна просмотра: {command}.");
                }

                if (!command.IsChanged && codes.ContainsKey(command))
                {
                    return codes[command];
                }

                var compiler = new CSharpCodeProvider();
                var parameters = new CompilerParameters();

                parameters.ReferencedAssemblies.AddRange(new string[] {
                    "System.dll",
                    "System.Core.dll",
                    "System.Data.dll",
                    "System.Drawing.dll",
                    "System.Windows.Forms.dll",
                    "Dapper.dll",
                    "DocumentFlow.Code.dll",
                    "DocumentFlow.Core.dll" });

                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                CompilerResults results = compiler.CompileAssemblyFromSource(parameters, command.script);
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
                    IEditorCode editor = browser.CreateEditor();

                    if (codes.ContainsKey(command))
                    {
                        codes[command] = (browser, editor);
                    }
                    else
                    {
                        codes.Add(command, (browser, editor));
                    }

                    command.IsChanged = false;
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException($"Код отвечающий за создание окна просмотра имеет более одного класса реализующего {nameof(IBrowserCode)} или {nameof(IEditorCode)}.", e);
                }

                return codes[command];
            }
        }
    }
}

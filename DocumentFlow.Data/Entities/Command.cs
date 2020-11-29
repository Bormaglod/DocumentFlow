//-----------------------------------------------------------------------
// Copyright © 2010-2019 Тепляшин Сергей Васильевич. 
// Contacts: <sergio.teplyashin@gmail.com>
// License: https://opensource.org/licenses/GPL-3.0
// Date: 10.03.2019
// Time: 18:43
//-----------------------------------------------------------------------

using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using DocumentFlow.Code;
using DocumentFlow.Core.Exceptions;
using DocumentFlow.Data.Core;


namespace DocumentFlow.Data.Entities
{
    public class Command : EntityUID
    {
        private object locker = new object();
        private bool needCompile = false;
        private string scriptCode;
        private IBrowserCode browser;
        private IEditorCode editor;

        public string code { get; set; }
        public string name { get; set; }
        public Guid? parent_id { get; set; }
        public Guid? picture_id { get; set; }
        public string note { get; set; }
        public Guid? entity_kind_id { get; set; }
        public string script
        {
            get => scriptCode;
            set
            {
                scriptCode = value;
                needCompile = true;
            }
        }

        public Picture Picture { get; set; }
        public EntityKind EntityKind { get; set; }

        public IBrowserCode Browser
        { 
            get
            {
                Task.Run(() => Compile()).Wait();
                return browser;
            }
        }

        public IEditorCode Editor
        {
            get
            {
                Task.Run(() => Compile()).Wait();
                return editor;
            }
        }

        public void Compile()
        {
            lock (locker)
            {
                if (string.IsNullOrEmpty(scriptCode))
                {
                    throw new EmptyCodeException($"Отсутствует код для создания окна просмотра: {ToString()}.");
                }

                if (!needCompile)
                {
                    return;
                }

                browser = null;
                editor = null;

                var compiler = new CSharpCodeProvider();
                CompilerParameters parameters = new CompilerParameters();

                parameters.ReferencedAssemblies.Add("System.Core.dll");
                parameters.ReferencedAssemblies.Add("System.Data.dll");
                parameters.ReferencedAssemblies.Add("System.Drawing.dll");
                parameters.ReferencedAssemblies.Add("Dapper.dll");
                parameters.ReferencedAssemblies.Add("DocumentFlow.Core.dll");
                parameters.ReferencedAssemblies.Add("DocumentFlow.Code.dll");
                parameters.GenerateInMemory = true;
                parameters.GenerateExecutable = false;

                CompilerResults results = compiler.CompileAssemblyFromSource(parameters, scriptCode);
                if (results.Errors.HasErrors)
                {
                    StringBuilder sb = new StringBuilder();

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

                    browser = Activator.CreateInstance(type) as IBrowserCode;
                    editor = browser.CreateEditor();

                    needCompile = false;
                }
                catch (InvalidOperationException e)
                {
                    throw new InvalidOperationException($"Код отвечающий за создание окна просмотра имеет более одного класса реализующего {nameof(IBrowserCode)} или {nameof(IEditorCode)}.", e);
                }
            }
        }

        public override string ToString() => $"{name} ({code})";
    }
}

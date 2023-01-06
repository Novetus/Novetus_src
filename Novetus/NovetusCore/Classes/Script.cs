#region Usings
using System;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
#endregion

// based on https://stackoverflow.com/questions/137933/what-is-the-best-scripting-language-to-embed-in-a-c-sharp-desktop-application
namespace Novetus.Core
{
    #region IExtension
    public class IExtension
    {
        public virtual string Name() { return "Unnamed Object"; }
        public virtual string Version() { return "1.0.0"; }
        public virtual string FullInfoString() { return (Name() + " v" + Version()); }
        public virtual void OnExtensionLoad() { }
        public virtual void OnExtensionUnload() { }
    }
    #endregion

    #region Script
    public class Script
    {
        public static object LoadScriptFromContent(string scriptPath)
        {
            try
            {
                using (var stream = File.OpenRead(scriptPath))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        string script = reader.ReadToEnd();
                        Assembly compiled = CompileScript(script, scriptPath);
                        object code = ExecuteScript(compiled, scriptPath);
                        return code;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler(scriptPath + ": " + ex.ToString());
            }

            return null;
        }

        private static object ExecuteScript(Assembly assemblyScript, string filePath)
        {
            if (assemblyScript == null)
            {
                goto error;
            }

            foreach (Type type in assemblyScript.GetExportedTypes())
            {
                if (type.IsInterface || type.IsAbstract)
                    continue;

                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                if (constructor != null && constructor.IsPublic)
                {
                    return constructor.Invoke(null);
                }
                else
                {
                    ErrorHandler(filePath + ": Constructor does not exist or it is not public.");
                    return null;
                }
            }

error:
            ErrorHandler(filePath + ": Failed to load script.");
            return null;
        }

        private static Assembly CompileScript(string code, string filePath)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();

            CompilerParameters perams = new CompilerParameters();
            perams.GenerateExecutable = false;
            perams.GenerateInMemory = true;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).Select(a => a.Location);
            perams.ReferencedAssemblies.AddRange(assemblies.ToArray());

            CompilerResults result = provider.CompileAssemblyFromSource(perams, code);

            foreach (CompilerError error in result.Errors)
            {
                ErrorHandler(error, filePath);
            }

            if (result.Errors.HasErrors)
            {
                return null;
            }

            return result.CompiledAssembly;
        }

        private static void ErrorHandler(string error)
        {
            Util.ConsolePrint("[SCRIPT ERROR] - " + error, 2);
        }

        private static void ErrorHandler(CompilerError error, string fileName)
        {
            Util.ConsolePrint("[SCRIPT ERROR] - " + fileName + " (" + error.Line + "," + error.Column + "): " + error.ErrorText, 2);
        }
    }
    #endregion
}

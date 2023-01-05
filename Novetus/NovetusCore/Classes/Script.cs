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
                ErrorHandler(scriptPath + ": " + ex.ToString(), true);
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
                    ErrorHandler(filePath + ": Constructor does not exist or it is not public.", true);
                    return null;
                }
            }

error:
            ErrorHandler(filePath + ": Failed to load script.", true);
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
                ErrorHandler(error, filePath, error.IsWarning);
            }

            if (result.Errors.HasErrors)
            {
                return null;
            }

            return result.CompiledAssembly;
        }

        public static void ErrorHandler(string error)
        {
            ErrorHandler(error, false);
        }

        private static void ErrorHandler(string error, bool warning)
        {
            Util.ConsolePrint(warning ? "[SCRIPT WARNING] - " : "[SCRIPT ERROR] - " + error, warning ? 5 : 2);
        }

        private static void ErrorHandler(CompilerError error, string fileName, bool warning)
        {
            Util.ConsolePrint(warning ? "[SCRIPT WARNING] - " : "[SCRIPT ERROR] - " + fileName + " (" + error.Line + "," + error.Column + "): " + error.ErrorText, warning ? 5 : 2);
        }
    }
    #endregion
}

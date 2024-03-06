#region Usings
using System;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Linq;
using System.Collections.Generic;
#endregion

// based on https://stackoverflow.com/questions/137933/what-is-the-best-scripting-language-to-embed-in-a-c-sharp-desktop-application
namespace Novetus.Core
{
    #region IExtension
    public class IExtension
    {
        public virtual string Name() { return "Unnamed Object"; }
        public virtual string Version() { return "1.0.0"; }
        public virtual string Author() { return GlobalVars.UserConfiguration.ReadSetting("PlayerName"); }
        public virtual string FullInfoString() { return (Name() + " v" + Version() + " by " + Author()); }
        public virtual void OnExtensionLoad() { }
        public virtual void OnExtensionUnload() { }
    }
    #endregion

    #region ExtensionManager
    public class ExtensionManager
    {
        private List<IExtension> ExtensionList = new List<IExtension>();
        private string directory = "";

        public ExtensionManager() 
        { 
        }

        public virtual List<IExtension> GetExtensionList()
        {
            return ExtensionList;
        }

        public virtual void LoadExtensions(string dirPath)
        {
            string nothingFoundError = "No extensions found.";

            if (!Directory.Exists(dirPath))
            {
                Util.ConsolePrint(nothingFoundError, 5);
                return;
            }
            else
            {
                directory = dirPath;
            }

            // load up all .cs files.
            string[] filePaths = Directory.GetFiles(dirPath, "*.cs", SearchOption.TopDirectoryOnly);

            if (filePaths.Count() == 0)
            {
                Util.ConsolePrint(nothingFoundError, 5);
                return;
            }

            foreach (string file in filePaths)
            {
                int index = 0;

                try
                {
                    IExtension newExt = (IExtension)ExtensionScript.LoadScriptFromContent(file);
                    ExtensionList.Add(newExt);
                    index = ExtensionList.IndexOf(newExt);
                    Util.ConsolePrint("Loaded extension " + newExt.FullInfoString() + " from " + Path.GetFileName(file), 3);
                    newExt.OnExtensionLoad();
                }
                catch (Exception)
                {
                    Util.ConsolePrint("Failed to load script " + Path.GetFileName(file), 2);
                    ExtensionList.RemoveAt(index);
                    continue;
                }
            }
        }

        public virtual void ReloadExtensions()
        {
            string nothingFoundError = "No extensions found. There is nothing to reload.";

            if (!ExtensionList.Any())
            {
                Util.ConsolePrint(nothingFoundError, 5);
                return;
            }

            Util.ConsolePrint("Reloading Extensions...", 2);

            UnloadExtensions();
            LoadExtensions(directory);
        }

        public virtual void UnloadExtensions()
        {
            string nothingFoundError = "No extensions found. There is nothing to unload.";

            if (!ExtensionList.Any())
            {
                Util.ConsolePrint(nothingFoundError, 5);
                return;
            }

            Util.ConsolePrint("Unloading all Extensions...", 2);

            foreach (IExtension extension in ExtensionList.ToArray())
            {
                try
                {
                    extension.OnExtensionUnload();
                }
                catch (Exception)
                {
                }
            }

            ExtensionList.Clear();
        }

        public virtual string GenerateExtensionList()
        {
            string nothingFoundError = "No extensions found.";

            if (!ExtensionList.Any())
            {
                return nothingFoundError;
            }

            string result = "";

            foreach (IExtension extension in ExtensionList.ToArray())
            {
                try
                {
                    result += "- " + extension.FullInfoString() + "\n";
                }
                catch (Exception)
                {
                }
            }

            result.Trim();
            return result;
        }
    }
    #endregion

    #region ExtensionScript
    public class ExtensionScript
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
                if (error.IsWarning)
                    continue;

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

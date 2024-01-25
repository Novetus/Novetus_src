using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Novetus.Core
{
    public class JSONFile
    {
        public string path;
        public Dictionary<string, string> defContents = null;
        public JObject obj;

        public JSONFile(string JSONPath, string section, bool createNewFile = true, Dictionary<string, string> contents = null)
        {
            path = JSONPath;
            obj = new JObject();

            if (contents != null)
            {
                defContents = contents;
            }

            if (createNewFile)
            {
                if (contents != null)
                {
                    JsonCreateFile(section, defContents);
                }
                else
                {
                    JsonCreateFile(section, new Dictionary<string, string>() { });
                }
            }
            else
            {
                JsonReload();
            }
        }

        public void JsonReload()
        {
            using (StreamReader file = File.OpenText(path))
            {
                obj = (JObject)JToken.ReadFrom(new JsonTextReader(file));
                file.Close();
            }
        }

        public void JsonSave()
        {
            IOSafe.File.Delete(path);
            File.WriteAllText(path, obj.ToString());

            JsonReload();
        }

        public void JsonCreateFile(string Section, Dictionary<string, string> contents)
        {
            obj = new JObject(
                               new JProperty(Section,
                               new JArray(from key in contents.Keys
                                          select new JObject(
                                                    new JProperty(key, contents[key])))));

            JsonSave();
        }

        public void JsonWriteValue(string Section, string Key, string Value)
        {
            JsonReload();

            var node = obj.SelectToken(Section + "[0]") as JObject;
            if (node != null)
            {
                bool found = false;

                foreach (var o in node.Descendants())
                {
                    JProperty p = o as JProperty;
                    if (p != null)
                    {
                        string keyName = p.Name;

                        if (keyName.Equals(Key))
                        {
                            p.Value = Value;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    node.Add(new JProperty(Key, Value));
                }
            }
            else
            {
                return;
            }

            JsonSave();
        }

        public string JsonReadValue(string Section, string Key, string Value = "")
        {
            JsonReload();

            bool found = false;

            foreach (var o in obj.Descendants())
            {
                JProperty p = o as JProperty;

                if (p != null)
                {
                    string keyName = p.Name;

                    if (keyName.Equals(Key))
                    {
                        found = true;
                        return p.Value.ToString();
                    }
                }
            }

            if (!found && !string.IsNullOrWhiteSpace(Value))
            {
                JsonWriteValue(Section, Key, Value);
                return JsonReadValue(Section, Key, Value);
            }

            return "";
        }
    }
}

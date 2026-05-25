#region Usings
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
#endregion

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
            JObject o = new JObject();
            obj.Add(new JProperty(Section, new JObject(o)));

            contents.Keys.ForEach(k => JsonWriteValue(Section, k, contents[k]));

            JsonSave();
        }

        public void JsonWriteValue(string Section, string Key, string Value, bool removeInvalidChars = false)
        {
            var node = obj.SelectToken(Section) as JObject;
            if (node != null)
            {
                bool found = false;

                string finalValue = Value;
                if (removeInvalidChars)
                {
                    finalValue = Util.RemoveSpecialCharacters(Value);
                }

                foreach (var o in node.Descendants())
                {
                    JProperty p = o as JProperty;
                    if (p != null)
                    {
                        string keyName = p.Name;

                        if (keyName.Equals(Key))
                        {
                            p.Value = finalValue;
                            found = true;
                            break;
                        }
                    }
                }

                if (!found)
                {
                    node.Add(new JProperty(Key, finalValue));
                }
            }
            else
            {
                return;
            }

            JsonSave();
        }

        public string JsonReadValue(string Section, string Key, string Value = "", bool removeInvalidChars = false)
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
                        string value = p.Value.ToString();
                        if (removeInvalidChars)
                        {
                            value = Util.RemoveSpecialCharacters(p.Value.ToString());
                        }
                        return value;
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

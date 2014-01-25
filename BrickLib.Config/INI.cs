using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrickLib.Config
{
    public class INI
    {
        private Dictionary<String, List<KeyValuePair<String, String>>> INIFile = new Dictionary<String, List<KeyValuePair<String, String>>>();

        public INI() { }
        public INI(String filename)
        {
            Load(filename);
        }

        public void Load(String filename)
        {
            foreach (String line in File.ReadAllLines(filename))
            {
                String l = line.Trim();
                if (l.StartsWith("#"))
                    continue;
                else if (l.StartsWith("["))
                {
                    INIFile[l.Substring(1, l.Length - 2)] = new List<KeyValuePair<String, String>>();
                }
                else if (l.Contains("="))
                {
                    INIFile[INIFile.Keys.Last()].Add(new KeyValuePair<String, String>(
                        l.Substring(0, l.IndexOf('=')),
                        l.Substring(l.IndexOf('=') + 1)
                        )
                    );
                }
            }
        }

        public void Save(String filename)
        {
            using (StreamWriter sw = new StreamWriter(filename))
                foreach (String k in INIFile.Keys)
                {
                    sw.WriteLine("[" + k + "]");
                    foreach (KeyValuePair<String, String> kv in INIFile[k])
                        sw.WriteLine(kv.Key + "=" + kv.Value);
                    sw.WriteLine("");
                }
        }

        public void AddSection(String name)
        {
            INIFile[name] = new List<KeyValuePair<String, String>>();
        }

        public int SectionCount
        {
            get
            {
                return INIFile.Keys.Count;
            }
        }

        public class ValueItem
        {
            public String Value;
            public String[] Array;
        }

        public ValueItemCollection this[String key]
        {
            get
            {
                return new ValueItemCollection(INIFile.Where(x => x.Key.ToLowerInvariant().Trim() == key.ToLowerInvariant().Trim()).Single().Value);
            }
        }

        public class ValueItemCollection
        {
            private List<KeyValuePair<String, String>> _items = new List<KeyValuePair<String, String>>();

            public void Set(String key, String value)
            {
                bool bSet = false;
                for (int i = 0; i < _items.Count; i++)
                {
                    if (_items[i].Key.ToLowerInvariant().Trim() == key.ToLowerInvariant().Trim())
                    {
                        bSet = true;
                        _items[i] = new KeyValuePair<String, String>(key, value);
                        break;
                    }
                }
                if (!bSet)
                    Add(key, value);
            }

            public void Add(String key, String value)
            {
                _items.Add(new KeyValuePair<String, String>(key, value));
            }

            public void AddArray(String key, IEnumerable<String> values)
            {
                foreach (String s in values)
                    _items.Add(new KeyValuePair<String, String>(key, s));
            }

            public List<KeyValuePair<String, String>> AsKVP()
            {
                return _items;
            }

            public ValueItemCollection(List<KeyValuePair<String, String>> items)
            {
                _items = items;
            }

            public bool ContainsKey(String key)
            {
                bool rv = false;
                foreach (KeyValuePair<String, String> kvp in _items)
                    if (kvp.Key.ToLowerInvariant().Trim() == key.ToLowerInvariant().Trim())
                    {
                        rv = true;
                        break;
                    }
                return rv;
            }

            public ValueItem this[String key]
            {
                get {
                    ValueItem rv = new ValueItem();
                    if (_items.Where(x => x.Key.ToLowerInvariant().Trim() == key.ToLowerInvariant().Trim()).ToList().Count == 0)
                    {
                        Add(key, String.Empty);
                        rv = this[key];
                    }
                    else
                    {
                        List<String> li = _items.Where(x => x.Key.ToLowerInvariant().Trim() == key.ToLowerInvariant().Trim()).Select(x => x.Value).ToList();
                        if (li.Count > 1)
                            rv.Array = li.ToArray();
                        else
                            rv.Value = li.Single();
                    }
                    return rv;
                }
            }
        }
    }
}
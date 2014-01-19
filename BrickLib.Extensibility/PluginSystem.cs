using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

namespace BrickLib.Extensibility
{
    public static class PluginSystem<T>
    {
        public static T Load(String file)
        {
            if (File.Exists(file))
            {
                Assembly a = Assembly.LoadFile((new FileInfo(file)).FullName);
                Type tPlugin = typeof(T);
                Type tAsm = default(Type);
                if (a != null)
                {
                    bool bFound = false;
                    foreach (Type t in a.GetTypes())
                    {
                        foreach (Type ti in t.GetInterfaces())
                            foreach (Type tif in t.GetInterfaces())
                                if (tPlugin == tif)
                                {
                                    tAsm = t;
                                    bFound = true;
                                    break;
                                }
                    }

                    if (bFound)
                        return (T)Activator.CreateInstance(tAsm);
                    else
                        throw new FileLoadException("The plugin " + file + " does not implement the required interface");
                }
            }
            else
                throw new FileNotFoundException("Plugin file " + file + " does not exist");

            return default(T);
        }
    }
}

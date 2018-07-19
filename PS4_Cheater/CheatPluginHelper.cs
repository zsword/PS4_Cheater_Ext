using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace PS4_Cheater
{
    class CheatPluginHelper
    {
        public List<ICheatPlugin> cheatPlugins { get; }
        public List<Type> cheatTypes{ get; }

        public CheatPluginHelper()
        {
            this.cheatPlugins = new List<ICheatPlugin>();
            this.cheatTypes = new List<Type>();
        }

        public void loadPlugins()
        {
            Type pluginType = typeof(ICheatPlugin);
            Type baseType = typeof(Cheat);
            string[] files = Directory.GetFiles(Application.StartupPath + "/Plugins");
            foreach(string file in files)
            {
                if(file.ToUpper().EndsWith(".DLL"))
                {
                    Assembly lib = Assembly.LoadFile(file);
                    Type[] types = lib.GetTypes();
                    foreach(Type t in types)
                    {
                        if(pluginType.IsAssignableFrom(t))
                        {
                            this.cheatPlugins.Add((ICheatPlugin)lib.CreateInstance(t.FullName, false));
                            continue;
                        }
                        if(baseType.Equals(t.BaseType))
                        {
                            this.cheatTypes.Add(t);
                        }
                    }
                }
            }
        }
    }
}

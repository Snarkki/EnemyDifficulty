using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Kingmaker.EntitySystem.Stats;
using UnityModManagerNet;

namespace DifficultyMod
{
    // Token: 0x0200000C RID: 12
    public static class Strings
    {
        // Token: 0x060000C5 RID: 197 RVA: 0x00029EC8 File Offset: 0x000280C8
        public static string RemoveWhitespaces(string s)
        {
            return Regex.Replace(s, "s", "");
        }



        // Token: 0x04000051 RID: 81
        public static Settings settings = Main.settings;

        // Token: 0x04000052 RID: 82
        public static UnityModManager.ModEntry.ModLogger modLogger = Main.modLogger;

        // Token: 0x04000054 RID: 84
        public static Dictionary<string, string> temp = new Dictionary<string, string>();

        // Token: 0x020000D8 RID: 216
        public class Localisation
        {
            // Token: 0x04000388 RID: 904
            [XmlAttribute]
            public string key;

            // Token: 0x04000389 RID: 905
            [XmlAttribute]
            public string value;
        }
    }
}

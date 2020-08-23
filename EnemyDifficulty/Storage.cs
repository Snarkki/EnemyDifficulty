using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Globalmap;
using Kingmaker.UnitLogic;
using TMPro;
using UnityEngine;

namespace DifficultyMod
{
    // Token: 0x0200001F RID: 31
    public static class Storage
    {
        // Token: 0x0400009E RID: 158
        public static Settings settings;


        // Token: 0x040000A0 RID: 160
        public static bool settingsWarning = false;

        // Token: 0x040000A1 RID: 161
        public static readonly string gamerVersionAtCreation = "2.0.7b";

        // Token: 0x040000A2 RID: 162
        public static readonly string gameHistoryLogPrefix = "[BagOfTricks] ";

        // Token: 0x040000A3 RID: 163
        public static readonly string assetBundlesFolder = "AssetBundles";

        // Token: 0x040000A4 RID: 164
        public static readonly string charactersImportFolder = "Characters";

        // Token: 0x040000A5 RID: 165
        public static readonly string favouritesFolder = "Favourites";

        // Token: 0x040000A6 RID: 166
        public static readonly string itemSetsFolder = "ItemSets";

        // Token: 0x040000A7 RID: 167
        public static readonly string localisationFolder = "Localisation";

        // Token: 0x040000A8 RID: 168
        public static readonly string savesFolder = "Saves";

        // Token: 0x040000A9 RID: 169
        public static readonly string taxCollectorFolder = "TaxCollector";

        // Token: 0x040000AA RID: 170
        public static readonly string modifiedBlueprintsFolder = "ModifiedBlueprints";

        // Token: 0x040000AB RID: 171
        public static readonly string exportFolder = "Export";

        // Token: 0x040000AC RID: 172
        public static string currentItemSearch = "";

        // Token: 0x040000AD RID: 173
        public static List<string> itemMultipleGuid = new List<string>();

        // Token: 0x040000AE RID: 174
        public static readonly string scribeScrollBlueprintPrefix = "#ScribeScroll";

        // Token: 0x040000AF RID: 175
        public static readonly string craftMagicItemsBlueprintPrefix = "#CraftMagicItems";

        // Token: 0x040001A9 RID: 425
        public static bool hudHidden = false;



        // Token: 0x040001B2 RID: 434
        public static bool SummonedByPlayerFaction = false;

        public static GameLogicComponent gameLogic;



    }
}

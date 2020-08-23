using Kingmaker.Blueprints.Area;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.UI.SettingsUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityModManagerNet;

namespace DifficultyMod
{
    // Token: 0x02000020 RID: 32
    public class Settings : UnityModManager.ModSettings
    {
        // Token: 0x06000153 RID: 339 RVA: 0x000308DD File Offset: 0x0002EADD
        public override void Save(UnityModManager.ModEntry modEntry)
        {
            UnityModManager.ModSettings.Save<Settings>(this, modEntry);
        }

        public bool settingShowDebugInfo = false;
        public int mainToolbarIndex = 1;
        public string itemGuid = "";

        public bool usestats = false;
        // Token: 0x040001B3 RID: 435
        public string modVersion = "0.0.0";

        // Token: 0x040001B4 RID: 436
        public bool firstLaunch = true;

        // Token: 0x040001B5 RID: 437
        public bool showCarryMoreCategory = false;

        public string enemyStrAmount = "0";
        public int enemyFinalStrAmount = 0;

        public string enemyDexAmount = "0";
        public int enemyFinalDexAmount = 0;

        public string enemyConAmount = "0";
        public int enemyFinalConAmount = 0;

        public string enemyIntAmount = "0";
        public int enemyFinalIntAmount = 0;

        public string enemyWisAmount = "0";
        public int enemyFinalWisAmount = 0;

        public string enemyChaAmount = "0";
        public int enemyFinalChaAmount = 0;

        public string enemyABAmount = "0";
        public int enemyFinalABAmount = 0;

        public string enemyACAmount = "0";
        public int enemyFinalACAmount = 0;

        public string enemyFortAmount = "0";
        public int enemyFinalFortAmount = 0;

        public string enemyWillAmount = "0";
        public int enemyFinalWillAmount = 0;

        public string enemyReflexAmount = "0";
        public int enemyFinalReflexAmount = 0;

        public string enemySpeedAmount = "0";
        public int enemyFinalSpeedAmount = 0;

        public float enemyHPmultiplier = 1f;

        public static BlueprintArea CurrentArea = null;

        public enum ModifierDescriptor
        {
            None,
            Racial,
            Dodge,
            Competence,
            Armor,
            Shield,
            Alchemical,
            Circumstance,
            Deflection,
            Enhancement,
            ArmorEnhancement,
            ShieldEnhancement,
            Inherent,
            Insight,
            Luck,
            Morale,
            NaturalArmor,
            NaturalArmorEnhancement,
            Profane,
            Sacred,
            Size,
            Trait,
            Resistance,
            FearPenalty,
            NegativeEnergyPenalty,
            UntypedStackable,
            DexterityBonus,
            ConstitutionBonus,
            Fatigued,
            Crippled,
            Feat,
            StatDamage,
            StatDrain,
            Focus,
            ShieldFocus = 33,
            Difficulty,
            BaseStatBonus,
            Penalty,
            ArmorFocus,
            Cooking,
            Polymorph,
            Helpless,
            Encumbrance,
            FavoredEnemy,
            Other,
            Prone
        }

        public static SettingsRoot.SettingsListScreen asetukset;
        public int game_difficulty = 0;

    }
}

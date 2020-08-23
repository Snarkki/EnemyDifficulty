using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using Harmony12;
using Kingmaker;
using Kingmaker.Assets.UI;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Area;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.GameDifficulties;
using Kingmaker.Blueprints.Items;
using Kingmaker.Blueprints.Root;
using Kingmaker.Cheats;
using Kingmaker.Controllers.Combat;
using Kingmaker.Controllers.GlobalMap;
using Kingmaker.Designers.EventConditionActionSystem.Events;
using Kingmaker.Designers.Mechanics.Buffs;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Globalmap;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Kingmaker.Kingdom;
using Kingmaker.Kingdom.Blueprints;
using Kingmaker.Kingdom.Buffs;
using Kingmaker.Kingdom.Tasks;
using Kingmaker.Localization;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UI;
using Kingmaker.UI._ConsoleUI.TurnBasedMode;
using Kingmaker.UI.Common;
using Kingmaker.UI.ServiceWindow.LocalMap;
using Kingmaker.UI.SettingsUI;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Alignments;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;
using TurnBased.Controllers;
using UberLogger;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityModManagerNet;

namespace DifficultyMod
{
    // Token: 0x02000021 RID: 33
    internal static class Main
    {
        public static bool Allow
        {
            get
            {
                return Main.enabled && Game.Instance.State.LoadedAreaState != null;
            }
        }
        private static bool Load(UnityModManager.ModEntry modEntry)
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
            Main.modLogger = modEntry.Logger;
            Main.settings = UnityModManager.ModSettings.Load<Settings>(modEntry);
            modEntry.OnToggle = new Func<UnityModManager.ModEntry, bool, bool>(Main.OnToggle);
            modEntry.OnGUI = new Action<UnityModManager.ModEntry>(Main.OnGUI);
            modEntry.OnSaveGUI = new Action<UnityModManager.ModEntry>(Main.OnSaveGUI);
            HarmonyInstance.Create(modEntry.Info.Id).PatchAll(Assembly.GetExecutingAssembly());
            return true;
        }


        private static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Main.enabled = value;
            return true;
        }

        private static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            if (Main.Allow)
            {
                Game instance = Game.Instance;
                bool flag;
                if (instance == null)
                {
                    flag = (null != null);
                }
                else
                {
                    Player player = instance.Player;
                    flag = (((player != null) ? player.MainCharacter.Value : null) != null);
                }
                if (flag)
                {

                    GUILayout.Space(5f);

                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    GUILayout.Space(5f);
                    GUILayout.Label("Adjust enemy attributes", new GUILayoutOption[]
                         {
                    GUILayout.Width(200f)
                         });
                    GUILayout.EndHorizontal();

                    //                    Main.settings.enemyStatsAmount = MenuTools.IntTestSettingStage1(Main.settings.enemyStatsAmount);
                    //                    Main.settings.enemyFinalStatsAmount = MenuTools.IntTestSettingStage2(Main.settings.enemyStatsAmount, Main.settings.enemyFinalStatsAmount);
                    CreateStatButtons("Strength", ref Main.settings.enemyStrAmount, ref Main.settings.enemyFinalStrAmount);
                    CreateStatButtons("Dexterity", ref Main.settings.enemyDexAmount, ref Main.settings.enemyFinalDexAmount);
                    CreateStatButtons("Constitution", ref Main.settings.enemyConAmount, ref Main.settings.enemyFinalConAmount);
                    CreateStatButtons("Intelligence", ref Main.settings.enemyIntAmount, ref Main.settings.enemyFinalIntAmount);
                    CreateStatButtons("Wisdom", ref Main.settings.enemyWisAmount, ref Main.settings.enemyFinalWisAmount);
                    CreateStatButtons("Charisma", ref Main.settings.enemyChaAmount, ref Main.settings.enemyFinalChaAmount);
                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    GUILayout.Label("Other adjustments", new GUILayoutOption[]
     {
                    GUILayout.Width(200f)
     });
                    GUILayout.EndHorizontal();
                    CreateStatButtons("Attack bonus", ref Main.settings.enemyABAmount, ref Main.settings.enemyFinalABAmount);
                    CreateStatButtons("AC", ref Main.settings.enemyACAmount, ref Main.settings.enemyFinalACAmount);
                    CreateStatButtons("Fortitude saving", ref Main.settings.enemyFortAmount, ref Main.settings.enemyFinalFortAmount);
                    CreateStatButtons("Will saving", ref Main.settings.enemyWillAmount, ref Main.settings.enemyFinalWillAmount);
                    CreateStatButtons("Reflex saving", ref Main.settings.enemyReflexAmount, ref Main.settings.enemyFinalReflexAmount);
                    CreateStatButtons("Speed", ref Main.settings.enemySpeedAmount, ref Main.settings.enemyFinalSpeedAmount);
                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    GUILayout.Space(5f);
                    GUILayout.Label("Enemy hitpoint multiplier", new GUILayoutOption[]
                         {
                    GUILayout.Width(200f)
                         });
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                    settings.enemyHPmultiplier = GUILayout.HorizontalSlider(settings.enemyHPmultiplier, 1f, 3f, GUILayout.Width(300f));
                    GUILayout.Label($" {settings.enemyHPmultiplier:p0}", GUILayout.ExpandWidth(false));
                    GUILayout.EndHorizontal();
                }
                // Create toggle for calculting adjusted stats
                GUILayout.Space(10f);
                GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
                Main.settings.usestats = GUILayout.Toggle(Main.settings.usestats, "Use adjusted stats", Array.Empty<GUILayoutOption>());
                GUILayout.EndHorizontal();
            } // Else game is not loaded
            if (!Main.Allow)
            {
                GUILayout.Label("Load your game.", new GUILayoutOption[]
            {
                GUILayout.ExpandWidth(false)
            });
            }


        }


        public static void CreateStatButtons(string stattype, ref string enemystat, ref int enemyfinalstat)

        {
            GUILayout.BeginHorizontal(Array.Empty<GUILayoutOption>());
            GUILayout.Label(stattype, GUILayout.ExpandWidth(false));
            GUILayout.Space(10);
            enemystat = GUILayout.TextField(enemystat, 2, new GUILayoutOption[]
                 {
                    GUILayout.Width(85f)
                 });
            enemystat = MenuTools.IntTestSettingStage1(enemystat);
            enemyfinalstat = MenuTools.IntTestSettingStage2(enemystat, enemyfinalstat);
            bool flag = GUILayout.Button("<b>+</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });
            if (flag)
            {
                enemyfinalstat = enemyfinalstat + 1;
                enemystat = enemyfinalstat.ToString();
                Main.settings.usestats = false;
            }
            bool flag1 = GUILayout.Button("<b>-</b>", new GUILayoutOption[]
            {
                GUILayout.Width(30f)
            });
            if (flag1)
            {
                enemyfinalstat = enemyfinalstat - 1;
                enemystat = enemyfinalstat.ToString();
                Main.settings.usestats = false;
            }
            GUILayout.EndHorizontal();

        }


        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source != null && source.IndexOf(value, comparisonType) >= 0;
        }
        private static void OnSaveGUI(UnityModManager.ModEntry modEntry)
        {
            Main.settings.Save(modEntry);
        }
        public static bool enabled;
        public static LocalMap localMap = null;
        public static bool rotationChanged = false;
        public static Settings settings;
        public static UnityModManager.ModEntry.ModLogger modLogger;
        public static bool versionMismatch = false;
        public static Vector2 scrollPosition;
        public static bool usestats = false;
        internal static LibraryScriptableObject library;


        [HarmonyPatch(typeof(UnitEntityData), "IsEnemy")]
        private static class UnitEntityData_IsEnemy_Patch
        {
            [HarmonyPostfix]
            private static void Postfix(UnitEntityData __instance)
            {
                if (Main.settings.usestats && __instance.IsPlayersEnemy)
                {
                    bool flag = __instance.Stats.Strength.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag)
                    {
                        __instance.Stats.Strength.AddModifier(settings.enemyFinalStrAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag1 = __instance.Stats.Dexterity.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag1)
                    {
                        __instance.Stats.Dexterity.AddModifier(settings.enemyFinalDexAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag2 = __instance.Stats.Constitution.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag2)
                    {
                        __instance.Stats.Constitution.AddModifier(settings.enemyFinalConAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag3 = __instance.Stats.Intelligence.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag3)
                    {
                        __instance.Stats.Intelligence.AddModifier(settings.enemyFinalIntAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag4 = __instance.Stats.Wisdom.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag4)
                    {
                        __instance.Stats.Wisdom.AddModifier(settings.enemyFinalWisAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag5 = __instance.Stats.Charisma.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag5)
                    {
                        __instance.Stats.Charisma.AddModifier(settings.enemyFinalChaAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag6 = __instance.Stats.AdditionalAttackBonus.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag6)
                    {
                        __instance.Stats.AdditionalAttackBonus.AddModifier(settings.enemyFinalABAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag7 = __instance.Stats.AC.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag7)
                    {
                        __instance.Stats.AC.AddModifier(settings.enemyFinalACAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag8 = __instance.Stats.SaveFortitude.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag8)
                    {
                        __instance.Stats.SaveFortitude.AddModifier(settings.enemyFinalFortAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag9 = __instance.Stats.SaveWill.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag9)
                    {
                        __instance.Stats.SaveWill.AddModifier(settings.enemyFinalWillAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag10 = __instance.Stats.SaveReflex.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag10)
                    {
                        __instance.Stats.SaveReflex.AddModifier(settings.enemyFinalReflexAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag11 = __instance.Stats.Speed.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag11)
                    {
                        __instance.Stats.Speed.AddModifier(settings.enemyFinalSpeedAmount, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                    bool flag12 = __instance.Stats.HitPoints.ContainsModifier(ModifierDescriptor.Cooking);
                    if (!flag12)
                    {
                        var basevalue = __instance.Stats.HitPoints.BaseValue;
                        int multiplier = (int)Math.Round(settings.enemyHPmultiplier);
                        __instance.Stats.HitPoints.AddModifier(basevalue * multiplier, Storage.gameLogic, ModifierDescriptor.Cooking);
                    }
                }
                if (!Main.settings.usestats && __instance.IsPlayersEnemy)
                {
                    __instance.Stats.Strength.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Dexterity.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Constitution.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Intelligence.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Wisdom.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Charisma.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.AdditionalAttackBonus.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.AC.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.SaveFortitude.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.SaveReflex.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.SaveWill.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.Speed.RemoveModifiers(ModifierDescriptor.Cooking);
                    __instance.Stats.HitPoints.RemoveModifiers(ModifierDescriptor.Cooking);


                }
            }
        }
    }

}

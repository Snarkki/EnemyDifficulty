using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Root;
using Kingmaker.Blueprints.Root.Strings.GameLog;
using Kingmaker.Cheats;
using Kingmaker.Designers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.Enums;
using Kingmaker.GameModes;
using Kingmaker.Globalmap.Blueprints;
using Kingmaker.Globalmap.State;
using Kingmaker.RuleSystem;
using Kingmaker.UI;
using Kingmaker.UI.Log;
using Kingmaker.UI.Selection;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.Utility;
using Kingmaker.View;
using UnityEngine;

namespace DifficultyMod
{
    // Token: 0x02000003 RID: 3
    public static class Common
    {

        // Token: 0x0600001E RID: 30 RVA: 0x00004368 File Offset: 0x00002568
        public static List<UnitEntityData> GetCustomCompanions()
        {
            List<UnitEntityData> allCharacters = Game.Instance.Player.AllCharacters;
            List<UnitEntityData> list = new List<UnitEntityData>();
            foreach (UnitEntityData unitEntityData in allCharacters)
            {
                bool flag = unitEntityData.IsCustomCompanion();
                if (flag)
                {
                    list.Add(unitEntityData);
                }
            }
            return list;
        }

        // Token: 0x0600001F RID: 31 RVA: 0x000043E8 File Offset: 0x000025E8
        public static List<UnitEntityData> GetPets()
        {
            List<UnitEntityData> allCharacters = Game.Instance.Player.AllCharacters;
            List<UnitEntityData> list = new List<UnitEntityData>();
            foreach (UnitEntityData unitEntityData in allCharacters)
            {
                bool isPet = unitEntityData.Descriptor.IsPet;
                if (isPet)
                {
                    list.Add(unitEntityData);
                }
            }
            return list;
        }

        // Token: 0x06000020 RID: 32 RVA: 0x0000446C File Offset: 0x0000266C
        public static List<UnitEntityData> GetEnemies()
        {
            List<UnitEntityData> list = new List<UnitEntityData>();
            foreach (UnitEntityData unitEntityData in Game.Instance.State.Units)
            {
                UnitEntityData unitEntityData2;
                bool flag = (unitEntityData2 = unitEntityData) != null && !unitEntityData2.IsPlayerFaction && unitEntityData2.IsInGame && unitEntityData2.IsRevealed && !unitEntityData2.Descriptor.State.IsFinallyDead && unitEntityData2.Descriptor.AttackFactions.Contains(Game.Instance.BlueprintRoot.PlayerFaction);
                if (flag)
                {
                    list.Add(unitEntityData2);
                }
            }
            return list;
        }


        // Token: 0x06000029 RID: 41 RVA: 0x000048F8 File Offset: 0x00002AF8
        public static void MoveArrayElementUp<T>(ref T[] array, T element)
        {
            int num = Array.IndexOf<T>(array, element);
            bool flag = num < array.Length - 1;
            if (flag)
            {
                T t = array[num + 1];
                array[num + 1] = element;
                array[num] = t;
            }
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00004940 File Offset: 0x00002B40
        public static void MakeArrayElementLast<T>(ref T[] array, T element)
        {
            int num = Array.IndexOf<T>(array, element);
            bool flag = num < array.Length - 1;
            if (flag)
            {
                int count = array.Length - 1 - num;
                array.Rotate(count);
            }
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00004978 File Offset: 0x00002B78
        public static void MoveArrayElementDown<T>(ref T[] array, T element)
        {
            int num = Array.IndexOf<T>(array, element);
            bool flag = num > 0;
            if (flag)
            {
                T t = array[num - 1];
                array[num - 1] = element;
                array[num] = t;
            }
        }

        // Token: 0x0600002C RID: 44 RVA: 0x000049B8 File Offset: 0x00002BB8
        public static void MakeArrayElementFirst<T>(ref T[] array, T element)
        {
            int num = Array.IndexOf<T>(array, element);
            bool flag = num > 0;
            if (flag)
            {
                array.Rotate(-num);
            }
        }

        // Token: 0x0600002D RID: 45 RVA: 0x000049E4 File Offset: 0x00002BE4
        public static void Rotate<T>(this T[] array, int count)
        {
            bool flag = array == null || array.Length < 2;
            if (!flag)
            {
                count %= array.Length;
                bool flag2 = count == 0;
                if (!flag2)
                {
                    int num = (count < 0) ? (-count) : (array.Length + count);
                    int num2 = (count > 0) ? count : (array.Length - count);
                    bool flag3 = num <= num2;
                    if (flag3)
                    {
                        for (int i = 0; i < num; i++)
                        {
                            T t = array[0];
                            Array.Copy(array, 1, array, 0, array.Length - 1);
                            array[array.Length - 1] = t;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < num2; j++)
                        {
                            T t2 = array[array.Length - 1];
                            Array.Copy(array, 0, array, 1, array.Length - 1);
                            array[0] = t2;
                        }
                    }
                }
            }
        }

        // Token: 0x0600002E RID: 46 RVA: 0x00004AC8 File Offset: 0x00002CC8
        public static void MoveListElementUp<T>(ref List<T> list, T element)
        {
            int num = list.IndexOf(element);
            bool flag = num < list.Count - 1;
            if (flag)
            {
                T value = list[num - 1];
                list[num + 1] = element;
                list[num] = value;
            }
        }

        // Token: 0x0600002F RID: 47 RVA: 0x00004B14 File Offset: 0x00002D14
        public static void MakeListElementLast<T>(ref List<T> list, T element)
        {
            int num = list.IndexOf(element);
            bool flag = num < list.Count - 1;
            if (flag)
            {
                list.Remove(element);
                list.Add(element);
            }
        }

        // Token: 0x06000030 RID: 48 RVA: 0x00004B50 File Offset: 0x00002D50
        public static void MoveListElementDown<T>(ref List<T> list, T element)
        {
            int num = list.IndexOf(element);
            bool flag = num > 0;
            if (flag)
            {
                T value = list[num - 1];
                list[num - 1] = element;
                list[num] = value;
            }
        }

        // Token: 0x06000031 RID: 49 RVA: 0x00004B94 File Offset: 0x00002D94
        public static void MakeListElementFirst<T>(ref List<T> list, T element)
        {
            int num = list.IndexOf(element);
            bool flag = num > 0;
            if (flag)
            {
                list.Remove(element);
                list.Insert(0, element);
            }
        }



        // Token: 0x06000036 RID: 54 RVA: 0x00004C64 File Offset: 0x00002E64
        public static bool IsEarlierVersion(string modEntryVersion)
        {
            return Common.CompareVersionStrings(Common.settings.modVersion, modEntryVersion) == -1;
        }

        // Token: 0x06000037 RID: 55 RVA: 0x00004C94 File Offset: 0x00002E94
        public static int CompareVersionStrings(string v1, string v2)
        {
            return new Version(v1).CompareTo(new Version(v2));
        }



        // Token: 0x06000041 RID: 65 RVA: 0x00004F1C File Offset: 0x0000311C
        public static void ModLoggerDebug(string message)
        {
            bool settingShowDebugInfo = Common.settings.settingShowDebugInfo;
            if (settingShowDebugInfo)
            {
                Main.modLogger.Log(message);
            }
        }

        // Token: 0x06000042 RID: 66 RVA: 0x00004F48 File Offset: 0x00003148
        public static void ModLoggerDebug(int message)
        {
            bool settingShowDebugInfo = Common.settings.settingShowDebugInfo;
            if (settingShowDebugInfo)
            {
                Main.modLogger.Log(message.ToString());
            }
        }

        // Token: 0x06000043 RID: 67 RVA: 0x00004F78 File Offset: 0x00003178
        public static void ModLoggerDebug(bool message)
        {
            bool settingShowDebugInfo = Common.settings.settingShowDebugInfo;
            if (settingShowDebugInfo)
            {
                Main.modLogger.Log(message.ToString());
            }
        }

        // Token: 0x04000015 RID: 21
        public static Settings settings = Main.settings;

    }
}

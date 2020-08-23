using System;
using System.Collections.Generic;
using System.Media;
using System.Text.RegularExpressions;
using Kingmaker;
using Kingmaker.Blueprints.Items;
using Kingmaker.Cheats;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Items;
using Kingmaker.UnitLogic.Alignments;
using UnityEngine;
using UnityModManagerNet;

namespace DifficultyMod
{
    public static class MenuTools
    {
        public static string IntTestSettingStage1(string setting)
        {
            int num;
            bool flag = int.TryParse(setting, out num) && !Regex.IsMatch(setting, "^[0]*$");
            string result;
            if (flag)
            {
                result = setting;
            }
            else
            {
                result = "";
            }
            return result;
        }

        // Token: 0x060000BA RID: 186 RVA: 0x00029718 File Offset: 0x00027918
        private static string IntTestSettingStageNeg1(string setting)
        {
            int num;
            bool flag = int.TryParse(setting, out num) && !Regex.IsMatch(setting, "^[0]*$");
            string result;
            if (flag)
            {
                result = setting;
            }
            else
            {
                result = "";
            }
            return result;
        }

        // Token: 0x060000BB RID: 187 RVA: 0x00029754 File Offset: 0x00027954
        public static int IntTestSettingStage2(string setting, int finalSetting)
        {
            bool flag = setting != "";
            int result;
            if (flag)
            {
                finalSetting = (result = int.Parse(setting));
            }
            else
            {
                result = finalSetting;
            }
            return result;
        }

        // Token: 0x060000BC RID: 188 RVA: 0x00029784 File Offset: 0x00027984
        public static string FloatTestSettingStage1(string setting)
        {
            float num;
            bool flag = float.TryParse(setting, out num) && !setting.Contains(",");
            string result;
            if (flag)
            {
                result = setting;
            }
            else
            {
                result = "";
            }
            return result;
        }

        // Token: 0x060000BD RID: 189 RVA: 0x000297C0 File Offset: 0x000279C0
        public static float FloatTestSettingStage2(string setting, float finalSetting)
        {
            bool flag = setting != "" && float.Parse(setting) != 0f;
            float result;
            if (flag)
            {
                finalSetting = (result = float.Parse(setting));
            }
            else
            {
                result = finalSetting;
            }
            return result;
        }

    }
}

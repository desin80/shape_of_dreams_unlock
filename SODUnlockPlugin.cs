using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
namespace SODUnlock
{
    [BepInPlugin("com.desin.SODUnlock", "SODUnlock", "1.0.0")]
    public class SODUnlockPlugin : BaseUnityPlugin
    {
        private readonly Harmony harmony = new Harmony("com.desin.SODUnlock");

        internal static ManualLogSource Log;

        private void Awake()
        {
            Log = BepInEx.Logging.Logger.CreateLogSource("SODUnlock");
            harmony.PatchAll();
            Log.LogInfo("SODUnlock loaded successfully!");
        }
    }

    [HarmonyPatch(typeof(DewProfile), "Validate")]
    public static class Patch_Validate_UnlockAll
    {
        public static void Postfix(DewProfile __instance)
        {
            foreach (var t in Dew.allSkills)
            {
                if (!__instance.skills.ContainsKey(t.Name))
                {
                    __instance.skills[t.Name] = new DewProfile.UnlockData()
                    {
                        didReadMemory = false,
                        isNewHeroOrHeroSkill = false,
                        status = UnlockStatus.Complete
                    };
                }
                __instance.skills[t.Name].status = UnlockStatus.Complete;
            }

            foreach (var t in Dew.allGems)
            {
                if (!__instance.gems.ContainsKey(t.Name))
                {
                    __instance.gems[t.Name] = new DewProfile.UnlockData()
                    {
                        didReadMemory = false,
                        isNewHeroOrHeroSkill = false,
                        status = UnlockStatus.Complete
                    };
                }
                __instance.gems[t.Name].status = UnlockStatus.Complete;
            }

            foreach (var t in Dew.allHeroes)
            {
                if (!__instance.heroes.ContainsKey(t.Name))
                {
                    __instance.heroes[t.Name] = new DewProfile.UnlockData()
                    {
                        didReadMemory = false,
                        isNewHeroOrHeroSkill = false,
                        status = UnlockStatus.Complete
                    };
                }
                __instance.heroes[t.Name].status = UnlockStatus.Complete;
            }

            foreach (var t in Dew.allLucidDreams)
            {
                if (!__instance.lucidDreams.ContainsKey(t.Name))
                {
                    __instance.lucidDreams[t.Name] = new DewProfile.UnlockData()
                    {
                        didReadMemory = false,
                        isNewHeroOrHeroSkill = false,
                        status = UnlockStatus.Complete
                    };
                }
                __instance.lucidDreams[t.Name].status = UnlockStatus.Complete;
            }

            foreach (var t in Dew.allArtifacts)
            {
                if (!__instance.artifacts.ContainsKey(t.Name))
                {
                    __instance.artifacts[t.Name] = new DewProfile.UnlockData()
                    {
                        didReadMemory = false,
                        isNewHeroOrHeroSkill = false,
                        status = UnlockStatus.Complete
                    };
                }
            }

            foreach (var kv in __instance.emotes.Values)
            {
                kv.isUnlocked = true;
            }
            foreach (var kv in __instance.accessories.Values)
            {
                kv.isUnlocked = true;
            }
            foreach (var kv in __instance.nametags.Values)
            {
                kv.isUnlocked = true;
            }
        }
    }

    [HarmonyPatch(typeof(UI_Lobby_DejavuButton), "Start")]
    public static class Patch_DejavuButton_AlwaysUnlocked
    {
        public static void Postfix(UI_Lobby_DejavuButton __instance)
        {
            bool isUnlocked = true;

            if (__instance.lockedObject != null)
                __instance.lockedObject.SetActive(!isUnlocked);

            if (__instance.buttonText != null)
                __instance.buttonText.text = DewLocalization.GetUIValue("Dejavu_Title");
        }
    }

    [HarmonyPatch(typeof(UI_Lobby_DejavuWindow_Item), "Setup")]
    public static class Patch_DejavuItem_AlwaysUnlocked
    {
        public static void Postfix(UI_Lobby_DejavuWindow_Item __instance)
        {
            if (__instance.lockedObject != null)
                __instance.lockedObject.SetActive(false);

            if (__instance.costDisplay != null)
                __instance.costDisplay.gameObject.SetActive(false);
        }
    }
}

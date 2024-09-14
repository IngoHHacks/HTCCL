using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HTCCL.Content;
using Object = UnityEngine.Object;

namespace HTCCL.Patches;

[HarmonyPatch]
internal class SearchScreenPatch
{
    /*
     * Patch:
     * - Enables search screen in the character select menu.
     */
    [HarmonyPatch(typeof(Characters), nameof(Characters.MKFNIFJNLEK))]
    [HarmonyPrefix]
    public static bool Characters_MKFNIFJNLEK(int DLMLPINGCBA, int GMJKGKDFHOH, int ADKBAGHAIGH)
    {
        if (SceneManager.GetActiveScene().name != "Select_Char")
        {
            return true;
        }
        if (Characters.filter == -2)
        {
            return false;
        }
        return true;
    }
    
    /*
     * Patch
     * - Filters characters by search string if filter is set to -2.
     */
    [HarmonyPatch(typeof(Character), nameof(Character.NJHKOCOPPMK))]
    [HarmonyPrefix]
    public static bool Character_NJHKOCOPPMK(ref int __result, Character __instance, int MFMDCFCOCFH)
    {
        if (MFMDCFCOCFH == -2)
        {
            if (SubstringDamerauLevenshteinDistance(__instance.name, _searchString) <= (int)(_searchString.Length * 0.2 + 0.5))
            {
                __result = 1;
            }
            else
            {
                __result = 0;
            }
            return false;
        }
        return true;
    }

    // Damerau Levenshtein distance algorithm from https://programm.top/en/c-sharp/algorithm/damerau-levenshtein-distance/
    private static int Minimum(int a, int b) => a < b ? a : b;
#pragma warning disable Harmony003
    private static int Minimum(int a, int b, int c) => (a = a < b ? a : b) < c ? a : c;
#pragma warning restore Harmony003
    private static int DamerauLevenshteinDistance(string firstText, string secondText)
    {
        var n = firstText.Length + 1;
        var m = secondText.Length + 1;
        var arrayD = new int[n, m];

        for (var i = 0; i < n; i++)
        {
            arrayD[i, 0] = i;
        }

        for (var j = 0; j < m; j++)
        {
            arrayD[0, j] = j;
        }

        for (var i = 1; i < n; i++)
        {
            for (var j = 1; j < m; j++)
            {
                var cost = firstText[i - 1] == secondText[j - 1] ? 0 : 1;

                arrayD[i, j] = Minimum(arrayD[i - 1, j] + 1, // delete
                    arrayD[i, j - 1] + 1, // insert
                    arrayD[i - 1, j - 1] + cost); // replacement

                if (i > 1 && j > 1
                          && firstText[i - 1] == secondText[j - 2]
                          && firstText[i - 2] == secondText[j - 1])
                {
                    arrayD[i, j] = Minimum(arrayD[i, j],
                        arrayD[i - 2, j - 2] + cost); // permutation
                }
            }
        }

        return arrayD[n - 1, m - 1];
    }

    private static int SubstringDamerauLevenshteinDistance(string firstText, string secondText)
    {
        int len1 = firstText.Length;
        int len2 = secondText.Length;
        if (len1 < len2)
        {
            return DamerauLevenshteinDistance(firstText, secondText);
        }
        string shortestWord = secondText;
        string longestWord = firstText;
        int start = 0;
        int len = shortestWord.Length;
        int min = int.MaxValue;
        while (start + len <= longestWord.Length)
        {
            int dist = DamerauLevenshteinDistance(shortestWord, longestWord.Substring(start, len));
            if (dist < min)
            {
                min = dist;
            }
            start++;
        }
        return min;
    }
    
    private static string _searchString = "";
    private static bool _searchUpdate = false;

    /*
     * Patch:
     * - Enables search screen in the character select menu.
     */
    [HarmonyPatch(typeof(Scene_Select_Char), nameof(Scene_Select_Char.Update))]
    [HarmonyPrefix]
    public static bool Scene_Select_Char_Update(Scene_Select_Char __instance)
    {
        if (Plugin.EnableCharacterSearchScreen.Value)
        {
            if (Characters.filter == -2)
            {
                MappedMenus.no_options = MappedMenus.no_menus - 1;
                if (MappedMenus.foc == MappedMenus.no_menus)
                {
                    MappedMenus.foc = 0;
                }
                HandleKeybinds();
                if (MappedMenus.foc == 0)
                {
                    if (Input.inputString != "" && Input.inputString != "\b")
                    {
                        String str = Input.inputString.Replace("\b", "").Replace("\n", "").Replace("\r", "")
                            .Replace("\t", "")
                            .Replace("\0", "");
                        UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD += str;
                        _searchString = UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD;
                        _searchUpdate = true;
                        UnmappedMenus.ICGNAJFLAHL();
                        return false;
                    }

                    if (Input.inputString == "\b" || Input.GetKeyDown(KeyCode.Delete))
                    {
                        if (UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD.Length > 0)
                        {
                            UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD = UnmappedMenus
                                .FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD.Substring(0,
                                    UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD.Length - 1);
                            _searchString = UnmappedMenus.FKANHDIMMBJ[UnmappedMenus.HOAOLPGEBKJ].FFCNPGPALPD;
                            _searchUpdate = true;
                            UnmappedMenus.ICGNAJFLAHL();
                        }

                        return false;
                    }

                    if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Z))
                    {
                        return _searchString.Length == 0;
                    }
                }
            }
        }
        return true;
    }

    private static void HandleKeybinds()
    {
        // Delete
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Delete))
        {
            if (Characters.foc > 0 && MappedMenus.foc > 0)
            {
                if (Characters.no_chars == 1)
                {
                    MappedSound.Play(MappedSound.block);
                    LogInfo("You can't delete the last character!");
                    return;
                }
                
                MappedSound.Play(MappedSound.death[3]);
                LogInfo("Deleting character " + Characters.c[Characters.foc].name);
                CharacterUtils.DeleteCharacter(Characters.foc);
                Characters.foc--;
                MappedMenus.foc--;
                for (int m = 1; m <= MappedPlayers.no_plays; m++)
                {
                    if (Characters.profileChar[m] > 0)
                    {
                        Characters.profileChar[m] = 0;
                    }
                }
                MappedSaveSystem.request = 1;
                MappedMenus.Load();
            }
        }
        // New
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
        {
            MappedSound.Play(MappedSound.tanoy);
            LogInfo("Creating new character");
            CharacterUtils.CreateRandomCharacter();
            MappedSaveSystem.request = 1;
            MappedMenus.Load();
        }
    }
    
    private static List<GameObject> _tempObjects = new();

    /*
     * Patch:
     * - Loads menus for the search screen.
     */
    [HarmonyPatch(typeof(UnmappedMenus), nameof(UnmappedMenus.ICGNAJFLAHL))]
    [HarmonyPostfix]
    public static void Menus_ICGNAJFLAHL()
    {
        if (MappedMenus.screen == 11 && Characters.filter == -2)
        {
            MappedMenus.Add();
            ((MappedMenu)MappedMenus.menu[MappedMenus.no_menus]).Load(2, "\u200BSearch\u200B", 0, 110, 1, 1);
            ((MappedMenu)MappedMenus.menu[MappedMenus.no_menus]).value = _searchString;
            ((MappedMenu)MappedMenus.menu[MappedMenus.no_menus]).id = 999999999;

            GameObject obj = Object.Instantiate(MappedSprites.gMenu[1]);
            _tempObjects.Add(obj);
            obj.transform.position = new Vector3(350f, 110f, 0f);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            RectTransform rt = obj.transform.Find("Title").transform as RectTransform;
            rt.sizeDelta *= 5;
            obj.transform.SetParent(MappedMenus.gDisplay.transform, false);
            Object.Destroy(obj.transform.Find("Background").gameObject);
            Object.Destroy(obj.transform.Find("Border").gameObject);
            obj.transform.Find("Title").gameObject.GetComponent<Text>().text =
                "Press [Ctrl+DEL] to delete the selected character.";

            obj = Object.Instantiate(MappedSprites.gMenu[1]);
            _tempObjects.Add(obj);
            obj.transform.position = new Vector3(-350f, 110f, 0f);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            rt = obj.transform.Find("Title").transform as RectTransform;
            rt.sizeDelta *= 5;
            obj.transform.SetParent(MappedMenus.gDisplay.transform, false);
            Object.Destroy(obj.transform.Find("Background").gameObject);
            Object.Destroy(obj.transform.Find("Border").gameObject);
            obj.transform.Find("Title").gameObject.GetComponent<Text>().text =
                "Press [Ctrl+N] to create a new character.";

            if (_searchUpdate)
            {
                MappedMenus.foc = 0;
                _searchUpdate = false;
            }
        }
        else
        {
            _searchString = "";
            if (_tempObjects.Count > 0)
            {
                foreach (GameObject obj in _tempObjects)
                {
                    if (obj != null)
                    {
                        Object.Destroy(obj);
                    }
                }
                _tempObjects.Clear();
            }
        }

        if (MappedMenus.screen == 2)
        {
            if (MappedMenus.tab == 5)
            {
                MappedMenus.Add();
                ((MappedMenu)MappedMenus.menu[MappedMenus.no_menus]).Load(2, "Mod", 0, 220, 1.5f, 1.5f);
            }
        }
    }

    /*
     * Patch:
     * - Makes 'Search' unselectable in the character search screen.
     */
    [HarmonyPatch(typeof(UnmappedMenu), nameof(UnmappedMenu.GBLDMIAPNEP))]
    [HarmonyPrefix]
    public static bool Menu_GBLDMIAPNEP(ref int __result, UnmappedMenu __instance, float MMBJPONJJGM, float EJOKLBHLEEJ, float GJGFOKOEANG)
    {
        if (__instance.NKEDCLBOOMJ.Equals("\u200BSearch\u200B"))
        {
            __result = 0;
            return false;
        }
        return true;
    }
    
    /*
     * Patch:
     * - Tick loop for the search screen, inside menus.
     */
    [HarmonyPatch(typeof(UnmappedMenu), nameof(UnmappedMenu.BBICLKGGIGB))]
    [HarmonyPostfix]
    public static void Menu_BBICLKGGIGB(UnmappedMenu __instance)
    {
        if (__instance.NKEDCLBOOMJ != null && __instance.NKEDCLBOOMJ.Equals("\u200BSearch\u200B") && UnmappedMenus.NNMDEFLLNBF == 0)
        {
            UnmappedSprites.BBLJCJMDDLO(__instance.MGHGFEHHEBA, UnmappedMenus.DEGLGENADOK.r, UnmappedMenus.DEGLGENADOK.g, UnmappedMenus.DEGLGENADOK.b);
            UnmappedSprites.BBLJCJMDDLO(__instance.KELNLAINAFB, UnmappedMenus.PLIABNOBFDO.r, UnmappedMenus.PLIABNOBFDO.g, UnmappedMenus.PLIABNOBFDO.b);
            if (__instance.BPJFLJPKKJK == 3)
            {
                UnmappedSprites.BBLJCJMDDLO(__instance.GPBKAFJHLML, UnmappedMenus.PLIABNOBFDO.r, UnmappedMenus.PLIABNOBFDO.g, UnmappedMenus.PLIABNOBFDO.b);
            }
            if (__instance.FHOEKMHCCEM != null)
            {
                __instance.FHOEKMHCCEM.color = new Color(UnmappedMenus.DKNOFHAFPHJ.r, UnmappedMenus.DKNOFHAFPHJ.g, UnmappedMenus.DKNOFHAFPHJ.b, __instance.FHOEKMHCCEM.color.a);
            }
            if (__instance.JAFNFBLIALC != null)
            {
                __instance.JAFNFBLIALC.color = new Color(UnmappedMenus.DDPBNKAHLFI.r, UnmappedMenus.DDPBNKAHLFI.g, UnmappedMenus.DDPBNKAHLFI.b, __instance.JAFNFBLIALC.color.a);
            }
        }
    }
    
    /*
     * Patch:
     * - Enables the search screen
     */
    [HarmonyPatch(typeof(Scene_Select_Char), nameof(Scene_Select_Char.Update))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Scene_Select_Char_Update(IEnumerable<CodeInstruction> instructions)
    {
        CodeInstruction prev = null;
        foreach (CodeInstruction instruction in instructions)
        {
            if (prev != null && prev.opcode == OpCodes.Ldc_R4 && (float)prev.operand == 5f && instruction.opcode == OpCodes.Ldc_R4 && (float)instruction.operand == -1)
            {
                yield return new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(SearchScreenPatch), nameof(ReplacementFloat)));
            }
            else
            {
                yield return instruction;
            }
            prev = instruction;
        }
    }

    public static float ReplacementFloat()
    {
        return Plugin.EnableCharacterSearchScreen.Value ? -2f : -1f;
    }
}
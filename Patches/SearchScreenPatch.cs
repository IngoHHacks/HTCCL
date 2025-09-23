using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace HTCCL.Patches;

[HarmonyPatch]
internal class SearchScreenPatch
{

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
            if (Characters.filter == 0)
            {
                HandleKeybinds(__instance);
            }
        }
        return true;
    }
    
    private static void HandleKeybinds(Scene_Select_Char __instance)
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
                __instance.oldFilter = -516391;
            }
        }
        // New
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.N))
        {
            MappedSound.Play(MappedSound.tanoy);
            LogInfo("Creating new character");
            int CharID = CharacterUtils.CreateRandomCharacter();
            LIPNHOMGGHF.NFOPGNENJGP();  //refresh the character selection pages
            MappedSaveSystem.request = 1;
            //__instance.oldFilter = -516391;
            __instance.oldPage = -516391;
            for (int i = 0; i < LIPNHOMGGHF.PDKAMOBFEPD.GetLength(0); i++)      //Character selection page matrix
            {
                for (int j = 0; j < LIPNHOMGGHF.PDKAMOBFEPD.GetLength(1); j++)
                {
                    if (CharID == LIPNHOMGGHF.PDKAMOBFEPD[i, j])
                    {
                        MappedMenus.page = i;
                        MappedMenus.foc = j;
                        Characters.foc = 0;
                        return;
                    }
                }
            }

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
        if (MappedMenus.screen == 11 && Characters.filter == 0)
        {
            GameObject obj = Object.Instantiate(MappedSprites.gMenu[1]);
            _tempObjects.Add(obj);
            obj.transform.position = new Vector3(150f, 190f, 0f);
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
            obj.transform.position = new Vector3(-450f, 190f, 0f);
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            rt = obj.transform.Find("Title").transform as RectTransform;
            rt.sizeDelta *= 5;
            obj.transform.SetParent(MappedMenus.gDisplay.transform, false);
            Object.Destroy(obj.transform.Find("Background").gameObject);
            Object.Destroy(obj.transform.Find("Border").gameObject);
            obj.transform.Find("Title").gameObject.GetComponent<Text>().text =
                "Press [Ctrl+N] to create a new character.";
        }
        else
        {
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
            if (MappedMenus.tab == 6)
            {
                MappedMenus.Add();
                ((MappedMenu)MappedMenus.menu[MappedMenus.no_menus]).Load(2, "Mod", 0, 220, 1.5f, 1.5f);
            }
        }
    }
}
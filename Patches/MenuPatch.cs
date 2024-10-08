using UnityEngine.UI;
using HTCCL.Content;

namespace HTCCL.Patches;

[HarmonyPatch]
internal class MenuPatch
{
    /*
     * Patch:
     * - Shows HTCCL version next to the game version in the main menu.
     */
    [HarmonyPatch(typeof(UnmappedMenus), nameof(UnmappedMenus.MOKABGFFGLC))]
    [HarmonyPostfix]
    public static void Menus_MOKABGFFGLC()
    {
         var text = GameObject.Find("Version").GetComponent<Text>(); 
         text.text = "\nHTCCL " + Plugin.PluginVerLong + "\t\t Game " + text.text + "\n" +
                     "Mods Loaded: " + LoadContent.NumContentMods + " Content, " + AllMods.Instance.NumMods + " Code";
         text.horizontalOverflow = HorizontalWrapMode.Overflow;
         text.verticalOverflow = VerticalWrapMode.Overflow;
         text.fontSize -= 4;
    }

    private static int _location;
    
    /*
     * Patch:
     * Fast travel option in pause menu
     */
    [HarmonyPatch(typeof(UnmappedMenus), nameof(UnmappedMenus.OPBBKBAJJHA))]
    [HarmonyPostfix]
    public static void Menus_OPBBKBAJJHA()
    {
        if (MappedMenus.page == 0 && MappedMenus.commit >= 5)
        {
            string title = ((MappedMenu)MappedMenus.menu[MappedMenus.foc]).title;
            if (title == "Fast Travel")
            {
                MappedMenus.page = 5;
                MappedMenus.Load();
                _location = Loc2Libindex(MappedWorld.location);
            }
            
        }
        else if (MappedMenus.page == 5)
        {
            if (World.library == null)
            {
                InitLibrary();
            }
            var extraCost = 0;
            var from = MappedWorld.location;
            var to = World.library[_location];
            if (from <= VanillaCounts.Data.NoLocations && to <= VanillaCounts.Data.NoLocations)
            {
                if (from == 22 || to == 22)
                {
                    extraCost = 50;
                }
                else
                {
                    extraCost = 100;
                }
            }
            else if (from > VanillaCounts.Data.NoLocations && to <= VanillaCounts.Data.NoLocations)
            {
                if (to != 22)
                {
                    extraCost = 50;
                }
            }
            else if (from <= VanillaCounts.Data.NoLocations && to > VanillaCounts.Data.NoLocations)
            {
                if (from != 22)
                {
                    extraCost = 100;
                }
            }
            var cost = MappedWorld.EntryFee(to) + extraCost;
            if (from == to)
            {
                cost = 0;
            }
            _location = Mathf.RoundToInt(((MappedMenu)MappedMenus.menu[1]).ChangeValue(_location, 1f, 10f, 2f, World.library.Length - 1, 1));
            var realLocation = World.library[_location];
            if (realLocation > VanillaCounts.Data.NoLocations)
            {
                ((MappedMenu)MappedMenus.menu[1]).value =
                    CustomArenaPrefabs[realLocation - VanillaCounts.Data.NoLocations - 1].name;
            }
            else
            {
                ((MappedMenu)MappedMenus.menu[1]).value = MappedWorld.DescribeLocation(realLocation);
            }
            ((MappedMenu)MappedMenus.menu[4]).title = "Cost: " + (cost == 0 ? "Free" : "$" + cost);
            var foc = MappedMenus.foc;
            switch (foc)
            {
                case 2:
                    if (((MappedMenu)MappedMenus.menu[2]).clicked != 0f)
                    {
                        var character = (MappedCharacter)Characters.c[Characters.star];
                        MappedWorld.Exit();
                        character.Relocate(realLocation, 0.01f, 0.01f);
                        character.EarnMoney(-cost, 0f);
                        if (MappedMenus.go <= 20)
                        {
                            MappedMenus.go = 50;
                        }
                    }
                    break;
                case 3:
                    if (((MappedMenu)MappedMenus.menu[3]).clicked != 0f)
                    {
                        MappedMenus.page = 0;
                        MappedMenus.Load();
                    }
                    break;
            }
        }
        MappedMenus.UpdateDisplay();
    }

    private static void InitLibrary()
    {
        World.library = Array.Empty<int>();
        for (int i = -1; i <= World.no_locations; i++)
        {
            if (MappedWorld.Available(i) > 0)
            {
                int[] array = new int[World.library.Length + 1];
                World.library.CopyTo(array, 0);
                World.library = array;
                World.library[World.library.Length - 1] = i;
                if (i == World.location)
                {
                    World.libraryFoc = World.library.Length - 1;
                }
            }
        }
    }
    
    private static int Loc2Libindex(int location)
    {
        if (World.library == null)
        {
            InitLibrary();
        }
        return Math.Abs(Array.IndexOf(World.library, location));
    }
}
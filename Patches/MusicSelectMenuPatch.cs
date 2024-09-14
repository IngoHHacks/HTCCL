using System.Reflection;
using System.Reflection.Emit;
using UnityEngine.UIElements;
using HTCCL.Content;

namespace HTCCL.Patches;

[HarmonyPatch]
internal class MusicSelectMenuPatch
{
    /*
     * Patch:
     * - Allows right clicking on the character search screen.
     */
    [HarmonyPatch(typeof(UnmappedMenus), nameof(UnmappedMenus.PIELJFKJFKF))]
    [HarmonyPostfix]
    public static void Menus_PIELJFKJFKF()
    {
        RightClickPatch.RightClickFoc = 0;
        MappedController controller = MappedControls.pad[MappedControls.host];
        for (MappedMenus.cyc = 1; MappedMenus.cyc <= MappedMenus.no_menus; MappedMenus.cyc++)
        {
            if (Input.GetMouseButton((int)MouseButton.RightMouse))
            {
                MappedMenu menu = MappedMenus.menu[MappedMenus.cyc];
                var clickX = Input.mousePosition.x;
                var clickY = Input.mousePosition.y;
                if (menu.Inside(clickX, clickY, 10f) <= 0 || MappedKeyboard.preventInput != 0)
                {
                    continue;
                }
                RightClickPatch.RightClickFoc = menu.id;
                MappedMenus.foc = RightClickPatch.RightClickFoc;
            }
            else if (MappedMenus.Control() > 0 && controller.type > 1 && MappedMenus.cyc == MappedMenus.foc)
            {
                if (controller.button[1] > 0)
                {
                    RightClickPatch.RightClickFoc = MappedMenus.foc;
                }
            }
        }
    }
    
    /*
     * Patch:
     * - Disables vanilla code for tab 1 if page is not 0.
     * - Disables music resetting on page -1.
     * (Lists use page -1)
     */
    [HarmonyPatch(typeof(Scene_Editor), nameof(Scene_Editor.Update))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Scene_Editor_Update(IEnumerable<CodeInstruction> instructions)
    {
        CodeInstruction prev = null;
        CodeInstruction prev2 = null;
        foreach (CodeInstruction instruction in instructions)
        {
            yield return instruction;
            if (prev2 != null) {
                if (prev2.opcode == OpCodes.Ldsfld && (FieldInfo)prev2.operand ==
                    AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.CHLJMEPFJOK)))
                {
                    if (prev.opcode == OpCodes.Ldc_I4_1)
                    {
                        yield return new CodeInstruction(OpCodes.Ldsfld,
                            AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.ODOAPLMOJPD)));
                        yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                        yield return new CodeInstruction(instruction);
                    }
                } else if (prev2.opcode == OpCodes.Ldsfld && (FieldInfo)prev2.operand ==
                    AccessTools.Field(typeof(CHLPMKEGJBJ), nameof(CHLPMKEGJBJ.CNNKEACKKCD)))
                {
                    if (prev.opcode == OpCodes.Ldsfld && (FieldInfo)prev.operand ==
                        AccessTools.Field(typeof(CHLPMKEGJBJ), nameof(CHLPMKEGJBJ.GEDDILDLILI)))
                    {
                        yield return new CodeInstruction(OpCodes.Ldsfld,
                            AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.ODOAPLMOJPD)));
                        yield return new CodeInstruction(OpCodes.Ldc_I4_M1);
                        yield return new CodeInstruction(instruction);
                    }
                }
            } 
            prev2 = prev;
            prev = instruction;
        }
    }
    
    /*
     * Patch:
     * - Disables vanilla code for tab 1 if page is not 0.
     * (Lists use page -1)
     */
    [HarmonyPatch(typeof(UnmappedMenus), nameof(UnmappedMenus.ICGNAJFLAHL))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> Menus_ICGNAJFLAHL(IEnumerable<CodeInstruction> instructions)
    {
        CodeInstruction prev = null;
        CodeInstruction prev2 = null;
        int screen = 0;
        foreach (CodeInstruction instruction in instructions)
        {
            yield return instruction;
            if (prev != null && prev.opcode == OpCodes.Ldsfld && (FieldInfo)prev.operand ==
                AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.FAKHAFKOBPB)))
            {
                if (instruction.opcode == OpCodes.Ldc_I4_S)
                {
                    screen = (sbyte)instruction.operand;
                }
            }
            else if (screen == 60)
            {
                if (prev2 != null && prev2.opcode == OpCodes.Ldsfld && (FieldInfo)prev2.operand ==
                    AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.CHLJMEPFJOK)))
                {
                    if (prev.opcode == OpCodes.Ldc_I4_1)
                    {
                        yield return new CodeInstruction(OpCodes.Ldsfld,
                            AccessTools.Field(typeof(LIPNHOMGGHF), nameof(LIPNHOMGGHF.ODOAPLMOJPD)));
                        yield return new CodeInstruction(OpCodes.Ldc_I4_0);
                        yield return new CodeInstruction(instruction);
                    }
                }
            }
            prev2 = prev;
            prev = instruction;
        }
    }
    
}
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection.Emit;
using VRage.Input;

namespace GrabSingleItem
{
    [HarmonyPatch]
    internal static class MyTerminalInventoryControllerPatch
    {
        [HarmonyPatch("Sandbox.Game.Gui.MyTerminalInventoryController", "grid_ItemClicked")]
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                yield return instruction;
                if (instruction.opcode == OpCodes.Or)
                {
                    yield return CodeInstruction.Call(typeof(MyTerminalInventoryControllerPatch), nameof(CheckForAlt));
                }
            }
        }

        private static bool CheckForAlt(bool original) => original || MyInput.Static.IsAnyAltKeyPressed();

        [HarmonyPatch("Sandbox.Game.Gui.MyTerminalInventoryController", "grid_ItemDragged")]
        [HarmonyPrefix]
        private static bool DragPrefix() => !MyInput.Static.IsAnyAltKeyPressed();

        [HarmonyPatch("Sandbox.Game.Gui.MyTerminalInventoryController", "grid_ItemDoubleClicked")]
        [HarmonyPrefix]
        private static bool DoubleClickPrefix() => !MyInput.Static.IsAnyAltKeyPressed();

    }
}

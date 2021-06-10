using System.Reflection;
using HarmonyLib;
using JetBrains.Annotations;

namespace Recipe_Show_Have_Count.Patches {
    [HarmonyPatch]
    [UsedImplicitly]
    public static class ShowHaveCountPatch {
        [HarmonyTargetMethod]
        [UsedImplicitly]
        public static MethodBase TargetMethod() {
            return typeof(FactoryStation).GetMethod("WriteInfoInputs", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [UsedImplicitly]
        [HarmonyPrefix]
        public static void Prefix(ref Recipe recipe, ref RichTextWriter result, ref FactoryTexts ___m_texts, ref OnlineCargo ___m_cargo) {
            var outputItem = recipe.Output;
            var haveAmount = ___m_cargo.GetAmount(outputItem.Item, outputItem.Stats);

            result.CurrentStyle = "Text";
            result.Text.ConcatFormat(___m_texts.InputAvailableFormat.Text, haveAmount);

            result.Text.AppendLine();
            result.Text.AppendLine();
        }
    }
}
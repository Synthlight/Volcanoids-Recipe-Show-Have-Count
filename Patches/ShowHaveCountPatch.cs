using System.Reflection;
using System.Text;
using HarmonyLib;
using JetBrains.Annotations;

namespace Recipe_Show_Have_Count.Patches;

[HarmonyPatch]
[UsedImplicitly]
public static class ShowHaveCountPatch {
    [HarmonyTargetMethod]
    [UsedImplicitly]
    public static MethodBase TargetMethod() {
        return typeof(FactoryUi).GetMethod(nameof(FactoryUi.WriteRecipeInfo), BindingFlags.NonPublic | BindingFlags.Instance);
    }

    [UsedImplicitly]
    [HarmonyPrefix]
    public static bool Prefix(ref FactoryUi __instance, ref RichTextWriter result) {
        var recipe     = FactoryUi.m_availabilityInfo.Recipe;
        var outputItem = recipe.Output;
        var haveAmount = __instance.m_producer.GetPullInventory().GetAmount(outputItem.Item, int.MaxValue);

        result.CurrentStyle = "Title";
        result.Text.ConcatFormat(recipe.Output.Amount == 1 ? __instance.Texts.TitleFormat : __instance.Texts.TitleFormatAmount, recipe.Output.Item.Name, recipe.Output.Amount);
        result.Text.ConcatFormat(__instance.Texts.InputAvailableFormat.Text, haveAmount);
        result.Text.AppendLine();
        result.AppendString("Text", recipe.Output.Item.Description);
        result.Text.AppendLine();

        return false;
    }
}
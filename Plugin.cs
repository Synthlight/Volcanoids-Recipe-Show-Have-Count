using Base_Mod;
using JetBrains.Annotations;

namespace XMod {
    [UsedImplicitly]
    public class Plugin : BaseGameMod {
        protected override string ModName => "Recipe-Show-Have-Count";

        protected override void Init() {
            Recipe_Available_Input_Count.Plugin.showAmountOfCurrentItem = true;

            base.Init();
        }
    }
}
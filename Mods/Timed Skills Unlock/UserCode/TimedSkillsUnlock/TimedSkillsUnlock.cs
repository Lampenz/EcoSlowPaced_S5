// Le Village Mod - Unlock skills planning
// See LICENSE file for full license information.

using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Items;
using Eco.Gameplay.Players;
using Eco.Shared.Utils;
using Eco.Simulation.Time;
using System;
using System.Collections.Generic;

namespace Eco.Mods.TechTree
{
    public class TSURegister : IModInit
    {
        public static ModRegistration Register() => new()
        {
            ModName = "TimedSkillsUnlock",
            ModDescription = "This mod allows associating a server day with the skill scroll that players can read.",
            ModDisplayName = "Le Village - Timed Skills Unlock",
        };
    }

    public static class TSUSettings
    {
		// Define below the server day for each skill by changing the number
        public static readonly Dictionary<Type, int> Planning = new Dictionary<Type, int>
        {
            { typeof(CarpentrySkillScroll),             0 },
            { typeof(MasonrySkillScroll),               0 },
            { typeof(FarmingSkillScroll),               0 },
            { typeof(ButcherySkillScroll),              0 },
            { typeof(TailoringSkillScroll),             4 },
            { typeof(BasicEngineeringSkillScroll),      7 },
            { typeof(PaperMillingSkillScroll),          14 },
            { typeof(ShipwrightSkillScroll),            4 },
            { typeof(SmeltingSkillScroll),              11 },
            { typeof(MillingSkillScroll),               7 },
            { typeof(FertilizersSkillScroll),           7 },
            { typeof(CookingSkillScroll),               14 },
            { typeof(BakingSkillScroll),                14 },
            { typeof(BlacksmithSkillScroll),            14 },
            { typeof(PaintingSkillScroll),              11 },
            { typeof(PotterySkillScroll),               14 },
            { typeof(MechanicsSkillScroll),             21 },
            { typeof(GlassworkingSkillScroll),          14 },
            { typeof(AdvancedSmeltingSkillScroll),      21 },
            { typeof(AdvancedCookingSkillScroll),       28 },
            { typeof(AdvancedBakingSkillScroll),        28 },
            { typeof(AdvancedMasonrySkillScroll),       32 },
            { typeof(OilDrillingSkillScroll),           28 },
            { typeof(ElectronicsSkillScroll),           32 },
            { typeof(CompositesSkillScroll),            32 },
            { typeof(IndustrySkillScroll),              28 },
            { typeof(CuttingEdgeCookingSkillScroll),    32 },
        };
    }

    public partial class CarpentrySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack) 
        {
            if (SkillUtils.CheckDay<CarpentrySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class MasonrySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<MasonrySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class FarmingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<FarmingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class ButcherySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<ButcherySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class TailoringSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<TailoringSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class BasicEngineeringSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<BasicEngineeringSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class PaperMillingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<PaperMillingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class ShipwrightSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<ShipwrightSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class SmeltingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<SmeltingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class MillingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<MillingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class FertilizersSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<FertilizersSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class CookingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<CookingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class BakingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<BakingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class BlacksmithSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<BlacksmithSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class PaintingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<PaintingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class PotterySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<PotterySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class MechanicsSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<MechanicsSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class GlassworkingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<GlassworkingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class AdvancedSmeltingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<AdvancedSmeltingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class AdvancedCookingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<AdvancedCookingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class AdvancedBakingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<AdvancedBakingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class AdvancedMasonrySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<AdvancedMasonrySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class OilDrillingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<OilDrillingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class ElectronicsSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<ElectronicsSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class CompositesSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<CompositesSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class IndustrySkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<IndustrySkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public partial class CuttingEdgeCookingSkillScroll
    {
        public override string OnUsed(Player player, ItemStack itemStack)
        {
            if (SkillUtils.CheckDay<CuttingEdgeCookingSkillScroll>(player)) return base.OnUsed(player, itemStack);
            else return string.Empty;
        }
    }

    public static class SkillUtils 
    {
        public static bool CheckDay<TSkill>(Player player)
        {
            // Retrieve the required day for the skill type TSkill from the Planning dictionary.
            int configDay = TSUSettings.Planning[typeof(TSkill)];

            if (WorldTime.Day < configDay)
            {
                player.User.ErrorLoc($"We are at day {Text.Num(WorldTime.Day)} and you have to wait the day {configDay} to learn this specialty.");
                return false;
            }

            return true;
        }
    }
}

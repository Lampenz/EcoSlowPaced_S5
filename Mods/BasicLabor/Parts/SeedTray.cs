namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Seed Tray")]
    [Weight(2500)]
    [RepairRequiresSkill(typeof(CarpentrySkill), 1)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("An iron tray with evenly spaced seed tubes, used in the Wheeled Seed Drill to plant seeds into tilled soil. Wears down with use.")]
    public partial class SeedTrayItem : PartItem
    {
        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(5, CarpentrySkill.MultiplicativeStrategy, typeof(CarpentrySkill), typeof(SeedTrayItem), Localizer.DoStr("repair cost"), DynamicValueType.Efficiency);

        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public override int FullRepairAmount            => 4;
        public float ReduceMaxDurabilityByPercent       => 0.05f;

        public override IEnumerable<RepairingItem> RepairItems { get
        {
            yield return new() { Item = Item.Get("IronBarItem"), MaterialMult = 5 };
        } }
    }

    [RequiresSkill(typeof(CarpentrySkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Seed Tray Item")]
    public partial class SeedTrayRecipe : RecipeFamily
    {
        public SeedTrayRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "SeedTray",  //noloc
                displayName: Localizer.DoStr("Seed Tray"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WoodenWheelItem), 2, true),
                    new IngredientElement(typeof(IronBarItem), 4, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)),
                    new IngredientElement("WoodBoard", 5, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<SeedTrayItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(CarpentrySkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(SeedTrayRecipe),
                start: 8,
                skillType: typeof(CarpentrySkill),
                typeof(CarpentryFocusedSpeedTalent),
                typeof(CarpentryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Seed Tray"),
                recipeType: typeof(SeedTrayRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

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
    [LocDisplayName("Cultivator Harrow")]
    [Weight(3000)]
    [RepairRequiresSkill(typeof(CarpentrySkill), 1)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A set of iron cultivator discs used in the Wheeled Reaper to harvest crops from fields. Wears down with use.")]
    public partial class CultivatorHarrowItem : PartItem
    {
        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(5, BlacksmithSkill.MultiplicativeStrategy, typeof(CarpentrySkill), typeof(CultivatorHarrowItem), Localizer.DoStr("repair cost"), DynamicValueType.Efficiency);

        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public override int FullRepairAmount            => 4;
        public float ReduceMaxDurabilityByPercent       => 0.05f;

        public override IEnumerable<RepairingItem> RepairItems { get
        {
            yield return new() { Item = Item.Get("IronBarItem"), MaterialMult = 5 };
        } }
    }

    [RequiresSkill(typeof(CarpentrySkill), 3)]
    [Ecopedia("Items", "Products", subPageName: "Cultivator Harrow Item")]
    public partial class CultivatorHarrowRecipe : RecipeFamily
    {
        public CultivatorHarrowRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CultivatorHarrow",  //noloc
                displayName: Localizer.DoStr("Cultivator Harrow"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronWheelItem), 2, true),
                    new IngredientElement(typeof(WoodenWheelItem), 2, true),
                    new IngredientElement("WoodBoard", 4, typeof(CarpentrySkill), typeof(CarpentryLavishResourcesTalent)), //noloc
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CultivatorHarrowItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(CarpentrySkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CultivatorHarrowRecipe),
                start: 8,
                skillType: typeof(CarpentrySkill),
                typeof(CarpentryFocusedSpeedTalent),
                typeof(CarpentryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Cultivator Harrow"),
                recipeType: typeof(CultivatorHarrowRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(CarpentryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

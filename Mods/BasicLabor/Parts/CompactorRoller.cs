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
    [LocDisplayName("Compactor Roller")]
    [Weight(2000)]
    [RepairRequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A heavy iron roller used in the Road Compactor to flatten dirt into smooth roads. Wears down with use.")]
    public partial class CompactorRollerItem : PartItem
    {
        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(5, MasonrySkill.MultiplicativeStrategy, typeof(MasonrySkill), typeof(CompactorRollerItem), Localizer.DoStr("repair cost"), DynamicValueType.Efficiency);

        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public override int FullRepairAmount            => 4;
        public float ReduceMaxDurabilityByPercent       => 0.05f;

        public override IEnumerable<RepairingItem> RepairItems { get
        {
            yield return new() { Item = Item.Get("IronBarItem"), MaterialMult = 5 };
        } }
    }

    [RequiresSkill(typeof(MasonrySkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Compactor Roller Item")]
    public partial class CompactorRollerRecipe : RecipeFamily
    {
        public CompactorRollerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "CompactorRoller",  //noloc
                displayName: Localizer.DoStr("Compactor Roller"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement("Rock", 20, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),  //noloc
                    new IngredientElement(typeof(MortarItem), 20, typeof(MasonrySkill), typeof(MasonryLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<CompactorRollerItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(MasonrySkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(CompactorRollerRecipe),
                start: 8,
                skillType: typeof(MasonrySkill),
                typeof(MasonryFocusedSpeedTalent),
                typeof(MasonryParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Compactor Roller"),
                recipeType: typeof(CompactorRollerRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

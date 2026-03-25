
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
    [LocDisplayName("Reel Blade Assembly")]
    [Weight(3000)]
    [RepairRequiresSkill(typeof(ShipwrightSkill), 1)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A set of spinning blades used in the Rustic Lawnmower to cut grass into plant fibers. Wears down with use.")]
    public partial class ReelBladeAssemblyItem : PartItem
    {
        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(5, ShipwrightSkill.MultiplicativeStrategy, typeof(ShipwrightSkill), typeof(ReelBladeAssemblyItem), Localizer.DoStr("repair cost"), DynamicValueType.Efficiency);

        public override IDynamicValue SkilledRepairCost => skilledRepairCost;
        public override int FullRepairAmount            => 4;
        public float ReduceMaxDurabilityByPercent       => 0.05f;

        public override IEnumerable<RepairingItem> RepairItems { get
        {
            yield return new() { Item = Item.Get("IronBarItem"), MaterialMult = 5 };
        } }
    }

    [RequiresSkill(typeof(ShipwrightSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Reel Blade Assembly Item")]
    public partial class ReelBladeAssemblyRecipe : RecipeFamily
    {
        public ReelBladeAssemblyRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ReelBladeAssembly",  //noloc
                displayName: Localizer.DoStr("Reel Blade Assembly"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(WoodenWheelItem), 2, true),
                    new IngredientElement(typeof(IronBarItem), 10, typeof(ShipwrightSkill),     typeof(ShipwrightLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<ReelBladeAssemblyItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 4;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(ShipwrightSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(ReelBladeAssemblyRecipe),
                start: 8,
                skillType: typeof(ShipwrightSkill),
                typeof(ShipwrightFocusedSpeedTalent),
                typeof(ShipwrightParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Reel Blade Assembly"),
                recipeType: typeof(ReelBladeAssemblyRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(SmallShipyardObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

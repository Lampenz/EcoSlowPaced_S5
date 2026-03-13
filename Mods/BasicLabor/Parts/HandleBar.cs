namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [LocDisplayName("Handle Bar")]
    [Weight(500)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("A sturdy iron bar fitted with a wooden grip. A common component used to steer and push various hand-powered tools.")]
    public partial class HandleBarItem : Item { }

    [RequiresSkill(typeof(SmeltingSkill), 1)]
    [Ecopedia("Items", "Products", subPageName: "Handle Bar Item")]
    public partial class HandleBarRecipe : RecipeFamily
    {
        public HandleBarRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "HandleBar",  //noloc
                displayName: Localizer.DoStr("Handle Bar"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(IronBarItem), 4, typeof(SmeltingSkill), typeof(SmeltingLavishResourcesTalent)),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<HandleBarItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 2;

            this.LaborInCalories = CreateLaborInCaloriesValue(150, typeof(SmeltingSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(HandleBarRecipe),
                start: 5,
                skillType: typeof(SmeltingSkill),
                typeof(SmeltingFocusedSpeedTalent),
                typeof(SmeltingParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Handle Bar"),
                recipeType: typeof(HandleBarRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(BloomeryObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

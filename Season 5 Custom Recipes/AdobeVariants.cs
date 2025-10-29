namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Core.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.SharedTypes;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.World.Water;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
    using Eco.Shared.Graphics;
    using Eco.World.Color;

    public partial class AdobeTropicalRecipe : RecipeFamily
    {
        public AdobeTropicalRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AdobeTropicalRecipe", //noloc
                displayName: Localizer.DoStr("Adobe Tropical style"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(ClayItem), 3, typeof(Skill)),
                    new IngredientElement("Wood", 1, typeof(Skill)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<AdobeItem>(4)
                });
            this.Recipes = new List<Recipe> { recipe };

            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(20);

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(0.16f);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Adobe"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Adobe Tropical style"), recipeType: typeof(AdobeTropicalRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(WorkbenchObject), recipeFamily: this);
        }
		
		/// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
	
	 public partial class AdobeSavannaRecipe : RecipeFamily
    {
        public AdobeSavannaRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "AdobeSavannaRecipe", //noloc
                displayName: Localizer.DoStr("Adobe Savanna style"),

                // Defines the ingredients needed to craft this recipe. An ingredient items takes the following inputs
                // type of the item, the amount of the item, the skill required, and the talent used.
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(DirtItem), 1, typeof(Skill)),
                    new IngredientElement(typeof(PlantFibersItem), 25, typeof(Skill)),
                },

                // Define our recipe output items.
                // For every output item there needs to be one CraftingElement entry with the type of the final item and the amount
                // to create.
                items: new List<CraftingElement>
                {
                    new CraftingElement<AdobeItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };

            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(20);

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(0.16f);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Adobe"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Adobe Savanna style"), recipeType: typeof(AdobeSavannaRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(WorkbenchObject), recipeFamily: this);
        }
		
		/// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
}
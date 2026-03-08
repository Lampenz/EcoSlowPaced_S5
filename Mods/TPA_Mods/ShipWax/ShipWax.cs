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
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Core.Items;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;
	using Eco.Gameplay.Housing.PropertyValues;
    
    [RequiresSkill(typeof(GatheringSkill), 3)]
    [Ecopedia("Items", "Products", subPageName: "Shipwax Item")]
    public partial class ShipWaxRecipe : RecipeFamily
    {
        public ShipWaxRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ShipWax",  //noloc
                displayName: Localizer.DoStr("Shipwax"),

                //Input + talents
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(CoconutItem), 2, typeof(GatheringSkill)),
                    new IngredientElement(typeof(WoodResinItem), 2, typeof(GatheringSkill)),
                    new IngredientElement(typeof(WoodPulpItem), 5, typeof(GatheringSkill)),
                },

                //Output
                items: new List<CraftingElement>
                {
                    new CraftingElement<ShipWaxItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(75, typeof(GatheringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(ShipWaxRecipe), start: 1, skillType: typeof(GatheringSkill));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Fern Campfire Salad"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Ship wax"), recipeType: typeof(ShipWaxRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(ArrastraObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }


    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("ship wax")] // Defines the localized name of the item.
    [Weight(150)] // Defines how heavy Shipwax is.
    [RepairRequiresSkill(typeof(ShipwrightSkill), 0)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("Shipwax made from coconut extract combined with wood resin and wood pulp")] //The tooltip description for the item.
    public partial class ShipWaxItem : Item    {
    }
}

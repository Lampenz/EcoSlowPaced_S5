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

    [RequiresSkill(typeof(ShipwrightSkill), 3)]
    [Ecopedia("Items", "Research Papers", subPageName: "Shipping Research Paper Basic Item")]
    public partial class ShippingResearchPaperBasicRecipe : RecipeFamily
    {
        public ShippingResearchPaperBasicRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "ShippingResearchPaperBasic",  //noloc
                displayName: Localizer.DoStr("Shipping Research Paper Basic"),

                // input
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PrimitiveResinItem), 5, typeof(ShipwrightSkill)), //noloc
                    new IngredientElement(typeof(SturdyWoodenPlankItem), 5, typeof(ShipwrightSkill)), //noloc      
                    new IngredientElement(typeof(WoodenHullPlanksItem), 5, typeof(ShipwrightSkill)), //noloc                  
                },

                // output
                items: new List<CraftingElement>
                {
                    new CraftingElement<ShippingResearchPaperBasicItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1.5f; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(30, typeof(ShipwrightSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(1);

            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Shipping Research Paper Basic"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Shipping Research Paper Basic"), recipeType: typeof(ShippingResearchPaperBasicRecipe));
            this.ModsPostInitialize();

            // Register our RecipeFamily instance with the crafting system so it can be crafted.
            CraftingComponent.AddRecipe(tableType: typeof(ResearchTableObject), recipeFamily: this);
        }

        /// <summary>Hook for mods to customize RecipeFamily before initialization. You can change recipes, xp, labor, time here.</summary>
        partial void ModsPreInitialize();

        /// <summary>Hook for mods to customize RecipeFamily after initialization, but before registration. You can change skill requirements here.</summary>
        partial void ModsPostInitialize();
    }
    
    [Serialized] // Tells the save/load system this object needs to be serialized. 
    [LocDisplayName("Shipping Research Paper Basic")] // Defines the localized name of the item.
    [Weight(10)] // Defines how heavy ShippingResearchPaperBasic is.
    [Ecopedia("Items", "Research Papers", createAsSubPage: true)]
    [Tag("Basic Research")]
    [Tag("Research")]
    [LocDescription("A document containing important research information. Used to discover new skills at the research table.")] //The tooltip description for the item.
    public partial class ShippingResearchPaperBasicItem : Item    {
    }

}

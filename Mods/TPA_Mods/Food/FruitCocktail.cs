namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using Eco.Core.Items;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.Time;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;

    [RequiresSkill(typeof(GatheringSkill), 5)]
    [Ecopedia("Food", "Campfire", subPageName: "Fruit Cocktail Item")]
    public partial class FruitCocktailRecipe : RecipeFamily
    {
        public FruitCocktailRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "FruitCocktail",  //noloc
                displayName: Localizer.DoStr("Fruit Cocktail"),

                //Input + talents
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(PapayaItem), 3, typeof(GatheringSkill)),
                    new IngredientElement(typeof(HuckleberriesItem), 5, typeof(GatheringSkill)),
                    new IngredientElement(typeof(CoconutItem), 1, typeof(GatheringSkill)),
                },

                //Output
                items: new List<CraftingElement>
                {
                    new CraftingElement<FruitCocktailItem>(1)
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1; // Defines how much experience is gained when crafted.
            
            // Defines the amount of labor required and the required skill to add labor
            this.LaborInCalories = CreateLaborInCaloriesValue(15, typeof(GatheringSkill));

            // Defines our crafting time for the recipe
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(FruitCocktailRecipe), start: 1, skillType: typeof(GatheringSkill));


            // Perform pre/post initialization for user mods and initialize our recipe instance with the display name "Fern Campfire Salad"
            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr("Fruit cocktail"), recipeType: typeof(FruitCocktailRecipe));
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
    [LocDisplayName("Fruit Cocktail")] // Defines the localized name of the item.
    [Weight(100)] // Defines how heavy the FruitCocktail is.
    [Ecopedia("Food", "Campfire", createAsSubPage: true)]
    [LocDescription("A myriad of fruits that make a healthy and juicy blend.")] //The tooltip description for the food item.
    public partial class FruitCocktailItem : FoodItem
    {
        // Prevent this item from being put on a table.
        public override bool CanBeHeld                  => false;

        /// <summary>The plural localization name for the food item.</summary>
        public override LocString DisplayNamePlural     => Localizer.DoStr("Fruit Cocktail");

        /// <summary>The amount of calories awarded for eating the food item.</summary>
        public override float Calories                  => 350;

        /// <summary>The nutritional value of the food item.</summary>
        public override Nutrients Nutrition             => new Nutrients() { Carbs = 0, Fat = 5, Protein = 0, Vitamins = 12};

        /// <summary>Defines the default time it takes for this item to spoil. This value can be modified by the inventory this item currently resides in.</summary>
        public override float BaseShelfLife            => (float)TimeUtil.HoursToSeconds(72);
    }
}

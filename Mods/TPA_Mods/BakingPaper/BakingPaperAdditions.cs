namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Items;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Modules;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    using Eco.Gameplay.Housing.PropertyValues;
    using Eco.Gameplay.Civics.Objects;
    using Eco.Gameplay.Settlements;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Core.Controller;
    using Eco.Core.Utils;
	using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Items.Recipes;
    

    // Add BakingPaper to bakedAgave
    public partial class BakedAgaveRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to bakedbeet
    public partial class BakedBeetRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }
    // Add BakingPaper to CamasBulbBake
    public partial class CamasBulbBakeRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to bakedcorn
    public partial class BakedCornRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to baked Heart of palm
    public partial class BakedHeartOfPalmRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to baked taro
    public partial class BakedTaroRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to bake tomato
    public partial class BakedTomatoRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }


    // Add BakingPaper to baked meat
    public partial class BakedMeatRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to baked roast
    public partial class BakedRoastRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to bread
    public partial class BreadRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to camasbread
    public partial class CamasBreadRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to FlatBread
    public partial class FlatbreadRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to Huckleberry Fritter
    public partial class HuckleberryFritterRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to PastryDough
    public partial class PastryDoughRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to PastryDoughRecipe
    public partial class RoastPumpkinRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to StuffedTurkeyRecipe
    public partial class StuffedTurkeyRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to TastytropicalPizza
    public partial class TastyTropicalPizzaRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to SensuousSeaPizza
    public partial class SensuousSeaPizzaRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to FantasticForestPizza
    public partial class FantasticForestPizzaRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to HeartyHometownPizza
    public partial class HeartyHometownPizzaRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to WorldlyDonut
    public partial class WorldlyDonutRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to Bearclaw
    public partial class BearclawRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to ElkWellingtonRecipe
    public partial class ElkWellingtonRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to MacaronsRecipe
    public partial class MacaronsRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }

    // Add BakingPaper to PirozhokRecipe
    public partial class PirozhokRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(BakingPaperItem), 1, true));
        }
    }  
}

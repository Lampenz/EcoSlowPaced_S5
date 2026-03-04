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


    // Add BakingPaper to FruitMuffin
    public partial class FruitMuffinRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(TartBakingMoldItem), 1, true));
        }
    }

    // Maybe also add tartshaped paper?
    // Add BakingPaper to FruitTart
    public partial class FruitTartRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(TartBakingMoldItem), 1, true));
        }
    }

    // add tartshaped paper?
    // Add BakingPaper to HuckleberryPie
    public partial class HuckleberryPieRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(TartBakingMoldItem), 1, true));
        }
    }

    // add tartshaped paper?
    // Add BakingPaper to Meatpie
    public partial class MeatPieRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(TartBakingMoldItem), 1, true));
        }
    }
}

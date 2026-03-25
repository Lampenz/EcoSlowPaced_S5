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
    

    // Add rough filter fabric to clay molds.
    public partial class ClayMoldRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 1, true));
        }
    }

    public partial class WoodenMoldRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 1, true));
        }
    }

    public partial class RockerBoxRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 5, true));
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class MechanicalWaterPumpRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 16, true));
        }
    }

    public partial class WasteFilterRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 40, true));
        }
    }

    public partial class YellowPowderRecipe : RecipeFamily    
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class WhitePowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class MagentaPowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class CyanPowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class ColoredPowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class CharcoalPowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class BluePowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

    public partial class BlackPowderRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(RoughFabricFilterItem), 2, true));
        }
    }

}

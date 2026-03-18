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
 
    //SmallWoodenBoatRecipe -> Found in SturdyWoodenPlanksAdditions.cs
    //WoodenBargeRecipe -> Found in SturdyWoodenPlanksAdditions.cs
    //WoodenTransportShipRecipe -> Found in SturdyWoodenPlanksAdditions.cs
    //MediumFishingTrawlerRecipe -> Found in SturdyWoodenPlanksAdditions.cs

    public partial class LumberBenchRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class LumberChairRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class LumberDoorRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class LumberDresserRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class LumberHallwayTableRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 1, true));
        }
    }

    public partial class LumberTableRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
        }
    }

    public partial class DecorativeShipWheelRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 3, true));
        }
    }

    // Rockerbox shipwax additions is added in rough fabric filter 
    // public partial class RockerBoxRecipe : RecipeFamily
    //{
    //    partial void ModsPostInitialize()
    //    {
    //        this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 2, true));
    //    }
    //}



}

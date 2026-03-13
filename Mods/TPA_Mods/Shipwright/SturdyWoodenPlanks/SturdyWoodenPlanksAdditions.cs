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
    
    #region Water vehicles

    // Add Sturdywoodenplanks to large canoe
    public partial class LargeCanoeRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 5, true));
        }
    }

 //   public partial class EgyptianCanoeRecipe : Recipe
 //   {
 //       partial void ModsPostInitialize()
 //       {
 //           this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 5, true));
 //       }
 //   }
    
    
    // Add Sturdywoodenplanks to Small wooden boat
    public partial class SmallWoodenBoatRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 8, true));
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 5, true));
        }
    }

    // Add Sturdywoodenplanks to wooden barge
    public partial class WoodenBargeRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 20, true));
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 10, true));
        }
    }

    // Add Sturdywoodenplanks to WoodenTransportShip
    public partial class WoodenTransportShipRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 10, true));
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 14, true));
        }
    }

    // Add Sturdywoodenplanks to MediumFishingTrawler
    public partial class MediumFishingTrawlerRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 10, true));
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ShipWaxItem), 10, true));
        }
    }


    #endregion

    #region land-vehicles

  // Add Sturdywoodenplanks to small wood cart
    public partial class SmallWoodCartRecipe : RecipeFamily
    {
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 1, true));
        }
    }

    // Add Sturdywoodenplanks to woodcart
    public partial class WoodCartRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 2, true));
        }
    }


    // Add Sturdywoodenplanks to WoodShopCart
    public partial class WoodShopCartRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 4, true));
        }
    }

    // Add Sturdywoodenplanks to WoodenElevator
    public partial class WoodenElevatorRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 4, true));
        }
    }

    // Add Sturdywoodenplanks to SteamTruck
    public partial class SteamTruckRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 3, true));
        }
    }

    // Add Sturdywoodenplanks to SteamTractor
    public partial class SteamTractorRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 3, true));
        }
    }

    // Add Sturdywoodenplanks to PoweredCart
    public partial class PoweredCartRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 2, true));
        }
    }

    // Add Sturdywoodenplanks to HandPlow
    public partial class HandPlowRecipe : RecipeFamily
    {        
        partial void ModsPostInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 1, true));
        }
    }
    #endregion


    #region Lumber additions.
    // Add Hullplanks to lumber.
    public partial class LumberRecipe : RecipeFamily
    {
        partial void ModsPreInitialize()
        {
            this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 1, true));
        }
    }

    // Add hullplanks to lumber
    public partial class SoftwoodLumberRecipe : Recipe
    {
        partial void ModsPostInitialize() // Use post initialise because this recipe doesnt have a preinit hook.
        {
            this.Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 1, true));
        }
    } 

    // Add hullplanks to lumber
    public partial class HardwoodLumberRecipe : Recipe
    {
        partial void ModsPostInitialize() // Use post initialise because this recipe doesnt have a preinit hook.
        {
            this.Ingredients.Add(new IngredientElement(typeof(SturdyWoodenPlankItem), 1, true));
        }
    } 
    #endregion


}

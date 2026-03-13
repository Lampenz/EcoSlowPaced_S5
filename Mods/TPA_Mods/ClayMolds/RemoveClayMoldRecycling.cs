using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Items;
using System;

namespace Eco.Mods.TechTree
{
	public static class Utils
    {
        public static void RemoveMolds(RecipeFamily recipeFamily, Type moldItem, int quantity)
        {
            var recipe = recipeFamily.Recipes[0];
            var products = recipe.Products;
            var ingredients = recipe.Ingredients;

            // Remove the mold as a product
            products.RemoveAt(products.FindIndex(x => x.Item.Type == moldItem));

            // Remove it as an ingredient
            ingredients.RemoveAt(ingredients.FindIndex(x => x.Item.Type == moldItem));

            // Add it back at the updated quantity (usually 50% less than original) and static
            ingredients.Add(new IngredientElement(moldItem, quantity, true));
        }
    }
    
    public partial class SmeltCopperRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 3);
        }
    }
    
    public partial class CopperBarRecipe : RecipeFamily
    {
		partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 2);
        }
	}
    
    public partial class SmeltGoldRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 2);
        }
	}
    
    public partial class GoldBarRecipe : RecipeFamily
    {
		partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 2);
        }
	}
    
    public partial class SmeltIronRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 2);
        }
	}
    
    public partial class IronBarRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(ClayMoldItem), 2);
        }
	}
    
    public partial class SteelBarRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(CeramicMoldItem), 2);
        }
    }
    
    public partial class CharcoalSteelRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	Utils.RemoveMolds(this, typeof(CeramicMoldItem), 2);
        }
    }
    
    public partial class CeramicMoldRecipe : RecipeFamily
    {
    	partial void ModsPreInitialize()
        {
        	this.Recipes[0].Ingredients.Add(new IngredientElement(typeof(ClayMoldItem), 1, true));
        }
    }
}
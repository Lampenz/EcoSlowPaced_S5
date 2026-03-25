using Eco.Core.Items;
using Eco.Core.Plugins.Interfaces;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

public class NoMoreBooksMod : IModInit
{
    public static ModRegistration Register() => new()
    {
        ModName = "NoMoreBooks",
        ModDescription = "Changes all book recipes to instead create a configurable amount of scrolls and also allows configuration of the crafting costs for skill scrolls.",
        ModDisplayName = "No More Books",
    };
}

namespace Eco.Mods.TechTree
{
    public static class NMBSettings
    {
        // How many scrolls should be created per craft
        public const int OutputAmount = 2;

        // Percentage modifiers for recipe ingredient costs
        public static readonly Dictionary<Type, float> IngredientModifiers = new Dictionary<Type, float>
        {
            { typeof(CarpentrySkillBookRecipe),             1.0f },
            { typeof(MasonrySkillBookRecipe),               1.0f },
            { typeof(FarmingSkillBookRecipe),               1.0f },
            { typeof(ButcherySkillBookRecipe),              1.0f },
            { typeof(TailoringSkillBookRecipe),             1.0f },
            { typeof(BasicEngineeringSkillBookRecipe),      1.0f },
            { typeof(PaperMillingSkillBookRecipe),          0.6f },
            { typeof(ShipwrightSkillBookRecipe),            0.8f },
            { typeof(SmeltingSkillBookRecipe),              1.0f },
            { typeof(MillingSkillBookRecipe),               1.0f },
            { typeof(FertilizersSkillBookRecipe),           1.0f },
            { typeof(CookingSkillBookRecipe),               1.0f },
            { typeof(BakingSkillBookRecipe),                1.0f },
            { typeof(BlacksmithSkillBookRecipe),            1.0f },
            { typeof(PaintingSkillBookRecipe),              0.6f },
            { typeof(PotterySkillBookRecipe),               1.0f },
            { typeof(MechanicsSkillBookRecipe),             1.0f },
            { typeof(GlassworkingSkillBookRecipe),          1.0f },
            { typeof(AdvancedSmeltingSkillBookRecipe),      1.0f },
            { typeof(AdvancedCookingSkillBookRecipe),       0.8f },
            { typeof(AdvancedBakingSkillBookRecipe),        0.8f },
            { typeof(AdvancedMasonrySkillBookRecipe),       0.6f },
            { typeof(OilDrillingSkillBookRecipe),           0.8f },
            { typeof(ElectronicsSkillBookRecipe),           0.8f },
            { typeof(CompositesSkillBookRecipe),            0.7f },
            { typeof(IndustrySkillBookRecipe),              0.8f },
            { typeof(CuttingEdgeCookingSkillBookRecipe),    0.5f },
        };
    }

    public partial class AdvancedBakingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<AdvancedBakingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
			recipe.Ingredients.Add(new IngredientElement(typeof(BakeryOvenItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class AdvancedCookingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<AdvancedCookingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(KitchenItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class AdvancedMasonrySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<AdvancedMasonrySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(AdvancedMasonryTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class AdvancedSmeltingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<AdvancedSmeltingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(BlastFurnaceItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class BakingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<BakingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(BakeryOvenItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class BasicEngineeringSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<BasicEngineeringSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(WainwrightTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class ButcherySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<ButcherySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(ButcheryTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class CarpentrySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<CarpentrySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(CarpentryTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class CompositesSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<CompositesSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(AdvancedCarpentryTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class CookingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<CookingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(CastIronStoveItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class CuttingEdgeCookingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<CuttingEdgeCookingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(LaboratoryItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class ElectronicsSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<ElectronicsSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(EpoxyItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class FarmingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<FarmingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(FarmersTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class FertilizersSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<FertilizersSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(FarmersTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class GlassworkingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<GlassworkingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }

            recipe.Ingredients.Add(new IngredientElement(typeof(ShippingResearchPaperBasicItem), 5, true));            
            recipe.Ingredients.Add(new IngredientElement(typeof(GlassworksItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class IndustrySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<IndustrySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class MasonrySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<MasonrySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(MasonryTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class MechanicsSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<MechanicsSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(MachinistTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class MillingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<MillingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(MillItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class OilDrillingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<OilDrillingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class PaperMillingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<PaperMillingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(SmallPaperMachineItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class PotterySkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<PotterySkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(TailoringResearchPaperBasicItem), 5, true));
            recipe.Ingredients.Add(new IngredientElement(typeof(KilnItem), 1, true));

        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class SmeltingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<SmeltingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(BloomeryItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class TailoringSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<TailoringSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(TailoringTableItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class ShipwrightSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<ShipwrightSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(SmallShipyardItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class BlacksmithSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<BlacksmithSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(AnvilItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }

    public partial class PaintingSkillBookRecipe
    {
        partial void ModsPreInitialize()
        {
            Recipes[0].Products.Clear();
            Recipes[0].Products.Add(new CraftingElement<PaintingSkillScroll>(NMBSettings.OutputAmount));

            Recipe recipe = Recipes[0];
            for (int i = 0; i < recipe.Ingredients.Count(); ++i)
            {
                IngredientElement ingredient = recipe.Ingredients[i];
                recipe.Ingredients[i] = recipe.Ingredients[i].IsSpecificItem
                    ? new IngredientElement(ingredient.ItemRepresentation, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true)
                    : new IngredientElement(ingredient.InnerName, ingredient.Quantity.GetBaseValue * NMBSettings.IngredientModifiers[this.GetType()], true);
            }
            
            recipe.Ingredients.Add(new IngredientElement(typeof(PaintMixerItem), 1, true));
        }

        partial void ModsPostInitialize()
        {
            RecipeName = $"{Recipes[0].Name.AddSpacesBetweenCapitals()} Skill Scroll";
        }
    }
}

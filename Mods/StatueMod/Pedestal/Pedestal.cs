using Eco.Core.Controller;
using Eco.Core.Items;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Items.Recipes;
using Eco.Gameplay.Players;
using Eco.Gameplay.Skills;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoPulse.StatueMod
{
    [Serialized]
    [LocDisplayName("Pedestal")]
    [Weight(1000)]
    [Ecopedia("Items", "Products", createAsSubPage: true)]
    [LocDescription("The pedestal for the statues made of gold, glass or copper")]
    [MaxStackSize(10)]
    public class PedestalItem : Item
    {

    }

    [RequiresSkill(typeof(MasonrySkill), 5)]
    [Ecopedia("Items", "Products", subPageName: "Pedestal Item")]
    public partial class PedestalRecipe : RecipeFamily
    {
        public PedestalRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "Pedestal",  //noloc
                displayName: Localizer.DoStr("Pedestal"),

                ingredients: new List<IngredientElement>
                {
                new IngredientElement(typeof(MortaredLimestoneItem), 20),
                },

                items: new List<CraftingElement>
                {
                new CraftingElement<PedestalItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;

            this.LaborInCalories = CreateLaborInCaloriesValue(50);

            this.CraftMinutes = CreateCraftTimeValue(start: 1);

            this.ModsPreInitialize();
            this.Initialize(displayText: Localizer.DoStr($"Pedestal"), recipeType: typeof(PedestalRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(MasonryTableObject), this);
        }

        partial void ModsPreInitialize();

        partial void ModsPostInitialize();
    }
}

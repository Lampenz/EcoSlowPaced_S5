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
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Core.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.SharedTypes;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.World.Water;
    using Eco.Gameplay.Pipes;
    using Eco.Core.Controller;
    using Eco.Gameplay.Items.Recipes;

    [RequiresSkill(typeof(FarmingSkill), 2)]
    public partial class HedgeRecipe : RecipeFamily
    {
        public HedgeRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "GardenHedges",
                displayName: Localizer.DoStr("Garden Hedges"),
                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(DirtItem), 1, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(PlantFibersItem), 10, typeof(FarmingSkill), typeof(FarmingLavishResourcesTalent)),
                    new IngredientElement(typeof(WoodPulpItem), 5, true),         
                },
                items: new List<CraftingElement>
                {
                    new CraftingElement<HedgeItem>()
                });
            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 1;
            this.LaborInCalories = CreateLaborInCaloriesValue(40, typeof(FarmingSkill));
            this.CraftMinutes = CreateCraftTimeValue(beneficiary: typeof(HedgeRecipe), start: 1.5f, skillType: typeof(FarmingSkill), typeof(FarmingFocusedSpeedTalent), typeof(FarmingParallelSpeedTalent));

            this.Initialize(displayText: Localizer.DoStr("Garden Hedges"), recipeType: typeof(HedgeRecipe));

            CraftingComponent.AddRecipe(tableType: typeof(FarmersTableObject), recipeFamily: this);
        }

    }

    [Serialized]
    [LocDisplayName("Garden Hedges")]
	[LocDescription("Hedges for your garden")]
    [MaxStackSize(20)]                           
    [Weight(1000)]
    [Ecopedia("Blocks", "Building Materials", createAsSubPage: true)]                  
    [Currency][Tag("Currency")]                              
    [Tag("Constructable")]                     
    [Tier(0)]                                      
    public partial class HedgeItem : BlockItem<HedgeBlock>
    {

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(HedgeCubeStacked1Block),
            typeof(HedgeCubeStacked2Block),
            typeof(HedgeCubeStacked3Block),
            typeof(HedgeCubeStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

	[Tag("Constructable")]
	[Tag(BlockTags.PartialStack)]
    [Serialized, Solid] public class HedgeCubeStacked1Block : PickupableBlock, IWaterLoggedBlock { }
	[Tag("Constructable")]
    [Tag(BlockTags.PartialStack)]
    [Serialized, Solid] public class HedgeCubeStacked2Block : PickupableBlock, IWaterLoggedBlock { }
	[Tag("Constructable")]
    [Tag(BlockTags.PartialStack)]
    [Serialized, Solid] public class HedgeCubeStacked3Block : PickupableBlock, IWaterLoggedBlock { }
	[Tag("Constructable")]
    [Tag(BlockTags.FullStack)]
    [Serialized, Solid,Wall] public class HedgeCubeStacked4Block : PickupableBlock, IWaterLoggedBlock { }

}
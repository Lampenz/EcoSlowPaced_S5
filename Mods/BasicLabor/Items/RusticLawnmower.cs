namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Core.Controller;
    using Eco.Core.Items;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.Storage;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Occupancy;
    using Eco.Gameplay.Systems.Exhaustion;
    using Eco.Gameplay.Systems.NewTooltip;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Items;
    using Eco.Gameplay.Items.Recipes;
    using static Eco.Gameplay.Components.PartsComponent;

    [Serialized]
    [LocDisplayName("Rustic Lawnmower")]
    [LocDescription("A primitive reel mower that cuts grass with nothing but muscle power. Slow but available long before modern machinery.")]
    [IconGroup("World Object Minimap")]
    [Weight(5000)]
    [Tag("Vehicles")]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class RusticLawnmowerItem : WorldObjectItem<RusticLawnmowerObject>, IPersistentData
    {
        [Serialized, SyncToView, NewTooltipChildren(CacheAs.Instance, flags: TTFlags.AllowNonControllerTypeForChildren)]
        public object PersistentData { get; set; }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PaintableComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(PartsComponent))]
    [RequireComponent(typeof(LawnmowerComponent))]
    [RequireComponent(typeof(StorageFillVisualComponent))]
    [RepairRequiresSkill(typeof(BlacksmithSkill), 1)]
    [ExhaustableUnlessOverridenVehicle]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Rustic Lawnmower Item")]
    public partial class RusticLawnmowerObject : PhysicsWorldObject, IRepresentsItem
    {
        static RusticLawnmowerObject()
        {
            WorldObject.AddOccupancy<RusticLawnmowerObject>(new List<BlockOccupancy>(0));
        }

        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName => Localizer.DoStr("Rustic Lawnmower");
        public Type RepresentedItemType => typeof(RusticLawnmowerItem);

        private RusticLawnmowerObject() { }

        protected override void Initialize()
        {
            this.ModsPreInitialize();
            base.Initialize();
            this.GetComponent<VehicleComponent>().HumanPowered(1.5f);
            this.GetComponent<MinimapComponent>().InitAsMovable();
            this.GetComponent<MinimapComponent>().SetCategory(Localizer.DoStr("Vehicles"));
            this.GetComponent<VehicleComponent>().Initialize(3, 1, 1);
            this.GetComponent<VehicleComponent>().FailDriveMsg =
                Localizer.Do($"You are too hungry to push this {this.DisplayName}!");
            // Grass catcher: 3 slots, plant fibers only
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(3);
            storage.Inventory.AddInvRestriction(new SpecificItemTypesRestriction(typeof(PlantFibersItem)));
            this.ModsPostInitialize();
            {
                this.GetComponent<PartsComponent>().Config(() => LocString.Empty, new PartInfo[]
                {
                    new() { TypeName = nameof(ReelBladeAssemblyItem), Quantity = 1 },
                    new() { TypeName = nameof(WoodenGearItem), Quantity = 2 },
                });
            }
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 3)]
    public partial class RusticLawnmowerRecipe : RecipeFamily
    {
        public RusticLawnmowerRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "RusticLawnmower",  //noloc
                displayName: Localizer.DoStr("Rustic Lawnmower"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HandleBarItem), 1, true),
                    new IngredientElement(typeof(ReelBladeAssemblyItem), 1, true),
                    new IngredientElement(typeof(WoodenGearItem), 2, true),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<RusticLawnmowerItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 8;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(BasicEngineeringSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(RusticLawnmowerRecipe),
                start: 8,
                skillType: typeof(BasicEngineeringSkill),
                typeof(BasicEngineeringFocusedSpeedTalent),
                typeof(BasicEngineeringParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Rustic Lawnmower"),
                recipeType: typeof(RusticLawnmowerRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(WainwrightTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}

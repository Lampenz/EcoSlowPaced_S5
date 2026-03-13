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
    [LocDisplayName("Wheeled Seed Drill")]
    [LocDescription("A rudimentary wheeled planter that sows seeds as you push it through tilled soil. A farmer's first step toward efficient planting.")]
    [IconGroup("World Object Minimap")]
    [Weight(6000)]
    [Tag("Vehicles")]
    [Ecopedia("Crafted Objects", "Vehicles", createAsSubPage: true)]
    public partial class WheeledSeedDrillItem : WorldObjectItem<WheeledSeedDrillObject>, IPersistentData
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
    [RequireComponent(typeof(SeedDrillComponent))]
    [RequireComponent(typeof(StorageFillVisualComponent))]
    [RepairRequiresSkill(typeof(BlacksmithSkill), 1)]
    [ExhaustableUnlessOverridenVehicle]
    [Ecopedia("Crafted Objects", "Vehicles", subPageName: "Wheeled Seed Drill Item")]
    public partial class WheeledSeedDrillObject : PhysicsWorldObject, IRepresentsItem
    {
        static WheeledSeedDrillObject()
        {
            WorldObject.AddOccupancy<WheeledSeedDrillObject>(new List<BlockOccupancy>(0));
        }

        public override TableTextureMode TableTexture => TableTextureMode.Metal;
        public override bool PlacesBlocks            => false;
        public override LocString DisplayName => Localizer.DoStr("Wheeled Seed Drill");
        public Type RepresentedItemType => typeof(WheeledSeedDrillItem);

        private WheeledSeedDrillObject() { }

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
            // Seed hopper: 3 slots, seeds only
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(3);
            storage.Inventory.AddInvRestriction(new SeedRestriction());
            this.ModsPostInitialize();
            {
                this.GetComponent<PartsComponent>().Config(() => LocString.Empty, new PartInfo[]
                {
                    new() { TypeName = nameof(SeedTrayItem), Quantity = 1 },
                    new() { TypeName = nameof(WoodenGearItem), Quantity = 2 },
                });
            }
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 2)]
    public partial class WheeledSeedDrillRecipe : RecipeFamily
    {
        public WheeledSeedDrillRecipe()
        {
            var recipe = new Recipe();
            recipe.Init(
                name: "WheeledSeedDrill",  //noloc
                displayName: Localizer.DoStr("Wheeled Seed Drill"),

                ingredients: new List<IngredientElement>
                {
                    new IngredientElement(typeof(HandleBarItem), 1, true),
                    new IngredientElement(typeof(SeedTrayItem), 1, true),
                    new IngredientElement(typeof(WoodenGearItem), 2, true),
                },

                items: new List<CraftingElement>
                {
                    new CraftingElement<WheeledSeedDrillItem>()
                });

            this.Recipes = new List<Recipe> { recipe };
            this.ExperienceOnCraft = 8;

            this.LaborInCalories = CreateLaborInCaloriesValue(200, typeof(BasicEngineeringSkill));

            this.CraftMinutes = CreateCraftTimeValue(
                beneficiary: typeof(WheeledSeedDrillRecipe),
                start: 8,
                skillType: typeof(BasicEngineeringSkill),
                typeof(BasicEngineeringFocusedSpeedTalent),
                typeof(BasicEngineeringParallelSpeedTalent));

            this.ModsPreInitialize();
            this.Initialize(
                displayText: Localizer.DoStr("Wheeled Seed Drill"),
                recipeType: typeof(WheeledSeedDrillRecipe));
            this.ModsPostInitialize();

            CraftingComponent.AddRecipe(tableType: typeof(WainwrightTableObject), recipeFamily: this);
        }

        partial void ModsPreInitialize();
        partial void ModsPostInitialize();
    }
}
